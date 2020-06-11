using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 编译原理辅助系统
{
    class DFA
    {
        //最大状态数
        public int DFA_maxState = 0;
        //初态集
        public String[] DFA_START = null;
        //终态集
        public String[] DFA_END = null;
        //输入符号集
        public String[] DFA_INPUT = null;
        //转换函数集
        public transformFunction[] DFA_Transform = null;
        //状态子集 (便于获取LR0分析中项目族信息)
        public stateSet_DFA[] stateSet = null;
        //DFA信息
        public String DFAInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("最大状态编号：" + DFA_maxState.ToString());
            sb.Append("\n");
            sb.Append("初态集: ");
            foreach (String item in DFA_START)
                sb.Append(item + " ");
            sb.Append("\n");
            sb.Append("终态集: ");
            foreach (String item in DFA_END)
                sb.Append(item + " ");
            sb.Append("\n");
            sb.Append("输入符号集: ");
            foreach (String item in DFA_INPUT)
                sb.Append(item + " ");
            sb.Append("\n");
            sb.Append("转化函数集:(" + DFA_Transform.Length + "个) \n");
            foreach (transformFunction item in DFA_Transform)
                sb.Append(item.tranformInfo() + "\n");
            return sb.ToString();
        }
    }
}
