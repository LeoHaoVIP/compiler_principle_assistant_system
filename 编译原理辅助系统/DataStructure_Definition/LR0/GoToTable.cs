using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 编译原理辅助系统
{
    //LR分析：GoTo表 (转换表)
    class GoToTable
    {
        //非终结符集
        public String[] nonTerminalChar = null;
        //最大状态数(0~maxState)
        public int maxState=0;
        //GoTo转换表
        public String[,] goToTable = null;
        public GoToTable(String[] nonTerminalChar = null, int maxState=0)
        {
            this.maxState = maxState;
            this.nonTerminalChar = nonTerminalChar;
            int num1 = maxState+1;
            int num2 = this.nonTerminalChar.Length;
            goToTable = new String[num1, num2];
            //初始化表格内容均为空
            for (int i = 0; i < num1; i++)
                for (int j = 0; j < num2; j++)
                    goToTable[i, j] = "";
        }
    }
}
