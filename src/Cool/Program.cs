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
using CmpProject;

namespace Cool
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                try
                {
                    //var streamReader = new StreamReader("Testing/Tipos.cl");
                    //var streamReader = new StreamReader("Testing/Dispatch.cl");
                    //var streamReader = new StreamReader("Testing/ZAHUIS.cl");
                    //var streamReader = new StreamReader("Testing/Comparaciones.cl");
                    //var streamReader = new StreamReader("Testing/Expresion Case.cl");
                    //var streamReader = new StreamReader("Testing/Bool.cool");
                    //var streamReader = new StreamReader("Testing/Nuevo tipo.cl");
                    //var streamReader = new StreamReader("Testing/String.cl");
                    //var streamReader = new StreamReader("Testing/Tipos.cl");
                    //var streamReader = new StreamReader("Testing/Dispatch.cl");
                    //var streamReader = new StreamReader("Testing/ZAHUIS.cl");
                    //var streamReader = new StreamReader("Testing/Expresion Case.cl");
                    //var streamReader = new StreamReader("Testing/Bool.cool");
                    //var streamReader = new StreamReader("Testing/Nuevo tipo.cl");
                    //var streamReader = new StreamReader("Testing/Comparaciones.cl");
                    //var streamReader = new StreamReader("Testing/String.cl");
                    //var streamReader = new StreamReader("Testing/Expresion let.cl");
                    //var streamReader = new StreamReader("Testing/Tipos.cl");
                    //var streamReader = new StreamReader("Testing/While.cl");
                    //var streamReader = new StreamReader("Testing/Example.cl");
                    //var streamReader = new StreamReader("Testing/Factorial.cl");
                    //var streamReader = new StreamReader("Testing/Tipos.cl");
                    //var streamReader = new StreamReader("Testing/Test de Cool.cl");
                    //var streamReader = new StreamReader("Testing/Basic Types.cl");
                    //var streamReader = new StreamReader("bigexample.cl");
                    var path = args[0];
                    var streamReader = new StreamReader(path);
                    #region Lexer
                    var MyLexer = new COOLgrammarLexer(new AntlrInputStream(streamReader.ReadToEnd()));
                    MyLexer.RemoveErrorListeners();
                    var myErrorsLexer = new MyErrorListenerLexer();
                    MyLexer.AddErrorListener(myErrorsLexer);
                    streamReader.Close();
                    if (myErrorsLexer.errorLoggers.msgs.Count != 0)
                    {
                        foreach (var e in myErrorsLexer.errorLoggers.msgs)
                            Console.WriteLine(e);
                        return;
                    }
                    #endregion
                    var tokens = new CommonTokenStream(MyLexer);
                    #region Parser
                    var MyParser = new COOLgrammarParser(tokens);
                    MyParser.RemoveErrorListeners();
                    var myErrors = new MyErrorListenerParser();
                    MyParser.AddErrorListener(myErrors);
                    var program = MyParser.program();
                    //Si hay errores para la ejecucion del programa
                    if (myErrors.errorLoggers.msgs.Count != 0)
                    {
                        foreach (var e in myErrors.errorLoggers.msgs)
                            Console.WriteLine(e);
                        return;
                    }
                    //Console.WriteLine(program.ToStringTree(MyParser));
                    #endregion
                    #region CheckSemantic
                    var typeVisitor = new TypeBuilderVisitor();
                    typeVisitor.Visit(program);
                    var FeatureVisitor = new FeatureBuilderVisitor(typeVisitor);
                    FeatureVisitor.Visit(program);
                    var CheckSemantic = new CheckSemanticVisitor(FeatureVisitor);
                    CheckSemantic.Visit(program);
                    if (CheckSemantic.errorLogger.msgs.Count != 0)
                    {
                        foreach (var item in CheckSemantic.errorLogger.msgs)
                            Console.WriteLine(item);
                        return;
                    }
                    #endregion
                    #region CoolToCil
                    var generateCilTypes = new GenerateToCilTypes(CheckSemantic);
                    generateCilTypes.Visit(program);
                    //var generateCilFeatures = new GenerateToCilFeatures(generateCilTypes);
                    //generateCilFeatures.Visit(program);
                    var generateCil = new GenerateToCil(CheckSemantic, generateCilTypes);
                    //var generateCil = new GenerateToCilOptimization(CheckSemantic, generateCilFeatures);
                    generateCil.Visit(program);
                    // Console.WriteLine(generateCil.CilAst);
                    // StreamWriter streamWriter = new StreamWriter("mips.cil");
                    // streamWriter.WriteLine(generateCil.CilAst);
                    // streamWriter.Close();
                    #endregion
                    #region CilToMIPS
                    var generateMips = new CilToMips(generateCil);
                    var mips = generateMips.Visit(program);
                    var streamWriter = new StreamWriter($"{path.Replace(".cl",".mips")}");
                    streamWriter.WriteLine(".data");
                    mips.Data.ForEach(x => streamWriter.WriteLine(x));
                    streamWriter.WriteLine(".text");
                    mips.Text.ForEach(x => streamWriter.WriteLine(x));
                    streamWriter.WriteLine("##-----functions-----");
                    mips.Functions.ForEach(x => streamWriter.WriteLine(x));
                    //Console.WriteLine(".data");
                    //mips.Data.ForEach(x => Console.WriteLine(x));
                    //Console.WriteLine(".text");
                    //mips.Text.ForEach(x => Console.WriteLine(x));
                    //Console.WriteLine("##-----functions-----");
                    //mips.Functions.ForEach(x => Console.WriteLine(x));
                    streamWriter.Close();
                    #endregion
                    Console.WriteLine("Success!!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Debe pasar como argumento el fichero a compilar");
            }
        }
    }
}
