using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 编译原理辅助系统
{
    //Follow集定义
    class FollowSet
    {
        //非终结符集
        public String[] nonTerminalChar = null;
        //终结符集
        public String[] terminalChar = null;
        //表格形式存储的Follow集
        public String[,] followSetTable=null;
        public FollowSet(String[] nonTerminalChar=null, String[] terminalChar=null)
        {
            this.nonTerminalChar = nonTerminalChar;
            List<String> _terminalChar=terminalChar.ToList();
            //加入结束符'#'
            _terminalChar.Add("#");
            this.terminalChar =_terminalChar.ToArray();
            int num1 = this.nonTerminalChar.Length;
            int num2 = this.terminalChar.Length;
            followSetTable = new String[num1, num2];
            //初始化表格内容均为空
            for (int i = 0; i < num1; i++)
                for (int j = 0; j < num2; j++)
                    followSetTable[i, j] = "";
        }
        //返回Follow集信息
        public String followSetInfo()
        {
            String s = "";
            int i, j;
            for (i = 0; i < followSetTable.GetLength(0); i++)
            {
                for (j = 0; j < followSetTable.GetLength(1); j++)
                    s += (followSetTable[i, j] + "\t");
                s += "\n";
            }
            return s;
        }
    }
}
