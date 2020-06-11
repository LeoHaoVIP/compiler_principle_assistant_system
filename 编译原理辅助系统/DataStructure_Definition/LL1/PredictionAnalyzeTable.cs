using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 编译原理辅助系统
{
    //预测分析表定义
    class PredictionAnalyzeTable
    {
         //非终结符集
        public String[] nonTerminalChar = null;
        //终结符集
        public String[] terminalChar = null;
        //LL1预测分析表
        public String[,] predictionAnalyzeTable = null;
        public PredictionAnalyzeTable(String[] nonTerminalChar = null, String[] terminalChar = null)
        {
            this.nonTerminalChar = nonTerminalChar;
            List<String> _terminalChar=terminalChar.ToList();
            //加入结束符'#'
            _terminalChar.Add("#");
            this.terminalChar =_terminalChar.ToArray();
            int num1 = this.nonTerminalChar.Length;
            int num2 = this.terminalChar.Length;
            predictionAnalyzeTable = new String[num1, num2];
            //初始化表格内容均为空
            for (int i = 0; i < num1; i++)
                for (int j = 0; j < num2; j++)
                    predictionAnalyzeTable[i, j] = "";
        }
    }
}
