/*********************************
* 作者：xzf
* 日期：2017-06-16 15:30:25
* 操作：创建
* ********************************/
using SteponTech.Utils.DHSH.Model.Part;
namespace SteponTech.Utils.DHSH.Model.Part.Post.SmallPostPart
{
    /// <summary>
    /// 获取数据参数实体body部分的参数集内的Token数据部分
    /// </summary>
    public class PTokenInfo
    {
        public PTokenInfo(){
            systemID=new Value();
            userTokenRSL=new Value();
        }
        /// <summary>
        /// 系统ID
        /// </summary>
        public Value systemID { get; set; }
        
        /// <summary>
        /// 用户Token信息
        /// </summary>
        public Value userTokenRSL { get; set; }
    }
}