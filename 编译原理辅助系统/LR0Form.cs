using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace 编译原理辅助系统
{
    public partial class LR0Form : Form
    {
        private bool isLegal = true;//标记读入的文法文件是否合法(含有空行等)
        private bool isLR0=true;//标记读入的文法文件是否为LR0文法
        private int stepIndex = 0;//分步显示时的步数
        public static List<String> stateList = null;
        public LR0Form()
        {
            InitializeComponent();
            MessageBox.Show("LR0预测分析测试样例已放在Debug目录下。", "提示");
            resetListViewLayout();//重置/初始化表格布局
        }
        //函数功能：将文法字符串转换为标准化的文法形式
        private Grammar StringToGrammer(String grammarInfo)
        {
            Grammar grammar = new Grammar();
            //生成产生式集合
            List<ProductionRule> PRSet = new List<ProductionRule>();
            String[] PRString = grammarInfo.Split('\n');
            foreach (String item in PRString)
            {
                String PR_left = item.Split('-')[0];
                String PR_right = item.Split('>')[1];
                ProductionRule PR = new ProductionRule(PR_left, PR_right);
                //MessageBox.Show(PR.productionRuleInfo());
                grammar.productionRule.Add(PR);
            }
            //生成终结符集、非终结符集
            foreach (ProductionRule PR in grammar.productionRule)
            {
                foreach (String s in getNonTerminalChar(PR).ToList())
                {
                    if (!grammar.nonTerminalChar.Contains(s))
                        grammar.nonTerminalChar.Add(s);
                }
                foreach (String s in getTerminalChar(PR).ToList())
                {
                    if (!grammar.terminalChar.Contains(s))
                        grammar.terminalChar.Add(s);
                }
            }
            //空串不属于终结符(只是在firstSet集中用到)
            grammar.terminalChar.Remove("$");
            grammar.startChar = grammar.nonTerminalChar.ToArray()[0];
            //MessageBox.Show(grammar.startChar);
            return grammar;
        }
        //函数功能：分离多产生式文法(例如：S->A|B，改写为S->A;S->B)
        private Grammar getSplitedGrammar(Grammar grammar)
        {
            Grammar splitGrammar = new Grammar();
            splitGrammar.startChar = grammar.startChar;
            splitGrammar.nonTerminalChar = grammar.nonTerminalChar;
            splitGrammar.terminalChar = grammar.terminalChar;
            foreach (ProductionRule PR in grammar.productionRule)
            {
                String left = PR.left;
                //拆分原产生式
                String[] rightArray = PR.right.Split('|');
                foreach (String item in rightArray)
                {
                    ProductionRule _PR = new ProductionRule(left, item);
                    splitGrammar.productionRule.Add(_PR);
                }
            }
            return splitGrammar;
        }
        //函数功能：对于分离后的文法生成其拓广文法
        private Grammar getExtensionGrammar(Grammar grammar)
        {
            Grammar extensionGrammar = new Grammar();
            extensionGrammar.startChar = grammar.startChar;
            extensionGrammar.nonTerminalChar = grammar.nonTerminalChar;
            extensionGrammar.terminalChar = grammar.terminalChar;
            extensionGrammar.productionRule.Add(new ProductionRule(extensionGrammar.startChar, extensionGrammar.startChar));
            extensionGrammar.productionRule.AddRange(grammar.productionRule);
            return extensionGrammar;
        }
        //函数功能：获取文法的项目集(添加圆点)
        private Grammar getGrammarItemSet(Grammar grammar)
        {
            Grammar grammarItemSet = new Grammar();
            grammarItemSet.startChar = grammar.startChar;
            grammarItemSet.nonTerminalChar = grammar.nonTerminalChar;
            grammarItemSet.terminalChar = grammar.terminalChar;
            foreach (ProductionRule PR in grammar.productionRule)
            {
                for (int i = 0; i <= PR.right.Length; i++)
                {
                    String newRight = PR.right.Insert(i, ".");
                    //将加入圆点后的项目加到文法项目集grammarItemSet的产生式中
                    grammarItemSet.productionRule.Add(new ProductionRule(PR.left, newRight));
                }
            }
            return grammarItemSet;
        }
        //函数功能：获取一条产生式中的非终结符（大写字母）
        private String[] getNonTerminalChar(ProductionRule _PR)
        {
            ProductionRule PR = new ProductionRule(_PR.left, _PR.right);
            List<String> NTCList = new List<String>();
            //去除或|连接符号
            PR.left = PR.left.Replace("|", "");
            PR.right = PR.right.Replace("|", "");
            foreach (char c in PR.left)
            {
                if (c >= 'A' && c <= 'Z' && !NTCList.Contains(c.ToString()))
                    NTCList.Add(c.ToString());
            }
            foreach (char c in PR.right)
            {
                if (c >= 'A' && c <= 'Z' && !NTCList.Contains(c.ToString()))
                    NTCList.Add(c.ToString());
            }
            //MessageBox.Show((NTCList==null).ToString());
            return NTCList.ToArray();
        }
        //函数功能：获取一条产生式中的终结符（运算符、小写字母等非大写字母）
        private String[] getTerminalChar(ProductionRule _PR)
        {
            ProductionRule PR = new ProductionRule(_PR.left, _PR.right);
            //去除或|连接符号
            PR.left = PR.left.Replace("|", "");
            PR.right = PR.right.Replace("|", "");
            List<String> TCList = new List<String>();
            foreach (char c in PR.left)
            {
                if (!(c >= 'A' && c <= 'Z') && !TCList.Contains(c.ToString()))
                    TCList.Add(c.ToString());
            }
            foreach (char c in PR.right)
            {
                if (!(c >= 'A' && c <= 'Z') && !TCList.Contains(c.ToString()))
                    TCList.Add(c.ToString());
            }
            return TCList.ToArray();
        }
        //函数功能：全局文法输出测试
        private void displayGrammarInfo(Grammar G)
        {
            String grammarInfo = "";
            grammarInfo += G.grammarInfo();
            foreach (ProductionRule PR in G.productionRule)
                grammarInfo += (PR.productionRuleInfo() + "\n");
            MessageBox.Show(grammarInfo);
        }
        //函数功能：构造识别活前缀的NFA 20181211
        private NFA getNFAOfGrammarItemSet(Grammar G)
        {
            NFA myNFA = new NFA();
            myNFA.NFA_START = new String[] { "0" };//设定NFA初态为0，即项目集的首个元素
            //存放NFA终态
            List<String> NFA_END_List = new List<String>();
            //存放NFA转换函数
            List<transformFunction> NFA_TF_List = new List<transformFunction>();
            ProductionRule[] PRSet = G.productionRule.ToArray();//获取全局文法项目集的产生式集合
            for (int i = 0; i < PRSet.Length; i++)
            {
                ProductionRule PR = PRSet[i];//获取第i条产生式
                //标记终态(以圆点结尾的项目)
                if (PR.right[PR.right.Length - 1] == '.')
                    NFA_END_List.Add(i.ToString());
                else //对于非终态，才有可能作为转换函数的开始状态
                {
                    //更一般的情况，如果圆点不在产生式右部的最后，那么项目(K)的目标状态即为下一个项目(K+1)
                    String charAfterDot = PR.right[PR.right.IndexOf('.') + 1].ToString();//紧跟圆点的符号
                    NFA_TF_List.Add(new transformFunction(i.ToString(), charAfterDot, (i + 1).ToString()));
                    //如果圆点右边符号为非终结符：则遍历所有产生式(第一条除外)，将所有以该非终结符为左部、且右部最后为圆点的产生式项目，
                    //建立空串连接到项目i
                    if (Program.globalGrammarItemSet.nonTerminalChar.Contains(charAfterDot))
                    {
                        for (int k = 1; k < PRSet.Length; k++)
                        {
                            ProductionRule _PR = PRSet[k];//获取k条产生式
                            //以该非终结符为左部、且右部最后为圆点的产生式项目
                            if (_PR.left == charAfterDot && _PR.right[0] == '.')
                                NFA_TF_List.Add(new transformFunction(i.ToString(), "$", k.ToString()));
                        }
                    }
                }
            }
            //NFA输入符号
            List<String> NFA_INPUT_List = new List<String>();
            NFA_INPUT_List.AddRange(G.nonTerminalChar);
            NFA_INPUT_List.AddRange(G.terminalChar);
            myNFA.NFA_END = NFA_END_List.ToArray();
            myNFA.NFA_Transform = NFA_TF_List.ToArray();
            myNFA.NFA_INPUT = NFA_INPUT_List.ToArray();
            return myNFA;
        }
        //函数功能：将NFA转化为等价的DFA(同时更新全局MFA)
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
            A = StringArraySort(A);
            B = StringArraySort(B);
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
                item.state = StringArraySort(item.state);
                //标记状态集的标识符
                item.ID = ID.ToString();
                ID++;
            }
            return finalSet;
        }
        //函数功能：将NFA转化为等价的DFA(同时更新全局MFA)
        private void NFAToDFA()
        {
            if (Program.globalNFA == null)
            {
                MessageBox.Show("未检测到NFA，请先生成NFA", "提示");
                return;
            }
            //MessageBox.Show(num.ToString());
            //读入NFA转换函数
            //从Program.cs中获取NFA转换函数
            transformFunction[] NFA_Transform = Program.globalNFA.NFA_Transform;
            //通过NFA生成DFA的状态集(已标记标识符)
            Program.globalDFA.stateSet = getSubSet(Program.globalNFA.NFA_Transform).ToArray();
            stateSet_DFA[] DFASet = Program.globalDFA.stateSet;
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
        }
        //函数功能：将DFA化简为MFA
        private void DFAToMFA()
        {
            if (Program.globalDFA == null)
            {
                MessageBox.Show("无法生成MFA，请先生成DFA", "提示");
                return;
            }
            //以下代码为将当前全局DFA赋值给全局MFA
            Program.globalMFA.MFA_Transform = Program.globalDFA.DFA_Transform;
            Program.globalMFA.MFA_START = Program.globalDFA.DFA_START;
            Program.globalMFA.MFA_END = Program.globalDFA.DFA_END;
            Program.globalMFA.MFA_INPUT = Program.globalDFA.DFA_INPUT;
            Program.globalMFA.stateSet = Program.globalDFA.stateSet;
            //消除无用状态（转换函数）
            Program.globalMFA.MFA_Transform = removeUselessState(Program.globalMFA.MFA_Transform, Program.globalMFA.MFA_START);
            Program.globalMFA.MFA_maxState = Program.globalDFA.DFA_maxState;
            //消除多余状态（转换函数）
            //Program.globalMFA.MFA_Transform = removeExtraState(Program.globalMFA.MFA_Transform, Program.globalMFA.MFA_START, Program.globalMFA.MFA_END,
            //    Program.globalMFA.MFA_INPUT);
            //规范化MFA
            //对生成的MFA的状态进行重新编号，同时更新转换函数、初态集、终态集
            Program.globalMFA = normalizeMFA(Program.globalMFA);
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
        private transformFunction[] removeExtraState(transformFunction[] TF, String[] startState, String[] endState, String[] inputChar)
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
                if (fromState.Count() > 1)
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
                                if (tf[from, iC] != "")
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
        //函数功能：在字符串数组中查找指定字符串的位置下标
        private int findInStringArray(String[] StringArray, String s)
        {
            for (int i = 0; i < StringArray.Length; i++)
                if (StringArray[i].Equals(s))
                    return i;
            return -1;
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
            Program.globalMFA.MFA_maxState = stateNum-1;//MFA最大状态编号(0-maxState)
            //构建新的状态集合
            List<String> newStateSet = new List<String>();
            for (int i = 0; i < stateNum; i++)
                newStateSet.Add(i.ToString());
            //更新状态转换函数、初态集、终态集
            for (int i = 0; i < stateNum; i++)
            {
                //更新DFA状态子集
                Program.globalMFA.stateSet[int.Parse(newStateSet[i])] = Program.globalDFA.stateSet[int.Parse(stateSet[i])];
                foreach (transformFunction TF in mfa.MFA_Transform)
                {
                    if (TF.from == stateSet[i])
                        TF.from = newStateSet[i];
                    if (TF.to == stateSet[i])
                        TF.to = newStateSet[i];
                }
                for (int j = 0; j < mfa.MFA_START.Length; j++)
                {
                    if (mfa.MFA_START[j] == stateSet[i])
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
        //打开文件按钮单击事件
        private void openFileButtonClick(object sender, EventArgs e)
        {
            this.Text = "LR(0)文法分析";
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Title = "打开";
            OFD.Filter = "文法文件|*.grammar;*.txt";
            OFD.ShowDialog();
            String openPath = OFD.FileName; //获取打开文件的路径
            if (openPath == "")
                return;
            try
            {
                FileStream openFileStream = new FileStream(openPath, FileMode.OpenOrCreate, FileAccess.Read);
                byte[] buffer = new byte[1024 * 1024 * 5];
                int _grammerContent = openFileStream.Read(buffer, 0, buffer.Length);
                String grammerContent = Encoding.GetEncoding("GB2312").GetString(buffer, 0, _grammerContent);
                editBoxGrammer.Text = grammerContent.Trim('\n').Trim('\r');
                editBoxGrammer.Text = editBoxGrammer.Text.Replace(" ", "");
                openFileStream.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("文法内容不合法，请检查！\r\n请检查是否含有非法字符，还有不要给我换行符哟 (*╹▽╹*)", "警告");
            }
        }
        //文法确认按钮单击事件
        private void affirmGrammarButtonClick(object sender, EventArgs e)
        {
            this.Text = "LR(0)文法分析";
            isLR0 = true;//初始化当前文法isLR0=true
            isLegal = true;
            editBoxGrammer.Text = editBoxGrammer.Text.Replace(" ", "");
            if (editBoxGrammer.Text == "")
            {
                MessageBox.Show("当前文法为空，请输入文法后操作", "提示");
                return;
            }
            try
            {
                initalGlobalVariables();//初始化全局变量
                listViewAnalyseResult.Items.Clear();//清空过程表内容
                //displayGrammarInfo(Program.globalGrammar);//文法输出测试
                //displayGrammarInfo(Program.globalGrammarItemSet);//文法项目集输出测试
                //MessageBox.Show(Program.globalNFA.NFAInfo());//全局NFA输出测试
                //MessageBox.Show(Program.globalDFA.DFAInfo());//全局DFA输出测试
                //MessageBox.Show(Program.globalMFA.MFAInfo());//全局MFA输出测试
                if (isLegal)
                {
                    buttonSaveFile.Visible = true;
                    buttonDirectlyToResult.Visible = true;
                    buttonDisplayStateInfo.Visible = true;
                    buttonCreateLR0AnalyzeTable.Visible = true;
                    buttonStepByStep.Visible = true;
                    buttonDFAVisualize.Visible = true;
                    //关闭DFA可视化窗体
                    foreach (Form item in Application.OpenForms)
                    {
                        if (item is DFAVisualForm)
                        {
                            item.Close();
                            break;
                        }
                    }
                }
                else
                {
                    buttonSaveFile.Visible = false;
                    buttonDirectlyToResult.Visible = false;
                    buttonDisplayStateInfo.Visible = false;
                    buttonCreateLR0AnalyzeTable.Visible = false;
                    buttonStepByStep.Visible = false;
                    buttonDFAVisualize.Visible = false;
                }
            }
            catch (Exception e1)
            {
                //MessageBox.Show("文法内容不合法，请检查！\r\n请检查是否含有非法字符，还有不要给我换行符哟 (*╹▽╹*)", "警告");
                isLegal = false;//标记读入的文法文件不合法
                MessageBox.Show(e1.Message, "警告");
            }
        }
        //文件保存按钮单击事件
        private void saveFileButtonClick(object sender, EventArgs e)
        {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.Title = "保存";
            SFD.Filter = "LR0-AnalyseResult File|*.result;*.txt";
            SFD.ShowDialog();
            String savePath = SFD.FileName; //获取打开文件的路径
            if (savePath == "")
                return;
            FileStream saveFileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write);
            StringBuilder SB = new StringBuilder();
            int rowNum = 0;
            int columnNum = 0;
            SB.Append("LL(0)自底向上预测分析详细信息");
            SB.Append("\r\n");
            SB.Append("\r\n");
            //保存当前文法
            SB.Append("文法内容");
            SB.Append("\r\n");
            SB.Append(editBoxGrammer.Text);
            SB.Append("\r\n");
            SB.Append("\r\n");
            //保存当前文法的状态信息(项目族信息)
            SB.Append("文法项目族信息");
            SB.Append("\r\n");
            rowNum = listViewStateInfo.Items.Count;
            columnNum = listViewStateInfo.Columns.Count;
            if (rowNum == 0)
            {
                SB.Append("未生成文法项目族！");
                SB.Append("\r\n");
            }
            else
            {
                SB.Append("\t");
                for (int i = 0; i < columnNum; i++)
                {
                    SB.Append(listViewStateInfo.Columns[i].Text);
                    SB.Append("\t");
                }
                SB.Append("\r\n");
                for (int i = 0; i < rowNum; i++)
                {
                    for (int j = 0; j < columnNum; j++)
                    {
                        SB.Append(listViewStateInfo.Items[i].SubItems[j].Text);
                        SB.Append("\t");
                    }
                    SB.Append("\r\n");
                }
            }
            SB.Append("\r\n");
            //保存当前文法的LR分析表
            SB.Append("文法LR分析表(ACTION+GOTO)");
            SB.Append("\r\n");
            rowNum = listViewLR0AnalyzeTable.Items.Count;
            columnNum = listViewLR0AnalyzeTable.Columns.Count;
            if (rowNum == 0)
            {
                SB.Append("未构造文法LR分析表！");
                SB.Append("\r\n");
            }
            else
            {
                for (int i = 0; i < columnNum; i++)
                {
                    SB.Append(listViewLR0AnalyzeTable.Columns[i].Text);
                    SB.Append("\t");
                }
                SB.Append("\r\n");
                for (int i = 0; i < rowNum; i++)
                {
                    for (int j = 0; j < columnNum; j++)
                    {
                        SB.Append(listViewLR0AnalyzeTable.Items[i].SubItems[j].Text);
                        SB.Append("\t");
                    }
                    SB.Append("\r\n");
                }
            }
            SB.Append("\r\n");
            //保存当前句子的分析过程和结果
            SB.Append("句子分析过程");
            SB.Append(" (" + textBoxSentence.Text + ")");
            SB.Append("\r\n");
            int procedureItemNum = createAnalyzeProcedure(textBoxSentence.Text).Count();
            rowNum = listViewAnalyseResult.Items.Count;
            columnNum = listViewAnalyseResult.Columns.Count;
            
            if (rowNum == 0)
            {
                SB.Append("未构造句子分析过程！");
                SB.Append("\r\n");
            }
            else
            {
                for (int i = 0; i < columnNum; i++)
                {
                    SB.Append(listViewAnalyseResult.Columns[i].Text);
                    SB.Append("\t");
                }
                SB.Append("\r\n");
                for (int i = 0; i < rowNum; i++)
                {
                    for (int j = 0; j < columnNum; j++)
                    {
                        SB.Append(listViewAnalyseResult.Items[i].SubItems[j].Text);
                        SB.Append("\t");
                    }
                    SB.Append("\r\n");
                }
            }
            if (rowNum == procedureItemNum)
            {
                if (SB.ToString().Contains("接受"))
                    SB.Append("句子 (" + textBoxSentence.Text + " )分析成功！");
                else
                    SB.Append("句子 (" + textBoxSentence.Text + " )分析失败！");
            }
            else SB.Append("句子 (" + textBoxSentence.Text + " )未分析完成！");
            SB.Append("\r\n");
            byte[] buffer = Encoding.GetEncoding("GB2312").GetBytes(SB.ToString());
            saveFileStream.Write(buffer, 0, buffer.Length);//写入LR0分析过程信息
            saveFileStream.Close();
            MessageBox.Show("文件以保存至" + savePath);
        }
        //退出按钮单击事件
        private void exitButtonClick(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();
        }
        //显示状态集信息按钮单击事件
        private void displayStateInfoButtonClick(object sender, EventArgs e)
        {
            listViewStateInfo.Items.Clear();//清除项目信息
            for (int i = 0; i <= Program.globalMFA.MFA_maxState; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Text = i.ToString();
                String itemInfo = "";//项目集信息
                foreach (String state in Program.globalMFA.stateSet[i].state)
                {
                    String PRInfo = Program.globalGrammarItemSet.productionRule[int.Parse(state)].productionRuleInfo();//获取项目产生式信息
                    itemInfo += (PRInfo+"; ");
                }
                item.SubItems.Add(itemInfo);
                listViewStateInfo.Items.Add(item);
            }
        }
        //LR0分析表创建按钮单击事件
        private void createLR0AnalyzeTableButtonClick(object sender, EventArgs e)
        {
            if (listViewStateInfo.Items.Count == 0)
            {
                MessageBox.Show("请先生成LR规范族信息", "提示");
                return;
            }
            //生成LR分析表(Action和GoTo)
            generateLRAnalyzeTable();
            listViewLR0AnalyzeTable.Clear();//清空旧项目信息
            listViewLR0AnalyzeTable.Columns.Add("状态", 60, HorizontalAlignment.Center);
            foreach (String VtChar in Program.globalActionTable.terminalChar)
                listViewLR0AnalyzeTable.Columns.Add(VtChar, 60, HorizontalAlignment.Center);
            foreach (String VnChar in Program.globalGoToTable.nonTerminalChar)
                listViewLR0AnalyzeTable.Columns.Add(VnChar, 60, HorizontalAlignment.Center);
            for (int i = 0; i <= Program.globalMFA.MFA_maxState; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Text = i.ToString();
                foreach (String VtChar in Program.globalActionTable.terminalChar)
                    item.SubItems.Add(getActionContentFromTable(i,VtChar));
                foreach (String VnChar in Program.globalGoToTable.nonTerminalChar)
                    item.SubItems.Add(getGoToContentFromTable(i,VnChar));
                listViewLR0AnalyzeTable.Items.Add(item);
            }
            if (isLR0 == false)
            {
                this.Text = "LR(0)文法分析\t(当前文法Grammar非LR(0)文法)";
                MessageBox.Show("当前非LR(0)文法，后续句子分析操作将无法继续！", "警告");
            }
            else this.Text = "LR(0)文法分析\t(当前文法Grammar为LR(0)文法)";
        }
        //函数功能：向Action表中插入一条记录(对应于stateID行、VtChar列)
        private void addOneRecordToActionTable(int stateID,String VtChar,String actionContent)
        {
            int row=stateID;
            int column=findInStringArray(Program.globalActionTable.terminalChar,VtChar);
            if(row>Program.globalMFA.MFA_maxState)
            {
                //MessageBox.Show("stateID超出状态总数","警告");
                return;
            }
            if(column==-1)
            {
                //MessageBox.Show("未找到终结符 "+VtChar,"警告");
                return;
            }
            //如果当前actionTable[row,column]已经有值，则说明存在冲突。
            if (Program.globalActionTable.actionTable[row, column] != "")
                isLR0 = false;
            //向row行column列放入动作内容actionContent
            Program.globalActionTable.actionTable[row,column]+=(actionContent+";");
        }
        //函数功能：向GoTo表中插入一条记录(对应于stateID行、VnChar列)
        private void addOneRecordToGoToTable(int stateID,String VnChar,String goToContent)
        {
            int row=stateID;
            int column=findInStringArray(Program.globalGoToTable.nonTerminalChar,VnChar);
            if(row>Program.globalMFA.MFA_maxState)
            {
                //MessageBox.Show("stateID超出状态总数","警告");
                return;
            }
            if(column==-1)
            {
                //MessageBox.Show("未找到非终结符 "+VnChar,"警告");
                return;
            }
            //向row行column列放入动作内容goToContent
            Program.globalGoToTable.goToTable[row,column]=goToContent;
        }
        //函数功能：生成Action和GoTo分析表
        private void generateLRAnalyzeTable()
        {
            //初始化Action表和GoTo表
            Program.globalActionTable = new ActionTable(Program.globalGrammar.terminalChar.ToArray(), Program.globalMFA.MFA_maxState);
            Program.globalGoToTable = new GoToTable(Program.globalGrammar.nonTerminalChar.ToArray(), Program.globalMFA.MFA_maxState);
            MFA myMFA = Program.globalMFA;//获取全局MFA
            //遍历每一个项目集Ik
            for (int k = 0; k <= myMFA.MFA_maxState; k++)
            {
                //遍历项目集Ik的所有项目
                foreach (String itemID in myMFA.stateSet[k].state)
                {
                    //获取第itemID个产生式
                    ProductionRule PR = Program.globalGrammarItemSet.productionRule[int.Parse(itemID)];
                    //查找点.后为终结符的产生式
                    if (PR.right.IndexOf(".") != PR.right.Length - 1 &&
                        Program.globalGrammar.terminalChar.Contains(PR.right[PR.right.IndexOf(".") + 1].ToString()))
                    {
                        String VtChar = PR.right[PR.right.IndexOf(".") + 1].ToString();
                        //查找Ik经过终结符VtChar的目标状态I
                        String objectState = getObjectStateFromTFArray(Program.globalMFA.MFA_Transform, k.ToString(), VtChar);
                        //向ActionTable中添加一条记录
                        addOneRecordToActionTable(k, VtChar, "S" + objectState);
                    }
                    //查找点在最后的项目（归约项目）约束k>1 是为了过滤前2个拓广产生式0和1
                    if (k>1&&PR.right.IndexOf(".") == PR.right.Length - 1)
                    {
                        //获取产生式PR原始状态在文法中的位置
                        int PRIndex = findInProductionRuleArray(Program.globalGrammar.productionRule.ToArray(), PR.productionRuleInfo().Replace(".",""));
                        //对所有终结符a和#，置ActionTable[k,a/#]为rj，j为PR原始状态(无点)在原文法中的位置下标
                        foreach (String VtChar in Program.globalGrammar.terminalChar)
                            addOneRecordToActionTable(k, VtChar, "r" + PRIndex.ToString());
                        addOneRecordToActionTable(k, "#", "r" + PRIndex.ToString());
                    }
                    //遍历转换函数
                    foreach (transformFunction TF in myMFA.MFA_Transform)
                    {
                        //如果转换经过的符号是终结符A，则置GoTo[k,A]为目标状态ID
                        //MessageBox.Show(TF.tranformInfo());
                        if (TF.from==k.ToString()&&Program.globalGrammar.nonTerminalChar.Contains(TF.by))
                            addOneRecordToGoToTable(k, TF.by, TF.to);
                    }
                    //记录accept
                    if (PR.left == Program.globalGrammar.startChar && PR.right == Program.globalGrammar.startChar + ".")
                        addOneRecordToActionTable(k, "#", "acc");
                }
            }
        }
        //函数功能：从ActionTable表格中获取指定stateID和终结符对应的动作内容
        private String getActionContentFromTable(int stateID,String VtChar)
        {
            int row = stateID;
            int column = findInStringArray(Program.globalActionTable.terminalChar.ToArray(), VtChar);
            if (row >Program.globalMFA.MFA_maxState)
            {
                //MessageBox.Show("row大于maxStateID", "警告");
                return "";
            }
            if (column == -1)
            {
                //MessageBox.Show("终结符未找到！", "警告");
                return "";
            }
            return Program.globalActionTable.actionTable[row, column];
        }
        //函数功能：从GoToTable表格中获取指定stateID和非终结符对应的动作内容
        private String getGoToContentFromTable(int stateID, String VnChar)
        {
            int row = stateID;
            int column = findInStringArray(Program.globalGoToTable.nonTerminalChar.ToArray(), VnChar);
            if (row > Program.globalMFA.MFA_maxState)
            {
                //MessageBox.Show("row大于maxStateID", "警告");
                return "";
            }
            if (column == -1)
            {
                //MessageBox.Show("非终结符未找到！", "警告");
                return "";
            }
            return Program.globalGoToTable.goToTable[row, column];
        }
        //函数功能：在产生式数组中查找指定产生式(用字符串表示，便于分析时反向查找)的位置下标
        private int findInProductionRuleArray(ProductionRule[] PRArray, String productionRuleInfo)
        {
            productionRuleInfo = productionRuleInfo.Replace(" ", "");//去除产生式字符串中的空格
            //按照产生式字符串内容查找
            for (int i = 0; i < PRArray.Length; i++)
                if (PRArray[i].productionRuleInfo().Equals(productionRuleInfo))
                    return i;
            return -1;
        }
        //函数功能：在指定转换函数集合TFArray[]中，根据from、by，获取目标转换状态
        private String getObjectStateFromTFArray(transformFunction[] TFArray,String from, String by)
        {
            foreach (transformFunction TF in TFArray)
                if (TF.from == from && TF.by == by)
                    return TF.to;
            return "";
        }
        //一键生成LR分析表按钮、告知分析结果单击事件
        private void directlyToResultButtonClick(object sender, EventArgs e)
        {
            if (isLR0 == false)
                MessageBox.Show("当前文法Grammar非LR(0)文法，以下分析结果可能存在错误，仅供参考！","警告");
            //用于填充分析结果表格
            listViewAnalyseResult.Items.Clear();
            if (listViewLR0AnalyzeTable.Items.Count == 0)
            {
                MessageBox.Show("请先生成LR分析表", "提示");
                return;
            }
            String sentence = textBoxSentence.Text.Replace(" ", "");
            if (sentence == "")
            {
                MessageBox.Show("请输入待分析句子后操作", "提示");
                return;
            }
            ListViewItem[] itemSet = createAnalyzeProcedure(textBoxSentence.Text.Replace(" ", ""));
            foreach (ListViewItem item in itemSet)
                listViewAnalyseResult.Items.Add(item);
            if (itemSet.Count()!=0&&itemSet[itemSet.Count() - 1].SubItems[4].Text.Contains("接受"))
                MessageBox.Show("分析成功！句子" + sentence.Trim('#') + "是该文法的一个句子", "提示");
            else
                MessageBox.Show("分析失败！句子" + sentence.Trim('#') + "不是该文法的一个句子", "提示");
        }
        //单步填充LR分析表按钮单击事件
        private void stepByStepButtonClick(object sender, EventArgs e)
        {
            //用于填充分
            if (listViewLR0AnalyzeTable.Items.Count == 0)
            {
                MessageBox.Show("请先生成LR分析表", "提示");
                return;
            }
            String sentence = textBoxSentence.Text.Replace(" ", "");
            if (sentence == "")
            {
                MessageBox.Show("请输入待分析句子后操作", "提示");
                return;
            }
            if (stepIndex == 0)
                listViewAnalyseResult.Items.Clear();//首步清空分析结果
            ListViewItem[] itemList = createAnalyzeProcedure(sentence);
            int stepNum = itemList.Length;//记录总步数
            if (stepIndex == stepNum - 1)
            {
                listViewAnalyseResult.Items.Add(itemList[stepIndex]);
                if (itemList[itemList.Length - 1].SubItems[4].Text.Contains("接受"))
                    MessageBox.Show("分析成功！句子" + sentence.Trim('#') + "是该文法的一个句子", "提示");
                else
                    MessageBox.Show("分析失败！句子" + sentence.Trim('#') + "不是该文法的一个句子", "提示");
                stepIndex++;
            }
            else if (stepIndex < stepNum - 1)
            {
                listViewAnalyseResult.Items.Add(itemList[stepIndex]);//每次将第stepIndex个项目添加到表格中
                stepIndex++;
            }
            else
            {
                MessageBox.Show("当前已是最后一步，下次点击将重新开始。", "提示");
                //用于填充分析结果表格
                stepIndex = 0;
            }
        }
        //函数功能：构造LR0句子分析过程，返回分析过程中产生的listView项目
        private ListViewItem[] createAnalyzeProcedure(String sentence)
        {
            sentence += "#";//向句子末尾添加句子结束标记#
            //状态栈(LR规范族项目集编号)
            Stack<String> stateStack = new Stack<String>();
            //符号栈：存放终结符和非终结符
            Stack<String> charStack = new Stack<String>();
            int pointer = 0;//代表指向输入符号串(待分析句子)的指针
            stateStack.Push("0");//第一个规范族进状态栈
            charStack.Push("#");//结束标记进符号栈
            int stepCount = 1;//步骤计数器
            List<ListViewItem> itemList = new List<ListViewItem>();
            ListViewItem item0 = new ListViewItem();
            item0.Text = stepCount.ToString();
            item0.SubItems.Add(getCurrentStateStackInfo(stateStack));
            item0.SubItems.Add(getCurrentStateStackInfo(charStack));
            item0.SubItems.Add(getCurrentLeftSentence(sentence, pointer));
            item0.SubItems.Add("Initial-State");
            item0.SubItems.Add("Initial-State");
            itemList.Add(item0);
            while (true)
            {
                String S = stateStack.Peek().Replace("(", "").Replace(")", "");//状态栈栈顶状态
                String a = sentence[pointer].ToString();//当前面对符号a
                //MessageBox.Show("["+S+","+a+"]");
                //MessageBox.Show(sentence.Substring(pointer));
                //在Action表中查找对应的动作Action[S,a]
                String actionContent = getActionContentFromTable(int.Parse(S), a).Replace(";","");
                if (actionContent == "")
                    return itemList.ToArray();
                if (actionContent == "acc")
                {
                    stepCount++;
                    //MessageBox.Show("分析成功！", "提示");
                    ListViewItem item = new ListViewItem();
                    item.Text = stepCount.ToString();
                    item.SubItems.Add(getCurrentStateStackInfo(stateStack));
                    item.SubItems.Add(getCurrentStateStackInfo(charStack));
                    item.SubItems.Add(getCurrentLeftSentence(sentence, pointer));
                    item.SubItems.Add("接受！");
                    item.SubItems.Add("-");
                    itemList.Add(item);
                    return itemList.ToArray();
                }
                else if (actionContent[0] == 'S' && Program.globalGrammar.terminalChar.Contains(a)) //对应于Sj
                {
                    charStack.Push(a);//a移入符号栈
                    String stateID = actionContent.Replace("S", "");
                    if (int.Parse(stateID) > 9)
                        stateID = "(" + stateID + ")";
                    stateStack.Push(stateID);//stateID移入状态栈
                    pointer++;
                    stepCount++;
                    ListViewItem item = new ListViewItem();
                    item.Text = stepCount.ToString();
                    item.SubItems.Add(getCurrentStateStackInfo(stateStack));
                    item.SubItems.Add(getCurrentStateStackInfo(charStack));
                    item.SubItems.Add(getCurrentLeftSentence(sentence, pointer));
                    item.SubItems.Add(actionContent + " (移进)");
                    item.SubItems.Add("-");
                    itemList.Add(item);
                }
                else if (actionContent[0] == 'r' && (Program.globalGrammar.terminalChar.Contains(a) || a == "#")) //对应于rj
                {
                    String PRID = actionContent.Replace("r", "");//产生式ID
                    //查找第PRID个产生式
                    ProductionRule PR = Program.globalGrammar.productionRule[int.Parse(PRID)];
                    //MessageBox.Show(charStack.Count().ToString());
                    //MessageBox.Show(PR.productionRuleInfo());
                    int PR_RightLength = PR.right.Length;//右部长度
                    //符号栈出栈PR_RightLength次
                    for (int i = 0; i < PR_RightLength; i++)
                    {
                        stateStack.Pop();//状态栈出栈
                        charStack.Pop();//符号栈出栈
                    }
                    charStack.Push(PR.left);//产生式左部进符号栈
                    S = stateStack.Peek().Replace("(","").Replace(")","");//状态栈栈顶状态
                    //查找GoTo[S,PR.left]
                    String stateID = getGoToContentFromTable(int.Parse(S), PR.left);
                    if (stateID != "")
                    {
                        if (int.Parse(stateID) > 9)
                            stateID = "(" + stateID + ")";
                        stateStack.Push(stateID);//stateID移入状态栈
                        stepCount++;
                        ListViewItem item = new ListViewItem();
                        item.Text = stepCount.ToString();
                        item.SubItems.Add(getCurrentStateStackInfo(stateStack));
                        item.SubItems.Add(getCurrentStateStackInfo(charStack));
                        item.SubItems.Add(getCurrentLeftSentence(sentence, pointer));
                        item.SubItems.Add(PR.productionRuleInfo()+" (归约)");
                        item.SubItems.Add(stateID);
                        itemList.Add(item);
                    }
                    else return itemList.ToArray();
                }
                else return itemList.ToArray();
            }
        }
        //函数功能：返回当前栈Stack信息
        private String getCurrentStateStackInfo(Stack<String> Stack)
        {
            String[] itemArray = Stack.ToArray();
            String stackInfo = "";
            foreach (String item in itemArray.Reverse())
                stackInfo += item;
            return stackInfo;
        }
        //函数功能：返回当前剩余符号串(待分析句子)
        private String getCurrentLeftSentence(String sentence, int pointer)
        {
            return sentence.Substring(pointer);
        }
        //一旦检测到文法输入框内容更改，隐藏操作按钮，阻止用户进一步操作，同时初始化所有listView控件
        private void grammarChanged(object sender, EventArgs e)
        {
            resetListViewLayout();//重置表格布局
            listViewAnalyseResult.Items.Clear();//清空过程表内容           
            buttonSaveFile.Visible = false;
            buttonDirectlyToResult.Visible = false;
            buttonDisplayStateInfo.Visible = false;
            buttonCreateLR0AnalyzeTable.Visible = false;
            buttonStepByStep.Visible = false;
            buttonDFAVisualize.Visible = false;
        }
        //函数功能：初始化全局变量
        private void initalGlobalVariables()
        {
            try
            {
                editBoxGrammer.Text = editBoxGrammer.Text.Trim('\n').Trim('\r');
                //赋值全局文法变量
                //分离多右部产生式
                Program.globalGrammar = getSplitedGrammar(StringToGrammer(editBoxGrammer.Text.Replace(" ", "")));
                //获取拓广文法
                Program.globalGrammarItemSet = getExtensionGrammar(Program.globalGrammar);
                //获取文法项目
                Program.globalGrammarItemSet = getGrammarItemSet(Program.globalGrammarItemSet);
                //根据文法项目集生成NFA
                Program.globalNFA = getNFAOfGrammarItemSet(Program.globalGrammarItemSet);
                //NFA转DFA
                NFAToDFA();
                //DFA化简为MFA
                DFAToMFA();
                //初始化Action表和GoTo表
                Program.globalActionTable = new ActionTable(Program.globalGrammar.terminalChar.ToArray(), Program.globalMFA.MFA_maxState);
                Program.globalGoToTable = new GoToTable(Program.globalGrammar.nonTerminalChar.ToArray(), Program.globalMFA.MFA_maxState);

            }
            catch (Exception)
            {
                MessageBox.Show("文法内容不合法，请检查！\r\n请检查是否含有非法字符，还有不要给我换行符哟 (*╹▽╹*)", "警告");
                isLegal = false;//标记读入的文法文件不合法

                //MessageBox.Show(e1.Message, "警告");
            }
        }
        //函数功能：重置表格布局
        private void resetListViewLayout()
        {
            listViewStateInfo.Clear();
            listViewStateInfo.Columns.Add("状态编号", 75, HorizontalAlignment.Center);
            listViewStateInfo.Columns.Add("项目集", 700, HorizontalAlignment.Left);
            listViewLR0AnalyzeTable.Clear();
            listViewLR0AnalyzeTable.Columns.Add("状态", 60, HorizontalAlignment.Center);
        }
        //DFA可视化按钮单击事件
        private void DFAVisualizeButtonClick(object sender, EventArgs e)
        {
            Form _DFAForm = new DFAVisualForm();
            _DFAForm.StartPosition = FormStartPosition.CenterScreen;
            _DFAForm.Show();
        }
        //判断当前文法是否为LR0文法
        private bool isLR0Grammar(Grammar G)
        {
            return isLR0 == true;
        }
    }
}