using Microsoft.AspNetCore.WebUtilities;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Tools
{
    /// <summary>
    /// Project:Sunuer Manage
    /// Description:Tools 类，提供常用的工具方法
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class Tools
    {
        /// <summary>
        /// 截取字符串，多余部分用 "..." 表示
        /// </summary>
        /// <param name="msg">输入的字符串</param>
        /// <param name="len">最大长度</param>
        /// <returns>截取后的字符串</returns>
        public static string SubStrings(string msg, int len)
        {
            if (msg.Length <= len)
            {
                return msg;  // 如果字符串长度小于等于指定长度，则直接返回
            }
            return msg.Substring(0, len) + "...";  // 超过长度部分用 "..." 表示
        }

        /// <summary>
        /// 隐藏手机号中间4位
        /// </summary>
        /// <param name="phones">输入的手机号</param>
        /// <returns>隐藏中间4位后的手机号</returns>
        public static string SubPhone(string phones)
        {
            if (phones.Length != 11)
            {
                return phones;  // 如果手机号长度不是 11 位，直接返回原始手机号
            }
            return phones.Substring(0, 3) + "****" + phones.Substring(7, 4);
        }

        /// <summary>
        /// 生成当前时间的 Unix 时间戳（秒）
        /// </summary>
        /// <returns>当前 Unix 时间戳</returns>
        public static long CreatenTimestamp()
        {
            return (DateTime.UtcNow.Ticks - 621355968000000000) / 10000000;
        }

        /// <summary>
        /// 生成指定长度的随机字符串
        /// </summary>
        /// <param name="pwdlen">字符串长度</param>
        /// <returns>随机生成的字符串</returns>
        public static string MakePassword(int pwdlen)
        {
            const string pwdchars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random rnd = new Random();
            char[] result = new char[pwdlen];
            for (int i = 0; i < pwdlen; i++)
            {
                result[i] = pwdchars[rnd.Next(pwdchars.Length)];
            }
            return new string(result);
        }

        /// <summary>
        /// 生成指定长度的随机数字字符串
        /// </summary>
        /// <param name="pwdlen">字符串长度</param>
        /// <returns>随机生成的数字字符串</returns>
        public static string MakephoneSMS(int pwdlen)
        {
            const string digits = "0123456789";
            Random rnd = new Random();
            char[] result = new char[pwdlen];
            for (int i = 0; i < pwdlen; i++)
            {
                result[i] = digits[rnd.Next(digits.Length)];
            }
            return new string(result);
        }

        /// <summary>
        /// 计算两个日期相差的天数
        /// </summary>
        /// <param name="startDate">起始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>相差的天数</returns>
        public static int DateToDays(string startDate, string endDate)
        {
            DateTime dt1 = DateTime.Parse(startDate);
            DateTime dt2 = DateTime.Parse(endDate);
            TimeSpan ts = dt2 - dt1;  // 修正：应计算 endDate - startDate
            return Math.Abs(ts.Days);  // 返回绝对值，避免负值
        }

        /// <summary>
        /// 检查日期格式是否有效
        /// </summary>
        /// <param name="dateStr">日期字符串</param>
        /// <returns>是否为有效日期</returns>
        public static bool IsDate(string dateStr)
        {
            return DateTime.TryParse(dateStr, out _);  // 使用 TryParse 尝试解析日期
        }

        /// <summary>
        /// 判断是否为正整数
        /// </summary>
        /// <param name="str">需要判断的字符串</param>
        /// <returns>是否为正整数</returns>
        public static bool IsNonNegativeInteger(string str)
        {
            return Regex.IsMatch(str, @"^[1-9]\d*$");  // 匹配正整数
        }

        /// <summary>
        /// 判断是否为手机号
        /// </summary>
        /// <param name="str">需要判断的字符串</param>
        /// <returns>是否为有效手机号</returns>
        public static bool IsPhoneNumber(string str)
        {
            return Regex.IsMatch(str, @"^1[3-9]\d{9}$");  // 手机号正则校验
        }

        /// <summary>
        /// 判断是否为车牌号
        /// </summary>
        /// <param name="str">需要判断的车牌号</param>
        /// <returns>是否为有效车牌号</returns>
        public static bool IsCarNumber(string str)
        {
            const string pattern = @"^[京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼][A-Z][A-HJ-NP-Z0-9]{5}[A-HJ-NP-Z0-9挂学警港澳]$";
            return Regex.IsMatch(str, pattern);
        }

        /// <summary>
        /// 判断是否为身份证号
        /// </summary>
        /// <param name="str">身份证号字符串</param>
        /// <returns>是否为有效身份证号</returns>
        public static bool IsIdCard(string str)
        {
            const string pattern = @"(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)";
            return Regex.IsMatch(str, pattern);
        }

        /// <summary>
        /// 判断是否为有效邮箱地址
        /// </summary>
        /// <param name="str">邮箱字符串</param>
        /// <returns>是否为有效邮箱</returns>
        public static bool IsEmail(string str)
        {
            const string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(str, pattern);
        }
        /// <summary>
        /// 获取用户的 IP 地址
        /// 优先获取外网 IP，如果没有则获取内网 IP
        /// </summary>
        /// <param name="httpContext">当前请求的 HttpContext</param>
        /// <returns>用户 IP 地址</returns>
        public static string GetUserIp(HttpContext httpContext)
        {
            // 优先尝试从 X-Forwarded-For 获取外网 IP
            if (httpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                var forwardedHeader = httpContext.Request.Headers["X-Forwarded-For"].ToString();
                if (!string.IsNullOrWhiteSpace(forwardedHeader))
                {
                    // 取第一个非空 IP 地址（多个 IP 用逗号分隔，顺序为：客户端 IP, 代理 IP）
                    var ips = forwardedHeader.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var ip in ips)
                    {
                        var trimmedIp = ip.Trim();
                        if (IsValidIpAddress(trimmedIp) && !IsPrivateIp(trimmedIp))
                        {
                            return trimmedIp; // 返回第一个有效的外网 IP
                        }
                    }
                }
            }

            // 如果没有外网 IP，获取内网 IP
            var remoteIpAddress = httpContext.Connection.RemoteIpAddress?.ToString();
            return !string.IsNullOrEmpty(remoteIpAddress) && IsValidIpAddress(remoteIpAddress)
                ? remoteIpAddress
                : "Unknown"; // 返回内网 IP 或 Unknown
        }

        /// <summary>
        /// 验证是否为有效的 IP 地址
        /// </summary>
        /// <param name="ipAddress">待验证的 IP 地址</param>
        /// <returns>是否为有效的 IP 地址</returns>
        private static bool IsValidIpAddress(string ipAddress)
        {
            return IPAddress.TryParse(ipAddress, out _);
        }

        /// <summary>
        /// 判断是否为私有 IP 地址（内网 IP）
        /// </summary>
        /// <param name="ipAddress">待验证的 IP 地址</param>
        /// <returns>是否为内网 IP</returns>
        private static bool IsPrivateIp(string ipAddress)
        {
            if (IPAddress.TryParse(ipAddress, out var ip))
            {
                var bytes = ip.GetAddressBytes();
                switch (bytes[0])
                {
                    case 10:
                        return true; // 10.0.0.0 - 10.255.255.255
                    case 172:
                        return bytes[1] >= 16 && bytes[1] <= 31; // 172.16.0.0 - 172.31.255.255
                    case 192:
                        return bytes[1] == 168; // 192.168.0.0 - 192.168.255.255
                    default:
                        return false;
                }
            }
            return false;
        }


        /// <summary>
        /// 获取当前时间的交易日期格式（yyyyMMdd）
        /// </summary>
        public static string GetTransDate()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }

        /// <summary>
        /// 生成订单号（16位数字）
        /// </summary>
        public static string GetOrderId()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
        }

        /// <summary>
        /// 获取当前日期是本年度的第几周
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>第几周</returns>
        public static int GetWeekNumOfTheYear(DateTime date)
        {
            CultureInfo ci = new CultureInfo("zh-CN");
            Calendar cal = ci.Calendar;
            CalendarWeekRule rule = ci.DateTimeFormat.CalendarWeekRule;
            DayOfWeek firstDay = ci.DateTimeFormat.FirstDayOfWeek;
            return cal.GetWeekOfYear(date, rule, firstDay);
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static String MD5(string password)
        {
            Byte[] clearBytes = new UnicodeEncoding().GetBytes(password);
            Byte[] hashedBytes = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);

            return BitConverter.ToString(hashedBytes);
        }


        //动态查询JSON
        public static string? FindDynamicValue(
       List<Dictionary<string, List<Dictionary<string, string>>>>? languages,
       string targetLanguage,
       string targetKey
   )
        {
            if (languages == null)
            {
                return null;
            }

            // 查找目标语言
            var languageData = languages.FirstOrDefault(lang => lang.ContainsKey(targetLanguage));

            if (languageData != null && languageData.TryGetValue(targetLanguage, out var details))
            {
                // 查找目标键的值
                var entry = details.FirstOrDefault(dict => dict.ContainsKey(targetKey));
                return entry?[targetKey];
            }

            return null; // 未找到目标语言或目标键
        }
        /// <summary>
        /// 替换或添加 URL 查询参数的值
        /// </summary>
        /// <param name="url">原始 URL</param>
        /// <param name="paramName">需要替换或添加的参数名</param>
        /// <param name="paramValue">新的参数值</param>
        /// <returns>返回修改后的 URL</returns>
        public static string ReplaceUrlParameter(string url, string paramName, string paramValue)
        {
            // 创建一个 UriBuilder 实例来解析和构建 URL
            var uriBuilder = new UriBuilder(url);

            // 解析查询字符串为字典
            var query = QueryHelpers.ParseQuery(uriBuilder.Query);

            // 将解析后的查询参数转换为 Dictionary 以支持修改
            var queryDictionary = new Dictionary<string, string>(query.ToDictionary(k => k.Key, v => v.Value.ToString()))
            {
                [paramName] = paramValue // 替换或添加参数
            };

            // 使用 & 拼接修改后的查询参数
            uriBuilder.Query = string.Join("&", queryDictionary.Select(kv => $"{kv.Key}={kv.Value}"));

            // 返回新的 URL
            return uriBuilder.ToString();
        }
    }
}
