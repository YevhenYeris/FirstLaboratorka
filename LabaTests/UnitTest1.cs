using PoorExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using Antlr4.Runtime.Tree;
using System.Threading;

namespace PoorExcel.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        [TestMethod()]
        public void CalcExprTest()
        {
            PoorCell cell = new PoorCell(0, 0);

            string expected = "";
            string actual = "";

            /*
             * Виконання арифметичних операцій
             */

            expected = "4";
            actual = cell.CalcExpr("= 1 + 3");
            Assert.AreEqual(expected, actual);

            expected = Math.Pow(1 + 2 * 3 - 5 / 6, 7).ToString();
            actual = cell.CalcExpr("=(1 + 2 * 3 - 5 / 6)^7");
            Assert.AreEqual(expected, actual);

            expected = Math.Pow((1 + 2) * 3 - 5 / 6, 7).ToString();
            actual = cell.CalcExpr("=((1 + 2) * 3 - 5 / 6)^7");
            Assert.AreEqual(expected, actual);

            expected = Math.Pow((1 + 2) * (3 - 5 / 6), 7).ToString();
            actual = cell.CalcExpr("=((1 + 2) * (3 - 5 / 6))^7");
            Assert.AreEqual(expected, actual);

            expected = Math.Pow((1 + 2) * ((3 - 5) / 6), 7).ToString();
            actual = cell.CalcExpr("=((1 + 2) * ((3 - 5) / 6))^7");
            Assert.AreEqual(expected, actual);

            expected = Math.Min(Math.Pow(1 + 2 * 3 - 5 / 6, 7), 34).ToString();
            actual = cell.CalcExpr("=min((1 + 2 * 3 - 5 / 6)^7, 34)");
            Assert.AreEqual(expected, actual);

            expected = Math.Max(Math.Pow(1 + 2 * 3 - 5 / 6, 7), 34).ToString();
            actual = cell.CalcExpr("=max((1 + 2 * 3 - 5 / 6)^7, 34)");
            Assert.AreEqual(expected, actual);

            expected = (Math.Max(Math.Pow(1 + 2 * 3 - 5 / 6, 7), 34) + 1).ToString();
            actual = cell.CalcExpr("=inc(max((1 + 2 * 3 - 5 / 6)^7, 34))");
            Assert.AreEqual(expected, actual);

            expected = (Math.Max(Math.Pow(1 + 2 * 3 - 5 / 6, 7), 34) - 1).ToString();
            actual = cell.CalcExpr("=dec(max((1 + 2 * 3 - 5 / 6)^7, 34))");
            Assert.AreEqual(expected, actual);

            /*
             * Виконання порівняльних операцій
             */

            expected = Convert.ToDouble(3 >= 4).ToString();
            actual = cell.CalcExpr("=3>=4");
            Assert.AreEqual(expected, actual);

            expected = Convert.ToDouble(3 >= 4 == 3 < 3).ToString();
            actual = cell.CalcExpr("=3 >= 4 == 3 < 3");
            Assert.AreEqual(expected, actual);

            expected = Convert.ToDouble(!(3 >= 4 == 3 < 3)).ToString();
            actual = cell.CalcExpr("=not(3 >= 4 == 3 < 3)");
            Assert.AreEqual(expected, actual);

            expected = Convert.ToDouble(!(3 >= 4 == 3 <= 3)).ToString();
            actual = cell.CalcExpr("=not(3 >= 4 == 3 <= 3)");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetContentTest()
        {
            PoorCell cell = new PoorCell(0, 0);

            string expected = "";
            string actual = "";

            /*
             * Дія методу після використання різних конструкторів
             */

            cell = new PoorCell(0, 0);
            expected = "";
            actual = cell.GetContent();
            Assert.AreEqual(expected, actual);

            cell = new PoorCell(0, 0, "123");
            expected = "123";
            actual = cell.GetContent();
            Assert.AreEqual(expected, actual);

            cell = new PoorCell(0, 0, "afadfs-\nf09di9f ji./\tsdff");
            expected = "afadfs-\nf09di9f ji./\tsdff";
            actual = cell.GetContent();
            Assert.AreEqual(expected, actual);

            cell = new PoorCell(0, 0, "=((1 + 2) * 3 - 5 / 6)^7");
            expected = Math.Pow(((1 + 2) * 3 - 5 / 6), 7).ToString();
            actual = cell.GetContent();
            Assert.AreEqual(expected, actual);

            cell = new PoorCell(0, 0, "((1 + 2) * 3 - 5 / 6)^7");
            expected = "((1 + 2) * 3 - 5 / 6)^7";
            actual = cell.GetContent();
            Assert.AreEqual(expected, actual);

            /*
             * Дія методу після зміни параметра IsResultShown
             */

            cell = new PoorCell(0, 0, "123");
            expected = "123";
            actual = cell.GetContent();
            Assert.AreEqual(expected, actual);

            cell.IsResultShown = false;
            expected = "123";
            actual = cell.GetContent();
            Assert.AreEqual(expected, actual);

            cell.IsResultShown = true;
            expected = "123";
            actual = cell.GetContent();
            Assert.AreEqual(expected, actual);

            cell.Result = "50";
            expected = "50";
            actual = cell.GetContent();
            Assert.AreEqual(expected, actual);

            cell.IsResultShown = false;
            expected = "123";
            actual = cell.GetContent();
            Assert.AreEqual(expected, actual);

            cell.Expression = "=((1 + 2) * 3 - 5 / 6)^7";
            expected = "=((1 + 2) * 3 - 5 / 6)^7";
            actual = cell.GetContent();
            Assert.AreEqual(expected, actual);

            cell.IsResultShown = true;
            cell.Expression = "=((1 + 2) * 3 - 5 / 6)^7";
            expected = Math.Pow(((1 + 2) * 3 - 5 / 6), 7).ToString();
            actual = cell.GetContent();
            Assert.AreEqual(expected, actual);

            cell.Expression = "123";
            cell.Result = "=((1 + 2) * 3 - 5 / 6)^7";
            expected = "=((1 + 2) * 3 - 5 / 6)^7";
            actual = cell.GetContent();
            Assert.AreEqual(expected, actual);

            cell.IsResultShown = false;
            expected = "123";
            actual = cell.GetContent();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void EvaluateTest()
        {
            /*
             * Запобігання рекурсії
             */

            PoorEcxel.indexToCell.Add(new Index(1, 1), new PoorCell(1, 1, "=A5"));
            try
            {
                PoorCalculator.Evaluate("A5=+G4");
            }
            catch (Exception exc)
            {
                Assert.AreEqual(exc, new LockRecursionException());
            }


            PoorEcxel.indexToCell[new Index(1, 1)].Expression = "=A5";
            try
            {
                PoorCalculator.Evaluate("A5=+G4");
            }
            catch (Exception exc)
            {
                Assert.AreEqual(exc, new LockRecursionException());
            }

            PoorEcxel.indexToCell[new Index(1, 1)].Expression = "";
            try
            {
                PoorCalculator.Evaluate("A5=+G4");
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}
