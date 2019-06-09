using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmpProject.CIL;
using static COOLgrammarParser;
namespace CmpProject
{
    
    class GenerateToCil:IVisitor<ProgramContext>,
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
    IVisitorString<DispatchContext, IFunctionCil, IHolderCil>,
    IVisitorCtor
    {
        public ICilAst CilAst { get; set; }
        public IGlobalContext GlobalContext { get; set; }
        private ITypeCil typeCil { get; set; }
        internal IType typeC { get; set; }
        private IFunctionCil functionCil { get; set; }
        public BasicTypes basicTypes { get; set; }
        public GenerateToCil(CheckSemanticVisitor visitor, GenerateToCilFeatures generateToCilTypes)
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
            {
                Visit(features);
            }
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
                function=CilAst.GetFunctionCilsByName($"{(parserRule.Parent as ClassContext).type.Text}_{parserRule.idText}");
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

            if (parserRule.expresion.computedType==GlobalContext.String)
            {
               return  VisitString(parserRule, cilTree, contextCil);
            }
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
            
            if (parserRule.type==null)
            {
                cilTree.LocalCils.Add(varType);
                cilTree.ThreeDirInses.Add(new TypeOf(varType, expr0));
            }
            cilTree.ThreeDirInses.Add(new ArgExprCil(expr0));
            //cada parametro los anado al metodo puede que tenga sentido pasarlos al revez
            foreach (var param in Params)
            {
                cilTree.ThreeDirInses.Add(new ArgExprCil(param));
            }
            ////nueva variable donde se almacena el valor que retorna el metodo
            var value = new LocalCil($"_value{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(value);
            
            if (parserRule.type == null)
            {
                //Se resuleve el tipo de la expr0 a partir de su tipo estatico calculado por el checkeo semantico
                var typeExpr0 = CilAst.GetTypeCilByName(parserRule.expresion.computedType.Name,typeCil);
                ////resuelve el metodo en cil de ese tipo estatico
                var functionCil = typeExpr0.GetFunctionCilsByCoolName(parserRule.id.Text);
                cilTree.ThreeDirInses.Add(new VCallCil(value, varType, functionCil));
            }
            else
            {
                //Se resuelve el tipo de la type
                var typeT = CilAst.GetTypeCilByName(parserRule.type.Text, typeCil);
                var functionCil = typeT.GetFunctionCilsByCoolName(parserRule.id.Text);
                cilTree.ThreeDirInses.Add(new VCallCil(value, typeT, functionCil));
            }
            return value;
        }
        public IHolderCil Visit(CondExprContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            var value = new LocalCil($"_value{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(value);
            var condValue = Visit(parserRule.ifExpr,cilTree,contextCil);
            var labelElse = new LabelCil("else" + cilTree.ThreeDirInses.Count);
            cilTree.ThreeDirInses.Add(new IfGoto(condValue, labelElse));
            //genero el codigo de elseValue
            var elseValue = Visit(parserRule.elseExpr, cilTree,contextCil);

            var labelEnd= new LabelCil("end" + cilTree.ThreeDirInses.Count);
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
                cilTree.ThreeDirInses.Add(new SetAttrCil(cilTree.self, typeCil.GetAttributeCilsByCoolName(parserRule.id.Text),valueExpr));
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
            switch (parserRule.op.Text)
            {
                case "<":
                    cilTree.ThreeDirInses.Add(new MinorCil(value,valueLeft,valueRight) );
                    break;
                case "<=":
                    cilTree.ThreeDirInses.Add(new Minor_EqualCil(value, valueLeft, valueRight));
                    break;
                case "=":
                    cilTree.ThreeDirInses.Add(new EqualCil(value, valueLeft, valueRight));
                    break;
                default:
                    break;
            }
            return value;
        }
        public IHolderCil Visit(NotExprContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            var value = new LocalCil($"_value{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(value);
            var valueExpr = Visit(parserRule.expresion, cilTree, contextCil);
            cilTree.ThreeDirInses.Add(new RestCil(value,new HolderCil("1"), valueExpr));
            //var valueExpr = Visit(parserRule.expresion, cilTree,contextCil);
            //var labelElse = new LabelCil("else" + cilTree.ThreeDirInses.Count);
            //cilTree.ThreeDirInses.Add(new IfGoto(valueExpr, labelElse));
            ////Si es igual a 0 le asigno al resultado 1
            //cilTree.ThreeDirInses.Add(new AssigCil(value,new ValuelCil("1")));
            //var labelEnd = new LabelCil("end" + cilTree.ThreeDirInses.Count);
            ////Voy pa la etiqueta end
            //cilTree.ThreeDirInses.Add(new GotoCil(labelEnd));
            ////Pongo la etiqueta de else
            //cilTree.ThreeDirInses.Add(new Label(labelElse));
            ////Si es diferente a 0 le asigno al resultado 0
            //cilTree.ThreeDirInses.Add(new AssigCil(value, new ValuelCil("0")));
            ////Pongo la etiqueta end
            //cilTree.ThreeDirInses.Add(new Label(labelEnd));
            return value;
        }
        public IHolderCil Visit(ArithContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            var value = new LocalCil($"_value{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(value);
            var valueLeft = Visit(parserRule.left, cilTree,contextCil);
            var valueRight = Visit(parserRule.right, cilTree,contextCil);

            switch (parserRule.op.Text)
            {
                case "/":
                    cilTree.ThreeDirInses.Add(new DivCil(value, valueLeft, valueRight));
                    break;
                case "*":
                    cilTree.ThreeDirInses.Add(new MultCil(value, valueLeft, valueRight));
                    break;
                case "+":
                    cilTree.ThreeDirInses.Add(new SumCil(value, valueLeft, valueRight));
                    break;
                case "-":
                    cilTree.ThreeDirInses.Add(new RestCil(value, valueLeft, valueRight));
                    break;
                default:
                    break;
            }
            return value;
        }
        public IHolderCil Visit(IsvoidExprContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            throw new NotImplementedException();
        }
        public IHolderCil Visit(NegExprContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            var value = new LocalCil($"_value{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(value);
            var valueExpr = Visit(parserRule.expresion, cilTree,contextCil);
            cilTree.ThreeDirInses.Add(new NegCil(value,valueExpr));
            return value;
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
            return new ValuelCil(parserRule.integer.Text);
        }
        public IHolderCil Visit(StringExprContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            var value = new LocalCil($"_value{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(value);
            var stringCool = parserRule.STRING().GetText();
            var varDataString = new VarCil($"s{CilAst.dataStringCils.Count}");
            CilAst.dataStringCils.Add(new DataStringCil(varDataString, new StringCil(stringCool)));
            cilTree.ThreeDirInses.Add(new LoadCil(value, varDataString));
            return value;
        }
        public IHolderCil Visit(BoolExprContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            var value = new LocalCil($"_value{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(value);
            switch (parserRule.@bool.Text)
            {
                case "true":
                    cilTree.ThreeDirInses.Add(new AssigCil(value,new ValuelCil("1")));
                    break;
                case "false":
                    cilTree.ThreeDirInses.Add(new AssigCil(value, new ValuelCil("0")));
                    break;
                default:
                    break;
            }
            return value;
        }
        public IHolderCil Visit(WhileExprContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            var value = new LocalCil($"_value{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(value);
            var whileElse = new LabelCil("while" + cilTree.ThreeDirInses.Count);
            //Voy para esa etiqueta para evaluar la cod del while
            cilTree.ThreeDirInses.Add(new GotoCil(whileElse));
            var loop = new LabelCil("loop" + cilTree.ThreeDirInses.Count);
            //Esta etiqueta indica evalua el cuerpo de while
            cilTree.ThreeDirInses.Add(new Label(loop));
            Visit(parserRule.loopExpr,cilTree,contextCil);
            //Pongo la etiqueta de while
            cilTree.ThreeDirInses.Add(new Label(whileElse));
            var condValue = Visit(parserRule.whileExpr, cilTree,contextCil);
            cilTree.ThreeDirInses.Add(new IfGoto(condValue, loop));
            //retorno el valor
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
            var value = Visit(parserRule.expression, cilTree, contextCil);
            cilTree.ThreeDirInses.Add(new AssigCil(Id,value));
        }
        public IHolderCil Visit(CaseExprContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            var value = new LocalCil($"_value{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(value);
            var expr0 = Visit(parserRule.expresion, cilTree,contextCil);
            var numberType=new LocalCil($"_numberType{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(numberType);
            var closestAncestor = new LocalCil($"_closestAncestor{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(closestAncestor);
            var typeExpr0 = new LocalCil($"_typeExpr0{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(typeExpr0);
            //Guardo el valor del typo de la expr0 en typrExpr0
            cilTree.ThreeDirInses.Add(new TypeOf(typeExpr0,expr0));
            //Inicializo el valor de numberType en 0 y closestAncestor con object 
            cilTree.ThreeDirInses.Add(new AssigCil(numberType, new ValuelCil("-1")));
            cilTree.ThreeDirInses.Add(new AssigCil(closestAncestor,new ValuelCil("Object")));
            var isNotConform= new LocalCil($"_isNotConform{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(isNotConform);
            var nextLabel = new LabelCil($"Case_{cilTree.ThreeDirInses.Count + 5}");
            var typeBranch = CilAst.GetTypeCilByName(parserRule.firstBranch.typeText, typeCil);

            //El tipo de la primera rama
            cilTree.ThreeDirInses.Add(new IsNotConformCil(isNotConform,typeExpr0,typeBranch));
            cilTree.ThreeDirInses.Add(new IfGoto(isNotConform, nextLabel));
            cilTree.ThreeDirInses.Add(new IsNotConformCil(isNotConform, typeBranch, closestAncestor));
            cilTree.ThreeDirInses.Add(new IfGoto(isNotConform, nextLabel));
            cilTree.ThreeDirInses.Add(new AssigCil(numberType,new ValuelCil("0")));
            
            for (int i = 0; i < parserRule._branches.Count; i++)
            {
                var branch = parserRule._branches[i];
                var Label = new LabelCil($"Case_{cilTree.ThreeDirInses.Count}");
                nextLabel = new LabelCil($"Case_{cilTree.ThreeDirInses.Count + 6}");
                cilTree.ThreeDirInses.Add(new Label(Label));
                //tipo de la rama
                typeBranch = CilAst.GetTypeCilByName(branch.typeText, typeCil);
                cilTree.ThreeDirInses.Add(new IsNotConformCil(isNotConform, typeExpr0, typeBranch));
                cilTree.ThreeDirInses.Add(new IfGoto(isNotConform, nextLabel));
                cilTree.ThreeDirInses.Add(new IsNotConformCil(isNotConform, typeBranch,closestAncestor));
                cilTree.ThreeDirInses.Add(new IfGoto(isNotConform, nextLabel));
                cilTree.ThreeDirInses.Add(new AssigCil(numberType, new ValuelCil($"{i+1}")));
            }

            //Label del final de la expresion case
            var EndLabel = new LabelCil($"End_{cilTree.ThreeDirInses.Count}");

            //Creo la variable en un nuevo contexto
            var newContextCil = contextCil.CreateAChild();
            newContextCil.Define(parserRule.firstBranch.idText);
            var firstLocalBranch = new LocalCil(newContextCil.variables[parserRule.firstBranch.idText].Name);
            cilTree.LocalCils.Add(firstLocalBranch);

            var Label2 = new LabelCil($"Case_{cilTree.ThreeDirInses.Count}");
            cilTree.ThreeDirInses.Add(new Label(Label2));

            //Voy preguntando por cada valor que toma numbertype para ver que que expresion ejecutar

            var valueCond = new LocalCil($"_valueCond{cilTree.LocalCils.Count}");
            cilTree.ThreeDirInses.Add(new NotEqualCil(valueCond, numberType, new ValuelCil($"{0}")));
            var LabelType = new LabelCil($"branch{0}");
            cilTree.ThreeDirInses.Add(new IfGoto(valueCond, LabelType));
            cilTree.ThreeDirInses.Add(new AssigCil(firstLocalBranch,expr0));
            var valueExpr = Visit(parserRule.firstBranch.expression, cilTree,newContextCil);
            cilTree.ThreeDirInses.Add(new AssigCil(value, valueExpr));
            cilTree.ThreeDirInses.Add(new GotoCil(EndLabel));
            LabelType =new LabelCil($"branch{0}_{cilTree.ThreeDirInses.Count}");
            //Parche para cambiar el nombre del label del goto
            cilTree.ThreeDirInses=new HashSet< IThreeDirIns>( cilTree.ThreeDirInses.Select(c =>  (((c is IfGoto p) && (p.LabelCil.Name == $"branch{0}")) ? new IfGoto(valueCond, LabelType) : c)));
            cilTree.ThreeDirInses.Add(new Label(LabelType));
            for (int i = 0; i < parserRule._branches.Count; i++)
            {
                var branch = parserRule._branches[i];
                newContextCil = contextCil.CreateAChild();
                newContextCil.Define(branch.idText);
                var localBranch = new LocalCil(newContextCil.variables[branch.idText].Name);
                cilTree.LocalCils.Add(localBranch);

                valueCond= new LocalCil($"_valueCond{cilTree.LocalCils.Count}");
                cilTree.ThreeDirInses.Add(new NotEqualCil(valueCond,numberType,new ValuelCil($"{i+1}")));
                var LabelType1= new LabelCil($"branch{i+1}");
                cilTree.ThreeDirInses.Add(new IfGoto(valueCond,LabelType1));
                //asigno a idk la expr0
                cilTree.ThreeDirInses.Add(new AssigCil(localBranch, expr0));
                valueExpr= Visit(branch.expression, cilTree,newContextCil);
                cilTree.ThreeDirInses.Add(new AssigCil(value,valueExpr));
                cilTree.ThreeDirInses.Add(new GotoCil(EndLabel));
                LabelType1 = new LabelCil($"branch{i + 1}_{cilTree.ThreeDirInses.Count}");
                cilTree.ThreeDirInses = new HashSet<IThreeDirIns>(cilTree.ThreeDirInses.Select(c => (((c is IfGoto p) && (p.LabelCil.Name == $"branch{i+1}")) ? new IfGoto(valueCond, LabelType1) : c)));
                cilTree.ThreeDirInses.Add(new Label(LabelType1));
            }   
            cilTree.ThreeDirInses.Add(new Label(EndLabel));
            return value;
        }
        public IHolderCil VisitString(DispatchContext parserRule, IFunctionCil cilTree, IContextCil contextCil)
        {
            var Params = new List<IHolderCil>();
            var expr0 = Visit(parserRule.expresion, cilTree, contextCil);
            foreach (var expr in parserRule._expresions)
            {
                //genera el codigo de cada parametro que le paso a los metodos
                var param = Visit(expr, cilTree, contextCil);
                Params.Add(param);
            }
            ////nueva variable donde se almacena el valor que retorna el metodo
            var value = new LocalCil($"_value{cilTree.LocalCils.Count}");
            cilTree.LocalCils.Add(value);
            switch (parserRule.id.Text)
            {
                case "lenght":
                    cilTree.ThreeDirInses.Add(new LenghtCil(value, expr0));
                    break;
                case "concat":
                    cilTree.ThreeDirInses.Add(new ConcatCil(value, expr0,Params[0]));
                    break;
                case "substr":
                    cilTree.ThreeDirInses.Add(new SubStringCil(value, expr0, Params[0],Params[1]));
                    break;
                default:
                    break;
            }
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
                        if (attributeTemp.Type == GlobalContext.Int|| attributeTemp.Type == GlobalContext.Bool)
                        {
                            init.ThreeDirInses.Add(new SetAttrCil(value, typeCilNew.GetAttributeCilsByCoolName(attributeTemp.ID), new ValuelCil("0")));
                        }
                        else if (attributeTemp.Type == GlobalContext.String)
                        {
                            var valueS = new LocalCil($"_value{init.LocalCils.Count}");
                            init.LocalCils.Add(valueS);
                            var stringCool = "";
                            var varDataString = new VarCil($"s{CilAst.dataStringCils.Count}");
                            CilAst.dataStringCils.Add(new DataStringCil(varDataString, new StringCil(stringCool)));
                            init.ThreeDirInses.Add(new LoadCil(valueS, varDataString));
                            init.ThreeDirInses.Add(new SetAttrCil(value, typeCilNew.GetAttributeCilsByCoolName(attributeTemp.ID), valueS));
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
                case "Object_abort":
                    cilTree.ThreeDirInses.Add(new Halt());
                    return null;
                case "Object_type_name":
                    var x_type_name = new LocalCil("x");
                    cilTree.LocalCils.Add(x_type_name);
                    cilTree.ThreeDirInses.Add(new Type_Name(x_type_name,cilTree.self));
                    return x_type_name;
                case "Object_copy":
                    var x_Object_copy = new LocalCil("x");
                    cilTree.LocalCils.Add(x_Object_copy);
                    cilTree.ThreeDirInses.Add(new Copy(x_Object_copy, cilTree.self));
                    return x_Object_copy;
                case "IO_out_string":
                    cilTree.ThreeDirInses.Add(new Out_strCil(cilTree.ArgCils.SingleOrDefault(t => t.Name != "self")));
                    return cilTree.self;
                case "IO_out_int":
                    cilTree.ThreeDirInses.Add(new Out_intCil(cilTree.ArgCils.SingleOrDefault(t => t.Name != "self")));
                    return cilTree.self;
                case "IO_in_string":
                    var x_in_string = new LocalCil("x");
                    cilTree.LocalCils.Add(x_in_string);
                    cilTree.ThreeDirInses.Add(new In_strCil(x_in_string));
                    return x_in_string;
                case "IO_in_int":
                    var x_in_int  = new LocalCil("x");
                    cilTree.LocalCils.Add(x_in_int);
                    cilTree.ThreeDirInses.Add(new In_intCil(x_in_int));
                    return x_in_int;
                default:
                    return null;
            }
        }
    }
}
