using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmpProject.CIL;

using static COOLgrammarParser;
namespace CmpProject
{
    
    public class GenerateToCil:IVisitor<ProgramContext>,
    IVisitorCil<ClassContext>,
    IVisitorCil<FeatureContext>,
    IVisitorCilWhitContext<FormalContext,IFunctionCil>,
    IVisitorCilWhitContext<ExprContext, IFunctionCil,IHolderCil>,
    IVisitorCilWhitContext<SelfDispatchContext, IFunctionCil,IHolderCil>,
    IVisitorCilWhitContext<DispatchContext, IFunctionCil,IHolderCil>,
    IVisitorCilWhitContext<CondExprContext, IFunctionCil, IHolderCil>,
    IVisitorCilWhitContext<NewTypeExprContext, IFunctionCil, IHolderCil>,
    IVisitorCilWhitContext<AssignExprContext, IFunctionCil, IHolderCil>,
    IVisitorCilWhitContext<NotExprContext, IFunctionCil, IHolderCil>,
    IVisitorCilWhitContext<CompaExprContext, IFunctionCil, IHolderCil>,
    IVisitorCilWhitContext<ArithContext, IFunctionCil, IHolderCil>,
    IVisitorCilWhitContext<IsvoidExprContext, IFunctionCil, IHolderCil>,
    IVisitorCilWhitContext<NegExprContext, IFunctionCil, IHolderCil>,
    IVisitorCilWhitContext<InParenthesisExprContext, IFunctionCil, IHolderCil>,
    IVisitorCilWhitContext<IdExprContext, IFunctionCil, IHolderCil>,
    IVisitorCilWhitContext<IntegerExprContext, IFunctionCil, IHolderCil>,
    IVisitorCilWhitContext<StringExprContext, IFunctionCil, IHolderCil>,
    IVisitorCilWhitContext<BoolExprContext, IFunctionCil, IHolderCil>,
    IVisitorCilWhitContext<WhileExprContext, IFunctionCil, IHolderCil>,
    IVisitorCilWhitContext<BlockExprContext, IFunctionCil, IHolderCil>,
    IVisitorCilWhitContext<LetExprContext, IFunctionCil, IHolderCil>,
    IVisitorCilWhitContext<LetRuleContext, IFunctionCil, IHolderCil>,
    IVisitorCilWhitContext<LetDeclContext, IFunctionCil, IHolderCil>,
    IVisitorCilWhitContext<LetBodyContext, IFunctionCil, IHolderCil>,
    IVisitorCilWhitContext<DeclarationContext, IFunctionCil>,
    IVisitorCilWhitContext<CaseExprContext, IFunctionCil, IHolderCil>,
    IVisitorCtor
    {
        public ICilAst CilAst { get; set; }
        public IGlobalContext GlobalContext { get; set; }
        private ITypeCil typeCil { get; set; }
        internal IType typeC { get; set; }
        private IFunctionCil functionCil { get; set; }
        public BasicTypes basicTypes { get; set; }
        public GenerateToCil(CheckSemanticVisitor visitor, GenerateToCilTypes generateToCilTypes)
        {
            GlobalContext = visitor.globalContext;
            CilAst=generateToCilTypes.CilAst;
            basicTypes = generateToCilTypes.basicTypes;
        }
        public virtual void Visit(ProgramContext parserRule)
        {
            Visit(basicTypes.entry);
            foreach (var _class in parserRule._classes)
            {
                typeCil = CilAst.GetTypeCilByName(_class.type.Text);
                typeC = GlobalContext.GetType(_class.type.Text);
                Visit(_class);
            }
        }
        public void Visit(ClassContext parserRule)
        {
            Visit();
            foreach (var features in parserRule._features)
                Visit(features);
        }
        public void Visit(FeatureContext parserRule)
        {
            switch (parserRule)
            {
                case MethodContext method:
                    Visit(method);
                    break;
                default:
                    break;
            }
        }   
        public virtual IFunctionCil Visit(MethodContext parserRule)
        {
            IFunctionCil function;
            var contextCil = new ContextCil();
            //El unico metodo que no pertenece a una clase es entry
            if (parserRule.Parent == null)
            {
                function= CilAst.GetFunctionCilsByName($"{parserRule.idText}");
            }
            else
            {
                // El nombre metodo en el tipo tiene siempre esta estructura (Type_CoolName)
                function=CilAst.GetFunctionCilsByName($"{(parserRule.Parent as ClassContext).type.Text}${parserRule.idText}");
                // Como toda fucion pertenece a una clase se le agrega self como una parametro
                var self = new ArgCil("self");
                function.ArgCils.Add(self);
                contextCil.Define("self");
                foreach (var formal in parserRule._formals)
                    Visit(formal, function,contextCil);
            }
            var result= Visit(parserRule.exprBody, function,contextCil);
            function.ThreeDirInses.Add(new ReturnCil(result));
            return function;
        }
        public void Visit(FormalContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            contextCil.Define(parserRule.idText);
            var arg = new ArgCil(contextCil.variables[parserRule.idText].Name);
            cilTree.ArgCils.Add(arg);
        }
        public IHolderCil Visit(ExprContext parserRule, IFunctionCil cilTree,IContextCil contextCil)
        {
            switch (parserRule)
            {
                case SelfDispatchContext rule:
                    return Visit(rule, cilTree,contextCil);             
                case DispatchContext rule:
                    return Visit(rule, cilTree, contextCil);
                case CondExprContext rule:
                    return Visit(rule, cilTree, contextCil);
                case WhileExprContext rule:
                    return Visit(rule, cilTree, contextCil);
                case BlockExprContext rule:
                    return Visit(rule, cilTree, contextCil);
                case LetExprContext rule:
                    return Visit(rule, cilTree, contextCil);
                case CaseExprContext rule:
                    return Visit(rule, cilTree, contextCil);
                case NewTypeExprContext rule:
                    return Visit(rule, cilTree, contextCil);
                case AssignExprContext rule:
                    return Visit(rule, cilTree, contextCil);
                case NotExprContext rule:
                    return Visit(rule, cilTree, contextCil);
                case CompaExprContext rule:
                    return Visit(rule, cilTree, contextCil);
                case ArithContext rule:
                    return Visit(rule, cilTree, contextCil);
                case IsvoidExprContext rule:
                    return Visit(rule, cilTree, contextCil);
                case NegExprContext rule:
                    return Visit(rule, cilTree, contextCil);
                case InParenthesisExprContext rule:
                    return Visit(rule, cilTree, contextCil);
                case IdExprContext rule:
                    return Visit(rule, cilTree, contextCil);
                case IntegerExprContext rule:
                    return Visit(rule, cilTree, contextCil);
                case StringExprContext rule:
                    return Visit(rule, cilTree, contextCil);
                case BoolExprContext rule:
                    return Visit(rule, cilTree, contextCil);
                default:
                    return Visit(cilTree);
            }
        }
        public virtual IHolderCil Visit(SelfDispatchContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            var Params = new List<IHolderCil>();
            foreach (var expr in parserRule._expresions)
            {
                //genera el codigo de cada parametro que le paso a los metodos
                var param = Visit(expr, cilTree,contextCil);
                Params.Add(param);
            }
            //Averiguo el tipo dinamico de self es decir( el de la clase que esta usando la funcion en ese momento)
            var varType = new LocalCil($"_Type{cilTree.ThreeDirInses.Count}");
            cilTree.LocalCils.Add(varType);
            var self = cilTree.self;
            cilTree.ThreeDirInses.Add(new TypeOf(varType, self));
            //cada parametro los anado al metodo puede que tenga sentido pasarlos al revez
            cilTree.ThreeDirInses.Add(new ArgExprCil(self));
            foreach (var param in Params)
            {
                cilTree.ThreeDirInses.Add(new ArgExprCil(param));
            }            
            //ITypeCil typeCil;
            ////resuelve el metodo en cil de ese tipo estatico
            var functionCil = typeCil.GetFunctionCilsByCoolName(parserRule.id.Text);
            ////nueva variable donde se almacena el valor que retorna el metodo
            var value = new LocalCil($"_value{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(value);
            cilTree.ThreeDirInses.Add(new VCallCil(value, varType, functionCil));
            return value;
        }
        public virtual IHolderCil Visit(DispatchContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            var Params = new List<IHolderCil>();
            var expr0 = Visit(parserRule.expresion, cilTree,contextCil);
            foreach (var expr in parserRule._expresions)
            {
                //genera el codigo de cada parametro que le paso a los metodos
                var param = Visit(expr, cilTree,contextCil);
                Params.Add(param);
            }
            //Averiguo el tipo dinamico de self es decir( el de la clase que esta usando la funcion en ese momento)
            var varType = new LocalCil($"_Type{cilTree.ThreeDirInses.Count}");
            cilTree.LocalCils.Add(varType);
            cilTree.ThreeDirInses.Add(new TypeOf(varType, expr0));
            //Verifico si el tipo del objeto que le voy hacer el dispatch es void
            if (cilTree.Name!="entry")
            {
                var isVoid= new LocalCil($"_isVoid{cilTree.ThreeDirInses.Count}");
                cilTree.LocalCils.Add(isVoid);
                cilTree.ThreeDirInses.Add(new NotEqualCil(isVoid,varType, CilAst.GetTypeCilByName("void")));
                Visit_Runtime_Error_whit_Cond(isVoid, cilTree,$"\"line {parserRule.id.Line} column {parserRule.id.Column+1} A dispatch on void\"");
                cilTree.ThreeDirInses.Add(new ArgExprCil(expr0));
            }
            //cada parametro los anado al metodo puede que tenga sentido pasarlos al revez

            foreach (var param in Params)
                cilTree.ThreeDirInses.Add(new ArgExprCil(param));

            ////nueva variable donde se almacena el valor que retorna el metodo
            var value = new LocalCil($"_value{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(value);
            
            if (parserRule.type == null)
            {
                //Se resuleve el tipo de la expr0 a partir de su tipo estatico calculado por el checkeo semantico
                var typeExpr0 = CilAst.GetTypeCilByName(parserRule.expresion.computedType.Name,typeCil);
                ////resuelve el metodo en cil de ese tipo estatico
                var functionCil = typeExpr0.GetFunctionCilsByCoolName(parserRule.id.Text);
                //como ningun tipo puede redefinir a string entonces llamo directamente al metodo
                if (parserRule.expresion.computedType == GlobalContext.String)
                    cilTree.ThreeDirInses.Add(new CallCil(value, functionCil.Function));
                else
                    cilTree.ThreeDirInses.Add(new VCallCil(value, varType, functionCil));
            }
            else
            {
                //Se resuelve el tipo de la type
                var typeT = CilAst.GetTypeCilByName(parserRule.type.Text, typeCil);
                var functionCil = typeT.GetFunctionCilsByCoolName(parserRule.id.Text).Function;
                cilTree.ThreeDirInses.Add(new CallCil(value, functionCil));
            }
            return value;
        }
        public IHolderCil Visit(CondExprContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            var value = new LocalCil($"_value{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(value);
            var condValue = Visit(parserRule.ifExpr,cilTree,contextCil);
            condValue = GetValue(condValue, cilTree, CilAst.Bool);
            var labelElse = cilTree.CreateLabel("else");
            cilTree.ThreeDirInses.Add(new IfGoto(condValue, labelElse));
            //genero el codigo de elseValue
            var elseValue = Visit(parserRule.elseExpr, cilTree,contextCil);
            var labelEnd=cilTree.CreateLabel("end");
            //El resultado lo almaceno en value
            cilTree.ThreeDirInses.Add(new AssigCil(value, elseValue));
            //Voy pa la etiquta end
            cilTree.ThreeDirInses.Add(new GotoCil(labelEnd));
            //Pongo la etiqueta de else
            cilTree.ThreeDirInses.Add(new Label(labelElse));
            //genero el codigo de thenValue
            var thenValue = Visit(parserRule.thenExpr, cilTree,contextCil);
            //Asigno el valor a esle value
            cilTree.ThreeDirInses.Add(new AssigCil(value, thenValue));
            //Pongo la etiqueta end
            cilTree.ThreeDirInses.Add(new Label(labelEnd));
            //retorno el valor
            return value;
        }
        public IHolderCil Visit(NewTypeExprContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            var value = new LocalCil($"_value{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(value);
            if (parserRule.type.Text=="SELF_TYPE")
            {
                var varType = new LocalCil($"_Type{cilTree.ThreeDirInses.Count}");
                cilTree.LocalCils.Add(varType);
                cilTree.ThreeDirInses.Add(new TypeOf(varType, new ValuelCil("self")));
                cilTree.ThreeDirInses.Add(new VCallCil(value,varType,new ValuelCil("Init")));
            }
            else
            {
                var varType= CilAst.GetTypeCilByName(parserRule.type.Text, typeCil);
                cilTree.ThreeDirInses.Add(new CallCil(value,varType.Init.Function));
            }
            return value;
        }
        public IHolderCil Visit(AssignExprContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            var valueExpr = Visit(parserRule.expresion, cilTree, contextCil);
            if (!contextCil.variables.ContainsKey(parserRule.id.Text))
            {
                var value = new LocalCil($"_value{cilTree.LocalCils.Count}");
                cilTree.LocalCils.Add(value);
                cilTree.ThreeDirInses.Add(new SetAttrCil(cilTree.self, typeCil.GetAttributeCilsByCoolName(parserRule.id.Text), valueExpr));
                return value;
            }
            else
            {
                var value = contextCil.variables[parserRule.id.Text];
                cilTree.ThreeDirInses.Add(new AssigCil(value, valueExpr));
                return value;
            }
        }
        public IHolderCil Visit(CompaExprContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            var value = new LocalCil($"_value{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(value);
            var valueLeft= Visit(parserRule.left,cilTree,contextCil);
            var valueRight= Visit(parserRule.right, cilTree,contextCil);
            if (parserRule.left.computedType==GlobalContext.Int)
            {
                valueLeft = GetValue(valueLeft, cilTree, CilAst.Int);
                valueRight = GetValue(valueRight, cilTree, CilAst.Int);
            }
            else if(parserRule.left.computedType == GlobalContext.Bool)
            {
                valueLeft = GetValue(valueLeft, cilTree, CilAst.Bool);
                valueRight = GetValue(valueRight, cilTree, CilAst.Bool);
            }
            else if ( parserRule.left.computedType == GlobalContext.String)
            {
                valueLeft = GetValue(valueLeft, cilTree, CilAst.String);
                valueRight = GetValue(valueRight, cilTree, CilAst.String);
            }
            switch (parserRule.op.Text)
            {
                case "<":
                    cilTree.ThreeDirInses.Add(new MinorCil(value,valueLeft,valueRight) );
                    break;
                case "<=":
                    cilTree.ThreeDirInses.Add(new Minor_EqualCil(value, valueLeft, valueRight));
                    break;
                case "=":
                    if (parserRule.left.computedType == GlobalContext.String)
                        cilTree.ThreeDirInses.Add(new EqualStringCil(value, valueLeft, valueRight));
                    else
                        cilTree.ThreeDirInses.Add(new EqualCil(value, valueLeft, valueRight));
                    break;
                default:
                    break;
            }
            return CreateABasicTypeWhitVal(cilTree, CilAst.Bool,value);
        }
        public IHolderCil Visit(NotExprContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            var _valueNum = new LocalCil($"_valueNum{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(_valueNum);
            var valueExpr = Visit(parserRule.expresion, cilTree, contextCil);
            var BoolCil= CilAst.Bool;
            valueExpr = GetValue(valueExpr, cilTree, BoolCil);
            cilTree.ThreeDirInses.Add(new RestCil(_valueNum, new HolderCil("1"), valueExpr));
            return CreateABasicTypeWhitVal(cilTree,BoolCil,_valueNum);
        }
        public IHolderCil Visit(ArithContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {

            var valueNum = new LocalCil($"_valueNum{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(valueNum);
            var valueLeft = Visit(parserRule.left, cilTree,contextCil);
            var valueRight = Visit(parserRule.right, cilTree,contextCil);
            var valLeft = GetValue(valueLeft, cilTree, CilAst.Int);
            var valRigth= GetValue(valueRight, cilTree, CilAst.Int);
            switch (parserRule.op.Text)
            {
                case "/":
                    var isZero = new LocalCil($"_isZero{cilTree.LocalCils.Count}");
                    cilTree.LocalCils.Add(isZero);
                    cilTree.ThreeDirInses.Add(new NotEqualCil(isZero,valRigth,new HolderCil("0")));
                    Visit_Runtime_Error_whit_Cond(isZero,cilTree, $"\"line {parserRule.Start.Line} column {parserRule.Start.Column+1} Division by zero\"");
                    cilTree.ThreeDirInses.Add(new DivCil(valueNum, valLeft, valRigth));
                    break;
                case "*":
                    cilTree.ThreeDirInses.Add(new MultCil(valueNum, valLeft, valRigth));
                    break;
                case "+":
                    cilTree.ThreeDirInses.Add(new SumCil(valueNum, valLeft, valRigth));
                    break;
                case "-":
                    cilTree.ThreeDirInses.Add(new RestCil(valueNum, valLeft, valRigth));
                    break;
                default:
                    break;
            }
            
            return CreateABasicTypeWhitVal(cilTree,CilAst.Int,valueNum);
        }
        public IHolderCil Visit(IsvoidExprContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            var value = new LocalCil($"_value{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(value);
            var valueExpr = Visit(parserRule.expresion, cilTree, contextCil);
            var TypeValue= new LocalCil($"_TypeValue{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(TypeValue);
            cilTree.ThreeDirInses.Add(new TypeOf(TypeValue, valueExpr));
            cilTree.ThreeDirInses.Add(new EqualCil(value,TypeValue,CilAst.GetTypeCilByName("void")));
            return CreateABasicTypeWhitVal(cilTree,CilAst.Bool,value);
        } 
        public IHolderCil Visit(NegExprContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            
            var value = new LocalCil($"_value{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(value);
            var valueExpr = Visit(parserRule.expresion, cilTree,contextCil);
            valueExpr = GetValue(valueExpr, cilTree, CilAst.Int);
            cilTree.ThreeDirInses.Add(new NegCil(value,valueExpr));
            return CreateABasicTypeWhitVal(cilTree, CilAst.Int, value);
        }
        public IHolderCil Visit(InParenthesisExprContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            return Visit(parserRule.expresion, cilTree,contextCil);
        }
        public IHolderCil Visit(IdExprContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            //Si no es una variable declarada dentro del metodo entonces es un atributo de la clase (self)
          
            if (!contextCil.variables.ContainsKey(parserRule.id.Text))
            {
                var value = new LocalCil($"_value{cilTree.LocalCils.Count}");
                cilTree.LocalCils.Add(value);
                cilTree.ThreeDirInses.Add(new GetAttrCil(value,cilTree.self,typeCil.GetAttributeCilsByCoolName(parserRule.id.Text)));
                return value;
            }
            
            return contextCil.variables[parserRule.id.Text];
        }
        public IHolderCil Visit(IntegerExprContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            return CreateABasicTypeWhitVal(cilTree, CilAst.Int, new HolderCil(parserRule.integer.Text));
        }
        public IHolderCil Visit(StringExprContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            var value = new LocalCil($"_value{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(value);
            var stringCool = parserRule.STRING().GetText();
            var varDataString = new VarCil($"s{CilAst.dataStringCils.Count}");
            CilAst.dataStringCils.Add(new DataStringCil(varDataString, new StringCil(stringCool)));
            cilTree.ThreeDirInses.Add(new LoadCil(value, varDataString));
            return CreateABasicTypeWhitVal(cilTree, CilAst.String,value);
        }
        public IHolderCil Visit(BoolExprContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            var value = CreateABasicType(cilTree, CilAst.Bool);
            switch (parserRule.@bool.Text)
            {
                case "true":
                    SetValue(value , new HolderCil("1"), cilTree, CilAst.Bool);
                    break;
                case "false":
                    SetValue(value , new HolderCil("0"), cilTree, CilAst.Bool);
                    break;
                default:
                    break;
            }
            return value;
        }
        public IHolderCil Visit(WhileExprContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            
            var whileElse = cilTree.CreateLabel("while");
            //Voy para esa etiqueta para evaluar la cod del while
            cilTree.ThreeDirInses.Add(new GotoCil(whileElse));
            var loop =cilTree.CreateLabel("loop");
            //Esta etiqueta indica evalua el cuerpo de while
            cilTree.ThreeDirInses.Add(new Label(loop));
            Visit(parserRule.loopExpr,cilTree,contextCil);
            //Pongo la etiqueta de while
            cilTree.ThreeDirInses.Add(new Label(whileElse));
            var condValue = Visit(parserRule.whileExpr, cilTree,contextCil);
            condValue= GetValue(condValue, cilTree, CilAst.Bool);
            cilTree.ThreeDirInses.Add(new IfGoto(condValue, loop));
            //retorno el valor
            var value = Visit_void(cilTree);
            return value;
        }
        public IHolderCil Visit(BlockExprContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            foreach (var expr in parserRule._expresions)
            {
                Visit(expr, cilTree,contextCil);
            }
            return Visit(parserRule.finalExpresion, cilTree,contextCil);
        }
        public IHolderCil Visit(LetExprContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
           return Visit(parserRule.let, cilTree,contextCil);
        }
        public IHolderCil Visit(LetRuleContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            switch (parserRule)
            {
                case LetDeclContext rule:
                    return Visit(rule, cilTree,contextCil);
                case LetBodyContext rule:
                    return Visit(rule, cilTree,contextCil);
                default:
                    return null;
            }
        }
        public IHolderCil Visit(LetDeclContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            var newContextCil = contextCil.CreateAChild();
            Visit(parserRule.declaretion, cilTree,newContextCil);
            return Visit(parserRule.let, cilTree,newContextCil);
        }
        public IHolderCil Visit(LetBodyContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            var newContextCil = contextCil.CreateAChild();
            Visit(parserRule.declaretion, cilTree, newContextCil);
            return Visit(parserRule.body, cilTree, newContextCil);
        }
        public void Visit(DeclarationContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            contextCil.Define(parserRule.idText);
            var Id = new LocalCil(contextCil.variables[parserRule.idText].Name);
            cilTree.LocalCils.Add(Id);
            IHolderCil value;

            if (parserRule.expression != null)
            {
                value = Visit(parserRule.expression, cilTree, contextCil);

            }
            else if (parserRule.type.Text == "Int")
            {
                value = CreateABasicType(cilTree, CilAst.Int);
            }
            else if (parserRule.type.Text == "String")
            {
                value = CreateABasicType(cilTree, CilAst.String);
            }
            else if (parserRule.type.Text == "Bool")
            {
                value = CreateABasicType(cilTree, CilAst.Bool);
            }
            else
            {
                value = Visit_void(cilTree);
            }
            cilTree.ThreeDirInses.Add(new AssigCil(Id,value));
        }
        public IHolderCil Visit(CaseExprContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {

            var value = new LocalCil($"_value{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(value);
            var expr0 = Visit(parserRule.expresion, cilTree,contextCil);
            //is void
            var TypeValue = new LocalCil($"_TypeValue{cilTree.LocalCils.Count}");
            var not_is_void = new LocalCil($"not_is_void{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(TypeValue);
            cilTree.LocalCils.Add(not_is_void);
            cilTree.ThreeDirInses.Add(new TypeOf(TypeValue, expr0));
            cilTree.ThreeDirInses.Add(new NotEqualCil(not_is_void, TypeValue, CilAst.GetTypeCilByName("void")));
         
            //lanzamos el error

            Visit_Runtime_Error_whit_Cond(not_is_void,cilTree, $"\"linea {parserRule.Start.Line} y columna {parserRule.Start.Column + 1} A case on void\"");
            
            //ejecucion del case
            var closestAncestor = new LocalCil($"_closestAncestor{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(closestAncestor);
         
            //Inicializo el valor de numberType en 0 y closestAncestor con object 
            var isNotConform= new LocalCil($"_isNotConform{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(isNotConform);

            var branches = parserRule._branches.Concat(new List<BranchContext>() { parserRule.firstBranch}).OrderBy(t => -(CilAst.GetTypeCilByName(t.typeText).IndexOfPrecedence)).ToArray();
            //El tipo de la primera rama
            var End=cilTree.CreateLabel("End_");
            for (int i = 0; i < branches.Length; i++)
            {
                var branch = branches[i];
                var nextLabel = cilTree.CreateLabel("Case_");
                //tipo de la rama
                var typeBranch = CilAst.GetTypeCilByName(branch.typeText, typeCil);
                cilTree.ThreeDirInses.Add(new IsNotConformCil(isNotConform, TypeValue, typeBranch));
                cilTree.ThreeDirInses.Add(new IfGoto(isNotConform, nextLabel));
                var valueBranch = new LocalCil(branch.idText);//preguntarle as zahuis
                cilTree.LocalCils.Add(valueBranch);
                cilTree.ThreeDirInses.Add(new AssigCil(valueBranch, expr0));
                var newContextCil = contextCil.CreateAChild();
                newContextCil.Define(branch.idText);
                var valueExpr = Visit(branch.expression, cilTree, newContextCil);
                cilTree.ThreeDirInses.Add(new AssigCil(value, valueExpr));
                cilTree.ThreeDirInses.Add(new GotoCil(End));
                cilTree.ThreeDirInses.Add(new Label(nextLabel));
            }
            Visit_Runtime_Error(cilTree, $"\"linea {parserRule.Start.Line} y columna {parserRule.Start.Column + 1} Execution of a case statement without a matching branch\"");
            cilTree.ThreeDirInses.Add(new Label(End));
            

            return value;
        }
        public void Visit()
        {
            IFunctionCil init = typeCil.Init.Function;
            var value = new LocalCil("self");
            init.LocalCils.Add(value);
            var typeCilNew = CilAst.GetTypeCilByName(typeCil.Name, typeCil);
            init.ThreeDirInses.Add(new Allocate(value, typeCilNew));
            var typeCool = GlobalContext.GetType(typeCil.Name);
            var contextCil = new ContextCil();
            contextCil.Define("self");
            foreach (var typeTemp in typeCool.Hierachty)
            {
                foreach (var attributeTemp in typeTemp.Attributes)
                {
                    //Inicializamos los atributos
                    if (attributeTemp.initializacion != null)
                    {
                        var valueAttribute = Visit(attributeTemp.initializacion, init, contextCil);
                        //No siempre los tipos de Cil estan para eso eso habria que hacer 2 pasadas al AST
                        init.ThreeDirInses.Add(new SetAttrCil(value, typeCilNew.GetAttributeCilsByCoolName(attributeTemp.ID), valueAttribute));
                    }
                    else
                    {
                        if (typeTemp == GlobalContext.Int || typeTemp == GlobalContext.Bool)
                        {
                            init.ThreeDirInses.Add(new SetAttrCil(value, typeCilNew.GetAttributeCilsByCoolName(attributeTemp.ID), new ValuelCil("0")));
                        }
                        else if (typeTemp == GlobalContext.String)
                        {
                            var valueS = new LocalCil($"_value{init.LocalCils.Count}");
                            init.LocalCils.Add(valueS);

                            var stringCool = "";
                            var varDataString = new VarCil($"s{CilAst.dataStringCils.Count}");
                            CilAst.dataStringCils.Add(new DataStringCil(varDataString, new StringCil(stringCool)));
                            init.ThreeDirInses.Add(new LoadCil(valueS, varDataString));
                            init.ThreeDirInses.Add(new SetAttrCil(value, typeCilNew.GetAttributeCilsByCoolName(attributeTemp.ID), valueS));
                        }
                        else if (attributeTemp.Type == GlobalContext.String)
                        {
                            init.ThreeDirInses.Add(new SetAttrCil(value, typeCilNew.GetAttributeCilsByCoolName(attributeTemp.ID), CreateABasicType(init, CilAst.String)));
                        }
                        else if (attributeTemp.Type == GlobalContext.Int)
                        {
                            init.ThreeDirInses.Add(new SetAttrCil(value, typeCilNew.GetAttributeCilsByCoolName(attributeTemp.ID), CreateABasicType(init,CilAst.Int)));
                        }
                        else if (attributeTemp.Type == GlobalContext.Bool)
                        {
                            init.ThreeDirInses.Add(new SetAttrCil(value, typeCilNew.GetAttributeCilsByCoolName(attributeTemp.ID), CreateABasicType(init, CilAst.Bool)));
                        }
                        else
                        {
                            init.ThreeDirInses.Add(new SetAttrCil(value, typeCilNew.GetAttributeCilsByCoolName(attributeTemp.ID), Visit_void(init)));
                        }
                    }
                }
            }
            init.ThreeDirInses.Add(new ReturnCil(value));
        }
        public IHolderCil Visit(IFunctionCil cilTree)
        {
            switch (cilTree.Name)
            {
                case "String$lenght":
                    var value= new LocalCil("value");
                    cilTree.LocalCils.Add(value);
                    cilTree.ThreeDirInses.Add(new LenghtCil(value, GetValue(cilTree.self,cilTree, CilAst.GetTypeCilByName("String"))));
                    return CreateABasicTypeWhitVal(cilTree, CilAst.GetTypeCilByName("Int"),value);
                case "String$concat":
                    value = new LocalCil("value");
                    cilTree.LocalCils.Add(value);
                    cilTree.ThreeDirInses.Add(new ConcatCil(value, GetValue(cilTree.self, cilTree, CilAst.GetTypeCilByName("String")), GetValue(cilTree.ArgCils.SingleOrDefault(t => t.Name != "self"), cilTree, CilAst.GetTypeCilByName("String"))));
                    return CreateABasicTypeWhitVal(cilTree, CilAst.GetTypeCilByName("String"), value);
                case "String$substr":
                    var Length = new LocalCil($"_length{cilTree.LocalCils.Count}");
                    cilTree.LocalCils.Add(Length);
                    value = new LocalCil("value");
                    cilTree.LocalCils.Add(value);
                    var isParam1NotInRange = new LocalCil($"_isParam1InRange{cilTree.LocalCils.Count}");
                    cilTree.LocalCils.Add(isParam1NotInRange);
                    var self = GetValue(cilTree.self, cilTree, CilAst.GetTypeCilByName("String"));
                    cilTree.ThreeDirInses.Add(new LenghtCil(Length,self ));
                    //tomamos los valores de los argumentos
                    var param1 = GetValue(cilTree.ArgCils.ElementAt(1), cilTree, CilAst.Int);
                    var param2 = GetValue(cilTree.ArgCils.ElementAt(2), cilTree, CilAst.Int);
                    //
                    cilTree.ThreeDirInses.Add(new MinorCil(isParam1NotInRange,param1, Length));
                    Visit_Runtime_Error_whit_Cond(isParam1NotInRange, cilTree, $"\"Substring out of range\"");
                    var lastIndex = new LocalCil($"_lastIndex{cilTree.LocalCils.Count}");
                    cilTree.LocalCils.Add(lastIndex);
                    cilTree.ThreeDirInses.Add(new SumCil(lastIndex, param1, param2));
                    var isParam2NotInRange = new LocalCil($"_isParam2InRange{cilTree.LocalCils.Count}");
                    cilTree.LocalCils.Add(isParam2NotInRange);
                    cilTree.ThreeDirInses.Add(new Minor_EqualCil(isParam2NotInRange, lastIndex, Length));
                    Visit_Runtime_Error_whit_Cond(isParam2NotInRange, cilTree, $"\"Substring out of range\"");
                    cilTree.ThreeDirInses.Add(new SubStringCil(value, self, param1,param2));
                    return CreateABasicTypeWhitVal(cilTree, CilAst.String,value);
                case "Object$abort":
                    cilTree.ThreeDirInses.Add(new Halt());
                    return null;
                case "Object$type_name":
                    var x_type_name = new LocalCil("x");
                    cilTree.LocalCils.Add(x_type_name);
                    cilTree.ThreeDirInses.Add(new Type_Name(x_type_name,cilTree.self));
                    return CreateABasicTypeWhitVal(cilTree, CilAst.GetTypeCilByName("String"),x_type_name);
                case "Object$copy":
                    var x_Object_copy = new LocalCil("x");
                    cilTree.LocalCils.Add(x_Object_copy);
                    cilTree.ThreeDirInses.Add(new Copy(x_Object_copy, cilTree.self));
                    return x_Object_copy;
                case "IO$out_string":
                    cilTree.ThreeDirInses.Add(new Out_strCil(GetValue(cilTree.ArgCils.SingleOrDefault(t => t.Name != "self"), cilTree, CilAst.GetTypeCilByName("String")))) ;
                    return cilTree.self;
                case "IO$out_int":
                    cilTree.ThreeDirInses.Add(new Out_intCil(GetValue(cilTree.ArgCils.SingleOrDefault(t => t.Name != "self"),cilTree, CilAst.GetTypeCilByName("Int"))));
                    return cilTree.self;
                case "IO$in_string":
                    var x_in_string = new LocalCil("x");
                    cilTree.LocalCils.Add(x_in_string);
                    cilTree.ThreeDirInses.Add(new In_strCil(x_in_string));
                    return CreateABasicTypeWhitVal(cilTree, CilAst.GetTypeCilByName("String"), x_in_string);
                case "IO$in_int":
                    var x_in_int  = new LocalCil("x");
                    cilTree.LocalCils.Add(x_in_int);
                    cilTree.ThreeDirInses.Add(new In_intCil(x_in_int));
                    return CreateABasicTypeWhitVal(cilTree, CilAst.GetTypeCilByName("Int"), x_in_int);
                default:
                    return null;
            }
        }

        public IHolderCil Visit_void(IFunctionCil cilTree)
        {
            var valueV = new LocalCil($"_value{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(valueV);
            cilTree.ThreeDirInses.Add(new CallCil(valueV, CilAst.void_init));
            return valueV;
        }
        public void Visit_Runtime_Error_whit_Cond(IHolderCil valueCond, IFunctionCil cilTree,string sms)
        {
            var Continue = cilTree.CreateLabel($"Continue_");
            cilTree.ThreeDirInses.Add(new IfGoto(valueCond, Continue));
            var varStr = new LocalCil($"_value{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(varStr);
            var varDataString = new VarCil($"s{CilAst.dataStringCils.Count}");
            CilAst.dataStringCils.Add(new DataStringCil(varDataString, new StringCil(sms)));
            cilTree.ThreeDirInses.Add(new LoadCil(varStr, varDataString));
            cilTree.ThreeDirInses.Add(new Out_strCil(varStr));
            cilTree.ThreeDirInses.Add(new Halt());
            cilTree.ThreeDirInses.Add(new Label(Continue));
        }
        public void Visit_Runtime_Error(IFunctionCil cilTree, string sms)
        {
            var varStr = new LocalCil($"_value{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(varStr);
            var varDataString = new VarCil($"s{CilAst.dataStringCils.Count}");
            CilAst.dataStringCils.Add(new DataStringCil(varDataString, new StringCil(sms)));
            cilTree.ThreeDirInses.Add(new LoadCil(varStr, varDataString));
            cilTree.ThreeDirInses.Add(new Out_strCil(varStr));
            cilTree.ThreeDirInses.Add(new Halt());
        }
        public IVarCil GetValue(IHolderCil obj, IFunctionCil cilTree,ITypeCil typeCil)
        {
            var value = new LocalCil($"_value{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(value);
            cilTree.ThreeDirInses.Add(new GetAttrCil(value, obj, typeCil.GetAttributeCilsByCoolName("x")));
            return value;
        }
        public IVarCil SetValue(IVarCil obj,IHolderCil value, IFunctionCil cilTree, ITypeCil typeCil)
        {   
            cilTree.ThreeDirInses.Add(new SetAttrCil(obj,typeCil.GetAttributeCilsByCoolName("x"),value));
            return obj;
        }
        public IVarCil CreateABasicType(IFunctionCil cilTree, ITypeCil typeCil)
        {
            var value = new LocalCil($"_value{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(value);
            cilTree.ThreeDirInses.Add(new Allocate(value, typeCil));
            return value;
        }
        public IVarCil CreateABasicTypeWhitVal( IFunctionCil cilTree, ITypeCil typeCil, IHolderCil value)
        {
            return SetValue(CreateABasicType(cilTree, typeCil), value, cilTree, typeCil);
        }
    }
}
