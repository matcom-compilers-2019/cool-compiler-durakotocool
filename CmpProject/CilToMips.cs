using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static COOLgrammarParser;
using CmpProject.CIL;
namespace CmpProject
{
    class CilToMips
    {
        public GenerateToCil Cil {get; set; }
        public CilToMips(GenerateToCil cil)
        {
            Cil = cil;
        }
        public MIPS Visit(ProgramContext program)
        {
            var functions = Cil.CilAst.FunctionCils;
            var types = Cil.CilAst.TypeCils;
            var data = Cil.CilAst.dataStringCils;
            //var functionsMIPS = functions.Select(x => Visitor(x));
            var mipsResult = new MIPS() { Data = new List<string>() { "heap: .space 2000000" } };
            mipsResult.Data.Add("heapPointer: .word 0");
            mipsResult.Data.AddRange(data.Select(x => $"{x.varCil}: asciiz {x.stringCil}"));
            foreach (var t in types)
            {
                mipsResult.Data.Add($"type_{t.Name}:");
                mipsResult.Data.Add($"\ttype_{t.Name}_Length: .byte {t.Attributes.Count *4}");
                foreach (var f in t.Functions)
                {
                    mipsResult.Data.Add($"\ttype_{t.Name}_{f.CilName}: .word {f.Function.Name}");
                }
                mipsResult.Data.Add($"\ttype_{t.Name}_name: .asciiz \"{t.Name}\"");
            }
            var functionsMIPS = (from i in functions
                          select Visitor(i, program)).ToList();
            for (int i = 0; i < functionsMIPS.Count; i++)
            {
                var current = functionsMIPS[i];
                mipsResult.Data.AddRange(current.Data);
                mipsResult.Functions.AddRange(current.Functions);
                mipsResult.Text.AddRange(current.Text);
            }
            return mipsResult;
            //var functionsMIPS = Visitor(functions.ToList()[17]);
        }
        public MIPS Visitor(IFunctionCil function, ProgramContext program)
        {
            var mipsResult = new MIPS() { Functions = Utils.AcomodarLocales(function) };
            var functionsVisited = function.ThreeDirInses.Select(x => {
                return x.ToMIPS(function, Cil);
            }).ToList();
            for (int i = 0; i < functionsVisited.Count; i++)
            {
                var current = functionsVisited[i];
                if (current == null)
                {
                    mipsResult.Functions.Add("-----\t"+function.ThreeDirInses.ToList()[i].ToString().TrimEnd()+ "\t-----");
                }
                else
                {
                    mipsResult.Data.AddRange(current.Data);
                    if (current.Functions.Count > 0 )
                        current.Functions[0] = current.Functions[0] + "\t\t##" + function.ThreeDirInses.ToList()[i].ToString().Trim();
                    mipsResult.Functions.AddRange(current.Functions);
                    mipsResult.Text.AddRange(current.Text);
                }
            }
            return mipsResult;
        }

        //public MIPS Visitor(IThreeDirIns threeDirIns)
        //{
        //    return null;
        //}
        
        #region Operations
        public MIPS Visitor (SumCil cil) { return null; }
        public MIPS Visitor (RestCil cil) { return null; }
        public MIPS Visitor (MultCil cil) { return null; }
        public MIPS Visitor (DivCil cil) { return null; }
        public MIPS Visitor (EqualCil cil) { return null; }
        public MIPS Visitor (NotEqualCil cil) { return null; }
        public MIPS Visitor (MinorCil cil) { return null; }
        public MIPS Visitor (Minor_EqualCil cil) { return null; }
        public MIPS Visitor (NegCil cil) { return null; }
        #endregion
        #region AssigCil
        public MIPS Visitor(AssigCil cil) { return null; }
        #endregion
        #region Attribute
        public MIPS Visitor(GetAttrCil cil) { return null; }
        public MIPS Visitor(SetAttrCil cil) { return null; }
        #endregion
        #region ArrayAcces
        //Por si acaso el indixe i puede ser un valor pelao
        public MIPS Visitor(IndexCil cil) { return null; }
        public MIPS Visitor(GetIndex cil) { return null; }
        public MIPS Visitor(SetIndex cil) { return null; }
        #endregion
        #region MemoryManage
        //I lo trato como un Ivar ya que en COOl los enteros son un objeto
        public MIPS Visitor(TypeManage cil) { return null; }
        public MIPS Visitor(Allocate cil) { return null; }
        public MIPS Visitor(TypeOf cil) { return null; }
        public MIPS Visitor(DecArrayCil cil) { return null; }
        #endregion
        #region Call_function
        public MIPS Visitor(CallCil cil) { return null; }
        public MIPS Visitor(VCallCil cil) { return null; }
        public MIPS Visitor(ArgExprCil cil) { return null; }
        #endregion
        #region Jump
        public MIPS Visitor(LabelCil cil) { return null; }
        public MIPS Visitor(Label cil) { return null; }
        public MIPS Visitor(Goto cil) { return null; }
        public MIPS Visitor(IfGoto cil) { return null; }
        public MIPS Visitor(GotoCil cil) { return null; }
        #endregion
        #region Return
        public MIPS Visitor(ReturnCil cil){ return null; }
        #endregion
    }

}
