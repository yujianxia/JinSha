/*********************************
* 作者：xzf
* 日期：2017-06-17 14:51:46
* 操作：创建
* ********************************/
using SteponTech.Utils.DHSH.Model.Message;
using SteponTech.Utils.DHSH.Utils;

namespace SteponTech.Utils.DHSH.Model.Config
{
    /// <summary>
    /// 获取数据的配置实体
    /// </summary>
    public class DataConfig
    {
        /// <summary>
        /// 使用随机加密配置
        /// </summary>
        /// <param name="code">服务代码</param>
        public DataConfig(string code)
        {
            init(code);
            aesConfig = new AESConfig();
        }

        /// <summary>
        /// 使用特定加密配置
        /// </summary>
        /// <param name="code">服务代码</param>
        /// <param name="id">加密配置标识ID</param>
        public DataConfig(string code, string id)
        {
            init(code);
            aesConfig = new AESConfig(id);
        }

        private void init(string code)
        {
            var url = ConfigHelper.GetValue("DataUrlHeader");
            URL = (url.EndsWith("/") ? url : url + "/") + code;
            postData = new PostData("chengdulifang");
        }

        /// <summary>
        /// 获取数据的URL
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// 服务code
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 实体或者字段的作用或含义
        /// </summary>
        public AESConfig aesConfig { get; set; }

        /// <summary>
        /// 提交数据的实体
        /// </summary>
        public PostData postData { get; set; }
    }
}