namespace 编译原理辅助系统
{
    partial class LL1Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LL1Form));
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.buttonAffirmGrammar = new System.Windows.Forms.Button();
            this.buttonSaveFile = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonExit = new System.Windows.Forms.Button();
            this.editBoxGrammer = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listViewFirstSet = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listViewFollowSet = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonCreatePredictionTable = new System.Windows.Forms.Button();
            this.listViewPredictionTable = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.buttonStepByStep = new System.Windows.Forms.Button();
            this.buttonDirectlyToResult = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSentence = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.listViewAnalyseResult = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonGenerateFollowSet = new System.Windows.Forms.Button();
            this.buttonGenerateFirstSet = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Location = new System.Drawing.Point(8, 26);
            this.buttonOpenFile.Margin = new System.Windows.Forms.Padding(0);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(100, 29);
            this.buttonOpenFile.TabIndex = 0;
            this.buttonOpenFile.Text = "打开文件";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.openFileButtonClick);
            // 
            // buttonAffirmGrammar
            // 
            this.buttonAffirmGrammar.Location = new System.Drawing.Point(131, 26);
            this.buttonAffirmGrammar.Margin = new System.Windows.Forms.Padding(0);
            this.buttonAffirmGrammar.Name = "buttonAffirmGrammar";
            this.buttonAffirmGrammar.Size = new System.Drawing.Size(100, 29);
            this.buttonAffirmGrammar.TabIndex = 1;
            this.buttonAffirmGrammar.Text = "确认文法";
            this.buttonAffirmGrammar.UseVisualStyleBackColor = true;
            this.buttonAffirmGrammar.Click += new System.EventHandler(this.affirmGrammarButtonClick);
            // 
            // buttonSaveFile
            // 
            this.buttonSaveFile.Location = new System.Drawing.Point(385, 26);
            this.buttonSaveFile.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSaveFile.Name = "buttonSaveFile";
            this.buttonSaveFile.Size = new System.Drawing.Size(100, 29);
            this.buttonSaveFile.TabIndex = 2;
            this.buttonSaveFile.Text = "保存文件";
            this.buttonSaveFile.UseVisualStyleBackColor = true;
            this.buttonSaveFile.Visible = false;
            this.buttonSaveFile.Click += new System.EventHandler(this.saveFileButtonClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.buttonExit);
            this.groupBox1.Controls.Add(this.editBoxGrammer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.buttonAffirmGrammar);
            this.groupBox1.Controls.Add(this.buttonOpenFile);
            this.groupBox1.Controls.Add(this.buttonSaveFile);
            this.groupBox1.Location = new System.Drawing.Point(19, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(513, 261);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文法输入";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 87);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(354, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tips：注意将开始符号为左部的产生式规则放在首行";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(259, 26);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(4);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(100, 29);
            this.buttonExit.TabIndex = 5;
            this.buttonExit.Text = "退出";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.exitButtonClick);
            // 
            // editBoxGrammer
            // 
            this.editBoxGrammer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editBoxGrammer.Location = new System.Drawing.Point(8, 114);
            this.editBoxGrammer.Margin = new System.Windows.Forms.Padding(4);
            this.editBoxGrammer.Name = "editBoxGrammer";
            this.editBoxGrammer.Size = new System.Drawing.Size(497, 139);
            this.editBoxGrammer.TabIndex = 4;
            this.editBoxGrammer.Text = "";
            this.editBoxGrammer.TextChanged += new System.EventHandler(this.grammarChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 64);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(351, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Tips: 请输入形如E->abc的文法，空符号串用$表示";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listViewFirstSet);
            this.groupBox2.Location = new System.Drawing.Point(19, 313);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(513, 197);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "First集";
            // 
            // listViewFirstSet
            // 
            this.listViewFirstSet.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.listViewFirstSet.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewFirstSet.GridLines = true;
            this.listViewFirstSet.Location = new System.Drawing.Point(8, 20);
            this.listViewFirstSet.Margin = new System.Windows.Forms.Padding(4);
            this.listViewFirstSet.Name = "listViewFirstSet";
            this.listViewFirstSet.Size = new System.Drawing.Size(497, 169);
            this.listViewFirstSet.TabIndex = 0;
            this.listViewFirstSet.UseCompatibleStateImageBehavior = false;
            this.listViewFirstSet.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "First集";
            this.columnHeader1.Width = 75;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listViewFollowSet);
            this.groupBox3.Location = new System.Drawing.Point(19, 515);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(513, 208);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Follow集";
            // 
            // listViewFollowSet
            // 
            this.listViewFollowSet.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.listViewFollowSet.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listViewFollowSet.GridLines = true;
            this.listViewFollowSet.Location = new System.Drawing.Point(8, 25);
            this.listViewFollowSet.Margin = new System.Windows.Forms.Padding(4);
            this.listViewFollowSet.Name = "listViewFollowSet";
            this.listViewFollowSet.Size = new System.Drawing.Size(497, 174);
            this.listViewFollowSet.TabIndex = 1;
            this.listViewFollowSet.UseCompatibleStateImageBehavior = false;
            this.listViewFollowSet.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Follow集";
            this.columnHeader2.Width = 75;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonCreatePredictionTable);
            this.groupBox4.Controls.Add(this.listViewPredictionTable);
            this.groupBox4.Location = new System.Drawing.Point(540, 15);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(716, 265);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "预测分析表";
            // 
            // buttonCreatePredictionTable
            // 
            this.buttonCreatePredictionTable.Location = new System.Drawing.Point(8, 23);
            this.buttonCreatePredictionTable.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCreatePredictionTable.Name = "buttonCreatePredictionTable";
            this.buttonCreatePredictionTable.Size = new System.Drawing.Size(123, 29);
            this.buttonCreatePredictionTable.TabIndex = 5;
            this.buttonCreatePredictionTable.Text = "构造预测分析表";
            this.buttonCreatePredictionTable.UseVisualStyleBackColor = true;
            this.buttonCreatePredictionTable.Visible = false;
            this.buttonCreatePredictionTable.Click += new System.EventHandler(this.createPredictionTableClick);
            // 
            // listViewPredictionTable
            // 
            this.listViewPredictionTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7});
            this.listViewPredictionTable.GridLines = true;
            this.listViewPredictionTable.Location = new System.Drawing.Point(8, 57);
            this.listViewPredictionTable.Margin = new System.Windows.Forms.Padding(4);
            this.listViewPredictionTable.Name = "listViewPredictionTable";
            this.listViewPredictionTable.Size = new System.Drawing.Size(700, 204);
            this.listViewPredictionTable.TabIndex = 1;
            this.listViewPredictionTable.UseCompatibleStateImageBehavior = false;
            this.listViewPredictionTable.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "预测分析表";
            this.columnHeader7.Width = 90;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.buttonStepByStep);
            this.groupBox5.Controls.Add(this.buttonDirectlyToResult);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.textBoxSentence);
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Location = new System.Drawing.Point(540, 284);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(716, 438);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "分析句子";
            // 
            // buttonStepByStep
            // 
            this.buttonStepByStep.Location = new System.Drawing.Point(133, 64);
            this.buttonStepByStep.Margin = new System.Windows.Forms.Padding(0);
            this.buttonStepByStep.Name = "buttonStepByStep";
            this.buttonStepByStep.Size = new System.Drawing.Size(100, 29);
            this.buttonStepByStep.TabIndex = 6;
            this.buttonStepByStep.Text = "单步显示";
            this.buttonStepByStep.UseVisualStyleBackColor = true;
            this.buttonStepByStep.Visible = false;
            this.buttonStepByStep.Click += new System.EventHandler(this.stepByStepButtonClick);
            // 
            // buttonDirectlyToResult
            // 
            this.buttonDirectlyToResult.Location = new System.Drawing.Point(11, 64);
            this.buttonDirectlyToResult.Margin = new System.Windows.Forms.Padding(0);
            this.buttonDirectlyToResult.Name = "buttonDirectlyToResult";
            this.buttonDirectlyToResult.Size = new System.Drawing.Size(100, 29);
            this.buttonDirectlyToResult.TabIndex = 5;
            this.buttonDirectlyToResult.Text = "一键显示";
            this.buttonDirectlyToResult.UseVisualStyleBackColor = true;
            this.buttonDirectlyToResult.Visible = false;
            this.buttonDirectlyToResult.Click += new System.EventHandler(this.directlyToResultButtonClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 32);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "待分析句子：";
            // 
            // textBoxSentence
            // 
            this.textBoxSentence.Location = new System.Drawing.Point(119, 29);
            this.textBoxSentence.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxSentence.Name = "textBoxSentence";
            this.textBoxSentence.Size = new System.Drawing.Size(589, 25);
            this.textBoxSentence.TabIndex = 1;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.listViewAnalyseResult);
            this.groupBox6.Location = new System.Drawing.Point(0, 100);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox6.Size = new System.Drawing.Size(744, 339);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "分析结果";
            // 
            // listViewAnalyseResult
            // 
            this.listViewAnalyseResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listViewAnalyseResult.GridLines = true;
            this.listViewAnalyseResult.Location = new System.Drawing.Point(11, 25);
            this.listViewAnalyseResult.Margin = new System.Windows.Forms.Padding(4);
            this.listViewAnalyseResult.Name = "listViewAnalyseResult";
            this.listViewAnalyseResult.Size = new System.Drawing.Size(697, 305);
            this.listViewAnalyseResult.TabIndex = 2;
            this.listViewAnalyseResult.UseCompatibleStateImageBehavior = false;
            this.listViewAnalyseResult.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "步骤";
            this.columnHeader3.Width = 50;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "符号栈";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "输入串";
            this.columnHeader5.Width = 100;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "所用产生式";
            this.columnHeader6.Width = 150;
            // 
            // buttonGenerateFollowSet
            // 
            this.buttonGenerateFollowSet.Location = new System.Drawing.Point(149, 281);
            this.buttonGenerateFollowSet.Margin = new System.Windows.Forms.Padding(0);
            this.buttonGenerateFollowSet.Name = "buttonGenerateFollowSet";
            this.buttonGenerateFollowSet.Size = new System.Drawing.Size(113, 29);
            this.buttonGenerateFollowSet.TabIndex = 8;
            this.buttonGenerateFollowSet.Text = "生成Follow集";
            this.buttonGenerateFollowSet.UseVisualStyleBackColor = true;
            this.buttonGenerateFollowSet.Visible = false;
            this.buttonGenerateFollowSet.Click += new System.EventHandler(this.generateFollowSetButtonClick);
            // 
            // buttonGenerateFirstSet
            // 
            this.buttonGenerateFirstSet.Location = new System.Drawing.Point(27, 281);
            this.buttonGenerateFirstSet.Margin = new System.Windows.Forms.Padding(0);
            this.buttonGenerateFirstSet.Name = "buttonGenerateFirstSet";
            this.buttonGenerateFirstSet.Size = new System.Drawing.Size(113, 29);
            this.buttonGenerateFirstSet.TabIndex = 7;
            this.buttonGenerateFirstSet.Text = "生成First集";
            this.buttonGenerateFirstSet.UseVisualStyleBackColor = true;
            this.buttonGenerateFirstSet.Visible = false;
            this.buttonGenerateFirstSet.Click += new System.EventHandler(this.generateFirstSetButtonClick);
            // 
            // LL1Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 726);
            this.Controls.Add(this.buttonGenerateFollowSet);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.buttonGenerateFirstSet);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LL1Form";
            this.Text = "LL(1)预测分析";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.Button buttonAffirmGrammar;
        private System.Windows.Forms.Button buttonSaveFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox editBoxGrammer;
        private System.Windows.Forms.ListView listViewFirstSet;
        private System.Windows.Forms.ListView listViewPredictionTable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSentence;
        private System.Windows.Forms.Button buttonStepByStep;
        private System.Windows.Forms.Button buttonDirectlyToResult;
        private System.Windows.Forms.ListView listViewAnalyseResult;
        private System.Windows.Forms.Button buttonGenerateFollowSet;
        private System.Windows.Forms.Button buttonGenerateFirstSet;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView listViewFollowSet;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Button buttonCreatePredictionTable;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Label label3;
    }
}