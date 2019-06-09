using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Dfa;
using CmpProject.Optimization;
namespace CmpProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //var streamReader = new StreamReader("Testing/Tipos.cl");
            //var streamReader = new StreamReader("Testing/Dispatch.cl");
            var streamReader = new StreamReader("Testing/ZAHUIS.cl");
            //var streamReader = new StreamReader("Testing/Expresion Case.cool");
            //var streamReader = new StreamReader("Testing/Bool.cool");
            //var streamReader = new StreamReader("Testing/Nuevo tipo.cl");
            //var streamReader = new StreamReader("Testing/Comparaciones.cl");
            //var streamReader = new StreamReader("Testing/String.cl");
            //var streamReader = new StreamReader("Testing/Expresion let.cl");
            //var streamReader = new StreamReader("Testing/Factorial.cl");
            //var streamReader = new StreamReader("Testing/Tipos.cl");
            var MyLexer = new COOLgrammarLexer(new AntlrInputStream(streamReader.ReadToEnd()));
            streamReader.Close();
            var tokens = new CommonTokenStream(MyLexer);
            var MyParser = new COOLgrammarParser(tokens);
            var program = MyParser.program();
            Console.WriteLine(program.ToStringTree(MyParser));
            var typeVisitor = new TypeBuilderVisitor();
            typeVisitor.Visit(program);
            var FeatureVisitor = new FeatureBuilderVisitor(typeVisitor);
            FeatureVisitor.Visit(program);
            var CheckSemantic = new CheckSemanticVisitor(FeatureVisitor);
            CheckSemantic.Visit(program);
            if (CheckSemantic.errorLogger.msgs.Count == 0)
            {
                var generateCilTypes = new GenerateToCilTypes(CheckSemantic);
                generateCilTypes.Visit(program);
                var generateCilFeatures = new GenerateToCilFeatures(generateCilTypes);
                generateCilFeatures.Visit(program);
                var generateCil = new GenerateToCil(CheckSemantic, generateCilFeatures);
                //var generateCil = new GenerateToCilOptimization(CheckSemantic, generateCilFeatures);
                generateCil.Visit(program);
                Console.WriteLine(generateCil.CilAst);
                var generateMips = new CilToMips(generateCil);
                var mips = generateMips.Visit(program);
                Console.WriteLine(".data");
                mips.Data.ForEach(x => Console.WriteLine(x));
                Console.WriteLine(".text");
                mips.Text.ForEach(x => Console.WriteLine(x));
                Console.WriteLine("-----functions-----");
                mips.Functions.ForEach(x => Console.WriteLine(x));

            }
            foreach (var item in CheckSemantic.errorLogger.msgs)
            {
                Console.WriteLine(item);
            }
        }

       
    }
}
