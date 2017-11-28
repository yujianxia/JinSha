/*********************************
* 作者：xzf
* 日期：2017-06-18 00:16:18
* 操作：创建
* ********************************/
using SteponTech.Utils.DHSH.Utils;
using System.Text;

namespace SteponTech.Utils.DHSH.Model.Config
{
    public class AESConfig
    {
        /// <summary>
        /// aes加密配置标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// aes加密 初始化向量
        /// </summary>
        public byte[] Iv { get; set; }

        /// <summary>
        /// aes加密 密钥
        /// </summary>
        public byte[] Key { get; set; }

        /// <summary>
        /// 随机配置
        /// </summary>
        public AESConfig() {
            var xml = new XmlHelper().GetRandomConfig();
            Iv = GetIV(xml);
            Key = Encoding.UTF8.GetBytes(xml.key);
            Id = xml.id;
        }
        /// <summary>
        /// 指定配置
        /// </summary>
        /// <param name="id">加密条目id</param>
        public AESConfig(string id) {
            var xml = new XmlHelper().GetConfig(id);
            Iv = GetIV(xml);
            Key = Encoding.UTF8.GetBytes(xml.key);
            Id = xml.id;
        }


        private byte[] GetIV(XmlRow xml) {
            long ivbase = long.Parse(xml.ivbase);
            long seed = long.Parse(xml.seed);
            long seq = long.Parse(xml.seq);
            long ivl = (ivbase - seed) + ((seed * seq) + seed) / seed;
            string iv = ivl + "";
            while (iv.Length < 16)
                iv = "E" + iv;
            return Encoding.UTF8.GetBytes(iv);
        }
    }
}
