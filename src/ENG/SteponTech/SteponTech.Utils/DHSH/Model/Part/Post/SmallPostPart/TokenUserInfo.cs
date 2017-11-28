/*********************************
* 作者：xzf
* 日期：2017-06-16 15:47:10
* 操作：创建
* ********************************/
using System;
using System.Collections.Generic;
using SteponTech.Utils.DHSH.Model.Part;
namespace SteponTech.Utils.DHSH.Model.Part.Post.SmallPostPart
{
    /// <summary>
    /// 获取token提交数据的用户信息部分
    /// </summary>
    public class TokenUserInfo
    {
        public TokenUserInfo(){

            userTokenID=new Value();
            userTokenPWD=new Value();
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Value userTokenID { get; set;}
        
        /// <summary>
        /// 用户密码
        /// </summary>
        public Value userTokenPWD{get;set;}
    }
}