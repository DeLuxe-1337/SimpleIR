using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Dfa;
using Antlr4.Runtime.Sharpen;
using SimpleErrorPresenter;

namespace SimpleIR_Code
{
    internal class ParseSyntaxError : BaseErrorListener
    {
        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line,
            int charPositionInLine, string msg, RecognitionException e)
        {
            SimpleError.Error($"Line: {line} | Offending symbol: ERROR!<{offendingSymbol.Text}>", msg);
        }

        public override void ReportAmbiguity(Parser recognizer, DFA dfa, int startIndex, int stopIndex, bool exact,
            BitSet ambigAlts,
            ATNConfigSet configs)
        {
            base.ReportAmbiguity(recognizer, dfa, startIndex, stopIndex, exact, ambigAlts, configs);
        }
    }
}