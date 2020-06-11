using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 编译原理辅助系统
{
    class NFA
    {
        //初态集
        public String[] NFA_START = null;
        //终态集
        public String[] NFA_END = null;
        //输入符号集
        public String[] NFA_INPUT = null;
        //转换函数集
        public transformFunction[] NFA_Transform = null;
        //NFA信息
        public String NFAInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("初态集: ");
            foreach (String item in NFA_START)
                sb.Append(item + " ");
            sb.Append("\n");
            sb.Append("终态集: ");
            foreach (String item in NFA_END)
                sb.Append(item + " ");
            sb.Append("\n");
            sb.Append("输入符号集: ");
            foreach (String item in NFA_INPUT)
                sb.Append(item + " ");
            sb.Append("\n");
            sb.Append("转化函数集:("+NFA_Transform.Length+"个) \n");
            foreach (transformFunction item in NFA_Transform)
                sb.Append(item.tranformInfo() + "\n");
            return sb.ToString();
        }
    }
}