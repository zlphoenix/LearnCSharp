namespace DynamicFormEngine
{
    partial class FormPrint
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Group1", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Group2", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Group3", System.Windows.Forms.HorizontalAlignment.Right);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Item1",
            "Item1",
            "Item2"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "ItemText",
            "Code",
            "Name"}, -1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "Item3",
            "Column2",
            "Column3"}, -1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "ItemX",
            "item1",
            "item2",
            "item3"}, -1);
            this.lbText = new System.Windows.Forms.Label();
            this.entityBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dgContent = new System.Windows.Forms.DataGridView();
            this.codeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Column_Title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_Code = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnDebug = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelSummary = new System.Windows.Forms.Panel();
            this.lbSummary = new System.Windows.Forms.Label();
            this.tbxText = new System.Windows.Forms.TextBox();
            this.panelRightBorder = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panLeftBorder = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.entityBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgContent)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelSummary.SuspendLayout();
            this.panelRightBorder.SuspendLayout();
            this.panLeftBorder.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbText
            // 
            this.lbText.AutoSize = true;
            this.lbText.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.entityBindingSource, "Code", true));
            this.lbText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.entityBindingSource, "Name", true));
            this.lbText.Location = new System.Drawing.Point(8, 51);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(215, 18);
            this.lbText.TabIndex = 0;
            this.lbText.Text = "Binding visible = flase";
            this.lbText.Click += new System.EventHandler(this.lbText_Click);
            // 
            // entityBindingSource
            // 
            this.entityBindingSource.AllowNew = false;
            this.entityBindingSource.DataSource = typeof(DynamicFormEngine.Entity);
            // 
            // dgContent
            // 
            this.dgContent.AllowUserToOrderColumns = true;
            this.dgContent.AutoGenerateColumns = false;
            this.dgContent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgContent.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dgContent.BackgroundColor = System.Drawing.Color.White;
            this.dgContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgContent.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgContent.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgContent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgContent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codeDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn});
            this.dgContent.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgContent.DataSource = this.entityBindingSource;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgContent.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgContent.GridColor = System.Drawing.Color.White;
            this.dgContent.Location = new System.Drawing.Point(130, 150);
            this.dgContent.MultiSelect = false;
            this.dgContent.Name = "dgContent";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgContent.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgContent.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgContent.RowTemplate.Height = 30;
            this.dgContent.Size = new System.Drawing.Size(1014, 1021);
            this.dgContent.TabIndex = 2;
            this.dgContent.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgContent_DataBindingComplete);
            // 
            // codeDataGridViewTextBoxColumn
            // 
            this.codeDataGridViewTextBoxColumn.DataPropertyName = "Code";
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.codeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.codeDataGridViewTextBoxColumn.HeaderText = "编号";
            this.codeDataGridViewTextBoxColumn.Name = "codeDataGridViewTextBoxColumn";
            this.codeDataGridViewTextBoxColumn.Width = 80;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.Width = 80;
            // 
            // panelHeader
            // 
            this.panelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHeader.Controls.Add(this.label1);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1270, 171);
            this.panelHeader.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(595, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Header";
            // 
            // panelFooter
            // 
            this.panelFooter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFooter.Controls.Add(this.listView1);
            this.panelFooter.Controls.Add(this.btnDebug);
            this.panelFooter.Controls.Add(this.label2);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 767);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(1270, 575);
            this.panelFooter.TabIndex = 7;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Column_Title,
            this.Column_Code,
            this.Column_Name});
            this.listView1.GridLines = true;
            listViewGroup1.Header = "Group1";
            listViewGroup1.Name = "listViewGroup1";
            listViewGroup2.Header = "Group2";
            listViewGroup2.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup2.Name = "listViewGroup2";
            listViewGroup3.Header = "Group3";
            listViewGroup3.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            listViewGroup3.Name = "Group3";
            this.listView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3});
            listViewItem1.Group = listViewGroup1;
            listViewItem2.Group = listViewGroup2;
            listViewItem3.Group = listViewGroup1;
            listViewItem4.Group = listViewGroup3;
            listViewItem4.UseItemStyleForSubItems = false;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4});
            this.listView1.Location = new System.Drawing.Point(25, 148);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(781, 314);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Column_Title
            // 
            this.Column_Title.Width = 180;
            // 
            // Column_Code
            // 
            this.Column_Code.Width = 201;
            // 
            // Column_Name
            // 
            this.Column_Name.Width = 202;
            // 
            // btnDebug
            // 
            this.btnDebug.Location = new System.Drawing.Point(1020, 73);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(136, 39);
            this.btnDebug.TabIndex = 2;
            this.btnDebug.Text = "Debug";
            this.btnDebug.UseVisualStyleBackColor = true;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(565, 65);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Footer";
            // 
            // panelContent
            // 
            this.panelContent.AutoSize = true;
            this.panelContent.Controls.Add(this.dgContent);
            this.panelContent.Controls.Add(this.panelSummary);
            this.panelContent.Controls.Add(this.panelRightBorder);
            this.panelContent.Controls.Add(this.panLeftBorder);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 171);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1270, 1171);
            this.panelContent.TabIndex = 8;
            // 
            // panelSummary
            // 
            this.panelSummary.Controls.Add(this.lbSummary);
            this.panelSummary.Controls.Add(this.lbText);
            this.panelSummary.Controls.Add(this.tbxText);
            this.panelSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSummary.Location = new System.Drawing.Point(130, 0);
            this.panelSummary.Margin = new System.Windows.Forms.Padding(4);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Size = new System.Drawing.Size(1014, 150);
            this.panelSummary.TabIndex = 9;
            // 
            // lbSummary
            // 
            this.lbSummary.AutoSize = true;
            this.lbSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbSummary.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.entityBindingSource, "Code", true));
            this.lbSummary.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.entityBindingSource, "Name", true));
            this.lbSummary.Location = new System.Drawing.Point(450, 73);
            this.lbSummary.Name = "lbSummary";
            this.lbSummary.Size = new System.Drawing.Size(127, 20);
            this.lbSummary.TabIndex = 4;
            this.lbSummary.Text = "panel Summary";
            // 
            // tbxText
            // 
            this.tbxText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxText.Location = new System.Drawing.Point(10, 92);
            this.tbxText.Margin = new System.Windows.Forms.Padding(4);
            this.tbxText.Name = "tbxText";
            this.tbxText.Size = new System.Drawing.Size(231, 21);
            this.tbxText.TabIndex = 3;
            this.tbxText.Text = "Binding To Entity";
            // 
            // panelRightBorder
            // 
            this.panelRightBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRightBorder.Controls.Add(this.label4);
            this.panelRightBorder.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRightBorder.Location = new System.Drawing.Point(1144, 0);
            this.panelRightBorder.Margin = new System.Windows.Forms.Padding(4);
            this.panelRightBorder.Name = "panelRightBorder";
            this.panelRightBorder.Size = new System.Drawing.Size(126, 1171);
            this.panelRightBorder.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.entityBindingSource, "Code", true));
            this.label4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.entityBindingSource, "Name", true));
            this.label4.Location = new System.Drawing.Point(6, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 18);
            this.label4.TabIndex = 11;
            this.label4.Text = "Right Padding";
            // 
            // panLeftBorder
            // 
            this.panLeftBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panLeftBorder.Controls.Add(this.label3);
            this.panLeftBorder.Dock = System.Windows.Forms.DockStyle.Left;
            this.panLeftBorder.Location = new System.Drawing.Point(0, 0);
            this.panLeftBorder.Margin = new System.Windows.Forms.Padding(4);
            this.panLeftBorder.Name = "panLeftBorder";
            this.panLeftBorder.Size = new System.Drawing.Size(130, 1171);
            this.panLeftBorder.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.entityBindingSource, "Code", true));
            this.label3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.entityBindingSource, "Name", true));
            this.label3.Location = new System.Drawing.Point(9, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "Left Padding";
            // 
            // FormPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1270, 1342);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPrint";
            this.ShowIcon = false;
            ((System.ComponentModel.ISupportInitialize)(this.entityBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgContent)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.panelFooter.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.panelSummary.ResumeLayout(false);
            this.panelSummary.PerformLayout();
            this.panelRightBorder.ResumeLayout(false);
            this.panelRightBorder.PerformLayout();
            this.panLeftBorder.ResumeLayout(false);
            this.panLeftBorder.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbText;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.BindingSource entityBindingSource;
        private System.Windows.Forms.TextBox tbxText;
        private System.Windows.Forms.DataGridView dgContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panLeftBorder;
        private System.Windows.Forms.Panel panelRightBorder;
        private System.Windows.Forms.Panel panelSummary;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbSummary;
        private System.Windows.Forms.Button btnDebug;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader Column_Title;
        private System.Windows.Forms.ColumnHeader Column_Code;
        private System.Windows.Forms.ColumnHeader Column_Name;
    }
}

