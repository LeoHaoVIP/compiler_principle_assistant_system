using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 编译原理辅助系统
{
    //LR分析：Action表 (动作表)
    class ActionTable
    {
        //终结符集
        public String[] terminalChar = null;
        //最大状态数(0~maxState)
        public int maxState = 0;
        //Action动作表
        public String[,] actionTable = null;
        public ActionTable(String[] terminalChar = null,int maxState=0)
        {
            List<String> _terminalChar=terminalChar.ToList();
            //加入结束符'#'
            _terminalChar.Add("#");
            this.terminalChar =_terminalChar.ToArray();
            int num1 = maxState + 1;
            int num2 = this.terminalChar.Length;
            actionTable = new String[num1, num2];
            //初始化表格内容均为空
            for (int i = 0; i < num1; i++)
                for (int j = 0; j < num2; j++)
                    actionTable[i, j] = "";
        }
    }
}
