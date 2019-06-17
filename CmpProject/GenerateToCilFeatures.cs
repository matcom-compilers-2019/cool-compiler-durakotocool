using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static COOLgrammarParser;
using CmpProject.CIL;
namespace CmpProject
{
    public class GenerateToCilFeatures:IVisitorCil<ClassContext, ITypeCil>,
    IVisitorCilWhitContext<FeatureContext, ITypeCil>,
    IVisitorCil<AttributeContext, ITypeCil>,
    IVisitorCil<MethodContext, ITypeCil>
    {
        public ICilAst CilAst { get; set; }

        public IGlobalContext GlobalContext;
        ClassContext type { get; set; }
        public IFunctionCil functionCil { get; set; }
        public BasicTypes basicTypes { get; set; }
        internal GenerateToCilFeatures(GenerateToCilTypes generateToCilTypes)
        {
            basicTypes = generateToCilTypes.basicTypes;
            GlobalContext = generateToCilTypes.GlobalContext;
            CilAst = generateToCilTypes.CilAst;
        }
        public void Visit(ProgramContext parserRule)
        {
            //Visit(basicTypes.entry, null);
            foreach (var _class in parserRule._classes)
            {
                var typeCil = CilAst.GetTypeCilByName(_class.type.Text);
                Visit(_class, typeCil);
            }
        }
        public void Visit(ClassContext parserRule, ITypeCil cilTree)
        {

            var types = new Stack<ClassContext>();
            var typeTemp = parserRule;
            types.Push(typeTemp);
            while (typeTemp.father != null)
            {
                typeTemp = typeTemp.father;
                types.Push(typeTemp);
            }
            while (types.Count > 0)
            {
                typeTemp = types.Pop();
                type = typeTemp;
                foreach (var item in typeTemp._features)
                    Visit(item, cilTree, new ContextCil());
            }
        }

        public void Visit(FeatureContext parserRule, ITypeCil cilTree, IContextCil contextCil)
        {
            switch (parserRule)
            {
                case AttributeContext attribute:
                    Visit(attribute, cilTree);
                    break;
                case MethodContext method:
                    Visit(method, cilTree);
                    break;
                default:
                    break;
            }
        }
        public void Visit(AttributeContext parserRule, ITypeCil cilTree)
        {
            cilTree.AddAttr(new AttributeCil(type.type.Text, parserRule.idText));
        }
        public void Visit(MethodContext parserRule, ITypeCil cilTree)
        {
            //Creo la funcion e cil y la se anade al CilAst
            var function = CilAst.CreateFunctionCil(type.type.Text, parserRule.idText);
            //Le hago referencia al tipo correspondiente
            //La al anadirla ya hay una funcion definida con el mismo nombre se le cambia el nombre de la funcionCil por el nuevo
            cilTree.AddFunc(new FunctionTypeCil(type.type.Text, parserRule.idText,function));   
        }
    }
}
