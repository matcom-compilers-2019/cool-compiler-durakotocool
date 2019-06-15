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
                typeCil.inherit = CilAst.GetTypeCilByName(_class.father?.type.Text);
                CilAst.TypeCils.Add(typeCil);
            }
        }
        public void Visit(MethodContext parserRule, ITypeCil cilTree)
        {
            CilAst.CreateFunctionCil(null, parserRule.idText);
        }
    }
}
