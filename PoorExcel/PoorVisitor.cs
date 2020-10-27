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

    class PoorVisitor : PoorGrammarBaseVisitor<double>
    {
        private string _routeCell = "";

        public override double VisitCompileUnit([NotNull] PoorGrammarParser.CompileUnitContext context)
        {
            return Visit(context.expression());
        }

        public override double VisitNumberExpr([NotNull] PoorGrammarParser.NumberExprContext context)
        {
            var result = double.Parse(context.GetText());
            Debug.WriteLine(result);

            return result;
        }

        public override double VisitIdentifierExpr([NotNull] PoorGrammarParser.IdentifierExprContext context)
        {
            var result = context.GetText();
            double value = 0.0;

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

        public override double VisitParenthesizedExpr([NotNull] PoorGrammarParser.ParenthesizedExprContext context)
        {
            return Visit(context.expression());
        }

        public override double VisitExponentialExpr([NotNull] PoorGrammarParser.ExponentialExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            Debug.WriteLine("{0}^{1}", left, right);
            return System.Math.Pow(left, right);
        }

        public override double VisitAdditiveExpr([NotNull] PoorGrammarParser.AdditiveExprContext context)
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

        public override double VisitMultiplicativeExpr([NotNull] PoorGrammarParser.MultiplicativeExprContext context)
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

        public override double VisitMinExpr([NotNull] PoorGrammarParser.MinExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            return Math.Min(left, right);
        }

        public override double VisitMaxExpr([NotNull] PoorGrammarParser.MaxExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            return Math.Max(left, right);
        }

        public override double VisitModExpr([NotNull] PoorGrammarParser.ModExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            return left % right;
        }

        public override double VisitDivExpr([NotNull] PoorGrammarParser.DivExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            return left / right;
        }

        public override double VisitDecExpr([NotNull] PoorGrammarParser.DecExprContext context)
        {
            var left = WalkLeft(context);

            return --left;
        }

        public override double VisitIncExpr([NotNull] PoorGrammarParser.IncExprContext context)
        {
            var left = WalkLeft(context);

            return ++left;
        }

        public override double VisitNotExpr([NotNull] PoorGrammarParser.NotExprContext context)
        {
            var left = WalkLeft(context);

            if (left == 0)
            {
                return 1;
            }

            return 0;
        }

        public override double VisitIsMoreExpr([NotNull] PoorGrammarParser.IsMoreExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);
            if (left > right)
            {
                return 1;
            }
            return 0;
        }

        public override double VisitIsLessExpr([NotNull] PoorGrammarParser.IsLessExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);
            if (left < right)
            {
                return 1;
            }
            return 0;
        }

        public override double VisitIsEqualExpr([NotNull] PoorGrammarParser.IsEqualExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);
            if (left == right)
            {
                return 1;
            }
            return 0;
        }

        public override double VisitIsMoreOrEqualExpr([NotNull] PoorGrammarParser.IsMoreOrEqualExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);
            if (left >= right)
            {
                return 1;
            }
            return 0;
        }

        public override double VisitIsLessOrEqualExpr([NotNull] PoorGrammarParser.IsLessOrEqualExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);
            if (left <= right)
            {
                return 1;
            }
            return 0;
        }

        private double WalkLeft(PoorGrammarParser.ExpressionContext context)
        {
            return Visit(context.GetRuleContext<PoorGrammarParser.ExpressionContext>(0));
        }

        private double WalkRight(PoorGrammarParser.ExpressionContext context)
        {
            return Visit(context.GetRuleContext<PoorGrammarParser.ExpressionContext>(1));
        }
    }
}
