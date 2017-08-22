using System;
using System.Collections.Generic;


namespace ExEsimEventsExcalibur
{
    /// <summary>
    /// 公共常量、公共变量
    /// </summary>
    public class Global
    {

        #region "常量定义区"


        #endregion


        #region "变量定义区"

        //ini文件路径
        public static string SettingIni = Environment.CurrentDirectory + "\\Settings.ini";

        //时区
        public static string PolandTimeZone = "Central European Standard Time";

        //杯赛类型翻译
        public static string[] starten = { "Team Cup start", "League start", "World War start", "New Cup Tournament FirstDay start", "New Cup Tournament SecondDay start" };
        public static string[] startcn = { "团体杯", "单挑杯", "世界大战", "淘汰赛第一天", "淘汰赛第二天" };

        //使用服务器简称
        public static bool StringSimpleServer = true;
        public static bool CodeSimpleServer = true;

        //服务器首字母大写
        public static bool StringUpperServer = false;
        public static bool CodeUpperServer = false;

        //如果在xx天数内则使用易读化时间
        public static double ETRDays = 3;

        //设定服务器简称用
        public static Dictionary<String, String> ServerDictionary = new Dictionary<String, String>(); 

        //其他自定义字符串
        public static string CodeTextCaption = "服务器,开始时间,杯赛类型,传送门";

        //无杯赛图片地址
        public static string Saltfish = "404 not found";

        //无杯赛文字
        public static string SaltfishText = "未发现杯赛";
        #endregion
    }
}
