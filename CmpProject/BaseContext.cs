using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmpProject;
namespace CmpProject
{

   public class TypeCool : IType
    {
        public string Name { get; set; }

        public IType Inherits { get; set; }

        public ISet<IMethod> Methods { get; set; }
        public IGlobalContext Parent { get; set; }

        public ISet<IAttribute> Attributes { get; set; }

        public IEnumerable<IType> Hierachty {
            get {
                if (Inherits != null)
                {
                    foreach (var item in Inherits.Hierachty)
                    {
                        yield return item;
                    }
                    yield return this;
                }
                else
                    yield return this;
            }
        }

        public bool IsBasicType { get; set; }

        public IType RealType(IType type) {
            return (this is SelfType selfType && selfType.type==type) ? type : this;
        }
        //Para las funciones que no pertenecen a ninguna clase y por tanto a ningun contexto
        public TypeCool(string Name)
        {
            this.Name = Name;
            Methods = new HashSet<IMethod>(new SymbolComparer());
            Attributes = new HashSet<IAttribute>(new SymbolComparer());
        }
        public TypeCool(string Name, IGlobalContext Parent)
        {
            this.Name = Name;
            Inherits = Parent.Object;
            Methods = new HashSet<IMethod>(new SymbolComparer());
            Attributes=new HashSet<IAttribute>(new SymbolComparer());
            this.Parent = Parent;
        }
        public TypeCool(string Name, IType Inherits, IGlobalContext Parent)
        {
            this.Name = Name;
            this.Inherits = Inherits;
            Methods = new HashSet<IMethod>(new SymbolComparer());
            Attributes = new HashSet<IAttribute>(new SymbolComparer());
            this.Parent = Parent;
        }
        //Si la variable esta definida se aplica un cortocircuito para no anadirla
        public  bool Define(string ID, IType Type)
        {
            return Attributes.Add(new Attribute(ID, Type));
        }
        //Si se encuentra el atributo le anade la expresion (se puede cambiar)
        public bool Define(string ID, IType Type, COOLgrammarParser.ExprContext initializacion)
        {
            return (!IsDefineVar(ID))&& Attributes.Add(new Attribute(ID, Type,initializacion));
        }
        //Podemos modificar el metodo y crear un scope del metodo ya que siempre que deino un metodo creo un scope nuevo
        public bool Define(string ID, IType ReturnType, IFormal[] Formals)
        {
            var method = new Method(ID, ReturnType, Formals);
            return IfRedefineMethod(method)&&Methods.Add(method);
        }
        //Se busca el atributo en la lista de atributos en el caso de que sea null
        //devolvemos el del tipo heredado
        //si el tipo heredado es null entonces retorno null
        public  IAttribute GetVar(string Name)
        {
            return Attributes.FirstOrDefault(a => a.ID == Name)??Inherits?.GetVar(Name);
        }
        //Lo mismo que el metodo anterior
        public IMethod GetMethod(string Name)
        {
            return Methods.FirstOrDefault(m => m.ID == Name)??Inherits?.GetMethod(Name);
        }
        //Si se encuentra el metodo en la lista de metodos o si se hereda de una clase se llama recursivamente este metodo
        //Si el tipo es igual al tipo indefinido entonces retorno true
        public virtual bool IsDefineMethod(string Name)
        {
            return GetMethod(Name)==null ? false : true;
        }
        public bool IsDefineVar(string Name)
        {
            return GetVar(Name) == null ? false : true;
        }
        public IObjectContext<IVar,IVar> CreateChildContext()
        {
            return new ObjectContext(this);
        }
        public IType GetType(string typeName)
        {
            return Parent.GetType(typeName);
        }     
        public override bool Equals(object obj)
        {
            return (obj is IType a)?Name==a.Name: base.Equals(obj);
        }
        public virtual bool Conform(IType a)
        {
            if (a is Undefines)
                return true;
            //aqui no tengo ni idea de lo que hay que hacer
            if (a is SelfType selfType)
                return false;
            else
                return ConformPrivate(a);
        }    
        //Esto no analiza los casos que los tipos sea undefined
        private bool ConformPrivate(IType a)
        {
            //Significa que no encontre a n en los ancestros
            
            if (Equals(a))
                return true;
            else if (Inherits == null)
                if (a.Equals(Parent.Object))
                    return true;
                else
                    return false;
            else
                return Inherits.Conform(a);
        }
        public virtual IType Join(IType a)
        {
            if (a is Undefines)
                return a;
            else
                return privateJoin(a);

        }
        private IType privateJoin(IType a)
        {
            HashSet<IType> typesC = new HashSet<IType>();
            IType type = a;
            while (type != null)
            {
                typesC.Add(type);
                type = type.Inherits;
            }
            foreach (var b in typesC)
            {
                if (Conform(b))
                    return b;
            }
            return Parent.Object;


        }
        public IType DispatchComputedType(IType type)
        {
            if (this is SelfType)
                return type;
            else
                return this;
        }
        
        public bool IfRedefineMethod(IMethod method)
        {
            var methodR = Methods.SingleOrDefault(m => m.ID == method.ID);
            if (methodR!=null)
            {
                //Encontro el metodo por tanto este se redefiine
                if (methodR.Equals(method))
                {
                    methodR.IsRedefined = true;
                    return true;
                }
                return false;
            }
            return (Inherits?.IfRedefineMethod(method)??true);
        }

        public bool IfDefineInTheCurrentType(string ID)
        {
            return Methods.Any(m => m.ID == Name);
        }
    }
   class Undefines : TypeCool
    {    
        public Undefines( IGlobalContext Parent) : base(null, Parent){}

        //El tipo undefined hereda de todo objeto para que en el chequo semantico no se atrapen errores
        public override bool Conform(IType a) => true;
        public override IType Join(IType a) => this;

    }
   class SelfType : TypeCool
    {
        public IType type { get; set; }
        
        public SelfType(IType type,IGlobalContext globalContext):base("SELF_TYPE",globalContext)
        {
            this.type = type;
        }
        public override bool Conform(IType a)
        {
            if (a is SelfType selfType)
            {
                //Puede que haya que tomar en cuenta otra cosa aqui
                return selfType.type == type;
            }
            else
            {
                return type.Conform(a);
            }
        }
    }
   public class ObjectContext : IObjectContext<IVar,IVar>
    {
        public IObjectContext<IVar,IVar> Parent { get; set; }
        public ISet<IVar> Attributes { get; set; }
        public ObjectContext(IObjectContext<IVar,IVar> Parent)
        {
            this.Parent = Parent;
            Attributes = new HashSet<IVar>(new SymbolComparer());
        }
        public IObjectContext<IVar,IVar> CreateChildContext()
        {
            return new ObjectContext(this);
        }
        public virtual bool Define(string var, IType type)
        {
            return Attributes.Add(new BaseVar(var, type));
        }
        public virtual IType GetType(string typeName)
        {
            return Parent.GetType(typeName);
        }
        public virtual IVar GetVar(string var)
        {
            return Attributes.FirstOrDefault(v => v.ID == var) ?? Parent.GetVar(var);
        }
        public virtual bool IsDefineVar(string Name)
        {
            return GetVar(Name) == null ? false : true;
        }
        public bool IfDefineType(string typeName)
        {
            return IfDefineType(typeName);
        }
    }
   public class GlobalContext : IGlobalContext
   {
        public ISet<IType> types { get; }
        public IType Undefined { get; }
        public IType Int { get; set; }
        public IType String { get; set; }
        public IType Bool { get; set; }
        public IType Object { get; set; }
        public IType Self_Type { get; }

        public IType IO { get; set; }

        public IType Main { get; set; }

        public GlobalContext()
        {
            types = new HashSet<IType>();
            Undefined=  CreateChildContext();
            Self_Type = CreateChildContext(Object);
  
        }
        public IType CreateChildContext(string typeName)
        {

            //Si typeName es null entonces es un tipo indefinido
            var type =new TypeCool(typeName, this);
            types.Add(type);
            if (typeName == "Object")
                Object = type;
            else if (typeName == "IO")
                IO = type;
            else if (typeName == "Main")
                Main = type;
            else if (typeName == "Int")
                Int = type;
            else if (typeName == "String")
                String = type;
            else if (typeName == "Bool")
                Bool = type;
            return type;
        }
        public IType CreateChildContext(IType typeName)
        {
            var type = new SelfType(typeName, this);
            types.Add(type);
            return type;
        }
        public IType CreateChildContext()
        {
            return new Undefines(this);
        }
        public IType GetType(string typeName)
        {
            return types.FirstOrDefault(t => t.Name == typeName)??Undefined;
        }
        public IType GetType(string typeName, IType ActualClass)
        {
            if (typeName == "SELF_TYPE")
                return new SelfType(ActualClass, this);
            return types.FirstOrDefault(t => t.Name == typeName) ?? Undefined;
        }
        public bool IfDefineType(string typeName)
        {
            return GetType(typeName).Equals(Undefined) ? false : true;
        }

        public bool IfDefineType(IType type)
        {
            return GetType(type.Name).Equals(Undefined) ? false : true;
        }

        public bool IfSELF_TYPE(string type)
        {
            return "SELF_TYPE" == type;
        }

    }
   public class SymbolComparer : EqualityComparer<ISymbol>
    {
        public override bool Equals(ISymbol x, ISymbol y)
        {
            return x.ID == y.ID;
        }

        public override int GetHashCode(ISymbol obj)
        {
            return obj.ID.GetHashCode();
        }
    }
   public class Symbol : ISymbol
    {
        public string ID { get; }

        public IType Type { get; }
        public Symbol(string ID, IType Type)
        {
            this.ID = ID;
            this.Type = Type;
        }
    }
   public class BaseVar : Symbol,IVar
    {
        public BaseVar(string ID, IType Type) : base(ID, Type)
        {
        }
    }
   public class Attribute :BaseVar, IAttribute
    {
        public Attribute(string ID, IType Type): base(ID, Type)
        {

        }
        public Attribute(string ID, IType Type, COOLgrammarParser.ExprContext initializacion) : base(ID, Type)
        {
            this.initializacion = initializacion;
        }
        public COOLgrammarParser.ExprContext initializacion { get; set; }
    }
   public class Method :Symbol,  IMethod {

        public IFormal[] Formals { get; set; }

        public bool IsRedefined { get; set; }

        public Method(string ID, IType Type,IFormal[]Formals): base(ID, Type)
            {
                this.Formals = Formals;
                IsRedefined = false;
            }
            public override bool Equals(object obj)
            {
                if (obj is Method method)
                {
                    if (Formals.Length != method.Formals.Length)
                        return false;
                    for (int i = 0; i < Formals.Length; i++)
                    {
                        if (Formals[i].Type != method.Formals[i].Type)
                            return false;
                    }
                    if (Type != method.Type)
                        return false;
                return true;
                }
                return base.Equals(obj);
            }
    }
   public class Formal :BaseVar, IFormal {
     
        public Formal(string ID,IType Type) : base(ID, Type)
        {

        }
        
    }

   public class ErrorLogger : IErrorLogger
    {
        public IList<string> msgs { get; }
        public ErrorLogger()
        {
            msgs = new List<string>();
        }
        public void LogError(string msg)
        {
            msgs.Add(msg);
        }
    }


}
