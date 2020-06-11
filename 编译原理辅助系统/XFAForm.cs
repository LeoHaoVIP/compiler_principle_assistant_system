using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace 编译原理辅助系统
{
    //注意：测试样例中的DFA是化简后的（消除了无用状态）
    public partial class XFAForm : Form
    {
        public static List<String> stateList = null;
        public XFAForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }
        //判读是否为正规式
        private String isRegularExpression(String Expression)
        {
            //括号不匹配，则非法
            if (!bracketMatched(Expression))
            {
                return "括号不匹配";
            }
            //去除括号
            Expression = Expression.Trim();
            Expression = Expression.Replace("(", "");
            Expression = Expression.Replace(")", "");
            int len = Expression.Count();
            //去除括号后，若len为0，则输入的为空表达式，是合法的
            if (len == 0)
                return null;
            //如果表达式首为*或|，或尾为|，则为非法正规式
            if (Expression[0] == '*')
            {
                return "未找到与运算符 '*' 存在联系的字符";
            }
            if (Expression[0] == '|' || Expression[len - 1] == '|')
            {
                return "未找到与运算符 '|' 存在联系的两个字符";
            }
            for (int i = 0; i < len; i++)
            {
                char c = Expression[i];
                //如果c不是字母、且不在运算符集{* |}内，则含有非法字符
                if (!isLetter(c) && !isInOperatorSet(c))
                {
                    return "含有非法字符,请输入基于小写字母的正规式！";
                }
                //*紧跟着|，非法( |* )
                if (i < len && c == '|' && Expression[i + 1] == '*')
                {
                    return "未找到与运算符 '*' 存在联系的字符";
                }
            }
            return null;
        }
        //判断表达式中括号是否匹配
        private bool bracketMatched(String expression)
        {
            Stack<char> myStack = new Stack<char>();
            foreach (char c in expression)
            {
                if (c == '(')
                    myStack.Push(c);
                else if (c == ')')
                {
                    if (myStack.Count == 0)
                    {
                        //MessageBox.Show("缺少 '(' 与 ')' 匹配", "括号不匹配");
                        return false;
                    }
                    else myStack.Pop();
                }
            }
            if (myStack.Count != 0)
            {
                //MessageBox.Show("缺少 ')' 与 '(' 匹配", "括号不匹配");
                return false;
            }
            return true;
        }
        //判断字符是否在运算符集{* |}中
        private bool isInOperatorSet(char c)
        {
            if (c == '*' || c == '|')
                return true;
            return false;
        }
        //判断字符c是否为字母（空符号串'$'作为非法字符处理）
        private bool isLetter(char c)
        {
            if ((c >= 'a' && c <= 'z'))
                return true;
            return false;
        }
        private void VerifyButtonClick(object sender, EventArgs e)
        {
            String expression = editBox.Text.Replace(" ", "");//去除输入框内的空格
            String scannerResult = isRegularExpression(expression);
            if (expression == "" || scannerResult == null)
                MessageBox.Show("合法正规式", "提示");
            else MessageBox.Show(scannerResult, "非法正规式");

        }
        private void ExitButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ReadXFAButtonClick(object sender, EventArgs e)
        {
            String line = "notNULL";
            String[] textItem = new String[3];
            String start = null;
            String end = null;
            OpenFileDialog OFD = new OpenFileDialog();
            switch (((Button)sender).Name)
            {
                case "buttonReadNFA":
                    OFD.Title = "打开NFA文件";
                    OFD.Filter = "NFA File|*.nfa;*.txt";
                    break;
                case "buttonReadDFA":
                    OFD.Title = "打开DFA文件";
                    OFD.Filter = "DFA File|*.dfa;*.txt";
                    break;
                default: break;
            }
            OFD.ShowDialog();
            String openPath = OFD.FileName; //获取打开文件的路径
            if (openPath == "")
                return;
            StreamReader sr = new StreamReader(openPath, Encoding.Default);
            //Trim(';')的目的是去除最后的分号，防止split(';')后出现空字符串
            start = sr.ReadLine().Trim(' ', ';').Replace("开始符:", "");
            end = sr.ReadLine().Trim(' ', ';').Replace("终结符:", "");
            try
            {
                switch (((Button)sender).Name)
                {
                    case "buttonReadNFA":
                        //获取NFA初态和终态集
                        Program.globalNFA.NFA_START = start.Split(';');
                        Program.globalNFA.NFA_END = end.Split(';');
                        textBoxNFAStart.Text = start + ";";
                        textBoxNFAEnd.Text = end + ";";
                        String nfaInput = sr.ReadLine().Trim(' ', ';').Replace("符号集:", "");
                        //NFA输入符号集合
                        Program.globalNFA.NFA_INPUT = nfaInput.Split(';');
                        listViewNFA.Items.Clear();//清除当前项目
                        while (line != null)
                        {
                            line = sr.ReadLine();
                            if (line == null)
                                break;
                            line=line.Replace("#","$");
                            textItem = line.Split('\t');
                            ListViewItem item = new ListViewItem();
                            item.Text = textItem[0];
                            item.SubItems.Add(textItem[1]);
                            item.SubItems.Add(textItem[2]);
                            listViewNFA.Items.Add(item);
                        }
                        sr.Close();
                        updateXFATransformFunction();
                        break;
                    case "buttonReadDFA":
                        //DFA最大状态数
                        //MessageBox.Show(sr.ReadLine().Replace("最大状态数:", ""));
                        //获取DFA初态集和终态集
                        Program.globalDFA.DFA_START = start.Split(';');
                        Program.globalDFA.DFA_END = end.Split(';');
                        int maxState = int.Parse(sr.ReadLine().Replace("最大状态数:", ""));
                        //赋值Program.cs中最大状态数
                        Program.globalDFA.DFA_maxState = maxState;
                        String dfaInput = sr.ReadLine().Trim(' ', ';').Replace("符号集:", "");
                        //DFA输入符号集合
                        Program.globalDFA.DFA_INPUT = dfaInput.Split(';');
                        textBoxDFAStart.Text = start + ";";
                        textBoxDFAEnd.Text = end + ";";

                        listViewDFA.Items.Clear();//清除当前项目
                        while (line != null)
                        {
                            line = sr.ReadLine();
                            if (line == null)
                                break;
                            line=line.Replace("#", "$");
                            textItem = line.Split('\t');
                            ListViewItem item = new ListViewItem();
                            item.Text = textItem[0];
                            item.SubItems.Add(textItem[1]);
                            item.SubItems.Add(textItem[2]);
                            listViewDFA.Items.Add(item);
                        }
                        sr.Close();
                        updateXFATransformFunction();
                        break;
                    default: break;
                }
            }
            catch (Exception) { }
        }
        private void GenerateXFAButtonClick(object sender, EventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "buttonGenerateNFA":
                    REtoNFA();
                    //MessageBox.Show("buttonGenerateNFA");
                    break;
                case "buttonGenerateDFA":
                    NFAToDFA();
                    //MessageBox.Show("buttonGenerateDFA");
                    break;
                case "buttonGenerateMFA":
                    DFAToMFA();
                    //MessageBox.Show("buttonGenerateMFA");
                    break;
                default: break;
            }
        }
        private void SaveXFAButtonClick(object sender, EventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "buttonSaveNFA":
                    if (listViewNFA.Items.Count <= 0)
                    {
                        MessageBox.Show("无法保存NFA，请先生成或读入NFA", "提示");
                        return;
                    }
                    SaveFileDialog SFD = new SaveFileDialog();
                    SFD.Title = "保存";
                    SFD.Filter = "NFA File|*.nfa;*.txt";
                    SFD.ShowDialog();
                    String savePath = SFD.FileName; //获取打开文件的路径
                    if (savePath == "")
                        return;
                    FileStream saveFileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write);
                    StringBuilder SB = new StringBuilder();
                    SB.Append("开始符:" + textBoxNFAStart.Text + "\n");
                    SB.Append("终结符:" + textBoxNFAEnd.Text + "\n");
                    SB.Append("符号集:");
                    foreach (String inputChar in Program.globalNFA.NFA_INPUT)
                        SB.Append(inputChar + ";");
                    SB.Append("\n");
                    int num = listViewNFA.Items.Count;
                    for (int i = 0; i < num; i++)
                    {
                        SB.Append(listViewNFA.Items[i].SubItems[0].Text);
                        SB.Append("\t");
                        SB.Append(listViewNFA.Items[i].SubItems[1].Text);
                        SB.Append("\t");
                        SB.Append(listViewNFA.Items[i].SubItems[2].Text);
                        SB.Append("\n");
                    }
                    byte[] buffer = Encoding.GetEncoding("GB2312").GetBytes(SB.ToString());
                    saveFileStream.Write(buffer, 0, buffer.Length);//写入DFA信息
                    saveFileStream.Close();
                    MessageBox.Show("文件以保存至" + savePath);
                    break;
                case "buttonSaveDFA":
                    if (listViewDFA.Items.Count <= 0)
                    {
                        MessageBox.Show("无法保存DFA，请先生成或读入DFA", "提示");
                        return;
                    }
                    SFD = new SaveFileDialog();
                    SFD.Title = "保存";
                    SFD.Filter = "DFA File|*.dfa;*.txt";
                    SFD.ShowDialog();
                    savePath = SFD.FileName; //获取打开文件的路径
                    if (savePath == "")
                        return;
                    saveFileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write);
                    SB = new StringBuilder();
                    SB.Append("开始符:" + textBoxDFAStart.Text + "\n");
                    SB.Append("终结符:" + textBoxDFAEnd.Text + "\n");
                    SB.Append("最大状态数:" + Program.globalDFA.DFA_maxState.ToString() + "\n");
                    SB.Append("符号集:");
                    foreach (String inputChar in Program.globalDFA.DFA_INPUT)
                        SB.Append(inputChar + ";");
                    SB.Append("\n");
                    num = listViewDFA.Items.Count;
                    for (int i = 0; i < num; i++)
                    {
                        SB.Append(listViewDFA.Items[i].SubItems[0].Text);
                        SB.Append("\t");
                        SB.Append(listViewDFA.Items[i].SubItems[1].Text);
                        SB.Append("\t");
                        SB.Append(listViewDFA.Items[i].SubItems[2].Text);
                        SB.Append("\n");
                    }
                    buffer = Encoding.GetEncoding("GB2312").GetBytes(SB.ToString());
                    saveFileStream.Write(buffer, 0, buffer.Length);//写入DFA信息
                    saveFileStream.Close();
                    MessageBox.Show("文件以保存至" + savePath);
                    break;
                default: break;
            }
        }
        //函数功能：将正规式r(RegularExpression)转化为等价的NFA
        private void REtoNFA()
        {
            //去除输入的空格
            String Expression = editBox.Text.Replace(" ", "");
            if (isRegularExpression(Expression) != null)
            {
                MessageBox.Show("正规式非法，请检查后重新输入！", "提示");
                return;
            }
            if (Expression == "")
            {
                MessageBox.Show("无法生成NFA，请先输入正规式", "提示");
                return;
            }
            NFA myNFA = REtoNFA_Helper.REtoNFA(Expression);
            //MessageBox.Show(myNFA.NFAInfo());
            //赋值Program.cs相关变量
            Program.globalNFA.NFA_INPUT = myNFA.NFA_INPUT;
            Program.globalNFA.NFA_START = myNFA.NFA_START;
            Program.globalNFA.NFA_END = myNFA.NFA_END;
            Program.globalNFA.NFA_Transform = myNFA.NFA_Transform;
            //NFA转化函数显示
            listViewNFA.Items.Clear();//清除当前项目
            foreach (transformFunction TFItem in myNFA.NFA_Transform)
            {
                ListViewItem item = new ListViewItem();
                item.Text = TFItem.from;
                item.SubItems.Add(TFItem.by);
                item.SubItems.Add(TFItem.to);
                listViewNFA.Items.Add(item);
            }
            //显示初态集
            String startState = "";
            foreach (String item in myNFA.NFA_START)
                startState = startState + item + ";";
            textBoxNFAStart.Text = startState;
            //显示终态集
            String endState = "";
            foreach (String item in myNFA.NFA_END)
                endState = endState + item + ";";
            textBoxNFAEnd.Text = endState;
        }
        //函数功能：将NFA转化为等价的DFA(同时更新全局MFA)
        private void NFAToDFA()
        {
            int num = listViewNFA.Items.Count;
            if (num <= 0)
            {
                MessageBox.Show("未检测到NFA，请先生成或读入NFA", "提示");
                return;
            }
            //MessageBox.Show(num.ToString());
            //读入NFA转换函数
            updateXFATransformFunction();
            //从Program.cs中获取NFA转换函数
            transformFunction[] NFA_Transform = Program.globalNFA.NFA_Transform;
            //通过NFA生成DFA的状态集(已标记标识符)
            List<stateSet_DFA> DFASet = getSubSet(Program.globalNFA.NFA_Transform);
            //testFinalSet(DFASet);
            //DFA初态即为DFA第一个状态集的标识符（即K0闭包）
            Program.globalDFA.DFA_START = new String[] { DFASet[0].ID };
            //最大状态数(最后一个状态)
            Program.globalDFA.DFA_maxState = int.Parse(DFASet[DFASet.Count() - 1].ID);
            //DFA与NFA输入字母表相同
            Program.globalDFA.DFA_INPUT = Program.globalNFA.NFA_INPUT;
            //DFA转换函数列表
            List<transformFunction> transfromFList = new List<transformFunction>();
            //终结符集
            List<String> DFA_END = new List<String>();

            foreach (stateSet_DFA item1 in DFASet)
            {
                //在遍历的过程中寻找DFA终态
                //与NFA终态集相交的状态集的标识符为DFA终态（不一定唯一）
                //if (isExistIntersection(item1.state, Program.globalNFA.NFA_END)&&!StringArrayEqual(item1.state,DFASet[0].state))//不为初态
                if (isExistIntersection(item1.state, Program.globalNFA.NFA_END))//可为初态
                    DFA_END.Add(item1.ID);
                foreach (stateSet_DFA item2 in DFASet)
                    foreach (String inputChar in Program.globalNFA.NFA_INPUT)
                    {
                        String[] eC = epsilonClouse(aArcMove(inputChar, item1.state, NFA_Transform).ToArray(), NFA_Transform).ToArray();
                        //若item1的inputChar闭包等于item2，则为DFA添加一条转换函数
                        if (StringArrayEqual(eC, item2.state))
                        {
                            transfromFList.Add(new transformFunction(item1.ID, inputChar, item2.ID));
                        }
                    }
            }
            //赋值全局变量
            Program.globalDFA.DFA_Transform = transfromFList.ToArray();
            Program.globalDFA.DFA_END = DFA_END.ToArray();
            //DFA转化函数显示
            listViewDFA.Items.Clear();//清除当前项目
            foreach (transformFunction TFItem in transfromFList)
            {
                ListViewItem item = new ListViewItem();
                item.Text = TFItem.from;
                item.SubItems.Add(TFItem.by);
                item.SubItems.Add(TFItem.to);
                listViewDFA.Items.Add(item);
            }
            //显示初态集
            String startState = "";
            foreach (String item in Program.globalDFA.DFA_START)
                startState = startState + item + ";";
            textBoxDFAStart.Text = startState;
            //显示终态集
            String endState = "";
            foreach (String item in Program.globalDFA.DFA_END)
                endState = endState + item + ";";
            textBoxDFAEnd.Text = endState;
            updateXFATransformFunction();
        }
        //函数功能：将DFA化简为MFA
        private void DFAToMFA()
        {
            if (listViewDFA.Items.Count <= 0)
            {
                MessageBox.Show("无法生成MFA，请先生成或读入DFA", "提示");
                return;
            }
            //消除无用状态（转换函数）
            Program.globalMFA.MFA_Transform = removeUselessState(Program.globalMFA.MFA_Transform, Program.globalMFA.MFA_START);
            //消除多余状态（转换函数）
            //Program.globalMFA.MFA_Transform = removeExtraState(Program.globalMFA.MFA_Transform, Program.globalMFA.MFA_START, Program.globalMFA.MFA_END,
            //    Program.globalMFA.MFA_INPUT);
            //规范化MFA
            //对生成的MFA的状态进行重新编号，同时更新转换函数、初态集、终态集
            Program.globalMFA = normalizeMFA(Program.globalMFA);
            //DFA转化函数显示
            listViewMFA.Items.Clear();//清除当前项目
            foreach (transformFunction TFItem in Program.globalMFA.MFA_Transform)
            {
                ListViewItem item = new ListViewItem();
                item.Text = TFItem.from; ;
                item.SubItems.Add(TFItem.by);
                item.SubItems.Add(TFItem.to);
                listViewMFA.Items.Add(item);
            }
            //显示初态集
            String startState = "";
            foreach (String item in Program.globalMFA.MFA_START)
                startState = startState + item + ";";
            textBoxMFAStart.Text = startState;
            //显示终态集
            String endState = "";
            foreach (String item in Program.globalMFA.MFA_END)
                endState = endState + item + ";";
            textBoxMFAEnd.Text = endState;
        }
        //消除DFA转换函数集TF中的无用状态，返回消除后的转换函数集合
        private transformFunction[] removeUselessState(transformFunction[] TF, String[] startState)
        {
            //最终保留的转换函数集合
            List<transformFunction> savedTF = new List<transformFunction>();
            foreach (transformFunction item in TF)
            {
                //只保留从开始状态***以及目标状态***出发，能够达到终态的转换函数(注意：课本例题考虑的不全面，未考虑目标状态)
                foreach (String endState in Program.globalDFA.DFA_END)
                    if (isReachable(item.from, endState, TF, Program.globalDFA.DFA_maxState)
                        && isReachable(item.to, endState, TF, Program.globalDFA.DFA_maxState))
                    {
                        savedTF.Add(item);
                        break;
                    }
            }
            return savedTF.ToArray();
        }
        //判断任意两个状态是否可达(参数：状态1、状态2、转换函数、最大状态)
        private bool isReachable(String state1, String state2, transformFunction[] TF, int maxState)
        {
            int stateNum = maxState + 1;
            //可达矩阵
            bool[,] reachableMatrix = new bool[stateNum, stateNum];
            for (int i = 0; i < stateNum; i++)
                for (int j = 0; j < stateNum; j++)
                {
                    reachableMatrix[i, j] = false;
                    //自身到自身状态可达
                    if (i == j)
                        reachableMatrix[i, j] = true;
                }
            //构建初始时的可达表
            foreach (transformFunction item in TF)
            {
                int i = int.Parse(item.from);
                int j = int.Parse(item.to);
                reachableMatrix[i, j] = true;
            }
            //在初始化后的可达表上延伸
            for (int i = 0; i < stateNum; i++)
                for (int j = 0; j < stateNum; j++)
                    for (int k = 0; k < stateNum; k++)
                        if (reachableMatrix[i, k] && reachableMatrix[k, j])
                            reachableMatrix[i, j] = true;
            //根据可达矩阵返回state1与state2是否可达
            return reachableMatrix[int.Parse(state1), int.Parse(state2)];
        }
        //消除转换函数集TF中的多余状态，返回消除后的转换函数集合（TODO:函数体未完成20181030）
        private transformFunction[] removeExtraState(transformFunction[] TF, String[] startState, String[] endState,String[] inputChar)
        {
            //最终保留的转换函数集合
            List<transformFunction> savedTF = new List<transformFunction>();
            //生成DFA状态集
            List<String> stateSet = new List<String>();
            foreach (transformFunction item in TF)
            {
                String from = item.from;
                String to = item.to;
                if (!stateSet.Contains(from))
                    stateSet.Add(from);
                if (!stateSet.Contains(to))
                    stateSet.Add(to);
            }
            int stateNum = stateSet.Count();
            //MessageBox.Show(stateNum.ToString());
            int[] classID = new int[stateNum];
            //初始化非终态类别为0，终态类别为1
            for (int i = 0; i < stateNum; i++)
            {
                classID[i] = 0;
                if (endState.Contains(stateSet[i]))
                    classID[i] = 1;
            }
            //MessageBox.Show(separatingFinished(TF, classID, stateSet.ToArray(), inputChar).ToString());
            //如果划分没有结束，则继续循环
            while (!separatingFinished(TF, classID, stateSet.ToArray(), inputChar))
            {
                //按照类别划分状态集合
                List<int> Class = new List<int>();
                foreach (int item in classID)
                    if (!Class.Contains(item))
                        Class.Add(item);
                foreach (int item in Class)
                {
                    //同一类别的状态集合
                    List<String> fromState = new List<String>();
                    for (int i = 0; i < stateSet.Count(); i++)
                        if (classID[i] == item)
                            fromState.Add(stateSet[i]);

                }
            }
            return TF;
        }
        //函数功能：判断集合分隔是否完成（不可再分则返回true，否则返回false）(消除多余状态的辅助函数)
        //输入参数：转换函数-TF、状态集各个状态所属类别-classID、DFA状态集-stateSet、DFA输入符号
        private bool separatingFinished(transformFunction[] TF, int[] classID, String[] stateSet, String[] inputChar)
        {
            //检索是否存在属于相同类别的状态，经过任意输入符号，到达的新状态不在该类别中。
            //如果存在，则将该类别的状态继续划分；否则类别划分完成，返回true
            //按照类别划分状态集合
            List<int> Class = new List<int>();
            foreach (int item in classID)
                if (!Class.Contains(item))
                    Class.Add(item);
            foreach (int item in Class)
            {
                //同一类别的状态集合
                List<String> fromState = new List<String>();
                for (int i = 0; i < stateSet.Count(); i++)
                    if (classID[i] == item)
                        fromState.Add(stateSet[i]);
                //MessageBox.Show("类别："+item.ToString());
                //StringArrayPrint(fromState.ToArray());
                //如果fromState集合中只有一个元素，则不必再划分，因此只考虑fromState.Count()>1的情况
                if(fromState.Count()>1)
                //生成同一类别状态经过特定输入符号到达的目标状态
                foreach (String iC in inputChar)
                {
                    //同类别状态经过同一输入符号，到达的目标状态集
                    List<String> objectState = new List<String>();
                    //同类别状态经过同一输入符号，到达的目标状态所属类别ID集
                    List<int> objectID = new List<int>();
                    //对于同一类别的状态集中的每一个状态
                    foreach (String from in fromState)
                    {
                        foreach (transformFunction tf in TF)
                        {
                            //添加新的可达目标状态入目标状态集objectState
                            if (tf[from, iC] != null)
                                objectState.Add(tf[from, iC]);
                        }
                    }
                    //MessageBox.Show("类别：" + item.ToString()+"; 输入符号："+iC);
                    //StringArrayPrint(objectState.ToArray());
                    //获取目标状态集所属类别ID
                    for (int j = 0; j < objectState.Count(); j++)
                        objectID.Add(classID[findInStringArray(stateSet, objectState[j])]);
                    //对objectID中的元素去重
                    List<int> newObjectID = new List<int>();
                    foreach (int itemID in objectID)
                        if (!newObjectID.Contains(itemID))
                            newObjectID.Add(itemID);
                    //若来自同一类别状态的目标状态类别不同，则划分尚未完成
                    if (newObjectID.Count() > 1)
                        return false;
                }
            }
            return true;
        }
        //函数功能：对生成的MFA的状态进行重新编号，同时更新转换函数、初态集、终态集
        private MFA normalizeMFA(MFA mfa)
        {
            //生成MFA状态集
            List<String> stateSet = new List<String>();
            foreach (transformFunction item in mfa.MFA_Transform)
            {
                String from = item.from;
                String to = item.to;
                if (!stateSet.Contains(from))
                    stateSet.Add(from);
                if (!stateSet.Contains(to))
                    stateSet.Add(to);
            }
            //对状态集按照ID从小到大排序(该句也可省略)
            stateSet = StringArraySort(stateSet.ToArray()).ToList();
            int stateNum = stateSet.Count();
            //构建新的状态集合
            List<String> newStateSet = new List<String>();
            for (int i = 0; i < stateNum; i++)
                newStateSet.Add(i.ToString());
            //更新状态转换函数、初态集、终态集
            for (int i = 0; i < stateNum;i++ )
            {
                foreach (transformFunction TF in mfa.MFA_Transform)
                {
                    if (TF.from == stateSet[i])
                        TF.from = newStateSet[i];
                    if (TF.to==stateSet[i])
                        TF.to = newStateSet[i];
                }
                for (int j = 0; j < mfa.MFA_START.Length; j++)
                {
                    if (mfa.MFA_START[j]==stateSet[i])
                        mfa.MFA_START[j] = newStateSet[i];
                }
                for (int j = 0; j < mfa.MFA_END.Length; j++)
                {
                    if (mfa.MFA_END[j] == stateSet[i])
                        mfa.MFA_END[j] = newStateSet[i];
                }
            }
            return mfa;
        }
        //字符串数组输出测试
        private void StringArrayPrint(String[] StringArray)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("数组长度："+StringArray.Length.ToString()+"\n");
            foreach (String item in StringArray)
                sb.Append(item+"\n");
            MessageBox.Show(sb.ToString());
        }
        //函数功能：在字符串数组中查找指定字符串的位置下标
        private int findInStringArray(String[] StringArray,String s)
        {
            for (int i = 0; i < StringArray.Length; i++)
                if (StringArray[i].Equals(s))
                    return i;
            return -1;
        }
        //函数功能：更新Program.cs中NFA、DFA转换函数(保证Program.cs中保存当前最新转换函数)
        private void updateXFATransformFunction()
        {
            int NFA_NUM = listViewNFA.Items.Count;
            int DFA_NUM = listViewDFA.Items.Count;
            transformFunction[] NFA_Transfrom = new transformFunction[NFA_NUM];
            transformFunction[] DFA_Transfrom = new transformFunction[DFA_NUM];
            for (int i = 0; i < NFA_NUM; i++)
            {
                //每一个对象都需要初始化
                NFA_Transfrom[i] = new transformFunction();
                NFA_Transfrom[i].from = listViewNFA.Items[i].SubItems[0].Text;
                NFA_Transfrom[i].by = listViewNFA.Items[i].SubItems[1].Text;
                NFA_Transfrom[i].to = listViewNFA.Items[i].SubItems[2].Text;
            }
            for (int i = 0; i < DFA_NUM; i++)
            {
                //每一个对象都需要初始化
                DFA_Transfrom[i] = new transformFunction();
                DFA_Transfrom[i].from = listViewDFA.Items[i].SubItems[0].Text;
                DFA_Transfrom[i].by = listViewDFA.Items[i].SubItems[1].Text;
                DFA_Transfrom[i].to = listViewDFA.Items[i].SubItems[2].Text;
            }
            //赋值全局变量DFA_Transform
            Program.globalNFA.NFA_Transform = NFA_Transfrom;
            Program.globalDFA.DFA_Transform = DFA_Transfrom;
            //以下代码为将当前全局DFA赋值给全局MFA
            Program.globalMFA.MFA_Transform = Program.globalDFA.DFA_Transform;
            Program.globalMFA.MFA_START = Program.globalDFA.DFA_START;
            Program.globalMFA.MFA_END = Program.globalDFA.DFA_END;
            Program.globalMFA.MFA_INPUT = Program.globalDFA.DFA_INPUT;
        }
        //NFA中，状态集合I的a弧转换运算,返回状态集I的a弧转换集合
        private List<String> aArcMove(String a, String[] I, transformFunction[] TF)
        {
            List<String> stateList = new List<String>();
            for (int i = 0; i < I.Count(); i++)
            {
                foreach (transformFunction transformming in TF)
                {
                    if (transformming.from == I[i] && transformming.by == a)
                    {
                        String objectState = transformming.to;
                        //保证加入的元素不重复
                        if (!stateList.Contains(objectState))
                            stateList.Add(objectState);
                    }
                }
            }
            return stateList;
        }
        //状态集合I的epsilon-闭包运算,返回状态集I的epsilon-闭包集合
        private List<String> epsilonClouse(String[] I, transformFunction[] TF)
        {
            stateList = new List<String>();
            for (int i = 0; i < I.Count(); i++)
            {
                if (!stateList.Contains(I[i]))
                    stateList.Add(I[i]);
            }
            _epsilonClouse(I, TF);//执行闭包计算辅助函数
            return stateList;
        }
        //状态集合I的epsilon-闭包辅助运算(不包含原状态集I)，对全局变量stateList进行修改
        private void _epsilonClouse(String[] I, transformFunction[] TF)
        {
            //List<String> stateList = new List<String>();
            for (int i = 0; i < I.Count(); i++)
            {
                foreach (transformFunction transformming in TF)
                {
                    if (transformming.from == I[i] && transformming.by == "$")
                    {
                        String objectState = transformming.to;
                        //保证加入的元素不重复
                        if (!stateList.Contains(objectState))
                        {
                            stateList.Add(objectState);
                            _epsilonClouse(new String[] { objectState }, TF);
                        }
                    }
                }
            }
        }
        //通过NFA构造DFA状态子集subSet
        private List<stateSet_DFA> getSubSet(transformFunction[] TF)
        {
            List<stateSet_DFA> finalSet = new List<stateSet_DFA>();
            String[] myStateSet = epsilonClouse(Program.globalNFA.NFA_START, TF).ToArray();
            //定义一个stateSet_DFA，标记位设为0
            stateSet_DFA stateSet = new stateSet_DFA(myStateSet, 0);
            //将epsilonClouse(K0)作为subSet唯一成员
            finalSet.Add(stateSet);

            while (findInFinalSet(finalSet) != null)
            {
                //未被标记的item集
                stateSet_DFA item = findInFinalSet(finalSet);
                //标记item集
                finalSet.Remove(item);
                item.marked = 1;
                finalSet.Add(item);
                foreach (String inputChar in Program.globalNFA.NFA_INPUT)
                {
                    //求出item.state状态集中，所有状态inputChar弧转换的epsilon-闭包
                    myStateSet = epsilonClouse(aArcMove(inputChar, item.state, TF).ToArray(), TF).ToArray();
                    stateSet_DFA newStateSet = new stateSet_DFA(myStateSet, 0);
                    if (!isExistInFinalSet(newStateSet.state, finalSet))
                        finalSet.Add(newStateSet);
                }
            }
            int ID = 0;
            foreach (stateSet_DFA item in finalSet)
            {
                item.state=StringArraySort(item.state);
                //标记状态集的标识符
                item.ID = ID.ToString();
                ID++;
            }
            return finalSet;
        }
        //finalSet输出测试
        private void testFinalSet(List<stateSet_DFA> finalSet)
        {
            String whole = "生成的finalSet如下\n";
            foreach (stateSet_DFA item in finalSet)
            {
                String str = "marked: " + item.marked.ToString() + " - ; ";
                str += "ID: " + item.ID.ToString() + "\t";
                foreach (String s in item.state)
                    str = str + " " + s;
                whole = whole + str + "\n";
            }
            MessageBox.Show(whole);
        }
        //判断finalSet中是否包含尚未被标记的子集，返回当前未被标记的子集（全都已标记则返回null）
        private stateSet_DFA findInFinalSet(List<stateSet_DFA> finalSet)
        {
            foreach (stateSet_DFA item in finalSet)
                if (item.marked == 0)
                    return item;
            return null;
        }
        //判断某状态集stateSet是否在finalSet集合中
        private bool isExistInFinalSet(String[] stateSet, List<stateSet_DFA> finalSet)
        {
            foreach (stateSet_DFA item in finalSet)
            {
                String[] temp = item.state;
                if (StringArrayEqual(stateSet, temp))
                    return true;
            }
            return false;
        }
        //判断字符串数组A和B是否相等
        private bool StringArrayEqual(String[] A, String[] B)
        {
            A=StringArraySort(A);
            B=StringArraySort(B);
            if (A.Count() != B.Count())
                return false;
            int flag = 0;
            for (int i = 0; i < A.Count(); i++)
                if (A[i] != B[i])
                    flag = 1;
            return (flag == 0);
        }
        //字符串数组排序(以整数作为元素)
        private String[] StringArraySort(String[] A)
        {
            for (int i = 1; i < A.Count(); i++)
            {
                for (int j = 0; j < A.Count() - i; j++)
                {
                    if (int.Parse(A[j]) > int.Parse(A[j + 1]))
                    {
                        String t = A[j];
                        A[j] = A[j + 1];
                        A[j + 1] = t;
                    }
                }
            }
            return A;
        }
        //判断集合是否相交（即是否存在重复元素）
        private bool isExistIntersection(String[] A, String[] B)
        {
            foreach (String item1 in A)
                foreach (String item2 in B)
                    if (item1.Equals(item2))
                        return true;
            return false;
        }
        private void ResetButtonClick(object sender, EventArgs e)
        {
            editBox.Text = "(a*|b)*";
            textBoxNFAStart.Clear();
            textBoxNFAEnd.Clear();
            textBoxDFAStart.Clear();
            textBoxDFAEnd.Clear();
            textBoxMFAStart.Clear();
            textBoxMFAEnd.Clear();
            listViewNFA.Items.Clear();
            listViewDFA.Items.Clear();
            listViewMFA.Items.Clear();
            //重置所有变量
            Program.resetAllVariebles();
            //MessageBox.Show("已重置","提示");
        }
    }
}