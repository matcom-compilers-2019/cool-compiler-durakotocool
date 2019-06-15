using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmpProject.CIL
{
    public interface ICil
    {
        
    }
    public interface ICilAst:ICil
    {
        ITypeCil Object { get; set; }
        ISet<ITypeCil> TypeCils { get; set; }

        IFunctionCil void_init { get; }
        ISet<IDataStringCil> dataStringCils { get; set; }
        ISet<IFunctionCil> FunctionCils { get; set; }
        ITypeCil GetTypeCilByName(string Name);
        ITypeCil GetTypeCilByName(string Name, ITypeCil typeCil);
        //devuelve la funcion por su nombre
        IFunctionCil GetFunctionCilsByName(string Name);
        IFunctionCil CreateFunctionCil(string Name, string CilName);
    }
    public interface IIde:ICil
    {
        string Name { get; set; }
    }
    //Representa a todos los entes que devuelven un tipo
    public interface IHolderCil:ICil
    {
        
        string Name { get; set; }
    }
    public interface IValuelCil:IHolderCil{}
    public interface IVarCil:IHolderCil{}
    public interface ILabelCil : IIde{}
    public interface IStringCil:IValuelCil{}
    public interface IDataStringCil 
    {
        IVarCil varCil { get; set; }
        IStringCil stringCil { get; set; }
    }
    public interface ITypeCil:IValuelCil
    {
        IFunctionTypeCil Init { get; set; }
        ISet<IAttributeCil> Attributes { get; set; }
        ISet<IFunctionTypeCil> Functions { get; set; }
        void AddAttr(IAttributeCil attributeCil);
        void AddFunc(IFunctionTypeCil functionCil);

        //devuelve la funcion por su nombre original en cool
        IFunctionTypeCil GetFunctionCilsByCoolName(string CoolName);
        IAttributeCil GetAttributeCilsByCoolName(string CoolName);
        int GetIndexAttributeCilsByCoolName(string CoolName);

    }
    public interface IFunctionCil:IFeuturesCil
    {
        ISet<IArgCil> ArgCils { get; set; }
        ISet<ILocalCil> LocalCils { get; set; }
        ISet<IThreeDirIns> ThreeDirInses { get; set; }
        Dictionary<string, int> localsDict { get; set; }
		Dictionary<string, int> argsDict { get; set; }
		IVarCil self { get; }
        ILabelCil CreateLabel(string label);
    }
    public interface IArgCil:IVarCil
    {

    }
    public interface ILocalCil:IVarCil
    {
        
    }
    public interface IThreeDirIns
    {
    }
    public interface IFeuturesCil:IVarCil
    {
        string CilName { get; set; }
    }
    public interface IAttributeCil:IFeuturesCil
    {
    }
    public interface IFunctionTypeCil:IFeuturesCil
    {
        IFunctionCil Function { get; set; }
    }
    public interface IContextCil
    {
        IDictionary<string, IVarCil> variables { get; set; }
        IContextCil CreateAChild();
        void Define(string var);

    }

    //interface IIde
    //{
    //    string Name { get; set; }
    //}

    //interface IVarCil:IIde
    //{

    //}
}
