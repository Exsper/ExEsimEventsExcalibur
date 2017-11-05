using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace ExEsimEventsExcalibur
{
    public partial class Form1 : Form
    {

        #region 读取ini通用
        /// <summary>
        /// 字符串转换为布尔值
        /// </summary>
        /// <param name="s">原字符串</param>
        /// <returns>布尔值</returns>
        private static bool S2B(string s)
        {
            if (s.Trim() == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 读取ini键值
        /// </summary>
        /// <param name="section">节点</param>
        /// <param name="item">项</param>
        /// <returns>键值</returns>
        private static string GetSettingsValue(string section, string item)
        {
            string value = INIOperationClass.INIGetStringValue(Global.SettingIni, section, item, null);
            if (value == null)
            {
                throw new Exception();
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// 读取ini以逗号分隔的数组键值
        /// </summary>
        /// <param name="section">节点</param>
        /// <param name="item">项</param>
        /// <returns>键值（数组）</returns>
        private static string[] GetSettingsValuePra(string section, string item)
        {
            string value = INIOperationClass.INIGetStringValue(Global.SettingIni, section, item, null);
            if (value == null)
            {
                throw new Exception();
            }
            else
            {
                int count = GetCharCountInString(value, ",") + 1;
                string[] s = value.Split(new char[] { ',' }, StringSplitOptions.None);
                string[] output = new string[count];
                int i;
                for (i = 0; i < count; i++)
                {
                    output[i] = s[i].Trim();
                }
                return output;
            }
        }

        #endregion 读取ini通用


        //LoadBasicSettings();
        #region ini读取基本设置

        private static string SettingsIniSection = "Settings";

        /// <summary>
        /// 获取字符串中某字符出现的次数
        /// </summary>
        /// <param name="s">输入的字符串</param>
        /// <param name="c">需要搜索的指定字符</param>
        /// <returns>出现次数</returns>
        private static int GetCharCountInString(string s, string c)
        {
            string r = s.Replace(c, "");
            int num = (s.Length - r.Length) / c.Length;
            return num;
        }



        private static void LoadBasicSettings()
        {
            if (File.Exists(Global.SettingIni) == false)
            {
                return;
            }
            //读取服务器简称设定
            try
            {
                Global.StringSimpleServer = S2B(GetSettingsValue(SettingsIniSection, "StringSimpleServer"));
            }
            catch { }
            //读取文章代码服务器简称设定
            try
            {
                Global.CodeSimpleServer = S2B(GetSettingsValue(SettingsIniSection, "CodeSimpleServer"));
            }
            catch { }
            //读取服务器首字大写设定
            try
            {
                Global.StringUpperServer = S2B(GetSettingsValue(SettingsIniSection, "StringUpperServer"));
            }
            catch { }
            //读取文章代码服务器首字大写设定
            try
            {
                Global.CodeUpperServer = S2B(GetSettingsValue(SettingsIniSection, "CodeUpperServer"));
            }
            catch { }
            //读取易读化时间限制
            try
            {
                Global.ETRDays = Convert.ToDouble(GetSettingsValue(SettingsIniSection, "ETRDays"));
            }
            catch { }
            //读取服务器时区
            try
            {
                Global.PolandTimeZone = GetSettingsValue(SettingsIniSection, "ServerTimeZone");
            }
            catch { }
            //读取英文杯赛类型
            try
            {
                Global.starten = GetSettingsValuePra(SettingsIniSection, "TypeEn");
            }
            catch { }
            //读取中文杯赛类型
            try
            {
                Global.startcn = GetSettingsValuePra(SettingsIniSection, "TypeCn");
            }
            catch { }
            //读取最大杯赛数量
            try
            {
                Global.MaxEventCounts = Convert.ToInt32(GetSettingsValue(SettingsIniSection, "MaxEventCounts"));
            }
            catch { }
        }

        #endregion


        //LoadServerSettings();
        #region ini读取服务器字符串

        private static string ServersIniSection = "Servers";

        private static string[] SplitServerString(string s)
        {
            int i = s.IndexOf('=');
            string[] ss = new string[2];
            if (i <= 0)
            {
                ss[0] = "";
                ss[1] = "";
                return ss;
            }
            ss[0] = s.Substring(0, i);
            ss[1] = s.Substring(i + 1, s.Length - i - 1);
            return ss;
        }

        private static void LoadServerSettings()
        {
            if (File.Exists(Global.SettingIni) == false)
            {
                return;
            }
            string[] items = INIOperationClass.INIGetAllItems(Global.SettingIni, ServersIniSection);
            string[] keyvalue = new string[2];
            try
            {
                foreach (string item in items)
                {
                    keyvalue = SplitServerString(item.Trim());
                    if (Global.ServerDictionary.ContainsKey(keyvalue[0]) == false)
                    {
                        Global.ServerDictionary.Add(keyvalue[0], keyvalue[1]);
                    }
                    else
                    {
                        MessageBox.Show("发现重复键 " + keyvalue[0]);
                    }
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #endregion


        //LoadStringsSettings();
        #region ini读取其他字符串
        private static string StringsIniSection = "Strings";

        private static void LoadStringsSettings()
        {
            if (File.Exists(Global.SettingIni) == false)
            {
                return;
            }
            //读取标题字符串
            try
            {
                Global.CodeTextCaption = GetSettingsValue(StringsIniSection, "CodeTextCaption");
            }
            catch { }
            //读取无杯赛图片
            try
            {
                Global.Saltfish = GetSettingsValue(StringsIniSection, "Saltfish");
            }
            catch { }
            //读取无杯赛文字
            try
            {
                Global.SaltfishText = GetSettingsValue(StringsIniSection, "SaltfishText");
            }
            catch { }
        }
        #endregion


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadBasicSettings();
            LoadServerSettings();
            LoadStringsSettings();
            OutTextBox.Text = "[center][img]" + Global.Saltfish + "[/img]\r\n" + Global.SaltfishText + "[/center]\r\n\r\n\r\n记录时间：" + DateTime.Now.ToString("yyyy年M月d日 HH:mm") + "（北京时间）";
        }

        /*
        private void Form1_Resize(object sender, EventArgs e)
        {
            InTextBox.Height = (this.Height - 60) / 4;
            OutTextBox.Height = (this.Height - 60) / 4 * 3;
            OutTextBox.Location = new Point(12, InTextBox.Height + 18);
        }
        */

        /*
        /// <summary>
        /// 获取指定字符在字符串中出现的次数
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="c">指定字符</param>
        /// <returns>指定字符在字符串中出现的次数</returns>
        private int GetCharInStringNumber(string s, char c)
        {
            return s.Split(c).Length - 1;
        }
        */

        private string[] SplitString(string s)
        {
            int i = s.IndexOf(' '); //如果没有空格，i为-1
            string[] ss = new string[2];
            if (i <= 0)
            {
                ss[0] = s;
                ss[1] = "";
                return ss;
            }
            if (s.StartsWith("http") == true)
            {
                ss[0] = s.Substring(0, i);
                ss[1] = s.Substring(i + 1, s.Length - i - 1);
                return ss;
            }
            else
            {
                ss[0] = "";
                ss[1] = s;
                return ss;
            }
        }

        private void InTextBox_TextChanged(object sender, EventArgs e)
        {

            string output = "";

            //没有杯赛
            if (InTextBox.Text == "")
            {
                output = "[center][img]" + Global.Saltfish + "[/img]\r\n" + Global.SaltfishText + "[/center]\r\n\r\n\r\n记录时间：" + DateTime.Now.ToString("yyyy年M月d日 HH:mm") + "（北京时间）";

                OutTextBox.Text = output;
                return;
            }

            string[] sseventInfo=new string[2];
            string[] eventInfo = InTextBox.Text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            //格式：网址 + " " + 时间

            int eventcounts = 0;

            //默认最大50场
            int maxeventcounts = Global.MaxEventCounts;

            DateTime[] oDateTime = new DateTime[maxeventcounts];
            string[] oString = new string[maxeventcounts];
            string[] oCode = new string[maxeventcounts];

            foreach (string s in eventInfo)
            {
                if (eventcounts >= maxeventcounts)
                {
                    MessageBox.Show("杯赛数量超过限制（30）");
                    break;
                }
                sseventInfo = SplitString(s.Trim());

                oDateTime[eventcounts] = PolandTimeToLocalTime.GetPolandTime(sseventInfo[1]);
                oString[eventcounts] = PolandTimeToLocalTime.PrintEventInfo(sseventInfo[1], sseventInfo[0], false);
                oCode[eventcounts] = PolandTimeToLocalTime.PrintEventInfo(sseventInfo[1], sseventInfo[0], true);
                eventcounts = eventcounts + 1;
            }


            //冒泡排序
            if (eventcounts > 1)
            {
                DateTime tempDT;
                string tempS;
                string tempC;
                for (int i = 0; i < eventcounts - 1; i++)
                {
                    for (int j = 0; j < eventcounts - 1 - i; j++)
                    {
                        //如果是按1-3服顺序输入的话，排序后相同时间顺序应该也是1-3，就不考虑时间相同的情况了
                        if (oDateTime[j] > oDateTime[j + 1])
                        {
                            //交换时间
                            tempDT = oDateTime[j + 1];
                            oDateTime[j + 1] = oDateTime[j];
                            oDateTime[j] = tempDT;
                            //交换说明字符串
                            tempS = oString[j + 1];
                            oString[j + 1] = oString[j];
                            oString[j] = tempS;
                            //交换代码字符串
                            tempC = oCode[j + 1];
                            oCode[j + 1] = oCode[j];
                            oCode[j] = tempC;
                        }
                    }
                }
            }

            string oStringAll = "";
            string oCodeAll = "[table]" + Global.CodeTextCaption;
            for (int i = 0; i < eventcounts; i++)
            {
                //换日加空行
                if (i > 0)
                {
                    if (oDateTime[i].Day != oDateTime[i - 1].Day)   //当为0点时属于第二天
                    {
                        oStringAll = oStringAll + "\r\n";
                        oCodeAll = oCodeAll + "| , , , ";
                    }
                }

                oStringAll = oStringAll + oString[i] + "\r\n";
                oCodeAll = oCodeAll + oCode[i];
            }
            oCodeAll = oCodeAll + "[/table]\r\n\r\n";




            output = oStringAll + "\r\n" + "\r\n" + oCodeAll;

            output = output + "\r\n记录时间：" + DateTime.Now.ToString("yyyy年M月d日 HH:mm") + "（北京时间）";

            OutTextBox.Text = output;
            OutTextBox.SelectionStart = oStringAll.Length + 4;
            OutTextBox.SelectionLength = OutTextBox.TextLength;
        }

        private void InTextBox_MouseEnter(object sender, EventArgs e)
        {
            InTextBox.Focus();
        }

        private void OutTextBox_MouseEnter(object sender, EventArgs e)
        {
            OutTextBox.Focus();
        }



    }
}
