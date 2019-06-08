using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static COOLgrammarParser;
using static CmpProject.TypeCool;
namespace CmpProject
{
    public class FeatureBuilderVisitor : IVisitor<ProgramContext>, IVisitor<ClassContext>, IVisitor<FeatureContext>,
        IVisitor<MethodContext>, IVisitor<AttributeContext>, IVisitor<FormalContext,Formal>, IVisitor<DeclarationContext>
    {
        public IGlobalContext globalContext;
        private IType type;
        public IErrorLogger errorLogger;
        public BasicTypes basicTypes;
        public FeatureBuilderVisitor(TypeBuilderVisitor typeBuilder)
        {
            basicTypes = typeBuilder.basicTypes;
            globalContext = typeBuilder.globalContext;
            errorLogger = typeBuilder.errorLogger;
        }
        public void Visit(ProgramContext parserRule)
        {
            var temp = Preprocecing(parserRule._classes);
            foreach (var _class in temp) {
                Visit(_class);
            }
        }
        public void Visit(ClassContext parserRule)
        {
            type = globalContext.GetType(parserRule.type.Text);
            if (parserRule.inherits != null)
            {    
                if (!globalContext.IfDefineType(parserRule.inherits.Text))
                    errorLogger.LogError($"El tipo con nombre {parserRule.inherits.Text} no ha sido encontrado");
                if (globalContext.Int.Name== parserRule.inherits.Text)
                    errorLogger.LogError($"El tipo {parserRule.type.Text} no puede heredar de Int");
                if (globalContext.String.Name == parserRule.inherits.Text)
                    errorLogger.LogError($"El tipo {parserRule.type.Text} no puede heredar de String");
                if (globalContext.Bool.Name == parserRule.inherits.Text)
                    errorLogger.LogError($"El tipo {parserRule.type.Text} no puede heredar de Bool");
                type.Inherits = globalContext.GetType(parserRule.inherits.Text);
            }
            foreach (var item in parserRule._features)
            {
                Visit(item);
            }
            //Verifico si la clase Main tiene el metodo main y si este no tiene parametros
            if (parserRule.type.Text=="Main")
            {
                if (type.IsDefineMethod("main"))
                {
                    var main = type.GetMethod("main");
                    if (main.Formals.Length>0)
                        errorLogger.LogError("El metodo main no tiene parametros");
                }
                else
                    errorLogger.LogError("El metodo main no esta definido en la clase Main");
            }
        }
        //Este tipo de visitor siempre retorna algo
        public Formal Visit(FormalContext parserRule)
        {
            return new Formal(parserRule.idText, globalContext.GetType(parserRule.typeText));

        }

        public void Visit(FeatureContext parserRule)
        {
            switch (parserRule)
            {
                case AttributeContext attribute:
                    Visit(attribute);
                    break;
                case MethodContext method:
                    Visit(method);
                    break;
                default:
                    break;
            }
        }

        public void Visit(AttributeContext parserRule)
        {
            Visit(parserRule.decl);
        }

        public void Visit(MethodContext parserRule)
        {
            var methodName = parserRule.methodName.Text;
            var typeReturn = globalContext.GetType(parserRule.TypeReturn.Text);
            var formals = (from f in parserRule._formals select Visit(f)).ToArray();
            if (!type.IfDefineInTheCurrentType(methodName))
            {   //Si no esta definida en la clase actual hay que verificar si se puede refinir de un ancestro
                if (!type.Define(methodName,typeReturn,formals))
                    errorLogger.LogError($"No se puede redefinir el metodo con nombre {parserRule.idText}, linea {parserRule.methodName.Line} y la columna {parserRule.methodName.Column}");
            }
            else
                errorLogger.LogError($"El tipo {type.Name} ya tiene un metodo con nombre {parserRule.idText}, linea {parserRule.methodName.Line} y la columna {parserRule.methodName.Column}");
            //if ( !type.Define(methodName, type, formals))
            //     errorLogger.LogError($"El tipo {type.Name} ya tiene un metodo con nombre {parserRule.idText}, linea {parserRule.methodName.Line} y la columna {parserRule.methodName.Column}");
        }

        public void Visit(DeclarationContext parserRule)
        {
           if( !type.Define(parserRule.idText, globalContext.GetType(parserRule.typeText),parserRule.expression))
                errorLogger.LogError($"El tipo {type.Name} ya tiene un atributo con nombre {parserRule.idText}, linea {parserRule.id.Line} y la columns {parserRule.id.Column}");
        }
        //Esta funcion clacula el orden topologico de las clases
        public ClassContext[] Preprocecing(IList<ClassContext> classes)
        {
            var stack = new Stack<ClassContext>();
            for (int i = 0; i < classes.Count; i++)
                DFS_Visit(stack, classes[i]);
            return stack.Reverse().ToArray();
        }
        private void DFS_Visit(Stack<ClassContext>stack,ClassContext @class)
        {
            if (@class==null|| stack.Contains(@class))
                return;
            DFS_Visit(stack,@class.father);
            stack.Push(@class);
        }
    }
}
