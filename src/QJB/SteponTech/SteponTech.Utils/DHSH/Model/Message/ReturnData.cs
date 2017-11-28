/*********************************
* 作者：xzf
* 日期：2017-06-17 14:06:19
* 操作：创建
* ********************************/
using SteponTech.Utils.DHSH.Model.Part;
using SteponTech.Utils.DHSH.Model.Part.Return;
using SteponTech.Utils.DHSH.Model.Part.Return.SmallReturnPart;
using SteponTech.Utils.DHSH.Utils;
using System;
using System.Collections.Generic;

namespace SteponTech.Utils.DHSH.Model.Message
{
    /// <summary>
    /// 获取数据的返回数据实体
    /// </summary>
    public class ReturnData : Data
    {
        public ReturnData()
        {
            header = new Header();
            body = new RData();
        }
        public ReturnData(string unuse)
        {
            header = new Header
            {
                senderCode = ConfigHelper.GetValue("UserCode"),
                messageReferenceNumber = new Random().Next() + "",
                messageMakingDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                messageName = "金沙官网消息",
                messageVersionNumber = ConfigHelper.GetValue("Version"),
            };
            body = new RData();
        }

        /// <summary>
        /// 获取数据返回数据的body部分
        /// </summary>
        public RData body { get; set; }

        /// <summary>
        /// 设置数据
        /// </summary>
        public void SetData(List<RDataRow> data)
        {
            body.dataTable.dataRow = data;
        }

        /// <summary>
        /// 设置附加数据
        /// </summary>
        public void SetExtData(List<RDataTableListItem> data)
        {
            body.dataTable.dataListMap.list = data;
        }
    }
}