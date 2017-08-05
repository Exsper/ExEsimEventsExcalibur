using System;
using System.Linq;


namespace ExEsimEventsExcalibur
{
    /// <summary>
    /// 将杯赛字符串转换为本地时间字符串
    /// </summary>
    class PolandTimeToLocalTime
    {

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

        /// <summary>
        /// 取出锦标赛类型，并返回锦标赛类型和时间字符串
        /// </summary>
        /// <param name="s">输入的字符串</param>
        /// <returns>字符串数组\nrs[0]为锦标赛类型\nrs[1]为时间字符串</returns>
        private static string[] CheckEventType(string s)
        {
            string[] rs = new string[2];
            string eventtype;
            string timestring;
            //如果前有类型，应该有3个冒号，没有类型应该有2个冒号
            int count = GetCharCountInString(s, ":");
            if (count != 3)
            {
                rs[0] = "";
                rs[1] = s;
                return rs;
            }
            //获取
            int mindex = s.IndexOf(':');
            eventtype = s.Substring(0, mindex).Trim();
            timestring = s.Substring(mindex + 1).Trim();
            //翻译
            int istart;
            int iend = Global.starten.Count();
            for (istart = 0; istart < iend; istart++)
            {
                if (eventtype == Global.starten[istart])
                {
                    eventtype = Global.startcn[istart];
                }
            }
            //返回
            rs[0] = eventtype;
            rs[1] = timestring;
            return rs;
        }

        /// <summary>
        /// 将dd-M-yyyy HH:mm:ss格式字符串转化为DateTime
        /// </summary>
        /// <param name="s">时间格式字符串</param>
        /// <returns>DateTime</returns>
        private static DateTime StringToDateTime(string s)
        {
            string rs = s;
            try
            {
                //不知道为什么日期和月份倒过来了
                /*
                DateTime dt;
                DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
                dtFormat.FullDateTimePattern = "dd-M-yyyy HH:mm:ss";
                dt = Convert.ToDateTime(s, dtFormat);
                */
                string[] DateAndTime = rs.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string[] Date = DateAndTime[0].Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                string[] Time = DateAndTime[1].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                int day = Convert.ToInt32(Date[0]);
                int month = Convert.ToInt32(Date[1]);
                int year = Convert.ToInt32(Date[2]);
                int hour = Convert.ToInt32(Time[0]);
                int minute = Convert.ToInt32(Time[1]);
                int second = Convert.ToInt32(Time[2]);
                DateTime dt = new DateTime(year, month, day, hour, minute, second);
                return dt;
            }
            catch
            {
                return new DateTime();
            }
        }


        /// <summary>
        /// 日期易读化（今天/明天/后天）
        /// </summary>
        /// <param name="end">未来时间</param>
        /// <param name="now">现在时间</param>
        /// <returns>易读化字符串，后接1个空格</returns>
        private static string EasyDate(DateTime end, DateTime now)
        {
            string result = "";
            DateTime tmp = now;
            int i;
            TimeSpan ts = end - now;

            if (ts.TotalDays <= Global.ETRDays && ts.TotalDays <= 3)
            {
                if (end.Day != now.Day)
                {
                    for (i = 1; i < 4; i++)
                    {
                        tmp = tmp.AddDays(1);
                        if (end.Day == tmp.Day)
                        {
                            break;
                        }
                    }
                    //直接用0点表示，不加特殊情况了，以避免各种奇怪bug

                    //第二天0点一般被叫做深夜12点，需要单独考虑
                    /*
                    if (end.Hour == 0)
                    {
                        tmp = end.AddDays(-1);
                        switch (i)
                        {
                            case 1: { result = result + "今天（" + tmp.Day.ToString() + "日） 深夜12点（" + end.Day.ToString() + "日0点）"; break; }
                            case 2: { result = result + "明天（" + tmp.Day.ToString() + "日） 深夜12点（" + end.Day.ToString() + "日0点）"; break; }
                            case 3: { result = result + "后天（" + tmp.Day.ToString() + "日） 深夜12点（" + end.Day.ToString() + "日0点）"; break; }
                            default: break;
                        }
                        return result;
                    }
                    else
                    {
                        switch (i)
                        {
                            case 1: { result = result + "明天（" + end.Day.ToString() + "日） "; break; }
                            case 2: { result = result + "后天（" + end.Day.ToString() + "日） "; break; }
                            case 3: { result = result + "大后天（" + end.Day.ToString() + "日） "; break; }
                            default: break;
                        }
                    }
                    */
                    switch (i)
                    {
                        case 1: { result = result + "明天（" + end.Day.ToString() + "日） "; break; }
                        case 2: { result = result + "后天（" + end.Day.ToString() + "日） "; break; }
                        case 3: { result = result + "大后天（" + end.Day.ToString() + "日） "; break; }
                        default: break;
                    }
                }
                else
                {
                    result = result + "今天（" + end.Day.ToString() + "日） ";
                }
            }
            else
            {
                result = result + end.Day.ToString() + "日（" + end.Date.ToString("dddd") + "） ";
            }
            return result;
        }

        /// <summary>
        /// 日期稍易读化（星期）
        /// </summary>
        /// <param name="end">未来时间</param>
        /// <param name="now">现在时间</param>
        /// <returns>易读化字符串，后接1个空格</returns>
        private static string NormalDate(DateTime end, DateTime now)
        {
            string result = "";
            TimeSpan ts = end - now;
            if (ts.TotalDays > 28)
            {
                result = result + end.Month.ToString() + "月" + end.Day.ToString() + "日（" + end.Date.ToString("dddd") + "） ";
            }
            else
            {
                result = result + end.Day.ToString() + "日（" + end.Date.ToString("dddd") + "） ";
            }
            return result;
        }

        /// <summary>
        /// 时间易读化
        /// </summary>
        /// <param name="end">未来时间</param>
        /// <returns>易读化字符串</returns>
        private static string EasyTime(DateTime end)
        {
            string result = "";
            int endhour = end.Hour;
            switch (endhour)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5: { result = result + "凌晨"; break; }
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11: { result = result + "上午"; break; }
                case 12: { result = result + "中午"; break; }
                case 13:
                case 14:
                case 15:
                case 16:
                case 17: { result = result + "下午"; break; }
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23: { result = result + "晚上"; break; }
                case 24: { result = result + "深夜"; break; }//不可能出现的情况
                default: break;
            }
            if (endhour > 12)
            {
                endhour = endhour - 12;
            }
            result = result + endhour.ToString() + "点";
            if (end.Minute != 0)
            {
                result = result + end.Minute.ToString() + "分";
            }
            return result;
        }

        /// <summary>
        /// 将DateTime易读化
        /// </summary>
        /// <param name="end">未来时间</param>
        /// <returns>易读化字符串</returns>
        private static string DateTime2EasyTime(DateTime end)
        {
            string result = "";
            DateTime now = DateTime.Now;
            TimeSpan ts = end - now;
            if (ts.TotalDays <= 0)
            {
                return "已过时";
            }

            //日期易读化
            result = result + EasyDate(end, now);
            //时间易读化

            //直接用0点表示，不加特殊情况了，以避免各种奇怪bug
            /*
            if (end.Hour != 0)
            {
                result = result + EasyTime(end);
            }
            */
            result = result + EasyTime(end);

            return result;
        }


        /// <summary>
        /// 将DateTime稍易读化
        /// </summary>
        /// <param name="end">未来时间</param>
        /// <returns>易读化字符串</returns>
        private static string DateTime2NormalTime(DateTime end)
        {
            string result = "";
            DateTime now = DateTime.Now;
            TimeSpan ts = end - now;
            if (ts.TotalDays <= 0)
            {
                return "已过时";
            }

            //日期稍易读化
            result = result + NormalDate(end, now);
            //时间易读化
            result = result + EasyTime(end);

            return result;
        }

        private static string OutputString(string s, string url)
        {
            string result = GetServerFromUrl.Url2BattleInfo(url,false) + " ";
            string[] rs = CheckEventType(s);
            DateTime PolandTime = StringToDateTime(rs[1]);
            DateTime end = TimeZoneInfo.ConvertTime(PolandTime, TimeZoneInfo.FindSystemTimeZoneById(Global.PolandTimeZone), TimeZoneInfo.Local);
            result = result + DateTime2EasyTime(end) + " " + rs[0];
            return result;
        }

        private static string OutputCode(string s, string url)
        {
            if (s == "")
            {
                return "| , , , ";
            }
            string result = "";
            string[] rs = CheckEventType(s);
            DateTime PolandTime = StringToDateTime(rs[1]);
            DateTime end = TimeZoneInfo.ConvertTime(PolandTime, TimeZoneInfo.FindSystemTimeZoneById(Global.PolandTimeZone), TimeZoneInfo.Local);
            result = result + "|" + GetServerFromUrl.Url2BattleInfo(url,true) + "," + DateTime2NormalTime(end) + "," + rs[0] + ",[url=" + url + "]进入[/url]";
            return result;
        }

        public static string PrintEventInfo(string s, string url, bool isCode)
        {
            if (isCode == false)
            {
                return OutputString(s, url);
            }
            else
            {
                return OutputCode(s, url);
            }
        }

        public static DateTime GetPolandTime(string s)
        {
            string[] rs = CheckEventType(s);
            DateTime PolandTime = StringToDateTime(rs[1]);
            return PolandTime;
        }
    }
}
