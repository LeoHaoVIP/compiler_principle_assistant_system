using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 编译原理辅助系统
{
    //Select选择集合定义
    class SelectSet
    {
        //非终结符集
        public String[] nonTerminalChar = null;
        //终结符集
        public String[] terminalChar = null;
        //文法产生式集
        public ProductionRule[] productionRule = null;
        //表格形式存储的Select集
        public String[,] selectSetTable=null;
        public SelectSet(ProductionRule[] productionRule = null, String[] nonTerminalChar = null, 
            String[] terminalChar = null)
        {
            this.productionRule = productionRule;
            this.nonTerminalChar = nonTerminalChar;
            List<String> _terminalChar=terminalChar.ToList();
            //加入结束符'#'
            _terminalChar.Add("#");
            this.terminalChar =_terminalChar.ToArray();
            //构成产生式-终结符(包括#)对应关系
            int num1 = this.productionRule.Length;
            int num2 = this.terminalChar.Length;
            selectSetTable = new String[num1, num2];
            //初始化表格内容均为空
            for (int i = 0; i < num1; i++)
                for (int j = 0; j < num2; j++)
                    selectSetTable[i, j] = "";
        }
        //返回Select集信息
        public String selectSetInfo()
        {
            String s = "";
            int i, j;
            for (i = 0; i < selectSetTable.GetLength(0); i++)
            {
                for (j = 0; j < selectSetTable.GetLength(1); j++)
                    s += (selectSetTable[i, j] + "\t");
                s += "\n";
            }
            return s;
        }
    }
}
