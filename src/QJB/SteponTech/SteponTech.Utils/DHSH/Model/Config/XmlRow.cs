/*********************************
 * 作者：xzf
 * 日期：2017-6-17 17:21:03
 * 操作：创建
 * ********************************/

namespace SteponTech.Utils.DHSH.Model.Config
{
    /// <summary>
    /// xml文件的一条条目
    /// </summary>
    public class XmlRow
    {
        public string id{ get; set; }

        public string key { get; set; }

        public string ivbase { get; set; }

        public string seed { get; set; }

        public string seq { get; set; }
    }
}
