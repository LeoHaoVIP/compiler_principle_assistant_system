namespace 编译原理辅助系统
{
    partial class XFAForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFAForm));
            this.buttonReadDFA = new System.Windows.Forms.Button();
            this.buttonSaveDFA = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonGenerateDFA = new System.Windows.Forms.Button();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.listViewMFA = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxMFAEnd = new System.Windows.Forms.TextBox();
            this.textBoxMFAStart = new System.Windows.Forms.TextBox();
            this.buttonGenerateMFA = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxDFAEnd = new System.Windows.Forms.TextBox();
            this.textBoxDFAStart = new System.Windows.Forms.TextBox();
            this.listViewDFA = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonVerify = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.editBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxNFAEnd = new System.Windows.Forms.TextBox();
            this.textBoxNFAStart = new System.Windows.Forms.TextBox();
            this.buttonSaveNFA = new System.Windows.Forms.Button();
            this.buttonGenerateNFA = new System.Windows.Forms.Button();
            this.buttonReadNFA = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listViewNFA = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonReadDFA
            // 
            this.buttonReadDFA.ForeColor = System.Drawing.Color.Black;
            this.buttonReadDFA.Location = new System.Drawing.Point(23, 579);
            this.buttonReadDFA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonReadDFA.Name = "buttonReadDFA";
            this.buttonReadDFA.Size = new System.Drawing.Size(83, 29);
            this.buttonReadDFA.TabIndex = 10;
            this.buttonReadDFA.Text = "读入DFA文件";
            this.buttonReadDFA.UseVisualStyleBackColor = true;
            this.buttonReadDFA.Click += new System.EventHandler(this.ReadXFAButtonClick);
            // 
            // buttonSaveDFA
            // 
            this.buttonSaveDFA.Location = new System.Drawing.Point(240, 579);
            this.buttonSaveDFA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSaveDFA.Name = "buttonSaveDFA";
            this.buttonSaveDFA.Size = new System.Drawing.Size(85, 29);
            this.buttonSaveDFA.TabIndex = 8;
            this.buttonSaveDFA.Text = "保存DFA";
            this.buttonSaveDFA.UseVisualStyleBackColor = true;
            this.buttonSaveDFA.Click += new System.EventHandler(this.SaveXFAButtonClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 536);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "终结状态集:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 499);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "开始状态集:";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "初始状态";
            this.columnHeader4.Width = 90;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "接受符号";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 90;
            // 
            // buttonGenerateDFA
            // 
            this.buttonGenerateDFA.Location = new System.Drawing.Point(128, 579);
            this.buttonGenerateDFA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonGenerateDFA.Name = "buttonGenerateDFA";
            this.buttonGenerateDFA.Size = new System.Drawing.Size(81, 29);
            this.buttonGenerateDFA.TabIndex = 9;
            this.buttonGenerateDFA.Text = "生成DFA";
            this.buttonGenerateDFA.UseVisualStyleBackColor = true;
            this.buttonGenerateDFA.Click += new System.EventHandler(this.GenerateXFAButtonClick);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "到达状态";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 90;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 536);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 15);
            this.label9.TabIndex = 12;
            this.label9.Text = "终结状态集:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 499);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 15);
            this.label10.TabIndex = 11;
            this.label10.Text = "开始状态集:";
            // 
            // listViewMFA
            // 
            this.listViewMFA.BackColor = System.Drawing.SystemColors.HighlightText;
            this.listViewMFA.CausesValidation = false;
            this.listViewMFA.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.listViewMFA.Cursor = System.Windows.Forms.Cursors.Default;
            this.listViewMFA.GridLines = true;
            this.listViewMFA.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewMFA.HideSelection = false;
            this.listViewMFA.LabelWrap = false;
            this.listViewMFA.Location = new System.Drawing.Point(15, 26);
            this.listViewMFA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listViewMFA.MultiSelect = false;
            this.listViewMFA.Name = "listViewMFA";
            this.listViewMFA.Size = new System.Drawing.Size(359, 442);
            this.listViewMFA.TabIndex = 1;
            this.listViewMFA.UseCompatibleStateImageBehavior = false;
            this.listViewMFA.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "初始状态";
            this.columnHeader7.Width = 90;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "接受符号";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader8.Width = 90;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "到达状态";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader9.Width = 90;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBoxMFAEnd);
            this.groupBox4.Controls.Add(this.textBoxMFAStart);
            this.groupBox4.Controls.Add(this.buttonGenerateMFA);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.listViewMFA);
            this.groupBox4.Location = new System.Drawing.Point(805, 99);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(387, 625);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "DFA->MFA";
            // 
            // textBoxMFAEnd
            // 
            this.textBoxMFAEnd.Location = new System.Drawing.Point(112, 529);
            this.textBoxMFAEnd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxMFAEnd.Name = "textBoxMFAEnd";
            this.textBoxMFAEnd.ReadOnly = true;
            this.textBoxMFAEnd.Size = new System.Drawing.Size(212, 25);
            this.textBoxMFAEnd.TabIndex = 13;
            // 
            // textBoxMFAStart
            // 
            this.textBoxMFAStart.Location = new System.Drawing.Point(112, 495);
            this.textBoxMFAStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxMFAStart.Name = "textBoxMFAStart";
            this.textBoxMFAStart.ReadOnly = true;
            this.textBoxMFAStart.Size = new System.Drawing.Size(212, 25);
            this.textBoxMFAStart.TabIndex = 12;
            // 
            // buttonGenerateMFA
            // 
            this.buttonGenerateMFA.Location = new System.Drawing.Point(128, 579);
            this.buttonGenerateMFA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonGenerateMFA.Name = "buttonGenerateMFA";
            this.buttonGenerateMFA.Size = new System.Drawing.Size(81, 29);
            this.buttonGenerateMFA.TabIndex = 14;
            this.buttonGenerateMFA.Text = "生成MFA";
            this.buttonGenerateMFA.UseVisualStyleBackColor = true;
            this.buttonGenerateMFA.Click += new System.EventHandler(this.GenerateXFAButtonClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxDFAEnd);
            this.groupBox3.Controls.Add(this.buttonReadDFA);
            this.groupBox3.Controls.Add(this.textBoxDFAStart);
            this.groupBox3.Controls.Add(this.buttonGenerateDFA);
            this.groupBox3.Controls.Add(this.buttonSaveDFA);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.listViewDFA);
            this.groupBox3.Location = new System.Drawing.Point(421, 98);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(373, 625);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "NFA->DFA";
            // 
            // textBoxDFAEnd
            // 
            this.textBoxDFAEnd.Location = new System.Drawing.Point(113, 529);
            this.textBoxDFAEnd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxDFAEnd.Name = "textBoxDFAEnd";
            this.textBoxDFAEnd.ReadOnly = true;
            this.textBoxDFAEnd.Size = new System.Drawing.Size(212, 25);
            this.textBoxDFAEnd.TabIndex = 11;
            // 
            // textBoxDFAStart
            // 
            this.textBoxDFAStart.Location = new System.Drawing.Point(113, 495);
            this.textBoxDFAStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxDFAStart.Name = "textBoxDFAStart";
            this.textBoxDFAStart.ReadOnly = true;
            this.textBoxDFAStart.Size = new System.Drawing.Size(212, 25);
            this.textBoxDFAStart.TabIndex = 10;
            // 
            // listViewDFA
            // 
            this.listViewDFA.BackColor = System.Drawing.SystemColors.HighlightText;
            this.listViewDFA.CausesValidation = false;
            this.listViewDFA.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listViewDFA.Cursor = System.Windows.Forms.Cursors.Default;
            this.listViewDFA.GridLines = true;
            this.listViewDFA.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewDFA.HideSelection = false;
            this.listViewDFA.LabelWrap = false;
            this.listViewDFA.Location = new System.Drawing.Point(13, 28);
            this.listViewDFA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listViewDFA.MultiSelect = false;
            this.listViewDFA.Name = "listViewDFA";
            this.listViewDFA.Size = new System.Drawing.Size(359, 442);
            this.listViewDFA.TabIndex = 0;
            this.listViewDFA.UseCompatibleStateImageBehavior = false;
            this.listViewDFA.View = System.Windows.Forms.View.Details;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonReset);
            this.groupBox1.Controls.Add(this.buttonExit);
            this.groupBox1.Controls.Add(this.buttonVerify);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.editBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(48, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(1103, 66);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "表达式";
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(877, 22);
            this.buttonReset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(100, 29);
            this.buttonReset.TabIndex = 5;
            this.buttonReset.Text = "重置";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.ResetButtonClick);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(995, 22);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(100, 29);
            this.buttonExit.TabIndex = 4;
            this.buttonExit.Text = "退出";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.ExitButtonClick);
            // 
            // buttonVerify
            // 
            this.buttonVerify.Location = new System.Drawing.Point(757, 22);
            this.buttonVerify.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonVerify.Name = "buttonVerify";
            this.buttonVerify.Size = new System.Drawing.Size(100, 29);
            this.buttonVerify.TabIndex = 3;
            this.buttonVerify.Text = "验证正规式";
            this.buttonVerify.UseVisualStyleBackColor = true;
            this.buttonVerify.Click += new System.EventHandler(this.VerifyButtonClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(652, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "例:（a*|b)*";
            // 
            // editBox
            // 
            this.editBox.Location = new System.Drawing.Point(164, 25);
            this.editBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.editBox.Name = "editBox";
            this.editBox.Size = new System.Drawing.Size(468, 25);
            this.editBox.TabIndex = 1;
            this.editBox.Text = "(a*|b)*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入一个正规式：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxNFAEnd);
            this.groupBox2.Controls.Add(this.textBoxNFAStart);
            this.groupBox2.Controls.Add(this.buttonSaveNFA);
            this.groupBox2.Controls.Add(this.buttonGenerateNFA);
            this.groupBox2.Controls.Add(this.buttonReadNFA);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.listViewNFA);
            this.groupBox2.Location = new System.Drawing.Point(40, 99);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(373, 625);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "正规式->NFA";
            // 
            // textBoxNFAEnd
            // 
            this.textBoxNFAEnd.Location = new System.Drawing.Point(105, 529);
            this.textBoxNFAEnd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxNFAEnd.Name = "textBoxNFAEnd";
            this.textBoxNFAEnd.ReadOnly = true;
            this.textBoxNFAEnd.Size = new System.Drawing.Size(212, 25);
            this.textBoxNFAEnd.TabIndex = 9;
            // 
            // textBoxNFAStart
            // 
            this.textBoxNFAStart.Location = new System.Drawing.Point(105, 495);
            this.textBoxNFAStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxNFAStart.Name = "textBoxNFAStart";
            this.textBoxNFAStart.ReadOnly = true;
            this.textBoxNFAStart.Size = new System.Drawing.Size(212, 25);
            this.textBoxNFAStart.TabIndex = 8;
            // 
            // buttonSaveNFA
            // 
            this.buttonSaveNFA.Location = new System.Drawing.Point(228, 579);
            this.buttonSaveNFA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSaveNFA.Name = "buttonSaveNFA";
            this.buttonSaveNFA.Size = new System.Drawing.Size(85, 29);
            this.buttonSaveNFA.TabIndex = 7;
            this.buttonSaveNFA.Text = "保存NFA";
            this.buttonSaveNFA.UseVisualStyleBackColor = true;
            this.buttonSaveNFA.Click += new System.EventHandler(this.SaveXFAButtonClick);
            // 
            // buttonGenerateNFA
            // 
            this.buttonGenerateNFA.Location = new System.Drawing.Point(123, 579);
            this.buttonGenerateNFA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonGenerateNFA.Name = "buttonGenerateNFA";
            this.buttonGenerateNFA.Size = new System.Drawing.Size(81, 29);
            this.buttonGenerateNFA.TabIndex = 6;
            this.buttonGenerateNFA.Text = "生成NFA";
            this.buttonGenerateNFA.UseVisualStyleBackColor = true;
            this.buttonGenerateNFA.Click += new System.EventHandler(this.GenerateXFAButtonClick);
            // 
            // buttonReadNFA
            // 
            this.buttonReadNFA.ForeColor = System.Drawing.Color.Black;
            this.buttonReadNFA.Location = new System.Drawing.Point(9, 579);
            this.buttonReadNFA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonReadNFA.Name = "buttonReadNFA";
            this.buttonReadNFA.Size = new System.Drawing.Size(83, 29);
            this.buttonReadNFA.TabIndex = 5;
            this.buttonReadNFA.Text = "读入NFA文件";
            this.buttonReadNFA.UseVisualStyleBackColor = true;
            this.buttonReadNFA.Click += new System.EventHandler(this.ReadXFAButtonClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 536);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "终结状态集:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 499);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "开始状态集:";
            // 
            // listViewNFA
            // 
            this.listViewNFA.BackColor = System.Drawing.SystemColors.HighlightText;
            this.listViewNFA.CausesValidation = false;
            this.listViewNFA.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewNFA.Cursor = System.Windows.Forms.Cursors.Default;
            this.listViewNFA.GridLines = true;
            this.listViewNFA.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewNFA.HideSelection = false;
            this.listViewNFA.LabelWrap = false;
            this.listViewNFA.Location = new System.Drawing.Point(7, 26);
            this.listViewNFA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listViewNFA.MultiSelect = false;
            this.listViewNFA.Name = "listViewNFA";
            this.listViewNFA.Size = new System.Drawing.Size(359, 443);
            this.listViewNFA.TabIndex = 0;
            this.listViewNFA.UseCompatibleStateImageBehavior = false;
            this.listViewNFA.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Tag = "";
            this.columnHeader1.Text = "初始状态";
            this.columnHeader1.Width = 90;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "接受符号";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 90;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "到达状态";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 90;
            // 
            // XFAForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 738);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "XFAForm";
            this.Text = "NFA_DFA_MFA";
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonReadDFA;
        private System.Windows.Forms.Button buttonSaveDFA;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button buttonGenerateDFA;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonVerify;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox editBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonSaveNFA;
        private System.Windows.Forms.Button buttonGenerateNFA;
        private System.Windows.Forms.Button buttonReadNFA;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button buttonGenerateMFA;
        private System.Windows.Forms.TextBox textBoxMFAEnd;
        private System.Windows.Forms.TextBox textBoxMFAStart;
        private System.Windows.Forms.TextBox textBoxDFAEnd;
        private System.Windows.Forms.TextBox textBoxDFAStart;
        private System.Windows.Forms.TextBox textBoxNFAEnd;
        private System.Windows.Forms.TextBox textBoxNFAStart;
        private System.Windows.Forms.ListView listViewMFA;
        private System.Windows.Forms.ListView listViewDFA;
        private System.Windows.Forms.ListView listViewNFA;
        private System.Windows.Forms.Button buttonReset;
    }
}