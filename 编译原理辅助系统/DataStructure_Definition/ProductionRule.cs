using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 编译原理辅助系统
{
    //文法产生式规则
    public class ProductionRule
    {
        //产生式左部
        public String left;
        //产生式右部
        public String right;
        public ProductionRule(String left = "", String right = "")
        {
            this.left = left;
            this.right = right;
        }
        //返回产生式信息
        public String productionRuleInfo()
        {
            String info = "";
            info += left;
            info += "->";
            info += right;
            return info;
        }
    }
}