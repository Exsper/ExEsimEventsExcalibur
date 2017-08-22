using System.Text.RegularExpressions;
using System.IO;

namespace ExEsimEventsExcalibur
{
    class GetServerFromUrl
    {

        /// <summary>
        /// 根据url获取服务器名
        /// </summary>
        /// <param name="url">网址</param>
        /// <returns>服务器全名</returns>
        private static string GetServerName(string url)
        {
            string result;
            Regex snreg = new Regex("//(.+)\\.e-sim");
            Match match = snreg.Match(url);
            result = match.Groups[0].Value;
            if (result == "" || result == null)
            {
                return null;
            }
            result = result.Substring(2, result.Length - 8);
            return result;
        }

        /// <summary>
        /// 以服务器首字母作为服务器简称
        /// </summary>
        /// <param name="serverName">服务器名</param>
        /// <returns>服务器简称</returns>
        private static string GetDefaultServerName(string serverName)
        {
            return serverName.Substring(0, 1);
        }

        /// <summary>
        /// 根据服务器名获取服务器简称
        /// </summary>
        /// <param name="serverName">服务器名</param>
        /// <returns>服务器简称</returns>
        private static string ShortServerName(string serverName)
        {
            string value;
            if (Global.ServerDictionary.ContainsKey(serverName.ToLowerInvariant()) == true)
            {

                Global.ServerDictionary.TryGetValue(serverName.ToLowerInvariant(), out value);
            }
            else
            {
                return GetDefaultServerName(serverName);
            }

            if (value == null)
            {
                return GetDefaultServerName(serverName);
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// 根据url获取服务器简称
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="IsCode">string=false code=true</param>
        /// <returns>服务器简称</returns>
        public static string Url2BattleInfo(string url, bool IsCode)
        {
            string serverName = GetServerName(url);
            if (serverName == null || serverName == "")
            {
                return "无效网址";
            }
            else
            {
                if (IsCode == false)
                {
                    if (Global.StringSimpleServer == true)
                    {
                        serverName = ShortServerName(serverName) + "服";
                    }
                    if (Global.StringUpperServer == true)
                    {
                        string c = serverName.Substring(0, 1);
                        c = c.ToUpperInvariant();
                        if (serverName.Length > 1)
                        {
                            serverName = c + serverName.Substring(1);
                            return serverName;
                        }
                        else
                        {
                            return c;
                        }
                    }
                    else
                    {
                        return serverName;
                    }
                }
                else
                {
                    if (Global.CodeSimpleServer == true)
                    {
                        serverName = ShortServerName(serverName) + "服";
                    }
                    if (Global.CodeUpperServer == true)
                    {
                        string c = serverName.Substring(0, 1);
                        c = c.ToUpperInvariant();
                        if (serverName.Length > 1)
                        {
                            serverName = c + serverName.Substring(1);
                            return serverName;
                        }
                        else
                        {
                            return c;
                        }
                    }
                    else
                    {
                        return serverName;
                    }
                }
            }
        }
    }
}
