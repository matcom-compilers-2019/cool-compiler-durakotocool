using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace CmpProject.CIL
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
                var index = function.ArgCils.ToList().FindIndex(i => i.Name.Equals(x.Name));
                result.Add($"move $a{index}, ${src}");
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
                var index = function.ArgCils.ToList().FindIndex(i => i.Name.Equals(x.Name));
                result.Add($"move ${dest}, $a{index}");
            }
            else    //si y es un valor
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
            List<string> result = new List<string>() {
                "\n\r",
                $"{function.Name}:",
                $"sub $sp, $sp, {(list.Count + 1) * 4}",
                $"sw $ra, {list.Count*4}($sp)"
            };   //reservar el espacio para guardar las variables locales en al pila
            for (int i = 0; i < list.Count; i++)
            {
                var index = (list.Count - i - 1) * 4;
                result.Add("li $t3, 0");
                result.Add($"sw $t3, {index}($sp)\t\t##{list[i].Name}");
                function.localsDict.Add(list[i].Name,index);
            }
            
            return result;
        }

    }
#region Data
    class StringCil:ValuelCil,IStringCil
    {
        public StringCil(string value):base(value)
        {
            Name = value;
        }
        public override string ToString()
        {
            return Name;
        }
    }
    class DataStringCil : IDataStringCil
    {
        public IVarCil varCil { get; set; }
        public IStringCil stringCil { get; set; }
        public DataStringCil(IVarCil varCil, IStringCil stringCil)
        {
            this.varCil = varCil;
            this.stringCil = stringCil;
        }
        public override string ToString()
        {
            return $"{varCil}={stringCil}\n";
        }
    }
    ////Variable que que 
    //class VarCilArray:VarCil
    //{
    //    public VarCilArray(string name) : base(name)
    //    {
    //    }
    //}
    #endregion
#region CIL
    class CilAst: ICilAst
    {
        public ISet<ITypeCil> TypeCils { get; set; }
        public ISet<IFunctionCil> FunctionCils { get; set; }
        public ISet<IDataStringCil> dataStringCils { get; set; }
        public ITypeCil Object { get ; set; }

        public IFunctionCil void_init => FunctionCils.Single(t => t.Name == "void@Init");

        public CilAst()
        {
                TypeCils=new HashSet<ITypeCil>();
                FunctionCils=new HashSet<IFunctionCil>(new NameComparer());
                dataStringCils = new HashSet<IDataStringCil>();
        }
        public override string ToString()
        {
            return $"{"TYPES."}\n{ Others.joinImplementation( TypeCils) } \n{"DATA."}\n{Others.joinImplementation(dataStringCils)}\n{"CODE."}\n{ Others.joinImplementation( FunctionCils)}";
        }

        public ITypeCil GetTypeCilByName(string Name, ITypeCil typeCil)
        {
            if (Name == "SELF_TYPE")
                return typeCil;
            return TypeCils.SingleOrDefault(t => t.Name == Name);
        }
        public ITypeCil GetTypeCilByName(string Name)
        {
            return TypeCils.SingleOrDefault(t => t.Name == Name);
        }
        public IFunctionCil GetFunctionCilsByName(string Name)
        {
            return FunctionCils.SingleOrDefault(t => t.Name == Name);
        }
        public IStringCil GetStringCilByName(string Name)
        {
            return dataStringCils.SingleOrDefault(t => t.varCil.Name == Name).stringCil;
        }

        public IFunctionCil CreateFunctionCil(string Name, string CilName)
        {
            var result = GetFunctionCilsByName($"{Name}@{CilName}") ?? new FunctionCil(Name, CilName);
            FunctionCils.Add(result);
            return result;
        }
    }
    class NameComparer : IEqualityComparer<IFunctionCil>
    {
        public NameComparer() { }
        public bool Equals(IFunctionCil x, IFunctionCil y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(IFunctionCil obj)
        {
            return obj.Name.GetHashCode();
            //return obj.GetHashCode();
        }
    }
    class Ide:IIde
    {
        public string Name { get; set; }
        public Ide(string name)
        {
            this.Name = name;
        }

   
    }

    public class HolderCil :IHolderCil
    {
        public string Name { get ; set; }
        public HolderCil(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object obj)
        {
            if (obj is IHolderCil valuelCil)
            {
                return Name == valuelCil.Name;
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
    class ValuelCil : HolderCil,IValuelCil
    {
        
        public ValuelCil(string value):base(value)
        {
            Name = value;
        }

    }
    class VarCil:HolderCil,IVarCil
    {
        public VarCil(string name):base(name)
        {
        }
        
    }

#endregion
#region  Type
    class TypeCil:ValuelCil,ITypeCil
    {
        public  ISet<IAttributeCil> Attributes { get; set; }
        public  ISet<IFunctionTypeCil> Functions { get; set; }
        public IFunctionTypeCil Init { get; set ; }

        public TypeCil(string name,ICilAst cilAst):base(name)
        {
            Attributes=new HashSet<IAttributeCil>();
            Functions=new HashSet<IFunctionTypeCil>();
            //Seria el metodo real com nombre "{Name}_Init" y el nombre de la clase Init para que se pueda llamar indistintamente del tipo que sea(cuando el tipo es un tipo dinamico)
            var ctor = cilAst.CreateFunctionCil(Name, "Init");
            var init = new FunctionTypeCil(null, "Init", ctor);
            AddFunc(init);
            Init = init;
        }

        public void AddAttr(IAttributeCil attributeCil)
        {
            var attribute = Attributes.FirstOrDefault(a => a.Name == attributeCil.Name);
            if (attribute==null)
                Attributes.Add(attributeCil);
            else
            {
                Attributes.Remove(attribute);
                Attributes.Add(attributeCil);
            }
        }
        public void AddFunc(IFunctionTypeCil functionCil)
        {
            var function = Functions.FirstOrDefault(a =>a.CilName == functionCil.CilName);
            if (function==null)
                Functions.Add(functionCil);
            else
                function.Function = functionCil.Function;
        }
        public override string ToString()
        {
            return $"type {Name} {{\n{Others.joinImplementation( Attributes)}{ Others.joinImplementation(Functions)}}}\n";
        }

        public IFunctionTypeCil GetFunctionCilsByCoolName(string CoolName)
        {
            return Functions.SingleOrDefault(t => t.CilName == CoolName);
        }
        public IAttributeCil GetAttributeCilsByCoolName(string CoolName)
        {
            return Attributes.SingleOrDefault(t => t.CilName == CoolName);
        }

        public int GetIndexAttributeCilsByCoolName(string CoolName)
        {
            return Attributes.ToList().IndexOf(GetAttributeCilsByCoolName(CoolName));
        }
    }
    abstract class FeuturesCil : VarCil,IFeuturesCil
    {
        public string CilName { get; set; }
        //Si TypeName es igual true o pertenece a ninguna clase
        public FeuturesCil(string TypeName,string CilName):base((TypeName == null)?CilName:$"{TypeName}@{CilName}")
        {
            this.CilName = CilName;
        }
        
    }
    class AttributeCil:FeuturesCil,IAttributeCil
    {
        public AttributeCil(string TypeName, string CilName) : base(TypeName,CilName)
        {
        }
        public override string ToString()
        {
            return $"    attribute {Name} ;\n";
        }

    }
    class  FunctionTypeCil: FeuturesCil, IFunctionTypeCil
    {
        public  IFunctionCil Function { get; set; }
        public FunctionTypeCil(string TypeName, string CilName) : base(TypeName, CilName)
        {
        }
       
        public FunctionTypeCil(string TypeName, string CilName, IFunctionCil function) : base(TypeName,CilName)
        {
            Function = function;
        }
        
        public override string ToString()
        {
            return $"    method {Name} : {Function.Name} ;\n";
        }
    }
#endregion
#region Function
    class FunctionCil : FeuturesCil, IFunctionCil
    {
        public ISet<IArgCil> ArgCils { get; set; }
        public ISet<ILocalCil> LocalCils { get; set; }
        public ISet<IThreeDirIns> ThreeDirInses { get; set; }
        public Dictionary<string, int> localsDict { get; set; }
        public IVarCil self => ArgCils.SingleOrDefault(t=>t.Name=="self")?? (IVarCil)LocalCils.Single(t => t.Name == "self");

        //Por ahora no hago un constructor de argCILs o localCILs
        public FunctionCil(string TypeName, string CilName) :base(TypeName,CilName)
        {
            ArgCils = new HashSet<IArgCil>();
            LocalCils = new HashSet<ILocalCil>();
            ThreeDirInses=new HashSet<IThreeDirIns>();
            localsDict = new Dictionary<string, int>();
        }
        
        //Voy a cambiarlo
        public override string ToString()
        {
            return $"function {Name} {{ \n{ Others.joinImplementation(new HashSet<string>( ArgCils.Select(t=> $"   PARAM {t.Name}\n"))) }{Others.joinImplementation(new HashSet<string>(LocalCils.Select(t => $"   LOCAL {t.Name}\n")))}{Others.joinImplementation(ThreeDirInses)} \n}}\n";
        }

       
    }
    class ArgCil:VarCil,IArgCil
    {
        public ArgCil(string name) : base(name)
        {

        }
        public override string ToString()
        {
            return Name;
        }
    }
    class LocalCil:VarCil,ILocalCil
    {
        public LocalCil(string name) : base(name)
        {

        }
        public override string ToString()
        {
            return Name;
        }
    }
#endregion
#region ThreeDirIns

    public abstract class ThreeDirIns : IThreeDirIns
    {
        public abstract MIPS ToMIPS(IFunctionCil function, GenerateToCil cil);
    }
    //Esta clase representa la instrucciones que contienen dos holder de CIL
    public abstract class BinaryVarCil:UnaryCil
    {
        public IHolderCil Z { get; set; }
        public BinaryVarCil(IVarCil x, IHolderCil y, IHolderCil z):base(x,y)
        {
            Z = z;
        }
    }
    //Esta clase representa la instrucciones que contienen un holder de CIL
    public abstract class UnaryCil: ZyroCil
    {
        public IHolderCil Y { get; set; }
        protected UnaryCil(IVarCil x,IHolderCil y):base(x)
        {
            Y = y;
        }
    }
    public abstract class ZyroCil : ThreeDirIns
    {

        public IVarCil X { get; set; }
        protected ZyroCil(IVarCil x)
        {
            X = x;
        }
        

    }
    #endregion
#region Operations
    class SumCil : BinaryVarCil
    {
        public SumCil(IVarCil x, IHolderCil y, IHolderCil z) : base(x, y, z)
        {
        }

        public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(Utils.AcomodarVariables(Y, Z, function)){
                    "add $t0, $t1, $t2" //Guarda en t0 la suma de t1 y t2
                };
            if (function.LocalCils.Contains(X))
            {
                var index = function.localsDict[X.Name];
                lines.Add($"sw $t0, {index}($sp)");
            }
            return new MIPS() { Functions = lines };
        }

        public override string ToString()
        {
            return $"   {X}={Y}+{Z}\n";
        }
    }//
    class RestCil : BinaryVarCil
    {
        public RestCil(IVarCil x, IHolderCil y, IHolderCil z) : base(x, y, z)
        {
        }

        public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(Utils.AcomodarVariables(Y, Z, function)){
                    "sub $t0, $t1, $t2" //Guarda en t0 la suma de t1 y t2
                };
            if (function.LocalCils.Contains(X))
            {
                var index = function.localsDict[X.Name];
                lines.Add($"sw $t0, {index}($sp)");
            }
            return new MIPS() { Functions = lines };
        }

        public override string ToString()
        {
            return $"   {X}={Y}-{Z}\n";
        }
    }//
    class MultCil : BinaryVarCil
    {
        public MultCil(IVarCil x, IHolderCil y, IHolderCil z) : base(x, y, z)
        {
        }

        public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(Utils.AcomodarVariables(Y, Z, function)){
                    "mul $t0, $t1, $t2" //Guarda en t0 la suma de t1 y t2
                };
            if (function.LocalCils.Contains(X))
            {
                var index = function.localsDict[X.Name];
                lines.Add($"sw $t0, {index}($sp)");
            }
            return new MIPS() { Functions = lines };
        }
        public override string ToString()
        {
            return $"   {X}={Y}*{Z}\n";
        }
    }//
    class DivCil : BinaryVarCil
    {
        public DivCil(IVarCil x, IHolderCil y, IHolderCil z) : base(x, y, z)
        {
        }

        public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(Utils.AcomodarVariables(Y, Z, function)){
                    "div $t0, $t1, $t2" //Guarda en t0 la suma de t1 y t2
                };
            if (function.LocalCils.Contains(X))
            {
                var index = function.localsDict[X.Name];
                lines.Add($"sw $t0, {index}($sp)");
            }
            return new MIPS() { Functions = lines };
        }
        public override string ToString()
        {
            return $"   {X}={Y}/{Z}\n";
        }
    }//
    class EqualCil : BinaryVarCil
    {
        public EqualCil(IVarCil x, IHolderCil y, IHolderCil z) : base(x, y, z)
        {
        }

        public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(Utils.AcomodarVariables(Y, Z, function)){
                    "seq $t0, $t1, $t2" //Guarda en t0 la suma de t1 y t2
                };
            if (function.LocalCils.Contains(X))
            {
                var index = function.localsDict[X.Name];
                lines.Add($"sw $t0, {index}($sp)");
            }
            return new MIPS() { Functions = lines };
        }
        public override string ToString()
        {
            return $"   {X}={Y.Name}=={Z.Name}\n";
        }
    }//
    class NotEqualCil : BinaryVarCil
    {
        public NotEqualCil(IVarCil x, IHolderCil y, IHolderCil z) : base(x, y, z)
        {
        }

        public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(Utils.AcomodarVariables(Y, Z, function)){
                    "sne $t0, $t1, $t2" //Guarda en t0 la suma de t1 y t2
                };
            if (function.LocalCils.Contains(X))
            {
                var index = function.localsDict[X.Name];
                lines.Add($"sw $t0, {index}($sp)");
            }
            return new MIPS() { Functions = lines };
        }
        public override string ToString()
        {
            return $"   {X}={Y.Name}!={Z.Name}\n";
        }
    }
    class MinorCil : BinaryVarCil
    {
        public MinorCil(IVarCil x, IHolderCil y, IHolderCil z) : base(x, y, z)
        {
        }

        public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(Utils.AcomodarVariables(Y, Z, function)){
                    "slt $t0, $t1, $t2" //Guarda en t0 la suma de t1 y t2
                };
            if (function.LocalCils.Contains(X))
            {
                var index = function.localsDict[X.Name];
                lines.Add($"sw $t0, {index}($sp)");
            }
            return new MIPS() { Functions = lines };
        }
        public override string ToString()
        {
            return $"   {X}={Y}<{Z}\n";
        }
    }
    class Minor_EqualCil : BinaryVarCil
    {
        public Minor_EqualCil(IVarCil x, IHolderCil y, IHolderCil z) : base(x, y, z)
        {
        }
        public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(Utils.AcomodarVariables(Y, Z, function)){
                    "sle $t0, $t1, $t2" //Guarda en t0 la suma de t1 y t2
                };
            if (function.LocalCils.Contains(X))
            {
                var index = function.localsDict[X.Name];
                lines.Add($"sw $t0, {index}($sp)");
            }
            return new MIPS() { Functions = lines };
        }
        public override string ToString()
        {
            return $"   {X}={Y}<={Z}\n";
        }
    }
    class NegCil : UnaryCil
    {
        public NegCil(IVarCil x, IHolderCil y) : base(x, y)
        {
        }
        public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(Utils.AcomodarVariables(Y, function)){
                    "neg $t0, $t1" //Guarda en t0 la suma de t1
                };
            if (function.LocalCils.Contains(X))
            {
                var index = function.localsDict[X.Name];
                lines.Add($"sw $t0, {index}($sp)");
            }
            return new MIPS() { Functions = lines };
        }
        public override string ToString()
        {
            return $"   {X}=-{Y}\n";
        }
    }
    #endregion
#region AssigCil
    class AssigCil :UnaryCil
    {
        public AssigCil(IVarCil x, IHolderCil y) : base(x, y)
        {
        }
        public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>();
            lines.AddRange(Utils.SaveToRegister(Y, function, "t1")); //muevo parte derecha para t1
            lines.AddRange(Utils.LoadFromRegister(X, function, "t1")); //salvar desde un registro
            return new MIPS() { Functions = lines };
        }
        public override string ToString()
        {
            return $"   {X}={Y}\n";
        }
    }//
    #endregion
#region Attribute
    class GetAttrCil:BinaryVarCil
    {
        //public IVarCil Y { get; set; }
        public GetAttrCil(IVarCil x, IVarCil y, IHolderCil b) : base(x,y, b)
        {
        }
        public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
        {
            var attr = Z as AttributeCil;
            var typeName = attr.Name.Substring(0, attr.Name.Length - attr.CilName.Length - 1);
            var type = cil.CilAst.TypeCils.First(x => x.Name == typeName);
            var indexOf = type.Attributes.ToList().IndexOf(attr);


            var lines = new List<string>(Utils.SaveToRegister((IVarCil)Y, function, "t0")){
                $"addi $t0, $t0, {(indexOf+2)*4}",
                $"lw $t1, ($t0)"
            };
            lines.AddRange(Utils.LoadFromRegister(X, function, "t0"));
            
            return new MIPS() { Functions = lines };
        }
        public override string ToString()
        {
            return $"   {X}=GETATTR {Y.Name} {Z.Name}\n";
        }
    }
    //X(objeto) Y(atributo) y Z(valor)
    class SetAttrCil:BinaryVarCil
    {
        public SetAttrCil(IVarCil x, IHolderCil b ,IHolderCil y) : base(x, b,y)
        {
        }
        public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
        {
            var attr = Y as AttributeCil;
            var typeName = attr.Name.Substring(0,attr.Name.Length - attr.CilName.Length - 1);
            var type = cil.CilAst.TypeCils.First(x => x.Name == typeName);
            var indexOf = type.Attributes.ToList().IndexOf(attr);


            var lines = new List<string>(Utils.SaveToRegister(X, function, "t0")){
                $"addi $t0, $t0, {(indexOf+2)*4}"
            };
            lines.AddRange(Utils.LoadFromRegister((IVarCil)Y, function, "t1"));
            lines.Add($"sw $t1, ($t0)");
            return new MIPS() { Functions = lines };
        }
        public override string ToString()
        {
            return $"   SETATTR {X} {Y.Name} {Z}\n";
        }
    }
    #endregion
#region ArrayAcces
    //Por si acaso el indixe i puede ser un valor pelao
    abstract class IndexCil:ThreeDirIns
    {
        
        public VarCil A { get; set; }
        public HolderCil I { get; set; }
        protected IndexCil( VarCil a, HolderCil i)
        {
            A = a;
            I = i;
        }
    }
    class GetIndex:IndexCil
    {
        public VarCil X { get; set; }
        public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
        {
            return null;
        }
        public GetIndex(VarCil x, VarCil a, HolderCil i) : base( a, i)
        {
            X = x;
        }
    }
    class  SetIndex:IndexCil
    {
        public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
        {
            return null;
        }
        public SetIndex( VarCil a, HolderCil i, HolderCil x) : base( a, i)
        {

        }
    }
    #endregion
#region MemoryManage
    //I lo trato como un Ivar ya que en COOl los enteros son un objeto
    abstract class TypeManage:UnaryCil
    {

        public TypeManage(IVarCil x, ITypeCil typeCil):base(x,typeCil)
        {
        }
    }
    class Allocate:TypeManage
    {
        internal static int heapPointer = 0;
        public Allocate(IVarCil x, ITypeCil typeCil) : base(x, typeCil)
        {
        }
        public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
        {
            var type = Y as ITypeCil;
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
                $"li $t2, {type.Attributes.Count*4}",       //Escribir la cantidad de bytes de los argumentos
	            $"sw $t2, ($t0)$"   //Salvarlo
            };
            lines.AddRange(modifyHeapPointer);
            lines.AddRange(Utils.LoadFromRegister(X, function, "v0"));
            return new MIPS() { Functions = lines };
        }
        public override string ToString()
        {
            return $"   {X}=ALLOCATE {Y.Name}\n";
        }
    }
    class TypeOf:UnaryCil
    {
        public TypeOf(IVarCil x, IHolderCil typeCil) : base(x, typeCil)
        {
        }
        public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
        {
            return null;
        }
        public override string ToString()
        {
            return $"   {X}= TYPEOF {Y.Name}\n";
        }
    }
    class DecArrayCil:UnaryCil
    {
        public DecArrayCil(IVarCil x, IHolderCil y) : base(x, y)
        {
           
        }
        public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
        {
            return null;
        }
    }
#endregion
#region Call_function
    class CallCil: UnaryCil
    {
        public CallCil(IVarCil x, IFunctionCil f):base(x,f)
        {
        }
        public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
        {
            ArgExprCil.count = 0;
            var lines = new List<string>() {
                $"jal {Y.Name}"
            };
            lines.AddRange(Utils.LoadFromRegister(X, function, "v0"));
            return new MIPS() { Functions = lines };
        }
        public override string ToString()
        {
            return $"   {X}=CALL {Y.Name}\n";
        }
    }
    class VCallCil: BinaryVarCil
    {

        public VCallCil(IVarCil x, IHolderCil t, IHolderCil f):base(x,t,f)
        {
            
        }
        public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
        {
            ArgExprCil.count = 0;
            if (Y is TypeCil)
            {
                var label = ((FunctionTypeCil)this.Z).Function.Name;
                var lines = new List<string>() {
                    $"jal {label}"
                };
                lines.AddRange(Utils.LoadFromRegister(X, function, "v0"));
                return new MIPS() { Functions = lines };
            }
            return null;
        }
        public override string ToString()
        {
            return $"   {X}=VCALL {Y.Name} {Z.Name}\n";
        }
    }
    class ArgExprCil:ThreeDirIns
    {
        internal static int count = 0;
        public IHolderCil X { get; set; }
        public ArgExprCil(IHolderCil x)
        {
            X = x;
        }
        public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
        {
            if (count < 4)
            {
                var lines = new List<string>(Utils.SaveToRegister(X,function,$"a{count}"));
                count++;
                return new MIPS() { Functions = lines };
            }
            return null;
        }
        public override string ToString()
        {
            return $"   arg {X}\n";
        }
    }
    #endregion
#region Jump
    class LabelCil :Ide,ILabelCil
    {
        public LabelCil(string name) : base(name)
        {
        }
        public override string ToString()
        {
            return Name;
        }
    }
    class Label:ThreeDirIns
    {
        public ILabelCil labelCil { get; set; }
        public Label(ILabelCil labelCil)
        {
            this.labelCil = labelCil;
        }
        public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(){
                    $"{labelCil.Name}:" //salta condicionalmente
                };
            return new MIPS() { Functions = lines };
        }
        public override string ToString()
        {
            return $"   {labelCil.Name}:\n";
        }
    }
    abstract class Goto : ThreeDirIns
    {
        public ILabelCil LabelCil { get; set; }
        public Goto(ILabelCil labelCil)
        {
            LabelCil = labelCil;
        }
    }
    class IfGoto: Goto
    {

        public IHolderCil VarCil { get; set; }
        public IfGoto(IHolderCil varCil, ILabelCil labelCil):base(labelCil)
        {
            VarCil = varCil;
        }
        public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(Utils.AcomodarVariables(VarCil, function)){
                    $"beq $t1, 1, {LabelCil.Name}" //salta condicionalmente
                };
            return new MIPS() { Functions = lines };
        }
        public override string ToString()
        {
            return $"   if {VarCil} goto {LabelCil}\n";
        }
    }
    class GotoCil: Goto
    {
        public GotoCil(ILabelCil labelCil):base(labelCil){}
        public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
        {
            var lines = new List<string>(){
                    $"j {LabelCil.Name}" //Guarda en t0 la suma de t1
                };
            return new MIPS() { Functions = lines };
        }
        public override string ToString()
        {
            return $"   goto {LabelCil.Name}\n";
        }
    }
    #endregion
#region Return
    class ReturnCil:ThreeDirIns
    {
        public IHolderCil X { get; set; }
        public ReturnCil(IHolderCil x) 
        {
            X = x;
        }
        public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
        {
            string returnedValue;

            if (function.LocalCils.Contains(X)) //si y es una variable local
            {
                var index = function.localsDict[X.Name];
                returnedValue = $"lw $v0, {index}($sp)";
            }
            else if (function.ArgCils.Contains(X))
            {   //si y es un parametro
                var index = function.ArgCils.ToList().FindIndex(i => i.Name.Equals(X.Name));
                returnedValue =$"move $v0, $a{index}";
            }
            else if (X is ValuelCil)
                returnedValue = $"lw $v0, {X.Name}";   
            else
                returnedValue = $"li $v0, 0";

            var lines = new List<string>(){
                    $"lw $ra, {function.LocalCils.Count*4}", //Carga Ra que lo habia guardado en la pila
                    $"addi $sp, $sp {(function.LocalCils.Count + 1) * 4}", // Setea el SP donde estaba anteriormente
                    returnedValue,    //Coloca el valor de retorno en el registro correspondiente
                    $"jr $ra"   //Salta para donde estaba
                };
            return new MIPS() { Functions = lines };
        }
        public override string ToString()
        {
            return $"   return {X}\n";
        }
    }//
#endregion
#region String_funcions
class LoadCil: UnaryCil
{

    public LoadCil(IVarCil x, IVarCil stringCil):base(x,stringCil)
    {
    }
    public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
    {
        List<string> lines = new List<string>() {
                $"lw $t0, {Y.Name}"
            };
        lines.AddRange(Utils.LoadFromRegister(X, function, "t0"));
        return new MIPS() { Functions = lines };
    }
    public override string ToString()
    {
        return $"   {X}= LOAD {Y}\n";
    }
}
class LenghtCil:UnaryCil
{
       
    public LenghtCil(IVarCil x, IHolderCil y) : base(x,y){
    }
    public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
    {
        return null;
    }
    public override string ToString()
    {
        return $"   {X} = LENGHT {Y}\n";
    }
}
class ConcatCil : BinaryVarCil
{
    public ConcatCil(IVarCil x, IHolderCil y, IHolderCil z) : base(x, y, z)
    {
    }
    public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
    {
        return null;
    }
    public override string ToString()
    {
        return $"   {X} = CONCAT {Y} {Z}\n";
    }
}
class SubStringCil : BinaryVarCil
{
    public IHolderCil L { get; set; }
    public SubStringCil(IVarCil x, IHolderCil y, IHolderCil i,IHolderCil l) : base(x, y, i)
    {
        L = l;
    }
    public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
    {
        return null;
    }
    public override string ToString()
    {
        return $"   {X} = SUBSTRING {Y} {Z} {L}\n";
    }
}
class StrCil:ZyroCil
{
    public IVarCil Y { get; set; }
    public StrCil(IVarCil x, IVarCil y) : base(x)
    {
        Y = y;
    }
    public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
    {
        return null;
    }
}
#endregion
#region IO
class In_strCil:ZyroCil
{
    public In_strCil(IVarCil x):base(x)
    {
    }
    public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
    {
        return null;
    }
    public override string ToString()
    {
        return $"   {X}=in_str\n";
    }
}
class In_intCil : ZyroCil
{
    public In_intCil(IVarCil x) : base(x)
    {
    }
    public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
    {
        return null;
    }
    public override string ToString()
    {
        return $"   {X}=in_int\n";
    }
}
class Out_strCil:ZyroCil
{
    public Out_strCil(IVarCil x):base(x)
    {
    }
    public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
    {
        return null;
    }
    public override string ToString()
    {
        return $"   out_str {X}\n";
    }
}
class Out_intCil : ZyroCil
{
    public Out_intCil(IVarCil x) : base(x)
    {
    }
    public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
    {
        return null;
    }
    public override string ToString()
    {
        return $"   out_int {X}\n";
    }
}
#endregion
#region IsConform
//Esta expresion devuelve 1 si a<=b y 0 etc.
class IsNotConformCil:BinaryVarCil
{

    public IsNotConformCil( IVarCil x, IHolderCil a, IHolderCil b):base(x,a,b){}
    public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
    {
        return null;
    }
    public override string ToString()
    {
        return $"   {X}= ISNOTCONFORM {Y.Name} {Z.Name}\n";
    }
}
#endregion
#region Object
class Halt:ThreeDirIns
{
    public Halt()
    {
    }
    public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
    {
        return null;
    }
    public override string ToString()
    {
        return $"   halt\n";
    }
}
class Copy : UnaryCil
{
    public Copy(IVarCil x, IHolderCil y) : base(x, y)
    {

    }

    public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
    {
            return null;
    }
    public override string ToString()
    {
        return $"   {X}= copy {Y}\n";
    }
}
class Type_Name : UnaryCil
{
    public Type_Name(IVarCil x, IHolderCil y) : base(x, y)
    {

    }

    public override MIPS ToMIPS(IFunctionCil function, GenerateToCil cil)
    {
            return null;
    }
    public override string ToString()
    {
        return $"   {X}= type_name {Y}\n";
    }
}
#endregion
    static class Others
    {
        //Este metodo empata las implementaciones que hereden de Ide (tipos o funciones)
        public static string joinImplementation<T>  (ISet<T> ides)
        {
            if (ides.Count==0)
            {
                return "";
            }
            else
            {
                return ides.Select(t => t.ToString()).Aggregate((x, y) => x + y);
            }
        }

    }
    class ContextCil:IContextCil
    {
        public IDictionary<string, IVarCil> variables { get; set; }
        public ContextCil()
        {
            variables = new Dictionary<string, IVarCil>(new NameComparer2());
        }
        public IContextCil CreateAChild()
        {
            var result = new ContextCil();
            result.variables = new Dictionary<string, IVarCil>(variables, new NameComparer2());
            return result;
        }
        public void Define(string var)
        {
            var regex = new Regex($"^(?<Num>\\d)(?<Id>{var})$");
            if (variables.ContainsKey(var))
            {
                var groups = regex.Match(variables[var].Name).Groups;
                var integre = int.Parse((groups["Num"].Value==string.Empty)?"0": groups["Num"].Value);
                variables[var] = new VarCil($"{++integre}{var}");
            }
            else
            {
                if (var == "self")
                    variables[var] = new VarCil($"self");
                else
                    variables[var] = new VarCil(var);
            }
        }

    }
    class NameComparer2 : IEqualityComparer<string>
    {
        public NameComparer2() { }
        public bool Equals(string x, string y)
        {
            return x == y;
        }
        public int GetHashCode(string obj)
        {
            return obj.GetHashCode();
            //return obj.GetHashCode();
        }
    }
}
