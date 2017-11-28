/*********************************
 * 作者：xzf
 * 日期：2017-6-14 00:18:45
 * 操作：创建
 * ********************************/
using Newtonsoft.Json;

namespace SteponTech.Utils.DHSH.Model
{
    /// <summary>
    /// 获取数据的时候发生错误返回的数据格式
    /// </summary>
    public class Error
    {
        /// <summary>
        /// 服务提供方返回的详细错误信息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 总线返回的错误信息
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 重写ToString方法，转换为Json字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
