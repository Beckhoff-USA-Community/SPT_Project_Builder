namespace TcTemplateBuilder
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            dataGridView1 = new DataGridView();
            typeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            nameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            parentDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            moduleBindingSource1 = new BindingSource(components);
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBox2 = new TextBox();
            btnAddItem = new Button();
            comboBoxType = new ComboBox();
            comboBoxParent = new ComboBox();
            moduleBindingSource = new BindingSource(components);
            btnCreateTc = new Button();
            txtBoxPath = new TextBox();
            label4 = new Label();
            label5 = new Label();
            txtBoxSln = new TextBox();
            label6 = new Label();
            txtBoxProjectName = new TextBox();
            label7 = new Label();
            txtBoxPLCName = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)moduleBindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)moduleBindingSource).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { typeDataGridViewTextBoxColumn, nameDataGridViewTextBoxColumn, parentDataGridViewTextBoxColumn });
            dataGridView1.DataSource = moduleBindingSource1;
            dataGridView1.Location = new Point(13, 56);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(837, 284);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.KeyDown += dataGridView1_KeyDown;
            // 
            // typeDataGridViewTextBoxColumn
            // 
            typeDataGridViewTextBoxColumn.DataPropertyName = "Type";
            typeDataGridViewTextBoxColumn.HeaderText = "Type";
            typeDataGridViewTextBoxColumn.MinimumWidth = 6;
            typeDataGridViewTextBoxColumn.Name = "typeDataGridViewTextBoxColumn";
            typeDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.True;
            typeDataGridViewTextBoxColumn.Width = 125;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            nameDataGridViewTextBoxColumn.HeaderText = "Name";
            nameDataGridViewTextBoxColumn.MinimumWidth = 6;
            nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            nameDataGridViewTextBoxColumn.Width = 125;
            // 
            // parentDataGridViewTextBoxColumn
            // 
            parentDataGridViewTextBoxColumn.DataPropertyName = "Parent";
            parentDataGridViewTextBoxColumn.HeaderText = "Parent";
            parentDataGridViewTextBoxColumn.MinimumWidth = 6;
            parentDataGridViewTextBoxColumn.Name = "parentDataGridViewTextBoxColumn";
            parentDataGridViewTextBoxColumn.Width = 125;
            // 
            // moduleBindingSource1
            // 
            moduleBindingSource1.DataSource = typeof(Module);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 8);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 1;
            label1.Text = "Type";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(237, 9);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 2;
            label2.Text = "Name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(552, 9);
            label3.Name = "label3";
            label3.Size = new Size(41, 15);
            label3.TabIndex = 3;
            label3.Text = "Parent";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(237, 27);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(292, 23);
            textBox2.TabIndex = 5;
            // 
            // btnAddItem
            // 
            btnAddItem.Location = new Point(779, 27);
            btnAddItem.Name = "btnAddItem";
            btnAddItem.Size = new Size(75, 23);
            btnAddItem.TabIndex = 7;
            btnAddItem.Text = "Add";
            btnAddItem.UseVisualStyleBackColor = true;
            btnAddItem.Click += btnAddItem_Click;
            // 
            // comboBoxType
            // 
            comboBoxType.FormattingEnabled = true;
            comboBoxType.Location = new Point(13, 26);
            comboBoxType.Name = "comboBoxType";
            comboBoxType.Size = new Size(143, 23);
            comboBoxType.TabIndex = 8;
            // 
            // comboBoxParent
            // 
            comboBoxParent.DataSource = moduleBindingSource;
            comboBoxParent.DisplayMember = "Parent";
            comboBoxParent.FormattingEnabled = true;
            comboBoxParent.Location = new Point(552, 27);
            comboBoxParent.Name = "comboBoxParent";
            comboBoxParent.Size = new Size(222, 23);
            comboBoxParent.TabIndex = 9;
            comboBoxParent.Click += comboBoxParent_Click;
            // 
            // btnCreateTc
            // 
            btnCreateTc.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCreateTc.Location = new Point(752, 350);
            btnCreateTc.Name = "btnCreateTc";
            btnCreateTc.Size = new Size(99, 31);
            btnCreateTc.TabIndex = 10;
            btnCreateTc.Text = "Create Tc";
            btnCreateTc.UseVisualStyleBackColor = true;
            btnCreateTc.Click += btnCreateTc_Click;
            // 
            // txtBoxPath
            // 
            txtBoxPath.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtBoxPath.Location = new Point(13, 361);
            txtBoxPath.Name = "txtBoxPath";
            txtBoxPath.Size = new Size(167, 23);
            txtBoxPath.TabIndex = 11;
            txtBoxPath.Text = "C:\\Repos\\SPT_Template";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(13, 343);
            label4.Name = "label4";
            label4.Size = new Size(31, 15);
            label4.TabIndex = 12;
            label4.Text = "Path";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Bottom;
            label5.AutoSize = true;
            label5.Location = new Point(199, 343);
            label5.Name = "label5";
            label5.Size = new Size(23, 15);
            label5.TabIndex = 13;
            label5.Text = "Sln";
            // 
            // txtBoxSln
            // 
            txtBoxSln.Anchor = AnchorStyles.Bottom;
            txtBoxSln.Location = new Point(199, 361);
            txtBoxSln.Name = "txtBoxSln";
            txtBoxSln.Size = new Size(167, 23);
            txtBoxSln.TabIndex = 14;
            txtBoxSln.Text = "TcBuilder";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Bottom;
            label6.AutoSize = true;
            label6.Location = new Point(381, 343);
            label6.Name = "label6";
            label6.Size = new Size(44, 15);
            label6.TabIndex = 15;
            label6.Text = "Project";
            // 
            // txtBoxProjectName
            // 
            txtBoxProjectName.Anchor = AnchorStyles.Bottom;
            txtBoxProjectName.Location = new Point(381, 361);
            txtBoxProjectName.Name = "txtBoxProjectName";
            txtBoxProjectName.Size = new Size(167, 23);
            txtBoxProjectName.TabIndex = 16;
            txtBoxProjectName.Text = "SPT_Template";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Bottom;
            label7.AutoSize = true;
            label7.Location = new Point(566, 343);
            label7.Name = "label7";
            label7.Size = new Size(28, 15);
            label7.TabIndex = 17;
            label7.Text = "PLC";
            // 
            // txtBoxPLCName
            // 
            txtBoxPLCName.Anchor = AnchorStyles.Bottom;
            txtBoxPLCName.Location = new Point(566, 361);
            txtBoxPLCName.Name = "txtBoxPLCName";
            txtBoxPLCName.Size = new Size(167, 23);
            txtBoxPLCName.TabIndex = 18;
            txtBoxPLCName.Text = "PLC_Template";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(861, 388);
            Controls.Add(txtBoxPLCName);
            Controls.Add(label7);
            Controls.Add(txtBoxProjectName);
            Controls.Add(label6);
            Controls.Add(txtBoxSln);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(txtBoxPath);
            Controls.Add(btnCreateTc);
            Controls.Add(comboBoxParent);
            Controls.Add(comboBoxType);
            Controls.Add(btnAddItem);
            Controls.Add(textBox2);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Name = "Form1";
            Text = "SPT Template Builder - Beta";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)moduleBindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)moduleBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBox2;
        private Button btnAddItem;
        private ComboBox comboBoxType;
        private ComboBox comboBoxParent;
        private BindingSource moduleBindingSource;
        private Button btnCreateTc;
        private TextBox txtBoxPath;
        private Label label4;
        private Label label5;
        private TextBox txtBoxSln;
        private Label label6;
        private TextBox txtBoxProjectName;
        private Label label7;
        private TextBox txtBoxPLCName;
        private BindingSource moduleBindingSource1;
        private DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn parentDataGridViewTextBoxColumn;
    }
}
