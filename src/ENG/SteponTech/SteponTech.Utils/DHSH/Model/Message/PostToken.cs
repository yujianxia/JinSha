/*********************************
* 作者：xzf
* 日期：2017-06-17 14:04:59
* 操作：创建
* ********************************/
using SteponTech.Utils.DHSH.Model.Part;
using SteponTech.Utils.DHSH.Model.Part.Post;
using SteponTech.Utils.DHSH.Utils;
using System;

namespace SteponTech.Utils.DHSH.Model.Message
{
    /// <summary>
    /// 获取Token的提交数据
    /// </summary>
    public class PostToken : Data
    {
        public PostToken()
        {
            header = new Header();
            body = new PToken();
        }

        public PostToken(string name)
        {
            header = new Header();
            body = new PToken();
            header.messageMakingDateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            header.messageName = name;
            header.senderCode = ConfigHelper.GetValue("UserCode");
            header.messageVersionNumber = ConfigHelper.GetValue("Version");
            header.messageReferenceNumber = "2";
            body.token.userTokenID.value = ConfigHelper.GetValue("UserName");
            body.token.userTokenPWD.value = ConfigHelper.GetValue("Password");
        }

        /// <summary>
        /// 获取token提交数据实体的body部分
        /// </summary>
        public PToken body { get; set; }
    }
}