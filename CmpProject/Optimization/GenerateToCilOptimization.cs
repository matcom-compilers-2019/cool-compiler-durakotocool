using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmpProject.CIL;
using static COOLgrammarParser;
namespace CmpProject.Optimization
{
    //class GenerateToCilOptimization : GenerateToCil
    //{
    //    public GenerateToCilOptimization(CheckSemanticVisitor visitor, GenerateToCilFeatures generateToCilFeatures) : base(visitor, generateToCilFeatures)
    //    {
    //    }
    //    public override void Visit(ProgramContext parserRule)
    //    {
    //        base.Visit(parserRule);
    //    }
    //    public override IFunctionCil Visit(MethodContext parserRule)
    //    {
    //        IFunctionCil functionCil= base.Visit(parserRule);
    //        var data_flow = new Data_Flow_Graph(functionCil.ThreeDirInses.ToList());
    //        data_flow.Calculate_Reaching_Definitions();
    //        return functionCil;
    //    }
    //    public override IHolderCil Visit(DispatchContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
    //    {
    //        var value = base.Visit(parserRule, cilTree, contextCil);
    //        IType typeCool;
    //        IMethod method;
    //        if (parserRule.type != null)
    //            typeCool = GlobalContext.GetType(parserRule.type.Text);
    //        else
    //            typeCool = parserRule?.expresion.computedType;
    //        method = typeCool?.GetMethod(parserRule.id.Text);
    //        if (method != null && !method.IsRedefined && !typeCool.IsBasicType)
    //        {
    //            var typeCil = CilAst.GetTypeCilByName(typeCool.Name);
    //            var methodCil = typeCil.GetFunctionCilsByCoolName(method.ID).Function;
    //            OptimizateDispatch(typeCil, methodCil, value, cilTree);
    //        }
    //        else if (parserRule.type == null && !typeCool.IsBasicType)
    //        {
    //            var listThreeDirInses = cilTree.ThreeDirInses.Reverse().Skip(1).ToList();
    //            //Seria como el self que le pasas a la funcion
    //            IHolderCil valueSelf;
    //            for (int i = 0; i < listThreeDirInses.Count; i++)
    //            {
    //                //recorro la lista de atras pa alante hasta cojer el primer argumento que le paso al vcall este es el que me da el valor de su tipo dinamico
    //                if (!(listThreeDirInses[i] is ArgExprCil))
    //                {
    //                    valueSelf = (listThreeDirInses[i - 1] is ArgExprCil holder) ? holder.X : null;
    //                    if (valueSelf != null && valueSelf.typeCil != null)
    //                        OptimizateDispatch(valueSelf.typeCil, valueSelf.typeCil.GetFunctionCilsByCoolName(parserRule.id.Text).Function, value, cilTree);
    //                }
    //            }
    //        }
    //        return value;
    //    }
    //    //Este metodo lo que hace es remover el type of para resolver el tipo dinamico y hacer un call en vez de un vcall
    //    public override IHolderCil Visit(SelfDispatchContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
    //    {
            
    //        var value = base.Visit(parserRule, cilTree, contextCil);
    //        IMethod method;
    //        method = typeC?.GetMethod(parserRule.id.Text);
    //        if (method != null && !method.IsRedefined && !typeC.IsBasicType)
    //        {
    //            var typeCil = CilAst.GetTypeCilByName(typeC.Name);
    //            var methodCil = typeCil.GetFunctionCilsByCoolName(method.ID).Function;
    //            OptimizateDispatch(typeCil, methodCil, value, cilTree);
    //        }
    //        return value;
    //    }
    //    public void OptimizateDispatch(ITypeCil typeCil, IFunctionCil methodCil, IHolderCil value, IFunctionCil cilTree)
    //    {
    //        var listThreeDirInses = cilTree.ThreeDirInses.ToList();
    //        var LastOfType = cilTree.ThreeDirInses.OfType<TypeOf>().Last();
    //        listThreeDirInses.Remove(LastOfType);

    //        listThreeDirInses.Remove(cilTree.ThreeDirInses.Last());
    //        //var typeCil = CilAst.GetTypeCilByName(typeCool.Name);
    //        //var methodCil = typeCil.GetFunctionCilsByCoolName(method.ID).Function;
    //        listThreeDirInses.Add(new CallCil(value as IVarCil, methodCil));
    //        cilTree.ThreeDirInses = new HashSet<IThreeDirIns>(listThreeDirInses);
    //    }
    //}
}
