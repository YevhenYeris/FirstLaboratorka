using Antlr4.Runtime.Tree;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoorExcel
{
    public struct Coord
    {
        public int x;
        public int y;

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public struct Index
    {
        public int x;
        public string column;
        public int y;
        public string name;

        public Index(int x, int y)
        {
            this.x = x;
            this.column = PoorCalculator.GetColumnIndex(x);
            this.y = y;
            name = column + y.ToString();
        }
    }

    public partial class PoorEcxel : Form
    {
        public static Dictionary<Index, PoorCell> _indexToCell =
            new Dictionary<Index, PoorCell>();

        public static Dictionary<string, Coord> _indeces = new Dictionary<string, Coord>();
        public static  Dictionary<Coord, string> _coords = new Dictionary<Coord, string>();

        private Coord _tableSize = new Coord();

        public static Dictionary<Coord, List<Coord>> refs = new Dictionary<Coord, List<Coord>>();

        public PoorEcxel()
        {
            InitializeComponent();
            setTableDefault();
        }

        private void setTableDefault()
        //За замовчуванням створюється таблиця 50х50
        {
            setTable(50, 50);
        }

        private void setTable(int w, int h)
        //Створюється таблиця w на h, заповнюються словники _indexToCell, _indeces, _coords;
        //установлюється значення _tableSize
        {
            dataGrid.RowsDefaultCellStyle.SelectionBackColor = Color.DarkSeaGreen;
            dataGrid.RowHeadersWidth = 65;

            for (int i = 0; i < w; ++i) { AddColumn(i); }

            for (int i = 0; i < h; ++i) { AddRow(i); }

            for (int i = 0; i < w; ++i)
            {
                string column = PoorCalculator.GetColumnIndex(i);
                for (int j = 0; j < h; ++j)
                {
                    _indeces[column + j.ToString()] = new Coord(i, j);
                    _coords[new Coord(i, j)] = column + j.ToString();
                }
            }

         //   _tableSize = new Coord(w, h);
        }

        private void AddRow(int number)
        //Додається рядок
        {
            DataGridViewRow row = new DataGridViewRow();
            row.HeaderCell.Value = number.ToString();
            row.DefaultCellStyle.BackColor = Color.AliceBlue;
            dataGrid.Rows.Add(row);
            ++_tableSize.y;
        }

        private void AddColumn(int number)
        //Додається стовпчик
        {
            DataGridViewColumn column = new DataGridViewColumn();
            column.CellTemplate = new DataGridViewTextBoxCell();
            column.HeaderCell.Value = PoorCalculator.GetColumnIndex(number);
            column.DefaultCellStyle.BackColor = Color.AliceBlue;
            dataGrid.Columns.Add(column);
            ++_tableSize.x;
        }

        private void DataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //Оновлюється значення в клітинці. Додається новий елемент до
        //_indexToCell, якщо значення вписується вперше
        {
            int x = e.ColumnIndex;
            int y = e.RowIndex;

            try
            {
                Index index = new Index(x, y);
                string value = "";

                if (dataGrid.Rows[y].Cells[x].Value != null)
                {
                    value = dataGrid.Rows[y].Cells[x].Value.ToString();
                }

                if (!_indexToCell.ContainsKey(index))
                {
                    _indexToCell.Add(index, new PoorCell(x, y, value));
                }
                else
                {
                    //Вираз записується лише при введенні в режимі відображення ВИРАЗ,
                    //або при введенні з тектового рядку
                    if (!_indexToCell[index].IsResultShown)
                    {
                        _indexToCell[index].Expression = value;
                    }
                }

                if (_indexToCell[index].Expression == _indexToCell[index].Result && _indexToCell[index].Result == "")
                {
                    _indexToCell.Remove(index);
                }
                else
                {
                    dataGrid.Rows[y].Cells[x].Value = _indexToCell[index].GetContent();
                }
                UpdateTable();
            }
            catch (ArgumentException exc)
            {
                dataGrid.Rows[y].Cells[x].Value = exc.Message;
            }
        }

        private void DataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        //У textBoxExpression і textBoxIndex записуються вираз й
        //індекс натиснутої клітинуи відповідно
        {
            int x = e.ColumnIndex;
            int y = e.RowIndex;

            if (x >= 0 && y >= 0)
            {
                Index index = new Index(x, y);

                if (_indexToCell.ContainsKey(index))
                {
                    textBoxExpression.Text = _indexToCell[index].Expression;
                }
                else
                {
                    textBoxExpression.Text = "";
                }

                textBoxIndex.Text = _coords[new Coord(x, y)];
            }

            if (y == -1)
            {
                foreach (DataGridViewCell cell in dataGrid.SelectedCells)
                {
                    cell.Selected = false;
                }

                for (int i = 0; i < _tableSize.y; ++i)
                {
                    dataGrid.Rows[i].Cells[x].Selected = true;
                }
            }
        }

        private void TextBoxExpression_LostFocus(object sender, EventArgs e)
        //У виділені клітинки записується вираз з textBoxExpression після виходу з нього
        {
            foreach (DataGridViewCell item in dataGrid.SelectedCells)
            {
                int x = item.ColumnIndex;
                int y = item.RowIndex;
                Index index = new Index(x, y);
                string text = textBoxExpression.Text;

                if (_indexToCell.ContainsKey(index))
                {
                    _indexToCell[index].Expression = text;
                    item.Value = _indexToCell[index].GetContent();
                    UpdateTable();
                }
                else
                {
                    _indexToCell.Add(index, new PoorCell(x, y, text));
                    item.Value = text;
                }
            }
        }

        private void buttonDisplay_Click(object sender, EventArgs e)
        //Змінюється режим відображення клітинки: ВИРАЗ/ЗНАЧЕННЯ
        {
            if (buttonDisplay.Text == "РЕЗУЛЬТАТ")
            {
                buttonDisplay.Text = "ВИРАЗ";
            }
            else
            {
                buttonDisplay.Text = "РЕЗУЛЬТАТ";
            }

            foreach (var item in _indexToCell)
            {
                item.Value.IsResultShown = !item.Value.IsResultShown;
                dataGrid.Rows[item.Key.y].Cells[item.Key.x].Value = item.Value.GetContent();
            }
        }

        private void TextBoxIndex_LostFocus(object sender, EventArgs e)
        //Виділяється клітинка з індексом, указаним в
        //textBoxIndex
        {
            if (_indeces.ContainsKey(textBoxIndex.Text))
            {
                foreach (DataGridViewCell item in dataGrid.SelectedCells)
                {
                    item.Selected = false;
                }
                int x = _indeces[textBoxIndex.Text].x;
                int y = _indeces[textBoxIndex.Text].y;
                dataGrid.Rows[y].Cells[x].Selected = true;
                dataGrid.FirstDisplayedCell = dataGrid.SelectedCells[0];
            }
            else
            {
                foreach (DataGridViewCell item in dataGrid.SelectedCells)
                {
                    textBoxIndex.Text = _indexToCell[new Index(item.ColumnIndex, item.RowIndex)].GetContent();
                    break;
                }
            }
        }

        private void TextBoxExpression_KeyPressed(object sender, KeyEventArgs e)
        //Вихід з textBoxExpression після натискання Enter
        {
            if (e.KeyCode == Keys.Enter)
            {
                //textBoxExpression.Focus;
                dataGrid.Focus();
            }
        }

        private void TextBoxIndex_KeyPressed(object sender, KeyEventArgs e)
        //Вихід з textBoxIndex після натискання Enter
        {
            if (e.KeyCode == Keys.Enter)
            {
                //textBoxExpression.Focus;
                dataGrid.Focus();
            }
        }

        private void ToolStripButtonSave_ButtonClick(Object sender, EventArgs e)
        //Виклик методу для збереження таблиці
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "pxcl files (*.pxcl)|*.pxcl";
            saveFileDialog.ShowDialog();

            string path = saveFileDialog.FileName;
            if (path.Length > 0)
            {
                SaveFile(path);
                MessageBox.Show("Успішно збережено до\n" + path);
            }
        }

        private void ToolStripButtonOpen_ButtonClick(Object sender, EventArgs e)
        //Виклик методу для відкриття таблиці
        {
            DialogResult dialogResult = MessageBox.Show("Зберегти цю таблицю перед створення нової?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ToolStripButtonSave_ButtonClick(sender, e);
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "pxcl files (*.pxcl)|*.pxcl";
            openFileDialog.ShowDialog();
            string path = openFileDialog.FileName;
            if (path.Length > 0)
            {
                ReadFile(path);
            }
        }

        private void ToolStripButtonNew_ButtonClick(Object sender, EventArgs e)
        //Виклик методу для створення нової таблиці
        {
            DialogResult dialogResult = MessageBox.Show("Зберегти цю таблицю перед створення нової?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ToolStripButtonSave_ButtonClick(sender, e);
            }

            ResetTable();
            setTableDefault();
        }

        private void ToolStripButtonHelp_ButtonClick(Object sender, EventArgs e)
        //Виведення інформації про програму
        {
            string text = "Виконав Єріс Євген, група К-25\n" +
                "Варіaнт 43\nПрограма для роботи з електронними таблицями" +
                "\nДоступне виконання наступних операцій:\n" +
                "+ - * / ^\n" +
                "inc dec min max\n" +
                "> < == <= >= not\n" +
                "Синтаксис виразу:\n" +
                "=*вираз*";
            MessageBox.Show(text);
        }

        private void AddRow_Click(Object sender, EventArgs e)
        //Виклик методу для додавання рядку
        {
            AddRow(_tableSize.y);
        }

        private void AddColumn_Click(Object sender, EventArgs e)
        //Виклик методу для додавання стовпчику
        {
            AddColumn(_tableSize.x);
        }

        private void DeleteRow_Click(Object sender, EventArgs e)
        //Видалення рядку
        {
            bool canRemove = true;
            for (int x = 0; x < _tableSize.x; ++x)
            {
                if (_indexToCell.ContainsKey(new Index(x, _tableSize.y - 1)))
                {
                    canRemove = false;
                }
            }

            if (canRemove)
            {
                dataGrid.Rows.RemoveAt(--_tableSize.y);
            }
        }

        private void DeleteColumn_Click(Object sender, EventArgs e)
        //Видалення колонки
        {
            bool canRemove = true;
            for (int y = 0; y < _tableSize.y; ++y)
            {
                if (_indexToCell.ContainsKey(new Index(_tableSize.x - 1, y)))
                {
                    canRemove = false;
                }
            }

            if (canRemove)
            {
                dataGrid.Columns.RemoveAt(--_tableSize.x);
            }
        }

        private void SaveFile(string path)
        //Збереження таблиці
        {
            FileStream fStream = File.Create(path);

            AddText(fStream, _tableSize.x.ToString() + " " + _tableSize.y.ToString() + Environment.NewLine);

            foreach (var cell in _indexToCell) {
                AddText(fStream, cell.Key.y.ToString() + " " + cell.Key.x.ToString() + " " + cell.Value.Expression.ToString() + Environment.NewLine);
            }

            fStream.Close();
        }

        private void AddText(FileStream fStream, string text)
        //Додавання тексту в потік (для запису таблиці в файл)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(text);
            fStream.Write(info, 0, text.Length);
        }
        
        private void ReadFile(string path)
        //Створення таблиці з відкритого файлу
        {
            System.IO.StreamReader file =
                new System.IO.StreamReader(path);
            string temp = "";

            if ((temp = file.ReadLine()) != null)
            {
                ResetTable();
                int w = int.Parse(temp.ToString().Substring(0, temp.IndexOf(" ")));
                int divider = temp.IndexOf(" ") + 1;
                int h = int.Parse(temp.Substring(divider));
                setTable(w, h);

                while ((temp = file.ReadLine()) != null)
                {
                    int y = int.Parse(temp.Substring(0, temp.IndexOf(" ")));
                    divider = temp.IndexOf(" ") + 1;
                    temp = temp.Substring(divider);
                    int x = int.Parse(temp.Substring(0, temp.IndexOf(" ")));
                    divider = temp.IndexOf(" ") + 1;
                    temp = temp.Substring(divider);
                    string expr = temp;
                    if (!_indexToCell.ContainsKey(new Index(x, y)))
                    {
                        _indexToCell.Add(new Index(x, y), new PoorCell(x, y, expr));
                    }
                    else
                    {
                        _indexToCell[new Index(x, y)] = new PoorCell(x, y, expr);
                    }
                }
                UpdateTable();
            }
        }

        private void ResetTable()
        //Обнулення таблиці
        {
            _indexToCell.Clear();
            _indeces.Clear();
            _coords.Clear();
            _tableSize = new Coord();
            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear();
        }

        private void UpdateTable()
        //Оновлення значень клітинок таблиці
        {
            foreach (var cell in _indexToCell)
            {
                cell.Value.UpdateCell();
                dataGrid.Rows[cell.Key.y].Cells[cell.Key.x].Value = cell.Value.GetContent();
            }
        }
    }
}


