/*********************************
* 作者：xzf
* 日期：2017-06-16 16:02:21
* 操作：创建
* ********************************/
using System;
using System.Collections.Generic;
using SteponTech.Utils.DHSH.Model.Part;
namespace SteponTech.Utils.DHSH.Model.Part.Return.SmallReturnPart
{
    /// <summary>
    /// 获取token的返回数据的Body的主体部分实体
    /// </summary>
    public class RTokenInfo
    {
        public RTokenInfo(){
            systemID=new Value();
            userTokenRSL=new Value();
            secureDecryption=new Value();
            userTokenID=new Value();
            userTokenPWD=new Value();
        }
        /// <summary>
        /// 系统ID
        /// </summary>
        public Value systemID { get; set; }
        
        /// <summary>
        /// token字符串
        /// </summary>
        public Value userTokenRSL { get; set; }

        /// <summary>
        /// unkonwn
        /// </summary>
        public Value secureDecryption { get; set; }
        
        /// <summary>
        /// 用户ID
        /// </summary>
        public Value userTokenID { get; set; }
        
        /// <summary>
        /// 用户密码
        /// </summary>
        public Value userTokenPWD { get; set; }
    }
}