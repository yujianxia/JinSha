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
    public class PData 
    {
        public PData(){
            accessParameters=new AccessParameters();
        }

        /// <summary>
        /// 具体内容
        /// </summary>
        public AccessParameters accessParameters { get; set; }
    }
}