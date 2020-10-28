using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PoorExcel
{
    public class PoorCalculator
    {
        public static int Evaluate(string expression)
        {
            var lexer = new PoorGrammarLexer(new AntlrInputStream(expression));
            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(new PoorThrowExceptionErrorListener());

            var tokens = new CommonTokenStream(lexer);
            var parser = new PoorGrammarParser(tokens);

            var tree = parser.compileUnit();

            var visitor = new PoorVisitor();

            return visitor.Visit(tree);
        }

        public static string GetColumnIndex(int number)
        //Створити буквенний індекс стовпчика з його порядкового номеру
        {
            const int Alphabet = 26;
            String index = "";

            if (number < Alphabet)
            {
                index += (char)(65 + number);
            }
            else
            {
                index += (char)(65 + number / Alphabet - 1);
                index += (char)(65 + number % Alphabet);
            }
            return index;
        }

        public static string GetIndex(int column, int row)
        {
            return GetColumnIndex(column) + row.ToString();
        }
    }
}
