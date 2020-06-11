using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace 编译原理辅助系统
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            InitializeComponent();
        }

        private void openButtonClick(object sender, EventArgs e)    //文件打开按钮单击
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Title = "打开";
            OFD.Filter = "文本文件|*.txt";
            OFD.ShowDialog();
            String openPath = OFD.FileName; //获取打开文件的路径
            if (openPath == "")
                return;
            Program.folderPath=getFolderPath(openPath);
            FileStream openFileStream = new FileStream(openPath,FileMode.OpenOrCreate,FileAccess.Read);
            byte[] buffer=new byte[1024*1024*10];
            int sourceCodeTemp = openFileStream.Read(buffer,0,buffer.Length);
            String _sourceCode = Encoding.GetEncoding("GB2312").GetString(buffer, 0, sourceCodeTemp);
            String sourceCode = removeAnnotation(_sourceCode);
            Program.mySourceCode = sourceCode;
            textBox_souceCode.Text = _sourceCode;
            this.Text = "编译原理辅助系统";
            this.Text += " ( ";
            this.Text += openPath;
            this.Text += " [已锁定] )";
            openFileStream.Close();
        }
        //去除源程序注释
        private String removeAnnotation(String _sourceCode)
        {
            String sourceCode = "";
            foreach (String line in _sourceCode.Split('\n'))
                sourceCode += (line.Split('/', '/')[0] + "\n");
            return sourceCode;
        }
        private void saveButtonClick(object sender, EventArgs e)
        {
            String savePath =Program.folderPath+ "analyseResult.txt";
            FileStream saveFileStream = new FileStream(savePath, FileMode.Create,FileAccess.Write);
            StringBuilder SB = new StringBuilder();
            SB.Append("--------源程序\r\n");
            SB.Append(Program.mySourceCode);
            SB.Append("\r\n");
            SB.Append("--------词法分析结果\r\n");
            SB.Append(textBox_result.Text);
            SB.Append("\r\n");
            SB.Append("--------错误信息\r\n");
            SB.Append(textBox_errors.Text);
            SB.Append("\r\n");
            StreamWriter sw = new StreamWriter(saveFileStream,Encoding.GetEncoding("GB2312"));
            sw.WriteLine(SB.ToString());//写入词法分析结果（源程序+结果+错误信息）
            sw.Close();
            saveFileStream.Close();
            MessageBox.Show("文件以保存至"+savePath,"保存成功！");
        }
        private void saveAsButtonClick(object sender, EventArgs e)
        {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.Title = "保存";
            SFD.Filter = "文本文件|*.txt";
            SFD.ShowDialog();
            String savePath = SFD.FileName; //获取打开文件的路径
            if (savePath == "")
                return;
            FileStream saveFileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write);
            StringBuilder SB = new StringBuilder();
            SB.Append("--------源程序\r\n");
            SB.Append(Program.mySourceCode);
            SB.Append("\r\n");
            SB.Append("--------词法分析结果\r\n");
            SB.Append(textBox_result.Text);
            SB.Append("\r\n");
            SB.Append("--------错误信息\r\n");
            SB.Append(textBox_errors.Text);
            SB.Append("\r\n");
            //MessageBox.Show(SB.ToString());
            byte[] buffer = Encoding.GetEncoding("GB2312").GetBytes(SB.ToString());
            saveFileStream.Write(buffer, 0, buffer.Length);//写入词法分析结果（源程序+结果+错误信息）
            saveFileStream.Close();
            MessageBox.Show("文件以保存至" + savePath, "另存为成功！");
        }
        private String getFolderPath(String filePath)
        {
            String[] s = filePath.Split('\\');
            String folderPath = "";
            for (int i = 0; i < s.Length - 1; i++)
            {
                folderPath += s[i];
                folderPath += "\\";
            }
            return folderPath;
        }

        private void exitButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void analyseButtonClick(object sender, EventArgs e)
        {
            //LexicalAnalyzer LA = new LexicalAnalyzer(Program.sourceCodeArray);
            //LA.analyzeRunner();
            //textBox_result.Text = LA.analyzeResult;
            //textBox_errors.Text += LA.ErrorInfo();
            //textBox_errors.Text += LA.errorDetailInfo;
            LexicalAnalyzer.row = 1;                           //初始化行号
            LexicalAnalyzer.errorsCount = 0;                      //初始化错误个数
            LexicalAnalyzer.errorDetailInfo = "";
            Program.mySourceCode = removeAnnotation(textBox_souceCode.Text);//将源代码（当前代码框内）复制给全局变量MysourceCode
            LexicalAnalyzer getString = new LexicalAnalyzer();
            String analyzeResult = getString.CodeReader(Program.mySourceCode);
            //MessageBox.Show(Program.mySourceCode);
            //MessageBox.Show(analyzeResult);
            textBox_result.Text = "☆☆☆☆☆☆☆☆☆☆☆☆[Lex-词法分析系统]☆☆☆☆☆☆☆☆☆☆☆" + "\r\n\r\n";
            textBox_result.Text += "------------------------------------------------------\r\n";
            textBox_result.Text += "行号\t单词\t类型\t是否合法\tToken值" + "\r\n";
            textBox_result.Text += "------------------------------------------------------\r\n";
            textBox_result.Text += analyzeResult;
            textBox_errors.Text = getString.wholeErrorInfo();
            textBox_errors.Text += "\r\n";
            textBox_errors.Text += LexicalAnalyzer.errorDetailInfo;
            textBox_result.Text += "------------------------------------------------------\r\n";
            //String readme = "标识符Token值统一记为75；常量Token值统一记为76";
            //MessageBox.Show(LexicalAnalyzer.text4);
        }

        private void editButtonClick(object sender, EventArgs e)
        {
            textBox_souceCode.ReadOnly = false;
            textBox_souceCode.BackColor = Color.White;
            this.Text=this.Text.Replace("[已锁定]", "[可编辑]");
        }
        private void exitEditButtonClick(object sender, EventArgs e)
        {
            textBox_souceCode.ReadOnly = true;
            textBox_souceCode.BackColor = SystemColors.Control;
            this.Text = this.Text.Replace("[可编辑]", "[已锁定]");
        }

        private void XFAButtonClick(object sender, EventArgs e)
        {
            XFAForm xfaForm = new XFAForm();
            xfaForm.StartPosition = FormStartPosition.CenterScreen;
            xfaForm.Show();
        }

        private void aboutButtonClick(object sender, EventArgs e)
        {
            MessageBox.Show("作者：LeoHao\n学号：16111204082\n最后更改时间：2018年12月25日","关于(编译原理辅助系统)");
        }

        private void LL1PredictionButtonClick(object sender, EventArgs e)
        {
            LL1Form _LL1Form = new LL1Form();
            _LL1Form.StartPosition = FormStartPosition.CenterScreen;
            _LL1Form.Show();
        }

        private void LR0AnalyzeButtonClick(object sender, EventArgs e)
        {
            LR0Form _LR0Form = new LR0Form();
            _LR0Form.StartPosition = FormStartPosition.CenterScreen;
            _LR0Form.Show();
        }
    }
}