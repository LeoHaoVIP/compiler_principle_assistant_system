using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 词法分析器
{
    class test
    {
        int j = 0;
        string token = "";                //记录识别出的单词   
        string wrong = "";                    //记录异常信息
        public string errorInfo = "";
        public string text4 = "";
        int flag1 = 0, flag2 = 0, flag3, flag4, flag5, flag6 = 0;                       //标记成对出现的界符
        string[] reservedWords = new string[32]{"auto", "double", "int","struct", "break", "else","long","switch","case","enum",
            "register","typedef","char","extern","return","union","const","float","short","unsigned","continue","for","signed","void","default",
        "goto","sizeof","volatile","do","if","while","static" };
        string[] operators = new string[28]{"+","-","*","/","%",">","<",">=","<=","==", "-=","+=","*=","/=",           //运算符
                           "!=","=","%=","&","&&","|","||","!","++","--","~","<<",">>","?:"};
        string[] bounds = new string[15] { "{", "}", "[", "]", ";", ",", ".", "(", ")", ":", "\"", "#", ">", "<", "\'" };              //界符


        string[] sourceCodeArray = null;
        public string analyzeResult = "";  //保存词法分析结果（行号、单词、单词类型、Token码）
        public string errorDetailInfo = "";      //保存错误信息（行号、错误段、错误信息）
        int errorCount = 0;                  //记录错误个数 
        public test(string[] sourceCodeArray)
        {
            this.sourceCodeArray = sourceCodeArray;
        }   //构造函数-传入指定源代码字符串数组（按行存储）
        public void analyzeRunner()
        {
            for(int i=0;i<sourceCodeArray.Length;i++)
                analyzeStep(sourceCodeArray[i],i+1);
        }
        private void analyzeStep(string rowCode,int row)    //row行号
        {
            int i = 0;//记录当前扫描指针所在位置
            string space = "";                         //空格数
            if (rowCode.Length == 0)                           //判断字符串是否为空
                return;
            try
            {
                while (rowCode[i] != '\0')  	//读入字符判断，空格、字母、数字、界符
                {
                    if (rowCode[i] == ' ' || rowCode[i] == '\t' || rowCode[i] == '\r')
                    {
                        i++;                                     //跳过无意义的字符
                    }
                    else if (rowCode[i] == '\n')		//如果是换行符，则行号加1
                    {
                        row++;
                        i++;
                    }

                    else if (isLetter(rowCode[i]))		//如果是字母
                    {
                        //MessageBox.Show(i.ToString());
                        i = IDRecognizer(rowCode, i);
                        //MessageBox.Show(i.ToString());
                        for (j = 0; j < reservedWords.Length; j++)
                        {
                            if (token.CompareTo(reservedWords[j]) == 0)
                                break;
                        }

                        if (j >= reservedWords.Length)					//不是保留字
                        {
                            for (int m = 0; m < 12 - token.Length; m++)
                                space = space + " ";
                            analyzeResult = analyzeResult + row.ToString() + ":  " + token + space + "标识符     Token码       75" + "\r\n"; ;
                            text4 = text4 + row.ToString() + ":  " + token + "     " + token.Length + space + "标识符" + "  " + "简单变量" + "  " + "未知" + "  " + "  未知" + "\r\n";
                            token = "";
                            space = "";
                        }

                        if (j < reservedWords.Length)								//是保留字
                        {
                            for (int m = 0; m < 12 - token.Length; m++)
                                space = space + " ";
                            analyzeResult = analyzeResult + row.ToString() + ":  " + reservedWords[j] + space + "保留字     Token码       " + Convert.ToString(gettoken(token, 1)) + "\r\n"; ;
                            token = "";
                            space = "";

                        }
                    }

                    else if (isDigit(rowCode[i]))		//如果是数字
                    {
                        int x = 0;
                        i = digitRecognizer(rowCode, i);//识别常数 
                        for (int m = 0; m < 12 - token.Length; m++)
                            space = space + " ";    //统计空格数
                        for (x = 0; x < token.Length; x++)
                            if (isLetter(token[x]))
                                break;
                        if (x < token.Length)
                        {
                            analyzeResult = analyzeResult + row.ToString() + ":  " + token + space
                                + "非法标识符       Token码       75" + "\r\n";//Added by LeoHao
                            error(0,row);
                            break;
                        }
                        else analyzeResult = analyzeResult + row.ToString() + ":  " + token + space + "常量       Token码       76" + "\r\n";
                        text4 = text4 + row.ToString() + ":  " + token + "     " + token.Length + space + "整数" + "    " + "简单变量" + "  " + "未知" + "  " + "  未知" + "\r\n";
                        token = "";
                        space = "";

                    }
                    else if (isBound(rowCode[i]))                  //识别界符
                    {
                        i = boundRecognizer(rowCode, i);
                        for (int m = 0; m < 12 - token.Length; m++)
                            space = space + " ";
                        analyzeResult = analyzeResult + row.ToString() + ":  " + token + space + "界符       Token码       " + Convert.ToString(gettoken(token, 3)) + "\r\n"; ;
                        token = "";
                        space = "";
                    }
                    else if (isOperator(rowCode[i]))
                    {
                        i = operatorRecognizer(rowCode, i);
                        for (int m = 0; m < 12 - token.Length; m++)
                            space = space + " ";
                        analyzeResult = analyzeResult + row.ToString() + ":  " + token + space + "运算符     Token码       " + Convert.ToString(gettoken(token, 2)) + "\r\n";
                        token = "";
                        space = "";
                    }
                    else { error(0,row); i++; }
                }

            }


            catch (DivideByZeroException e1)
            {

                wrong = e1.Message;
            }
            catch (IndexOutOfRangeException e2)
            {

                wrong = e2.Message;
            }

            catch (Exception e)
            {

                wrong = e.Message;
            }
            return;
        }
        private bool isLetter(char c)
        {   //判断c是否为字母
            return (c>='a'&&c<='z')||(c>='A'&&c<='Z');
        }
        private bool isDigit(char c)
        {   //判断c是否为数字
            return (c >= '0' && c <= '9');
        }
        private bool isUnderline(char c)
        {   //判断c是否为下划线
            return c == '_';
        }
        public bool isBound(char ch)                           //判断是否为界符
        {
            for (int j = 0; j < bounds.Length; j++)
                if (ch.CompareTo(bounds[j][0]) == 0)
                {
                    return true;
                }
            return false;

        }

        public bool isOperator(char ch)
        {
            for (int i = 0; i < operators.Length; i++)
                if (ch == operators[i][0])
                {
                    return true;

                }
            return false;
        }

        private int operatorRecognizer(string str, int i)
        {
            char state = '0';
            string sstr = "";

            while (state != '2')
            {
                switch (state)
                {
                    case '0':
                        sstr += str[i];
                        i++;
                        state = '1';
                        break;


                    case '1':  //判断为双个运算符 
                        if (str.Substring(i - 1, 2) == "++" || str.Substring(i - 1, 2) == "--" || str.Substring(i - 1, 2) == "<<" || str.Substring(i - 1, 2) == ">>" || str.Substring(i - 1, 2) == "+=" || str.Substring(i - 1, 2) == "-=" || str.Substring(i - 1, 2) == "*=" || str.Substring(i - 1, 2) == "/=" || str.Substring(i - 1, 2) == "!=" || str.Substring(i - 1, 2) == "%=")
                        {
                            sstr += str[i];
                            i++;
                            state = '2';

                        }
                        if (str[i - 1] == '?' && str[i] == ':')                     //三目运算符
                        {
                            sstr += str[i];
                            i++;
                            state = '2';

                        }
                        else { state = '2'; }                       //运算符
                        break;
                }
            }

            token = sstr;
            return i;

        }

        public int IDRecognizer(string str, int i)                          //识别单词
        {
            char state = '0';
            string sstr = "";                             //记录单词
            while (state != '2')
            {
                switch (state)
                {
                    case '0': if (isLetter(str[i])) { state = '1'; sstr = sstr + str[i]; i++; }
                        // else error(1);
                        break;
                    case '1':
                        if (isLetter(str[i]) || isDigit(str[i]) || isUnderline(str[i])) { state = '1'; sstr = sstr + str[i]; i++; }
                        else state = '2';
                        break;
                }
            }
            token = sstr;                   //记录识别的字符串
            return i;
        }

        public int digitRecognizer(string str, int i)                                //识别常数
        {
            char state = '0';
            string sstr = "";
            while (state != '2')
            {
                switch (state)
                {
                    case '0':
                        if (isDigit(str[i]))
                        {
                            sstr += str[i];
                            state = '1';
                            i++;
                        }
                        break;
                    case '1':
                        if (isDigit(str[i]))
                        {
                            sstr += str[i];
                            state = '1';
                            i++;
                        }
                        else if (str[i] == '.' && isDigit(str[i + 1]))      //实数(小数点)
                        {
                            sstr += str[i];
                            state = '1';
                            i++;
                        }
                        else if (isLetter(str[i]))
                        {
                            sstr += str[i];
                            state = '1';
                            i++;
                        }//Added by LeoHao
                        else state = '2';
                        break;
                }
                token = sstr;

            }
            return i;

        }

        public int boundRecognizer(string str, int i)
        {
            string sstr = "";
            for (int k = 0; k < bounds.Length; k++) 　　//判断为界符
                if (str[i].CompareTo(bounds[k][0]) == 0)
                {
                    sstr += str[i];
                    i++;
                    break;
                }
                else continue;
            token = sstr;

            if (token == "{" || token == "}")
                flag1++;
            else if (token == "[" || token == "]")
                flag2++;
            else if (token == "(" || token == ")")
                flag3++;
            else if (token == "<" || token == ">")
                flag4++;
            else if (token == "\'")
                flag5++;
            else if (token == "\"")
                flag6++;
            return i;
        }

        public int gettoken(string str, int k)                        //获得单词的token值
        {
            switch (k)
            {
                case 1:
                    for (int i = 0; i < reservedWords.Length; i++)                          //关键字
                    {
                        if (str == reservedWords[i])
                            return i;

                    }
                    break;
                case 2:
                    for (int i = 0; i < operators.Length; i++)                         //运算符
                    {
                        if (str == operators[i])
                            return i + 32;
                    }
                    break;
                case 3:
                    for (int i = 0; i < bounds.Length; i++)                                //界符
                    {
                        if (str == bounds[i])
                            return i + 60;
                    }
                    break;

            }

            return 0;

        }

        public string ErrorInfo()                                         //错误信息个数
        {
            // error(0);
            error(1,0);
            errorInfo = errorCount.ToString() + "   errors";
            MessageBox.Show(errorCount.ToString());
            return errorInfo;
        }

        public void error(int k,int row)                                       //错误信息
        {
            switch (k)
            {
                case 0: errorDetailInfo = errorDetailInfo + row.ToString() + ": " + "非法字符" + "\r\n";                          //输入了非法字符
                    errorCount++;
                    break;
                case 1:                             //界符不匹配
                    if (flag1 % 2 != 0)
                    {
                        errorDetailInfo = errorDetailInfo + "{ 不匹配" + "\r\n";
                        errorCount++;
                    }

                    if (flag2 % 2 != 0)
                    {
                        errorDetailInfo = errorDetailInfo + "[ 不匹配" + "\r\n";
                        errorCount++;
                    }

                    if (flag3 % 2 != 0)
                    {
                        errorDetailInfo = errorDetailInfo + "( 不匹配" + "\r\n";
                        errorCount++;
                    }
                    if (flag4 % 2 != 0)
                    {
                        errorDetailInfo = errorDetailInfo + "< 不匹配" + "\r\n";
                        errorCount++;
                    }
                    if (flag5 % 2 != 0)
                    {
                        errorDetailInfo = errorDetailInfo + "' 不匹配" + "\r\n";
                        errorCount++;
                    }
                    if (flag6 % 2 != 0)
                    {
                        errorDetailInfo = errorDetailInfo + " \" 不匹配" + "\r\n";
                        errorCount++;
                    }
                    break;

            }
        }
    }
}