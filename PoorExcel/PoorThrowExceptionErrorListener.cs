using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoorExcel
{
    class PoorThrowExceptionErrorListener : BaseErrorListener, IAntlrErrorListener<int>
    {
        public override void SyntaxError([NotNull] IRecognizer recognizer, [Nullable] IToken offendingSymbol, int line, int charPositionInLine, [NotNull] string msg, [Nullable] RecognitionException e)
        {
            throw new ArgumentException("Invalid Expression: {0}", msg, e);
        }

        public void SyntaxError([NotNull] IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, [NotNull] string msg, [Nullable] RecognitionException e)
        {
            throw new ArgumentException("Invalid Expression: {0}", msg, e);
        }
    }
}
