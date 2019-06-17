using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using static COOLgrammarParser;
using static CmpProject.TypeCool;
namespace CmpProject
{
    
   public class CheckSemanticVisitor:IVisitor<ProgramContext>,IVisitor<ClassContext>,IVisitor<FeatureContext>,
        IVisitor<MethodContext>,IVisitor<AttributeContext>,
        IVisitorC<FormalContext,IObjectContext<IVar,IVar>>,
        IVisitorC<ExprContext, IObjectContext<IVar, IVar>>,
        IVisitorC<DeclarationContext,IObjectContext<IVar,IVar>>,
        IVisitorC<SelfDispatchContext,IObjectContext<IVar,IVar>>,
        IVisitorC<CondExprContext, IObjectContext<IVar, IVar>>,
        IVisitorC<WhileExprContext, IObjectContext<IVar, IVar>>,
        IVisitorC<BlockExprContext, IObjectContext<IVar, IVar>>,
        IVisitorC<LetExprContext, IObjectContext<IVar, IVar>>,
        IVisitorC<LetRuleContext,IObjectContext<IVar, IVar>>,
        IVisitorC<LetDeclContext, IObjectContext<IVar, IVar>>,
        IVisitorC<LetBodyContext, IObjectContext<IVar, IVar>>,
        IVisitorC<CaseExprContext, IObjectContext<IVar, IVar>>,
        IVisitorC<BranchContext,IObjectContext<IVar,IVar>>,
        IVisitorC<NewTypeExprContext, IObjectContext<IVar, IVar>>,
        IVisitorC<AssignExprContext, IObjectContext<IVar, IVar>>,
        IVisitorC<NotExprContext, IObjectContext<IVar, IVar>>,
        IVisitorC<CompaExprContext, IObjectContext<IVar, IVar>>,
        IVisitorC<ArithContext, IObjectContext<IVar, IVar>>,
        IVisitorC<IsvoidExprContext, IObjectContext<IVar, IVar>>,
        IVisitorC<NegExprContext, IObjectContext<IVar, IVar>>,
        IVisitorC<DispatchContext, IObjectContext<IVar, IVar>>,
        IVisitorC<InParenthesisExprContext, IObjectContext<IVar, IVar>>,
        IVisitorC<IdExprContext, IObjectContext<IVar, IVar>>,
        IVisitorC<IntegerExprContext, IObjectContext<IVar, IVar>>,
        IVisitorC<StringExprContext, IObjectContext<IVar, IVar>>,
        IVisitorC<BoolExprContext, IObjectContext<IVar, IVar>>
    {
        public IGlobalContext globalContext;
        //Con el tipo tambien accedemos al contexto metodos de este
        public IType type;
        public IErrorLogger errorLogger;
        public BasicTypes basicTypes { get; set; }
        public CheckSemanticVisitor(FeatureBuilderVisitor feature)
        {
           basicTypes = feature.basicTypes;
            globalContext = feature.globalContext;
            errorLogger = feature.errorLogger;
        }
        public void Visit(ProgramContext parserRuleContext)
        {
            //Evaluo la funcion de entrada para calcular su tipo estatico
            Visit(basicTypes.entry);
            foreach (var _class in parserRuleContext._classes)
                Visit(_class);
        }
        //El unico chequeo que hay que ser es si el tipo heredado existe
        public void Visit(ClassContext parserRuleContext)
        {
            type = globalContext.GetType(parserRuleContext.type.Text);
            foreach (var item in parserRuleContext._features)
                Visit(item);
        }
        public void Visit(FeatureContext parserRuleContext)
        {
            switch (parserRuleContext)
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
        public void Visit(AttributeContext parserRuleContext)
        {
            //Al definir un atributo creo un nuevo contexto
            Visit(parserRuleContext.decl, new ObjectContext(type));
        }
        public void Visit(MethodContext parserRuleContext)
        {
            //Creo un nuevo contexto dentro de metodos
            var context = new ObjectContext(type);
            //O_C[SELF_TYPE_type/self] defino la variable self con tipo SELF_TYPE_type
            context.Define("self", new SelfType(type, globalContext));
            foreach (var formal in parserRuleContext._formals)
                Visit(formal,context);
            //Porque los tipo IO y Object no tienen implementacion
            if (type==globalContext.IO||type==globalContext.Object||type==globalContext.Int|| type == globalContext.String)
                return;
            //Visito el cuerpo del metodo
            Visit(parserRuleContext.exprBody,context);
            var typeMethod = globalContext.GetType(parserRuleContext.TypeReturn.Text,type);
            //si al menos un tipo es undefined no se reporta error plq la expresion es null
            //Alomejor hay que especificar el nombre del metodo en el error
            if (!parserRuleContext.exprBody.computedType.Conform(typeMethod))
                errorLogger.LogError($"El tipo {parserRuleContext.exprBody.computedType.Name} no conforma al tipo {typeMethod.Name}, linea {parserRuleContext.exprBody.Start.Line} y columna {parserRuleContext.exprBody.Start.Column+1}");
        }
        public void Visit(FormalContext parserRule,IObjectContext<IVar,IVar> context)
        {
            context.Define(parserRule.idText, globalContext.GetType(parserRule.typeText)); 
        }
        public void Visit(DeclarationContext parserRule,IObjectContext<IVar,IVar> context)
        {
            if (!globalContext.IfDefineType(parserRule.typeText))
                errorLogger.LogError($"El tipo con nombre {parserRule.typeText} no ha sido encontrado en el programa, linea {parserRule.type.Line} y columna {parserRule.type.Column+1}");    
            if (parserRule.expression != null)
            {
                var TypeDec = globalContext.GetType(parserRule.typeText,type);
                //O_C[SELF_TYPE_type/self] defino la variable self con tipo SELF_TYPE_type
                context.Define("self", new SelfType(type, globalContext));
                Visit(parserRule.expression,context);
                //Despues de visitar la expresion verifico si su tipo estatico hereda del tipo declarado
                if (!parserRule.expression.computedType.Conform(TypeDec))
                    errorLogger.LogError($"El tipo {parserRule.expression.computedType.Name} no conforma al tipo {parserRule.typeText}, linea {parserRule.expression.Start.Line} y columna {parserRule.expression.Start.Column+1}");
            }
            //Defino la declaracion aun si el tipo no esta definido y despues de haber entrado
            context.Define(parserRule.idText, globalContext.GetType(parserRule.typeText));

        }
        public void Visit(ExprContext parserRule,IObjectContext<IVar,IVar> context)
        {
            switch (parserRule)
            {
                case SelfDispatchContext rule:
                    Visit(rule,context);
                    break;
                case CondExprContext rule:
                    Visit(rule,context);
                    break;
                case WhileExprContext rule:
                    Visit(rule,context);
                    break;
                case BlockExprContext rule:
                    Visit(rule,context);
                    break;
                case LetExprContext rule:
                    Visit(rule,context);
                    break;
                case CaseExprContext rule:
                    Visit(rule,context);
                    break;
                case NewTypeExprContext rule:
                    Visit(rule,context);
                    break;
                case AssignExprContext rule:
                    Visit(rule,context);
                    break;
                case NotExprContext rule:
                    Visit(rule,context);
                    break;
                case CompaExprContext rule:
                    Visit(rule,context);
                    break;
                //case PlusRestExprContext rule:
                //    Visit(rule,context);
                //    break;
                //case MultDivExprContext rule:
                //    Visit(rule,context);
                //    break;
                case ArithContext rule:
                    Visit(rule, context);
                    break;
                case IsvoidExprContext rule:
                    Visit(rule,context);
                    break;
                case NegExprContext rule:
                    Visit(rule,context);
                    break;
                case DispatchContext rule:
                    Visit(rule,context);
                    break;
                case InParenthesisExprContext rule:
                    Visit(rule,context);
                    break;
                case IdExprContext rule:
                    Visit(rule, context);
                    break;
                case IntegerExprContext rule:
                    Visit(rule, context);
                    break;
                case StringExprContext rule:
                    Visit(rule, context);
                    break;
                case BoolExprContext rule:
                    Visit(rule, context);
                    break;
                default:
                    break;
            }
        }
        //No se si la cond su tipo no es bool devuelvo un tipo no definido
        public void Visit(CondExprContext parserRule,IObjectContext<IVar,IVar> context)
        {
            Visit(parserRule.ifExpr,context);
            Visit(parserRule.thenExpr, context);
            Visit(parserRule.elseExpr, context);
            //Si la expresion ifExpr es undefined devuelvo como tipo estatico undefined
            if (!globalContext.IfDefineType(parserRule.ifExpr.computedType) )
                parserRule.computedType = globalContext.Undefined;
            else if (!parserRule.ifExpr.computedType.Equals(globalContext.Bool))
            {
                errorLogger.LogError($"No se puede convertir el tipo {parserRule.ifExpr.computedType.Name} en bool, linea {parserRule.ifExpr.Start.Line} y columna {parserRule.ifExpr.Start.Column+1}");
                parserRule.computedType = globalContext.Undefined;
            } 
            else//Si al menos uno es undefined entonces el tipo estatico lo sera
                parserRule.computedType = parserRule.thenExpr.computedType.Join(parserRule.elseExpr.computedType);

        }
        //No se si en el while si algun tipo no esta definido cambio su tipo estatico
        public void Visit(WhileExprContext parserRule, IObjectContext<IVar, IVar> context)
        {
            Visit(parserRule.whileExpr, context);
            Visit(parserRule.loopExpr, context);
            //Si la expresion ifExpr es undefined devuelvo como tipo estatico undefined
            if (!globalContext.IfDefineType(parserRule.whileExpr.computedType))
                parserRule.computedType = globalContext.Undefined;
            else if (!parserRule.whileExpr.computedType.Equals(globalContext.Bool))
            {
                errorLogger.LogError($"No se puede convertir el tipo {parserRule.whileExpr.computedType.Name} en bool, linea {parserRule.whileExpr.Start.Line} y columna {parserRule.whileExpr.Start.Column + 1}");
                parserRule.computedType = globalContext.Undefined;
            }
            //verifico si la expresion loopExpr es undefined
            else if (!globalContext.IfDefineType(parserRule.loopExpr.computedType))
                parserRule.computedType = globalContext.Undefined;
            else//Si al menos uno es undefined entonces el tipo estatico lo sera
                parserRule.computedType = globalContext.Object;
        }
        //Si al menos un expresion devuelve un tipo undefinido entonces la expresion total tambien
        public void Visit(BlockExprContext parserRule, IObjectContext<IVar, IVar> context)
        {
            foreach (var expr in parserRule._expresions)
            {
                Visit(expr, context);
                if (!globalContext.IfDefineType(expr.computedType))
                    parserRule.computedType = expr.computedType;
            }
            Visit(parserRule.finalExpresion, context);
            parserRule.computedType = parserRule.computedType??parserRule.finalExpresion.computedType;
        }
        public void Visit(CaseExprContext parserRule, IObjectContext<IVar, IVar> context)
        {
            Visit(parserRule.expresion,context);
            Visit(parserRule.firstBranch, context);
            //Guardo el valor estatico de la primera rama
            IType staticType = parserRule.firstBranch.computedType;
            //hago a todos los valores estaticos de tal manera que sera el valor estatico de la expresion
            foreach (var branch in parserRule._branches)
            {
                Visit(branch,context);
                staticType = staticType.Join(branch.computedType);
            }
            parserRule.computedType = staticType;
        }
        public void Visit(BranchContext parserRule, IObjectContext<IVar, IVar> context)
        {
            var newContext = context.CreateChildContext();
            if (!globalContext.IfDefineType(parserRule.typeText))
                errorLogger.LogError($"El tipo con nombre {parserRule.typeText} no ha sido encontrado en el programa, linea {parserRule.type.Line} y columna {parserRule.type.Column + 1}");
            newContext.Define(parserRule.idText, globalContext.GetType(parserRule.typeText));
            Visit(parserRule.expression, newContext);
            parserRule.computedType = parserRule.expression.computedType;
        }
        public void Visit(LetExprContext parserRule, IObjectContext<IVar, IVar> context)
        {
            Visit(parserRule.let,context);
            parserRule.computedType = parserRule.let.computedType;
        }
        public void Visit(LetRuleContext parserRule, IObjectContext<IVar, IVar> context)
        {
            switch (parserRule)
            {
                case LetDeclContext rule:
                    Visit(rule,context);
                    break;
                case LetBodyContext rule:
                    Visit(rule,context);
                    break;
                default:
                    break;
            }
        }
        public void Visit(LetDeclContext parserRule,IObjectContext<IVar,IVar> context)
        {
            var newContext = context.CreateChildContext();
            Visit(parserRule.declaretion, newContext);
            Visit(parserRule.let,newContext);
            parserRule.computedType = parserRule.let.computedType;
        }
        public void Visit(LetBodyContext parserRule,IObjectContext<IVar, IVar> context)
        {
            var newContext = context.CreateChildContext();
            Visit(parserRule.declaretion, newContext);
            Visit(parserRule.body,newContext);
            parserRule.computedType = parserRule.body.computedType;
        }
        public void Visit(NewTypeExprContext parserRule,IObjectContext<IVar, IVar> context)
        {
            if (!globalContext.IfDefineType(parserRule.type.Text))
            {
                errorLogger.LogError($"El tipo con nombre {parserRule.type.Text} no ha sido encontrado en el programa, linea {parserRule.type.Line} y columna {parserRule.type.Column + 1}");
                parserRule.computedType = globalContext.Undefined;
            }
            else
                parserRule.computedType = globalContext.GetType(parserRule.type.Text,type);
        }
        public void Visit(AssignExprContext parserRule, IObjectContext<IVar, IVar> context)
        {
            if (!context.IsDefineVar(parserRule.id.Text))
            {
                errorLogger.LogError($"La variable {parserRule.id.Text} no existe en el contexto actual, linea {parserRule.id.Line} y columna {parserRule.id.Column+1}");
                parserRule.computedType = globalContext.Undefined;
            }
            else
            {
                var variable= context.GetVar(parserRule.id.Text).Type;
                Visit(parserRule.expresion,context);
                //si el tipo de la variable no esta definido
                if (!globalContext.IfDefineType(variable.Name))
                    parserRule.computedType = globalContext.Undefined;
                //si el tipo de la expresion es Undefined no entra aqui y se le asigna despues
                else if (!parserRule.expresion.computedType.Conform(variable))
                {
                    errorLogger.LogError($"El tipo {parserRule.expresion.computedType.Name} de la expresion  no conforma el tipo de la variable {variable.Name}");
                    parserRule.computedType = globalContext.Undefined;//Si el tipo que devuelve la expresion no conforma con el de la variable se devuelve undefined (puede que lo quite)
                }
                else
                    parserRule.computedType = parserRule.expresion.computedType;

            }
        }
        public void Visit(NotExprContext parserRule, IObjectContext<IVar, IVar> context)
        {
            Visit(parserRule.expresion, context);
            //Verifico si es undefined
            if (!globalContext.IfDefineType(parserRule.expresion.computedType.Name))
                parserRule.computedType = globalContext.Undefined;
            //Si es un tipo diferente a Bool lanzo una expecion
            else if (!parserRule.expresion.computedType.Equals(globalContext.Bool))
            {
                errorLogger.LogError($"El operador 'not' no se puede aplicar a un tipo {parserRule.expresion.computedType.Name}, linea {parserRule.expresion.Start.Line} y columna {parserRule.expresion.Start.Column+1}");
                parserRule.computedType = globalContext.Undefined;
            }
            //Si es bool es tipo estatico de la expresion es bool
            else
                parserRule.computedType = parserRule.expresion.computedType;
        }
        public void Visit(CompaExprContext parserRule, IObjectContext<IVar, IVar> context)
        {
            Visit(parserRule.left, context);
            Visit(parserRule.right, context);
            if (!globalContext.IfDefineType(parserRule.left.computedType.Name) || !globalContext.IfDefineType(parserRule.right.computedType.Name))
                parserRule.computedType = globalContext.Undefined;
            else if (parserRule.op.Text=="=")
            {

                //Si al menos uno es de tipo int, bool o' string y los tipos son diferentes capturamos el error
                if (!parserRule.right.computedType.Equals(parserRule.left.computedType) && (parserRule.left.computedType.Equals(globalContext.Int) || parserRule.left.computedType.Equals(globalContext.Bool) || parserRule.left.computedType.Equals(globalContext.String) || parserRule.right.computedType.Equals(globalContext.Int) || parserRule.right.computedType.Equals(globalContext.Bool) || parserRule.right.computedType.Equals(globalContext.String)))
                {
                    errorLogger.LogError($"No se puede aplicar el operador = entre el tipo {parserRule.left.computedType.Name} y el tipo {parserRule.right.computedType.Name}, linea {parserRule.Start.Line} y columna {parserRule.Start.Column+1}");
                    parserRule.computedType = globalContext.Undefined;
                }
                else
                    parserRule.computedType = globalContext.Bool;
            }
            else
            {
                //Lo dejo como un tipo idefinido
                if (!parserRule.left.computedType.Equals(globalContext.Int) || !parserRule.right.computedType.Equals(globalContext.Int))
                {
                    errorLogger.LogError($"No se puede aplicar el operador {parserRule.op.Text } entre el tipo {parserRule.left.computedType.Name} y el tipo {parserRule.right.computedType.Name} , linea {parserRule.Start.Line} y columna {parserRule.Start.Column + 1}");
                    parserRule.computedType = globalContext.Undefined;
                }
                //else if (!parserRule.right.computedType.Equals(globalContext.Int))
                //{
                //    errorLogger.LogError($"No se puede {parserRule.op.Text } con un tipo {parserRule.right.computedType.Name}");
                //    parserRule.computedType = globalContext.Undefined;
                //}
                else
                    parserRule.computedType = globalContext.Bool;
            }
        }
        public void Visit(ArithContext parserRule, IObjectContext<IVar, IVar> context)
        {
            Visit(parserRule.left, context);
            Visit(parserRule.right, context);
            if (!globalContext.IfDefineType(parserRule.left.computedType.Name) || !globalContext.IfDefineType(parserRule.right.computedType.Name))
                parserRule.computedType = globalContext.Undefined;
            else
            {
                //Lo dejo como un tipo idefinido
                if (!parserRule.left.computedType.Equals(globalContext.Int)||!parserRule.right.computedType.Equals(globalContext.Int))
                {
                    errorLogger.LogError($"No se puede aplicar el operador {parserRule.op.Text } entre el tipo {parserRule.left.computedType.Name} y el tipo {parserRule.right.computedType.Name} , linea {parserRule.Start.Line} y columna {parserRule.Start.Column + 1}");
                    parserRule.computedType = globalContext.Undefined;
                }
                //else if (!parserRule.right.computedType.Equals(globalContext.Int))
                //{
                //    errorLogger.LogError($"No se puede {parserRule.op.Text } con un tipo {parserRule.right.computedType.Name}");
                //    parserRule.computedType = globalContext.Undefined;
                //}
                else
                    parserRule.computedType = globalContext.Int;
            }
        }
        public void Visit(IsvoidExprContext parserRule, IObjectContext<IVar, IVar> context)
        {
            Visit(parserRule.expresion,context);
            if (!globalContext.IfDefineType(parserRule.expresion.computedType.Name))
                parserRule.computedType = globalContext.Undefined;
            else
                parserRule.computedType = globalContext.Bool;
        }
        public void Visit(NegExprContext parserRule, IObjectContext<IVar, IVar> context)
        {
            Visit(parserRule.expresion, context);
            if (!globalContext.IfDefineType(parserRule.expresion.computedType.Name))
                parserRule.computedType = globalContext.Undefined;
            else
            {
                //Lo dejo como un tipo idefinido
                //$"El operador 'not' no se puede aplicar a un tipo {parserRule.expresion.computedType.Name}, linea {parserRule.expresion.Start.Line} y columna {parserRule.expresion.Start.Column+1}"
                if (!parserRule.expresion.computedType.Equals(globalContext.Int))
                {
                    errorLogger.LogError($"El operador '~' no se puede aplicar a un tipo {parserRule.expresion.computedType.Name}, linea {parserRule.expresion.Start.Line} y columna {parserRule.expresion.Start.Column + 1}");
                    parserRule.computedType = globalContext.Undefined;
                }
                else
                    parserRule.computedType = globalContext.Int;
            }
        }
        //Posiblemente ambos tipos de dispatch los pueda poner en una misma definicion
        public void Visit(DispatchContext parserRule, IObjectContext<IVar, IVar> context)
        {
            //Visito la expresion
            Visit(parserRule.expresion,context);
            foreach (var expr in parserRule._expresions)
                Visit(expr,context);
            IType typeFunc;
            var typeExp0 = parserRule.expresion.computedType.RealType(type);
            if (parserRule.type != null)
            {
                //Si el tipo estatico no esta definido entonces no se analiza ni su metodo ni si el tipo estatico de la expresion hereda des este
                if (!globalContext.IfDefineType(parserRule.type.Text))
                {
                    errorLogger.LogError($"El tipo con nombre {parserRule.type.Text} no ha sido encontrado en el programa, linea {parserRule.type.Line} y columna {parserRule.type.Column + 1}");
                    parserRule.computedType = globalContext.Undefined;
                    return;
                }
                typeFunc = globalContext.GetType(parserRule.type.Text);
                //Analizamos si el tipo estatico hereda de la expresion hereda del tipo estatico y en el caso contrario  sigo analizando si el tipo estatico
                //contiene la funcion
                
                //verico con lo del self_type?
                if (!typeExp0.Conform( typeFunc))
                    errorLogger.LogError($"El tipo {parserRule.expresion.computedType.Name} no conforma al tipo {parserRule.type.Text}, linea {parserRule.expresion.Start.Line} y columna {parserRule.expresion.Start.Column + 1}");
            }
            //Si no el tipo al cual se le va analizar el metodo es el de la expresion
            else
                typeFunc = typeExp0;
            //analizando si el tipo estatico contiene esa funcion
            if (!typeFunc.IsDefineMethod(parserRule.id.Text))
            {
                if(globalContext.IfDefineType(typeFunc.Name))
                    errorLogger.LogError($"No esta definido el metodo {parserRule.id.Text} para el tipo {typeFunc.Name}, linea {parserRule.id.Line} y columna {parserRule.id.Column + 1}");
                parserRule.computedType = globalContext.Undefined;
            }
            else
            {
                //Si esta definido el metodo comparamos chequemos los tipos que recibe el metodo
                var method = typeFunc.GetMethod(parserRule.id.Text);
                //Los tipos computados los almaceno en un array
                var types_expressions = (from t in parserRule._expresions select t.computedType).ToArray();
                //Si la cantidad de expresiones que le paso al metodo es diferentes a los que realmente el metodo recibe
                if (parserRule._expresions.Count != method.Formals.Length)
                    errorLogger.LogError($"El metodo {parserRule.id.Text} no toma {parserRule._expresions.Count} argumentos, linea {parserRule.id.Line} y columna {parserRule.id.Column + 1}");
                else
                {
                    for (int i = 0; i < types_expressions.Length; i++)
                    {
                        if (!types_expressions[i].Conform(method.Formals[i].Type))
                            errorLogger.LogError($"No se puede convertir un tipo {types_expressions[i].Name} en {method.Formals[i].Type.Name}, linea {parserRule._expresions[i].Start.Line} y columna {parserRule._expresions[i].Start.Column+1}");
                    }
                }
                parserRule.computedType = method.Type.DispatchComputedType(parserRule.expresion.computedType);
            }
        }
        public void Visit(SelfDispatchContext parserRule, IObjectContext<IVar, IVar> context)
        {
            foreach (var expr in parserRule._expresions)
                Visit(expr,context);
            if (!type.IsDefineMethod(parserRule.id.Text))
            {
                errorLogger.LogError($"No esta definido el metodo '{parserRule.id.Text}' para el tipo '{type.Name}', linea {parserRule.id.Line} y columna {parserRule.id.Column + 1}");
                parserRule.computedType = globalContext.Undefined;
            }
            else
            {
                //Si esta definido el metodo comparamos chequemos los tipos que recibe el metodo
                var method = type.GetMethod(parserRule.id.Text);
                //Los tipos computados los almaceno en un IEnumerable
                var types_expresions = (from t in parserRule._expresions select t.computedType).ToArray();
                if (parserRule._expresions.Count != method.Formals.Length)
                    errorLogger.LogError($"El metodo '{parserRule.id.Text}' no toma {parserRule._expresions.Count} argumentos, linea {parserRule.id.Line} y columna {parserRule.id.Column + 1}");
                else
                {
                    for (int i = 0; i < types_expresions.Length; i++)
                    {
                        if (!types_expresions[i].Conform(method.Formals[i].Type))
                            errorLogger.LogError($"No se puede convertir un tipo {types_expresions[i].Name} en {method.Formals[i].Type.Name}, linea {parserRule._expresions[i].Start.Line} y columna {parserRule._expresions[i].Start.Column + 1}");
                    }
                }
                //es como el dispatch anterior pero la primera expresion es self
                parserRule.computedType = method.Type.DispatchComputedType(new SelfType(type,globalContext));
            }

        }
        public void Visit(InParenthesisExprContext parserRule, IObjectContext<IVar, IVar> context)
        {
            Visit(parserRule.expresion,context);
            parserRule.computedType = parserRule.expresion.computedType;
        }
        public void Visit(IdExprContext parserRule, IObjectContext<IVar, IVar> context)
        {
            if (!context.IsDefineVar(parserRule.id.Text))
            {
                errorLogger.LogError($"La variable {parserRule.id.Text} no existe en el contexto actual, linea {parserRule.id.Line} y columna {parserRule.id.Column + 1}");
                parserRule.computedType = globalContext.Undefined;
            }
            else
            {
                parserRule.computedType = context.GetVar(parserRule.id.Text).Type;
            }
            
        }
        public void Visit(IntegerExprContext parserRule, IObjectContext<IVar, IVar> context)
        {
            parserRule.computedType = globalContext.Int;
        }
        public void Visit(StringExprContext parserRule, IObjectContext<IVar, IVar> context)
        {
            parserRule.computedType = globalContext.String;
        }
        public void Visit(BoolExprContext parserRule, IObjectContext<IVar, IVar> context)
        {
            parserRule.computedType = globalContext.Bool;
        }
    }
}
