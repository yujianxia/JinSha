/*********************************
 * 作者：xzf
 * 日期：2017-6-16 9:59:38
 * 操作：创建
 * ********************************/
using SteponTech.Utils.DHSH.Model.Part.Post.SmallPostPart;
namespace SteponTech.Utils.DHSH.Model.Part.Post
{
    /// <summary>
    /// 查询数据报文body部分
    /// </summary>
    public class DHPData 
    {
        public DHPData(){
            accessParameters=new DHAccessParameters();
            token = new PTokenInfo();
        }
        /// <summary>
        /// Token信息
        /// </summary>
        public PTokenInfo token { get; set; }
        /// <summary>
        /// 具体内容
        /// </summary>
        public DHAccessParameters accessParameters { get; set; }
    }
}