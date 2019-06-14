using CmpProject.CIL;
using System;
using System.Collections.Generic;
using System.Linq;
using static COOLgrammarParser;
namespace CmpProject
{
    public static class Utils
    {
        public static List<string> LoadFromRegister(IVarCil x, IFunctionCil function, string src)
        {
            List<string> result = new List<string>();
            if (function.LocalCils.Contains(x)) //si y es una variable local
            {
                var index = function.localsDict[x.Name];
                result.Add($"sw ${src}, {index}($sp)");
            }
            else if (function.ArgCils.Contains(x))
            {   //si y es un parametro
                //var index = function.ArgCils.ToList().FindIndex(i => i.Name.Equals(x.Name));
                //result.Add($"move $a{index}, ${src}");
                var index = function.argsDict[x.Name];
                result.Add($"sw ${src}, {index}($sp)");
            }
            return result;
        }
        public static List<string> SaveToRegister(IHolderCil x, IFunctionCil function, string dest)
        {
            List<string> result = new List<string>();
            if (function.LocalCils.Contains(x)) //si y es una variable local
            {
                var index = function.localsDict[x.Name];
                result.Add($"lw ${dest}, {index}($sp)");
            }
            else if (function.ArgCils.Contains(x))
            {   //si y es un parametro
                //var index = function.ArgCils.ToList().FindIndex(i => i.Name.Equals(x.Name));
                //result.Add($"move ${dest}, $a{index}");
                var index = function.argsDict[x.Name];
                result.Add($"lw ${dest}, {index}($sp)");
            }
            else if (x == null)
            {
                result.Add($"li ${dest}, 0");
            }
            else //si y es un valor
            {
                result.Add($"li ${dest}, " + x.Name);
            }
            return result;
        }
        public static List<string> AcomodarVariables(IHolderCil y, IHolderCil z, IFunctionCil function)
        {
            List<string> result = new List<string>();
            result.AddRange(SaveToRegister(y, function, "t1"));
            result.AddRange(SaveToRegister(z, function, "t2"));
            return result;
        }
        public static List<string> AcomodarVariables(IHolderCil y, IFunctionCil function)
        {
            List<string> result = new List<string>();
            result.AddRange(SaveToRegister(y, function, "t1"));
            return result;
        }
        public static List<string> AcomodarLocales(IFunctionCil function)
        {
            var list = function.LocalCils.ToList();
            var args = function.ArgCils.ToList();
            List<string> result = new List<string>() {
                "\n\r",
                $"{function.Name}:",
                $"sub $sp, $sp, {(function.ArgCils.Count + list.Count + 1) * 4}",
                $"sw $ra, {(function.ArgCils.Count+list.Count)*4}($sp)"
            };   //reservar el espacio para guardar las variables locales en al pila
            for (int i = 0; i < args.Count; i++)
            {
                //result.Add("li $t3, 0");
                var index = (args.Count - i - 1 + list.Count) * 4;
                result.Add($"##\t\t{index}($sp)\t{args[i].Name}");
                function.argsDict.Add(args[i].Name, index);
            }
            result.Add("li $t0, 0");
            for (int i = 0; i < list.Count; i++)
            {
                var index = (list.Count - i - 1) * 4;
                result.Add($"sw $t0, {index}($sp)\t\t##{list[i].Name}");
                function.localsDict.Add(list[i].Name, index);
            }
            return result;
        }
    }
    class CilToMips
    {
        public GenerateToCil Cil {get; set; }

        private int CountArgs;

        public CilToMips(GenerateToCil cil)
        {
            Cil = cil;
            CountArgs = 0;
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
                return Selector(x,function, Cil);
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

        private MIPS Selector(IThreeDirIns x, IFunctionCil function, GenerateToCil cil)
        {
            #region Operations
            if (x is SumCil)
                return Visitor(x as SumCil, function, cil);
            else if (x is RestCil)
                return Visitor(x as RestCil, function, cil);
            else if (x is MultCil)
                return Visitor(x as MultCil, function, cil);
            else if (x is DivCil)
                return Visitor(x as DivCil, function, cil);
            else if (x is EqualCil)
                return Visitor(x as EqualCil, function, cil);
            else if (x is NotEqualCil)
                return Visitor(x as NotEqualCil, function, cil);
            else if (x is MinorCil)
                return Visitor(x as MinorCil, function, cil);
            else if (x is Minor_EqualCil)
                return Visitor(x as Minor_EqualCil, function, cil);
            else if (x is NegCil)
                return Visitor(x as NegCil, function, cil);
            #endregion
            #region AssigCil
            else if (x is AssigCil)
                return Visitor(x as AssigCil, function, cil);
            #endregion
            #region Attribute
            else if (x is GetAttrCil)
                return Visitor(x as GetAttrCil, function, cil);
            else if (x is SetAttrCil)
                return Visitor(x as SetAttrCil, function, cil);
            #endregion
            #region MemoryManage
            else if (x is Allocate)
                return Visitor(x as Allocate, function, cil);
            else if (x is TypeOf)
                return Visitor(x as TypeOf, function, cil);
            #endregion
            #region Call_function
            else if (x is CallCil)
                return Visitor(x as CallCil, function, cil);
            else if (x is VCallCil)
                return Visitor(x as VCallCil, function, cil);
            else if (x is ArgExprCil)
                return Visitor(x as ArgExprCil, function, cil);
            #endregion
            #region Jump
            else if (x is Label)
                return Visitor(x as Label, function, cil);
            else if (x is IfGoto)
                return Visitor(x as IfGoto, function, cil);
            else if (x is GotoCil)
                return Visitor(x as GotoCil, function, cil);
            #endregion
            #region Return
            else if (x is ReturnCil)
                return Visitor(x as ReturnCil, function, cil);
            #endregion
            #region String_funcions
            else if (x is LoadCil)
                return Visitor(x as LoadCil, function, cil);
            else if (x is LenghtCil)
                return Visitor(x as LenghtCil, function, cil);
            else if (x is ConcatCil)
                return Visitor(x as ConcatCil, function, cil);
            else if (x is SubStringCil)
                return Visitor(x as SubStringCil, function, cil);
            else if (x is StrCil)
                return Visitor(x as StrCil, function, cil);
            #endregion
            #region IO
            else if (x is In_strCil)
                return Visitor(x as In_strCil, function, cil);
            else if (x is In_intCil)
                return Visitor(x as In_intCil, function, cil);
            else if (x is Out_strCil)
                return Visitor(x as Out_strCil, function, cil);
            else if (x is Out_intCil)
                return Visitor(x as Out_intCil, function, cil);
            #endregion
            #region IsConform
            else if (x is IsNotConformCil)
                return Visitor(x as IsNotConformCil, function, cil);
            #endregion
            #region Object
            else if (x is Halt)
                return Visitor(x as Halt, function, cil);
            else if (x is Copy)
                return Visitor(x as Copy, function, cil);
            else if (x is Type_Name)
                return Visitor(x as Type_Name, function, cil);
            #endregion
            else
                return null;
        }
        #region Operations
        public MIPS Visitor(SumCil instance, IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(Utils.AcomodarVariables(instance.Y, instance.Z, function)){
                "add $t0, $t1, $t2" //Guarda en t0 la suma de t1 y t2
            };
            lines.AddRange(Utils.LoadFromRegister(instance.X, function, "t0"));
            return new MIPS() { Functions = lines };
        }
        public MIPS Visitor(RestCil instance, IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(Utils.AcomodarVariables(instance.Y, instance.Z, function)){
                    "sub $t0, $t1, $t2" //Guarda en t0 la suma de t1 y t2
                };
            lines.AddRange(Utils.LoadFromRegister(instance.X, function, "t0"));
            return new MIPS() { Functions = lines };
        }
        public MIPS Visitor(MultCil instance, IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(Utils.AcomodarVariables(instance.Y, instance.Z, function)){
                    "mul $t0, $t1, $t2" //Guarda en t0 la suma de t1 y t2
                };
            lines.AddRange(Utils.LoadFromRegister(instance.X, function, "t0"));
            return new MIPS() { Functions = lines };
        }
        public MIPS Visitor(DivCil instance, IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(Utils.AcomodarVariables(instance.Y, instance.Z, function)){
                    "div $t0, $t1, $t2" //Guarda en t0 la suma de t1 y t2
                };
            lines.AddRange(Utils.LoadFromRegister(instance.X, function, "t0"));
            return new MIPS() { Functions = lines };
        }
        public MIPS Visitor(EqualCil instance, IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(Utils.AcomodarVariables(instance.Y, instance.Z, function)){
                    "seq $t0, $t1, $t2" //Guarda en t0 la suma de t1 y t2
                };
            lines.AddRange(Utils.LoadFromRegister(instance.X, function, "t0"));
            return new MIPS() { Functions = lines };
        }
        public MIPS Visitor(NotEqualCil instance, IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(Utils.AcomodarVariables(instance.Y, instance.Z, function)){
                    "sne $t0, $t1, $t2" //Guarda en t0 la suma de t1 y t2
                };
            lines.AddRange(Utils.LoadFromRegister(instance.X, function, "t0"));
            return new MIPS() { Functions = lines };
        }
        public MIPS Visitor(MinorCil instance, IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(Utils.AcomodarVariables(instance.Y, instance.Z, function)){
                    "slt $t0, $t1, $t2" //Guarda en t0 la suma de t1 y t2
                };
            lines.AddRange(Utils.LoadFromRegister(instance.X, function, "t0"));
            return new MIPS() { Functions = lines };
        }
        public MIPS Visitor(Minor_EqualCil instance, IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(Utils.AcomodarVariables(instance.Y, instance.Z, function)){
                    "sle $t0, $t1, $t2" //Guarda en t0 la suma de t1 y t2
                };
            lines.AddRange(Utils.LoadFromRegister(instance.X, function, "t0"));
            return new MIPS() { Functions = lines };
        }
        public MIPS Visitor(NegCil instance, IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(Utils.AcomodarVariables(instance.Y, function)){
                    "neg $t0, $t1" //Guarda en t0 la suma de t1
                };
            lines.AddRange(Utils.LoadFromRegister(instance.X, function, "t0"));
            return new MIPS() { Functions = lines };
        }
        #endregion
        #region AssigCil
        public MIPS Visitor(AssigCil instance, IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>();
            lines.AddRange(Utils.SaveToRegister(instance.Y, function, "t1")); //muevo parte derecha para t1
            lines.AddRange(Utils.LoadFromRegister(instance.X, function, "t1")); //salvar desde un registro
            return new MIPS() { Functions = lines };
        }
        #endregion
        #region Attribute
        public MIPS Visitor(GetAttrCil instance, IFunctionCil function, GenerateToCil cil)
        {
            var attr = instance.Z as AttributeCil;
            var typeName = attr.Name.Substring(0, attr.Name.Length - attr.CilName.Length - 1);
            var type = cil.CilAst.TypeCils.First(x => x.Name == typeName);
            var indexOf = type.Attributes.ToList().IndexOf(attr);


            var lines = new List<string>(Utils.SaveToRegister((IVarCil)instance.Y, function, "t0")){
                $"addi $t0, $t0, {(indexOf+2)*4}",
                $"lw $t0, ($t0)"
            };
            lines.AddRange(Utils.LoadFromRegister(instance.X, function, "t0"));

            return new MIPS() { Functions = lines };
        }
        public MIPS Visitor(SetAttrCil instance, IFunctionCil function, GenerateToCil cil)
        {
            var attr = instance.Y as AttributeCil;
            var typeName = attr.Name.Substring(0, attr.Name.Length - attr.CilName.Length - 1);
            var type = cil.CilAst.TypeCils.First(x => x.Name == typeName);
            var indexOf = type.Attributes.ToList().IndexOf(attr);


            var lines = new List<string>(Utils.SaveToRegister(instance.X, function, "t0")){
                $"addi $t0, $t0, {(indexOf+2)*4}"
            };
            if (instance.Z is ValuelCil)
            {
                lines.Add($"li $t1, {instance.Z.Name}");
            }
            else
            {
                lines.AddRange(Utils.SaveToRegister(instance.Z, function, "t1"));
            }
            lines.Add($"sw $t1, ($t0)");
            return new MIPS() { Functions = lines };
        }
        #endregion
        #region MemoryManage
        public MIPS Visitor(Allocate instance, IFunctionCil function, GenerateToCil cil)
        {
            var type = instance.Y as ITypeCil;
            var modifyHeapPointer = new string[]{
                    $"addi $t1, $t1, {(type.Attributes.Count+2) * 4}",
                    $"sw $t1, heapPointer"      //Correr el heapPointer
                };
            var lines = new List<string>(){
                $"la $t0, heap",        //Cargar la direccion del heap
                $"lw $t1, heapPointer", //Cargar la direccion del heapPointer
                $"add $t0, $t0, $t1",   //Obtener la direccion a escribir
                $"move $v0, $t0",    //Guardarlo para devolverlo
                $"la $t2, type_{type.Name}_name", //Escribir el nombre
                $"sw $t2, ($t0)",   //Salvarlo
	            $"add $t0, $t0, 4",
                $"li $t2, {type.Attributes.Count}",       //Escribir la cantidad de bytes de los argumentos
	            $"sw $t2, ($t0)"   //Salvarlo
            };
            lines.AddRange(modifyHeapPointer);
            lines.AddRange(Utils.LoadFromRegister(instance.X, function, "v0"));
            return new MIPS() { Functions = lines };
        }
        public MIPS Visitor(TypeOf instance, IFunctionCil function, GenerateToCil cil)
        {
            List<string> lines = new List<string>();
            lines.AddRange(Utils.SaveToRegister(instance.Y, function, "t0"));
            lines.AddRange(Utils.LoadFromRegister(instance.X, function, "t0"));
            return new MIPS() { Functions = lines };
        }
        #endregion
        #region Call_function
        public MIPS Visitor(CallCil instance, IFunctionCil function, GenerateToCil cil)
        {
            CountArgs = 0;
            var lines = new List<string>() {
                $"jal {instance.Y.Name}"
            };
            lines.AddRange(Utils.LoadFromRegister(instance.X, function, "v0"));
            return new MIPS() { Functions = lines };
        }
        public MIPS Visitor(VCallCil instance, IFunctionCil function, GenerateToCil cil)
        {
            CountArgs = 0;
            if (instance.Y is TypeCil)
            {
                var label = ((FunctionTypeCil)instance.Z).Function.Name;
                var lines = new List<string>() {
                    $"jal {label}"
                };
                lines.AddRange(Utils.LoadFromRegister(instance.X, function, "v0"));
                return new MIPS() { Functions = lines };
            }
            else
            {
                var type = cil.CilAst.TypeCils.First(x => x.Functions.Contains(instance.Z));
                var element = type.Functions.Single(x => x.Name == instance.Z.Name);
                var index = type.Functions.ToList().IndexOf(element);
                var lines = new List<string>(Utils.SaveToRegister(instance.Y, function, "t0"))
                {
                    $"lw $t0, ($t0)",	//cargo la direccion del puntero (que es donde esta la direccion del nombre)
					$"add $t0,$t0,-4",	//en -4 esta la cantidad de metodos
					$"lw $t1, ($t0)",	//cargo la cantidad de metodos
					$"mul $t1,$t1,-4",	//desplazamiento hasta el primer metodo
					$"add $t0, $t0, $t1", //la direccion del primer metodo
					$"add $t0,$t0,{index*4}", //aumentar desplazamiento del metodo buscado
					$"lw $t0,($t0)",	//carga la direccion del metodo
					$"jalr $t0"
                };
                lines.AddRange(Utils.LoadFromRegister(instance.X, function, "v0"));
                return new MIPS() { Functions = lines };
            }
        }
        public MIPS Visitor(ArgExprCil instance, IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(Utils.SaveToRegister(instance.X, function, "t0")) {
                $"sw $t0, {(CountArgs+2)*(-4)}($sp)"
            };
            CountArgs++;
            return new MIPS() { Functions = lines };
        }
        #endregion
        #region Jump
        public MIPS Visitor(Label instance, IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(){
                    $"{instance.labelCil.Name}:" //salta condicionalmente
                };
            return new MIPS() { Functions = lines };
        }
        public MIPS Visitor(IfGoto instance, IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(Utils.AcomodarVariables(instance.VarCil, function)){
                    $"beq $t1, 1, {instance.LabelCil.Name}" //salta condicionalmente
                };
            return new MIPS() { Functions = lines };
        }
        public MIPS Visitor(GotoCil instance, IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(){
                    $"j {instance.LabelCil.Name}" //Guarda en t0 la suma de t1
                };
            return new MIPS() { Functions = lines };
        }
        #endregion
        #region Return
        public MIPS Visitor(ReturnCil instance, IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(){
                    $"lw $ra, {(function.ArgCils.Count+function.LocalCils.Count)*4}($sp)", //Carga Ra que lo habia guardado en la pila
				};
            lines.AddRange(Utils.SaveToRegister(instance.X, function, "v0"));    //Coloca el valor de retorno en el registro correspondiente
            lines.Add($"addi $sp, $sp {(function.ArgCils.Count + function.LocalCils.Count + 1) * 4}"); // Setea el SP donde estaba anteriormente
            lines.Add($"jr $ra");   //Salta para donde estaba
            return new MIPS() { Functions = lines };
        }
        #endregion
        #region String_funcions
        public MIPS Visitor(LoadCil instance, IFunctionCil function, GenerateToCil cil)
        {
            List<string> lines = new List<string>() {
                $"la $t0, {instance.Y.Name}"
            };
            lines.AddRange(Utils.LoadFromRegister(instance.X, function, "t0"));
            return new MIPS() { Functions = lines };
        }
        public MIPS Visitor(LenghtCil instance, IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(Utils.SaveToRegister(instance.Y, function, "t0")){
            "jal lengthFunctionStart"
        };
            lines.AddRange(Utils.LoadFromRegister(instance.X, function, "v0"));
            return new MIPS() { Functions = lines };
        }
        public MIPS Visitor(ConcatCil instance, IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>();
            lines.AddRange(Utils.SaveToRegister(instance.Y, function, "t0"));
            lines.AddRange(Utils.SaveToRegister(instance.Z, function, "t1"));
            lines.Add("jal concatFunctionStart");
            lines.AddRange(Utils.LoadFromRegister(instance.X, function, "v0"));
            return new MIPS() { Functions = lines };
        }
        public MIPS Visitor(SubStringCil instance, IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>();
            lines.AddRange(Utils.SaveToRegister(instance.Y, function, "t0"));
            lines.AddRange(Utils.SaveToRegister(instance.Z, function, "t1"));
            lines.AddRange(Utils.SaveToRegister(instance.L, function, "t2"));
            lines.Add("jal substrFunctionStart");
            lines.AddRange(Utils.LoadFromRegister(instance.X, function, "v0"));
            return new MIPS() { Functions = lines };
        }
        public MIPS Visitor(StrCil instance, IFunctionCil function, GenerateToCil cil)
        {
            return null;
        }
        #endregion
        #region IO
        public MIPS Visitor(In_strCil instance, IFunctionCil function, GenerateToCil cil)
        {
            List<string> lines = new List<string>()
            {
                "li $v0, 8",
                "la $a0, heap",
                "lw $t0, heapPointer",
                "add $a0, $a0, $t0",
                "li $a1, 1025",
                "syscall",		//Leo para la posicion del heapPointer 1025bytes
			    "rnw:",
                "lb $t1, heap($t0)",	//recorro hasta que llega a un salto de linea
			    "beq $t1,10,endrnw",	//si lo encuentro, salto, lo quito y pongo null
			    "beqz $t1,endrnw",	//si lo encuentro, salto, lo quito y pongo null
			    "add $t0, $t0, 1",
                "j rnw",
                "endrnw:",
                "sb $zero, heap($t0)",
                "add $t0, $t0, 1",
                "sw $t0, heapPointer"
            };
            lines.AddRange(Utils.LoadFromRegister(instance.X, function, "a0"));
            return new MIPS() { Functions = lines };
        }
        public MIPS Visitor(In_intCil instance, IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(){
                "li $v0, 5",
                "syscall"
            };
            lines.AddRange(Utils.LoadFromRegister(instance.X, function, "v0"));
            return new MIPS() { Functions = lines };
        }
        public MIPS Visitor(Out_strCil instance, IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(Utils.SaveToRegister(instance.X, function, "a0")){
                "li $v0, 4",
                "syscall",
                "la $a0, newLine",
                "li $v0, 4",
                "syscall"
            };
            return new MIPS() { Functions = lines };
        }
        public MIPS Visitor(Out_intCil instance, IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(Utils.SaveToRegister(instance.X, function, "a0")){
                "li $v0, 1",
                "syscall",
                "la $a0, newLine",
                "li $v0, 4",
                "syscall"
            };
            return new MIPS() { Functions = lines };
        }
        #endregion
        #region IsConform
        public MIPS Visitor(IsNotConformCil instance, IFunctionCil function, GenerateToCil cil)
        {
            return null;
        }
        #endregion
        #region Object
        public MIPS Visitor(Halt instance, IFunctionCil function, GenerateToCil cil)
        {
            return new MIPS() { Functions = new List<string>() { "eret" } };
        }
        public MIPS Visitor(Copy instance, IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(Utils.SaveToRegister(instance.Y, function, "t0"))
        {
            "la $t1, heap",
            "lw $t2, heapPointer",
            "add $t1, $t1, $t2",
            "move $v0, $t1",
            "lw $t2, ($t1)",
            "sw $t2, ($t0)",
            "add $t0,$t0,4",
            "add $t1,$t1,4",
            "lw $t2, ($t1)",
            "sw $t2, ($t0)",
            "move $t3, $t2",
            "loopCopy:",
            "beqz $t3,endCopy",
            "add $t0,$t0,4",
            "add $t1,$t1,4",
            "lw $t2, ($t1)",
            "sw $t2, ($t0)",
            "sub $t3, $t3, 1",
            "j loopCopy",
            "endCopy: "
        };
            lines.AddRange(Utils.LoadFromRegister(instance.X, function, "v0"));
            return new MIPS() { Functions = lines };
        }
        public MIPS Visitor(Type_Name instance, IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(Utils.SaveToRegister(instance.Y, function, "t0"))
            {
                "lw $t0, ($t0)"
            };
            lines.AddRange(Utils.LoadFromRegister(instance.X, function, "t0"));
            return new MIPS() { Functions = lines };
        }
        #endregion
    }

}
