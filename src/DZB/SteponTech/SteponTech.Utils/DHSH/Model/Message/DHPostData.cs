using SteponTech.Utils.DHSH.Model.Part;
using SteponTech.Utils.DHSH.Model.Part.Post;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SteponTech.Utils.DHSH.Model.Message
{
    public class DHPostData : Data
    {
        public DHPostData()
        {
            header = new Header();
            body = new DHPData();
        }

        /// 获取数据的提交数据的实体的body部分
        /// </summary>
        public DHPData body { get; set; }
    }
}