using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 编译原理辅助系统
{
    //正规式r(RegularExpression)转化为等价的NFA——辅助类
    class REtoNFA_Helper
    {
        static int stateID = 0;
        //计算 a 的NFA
        public static NFA NFAofA(char a)
        {
            NFA newNFA = new NFA();
            List<String> startState = new List<String>();
            List<String> endState = new List<String>();
            List<String> inputChar = new List<String>();
            List<transformFunction> TF = new List<transformFunction>();
            inputChar.Add(a.ToString());
            startState.Add(stateID.ToString());
            stateID++;
            endState.Add(stateID.ToString());
            stateID++;
            TF.Add(new transformFunction(startState[0], a.ToString(), endState[0]));
            //更新NFA
            newNFA.NFA_START = startState.ToArray();
            newNFA.NFA_END = endState.ToArray();
            newNFA.NFA_INPUT = inputChar.ToArray();
            newNFA.NFA_Transform = TF.ToArray();
            return newNFA;
        }
        //计算 a* 的NFA
        public static NFA NFAofA_Clouse(NFA A)
        {
            NFA newNFA = new NFA();
            List<String> startState = new List<String>();
            List<String> endState = new List<String>();
            List<String> inputChar = new List<String>();
            List<transformFunction> TF = new List<transformFunction>();
            inputChar = A.NFA_INPUT.ToList();//输入符号与输入的NFA A相同
            startState.Add(stateID.ToString());
            stateID++;
            endState.Add(stateID.ToString());
            stateID++;
            TF = A.NFA_Transform.ToList();//保留原NFA A的转换函数
            //在原NFA A的转换函数基础上添加转换函数
            TF.Add(new transformFunction(startState[0],"$",endState[0]));
            TF.Add(new transformFunction(startState[0], "$", A.NFA_START[0]));
            TF.Add(new transformFunction(A.NFA_END[0], "$", A.NFA_START[0]));
            TF.Add(new transformFunction(A.NFA_END[0], "$", endState[0]));
            //更新NFA
            newNFA.NFA_START = startState.ToArray();
            newNFA.NFA_END = endState.ToArray();
            newNFA.NFA_INPUT = inputChar.ToArray();
            newNFA.NFA_Transform = TF.ToArray();
            return newNFA;
        }
        //计算 a|b 的NFA
        public static NFA NFAofA_Union_B(NFA A, NFA B)
        {
            NFA newNFA = new NFA();
            List<String> startState = new List<String>();
            List<String> endState = new List<String>();
            List<String> inputChar = new List<String>();
            List<transformFunction> TF = new List<transformFunction>();
            inputChar = A.NFA_INPUT.ToList();//先把A的输入符号加入新的NFA输入符号集合
            foreach (String item in B.NFA_INPUT)
                if (!inputChar.Contains(item)) //若B的输入符号不在A中，则加入新的NFA输入符号集合
                    inputChar.Add(item);
            startState.Add(stateID.ToString());
            stateID++;
            endState.Add(stateID.ToString());
            stateID++;
            TF = A.NFA_Transform.ToList();//先把A的转换函数加入新的NFA转换函数集合
            foreach (transformFunction item in B.NFA_Transform)
                if (!TF.Contains(item)) //若B的转换函数不在A中，则加入新的NFA转换函数集合
                    TF.Add(item);
            //在原NFA A的转换函数基础上添加转换函数
            TF.Add(new transformFunction(startState[0], "$", A.NFA_START[0]));
            TF.Add(new transformFunction(startState[0], "$", B.NFA_START[0]));
            TF.Add(new transformFunction(A.NFA_END[0], "$", endState[0]));
            TF.Add(new transformFunction(B.NFA_END[0], "$", endState[0]));
            //更新NFA
            newNFA.NFA_START = startState.ToArray();
            newNFA.NFA_END = endState.ToArray();
            newNFA.NFA_INPUT = inputChar.ToArray();
            newNFA.NFA_Transform = TF.ToArray();
            return newNFA;
        }
        //计算ab 的NFA
        public static NFA NFAofA_Join_B(NFA A, NFA B)
        {
            NFA newNFA = new NFA();
            List<String> startState = new List<String>();
            List<String> endState = new List<String>();
            List<String> inputChar = new List<String>();
            List<transformFunction> TF = new List<transformFunction>();
            inputChar = A.NFA_INPUT.ToList();//先把A的输入符号加入新的NFA输入符号集合
            foreach (String item in B.NFA_INPUT)
                if (!inputChar.Contains(item)) //若B的输入符号不在A中，则加入新的NFA输入符号集合
                    inputChar.Add(item);
            startState.Add(stateID.ToString());
            stateID++;
            endState.Add(stateID.ToString());
            stateID++;
            TF = A.NFA_Transform.ToList();//先把A的转换函数加入新的NFA转换函数集合
            foreach (transformFunction item in B.NFA_Transform)
                if (!TF.Contains(item)) //若B的转换函数不在A中，则加入新的NFA转换函数集合
                    TF.Add(item);
            //在原NFA A的转换函数基础上添加转换函数
            TF.Add(new transformFunction(A.NFA_END[0], "$", B.NFA_START[0]));
            TF.Add(new transformFunction(startState[0], "$", A.NFA_START[0]));
            TF.Add(new transformFunction(B.NFA_END[0], "$", endState[0]));
            //更新NFA
            newNFA.NFA_START = startState.ToArray();
            newNFA.NFA_END = endState.ToArray();
            newNFA.NFA_INPUT = inputChar.ToArray();
            newNFA.NFA_Transform = TF.ToArray();
            return newNFA;
        }
        //NFA运算测试（返回NFA信息）
        public static String NFATest()
        {
            stateID = 0;
            NFA A = NFAofA('a');
            NFA B = NFAofA('b');
            //NFA C = NFAofA_Union_B(A,B);
            NFA C = NFAofA_Join_B(A, B);
            return C.NFAInfo();
        }
        //正规式转NFA
        //测试样例：r=(a|b)*(aa|bb)(a|b)*
        public static NFA REtoNFA(String Expression)
        {
            int i = 0;
            stateID = 0;//初始化stateID为0
            Expression = addJoinOperator(Expression.Trim());
            Expression = postfixExpression(Expression);
            //Expression = "aa|*aa+b|b++ab|*+";
            //MessageBox.Show("生成的后缀表达式：\n"+Expression);
            Stack<NFA> NFAStack = new Stack<NFA>();
            NFA A = null, B = null;//用于临时保存NFA
            while (i < Expression.Length)
            {
                char currentChar = Expression[i];
                //如果当前字符为字母，则构造该字母的NFA，并将其入NFA栈
                if (isLetter(currentChar))
                    NFAStack.Push(NFAofA(currentChar));
                else
                {
                    switch (currentChar)
                    {
                        case '*':
                            A = NFAStack.Peek();
                            //MessageBox.Show(A.NFAInfo());
                            NFAStack.Pop();
                            NFAStack.Push(NFAofA_Clouse(A));
                            break;
                        case '|':
                            B = NFAStack.Peek();
                            //MessageBox.Show(B.NFAInfo());
                            NFAStack.Pop();
                            A = NFAStack.Peek();
                            //MessageBox.Show(A.NFAInfo());
                            NFAStack.Pop();
                            NFAStack.Push(NFAofA_Union_B(A, B));
                            break;
                        case '+':
                            B = NFAStack.Peek();
                            //MessageBox.Show(B.NFAInfo());
                            NFAStack.Pop();
                            A = NFAStack.Peek();
                            //MessageBox.Show(A.NFAInfo());
                            NFAStack.Pop();
                            NFAStack.Push(NFAofA_Join_B(A, B));
                            break;
                        default: break;
                    }
                }
                i++;
            }
            //栈顶NFA即为构造的最终NFA
            return NFAStack.Peek();
        }
        //比较运算符优先级
        public static String priorityCompare(char a, char b)
        {
            String OP = "+|()*#";
            int i = OP.IndexOf(a);
            int j = OP.IndexOf(b);
            int[,] compareMatrix = new int[6, 6]
            {
                {1,-1,-1,1,1,-1},
                {1,1,-1,1,1,-1},
                {-1,-1,-1,0,-1,-1},
                {1,1,0,1,1,-1},
                {-1,-1,-1,1,1,-1},
                {1,1,1,1,1,0}
            };
            int result = compareMatrix[i, j];
            if (result == 0)
                return "=";
            else if (result ==1)
                return ">";
            else if (result == -1)
                return "<";
            else return "unknown";
        }
        //添加连接符号(+)
        public static String addJoinOperator(String Expression)
        {
            List<char> newExpression = new List<char>();
            for (int i = 0; i < Expression.Length-1; i++)
            {
                char first=Expression[i];
                char second=Expression[i+1];
                newExpression.Add(Expression[i]);
                if (first != '(' && first != '|' && isLetter(second))
                    newExpression.Add('+');
                else if (second == '(' && first != '|' && first != '(')
                {
                    newExpression.Add('+');
                }
            }
            newExpression.Add(Expression[Expression.Length-1]);
                return new String(newExpression.ToArray());
        }
        //判断是否为字母
        public static bool isLetter(char c)
        {
            if ((c >= 'a' && c <= 'z'))
                return true;
            return false;
        }
        //将用户输入的中缀表达式转换为后缀表达式
        public static String postfixExpression(String Expression)
        {
            //设定e的最后一个符号为“#”，而其“#”一开始先放在栈s的栈底
            Expression += "#";
            Stack<char> charStack=new Stack<char>();
            char ch = '#', ch1, op;
            charStack.Push(ch);
            /*
             * 
             *  String OP = "+|()*#";
            int i = OP.IndexOf(a);
            int j = OP.IndexOf(b);
            int[,] compareMatrix = new int[6, 6]
            {
                {1,-1,-1,1,1,-1},
                {1,1,-1,1,1,-1},
                {-1,-1,-1,0,-1,-1},
                {1,1,0,1,1,-1},
                {-1,-1,-1,1,1,-1},
                {1,1,1,1,1,0}
            };
             */
            //读一个字符
            List<char> newExpression = new List<char>();
            int read_location = 0;
            ch = Expression[read_location];
            read_location++;
            while (charStack.Count()!=0)
            {
                if (isLetter(ch))
                {
                    newExpression.Add(ch);
                    ch = Expression[read_location];
                    read_location++;
                }
                else
                {
                    ch1 = charStack.Peek();
                    if (priorityCompare(ch,ch1)=="<")
                    {
                        charStack.Push(ch);
                        ch = Expression[read_location];
                        read_location++;
                    }
                    else if (priorityCompare(ch,ch1) == ">")
                    {
                        op = charStack.Peek();
                        charStack.Pop();
                        newExpression.Add(op);
                    }
                    else  //考虑优先级相等的情况
                    {
                        op = charStack.Peek();
                        charStack.Pop();
                        if (op == '(')
                        {
                            ch = Expression[read_location];
                            read_location++;
                        }
                    }
                }
            }
            return new String(newExpression.ToArray()); ;
        }
    }
}