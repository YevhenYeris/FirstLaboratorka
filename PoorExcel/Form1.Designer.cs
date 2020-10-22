namespace PoorExcel
{
    partial class PoorEcxel
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PoorEcxel));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButtonEdit = new System.Windows.Forms.ToolStripDropDownButton();
            this.addRow = new System.Windows.Forms.ToolStripMenuItem();
            this.addColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonDisplay = new System.Windows.Forms.Button();
            this.textBoxExpression = new System.Windows.Forms.TextBox();
            this.textBoxIndex = new System.Windows.Forms.TextBox();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Teal;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 95F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1262, 673);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.Color.DarkCyan;
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator,
            this.helpToolStripButton,
            this.toolStripDropDownButtonEdit});
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip.Location = new System.Drawing.Point(1198, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(64, 673);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(62, 24);
            this.newToolStripButton.Text = "&New";
            this.newToolStripButton.Click += new System.EventHandler(this.ToolStripButtonNew_ButtonClick);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(62, 24);
            this.openToolStripButton.Text = "&Open";
            this.openToolStripButton.Click += new System.EventHandler(this.ToolStripButtonOpen_ButtonClick);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(62, 24);
            this.saveToolStripButton.Text = "&Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.ToolStripButtonSave_ButtonClick);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(62, 6);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(62, 24);
            this.helpToolStripButton.Text = "He&lp";
            this.helpToolStripButton.Click += new System.EventHandler(this.ToolStripButtonHelp_ButtonClick);
            // 
            // toolStripDropDownButtonEdit
            // 
            this.toolStripDropDownButtonEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButtonEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRow,
            this.addColumn,
            this.deleteRow,
            this.deleteColumn});
            this.toolStripDropDownButtonEdit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonEdit.Image")));
            this.toolStripDropDownButtonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonEdit.Name = "toolStripDropDownButtonEdit";
            this.toolStripDropDownButtonEdit.Size = new System.Drawing.Size(62, 24);
            this.toolStripDropDownButtonEdit.Text = "toolStripDropDownButton1";
            // 
            // addRow
            // 
            this.addRow.Name = "addRow";
            this.addRow.Size = new System.Drawing.Size(225, 26);
            this.addRow.Text = "Додати рядок";
            this.addRow.Click += new System.EventHandler(this.AddRow_Click);
            // 
            // addColumn
            // 
            this.addColumn.Name = "addColumn";
            this.addColumn.Size = new System.Drawing.Size(225, 26);
            this.addColumn.Text = "Додати стовпчик";
            this.addColumn.Click += new System.EventHandler(this.AddColumn_Click);
            // 
            // deleteRow
            // 
            this.deleteRow.Name = "deleteRow";
            this.deleteRow.Size = new System.Drawing.Size(225, 26);
            this.deleteRow.Text = "Видалити рядок";
            this.deleteRow.Click += new System.EventHandler(this.DeleteRow_Click);
            // 
            // deleteColumn
            // 
            this.deleteColumn.Name = "deleteColumn";
            this.deleteColumn.Size = new System.Drawing.Size(225, 26);
            this.deleteColumn.Text = "Видалити стовпчик";
            this.deleteColumn.Click += new System.EventHandler(this.DeleteColumn_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.dataGrid, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1192, 667);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.buttonDisplay, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.textBoxExpression, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.textBoxIndex, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1186, 34);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // buttonDisplay
            // 
            this.buttonDisplay.AllowDrop = true;
            this.buttonDisplay.BackColor = System.Drawing.Color.Thistle;
            this.buttonDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonDisplay.Location = new System.Drawing.Point(63, 3);
            this.buttonDisplay.Name = "buttonDisplay";
            this.buttonDisplay.Size = new System.Drawing.Size(124, 28);
            this.buttonDisplay.TabIndex = 1;
            this.buttonDisplay.Text = "РЕЗУЛЬТАТ";
            this.buttonDisplay.UseVisualStyleBackColor = false;
            this.buttonDisplay.Click += new System.EventHandler(this.buttonDisplay_Click);
            // 
            // textBoxExpression
            // 
            this.textBoxExpression.BackColor = System.Drawing.Color.PowderBlue;
            this.textBoxExpression.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxExpression.Location = new System.Drawing.Point(193, 3);
            this.textBoxExpression.Name = "textBoxExpression";
            this.textBoxExpression.Size = new System.Drawing.Size(990, 27);
            this.textBoxExpression.TabIndex = 2;
            this.textBoxExpression.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxExpression_KeyPressed);
            this.textBoxExpression.LostFocus += new System.EventHandler(this.TextBoxExpression_LostFocus);
            // 
            // textBoxIndex
            // 
            this.textBoxIndex.BackColor = System.Drawing.Color.PowderBlue;
            this.textBoxIndex.Location = new System.Drawing.Point(3, 3);
            this.textBoxIndex.Name = "textBoxIndex";
            this.textBoxIndex.Size = new System.Drawing.Size(52, 27);
            this.textBoxIndex.TabIndex = 3;
            this.textBoxIndex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxIndex_KeyPressed);
            this.textBoxIndex.LostFocus += new System.EventHandler(this.TextBoxIndex_LostFocus);
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid.GridColor = System.Drawing.Color.SkyBlue;
            this.dataGrid.Location = new System.Drawing.Point(3, 43);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowHeadersWidth = 51;
            this.dataGrid.Size = new System.Drawing.Size(1186, 621);
            this.dataGrid.TabIndex = 1;
            this.dataGrid.Text = "dataGridView1";
            this.dataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGrid_CellClick);
            this.dataGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGrid_CellValueChanged);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(62, 24);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            // 
            // PoorEcxel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.tableLayoutPanel1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PoorEcxel";
            this.Text = "PoorExcel";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button buttonDisplay;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.TextBox textBoxExpression;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.TextBox textBoxIndex;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonEdit;
        private System.Windows.Forms.ToolStripMenuItem addColumn;
        private System.Windows.Forms.ToolStripMenuItem deleteRow;
        private System.Windows.Forms.ToolStripMenuItem deleteColumn;
        public System.Windows.Forms.ToolStripMenuItem addRow;
    }
}

