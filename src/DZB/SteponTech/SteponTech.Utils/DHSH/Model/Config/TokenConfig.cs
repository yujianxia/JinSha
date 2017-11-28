/*********************************
* 作者：xzf
* 日期：2017-06-17 14:54:35
* 操作：创建
* ********************************/
using SteponTech.Utils.DHSH.Model.Message;
using SteponTech.Utils.DHSH.Utils;

namespace SteponTech.Utils.DHSH.Model.Config
{
    /// <summary>
    /// 获取token的配置实体
    /// </summary>
    public class TokenConfig
    {
        /// <summary>
        /// 使用随机加密配置
        /// </summary>
        public TokenConfig()
        {
            init();
            aesConfig = new AESConfig();
        }

        /// <summary>
        /// 使用特定加密配置
        /// </summary>
        /// <param name="id">加密配置标识ID</param>
        public TokenConfig(string id)
        {
            init();
            aesConfig = new AESConfig(id);
        }

        private void init()
        {
            URL = ConfigHelper.GetValue("TokenUrl");
            postToken = new PostToken("chengdulifang");
        }

        /// <summary>
        /// 获取token的url
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// 获取token的提交数据
        /// </summary>
        public PostToken postToken { get; set; }

        /// <summary>
        /// 实体或者字段的作用或含义
        /// </summary>
        public AESConfig aesConfig { get; set; }
    }
}