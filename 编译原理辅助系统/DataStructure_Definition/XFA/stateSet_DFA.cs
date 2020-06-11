using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 编译原理辅助系统
{
    class stateSet_DFA
    {
        //NFA转化为DFA生成的状态集
        //包含：所有由NFA状态集中状态组成的集合、标记位、状态集标识符
        public String[] state = null;
        //状态集访问标记位(1-已标记；0-未标记)
        public int marked = 0;
        //状态集标识符(用于生成最终的DFA)
        public String ID = "unCertain";
        public stateSet_DFA(String[] state,int marked=0,String ID="unCertain")
        {
            this.state = state;
            this.marked = marked;
            this.ID = ID;
        }
    }
}