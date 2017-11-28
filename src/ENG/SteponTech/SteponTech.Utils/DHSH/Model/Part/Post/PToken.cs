/*********************************
 * 作者：xzf
 * 日期：2017-6-16 9:24:07
 * 操作：创建
 * ********************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteponTech.Utils.DHSH.Model.Part.Post.SmallPostPart;
namespace SteponTech.Utils.DHSH.Model.Part.Post
{
    /// <summary>
    /// 获取Token的提交数据Body部分的实体
    /// </summary>
    public class PToken
    {
        public PToken(){
            token=new TokenUserInfo();
        }
        /// <summary>
        /// 获取token的提交数据Body部分的主要字段
        /// </summary>
        public TokenUserInfo token {get;set;}
    }
}
