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
    public partial class LL1Form : Form
    {
        private int stepIndex = 0;//分步显示时的步数
        public bool isLL1Type = true;//记录当前文法是否为LL1型文法，赋值一次后，不用再调用isLL1TypeGrammar(G)判断
        public LL1Form()
        {
            InitializeComponent();
            resetListViewLayout();//重置表格布局
            MessageBox.Show("LL1预测分析测试样例已放在Debug目录下。","提示");
        }
        private void openFileButtonClick(object sender, EventArgs e)
        {
            this.Text = "LL(1)预测分析";
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
                initalGlobalVariables();//初始化(实例化)全局变量（First集、Follow集、Select集、预测分析表）
                //displayGrammarInfo();
                openFileStream.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("文法内容不合法，请检查！\r\n请检查是否含有非法字符，还有不要给我换行符哟 (*╹▽╹*)", "警告");
            }
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
        private void displayGrammarInfo()
        {
            String grammarInfo = "";
            grammarInfo += Program.globalGrammar.grammarInfo();
            foreach (ProductionRule PR in Program.globalGrammar.productionRule)
                grammarInfo += (PR.productionRuleInfo() + "\n");
            MessageBox.Show(grammarInfo);
        }
        //文法确认
        private void affirmGrammarButtonClick(object sender, EventArgs e)
        {
            editBoxGrammer.Text = editBoxGrammer.Text.Replace(" ", "");
            if (editBoxGrammer.Text == "")
            {
                MessageBox.Show("当前文法为空，请输入文法后操作", "提示");
                return;
            }
            if (!isLL1TypeGrammar(Program.globalGrammar))
            {
                MessageBox.Show("当前非LL1文法，后续句子分析操作将无法继续！", "警告");
                //return;//此时不用return，可生成first、follow、select集，只是句子分析操作无法进行
            }
            try
            {
                initalGlobalVariables();//初始化(实例化)全局变量（First集、Follow集、Select集、预测分析表）
                buttonSaveFile.Visible = true;
                buttonGenerateFirstSet.Visible = true;
                buttonGenerateFollowSet.Visible = true;
                buttonDirectlyToResult.Visible = true;
                buttonStepByStep.Visible = true;
                buttonCreatePredictionTable.Visible = true;
            }
            catch (Exception)
            {
                MessageBox.Show("文法内容不合法，请检查！\r\n请检查是否含有非法字符，还有不要给我换行符哟 (*╹▽╹*)", "警告");
            }
        }
        //函数功能：从firstSetTable表格中获取指定非终结符(或$或终结符)的firstSet集
        private String[] getFirstSetFromTable(String VChar)
        {
            //如果为终结符或空串$，则返回终结符自身
            if (!Char.IsUpper(VChar[0]))
                return new String[] { VChar };
            List<String> firstSet = new List<String>();
            int row = findInStringArray(Program.globalFirstSet.nonTerminalChar, VChar);
            foreach (String VtChar in Program.globalFirstSet.terminalChar)
            {
                int column = findInStringArray(Program.globalFirstSet.terminalChar, VtChar);
                if (Program.globalFirstSet.firstSetTable[row, column] == "1")
                    firstSet.Add(VtChar);
            }
            return firstSet.ToArray();
        }
        //函数功能：从followSetTable表格中获取指定非终结符的followSet集
        private String[] getFollowSetFromTable(String VnChar)
        {
            List<String> followSet = new List<String>();
            int row = findInStringArray(Program.globalFollowSet.nonTerminalChar, VnChar);
            foreach (String VtChar in Program.globalFollowSet.terminalChar)
            {
                int column = findInStringArray(Program.globalFollowSet.terminalChar, VtChar);
                if (Program.globalFollowSet.followSetTable[row, column] == "1")
                    followSet.Add(VtChar);
            }
            return followSet.ToArray();
        }
        //函数功能：从selectSetTable表格中获取指定产生式PR的selectSet集
        private String[] getSelectSetFromTable(ProductionRule PR)
        {
            List<String> selectSet = new List<String>();
            int row = findInProductionRuleArray(Program.globalSelectSet.productionRule, PR.productionRuleInfo());
            foreach (String VtChar in Program.globalSelectSet.terminalChar)
            {
                int column = findInStringArray(Program.globalSelectSet.terminalChar, VtChar);
                if (Program.globalSelectSet.selectSetTable[row, column] == "1")
                    selectSet.Add(VtChar);
            }
            return selectSet.ToArray();
        }
        //函数功能：从predictionAnalyzeTable表格中获取指定非终结符、终结符(包含#)下，所用产生式PR
        private ProductionRule getCertainProductionRule(String VnChar, String VtChar)
        {
            int row = findInStringArray(Program.globalPredictionAnalyzeTable.nonTerminalChar, VnChar);
            int column = findInStringArray(Program.globalPredictionAnalyzeTable.terminalChar, VtChar);
            if (row == -1)
            {
                //MessageBox.Show("非终结符未找到！","警告");
                return null;
            }
            if (column == -1)
            {
                //MessageBox.Show("终结符未找到！", "警告");
                return null;
            }
            int PRIndex = findInProductionRuleArray(Program.globalSelectSet.productionRule, Program.globalPredictionAnalyzeTable.predictionAnalyzeTable[row, column]);
            if (PRIndex != -1)
                return Program.globalSelectSet.productionRule[PRIndex];
            else
            {
                //MessageBox.Show("匹配产生式未找到！", "警告");
                return null;//代表未找到与之匹配的产生式
            }
        }
        //函数功能：添加一条First集记录
        private void addOneRecordToFirstSet(String nonTerminalChar, String terminalChar)
        {
            int row = findInStringArray(Program.globalFirstSet.nonTerminalChar, nonTerminalChar);
            int column = findInStringArray(Program.globalFirstSet.terminalChar, terminalChar);
            Program.globalFirstSet.firstSetTable[row, column] = "1";
        }
        //函数功能：添加一条Follow集记录
        private void addOneRecordToFollowSet(String nonTerminalChar, String terminalChar)
        {
            int row = findInStringArray(Program.globalFollowSet.nonTerminalChar, nonTerminalChar);
            int column = findInStringArray(Program.globalFollowSet.terminalChar, terminalChar);
            Program.globalFollowSet.followSetTable[row, column] = "1";
        }
        //函数功能：添加一条Select集记录
        private void addOneRecordToSelectSet(ProductionRule PR, String terminalChar)
        {
            int row = findInProductionRuleArray(Program.globalSelectSet.productionRule.ToArray(), PR.productionRuleInfo());
            int column = findInStringArray(Program.globalSelectSet.terminalChar, terminalChar);
            Program.globalSelectSet.selectSetTable[row, column] = "1";
        }
        //函数功能：向预测分析表中添加一条产生式记录
        private void addOneRecordToPredictionAnalyzeTable(String nonTerminalChar, String terminalChar, ProductionRule productionRule)
        {
            int row = findInStringArray(Program.globalPredictionAnalyzeTable.nonTerminalChar, nonTerminalChar);
            int column = findInStringArray(Program.globalPredictionAnalyzeTable.terminalChar, terminalChar);
            Program.globalPredictionAnalyzeTable.predictionAnalyzeTable[row, column] += (productionRule.productionRuleInfo() + " ");
        }
        //函数功能：在字符串数组中查找指定字符串的位置下标
        private int findInStringArray(String[] StringArray, String s)
        {
            for (int i = 0; i < StringArray.Length; i++)
                if (StringArray[i].Equals(s))
                    return i;
            return -1;
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
        //函数功能：判断非终结符X能否推出空串epsilon
        private bool isDerivableToEpsilon_Char(String VnChar)
        {
            //如果VnChar是空串$，则返回true
            if (VnChar == "$")
                return true;
            //如果是终结符，则返回false
            if (!Char.IsUpper(VnChar[0]))
                return false;
            //displayGrammarInfo();
            //derivableToEpsilon记录终结符能否推导出空串（初始值为未定）
            String[] derivableToEpsilon = new String[Program.globalGrammar.nonTerminalChar.Count()];
            for (int i = 0; i < derivableToEpsilon.Length; i++)
                derivableToEpsilon[i] = "未定";
            //定义与PRList元素相同的PRArray，防止发生“集合已修改”错误
            ProductionRule[] PRArray = new ProductionRule[Program.globalGrammar.productionRule.Count()];
            Program.globalGrammar.productionRule.CopyTo(PRArray);
            //用于保留最终留下的文法产生式
            List<ProductionRule> PRList = PRArray.ToList();
            //扫描文法产生式，删除所有右部含有终结符的产生式
            foreach (ProductionRule item in Program.globalGrammar.productionRule)
            {
                //删除所有右部含有终结符的产生式
                foreach (char c in item.right)
                {
                    if (Program.globalGrammar.terminalChar.Contains(c.ToString()))
                    {
                        PRList.Remove(item);
                        break;
                    }
                }
            }
            //displayPRList(PRList);
            //删除右部含有终结符的产生式后，所有产生式左部的非终结符
            List<String> savedLeftNonTerminalChar = new List<String>();
            foreach (ProductionRule item in PRList)
            {
                //MessageBox.Show(item.productionRuleInfo());
                //将所有产生式此时左部的非终结符加入savedLeftNonTerminalChar中
                savedLeftNonTerminalChar.Add(item.left);
            }
            //标记不在savedLeftNonTerminalChar中的非终结符不能推导出空串
            foreach (String Vn in Program.globalGrammar.nonTerminalChar)
                if (!savedLeftNonTerminalChar.Contains(Vn))
                    derivableToEpsilon[Program.globalGrammar.nonTerminalChar.IndexOf(Vn)] = "否";
            PRArray = new ProductionRule[PRList.Count()];
            PRList.CopyTo(PRArray);
            foreach (ProductionRule item in PRArray)
            {
                //若某个产生式可推导出空串，则设置对应derivableToEpsilon[]为'是'
                if (item.right.Contains("$"))
                {
                    derivableToEpsilon[Program.globalGrammar.nonTerminalChar.IndexOf(item.left)] = "是";
                    //删除左部为item.left非终结符的所有产生式
                    foreach (ProductionRule PR in PRArray)
                        if (PR.left == item.left)
                            PRList.Remove(PR);
                }
            }
            //MessageBox.Show(PRList.Count().ToString());
            //displayGrammarInfo();
            int loopCount = 0;
            while (!isCertain(derivableToEpsilon))
            {
                loopCount++;
                if (loopCount > 50)
                {
                    MessageBox.Show("存在非终结符推导出空串未定，请检查文法，否则后续程序将出现错误", "提示");
                    return false;
                }
                PRArray = new ProductionRule[PRList.Count()];
                PRList.CopyTo(PRArray);
                foreach (ProductionRule item in PRArray)
                {
                    int flag = 0;
                    foreach (char c in item.right)
                    {
                        //如果右部存在某个非终结符对应的导出空串不是 "是"，记flag=1
                        if (Program.globalGrammar.nonTerminalChar.Contains(c.ToString()) &&
                            derivableToEpsilon[Program.globalGrammar.nonTerminalChar.IndexOf(c.ToString())] != "是")
                            flag = 1;
                    }
                    //flag=0，说明所有item产生式的右部非终结符均可推导出空串，标记item的左部非终结符可推导出空串
                    if (flag == 0)
                    {
                        derivableToEpsilon[Program.globalGrammar.nonTerminalChar.IndexOf(item.left)] = "是";
                        //删除左部为item.left非终结符的所有产生式
                        foreach (ProductionRule PR in PRArray)
                            if (PR.left == item.left)
                                PRList.Remove(PR);
                    }
                }
                //displayPRList(PRList);
                PRArray = new ProductionRule[PRList.Count()];
                PRList.CopyTo(PRArray);
                //记录当前PRList中所有非终结符
                List<String> VnList = new List<String>();
                foreach (ProductionRule PR in PRList)
                    VnList.Add(PR.left);
                foreach (ProductionRule item in PRArray)
                {
                    foreach (char c in item.right)
                    {
                        //如果右部存在某个非终结符对应的导出空串为"否"，则将该产生式删除
                        if (Program.globalGrammar.nonTerminalChar.Contains(c.ToString()) &&
                            derivableToEpsilon[Program.globalGrammar.nonTerminalChar.IndexOf(c.ToString())] == "否")
                        {
                            PRList.Remove(item);
                            break;
                        }
                    }
                }
                //displayPRList(PRList);
                savedLeftNonTerminalChar = new List<String>();
                foreach (ProductionRule item in PRList)
                {
                    //MessageBox.Show(item.productionRuleInfo());
                    //将所有产生式此时左部的非终结符加入savedLeftNonTerminalChar中
                    savedLeftNonTerminalChar.Add(item.left);
                }
                //标记不在savedLeftNonTerminalChar中的非终结符不能推导出空串
                foreach (String Vn in VnList)
                    if (!savedLeftNonTerminalChar.Contains(Vn))
                        derivableToEpsilon[Program.globalGrammar.nonTerminalChar.IndexOf(Vn)] = "否";
                //displayDerivableToEpsilon(derivableToEpsilon);
            }
            //displayPRList(PRList);
            //displayDerivableToEpsilon(derivableToEpsilon);
            //返回非终结符VnChar能否推导出空串
            return (derivableToEpsilon[Program.globalGrammar.nonTerminalChar.IndexOf(VnChar)] == "是");
        }
        //函数功能：判断符号串(V*)能否推出空串epsilon
        private bool isDerivableToEpsilon_String(String Vstar)
        {
            //只有Vstar字符串中所有字符均可推出空串，Vstar方能推出空串
            foreach (char item in Vstar)
                if (!isDerivableToEpsilon_Char(item.ToString()))
                    return false;
            return true;
        }
        //函数功能：判断derivableToEpsilon表中是否有不确定项目
        private bool isCertain(String[] derivableToEpsilon)
        {
            foreach (String item in derivableToEpsilon)
                if (item == "未定")
                    return false;
            return true;
        }
        //函数功能：derivableToEpsilon输出测试
        private void displayDerivableToEpsilon(String[] derivableToEpsilon)
        {
            String s = "";
            foreach (String item in Program.globalGrammar.nonTerminalChar)
                s += (item + "\t");
            s += "\n";
            foreach (String item in derivableToEpsilon)
                s += (item + "\t");
            MessageBox.Show(s);
        }
        //函数功能：PRList输出测试
        private void displayPRList(List<ProductionRule> PRList)
        {
            String s = "";
            foreach (ProductionRule PR in PRList)
                s = s + PR.productionRuleInfo() + "\n";
            MessageBox.Show(s);
        }
        //函数功能：判断两个集合内元素是否相等
        private bool isEqual(List<String> L1, List<String> L2)
        {
            if (L1.Count() == 0 || L2.Count() == 0)
                return false;
            foreach (String item1 in L1)
                foreach (String item2 in L2)
                    if (item1 != item2)
                        return false;
            return true;
        }
        //函数功能：输出字符串数组/链表
        private void displayStringArray(String[] StringArray)
        {
            String s = "";
            foreach (String item in StringArray)
                s = s + (item + "\n");
            MessageBox.Show(s);
        }
        //函数功能：输出int数组/链表
        private void displayIntArray(int[] IntArray)
        {
            String s = "";
            foreach (int item in IntArray)
                s = s + (item.ToString() + "\n");
            MessageBox.Show(s);
        }
        //函数功能：获取符号串的First集（20181206完成）
        private String[] getFirstSetOfString(String s)
        {
            List<String> firstSet = new List<String>();
            //将符号串每个字符存储在splitCharArray中，记为X1X2X3X4...
            char[] splitCharArray = s.ToCharArray();
            //当X1不能推出空串时，firstSet(s)=firstSet(X1)
            if (!isDerivableToEpsilon_Char(splitCharArray[0].ToString()))
            {
                firstSet = getFirstSetFromTable(splitCharArray[0].ToString()).ToList();
                return firstSet.ToArray();
            }
            firstSet = new List<String>();
            List<String> savedCharArray = new List<String>();
            //保存firstSei集中包含空串的符号(一旦检测到不包含空串，则不再存入)
            foreach (char c in splitCharArray)
                if (getFirstSetFromTable(c.ToString()).Contains("$"))
                    savedCharArray.Add(c.ToString());
                else break;
            //savedCharArray与splitCharArray集合元素相等代表符号串的所有字符的firstSet集均包含空串$，此时进行以下操作
            if (savedCharArray.Count() == splitCharArray.Count())
            {
                foreach (String item in savedCharArray)
                {
                    foreach (String temp in getFirstSetFromTable(item))
                        if (!firstSet.Contains(temp))   //保证元素不重复
                            firstSet.Add(temp);
                    //displayStringArray(firstSet.ToArray());
                }
                if (!firstSet.Contains("$"))
                    firstSet.Add("$");
                return firstSet.ToArray();
            }
            //否则进行以下操作
            else
            {
                foreach (String item in savedCharArray)
                {
                    foreach (String temp in getFirstSetFromTable(item))
                        if (!firstSet.Contains(temp))   //保证元素不重复
                            firstSet.Add(temp);
                    firstSet.Remove("$");
                }
                foreach (String temp in getFirstSetFromTable(splitCharArray[savedCharArray.Count()].ToString()))
                    if (!firstSet.Contains(temp))   //保证元素不重复
                        firstSet.Add(temp);
                return firstSet.ToArray();
            }
        }
        //函数功能：生成所有非终结符VnChar的First集
        //为了防止无限递归，将每个非终结符的firstSet集保存下来，如果集合不再变化，则算法终止(follow集采用同样的算法)
        private void generateFirstSet()
        {
            initalGlobalVariables();//初始化(实例化)全局变量（First集、Follow集、Select集、预测分析表）
            bool flag = true;
            //获取全局文法的产生式规则集合
            List<ProductionRule> PRList = Program.globalGrammar.productionRule;
            //获取全局文法的终结符集
            List<String> terminalChar = Program.globalGrammar.terminalChar;
            while (flag)
            {
                flag = false;
                //构造FirstSet前，每个VnChar的First集大小
                int[] oldFirstSetSize = getSizeOfFirstSet();
                //对于每个非终结符VnChar生成其FirstSet
                foreach (String VnChar in Program.globalGrammar.nonTerminalChar)
                {
                    //MessageBox.Show(VnChar);
                    //规则②：存在产生式X->a...,a(-VT,则a(-First(X)
                    foreach (ProductionRule item in PRList)
                    {
                        if (item.left == VnChar && terminalChar.Contains(item.right[0].ToString()))
                            if (!getFirstSetFromTable(VnChar).Contains(item.right[0].ToString()))
                                addOneRecordToFirstSet(VnChar, item.right[0].ToString());
                    }
                    //规则③：如果可推导空串epsilon，则将空串加入到First(X)中
                    if (isDerivableToEpsilon_Char(VnChar))
                        if (!getFirstSetFromTable(VnChar).Contains("$"))
                            addOneRecordToFirstSet(VnChar, "$");
                    //规则④：产生式规则PR右部全是非终结符且均可推导出空串epsilon，则进行以下操作:
                    //将各个非终结符的firstSet集去除空串后加入到VnChar的first集中
                    foreach (ProductionRule PR in PRList)
                    {
                        //以VnChar为左部
                        if (PR.left == VnChar)
                        {
                            //int vnFlag = 0;
                            char[] arrayRight = PR.right.ToCharArray();
                            //课本上的算法有点问题，实际上不需要保证右部均是非终结符
                            //foreach (char c in arrayRight)
                            //{
                            //    //一旦检测到非终结符则置flag=1，并退出
                            //    if (!Char.IsUpper(c))
                            //    {
                            //        vnFlag = 1;
                            //        break;
                            //    }
                            //}
                            //flag==0代表产生式规则PR右部全是非终结符
                            //if (vnFlag == 0)
                            //{
                                List<char> savedCharList = new List<char>();
                                foreach (char c in arrayRight)
                                {
                                    //将能够推出空串的非终结符保存在savedCharList中(一旦检测到终结符，则退出循环，之后的非终结符也不存入)
                                    if (isDerivableToEpsilon_Char(c.ToString()))
                                    {
                                        savedCharList.Add(c);
                                    }
                                    else break;
                                }
                                //(savedCharList.Count()==arrayRight.Length)代表全部非终结符均可推导出空串
                                if (savedCharList.Count() == arrayRight.Length)
                                {
                                    foreach (char c in savedCharList)
                                    {
                                        List<String> firstSetOfc = getFirstSetFromTable(c.ToString()).ToList();
                                        foreach (String item in firstSetOfc)
                                            if (!getFirstSetFromTable(VnChar).Contains(item))
                                                addOneRecordToFirstSet(VnChar, item);
                                    }
                                    if (!getFirstSetFromTable(VnChar).Contains("$"))
                                        addOneRecordToFirstSet(VnChar, "$");
                                }
                                else
                                {
                                    foreach (char c in savedCharList)
                                    {
                                        List<String> firstSetOfc = getFirstSetFromTable(c.ToString()).ToList();
                                        firstSetOfc.Remove("$");
                                        foreach (String item in firstSetOfc)
                                            if (!getFirstSetFromTable(VnChar).Contains(item))
                                                addOneRecordToFirstSet(VnChar, item);
                                    }
                                    foreach (String item in getFirstSetFromTable(arrayRight[savedCharList.Count()].ToString()).ToList())
                                        if (!getFirstSetFromTable(VnChar).Contains(item))
                                            addOneRecordToFirstSet(VnChar, item);
                                }
                            }
                        //}
                    }
                }
                int[] newFirstSetSize = getSizeOfFirstSet();//构造FirstSet后，VnChar的First集大小
                //displayIntArray(newFirstSetSize);
                //displayIntArray(oldFirstSetSize);
                for (int i = 0; i < Program.globalFirstSet.nonTerminalChar.Length; i++)
                {
                    //一旦检测到集合不再变化，则退出循环，构造firstSet集完毕
                    if (newFirstSetSize[i] != oldFirstSetSize[i])
                    {
                        flag = true;
                        break;
                    }
                }
            }
        }
        //函数功能：生成所有非终结符VnChar的Follow集
        //为了防止无限递归，将每个非终结符的followSet集保存下来，如果集合不再变化，则算法终止(first集采用同样的算法)
        private void generateFollowSet()
        {
            bool flag = true;
            //将句子结束标记#加入到开始符号的followSet中
            addOneRecordToFollowSet(Program.globalGrammar.startChar, "#");
            while (flag)
            {
                flag = false;
                //构造FollowSet前，每个VnChar的Follow集大小
                int[] oldFollowSetSize = getSizeOfFollowSet();
                //对文法的每个非终结符构造其FollowSet集
                foreach (String VnChar in Program.globalGrammar.nonTerminalChar)
                {
                    //遍历每个产生式
                    foreach (ProductionRule PR in Program.globalGrammar.productionRule)
                    {
                        //MessageBox.Show(PR.productionRuleInfo());
                        //记录产生式PR右部中，位于VnChar后面的符号串(由V*构成)
                        List<String> stringAfterVnChar = new List<string>();
                        String PRRight = new String(PR.right.ToCharArray());
                        //MessageBox.Show(PRRight);
                        foreach (char item in PRRight.Reverse())
                        {
                            if (item.ToString() != VnChar)//注意item!='$'条件！考虑到A->$类型的产生式
                                stringAfterVnChar.Add(item.ToString());
                            else break;
                        }
                        stringAfterVnChar.Reverse();
                        //将stringAfterVnChar转换为String beta
                        String beta = "";
                        foreach (String item in stringAfterVnChar)
                            beta += item;
                        //beta.Length < PR.right.Length，说明PR右部出现了VnChar
                        if (beta.Length == 0)
                            beta = "$";
                        if (beta.Length < PR.right.Length || (beta == "$" && PR.right != "$"))//注意加上beta == "$"判断条件
                        {
                            //MessageBox.Show(PR.productionRuleInfo());
                            //MessageBox.Show(beta);
                            //将beta的firstSet集的非空($)元素加入到followSet(VnChar)中
                            foreach (String item in getFirstSetOfString(beta))
                                if (!getFollowSetFromTable(VnChar).Contains(item) && item != "$")
                                    addOneRecordToFollowSet(VnChar, item);
                            //如果beta能推出空串，则将followSet(PR左部)也加入到followSet(VnChar)中
                            //if (PR.right == "C" && VnChar == "C")
                            //{
                            //    displayGrammarInfo();
                            //    MessageBox.Show("PRLeft: "+PR.left);
                            //    displayStringArray(getFollowSetFromTable(PR.left));
                            //}
                            if (isDerivableToEpsilon_String(beta))
                            {
                                foreach (String item in getFollowSetFromTable(PR.left))
                                    if (!getFollowSetFromTable(VnChar).Contains(item))
                                        addOneRecordToFollowSet(VnChar, item);
                            }
                        }
                    }
                }
                int[] newFollowSetSize = getSizeOfFollowSet();//构造FollowSet后，VnChar的Follow集大小
                for (int i = 0; i < Program.globalFollowSet.nonTerminalChar.Length; i++)
                {
                    //一旦检测到集合未变化，则退出循环，构造followSet集完毕
                    if (newFollowSetSize[i] != oldFollowSetSize[i])
                    {
                        flag = true;
                        break;
                    }
                }

            }
        }
        //函数功能：生成文法Grammar各个产生式的SelectSet选择集合
        private void generateSelectSet()
        {
            foreach (ProductionRule PR in Program.globalSelectSet.productionRule)
            {
                //如果right不能推出空串，则select(PR)=first(right)
                if (!isDerivableToEpsilon_String(PR.right))
                {
                    foreach (String VtChar in getFirstSetOfString(PR.right))
                        addOneRecordToSelectSet(PR, VtChar);
                }
                else //如果right能推出空串，则select(PR)={first(right)-$}∪follow(left)
                {
                    foreach (String VtChar in getFirstSetOfString(PR.right))
                    {
                        if (VtChar != "$")
                            addOneRecordToSelectSet(PR, VtChar);
                    }
                    foreach (String VtChar in getFollowSetFromTable(PR.left))
                        addOneRecordToSelectSet(PR, VtChar);
                }
            }
        }
        //函数功能：获取当前每个非终结符VnChar的FollowSet集合大小(返回int类型数组)
        private int[] getSizeOfFollowSet()
        {
            int[] FollowSetSize = new int[Program.globalFollowSet.nonTerminalChar.Length];
            //构造FollowSet前，每个VnChar的Follow集大小
            for (int i = 0; i < FollowSetSize.Length; i++)
                FollowSetSize[i] = getFollowSetFromTable(Program.globalFollowSet.nonTerminalChar[i]).Length;
            return FollowSetSize;
        }
        //函数功能：获取当前每个非终结符VnChar的FirstSet集合大小(返回int类型数组)
        private int[] getSizeOfFirstSet()
        {
            int[] FirstSetSize = new int[Program.globalFirstSet.nonTerminalChar.Length];
            //构造FirstSet前，每个VnChar的First集大小
            for (int i = 0; i < FirstSetSize.Length; i++)
                FirstSetSize[i] = getFirstSetFromTable(Program.globalFirstSet.nonTerminalChar[i]).Length;
            return FirstSetSize;
        }
        //生成FirstSet集合按钮单击事件
        private void generateFirstSetButtonClick(object sender, EventArgs e)
        {
            Program.globalFirstSet = new FirstSet(Program.globalGrammar.nonTerminalChar.ToArray(), Program.globalGrammar.terminalChar.ToArray());
            //对于文法Grammar中的所有非终结符，求其First集
            generateFirstSet();
            //MessageBox.Show(Program.globalFirstSet.firstSetInfo());
            listViewFirstSet.Columns.Clear();
            listViewFirstSet.Items.Clear();
            //添加表头(终结符)
            listViewFirstSet.Columns.Add("First集", 75, HorizontalAlignment.Center);
            foreach (String Vt in Program.globalFirstSet.terminalChar)
                listViewFirstSet.Columns.Add(Vt, 55, HorizontalAlignment.Center);
            //将First集信息显示在FirstSet表中
            foreach (String Vn in Program.globalFirstSet.nonTerminalChar)
            {
                ListViewItem item = new ListViewItem();
                item.Text = Vn;
                foreach (String Vt in Program.globalFirstSet.terminalChar)
                {
                    int row = findInStringArray(Program.globalFirstSet.nonTerminalChar, Vn);
                    int column = findInStringArray(Program.globalFirstSet.terminalChar, Vt);
                    //添加一条first集记录
                    item.SubItems.Add(Program.globalFirstSet.firstSetTable[row, column]);
                }
                listViewFirstSet.Items.Add(item);
            }
        }
        //生成Follow集合按钮单击事件
        private void generateFollowSetButtonClick(object sender, EventArgs e)
        {
            Program.globalFirstSet = new FirstSet(Program.globalGrammar.nonTerminalChar.ToArray(), Program.globalGrammar.terminalChar.ToArray());
            Program.globalFollowSet = new FollowSet(Program.globalGrammar.nonTerminalChar.ToArray(), Program.globalGrammar.terminalChar.ToArray());
            //对于文法Grammar中的所有非终结符，求其First集
            generateFirstSet();
            //生成文法Grammar的followSet集合
            generateFollowSet();
            //MessageBox.Show(Program.globalFollowSet.followSetInfo());
            listViewFollowSet.Columns.Clear();
            listViewFollowSet.Items.Clear();
            //添加表头(终结符)
            listViewFollowSet.Columns.Add("Follow集", 75, HorizontalAlignment.Center);
            foreach (String Vt in Program.globalFollowSet.terminalChar)
                listViewFollowSet.Columns.Add(Vt, 55, HorizontalAlignment.Center);
            //将Follow集信息显示在FollowSet表中
            foreach (String Vn in Program.globalFollowSet.nonTerminalChar)
            {
                ListViewItem item = new ListViewItem();
                item.Text = Vn;
                foreach (String Vt in Program.globalFollowSet.terminalChar)
                {
                    int row = findInStringArray(Program.globalFollowSet.nonTerminalChar, Vn);
                    int column = findInStringArray(Program.globalFollowSet.terminalChar, Vt);
                    //添加一条follow集记录
                    item.SubItems.Add(Program.globalFollowSet.followSetTable[row, column]);
                }
                listViewFollowSet.Items.Add(item);
            }
        }
        //生成预测分析表按钮单击事件(借助SelectSet集)
        private void createPredictionTableClick(object sender, EventArgs e)
        {
            Program.globalSelectSet = new SelectSet(Program.globalGrammar.productionRule.ToArray(), Program.globalGrammar.nonTerminalChar.ToArray(), Program.globalGrammar.terminalChar.ToArray());
            Program.globalPredictionAnalyzeTable = new PredictionAnalyzeTable(Program.globalGrammar.nonTerminalChar.ToArray(), Program.globalGrammar.terminalChar.ToArray());
            if (listViewFirstSet.Items.Count == 0)
            {
                MessageBox.Show("请先生成First集", "提示");
                return;
            }
            if (listViewFollowSet.Items.Count == 0)
            {
                MessageBox.Show("请先生成Follow集", "提示");
                return;
            }
            //首先生成文法Grammar各产生式的Select集合
            generateSelectSet();
            //遍历每个产生式(的Select集合)
            foreach (ProductionRule PR in Program.globalSelectSet.productionRule)
            {
                //向预测分析表PredictionAnalyzeTable中添加一条记录
                foreach (String VtChar in getSelectSetFromTable(PR))
                    addOneRecordToPredictionAnalyzeTable(PR.left, VtChar, PR);
            }
            listViewPredictionTable.Columns.Clear();
            listViewPredictionTable.Items.Clear();
            //添加表头(终结符)
            listViewPredictionTable.Columns.Add("预测分析表", 90, HorizontalAlignment.Center);
            foreach (String Vt in Program.globalPredictionAnalyzeTable.terminalChar)
                listViewPredictionTable.Columns.Add(Vt, 100, HorizontalAlignment.Center);
            //将Select集信息显示在预测分析表PredictionAnalyzeTable中
            foreach (String Vn in Program.globalPredictionAnalyzeTable.nonTerminalChar)
            {
                ListViewItem item = new ListViewItem();
                item.Text = Vn;
                foreach (String Vt in Program.globalPredictionAnalyzeTable.terminalChar)
                {
                    int row = findInStringArray(Program.globalPredictionAnalyzeTable.nonTerminalChar, Vn);
                    int column = findInStringArray(Program.globalPredictionAnalyzeTable.terminalChar, Vt);
                    //添加一条select集记录
                    item.SubItems.Add(Program.globalPredictionAnalyzeTable.predictionAnalyzeTable[row, column]);
                }
                listViewPredictionTable.Items.Add(item);
            }
        }
        //一键生成预测分析表按钮、告知分析结果单击事件
        private void directlyToResultButtonClick(object sender, EventArgs e)
        {
            //若非LL1文法，则无法进行句子分析（无法保证结果正确性）
            if (!isLL1Type)
            {
                MessageBox.Show("当前文法非LL1类型，无法保证句子分析结果的正确性！", "警告");
                return;
            }
            //用于填充分析结果表格
            listViewAnalyseResult.Items.Clear();
            if (listViewPredictionTable.Items.Count == 0)
            {
                MessageBox.Show("请先生成预测分析表", "提示");
                return;
            }
            String sentence = textBoxSentence.Text.Replace(" ", "");
            if (sentence == "")
            {
                MessageBox.Show("请输入待分析句子后操作", "提示");
                return;
            }
            ListViewItem[] itemList = createAnalyzeProcedure(sentence);
            foreach (ListViewItem item in itemList)
                listViewAnalyseResult.Items.Add(item);
            //根据返回过程项目判断句子是否分析成功
            if (itemList[itemList.Length - 1].SubItems[3].Text == "接受")
                MessageBox.Show("分析成功！句子" + sentence.Trim('#') + "是该文法的一个句子", "提示");
            else
                MessageBox.Show("分析失败！句子" + sentence.Trim('#') + "不是该文法的一个句子", "提示");
        }
        //单步填充预测分析表按钮单击事件
        private void stepByStepButtonClick(object sender, EventArgs e)
        {
            //若非LL1文法，则无法进行句子分析（无法保证结果正确性）
            if (!isLL1Type)
            {
                MessageBox.Show("当前文法非LL1类型，无法保证句子分析结果的正确性！", "警告");
                return;
            }
            //用于填充分析结果表格
            if (listViewPredictionTable.Items.Count == 0)
            {
                MessageBox.Show("请先生成预测分析表", "提示");
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
                if (itemList[itemList.Length - 1].SubItems[3].Text == "接受")
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
        //函数功能：构造句子分析过程，返回分析过程中产生的listView项目
        private ListViewItem[] createAnalyzeProcedure(String sentence)
        {
            List<ListViewItem> itemList = new List<ListViewItem>();
            sentence += "#";//向句子末尾添加句子结束标记#
            Stack<String> stateStack = new Stack<String>();//状态栈，存放当前分析过程，由V*和#构成
            int pointer = 0;//代表指向输入符号串(待分析句子)的指针
            int stepCount = 1;//代表当前步骤编号
            stateStack.Push("#");//#入栈
            stateStack.Push(Program.globalGrammar.startChar);//文法开始符号入栈
            //项目0类
            ListViewItem item0 = new ListViewItem();
            item0.Text = stepCount.ToString();
            item0.SubItems.Add(getCurrentStateStackInfo(stateStack));
            item0.SubItems.Add(getCurrentLeftSentence(sentence, pointer));
            item0.SubItems.Add("Initial-State");
            itemList.Add(item0);
            while (true)
            {
                String X = stateStack.Peek();//栈顶符号放入X
                char a = sentence[pointer];//当前指向符号放入a
                //判断X是否为终结符
                if (Program.globalGrammar.terminalChar.Contains(X))
                {
                    //匹配
                    if (X == a.ToString())
                    {
                        pointer++;//指针后移一位
                        stateStack.Pop();//出栈
                        stepCount++;//匹配：步骤数+1
                        ListViewItem item1 = new ListViewItem();
                        //项目1类
                        item1.Text = stepCount.ToString();
                        item1.SubItems.Add(getCurrentStateStackInfo(stateStack));
                        item1.SubItems.Add(getCurrentLeftSentence(sentence, pointer));
                        item1.SubItems.Add("匹配！");
                        itemList.Add(item1);
                        continue;
                    }
                    else
                    {
                        //MessageBox.Show("1-分析失败！句子" + sentence.Trim('#') + "不是该文法的一个句子", "提示");
                        return itemList.ToArray();
                    }
                }
                else
                {
                    if (X == "#")
                    {
                        if (X == a.ToString())
                        {
                            stepCount++;//接受：步骤数+1
                            //项目2类
                            ListViewItem item2 = new ListViewItem();
                            item2.Text = stepCount.ToString();
                            item2.SubItems.Add(getCurrentStateStackInfo(stateStack));
                            item2.SubItems.Add(getCurrentLeftSentence(sentence, pointer));
                            item2.SubItems.Add("接受");
                            itemList.Add(item2);
                            //MessageBox.Show("分析成功！句子" + sentence.Trim('#') + "是该文法的一个句子", "提示");
                            return itemList.ToArray();
                        }
                        else
                        {
                            //MessageBox.Show("2-分析失败！句子" + sentence.Trim('#') + "不是该文法的一个句子", "提示");
                            return itemList.ToArray();
                        }
                    }
                    else //X!="#"
                    {
                        ProductionRule PR = getCertainProductionRule(X, a.ToString());
                        //这里涵盖了句子出现非法字符的情况，对应PR为空
                        if (PR != null)
                        {
                            stateStack.Pop();//出栈
                            if (PR.left == X)
                            {
                                foreach (char c in PR.right.Reverse())
                                {
                                    if (c != '$')    //切记右部为空串时，空串不进入状态栈！
                                        stateStack.Push(c.ToString());
                                    continue;
                                }
                                stepCount++;//推导：步骤数+1
                                //项目3类
                                ListViewItem item3 = new ListViewItem();
                                item3.Text = stepCount.ToString();
                                item3.SubItems.Add(getCurrentStateStackInfo(stateStack));
                                item3.SubItems.Add(getCurrentLeftSentence(sentence, pointer));
                                item3.SubItems.Add(PR.productionRuleInfo());
                                itemList.Add(item3);
                            }
                        }
                        else
                        {
                            //MessageBox.Show("X a: " + X.ToString() + " " + a.ToString());
                            //MessageBox.Show("3-分析失败！句子" + sentence.Trim('#') + "不是该文法的一个句子", "提示");
                            return itemList.ToArray();
                        }
                    }
                }
            }
        }
        //函数功能：返回当前状态栈stateStack信息
        private String getCurrentStateStackInfo(Stack<String> stateStack)
        {
            String[] itemArray = stateStack.ToArray();
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
        private void exitButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }
        //一旦检测到文法输入框内容更改，隐藏操作按钮，阻止用户进一步操作，同时初始化所有listView控件
        private void grammarChanged(object sender, EventArgs e)
        {
            initalGlobalVariables();//初始化(实例化)全局变量（First集、Follow集、Select集、预测分析表）
            buttonSaveFile.Visible = false;
            buttonGenerateFirstSet.Visible = false;
            buttonGenerateFollowSet.Visible = false;
            buttonDirectlyToResult.Visible = false;
            buttonStepByStep.Visible = false;
            buttonCreatePredictionTable.Visible = false;
            resetListViewLayout();//重置表格布局
        }
        //函数功能：判断给定的文法G是否为LL1类型
        private bool isLL1TypeGrammar(Grammar G)
        {
            //判断是否为LL1文法前，需要先生成SelectSet集(为了与之后的操作冲突，后面会恢复)
            generateFirstSet();
            generateFollowSet();
            generateSelectSet();
            foreach (String VnChar in Program.globalGrammar.nonTerminalChar)
            {
                //用于保存相同左部的产生式的Select集合
                List<String> selectSetOfSameLeftPR = new List<String>();
                foreach (ProductionRule PR in Program.globalGrammar.productionRule)
                {
                    //保存相同左部的产生式的Select集合
                    if (PR.left == VnChar)
                    {
                        foreach (String VtChar in getSelectSetFromTable(PR))
                        {
                            //一旦已存在VtChar，则必有交集，非LL1文法
                            if (selectSetOfSameLeftPR.Contains(VtChar))
                            {
                                isLL1Type = false;//标记当前文法为非LL1文法
                                this.Text = "LL(1)预测分析";
                                this.Text += "  (当前Grammar非LL1类型文法)";
                                initalGlobalVariables();//恢复全局变量（First集、Follow集、Select集、预测分析表）
                                return false;
                            }
                            selectSetOfSameLeftPR.Add(VtChar);
                        }
                    }
                }
                //判断selectSetOfSameLeftPR中是否有重复元素

            }
            isLL1Type = true;//标记当前文法为LL1文法
            this.Text = "LL(1)预测分析";
            this.Text += "  (当前Grammar为LL1类型文法)";
            initalGlobalVariables();//恢复全局变量（First集、Follow集、Select集、预测分析表）
            return true;
        }
        //函数功能：初始化全局变量
        private void initalGlobalVariables()
        {
            try
            {
                //赋值全局文法变量
                Program.globalGrammar = getSplitedGrammar(StringToGrammer(editBoxGrammer.Text.Replace(" ", "")));
                //displayGrammarInfo();
                //实例化全局变量（First集、Follow集、Select集、预测分析表）
                Program.globalFirstSet = new FirstSet(Program.globalGrammar.nonTerminalChar.ToArray(), Program.globalGrammar.terminalChar.ToArray());
                Program.globalFollowSet = new FollowSet(Program.globalGrammar.nonTerminalChar.ToArray(), Program.globalGrammar.terminalChar.ToArray());
                Program.globalSelectSet = new SelectSet(Program.globalGrammar.productionRule.ToArray(), Program.globalGrammar.nonTerminalChar.ToArray(), Program.globalGrammar.terminalChar.ToArray());
                Program.globalPredictionAnalyzeTable = new PredictionAnalyzeTable(Program.globalGrammar.nonTerminalChar.ToArray(), Program.globalGrammar.terminalChar.ToArray());
            }
            catch (Exception) { }
        }
        //函数功能：重置表格布局
        private void resetListViewLayout()
        {
            listViewFirstSet.Clear();
            listViewFirstSet.Columns.Add("First集", 75, HorizontalAlignment.Center);
            listViewFollowSet.Clear();
            listViewFollowSet.Columns.Add("Follow集", 75, HorizontalAlignment.Center);
            listViewPredictionTable.Clear();
            listViewPredictionTable.Columns.Add("预测分析表", 90, HorizontalAlignment.Center);
            listViewAnalyseResult.Clear();
            listViewAnalyseResult.Columns.Add("步骤", 50, HorizontalAlignment.Center);
            listViewAnalyseResult.Columns.Add("符号栈", 100, HorizontalAlignment.Left);
            listViewAnalyseResult.Columns.Add("输入串", 100, HorizontalAlignment.Left);
            listViewAnalyseResult.Columns.Add("所用产生式", 150, HorizontalAlignment.Left);
        }
        //文件保存按钮单击事件
        private void saveFileButtonClick(object sender, EventArgs e)
        {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.Title = "保存";
            SFD.Filter = "LL1-AnalyseResult File|*.result;*.txt";
            SFD.ShowDialog();
            String savePath = SFD.FileName; //获取打开文件的路径
            if (savePath == "")
                return;
            FileStream saveFileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write);
            StringBuilder SB = new StringBuilder();
            int rowNum = 0;
            int columnNum = 0;
            SB.Append("LL(1)自顶向下预测分析详细信息");
            SB.Append("\r\n");
            SB.Append("\r\n");
            //保存当前文法
            SB.Append("文法内容");
            if (isLL1Type)
                SB.Append(" (LL1型文法)");
            else
                SB.Append(" (非LL1型文法)");
            SB.Append("\r\n");
            SB.Append(editBoxGrammer.Text);
            SB.Append("\r\n");
            SB.Append("\r\n");
            //保存当前文法的FirstSet集
            SB.Append("First集");
            SB.Append("\r\n");
            rowNum = listViewFirstSet.Items.Count;
            columnNum = listViewFirstSet.Columns.Count;
            if (rowNum == 0)
            {
                SB.Append("未构造First集！");
                SB.Append("\r\n");
            }
            else
            {
                SB.Append("\t");
                for (int i = 1; i < columnNum; i++)
                {
                    SB.Append(listViewFirstSet.Columns[i].Text);
                    SB.Append("\t");
                }
                SB.Append("\r\n");
                for (int i = 0; i < rowNum; i++)
                {
                    for (int j = 0; j < columnNum; j++)
                    {
                        SB.Append(listViewFirstSet.Items[i].SubItems[j].Text);
                        SB.Append("\t");
                    }
                    SB.Append("\r\n");
                }
            }
            SB.Append("\r\n");
            //保存当前文法的FollowSet集
            SB.Append("Follow集");
            SB.Append("\r\n");
            rowNum = listViewFollowSet.Items.Count;
            columnNum = listViewFollowSet.Columns.Count;
            if (rowNum == 0)
            {
                SB.Append("未构造First集！");
                SB.Append("\r\n");
            }
            else
            {
                SB.Append("\t");
                for (int i = 1; i < columnNum; i++)
                {
                    SB.Append(listViewFollowSet.Columns[i].Text);
                    SB.Append("\t");
                }
                SB.Append("\r\n");
                for (int i = 0; i < rowNum; i++)
                {
                    for (int j = 0; j < columnNum; j++)
                    {
                        SB.Append(listViewFollowSet.Items[i].SubItems[j].Text);
                        SB.Append("\t");
                    }
                    SB.Append("\r\n");
                }
            }
            SB.Append("\r\n");
            //保存当前文法的预测分析表
            SB.Append("LL1预测分析表");
            SB.Append("\r\n");
            rowNum = listViewPredictionTable.Items.Count;
            columnNum = listViewPredictionTable.Columns.Count;
            if (rowNum == 0)
            {
                SB.Append("未构造LL1预测分析表！");
                SB.Append("\r\n");
            }
            else
            {
                SB.Append("\t");
                for (int i = 1; i < columnNum; i++)
                {
                    SB.Append(listViewPredictionTable.Columns[i].Text);
                    SB.Append("\t");
                }
                SB.Append("\r\n");
                for (int i = 0; i < rowNum; i++)
                {
                    for (int j = 0; j < columnNum; j++)
                    {
                        SB.Append(listViewPredictionTable.Items[i].SubItems[j].Text);
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
    }
}