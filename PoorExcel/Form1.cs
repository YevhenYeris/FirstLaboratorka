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
using System.Threading;
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
        public static Dictionary<Index, PoorCell> indexToCell = new Dictionary<Index, PoorCell>(); //Словник для зберігання непорожніх клітинок         

        public static Dictionary<string, Coord> indeces = new Dictionary<string, Coord>();         //Словник буквенна адреса - координати
        public static  Dictionary<Coord, string> coords = new Dictionary<Coord, string>();         //Словник координати - буквенна адреса

        private Coord _tableSize = new Coord();                                                    //Розмір таблиці

        public PoorEcxel()
        {
            InitializeComponent();
            setTableDefault();
        }
        
        #region Редагування таблиці

        private void setTableDefault()
        //За замовчуванням створюється таблиця 50х50
        {
            setTable(50, 50);
        }

        private void setTable(int w, int h)
        //Створюється таблиця w на h, заповнюються словники indexToCell, indeces, coords;
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
                    indeces[column + j.ToString()] = new Coord(i, j);
                    coords[new Coord(i, j)] = column + j.ToString();
                }
            }
        }

        private void AddRow(int number)
        //Додається рядок
        {
            DataGridViewRow row = new DataGridViewRow();
            row.HeaderCell.Value = number.ToString();
            row.DefaultCellStyle.BackColor = Color.AliceBlue;
            dataGrid.Rows.Add(row);
            ++_tableSize.y;

            for (int x = 0; x < _tableSize.x; ++x)
            {
                Coord coord = new Coord(x, number);
                string index = PoorCalculator.GetIndex(x, number);
                if (!coords.ContainsKey(coord))
                {
                    coords.Add(coord, index);
                }
                if (!indeces.ContainsKey(index))
                {
                    indeces.Add(index, coord);
                }
            }
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
            for (int y = 0; y < _tableSize.y; ++y)
            {
                Coord coord = new Coord(number, y);
                string index = PoorCalculator.GetIndex(number, y);
                if (!coords.ContainsKey(coord))
                {
                    coords.Add(coord, index);
                }
                if (!indeces.ContainsKey(index))
                {
                    indeces.Add(index, coord);
                }
            }
        }

        private void DataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //Оновлюється значення в клітинці. Додається новий елемент до
        //indexToCell, якщо значення вписується вперше
        {
            int x = e.ColumnIndex;
            int y = e.RowIndex;
            Index index = new Index(x, y);
            string value = "";


            try
            {
                if (dataGrid.Rows[y].Cells[x].Value != null)
                {
                    value = dataGrid.Rows[y].Cells[x].Value.ToString();
                }

                PoorCell cell = new PoorCell(x, y, value);

                if (!indexToCell.ContainsKey(index))
                {
                    indexToCell.Add(index, cell);
                }
                else
                {
                    //Вираз записується лише при введенні в режимі відображення ВИРАЗ,
                    //або при введенні з тектового рядку
                    if (!indexToCell[index].IsResultShown)
                    {
                        indexToCell[index].Expression = value;
                    }
                }
            
                if (indexToCell[index].Expression == indexToCell[index].Result && indexToCell[index].Result == "")
                {
                    indexToCell.Remove(index);
                }
                else
                {
                    dataGrid.Rows[y].Cells[x].Value = indexToCell[index].GetContent();
                }
    
                UpdateTable();
            }
            catch (DivideByZeroException exc)
            {
                MessageBox.Show("Помилка: спроба ділення на нуль!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (LockRecursionException exc)
            {
                MessageBox.Show("Помилка: вираз створює рекурсію!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Помилка: " + exc.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                if (indexToCell.ContainsKey(index))
                {
                    textBoxExpression.Text = indexToCell[index].Expression;
                }
                else
                {
                    textBoxExpression.Text = "";
                }

                textBoxIndex.Text = coords[new Coord(x, y)];
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

        private void buttonDisplay_Click(object sender, EventArgs e)
        //Змінюється режим відображення клітинки: ВИРАЗ/ЗНАЧЕННЯ
        {
            if (buttonDisplay.Text == "РЕЗУЛЬТАТ")
            {
                buttonDisplay.Text = "ВИРАЗ";
                foreach (var item in indexToCell)
                {
                    item.Value.IsResultShown = false;
                    dataGrid.Rows[item.Key.y].Cells[item.Key.x].Value = item.Value.GetContent();
                }
            }
            else
            {
                buttonDisplay.Text = "РЕЗУЛЬТАТ";
                foreach (var item in indexToCell)
                {
                    item.Value.IsResultShown = true;
                    dataGrid.Rows[item.Key.y].Cells[item.Key.x].Value = item.Value.GetContent();
                }
            }

            
        }
        private void UpdateTable()
        //Оновлення значень клітинок таблиці
        {
            for (int i = 0; i < indexToCell.Count; ++i)
            {
                int x = indexToCell.ElementAt(i).Key.x;
                int y = indexToCell.ElementAt(i).Key.y;

                indexToCell.ElementAt(i).Value.UpdateCell();
                dataGrid.Rows[y].Cells[x].Value= indexToCell.ElementAt(i).Value.GetContent();
            }
        }

        #endregion

        #region Робота з TextBox

        private void TextBoxIndex_LostFocus(object sender, EventArgs e)
        //Виділяється клітинка з індексом, указаним в
        //textBoxIndex
        {
            if (indeces.ContainsKey(textBoxIndex.Text))
            {
                foreach (DataGridViewCell item in dataGrid.SelectedCells)
                {
                    item.Selected = false;
                }
                int x = indeces[textBoxIndex.Text].x;
                int y = indeces[textBoxIndex.Text].y;
                dataGrid.Rows[y].Cells[x].Selected = true;
                dataGrid.FirstDisplayedCell = dataGrid.SelectedCells[0];
            }
            else
            {
                MessageBox.Show("Помилка: указаної клітинки не існує!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void ResetTable()
        //Обнулення таблиці
        {
            indexToCell.Clear();
            indeces.Clear();
            coords.Clear();
            _tableSize = new Coord();
            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear();
        }

        private void TextBoxExpression_LostFocus(object sender, EventArgs e)
        //У виділені клітинки записується вираз з textBoxExpression після виходу з нього
        {
            try
            {
                foreach (DataGridViewCell item in dataGrid.SelectedCells)
                {
                    int x = item.ColumnIndex;
                    int y = item.RowIndex;
                    Index index = new Index(x, y);
                    string text = textBoxExpression.Text;

                    if (indexToCell.ContainsKey(index))
                    {
                        indexToCell[index].Expression = text;
                        item.Value = indexToCell[index].GetContent();
                        UpdateTable();
                    }
                    else
                    {
                        indexToCell.Add(index, new PoorCell(x, y, text));
                        item.Value = text;
                    }
                }
            }
            catch (DivideByZeroException exc)
            {
                MessageBox.Show("Помилка: спроба ділення на нуль!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (LockRecursionException exc)
            {
                MessageBox.Show("Помилка: вираз створює рекурсію!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Помилка: " + exc.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Обробка натискання ToolStrip кнопок

        private void ToolStripButtonOpen_ButtonClick(Object sender, EventArgs e)
        //Виклик методу для відкриття таблиці
        {
            DialogResult dialogResult = MessageBox.Show("Зберегти цю таблицю перед створення нової?", "Попередження",
                                                                       MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                ToolStripButtonSave_ButtonClick(sender, e);
            }
            else if (dialogResult != DialogResult.Cancel)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "pxcl files (*.pxcl)|*.pxcl";
                openFileDialog.ShowDialog();
                string path = openFileDialog.FileName;
                if (path.Length > 0)
                {
                    ReadFile(path);
                }
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
                MessageBox.Show("Успішно збережено до\n" + path, "Збержено", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ToolStripButtonNew_ButtonClick(Object sender, EventArgs e)
        //Виклик методу для створення нової таблиці
        {
            DialogResult dialogResult = MessageBox.Show("Зберегти цю таблицю перед створення нової?",
                                                                    "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                ToolStripButtonSave_ButtonClick(sender, e);
            }
            else if (dialogResult != DialogResult.Cancel)
            {
                ResetTable();
                setTableDefault();
            }
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
                "=*вираз*\n" +
                "Використовуються цілі числа."; 
            MessageBox.Show(text, "Довідка", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (!DeleteRow())
            {
                MessageBox.Show("Hеможливо видалити непорожній рядок", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool DeleteRow()
        {
            bool canRemove = true;

            for (int x = 0; x < _tableSize.x; ++x)
            {
                if (indexToCell.ContainsKey(new Index(x, _tableSize.y - 1)))
                {
                    return false;
                }
            }

            if (canRemove)
            {
                dataGrid.Rows.RemoveAt(--_tableSize.y);

                for (int x = 0; x < _tableSize.x; ++x)
                {
                    Coord coord = new Coord(x, _tableSize.y);
                    string index = PoorCalculator.GetIndex(x, _tableSize.y);
                    if (coords.ContainsKey(coord))
                    {
                        coords.Remove(coord);
                    }
                    if (indeces.ContainsKey(index))
                    {
                        indeces.Remove(index);
                    }
                }
            }

            return true;
        }

        private void DeleteColumn_Click(Object sender, EventArgs e)
        //Видалення колонки
        {
            bool canRemove = true;
            for (int y = 0; y < _tableSize.y; ++y)
            {
                if (indexToCell.ContainsKey(new Index(_tableSize.x - 1, y)))
                {
                    canRemove = false;
                    MessageBox.Show("Hеможливо видалити непорожній стовпчик", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (canRemove)
            {
                dataGrid.Columns.RemoveAt(--_tableSize.x);
                for (int y = 0; y < _tableSize.y; ++y)
                {
                    Coord coord = new Coord(_tableSize.x, y);
                    string index = PoorCalculator.GetIndex(_tableSize.x, y);
                    if (coords.ContainsKey(coord))
                    {
                        coords.Remove(coord);
                    }
                    if (indeces.ContainsKey(index))
                    {
                        indeces.Remove(index);
                    }
                }
            }
        }

        #endregion

        #region Збереження й відкривання файллів

        private void SaveFile(string path)
        //Збереження таблиці
        {
            FileStream fStream = File.Create(path);

            AddText(fStream, _tableSize.x.ToString() + " " + _tableSize.y.ToString() + Environment.NewLine);

            foreach (var cell in indexToCell)
            {
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
                    if (!indexToCell.ContainsKey(new Index(x, y)))
                    {
                        indexToCell.Add(new Index(x, y), new PoorCell(x, y, expr));
                    }
                    else
                    {
                        indexToCell[new Index(x, y)] = new PoorCell(x, y, expr);
                    }
                }
                UpdateTable();
            }
        }
        #endregion

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (AskExit() == false)
            {
                e.Cancel = true;
            };
        }

        public static bool AskExit()
        {
            const string message = "Ви дійсно бажаєте закрити програму?";
            const string caption = "Закрити програму";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                return true;
            else
                return false;
        }
    }
}