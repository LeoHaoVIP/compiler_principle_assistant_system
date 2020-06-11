using System;

namespace 编译原理辅助系统
{
    class LexicalAnalyzer
    {
        int i = 0, j = 0;                    //记录字符位置,token数组的位置
        public static int row = 1;                                       //行号
        String token = "";                //记录识别出的单词   
        String analyzeResult = "";
        String errorInfo = "";                        //用来记录错误信息
        String wrong = "";                    //记录异常信息
        public static int errorsCount = 0;                   //错误字段的个数
        public static String errorDetailInfo = "";                      // 记录错误的详细信息
        int flag1 = 0, flag2 = 0, flag3, flag4, flag5, flag6 = 0;                       //标记成对出现的界符

        public static String text4 = "入口:单词名称   长度    类型     种属     值    内存地址" + "\r\n";                  //用来记录符号表

        String[] reservedWords = new String[32]{"auto", "double", "int","struct", "break", "else","long","switch","case","enum",
            "register","typedef","char","extern","return","union","const","float","short","unsigned","continue","for","signed","void","default",
        "goto","sizeof","volatile","do","if","while","static" };

        String[] operators = new String[28]{"+","-","*","/","%",">","<",">=","<=","==", "-=","+=","*=","/=",           //运算符
                           "!=","=","%=","&","&&","|","||","!","++","--","~","<<",">>","?:"};

        String[] bounds = new String[15] { "{", "}", "[", "]", ";", ",", ".", "(", ")", ":", "\"", "#", ">", "<", "\'" };              //界符


        public String CodeReader(String rowCode)                              //读入字符串
        {
            String space = "";                         //空格数
            if (rowCode.Length == 0)                           //判断字符串是否为空
                return "";
            try
            {
                rowCode += " ";
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
                            analyzeResult = analyzeResult + row.ToString() + "\t" + token +"\t标识符\t  合法\t\t75" + "\r\n"; ;
                            text4 = text4 + row.ToString() + ":  " + token + "\t" + token.Length + space + "标识符" + "\t" + "简单变量" + "\t" + "未知" + "  " + "  未知" + "\r\n";
                            token = "";
                            space = "";
                        }

                        if (j < reservedWords.Length)								//是保留字
                        {
                            for (int m = 0; m < 12 - token.Length; m++)
                                space = space + " ";
                            analyzeResult = analyzeResult + row.ToString() + "\t" + reservedWords[j] + "\t保留字\t  合法\t\t" + Convert.ToString(gettoken(token, 1)) + "\r\n"; ;
                            token = "";
                            space = "";

                        }
                    }

                    else if (isDigit(rowCode[i]))		//如果是数字
                    {
                        int x = 0;
                        int pointNum = 0;
                        bool letterContent = false;
                        i = digitRecognizer(rowCode, i);//识别常数 
                        for (int m = 0; m < 12 - token.Length; m++)
                            space = space + " ";    //统计空格数
                        for (x = 0; x < token.Length; x++)
                        {
                            if (isLetter(token[x]))
                                letterContent = true;
                            if (token[x] == '.')
                                pointNum++;
                        }
                        if (letterContent)
                        {
                            analyzeResult = analyzeResult + row.ToString() + "\t" + token + "\t标识符\t  非法\t\t77\t←（错误行）" + "\r\n";//Added by LeoHao
                            error(0, token);
                        }
                        else
                        {
                            if (pointNum == 0)
                                analyzeResult = analyzeResult + row.ToString() + "\t" + token + "\t常量\t  合法\t\t76" + "\r\n";
                            else if (pointNum == 1)
                                analyzeResult = analyzeResult + row.ToString() + "\t" + token + "\t常量\t  合法\t\t76" + "\r\n";
                            else
                            {
                                analyzeResult = analyzeResult + row.ToString() + "\t" + token + "\t常量\t  非法\t\t76\t←（错误行）" + "\r\n";
                                error(0, token);
                            }
                        }
                        if(pointNum==0)
                            text4 = text4 + row.ToString() + ":  " + token + "     " + token.Length + space + "整数" + "    " + "简单变量" + "  " + "未知" + "  " + "  未知" + "\r\n";
                        else if(pointNum==1)
                            text4 = text4 + row.ToString() + ":  " + token + "     " + token.Length + space + "实数" + "    " + "简单变量" + "  " + "未知" + "  " + "  未知" + "\r\n";
                        else text4 = text4 + row.ToString() + ":  " + token + "     " + token.Length + space + "不合法实数" + "    " + "简单变量" + "  " + "未知" + "  " + "  未知" + "\r\n";
                        token = "";
                        space = "";

                    }
                    else if (isBound(rowCode[i]))                  //识别界符
                    {
                        i = boundRecognizer(rowCode, i);
                        for (int m = 0; m < 12 - token.Length; m++)
                            space = space + " ";
                        analyzeResult = analyzeResult + row.ToString() + "   \t" + token + "\t界符\t  合法\t\t" + Convert.ToString(gettoken(token, 3)) + "\r\n"; ;
                        token = "";
                        space = "";
                    }
                    else if (isOperator(rowCode[i]))
                    {
                        i = operatorRecognizer(rowCode, i);
                        for (int m = 0; m < 12 - token.Length; m++)
                            space = space + " ";
                        analyzeResult = analyzeResult + row.ToString() + "\t" + token + "\t运算符\t  合法\t\t" + Convert.ToString(gettoken(token, 2)) + "\r\n";
                        token = "";
                        space = "";
                    }
                    else { error(0,rowCode[i].ToString()); i++; }
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
            return analyzeResult;
        }

        public bool isLetter(char ch)                      //判断是否为字母
        {
            if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z'))
                return true;
            else return false;
        }

        public bool isDigit(char ch)                           //判断是否为数字
        {
            if (ch >= '0' && ch <= '9')
                return true;
            else return false;
        }

        public bool isUnderline(char ch)                 //识别下划线
        {
            if (ch == '_')
                return true;
            else return false;
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

        private int operatorRecognizer(String str, int i)
        {
            char state = '0';
            String sstr = "";

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

        public int IDRecognizer(String str, int i)                          //识别单词
        {
            char state = '0';
            String sstr = "";                             //记录单词
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

        public int digitRecognizer(String str, int i)                                //识别常数
        {
            char state = '0';
            String sstr = "";
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

        public int boundRecognizer(String str, int i)
        {
            String sstr = "";
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

        public int gettoken(String str, int k)                        //获得单词的token值
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

        public String wholeErrorInfo()                                         //错误信息个数
        {
            // error(0);
            error(1,"");
            errorInfo = ""+errorsCount.ToString() + "   errors";
            return errorInfo;
        }

        public void error(int k,String refers)                                       //错误信息
        {
            switch (k)
            {
                case 0: errorDetailInfo = errorDetailInfo + row.ToString() + "行\t " +refers+ "\t   非法字符" + "\r\n";                          //输入了  非法\t字符
                    errorsCount++;
                    break;
                case 1:                             //界符不匹配
                    if (flag1 % 2 != 0)
                    {
                        errorDetailInfo = errorDetailInfo + "{ 不匹配" + "\r\n";
                        errorsCount++;
                    }

                    if (flag2 % 2 != 0)
                    {
                        errorDetailInfo = errorDetailInfo + "[ 不匹配" + "\r\n";
                        errorsCount++;
                    }

                    if (flag3 % 2 != 0)
                    {
                        errorDetailInfo = errorDetailInfo + "( 不匹配" + "\r\n";
                        errorsCount++;
                    }
                    //if (flag4 % 2 != 0)
                    //{
                    //    errorDetailInfo = errorDetailInfo + "< 不匹配" + "\r\n";
                    //    errorsCount++;
                    //}
                    if (flag5 % 2 != 0)
                    {
                        errorDetailInfo = errorDetailInfo + "' 不匹配" + "\r\n";
                        errorsCount++;
                    }
                    if (flag6 % 2 != 0)
                    {
                        errorDetailInfo = errorDetailInfo + " \" 不匹配" + "\r\n";
                        errorsCount++;
                    }
                    break;

            }
        }

    }
}
