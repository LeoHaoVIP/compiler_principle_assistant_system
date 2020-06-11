using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 编译原理辅助系统
{
    //First集定义
    class FirstSet
    {
        //非终结符集
        public String[] nonTerminalChar = null;
        //终结符集
        public String[] terminalChar = null;
        //表格形式存储的First集
        public String[,] firstSetTable=null;
        public FirstSet(String[] nonTerminalChar = null, String[] terminalChar = null)
        {
            this.nonTerminalChar = nonTerminalChar;
            List<String> _terminalChar = terminalChar.ToList();
            _terminalChar.Add("$");
            this.terminalChar = _terminalChar.ToArray();
            int num1 = this.nonTerminalChar.Length;
            int num2 = this.terminalChar.Length;
            firstSetTable = new String[num1, num2];
            //初始化表格内容均为空
            for (int i = 0; i < num1; i++)
                for (int j = 0; j < num2; j++)
                    firstSetTable[i, j] = "";
        }
        //返回First集信息
        public String firstSetInfo()
        {
            String s = "";
            int i, j;
            for(i=0;i<firstSetTable.GetLength(0);i++)
            {
                for (j = 0; j < firstSetTable.GetLength(1); j++)
                    s += (firstSetTable[i, j] + "\t");
                s += "\n";
            }
            return s;
        }
    }
}
