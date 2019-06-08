﻿using System;
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
            //var streamReader = new StreamReader("Testing/Tipos.cl").ReadToEnd();
            //var streamReader = new StreamReader("Testing/Dispatch.cl").ReadToEnd();
            //var streamReader = new StreamReader("Testing/Expresion Case.cool").ReadToEnd();
            //var streamReader = new StreamReader("Testing/Bool.cool").ReadToEnd();
            var streamReader = new StreamReader("Testing/Nuevo tipo.cl").ReadToEnd();
            //var streamReader = new StreamReader("Testing/Comparaciones.cl").ReadToEnd();
            //var streamReader = new StreamReader("Testing/String.cl").ReadToEnd();
            //var streamReader = new StreamReader("Testing/Expresion let.cl").ReadToEnd();
            //var streamReader = new StreamReader("Testing/Factorial.cl").ReadToEnd();
            //var streamReader = new StreamReader("Testing/Tipos.cl").ReadToEnd();
            var MyLexer = new COOLgrammarLexer(new AntlrInputStream(streamReader));
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