using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
namespace CmpProject
{
    // ConsoleErrorListener<IToken>
    class MyErrorListenerLexer : IAntlrErrorListener<int>
    {
        public ErrorLogger errorLoggers { get; private set; }
        public MyErrorListenerLexer()
        {
            errorLoggers = new ErrorLogger();
        }
        public void SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            errorLoggers.LogError($"{e} line {line}:{charPositionInLine} {msg}");
        }
    }
    class MyErrorListener : BaseErrorListener
    {
        public ErrorLogger errorLoggers { get; private set; }
        public MyErrorListener()
        {
            errorLoggers = new ErrorLogger();
        }
      
        public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            errorLoggers.LogError($"{e} line {line}:{charPositionInLine} {msg}");
            base.SyntaxError(output, recognizer, offendingSymbol, line, charPositionInLine, msg, e);
        }
    }
   
}
