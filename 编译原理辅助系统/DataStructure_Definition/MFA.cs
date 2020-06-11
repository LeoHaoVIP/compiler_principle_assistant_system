using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 编译原理辅助系统
{
    class MFA
    {
        //最大状态数
        public int MFA_maxState = 0;
        //初态集
        public String[] MFA_START = null;
        //终态集
        public String[] MFA_END = null;
        //输入符号集
        public String[] MFA_INPUT = null;
        //转换函数集
        public transformFunction[] MFA_Transform = null;//DFA信息
        //状态子集 (便于获取LR0分析中项目族信息)
        public stateSet_DFA[] stateSet = null;
        public String MFAInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("最大状态编号："+MFA_maxState.ToString());
            sb.Append("\n");
            sb.Append("初态集: ");
            foreach (String item in MFA_START)
                sb.Append(item + " ");
            sb.Append("\n");
            sb.Append("终态集: ");
            foreach (String item in MFA_END)
                sb.Append(item + " ");
            sb.Append("\n");
            sb.Append("输入符号集: ");
            foreach (String item in MFA_INPUT)
                sb.Append(item + " ");
            sb.Append("\n");
            sb.Append("转化函数集:(" + MFA_Transform.Length + "个) \n");
            foreach (transformFunction item in MFA_Transform)
                sb.Append(item.tranformInfo() + "\n");
            return sb.ToString();
        }
    }
}
