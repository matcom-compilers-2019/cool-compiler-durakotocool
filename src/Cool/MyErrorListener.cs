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
            errorLoggers.LogError($"({line},{charPositionInLine}) - LexicographicError {msg}");
        }
    }
    class MyErrorListenerParser : BaseErrorListener
    {
        public ErrorLogger errorLoggers { get; private set; }
        public MyErrorListenerParser()
        {
            errorLoggers = new ErrorLogger();
        }
      
        public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            errorLoggers.LogError($"({line},{charPositionInLine}) - SyntaticError {msg}");
            base.SyntaxError(output, recognizer, offendingSymbol, line, charPositionInLine, msg, e);
        }
    }
   
}
