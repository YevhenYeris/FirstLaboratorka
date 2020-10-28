using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Windows.Forms;

namespace PoorExcel
{
    public class PoorCell : DataGridViewTextBoxCell
    {
        private string _column;         //Індекс стовпчика
        private int _row;               //Номер рядку
        private string expression = ""; //Вираз
        private string result = "";     //Результат обчислення виразу

        public string Expression
        //Вираз у клітинці
        {
            get { return expression; }
            set
            {
                expression = value;
                Result = CalcExpr(value);
            }
        }

        public string Result
        //Результат обчислення виразу
        {
            get { return result; }
            set { result = value; }
        }
        public string Name
        //Стовпчик+рядок
        {
            get;
            set;
        }

        public bool IsResultShown
        //Індикатор режиму відображення
        {
            get;
            set;
        }

        public PoorCell(int column, int row)
        {
            _column = PoorCalculator.GetColumnIndex(column);
            _row = row;
            Expression = "";
            IsResultShown = true;
        }

        public PoorCell(int column, int row, string expr) : this(column, row)
        {
            Expression = expr;
        }

        public string CalcExpr(string expr)
        //Підрахунок значення виразу
        {
            //try
            //{
                if (expr.Length > 0 && expr[0] == '=')
                {
                    return PoorCalculator.Evaluate(_column + _row.ToString() + "=+" + expr.Substring(1)).ToString();
                }
            /*}
            catch (DivideByZeroException exc)
            {
                MessageBox.Show(exc.Message);
            }*/
            return expr;
        }

        public void UpdateCell()
        //Оновлення значення виразу
        {
            Result = CalcExpr(expression);
        }

        public string GetContent()
        //Отримання відображуваних даних
        {
            if (IsResultShown)
            {
                return result;
            }
            return expression;
        }
    }
}
