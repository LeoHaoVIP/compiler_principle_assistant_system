using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 编译原理辅助系统
{
    //定义转化函数
    class transformFunction
    {
        public String from;
        public String by;
        public String to;
        //构造函数
        public transformFunction(String from = "$", String by = "$", String to = "$")
        {
            this.from = from;
            this.by = by;
            this.to = to;
        }
        //返回转换函数信息
        public String tranformInfo()
        {
            return from + "—" + by + "→" + to;
        }
        //定义索引器：通过开始状态from和输入符号by，确定目标状态to
        public String this[String from, String by]
        {
            get
            {
                if (this.from == from && this.by == by)
                    return this.to;
                else return "";
            }
        }
    }
}