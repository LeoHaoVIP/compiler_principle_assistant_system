using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 词法分析器
{
    class token //单词信息类
    {
        public static int rowID;   //单词所在行
        public static string word;  //单词信息
        public static string type;  //单词类型（5类：运算符、界符、保留字、标识符、常数）
        public static int num;   //单词token码
        public token()
        {
            rowID = 0;
            word = "";
            type = "";
            num = 0;
        }
    }
}
