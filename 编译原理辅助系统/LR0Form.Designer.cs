namespace 编译原理辅助系统
{
    partial class LR0Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LR0Form));
            this.buttonCreateLR0AnalyzeTable = new System.Windows.Forms.Button();
            this.buttonDisplayStateInfo = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonExit = new System.Windows.Forms.Button();
            this.editBoxGrammer = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonAffirmGrammar = new System.Windows.Forms.Button();
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.buttonSaveFile = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.buttonStepByStep = new System.Windows.Forms.Button();
            this.buttonDirectlyToResult = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSentence = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.listViewAnalyseResult = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listViewStateInfo = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listViewLR0AnalyzeTable = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonDFAVisualize = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCreateLR0AnalyzeTable
            // 
            this.buttonCreateLR0AnalyzeTable.Location = new System.Drawing.Point(220, 223);
            this.buttonCreateLR0AnalyzeTable.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonCreateLR0AnalyzeTable.Name = "buttonCreateLR0AnalyzeTable";
            this.buttonCreateLR0AnalyzeTable.Size = new System.Drawing.Size(98, 23);
            this.buttonCreateLR0AnalyzeTable.TabIndex = 7;
            this.buttonCreateLR0AnalyzeTable.Text = "构造LR分析表";
            this.buttonCreateLR0AnalyzeTable.UseVisualStyleBackColor = true;
            this.buttonCreateLR0AnalyzeTable.Visible = false;
            this.buttonCreateLR0AnalyzeTable.Click += new System.EventHandler(this.createLR0AnalyzeTableButtonClick);
            // 
            // buttonDisplayStateInfo
            // 
            this.buttonDisplayStateInfo.Location = new System.Drawing.Point(16, 223);
            this.buttonDisplayStateInfo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonDisplayStateInfo.Name = "buttonDisplayStateInfo";
            this.buttonDisplayStateInfo.Size = new System.Drawing.Size(98, 23);
            this.buttonDisplayStateInfo.TabIndex = 6;
            this.buttonDisplayStateInfo.Text = "显示项目族信息";
            this.buttonDisplayStateInfo.UseVisualStyleBackColor = true;
            this.buttonDisplayStateInfo.Visible = false;
            this.buttonDisplayStateInfo.Click += new System.EventHandler(this.displayStateInfoButtonClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.buttonExit);
            this.groupBox1.Controls.Add(this.editBoxGrammer);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.buttonAffirmGrammar);
            this.groupBox1.Controls.Add(this.buttonOpenFile);
            this.groupBox1.Controls.Add(this.buttonSaveFile);
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(418, 209);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文法输入";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(281, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tips：注意将开始符号为左部的产生式规则放在首行";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(193, 21);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 23);
            this.buttonExit.TabIndex = 5;
            this.buttonExit.Text = "退出";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.exitButtonClick);
            // 
            // editBoxGrammer
            // 
            this.editBoxGrammer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editBoxGrammer.Location = new System.Drawing.Point(6, 91);
            this.editBoxGrammer.Name = "editBoxGrammer";
            this.editBoxGrammer.Size = new System.Drawing.Size(408, 112);
            this.editBoxGrammer.TabIndex = 4;
            this.editBoxGrammer.Text = "";
            this.editBoxGrammer.TextChanged += new System.EventHandler(this.grammarChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(275, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tips: 请输入形如E->abc的文法，空符号串用$表示";
            // 
            // buttonAffirmGrammar
            // 
            this.buttonAffirmGrammar.Location = new System.Drawing.Point(98, 21);
            this.buttonAffirmGrammar.Margin = new System.Windows.Forms.Padding(0);
            this.buttonAffirmGrammar.Name = "buttonAffirmGrammar";
            this.buttonAffirmGrammar.Size = new System.Drawing.Size(75, 23);
            this.buttonAffirmGrammar.TabIndex = 1;
            this.buttonAffirmGrammar.Text = "确认文法";
            this.buttonAffirmGrammar.UseVisualStyleBackColor = true;
            this.buttonAffirmGrammar.Click += new System.EventHandler(this.affirmGrammarButtonClick);
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Location = new System.Drawing.Point(6, 21);
            this.buttonOpenFile.Margin = new System.Windows.Forms.Padding(0);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenFile.TabIndex = 0;
            this.buttonOpenFile.Text = "打开文件";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.openFileButtonClick);
            // 
            // buttonSaveFile
            // 
            this.buttonSaveFile.Location = new System.Drawing.Point(295, 21);
            this.buttonSaveFile.Name = "buttonSaveFile";
            this.buttonSaveFile.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveFile.TabIndex = 2;
            this.buttonSaveFile.Text = "保存文件";
            this.buttonSaveFile.UseVisualStyleBackColor = true;
            this.buttonSaveFile.Visible = false;
            this.buttonSaveFile.Click += new System.EventHandler(this.saveFileButtonClick);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.buttonStepByStep);
            this.groupBox5.Controls.Add(this.buttonDirectlyToResult);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.textBoxSentence);
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Location = new System.Drawing.Point(434, 10);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(496, 534);
            this.groupBox5.TabIndex = 15;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "分析句子";
            // 
            // buttonStepByStep
            // 
            this.buttonStepByStep.Location = new System.Drawing.Point(100, 51);
            this.buttonStepByStep.Margin = new System.Windows.Forms.Padding(0);
            this.buttonStepByStep.Name = "buttonStepByStep";
            this.buttonStepByStep.Size = new System.Drawing.Size(75, 23);
            this.buttonStepByStep.TabIndex = 6;
            this.buttonStepByStep.Text = "单步显示";
            this.buttonStepByStep.UseVisualStyleBackColor = true;
            this.buttonStepByStep.Visible = false;
            this.buttonStepByStep.Click += new System.EventHandler(this.stepByStepButtonClick);
            // 
            // buttonDirectlyToResult
            // 
            this.buttonDirectlyToResult.Location = new System.Drawing.Point(8, 51);
            this.buttonDirectlyToResult.Margin = new System.Windows.Forms.Padding(0);
            this.buttonDirectlyToResult.Name = "buttonDirectlyToResult";
            this.buttonDirectlyToResult.Size = new System.Drawing.Size(75, 23);
            this.buttonDirectlyToResult.TabIndex = 5;
            this.buttonDirectlyToResult.Text = "一键显示";
            this.buttonDirectlyToResult.UseVisualStyleBackColor = true;
            this.buttonDirectlyToResult.Visible = false;
            this.buttonDirectlyToResult.Click += new System.EventHandler(this.directlyToResultButtonClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "待分析句子：";
            // 
            // textBoxSentence
            // 
            this.textBoxSentence.Location = new System.Drawing.Point(89, 23);
            this.textBoxSentence.Name = "textBoxSentence";
            this.textBoxSentence.Size = new System.Drawing.Size(344, 21);
            this.textBoxSentence.TabIndex = 1;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.listViewAnalyseResult);
            this.groupBox6.Location = new System.Drawing.Point(0, 80);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(496, 454);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "分析结果";
            // 
            // listViewAnalyseResult
            // 
            this.listViewAnalyseResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader8,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader9});
            this.listViewAnalyseResult.GridLines = true;
            this.listViewAnalyseResult.Location = new System.Drawing.Point(8, 20);
            this.listViewAnalyseResult.Name = "listViewAnalyseResult";
            this.listViewAnalyseResult.Size = new System.Drawing.Size(483, 428);
            this.listViewAnalyseResult.TabIndex = 2;
            this.listViewAnalyseResult.UseCompatibleStateImageBehavior = false;
            this.listViewAnalyseResult.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "步骤";
            this.columnHeader3.Width = 50;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "状态栈";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader8.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "符号栈";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "输入串";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 100;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "动作";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 160;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "GOTO";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader9.Width = 160;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listViewStateInfo);
            this.groupBox2.Location = new System.Drawing.Point(10, 251);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(173, 293);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "项目族信息";
            // 
            // listViewStateInfo
            // 
            this.listViewStateInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewStateInfo.GridLines = true;
            this.listViewStateInfo.Location = new System.Drawing.Point(5, 14);
            this.listViewStateInfo.Name = "listViewStateInfo";
            this.listViewStateInfo.Size = new System.Drawing.Size(164, 273);
            this.listViewStateInfo.TabIndex = 3;
            this.listViewStateInfo.UseCompatibleStateImageBehavior = false;
            this.listViewStateInfo.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "状态编号";
            this.columnHeader1.Width = 75;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "项目集";
            this.columnHeader2.Width = 600;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listViewLR0AnalyzeTable);
            this.groupBox3.Location = new System.Drawing.Point(188, 251);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(240, 293);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "LR0分析表";
            // 
            // listViewLR0AnalyzeTable
            // 
            this.listViewLR0AnalyzeTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7});
            this.listViewLR0AnalyzeTable.GridLines = true;
            this.listViewLR0AnalyzeTable.Location = new System.Drawing.Point(5, 14);
            this.listViewLR0AnalyzeTable.Name = "listViewLR0AnalyzeTable";
            this.listViewLR0AnalyzeTable.Size = new System.Drawing.Size(230, 274);
            this.listViewLR0AnalyzeTable.TabIndex = 4;
            this.listViewLR0AnalyzeTable.UseCompatibleStateImageBehavior = false;
            this.listViewLR0AnalyzeTable.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "状态";
            this.columnHeader7.Width = 76;
            // 
            // buttonDFAVisualize
            // 
            this.buttonDFAVisualize.Location = new System.Drawing.Point(118, 223);
            this.buttonDFAVisualize.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonDFAVisualize.Name = "buttonDFAVisualize";
            this.buttonDFAVisualize.Size = new System.Drawing.Size(98, 23);
            this.buttonDFAVisualize.TabIndex = 18;
            this.buttonDFAVisualize.Text = "可视化DFA";
            this.buttonDFAVisualize.UseVisualStyleBackColor = true;
            this.buttonDFAVisualize.Visible = false;
            this.buttonDFAVisualize.Click += new System.EventHandler(this.DFAVisualizeButtonClick);
            // 
            // LR0Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 549);
            this.Controls.Add(this.buttonDFAVisualize);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonCreateLR0AnalyzeTable);
            this.Controls.Add(this.buttonDisplayStateInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "LR0Form";
            this.Text = "LR(0)文法分析";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCreateLR0AnalyzeTable;
        private System.Windows.Forms.Button buttonDisplayStateInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.RichTextBox editBoxGrammer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonAffirmGrammar;
        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.Button buttonSaveFile;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button buttonStepByStep;
        private System.Windows.Forms.Button buttonDirectlyToResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSentence;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ListView listViewAnalyseResult;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView listViewStateInfo;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView listViewLR0AnalyzeTable;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.Button buttonDFAVisualize;
    }
}