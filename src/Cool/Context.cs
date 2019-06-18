using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static COOLgrammarParser;
namespace CmpProject
{
    //Representa el contexto de objetos
    public interface IContext
    {

        //Al acceder a un tipo accedo al contexto de objetos de este
        IType GetType(string typeName);

       
    }
    //Es el contexto global del programa que engloba todas las clases
    public interface IGlobalContext : IContext
    {
        //Creo un scope hijo que es un nuevo tipo
        IType Undefined { get; }

        IType Int { get; }

        IType String { get; }

        IType Bool { get; }
        IType Object { get; }
        IType IO { get; }
        IType Main { get;}
        IType CreateChildContext(string typeName);
        //Este crea un tipo selftype
        IType CreateChildContext(IType typeName);
        //Para el tipo undefined
        IType CreateChildContext();
        //Este getType toma en cuenta si es SELF_TYPE
        IType GetType(string typeName, IType ActualClass);
        bool IfDefineType(string typeName);
        bool IfDefineType(IType type);
        bool IfSELF_TYPE(string type);
    }
    //T representa el tipo de variables que se van a oabordar en el Contexto y
    //P el tipo que se va abordar en el contexto hijo
    //Esto lo hago asi ya que el contexto de una clase se aborda como tipo a IAttribute
    //crea como hijo un contexto que tiene como tipo  IVar (para englobar parametros y variables creadas como resultado de expresiones)
    public interface IObjectContext<out T,out P> : IContext where T : IVar where P : IVar
    {
       
        bool Define(string var, IType type);
        T GetVar(string var);

        bool IsDefineVar(string Name);
        //Creo un hijo donde el tipo del padre es el mismo tipo mio
        IObjectContext<P,P> CreateChildContext();
    
        

    }
    
    //El context de metodos
   public interface IMethodContext:IContext
    {
        //Define un metodo
        bool Define(string ID, IType ReturnType, IFormal[] Formals);
        //Devuelve un metodo que esta definido
        IMethod GetMethod(string ID);
        bool IsDefineMethod(string Name);
  
    }
    public interface IErrorLogger
    {
        IList<string> msgs { get; }
        void LogError(string msg);
    }
    //IType implementa a IIObjectContext donde el tipo de parent es IGlobalContext 
    public interface IType : IObjectContext<IAttribute, IVar>, IMethodContext
    {

        string Name { get; }
        IType Inherits { get; set; }
        ISet<IMethod> Methods { get; }
        ISet<IAttribute> Attributes { get; }
        bool IsBasicType { get; set; }
        bool Define(string var, IType type, ExprContext initializacion);
        new bool Define(string ID, IType ReturnType, IFormal[] Formals);
        bool IfRedefineMethod(IMethod method);
        bool IfDefineInTheCurrentType(string ID);
        bool Conform(IType a);
        IType Join(IType a);
        //Calcula cual es el tipo de esta clase dentro de una clase dependiende si es self_type o no (se utiliza e el checkeo semantico de los dispatch)
        IType RealType(IType type);
        //Calcula el tipo dependiendo si es self_type o no (se utiliza e el checkeo semantico de los dispatch)
        IType DispatchComputedType(IType type);
        IEnumerable<IType> Hierachty { get; }
    }
    //Todo identificador declarado con un tipo metodos , atributos, parametros, declaraciones (let,case)
    public interface ISymbol
    {
        string ID { get; }
        IType Type { get; }
    }

    //Todo identificador que pueda tratarse como una variable (se le puede asignar una expresion) atributo,formal, letvar,branch
    public interface IVar : ISymbol
    {

    }
    //Define los componentes de una clase
    //public interface IFeature:ISymbol
    //{

    //}
    //Define los parametros de un metodo
    public interface IFormal : IVar
    {

    }
    //Define los metodos
    public interface IMethod : ISymbol
    {
        bool IsRedefined { get; set; }
        IFormal[] Formals { get; }
    }
    //Define los atributos
    public interface IAttribute : IVar
    {
        ExprContext initializacion { get; set; }
    }

}
