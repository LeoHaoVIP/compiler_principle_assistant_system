namespace 编译原理辅助系统
{
    partial class mainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonSaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.buttoSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonExit = new System.Windows.Forms.ToolStripMenuItem();
            this.词法分析ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonAnalyse = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonXFA = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonLL1Prediction = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonLR0Analyze = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonExitEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_result = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox_errors = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox_souceCode = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(150, 150);
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonOpenFile,
            this.buttonSaveFile,
            this.buttoSaveAs,
            this.buttonExit});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.buttonOpenFile.Size = new System.Drawing.Size(222, 24);
            this.buttonOpenFile.Text = "打开";
            this.buttonOpenFile.Click += new System.EventHandler(this.openButtonClick);
            // 
            // buttonSaveFile
            // 
            this.buttonSaveFile.Name = "buttonSaveFile";
            this.buttonSaveFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.buttonSaveFile.Size = new System.Drawing.Size(222, 24);
            this.buttonSaveFile.Text = "保存";
            this.buttonSaveFile.Click += new System.EventHandler(this.saveButtonClick);
            // 
            // buttoSaveAs
            // 
            this.buttoSaveAs.Name = "buttoSaveAs";
            this.buttoSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.buttoSaveAs.Size = new System.Drawing.Size(222, 24);
            this.buttoSaveAs.Text = "另存为";
            this.buttoSaveAs.Click += new System.EventHandler(this.saveAsButtonClick);
            // 
            // buttonExit
            // 
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.E)));
            this.buttonExit.Size = new System.Drawing.Size(222, 24);
            this.buttonExit.Text = "退出";
            this.buttonExit.Click += new System.EventHandler(this.exitButtonClick);
            // 
            // 词法分析ToolStripMenuItem
            // 
            this.词法分析ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonAnalyse,
            this.buttonXFA,
            this.buttonLL1Prediction,
            this.buttonLR0Analyze,
            this.buttonAbout});
            this.词法分析ToolStripMenuItem.Name = "词法分析ToolStripMenuItem";
            this.词法分析ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.词法分析ToolStripMenuItem.Text = "开始";
            // 
            // buttonAnalyse
            // 
            this.buttonAnalyse.Name = "buttonAnalyse";
            this.buttonAnalyse.Size = new System.Drawing.Size(187, 24);
            this.buttonAnalyse.Text = "词法分析";
            this.buttonAnalyse.Click += new System.EventHandler(this.analyseButtonClick);
            // 
            // buttonXFA
            // 
            this.buttonXFA.Name = "buttonXFA";
            this.buttonXFA.Size = new System.Drawing.Size(187, 24);
            this.buttonXFA.Text = "NFA_DFA_MFA";
            this.buttonXFA.Click += new System.EventHandler(this.XFAButtonClick);
            // 
            // buttonLL1Prediction
            // 
            this.buttonLL1Prediction.Name = "buttonLL1Prediction";
            this.buttonLL1Prediction.Size = new System.Drawing.Size(187, 24);
            this.buttonLL1Prediction.Text = "LL(1)预测分析";
            this.buttonLL1Prediction.Click += new System.EventHandler(this.LL1PredictionButtonClick);
            // 
            // buttonLR0Analyze
            // 
            this.buttonLR0Analyze.Name = "buttonLR0Analyze";
            this.buttonLR0Analyze.Size = new System.Drawing.Size(187, 24);
            this.buttonLR0Analyze.Text = "LR(0)分析";
            this.buttonLR0Analyze.Click += new System.EventHandler(this.LR0AnalyzeButtonClick);
            // 
            // buttonAbout
            // 
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.ShowShortcutKeys = false;
            this.buttonAbout.Size = new System.Drawing.Size(187, 24);
            this.buttonAbout.Text = "关于";
            this.buttonAbout.Click += new System.EventHandler(this.aboutButtonClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.词法分析ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1367, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonEdit,
            this.buttonExitEdit});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(51, 24);
            this.toolStripMenuItem1.Text = "编辑";
            // 
            // buttonEdit
            // 
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(153, 24);
            this.buttonEdit.Text = "编辑源程序";
            this.buttonEdit.Click += new System.EventHandler(this.editButtonClick);
            // 
            // buttonExitEdit
            // 
            this.buttonExitEdit.Name = "buttonExitEdit";
            this.buttonExitEdit.Size = new System.Drawing.Size(153, 24);
            this.buttonExitEdit.Text = "退出编辑";
            this.buttonExitEdit.Click += new System.EventHandler(this.exitEditButtonClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox_result);
            this.groupBox1.Location = new System.Drawing.Point(569, 35);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(891, 362);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "词法分析结果";
            // 
            // textBox_result
            // 
            this.textBox_result.Location = new System.Drawing.Point(9, 26);
            this.textBox_result.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_result.Name = "textBox_result";
            this.textBox_result.ReadOnly = true;
            this.textBox_result.Size = new System.Drawing.Size(775, 328);
            this.textBox_result.TabIndex = 0;
            this.textBox_result.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox_errors);
            this.groupBox2.Location = new System.Drawing.Point(569, 405);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(891, 238);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "错误信息";
            // 
            // textBox_errors
            // 
            this.textBox_errors.Location = new System.Drawing.Point(9, 26);
            this.textBox_errors.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_errors.Name = "textBox_errors";
            this.textBox_errors.ReadOnly = true;
            this.textBox_errors.Size = new System.Drawing.Size(775, 183);
            this.textBox_errors.TabIndex = 0;
            this.textBox_errors.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox_souceCode);
            this.groupBox3.Location = new System.Drawing.Point(16, 35);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(537, 595);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "源程序";
            // 
            // textBox_souceCode
            // 
            this.textBox_souceCode.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_souceCode.Location = new System.Drawing.Point(9, 26);
            this.textBox_souceCode.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_souceCode.Name = "textBox_souceCode";
            this.textBox_souceCode.ReadOnly = true;
            this.textBox_souceCode.Size = new System.Drawing.Size(519, 560);
            this.textBox_souceCode.TabIndex = 0;
            this.textBox_souceCode.Text = "";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1367, 658);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "mainForm";
            this.Text = "编译原理辅助系统";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buttonOpenFile;
        private System.Windows.Forms.ToolStripMenuItem buttonSaveFile;
        private System.Windows.Forms.ToolStripMenuItem buttoSaveAs;
        private System.Windows.Forms.ToolStripMenuItem 词法分析ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buttonAnalyse;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem buttonExit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox textBox_result;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox textBox_errors;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox textBox_souceCode;
        private System.Windows.Forms.ToolStripMenuItem buttonEdit;
        private System.Windows.Forms.ToolStripMenuItem buttonExitEdit;
        private System.Windows.Forms.ToolStripMenuItem buttonXFA;
        private System.Windows.Forms.ToolStripMenuItem buttonAbout;
        private System.Windows.Forms.ToolStripMenuItem buttonLL1Prediction;
        private System.Windows.Forms.ToolStripMenuItem buttonLR0Analyze;
    }
}

