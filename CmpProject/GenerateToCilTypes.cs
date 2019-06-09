using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmpProject.CIL;
using static COOLgrammarParser;

namespace CmpProject
{
    internal class GenerateToCilTypes:IVisitor<ProgramContext>
    //,IVisitorCil<ClassContext, ITypeCil>,
    //IVisitorCilWhitContext<FeatureContext, ITypeCil>,
    //IVisitorCil<AttributeContext, ITypeCil>,
    //IVisitorCil<MethodContext, ITypeCil>
    {
        public ICilAst CilAst { get; set; }

        public IGlobalContext GlobalContext;
        ClassContext type { get; set; }
        public IFunctionCil functionCil { get; set; }
        public BasicTypes basicTypes { get; set; }
        public GenerateToCilTypes(CheckSemanticVisitor visitor)
        {
            basicTypes = visitor.basicTypes;
            GlobalContext = visitor.globalContext;
            CilAst = new CilAst();
        }
        public void Visit(ProgramContext parserRule)
        {
            Visit(basicTypes.entry, null);
            foreach (var _class in parserRule._classes)
            {
                var typeCil = new TypeCil(_class.type.Text,CilAst);
                CilAst.TypeCils.Add(typeCil);
                //Visit(_class, typeCil);
            }
        }
        public void Visit(MethodContext parserRule, ITypeCil cilTree)
        {
            CilAst.CreateFunctionCil(null, parserRule.idText);
        }
    }
}
