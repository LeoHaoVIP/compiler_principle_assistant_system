using Northwoods.Go;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;

namespace 编译原理辅助系统
{
    public partial class DFAVisualForm : Form
    {
        float intervalHorizonal = 200;//垂直间距
        float intervalVertical = 150;//水平间距
        public DFAVisualForm()
        {
            InitializeComponent();
        }
        private String[] getAllStateOfMFA(MFA myMFA)
        {
            List<String> stateList = new List<String>();
            foreach (transformFunction TF in myMFA.MFA_Transform)
            {
                if (!stateList.Contains(TF.from))
                    stateList.Add(TF.from);
                if (!stateList.Contains(TF.to))
                    stateList.Add(TF.to);
            }
            return stateList.ToArray();
        }
        //函数功能：根据stateID获取状态所在行数和列数(从1开始)
        private int[] getPositionOfState(int[] startIDArray, int stateID)
        {
            //stateID位置：列数，行数(从1开始编号)
            int[] position = new int[2];
            int column = 0;
            //计算stateID所在列数
            for (int i = 0; i < startIDArray.Length; i++)
            {
                if (startIDArray[i] > stateID)
                {
                    column = i;
                    break;
                }
            }
            //计算stateID所在行数
            int row = startIDArray[column - 1] - stateID + 1;
            return new int[2] { row, column };
        }
        private void DFA_Painter(object sender, PaintEventArgs e)
        {
            GoView myView = new GoView();
            myView.Dock = DockStyle.Fill;
            this.Controls.Add(myView);
            MFA myMFA = Program.globalMFA;
            //获取MFA的所有状态
            String[] stateSet = getAllStateOfMFA(myMFA);
            int stateNum = stateSet.Length;//椭圆的个数
            int[] startIDArray = new int[10];//每一列的开始状态编号
            startIDArray[0] = 0;
            for (int i = 1; i < 10; i++)
            {
                startIDArray[i] = startIDArray[i - 1] + i;
                //MessageBox.Show(startIDArray[i].ToString());
            }
            GoBasicNode[] myNode = new GoBasicNode[stateNum];
            for (int i = 0; i < stateNum; i++)
            {
                //创建节点
                myNode[i] = new GoBasicNode();
                myNode[i].Text = i.ToString();
                myNode[i].Height = 50;
                myNode[i].Width = 50;
                myNode[i].Editable = false;
                myNode[i].Shape.BrushColor = Color.White;
                if (i == 0)
                {
                    myNode[i].Shape.BrushColor = Color.Thistle;
                    myNode[i].Height = 55;
                    myNode[i].Width = 55;
                }
                //标记终态
                if (myMFA.MFA_END.Contains(i.ToString()))
                {
                    myNode[i].Shape.BrushColor = Color.Green;
                    myNode[i].Height = 55;
                    myNode[i].Width = 55;
                }
                //设置节点位置
                int row = getPositionOfState(startIDArray, i)[0];
                int column = getPositionOfState(startIDArray, i)[1];
                float X = 0;
                float Y = 0;
                if(row%2!=0)
                X = column * intervalHorizonal+row*20;
                else X = column * intervalHorizonal-row*20;
                if (column % 2 == 0)
                    Y = row * intervalVertical + column * 20;
                else Y = row * intervalVertical - column * 20;
                myNode[i].Position = new PointF(X, Y);
                //向GO视图中添加node控件
                myView.Document.Add(myNode[i]);
            }
            //遍历MFA转换函数，向对应两个状态之间添加连线
            foreach (transformFunction TF in myMFA.MFA_Transform)
            {
                //MessageBox.Show(TF.tranformInfo());
                int from = int.Parse(TF.from);
                String by = TF.by;
                int to = int.Parse(TF.to);
                if (from == 34)
                {
                    #region
                    GoBasicNode node = new GoBasicNode();
                    node.Size = new SizeF(0, 0);
                    node.Text = by;
                    node.Shape.BrushColor = Color.White;
                    //设置中间节点位置在from和to节点中间
                    node.Position = new PointF((myNode[from].Position.X + myNode[to].Position.X) / 2,
                        (myNode[from].Position.Y + myNode[to].Position.Y) / 2);
                    myView.Document.Add(node);
                    #endregion
                    GoLink link1 = new GoLink();
                    link1.ToArrow = false;
                    link1.FromPort = myNode[from].Port;
                    link1.ToPort = node.Port;
                    link1.Style = GoStrokeStyle.RoundedLineWithJumpOvers;
                    myView.Document.Add(link1);
                    GoLink link2 = new GoLink();
                    link2.ToArrow = true;
                    link1.Style = GoStrokeStyle.Bezier;
                    link2.FromPort = node.Port;
                    link2.ToPort = myNode[to].Port;
                    link2.Style = GoStrokeStyle.RoundedLineWithJumpOvers;
                    myView.Document.Add(link2);
                }
                else
                {
                    GoLabeledLink link = new GoLabeledLink();
                    link.ToArrow = true;
                    link.FromPort = myNode[from].Port;
                    link.ToPort = myNode[to].Port;
                    GoText text = new GoText();
                    text.Text = by;
                    link.MidLabel = text;
                    myView.Document.Add(link);
                }
            }
            GoText MFAInfoText = new GoText();
            StringBuilder SB = new StringBuilder();
            SB.Append("项目编号        项目族信息\r\n");
            for (int i = 0; i < myMFA.stateSet.Length;i++ )
            {
                SB.Append(i.ToString());
                SB.Append("                ");
                foreach (String item in myMFA.stateSet[i].state)
                {
                    SB.Append(item);
                    SB.Append(";");
                }
                SB.Append("\r\n");
            }
            SB.Append("\r\n");
            SB.Append("DFA转换函数");
            SB.Append("\r\n");
            foreach (transformFunction TF in myMFA.MFA_Transform)
            {
                SB.Append(TF.tranformInfo());
                SB.Append("\r\n");
            }
            MFAInfoText.Text = SB.ToString();
            MFAInfoText.Multiline = true;
            MFAInfoText.Bold = true;
            MFAInfoText.AutoRescales = true;
            MFAInfoText.Position = new PointF(10,50);
            myView.Document.Add(MFAInfoText);
        }
    }
}