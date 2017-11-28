/*********************************
 * 作者：xzf
 * 日期：2017-6-14 17:01:32
 * 操作：创建
 * ********************************/

namespace SteponTech.Utils.DHSH.Model
{
    /// <summary>
    /// 提交的
    /// </summary>
    public class Post
    {
        /// <summary>
        /// 密钥标识
        /// </summary>
        public string keyid { get; set; }

        /// <summary>
        /// 总线参数
        /// </summary>
        public Data data { get; set; }

        /// <summary>
        /// 获取实际post参数
        /// </summary>
        /// <returns></returns>
        public string Get()
        {
            return "";
        }
    }
}
