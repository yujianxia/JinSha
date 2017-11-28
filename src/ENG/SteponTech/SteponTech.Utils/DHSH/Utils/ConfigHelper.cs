/*********************************
 * 作者：xzf
 * 日期：2017-06-18 00:18:00
 * 操作：创建
 * ********************************/

using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SteponTech.Utils.DHSH.Utils
{
    public class ConfigHelper
    {
        static JObject jobj = null;

        private static void init()
        {
            jobj = ReadFile();
        }
        private static JObject ReadFile()
        {
            var bytes = File.ReadAllBytes("DHSH.json");
            if (bytes.Length == 0) throw new Exception("配置文件错误！");
            if (bytes[0] != '{')
            {
                var index = 1;
                while (bytes[index] == '{')
                {
                    index++;
                }
                var temp = new byte[bytes.Length - index + 1]; //Array.Copy(a, 3, b, 0, 6);
                Array.Copy(bytes, index, temp, 0, temp.Length);
                bytes = temp;
            }
            var fileContent = Encoding.UTF8.GetString(bytes);
            string RegexStr = string.Empty;
            RegexStr = "(//[^\n]*\n)|(//[^(\r\n)]*\r\n)";
            var match = Regex.Matches(fileContent, RegexStr);
            foreach (var m in match)
            {
                fileContent.Replace(m.ToString(), "");
            }
            RegexStr = @"/\*[^\*/]*\*/";
            match = Regex.Matches(fileContent, RegexStr);
            foreach (var m in match)
            {
                fileContent.Replace(m.ToString(), "");
            }
            return JObject.Parse(fileContent);
        }
        /// <summary>
        /// 根据配置名称获取配置信息
        /// </summary>
        public static string GetValue(string key)
        {
            if (jobj == null)
                init();
            JToken data;
            var flag = jobj.TryGetValue(key, StringComparison.CurrentCulture, out data);
            if (!flag) throw new Exception("没有属性名为" + key + "的值！");
            var value = data + "";
            return string.IsNullOrEmpty(value) ? "" : value;
        }
    }
}
