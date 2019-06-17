using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmpProject;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using static COOLgrammarParser;
namespace CmpProject
{
    public class BasicTypes
    {
        public ClassContext Object { get; set; }
        public ClassContext IO { get; set; }

        public ClassContext Void { get; set; }
        public ClassContext Int { get; set; }
        public ClassContext Bool { get; set; }
        public ClassContext String { get; set; }
        public MethodContext entry { get; set; }
        public BasicTypes(ProgramContext program,IGlobalContext globalContext)
        {
            
            //Tipo Int
            Int = new ClassContext(program, 0);
            Int.type = new CommonToken(42, "Int");
            var value = new AttributeContext(new FeatureContext());
            value.idText = "x";
            value.decl = new DeclarationContext(program, 0);
            value.decl.idText = "x";
            value.decl.typeText = "Int";
            value.decl.type = new CommonToken(42, "Int");

            Int._features = new FeatureContext[1] { value};
            //var value = new AttributeContext(new FeatureContext());
            //value.= new CommonToken(42, "value");
            //Tipo Bool
            Bool = new ClassContext(program, 0);
            Bool.type = new CommonToken(42, "Bool");
            Bool._features = new FeatureContext[1] { value };
            //Tipo String
            String = new ClassContext(program, 0);
            String.type = new CommonToken(42, "String");
            var lenght = new MethodContext(new FeatureContext());
            lenght.methodName = new CommonToken(42, "lenght");
            lenght.TypeReturn = new CommonToken(42, "Int");
            lenght.idText = "lenght";
            lenght.typeText = "String";
            lenght._formals = new FormalContext[] { };
            lenght.Parent = String;
            var concat = new MethodContext(new FeatureContext());
            concat.methodName = new CommonToken(42, "concat");
            concat.TypeReturn = new CommonToken(42, "String");
            concat.idText = "concat";
            concat.typeText = "String";
            var str_x = new FormalContext(program, 0);
            str_x.idText = "x";
            str_x.typeText = "String";
            concat._formals = new FormalContext[1] {str_x };
            concat.Parent = String;
            var substr = new MethodContext(new FeatureContext());
            substr.methodName = new CommonToken(42, "substr");
            substr.TypeReturn = new CommonToken(42, "String");
            substr.idText = "substr";
            substr.typeText = "String";
            var int_i = new FormalContext(program, 0);
            int_i.idText = "i";
            int_i.typeText = "Int";
            var int_l = new FormalContext(program, 0);
            int_l.idText = "l";
            int_l.typeText = "Int";
            substr._formals = new FormalContext[2] { int_i,int_l};
            substr.Parent = String;
            String._features = new FeatureContext[4] { value,concat, lenght,substr };

            //Object
            Object = new ClassContext(program, 0);
            Object.type = new CommonToken(42, "Object");
            var abort = new MethodContext(new FeatureContext());
            abort.methodName= new CommonToken(42, "abort");
            abort.TypeReturn = new CommonToken(42, "Object");
            abort.idText = "abort";
            abort.typeText = "Object";
     
            abort._formals = new FormalContext[] { };
            abort.Parent = Object;
            var type_name= new MethodContext(new FeatureContext());
            type_name.methodName= new CommonToken(42, "type_name");
            type_name.TypeReturn = new CommonToken(42, "String");
            type_name.idText = "type_name";
            type_name.typeText = "String";
            type_name.Parent = Object;
            var copy= new MethodContext(new FeatureContext());
            copy.methodName = new CommonToken(42, "copy");
            copy.TypeReturn = new CommonToken(42, "SELF_TYPE");
            copy.idText = "copy";
            copy.typeText = "SELF_TYPE";
            copy.Parent = Object;
            Object._features = new FeatureContext[3] {abort,type_name,copy };
            //IO
            IO = new ClassContext(program,0);
            IO.type = new CommonToken(42, "IO");
            //out_string
            var out_string = new MethodContext(new FeatureContext());
            out_string.methodName = new CommonToken(42, "out_string");
            out_string.TypeReturn= new CommonToken(42, "SELF_TYPE");
            out_string.idText = "out_string";
            out_string.typeText = "SELF_TYPE";
            out_string.Parent = IO;
            var string_x = new FormalContext(program, 0);
            string_x.idText = "x";
            string_x.typeText = "String";
            out_string._formals = new FormalContext[1] {string_x };
            //out_int
            var out_int = new MethodContext(new FeatureContext());
            out_int.methodName = new CommonToken(42, "out_int");
            out_int.TypeReturn = new CommonToken(42, "SELF_TYPE");
            out_int.idText = "out_int";
            out_int.typeText = "SELF_TYPE";
            out_int.Parent = IO;
            var int_x = new FormalContext(program, 0);
            int_x.idText = "x";
            int_x.typeText = "Int";
            out_int._formals = new FormalContext[1] { int_x };
            //in_string
            var in_string = new MethodContext(new FeatureContext());
            in_string.methodName = new CommonToken(42, "in_string");
            in_string.TypeReturn = new CommonToken(42, "String");
            in_string.idText = "in_string";
            in_string.typeText = "String";
            in_string.Parent = IO;
            //in_int
            var in_int = new MethodContext(new FeatureContext());
            in_int.methodName = new CommonToken(42, "in_int");
            in_int.TypeReturn = new CommonToken(42, "Int");
            in_int.idText = "in_int";
            in_int.typeText = "Int";
            in_int.Parent = IO;
            IO._features = new FeatureContext[4] { out_string, out_int, in_string,in_int };
            entry = new MethodContext(new FeatureContext());
            entry.methodName = new CommonToken(42, "entry");
            entry.TypeReturn=new CommonToken(42, "Int");
            entry.idText = "entry";
            entry.typeText = "Int";
            var exprBody = new BlockExprContext(new ExprContext());
            //La primera expresion
            var exprBody1= new DispatchContext(new ExprContext());
            exprBody1.id = new CommonToken(42, "main");
            exprBody1._expresions = new List<ExprContext>();
            var expr=new NewTypeExprContext(new ExprContext());
            expr.type= new CommonToken(42, "Main");
            exprBody1.expresion = expr;
            entry.exprBody = exprBody1;
            //La segunda expresion
            var exprBody2 = new IntegerExprContext(new ExprContext());
            exprBody2.integer=new  CommonToken(41, "0");
            exprBody._expresions = new List<ExprContext>();
            exprBody._expresions.Add(exprBody1);
            exprBody.finalExpresion = exprBody2;
            entry.exprBody = exprBody;
            //El tipo void
            Void = new ClassContext(program, 0);
            Void.type = new CommonToken(42, "void");
            
        }
    }
}
