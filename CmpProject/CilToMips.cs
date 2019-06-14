using CmpProject.CIL;
using System;
using System.Collections.Generic;
using System.Linq;
using static COOLgrammarParser;
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
            var emptyString = "\"\"";
            var functions = Cil.CilAst.FunctionCils;
            var types = Cil.CilAst.TypeCils;
            var data = Cil.CilAst.dataStringCils;
            //var functionsMIPS = functions.Select(x => Visitor(x));
            var mipsResult = new MIPS() { Data = new List<string>() { "heap: .space 2000000" } };
            mipsResult.Data.Add("heapPointer: .word 0");
            mipsResult.Data.Add("newLine: .asciiz \"\\n\"");
            mipsResult.Data.AddRange(data.Select(x => $"{x.varCil}: .asciiz {(x.stringCil.Name.Length!=0 ? x.stringCil.Name : emptyString)}"));
            foreach (var t in types)
            {
                mipsResult.Data.Add($"type_{t.Name}:");
                mipsResult.Data.Add($"\ttype_{t.Name}_Length: .byte {t.Attributes.Count *4}");
                foreach (var f in t.Functions)
                {
                    mipsResult.Data.Add($"\ttype_{t.Name}_{f.CilName}: .word {f.Function.Name}");
                }
				mipsResult.Data.Add($"\ttype_{t.Name}_count_methods: .word {t.Functions.Count}");
				mipsResult.Data.Add($"\ttype_{t.Name}_name: .asciiz \"{t.Name}\"");
            }
			var entry = functions.Single(x => x.Name == "entry");
			functions.Remove(entry);
			mipsResult.Add(Visitor(entry, program));
			for (int i = 0; i < mipsResult.Functions.Count; i++)
			{
				if (mipsResult.Functions[i].StartsWith("jr"))
				{
					mipsResult.Functions.RemoveAt(i);
					mipsResult.Functions.InsertRange(i, new List<string>() {"li $v0, 10","syscall"});
				}
			}
			mipsResult.Functions.Select(x => x.StartsWith("jr") ? "" : x);
			var functionsMIPS = (from i in functions
                          select Visitor(i, program)).ToList();
            for (int i = 0; i < functionsMIPS.Count; i++)
            {
                var current = functionsMIPS[i];
                mipsResult.Add(current);
            }
			mipsResult.Functions.AddRange(CreateLengthFunction());
			mipsResult.Functions.AddRange(CreateConcatFunction());
			mipsResult.Functions.AddRange(CreateSubstringFunction());
			return mipsResult;
            //var functionsMIPS = Visitor(functions.ToList()[17]);
        }


		private List<string> CreateLengthFunction()
		{
			var lines = new List<string>(){
				"\n\r",
				"lengthFunctionStart:",
				"li $v0, 0",
				"lengthLoad:",
				"lb $t1, ($t0)",
				"beqz $t1, lengthReturn",
				"add $t0, $t0, 1",
				"add $v0, $v0, 1",
				"j lengthLoad",
				"lengthReturn:",
				$"jr $ra"
			};
			return lines;
		}
		private List<string> CreateConcatFunction()
		{
			var lines = new List<string>(){
				"\n\r",
				"concatFunctionStart:",
				"la $t3, heap",
				"lw $t4, heapPointer",
				"lw $t5, heapPointer",
				"add $t4, $t4, $t3",
				"move $v0, $t4",
				"concatFunctionFirst:",
 				"lb $a0, ($t0)",
 				"beqz $a0, concatFunctionSecond",
				"sb $a0, ($t4)",
				"addi $t4, $t4, 1",
				"addi $t5, $t5, 1",
				"addi $t0, $t0, 1",
				"j concatFunctionFirst",
				"concatFunctionSecond:",
				"lb $a0, ($t1)",
 				"beqz $a0, concatFunctionEnd",
				"sb $a0, ($t4)",
				"addi $t4, $t4, 1",
				"addi $t5, $t5, 1",
				"addi $t1, $t1, 1",
				"j concatFunctionSecond",
				"concatFunctionEnd:",
				"sb $zero, ($t4)",
				"addi $t5, $t5, 1",
                "rem $t6, $t5, 4",
                "neg $t6, $t6",
                "add $t6, $t6, 4",
                "add $t5, $t5, $t6",
				"sb $t5, heapPointer",
				"jr $ra"
			};
			return lines;
		}
        private List<string> CreateSubstringFunction()
        {
            var list = new List<string>(){
                "substrFunctionStart:",
                "la $t3, heap",
                "lw $t4, heapPointer",
                "lw $t5, heapPointer",
                "add $t4, $t4, $t3",
                "move $v0, $t4",
                "add $t0, $t0, $t1",
                "move $v0, $t4",
                "substrFunctionLoop:",
                "lb $a0, 0($t0)",
                "beqz $t2, substrFunctionFin  #llego al final del substr",
                "addi $t2, $t2, -1",
                "beqz $a0, substrFunctionFin 	# encontro el fin del str",
                "sb $a0,  0($t4)",
                "addi $t4, $t4, 1",
                "addi $t5, $t5, 1",
                "addi $t0, $t0, 1",
                "j substrFunctionLoop",
                "substrFunctionFin:",
                "sb $zero, ($t4)",
                "addi $t5, $t5, 1",
                "rem $t6, $t5, 4",
                "neg $t6, $t6",
                "add $t6, $t6, 4",
                "add $t5, $t5, $t6",
                "sb $t5, heapPointer",
                //"move $a0, $v0",
                //"li $v0, 4",
                //"syscall",
                "# lw $ra, ($sp)",
                "jr $ra"
            };
            return list;
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
                    mipsResult.Functions.Add("##-----\t"+function.ThreeDirInses.ToList()[i].ToString().TrimEnd()+ "\t-----");
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
