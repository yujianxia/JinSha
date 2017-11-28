/*********************************
 * 作者：xzf
 * 日期：2017-06-18 00:18:06
 * 操作：创建
 * ********************************/
using RestSharp;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace SteponTech.Utils.DHSH.Utils
{
    public class WebHelper
    {
        private static HttpClient Client = null;

        public WebHelper()
        {
            if (Client == null)
                Client = new HttpClient();
        }

        public void Dispose()
        {
            Client.Dispose();
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url">请求url路径</param>
        /// <param name="data">请求post数据</param>
        /// <returns></returns>
        public string PostData(string url, string keyid, string data)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("keyid", ToUrlString(keyid));
            request.AddParameter("data", (data));
            var response = client.ExecuteAsPost(request, "POST");
            if(response.StatusCode==HttpStatusCode.OK)
                return WebUtility.UrlDecode(response.Content);
            return "";
        }

        public string ToUrlString(string str)
        {
            var result = new StringBuilder();
            foreach (char c in str)
            {
                var temp = WebUtility.UrlEncode(c.ToString());
                if (temp != null)
                    if (temp.Length > 1)
                        result.Append(temp.ToUpper());
                    else
                        result.Append(c);
                else
                {
                    return "";
                }
            }
            return result.ToString();
        }
    }
}
