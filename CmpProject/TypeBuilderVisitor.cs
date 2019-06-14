using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static COOLgrammarParser;
using static CmpProject.TypeCool;
namespace CmpProject
{
   public class TypeBuilderVisitor : IVisitor<ProgramContext>
    {
        public IGlobalContext globalContext;
        public BasicTypes basicTypes;
        public IErrorLogger errorLogger;
        public TypeBuilderVisitor()
        {
            globalContext = new GlobalContext();
            errorLogger = new ErrorLogger();
        }
        public void Visit(ProgramContext parserRule)
        {
            basicTypes = new BasicTypes(parserRule,globalContext);
            parserRule._classes.Insert(0,basicTypes.Object);
            parserRule._classes.Insert(0, basicTypes.IO);
            parserRule._classes.Insert(0, basicTypes.Void);
            foreach (var _class in parserRule._classes)
            {
                if (globalContext.IfDefineType(_class.type.Text))
                    errorLogger.LogError($"El programa ya contiene una definicion para { _class.type.Text}, linea {_class.type.Line} y la columna {_class.type.Column}");
                else
                {
                    globalContext.CreateChildContext(_class.type.Text);
                    if (_class.inherits != null)
                        _class.father = parserRule._classes.FirstOrDefault(p => p.type.Text == _class.inherits.Text);
                    else if(_class.type.Text!="Object"&& _class.type.Text != "void")
                        _class.father = basicTypes.Object;
                    
                }
                
            }
            if (!globalContext.IfDefineType("Main"))
                errorLogger.LogError("El programa no contiene la definicion para Main");
        }
    }
}
