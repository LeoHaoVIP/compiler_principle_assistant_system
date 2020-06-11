using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace 编译原理辅助系统
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点
        /// </summary>
        public static String mySourceCode="";   //源程序
        public static String folderPath="";     //源程序文件所在目录
        //XFA相关全局变量（在Main()函数中实例化）
        public static NFA globalNFA = null;
        public static DFA globalDFA = null;
        public static MFA globalMFA = null;
        //全局文法定义
        public static Grammar globalGrammar = null;
        //全局First集
        public static FirstSet globalFirstSet = null;
        //全局Follow集
        public static FollowSet globalFollowSet = null;
        //全局Select集
        public static SelectSet globalSelectSet = null;
        //全局预测分析表
        public static PredictionAnalyzeTable globalPredictionAnalyzeTable = null;
        //全局文法项目集定义
        public static Grammar globalGrammarItemSet = null;
        //全局ACTION表定义
        public static ActionTable globalActionTable = null;
        //全局GOTO表定义
        public static GoToTable globalGoToTable=null;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainForm mForm = new mainForm();
            mForm.StartPosition = FormStartPosition.CenterScreen;
            //实例化全局XFA对象
            globalNFA = new NFA();
            globalDFA = new DFA();
            globalMFA = new MFA();
            Application.Run(mForm);
        }
        public static void resetAllVariebles()
        {
            //实例化全局XFA对象
            globalNFA = new NFA();
            globalDFA = new DFA();
            globalMFA = new MFA();
        }
    }
}
