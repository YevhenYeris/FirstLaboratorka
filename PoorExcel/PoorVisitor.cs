using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;

namespace PoorExcel
{

    class PoorVisitor : PoorGrammarBaseVisitor<int>
    {
        private string _routeCell = "";

        public override int VisitCompileUnit([NotNull] PoorGrammarParser.CompileUnitContext context)
        {
            return Visit(context.expression());
        }

        public override int VisitNumberExpr([NotNull] PoorGrammarParser.NumberExprContext context)
        {
            var result = int.Parse(context.GetText());
            Debug.WriteLine(result);

            return result;
        }

        public override int VisitIdentifierExpr([NotNull] PoorGrammarParser.IdentifierExprContext context)
        {
            var result = context.GetText();
            int value = 0;

            if (result[result.Length - 1] == '=')
            {
                _routeCell = result.Remove(result.Length - 1);
            }
            else
            {
                if (PoorEcxel.indeces.ContainsKey(result))
                {
                    Index index = new Index(PoorEcxel.indeces[result].x, PoorEcxel.indeces[result].y);

                    if (!PoorEcxel.indexToCell.ContainsKey(index))
                    {
                        PoorEcxel.indexToCell.Add(index, new PoorCell(PoorEcxel.indeces[result].x, PoorEcxel.indeces[result].y, "0"));
                    }

                    string expr = PoorEcxel.indexToCell[index].Result;

                    if (PoorEcxel.indexToCell[index].Expression.Contains(_routeCell))
                    //Клітинка посилається на іншу клітинку, яка вже містить посилання на першу
                    {
                        throw new LockRecursionException();
                    }

                    value = PoorCalculator.Evaluate(expr);
                }
            }
            return value;
        }

        public override int VisitParenthesizedExpr([NotNull] PoorGrammarParser.ParenthesizedExprContext context)
        {
            return Visit(context.expression());
        }

        public override int VisitExponentialExpr([NotNull] PoorGrammarParser.ExponentialExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            Debug.WriteLine("{0}^{1}", left, right);
            return (int)System.Math.Pow(left, right);
        }

        public override int VisitAdditiveExpr([NotNull] PoorGrammarParser.AdditiveExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            if (context.operatorToken.Type == PoorGrammarLexer.ADD)
            {
                Debug.WriteLine("{0}+{1}", left, right);
                return left + right;
            }
            else
            {
                Debug.WriteLine("{0}-{1}", left, right);
                return left - right;
            }
        }

        public override int VisitMultiplicativeExpr([NotNull] PoorGrammarParser.MultiplicativeExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            if (context.operatorToken.Type == PoorGrammarLexer.MULTIPLY)
            {
                Debug.WriteLine("{0}*{1}", left, right);
                return left * right;
            }
            else
            {
                if (right == 0)
                {
                    throw new DivideByZeroException();
                }
                Debug.WriteLine("{0}/{1}", left, right);
                return left / right;
            }
        }

        public override int VisitMinExpr([NotNull] PoorGrammarParser.MinExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            if (context.operatorToken.Type == PoorGrammarLexer.MIN)
            {
                return Math.Min(left, right);
            }
            else
            {
                return Math.Max(left, right);
            }
        }

        public override int VisitModExpr([NotNull] PoorGrammarParser.ModExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            if (context.operatorToken.Type == PoorGrammarLexer.MOD)
            {
                return left % right;
            }
            else
            {
                return left / right;
            }
        }

        public override int VisitIncExpr([NotNull] PoorGrammarParser.IncExprContext context)
        {
            var left = WalkLeft(context);

            if (context.operatorToken.Type == PoorGrammarLexer.INC)
            {
                return ++left;
            }
            else
            {
                return --left;
            }
        }

        public override int VisitNotExpr([NotNull] PoorGrammarParser.NotExprContext context)
        {
            var left = WalkLeft(context);

            if (left == 0)
            {
                return 1;
            }

            return 0;
        }

        public override int VisitIsEqualExpr([NotNull] PoorGrammarParser.IsEqualExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);
            if (left == right)
            {
                return 1;
            }
            return 0;
        }

        public override int VisitCompareExpr([NotNull] PoorGrammarParser.CompareExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            switch(context.operatorToken.Type)
            {
                case PoorGrammarLexer.ISMORE:
                    if (left > right)
                    {
                        return 1;
                    }
                    return 0;

                case PoorGrammarLexer.ISLESS:
                    if (left < right)
                    {
                        return 1;
                    }
                    return 0;

                case PoorGrammarLexer.ISMOREOREQUAL:
                    if (left >= right)
                    {
                        return 1;
                    }
                    return 0;
            }

            if (left <= right)
            {
                return 1;
            }
            return 0;
        }

        private int WalkLeft(PoorGrammarParser.ExpressionContext context)
        {
            return Visit(context.GetRuleContext<PoorGrammarParser.ExpressionContext>(0));
        }

        private int WalkRight(PoorGrammarParser.ExpressionContext context)
        {
            return Visit(context.GetRuleContext<PoorGrammarParser.ExpressionContext>(1));
        }
    }
}
