using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 编译原理辅助系统
{
    //文法数据结构定义
    public class Grammar
    {
        //文法开始符号
        public String startChar = "";
        //非终结符
        public List<String> nonTerminalChar = new List<String>();
        //终结符
        public List<String> terminalChar = new List<String>();
        //文法产生式集合
        public List<ProductionRule> productionRule = new List<ProductionRule>();
        //文法信息
        public String grammarInfo()
        {
            String info = "开始符号：" + startChar+"\n";
            info += "非终结符：";
            foreach (String c in nonTerminalChar)
                info = info+" " + c;
            info += "\n终结符：";
            foreach (String c in terminalChar)
                info =info+  " " + c;
            info += "\n";
            return info;
        }
    }
}
