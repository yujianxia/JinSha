/*********************************
* 作者：xzf
* 日期：2017-06-17 14:01:40
* 操作：创建
* ********************************/
using SteponTech.Utils.DHSH.Model.Part;
using SteponTech.Utils.DHSH.Model.Part.Post;
using SteponTech.Utils.DHSH.Model.Part.Post.SmallPostPart;
using SteponTech.Utils.DHSH.Utils;
using System;
using System.Collections.Generic;

namespace SteponTech.Utils.DHSH.Model.Message
{
    /// <summary>
    /// 获取数据的提交数据实体
    /// </summary>
    public class PostData : Data
    {
        public PostData()
        {
            header = new Header();
            body = new PData();
        }

        public PostData(string name)
        {
            header = new Header();
            body = new PData();
            header.messageMakingDateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            header.messageName = name;
            header.senderCode = ConfigHelper.GetValue("UserCode");
            header.messageVersionNumber = ConfigHelper.GetValue("Version");
            header.messageReferenceNumber = "2";
        }
        public void setToken(ReturnToken token)
        {
            body.accessParameters.token.systemID = token.body.token.systemID;
            body.accessParameters.token.userTokenRSL = token.body.token.userTokenRSL;
        }
        public void setParameters(List<Parameter> paras)
        {
            body.accessParameters.parameters.parameter.AddRange(paras);
        }
        /// <summary>
        /// 获取数据的提交数据的实体的body部分
        /// </summary>
        public PData body { get; set; }
    }
}