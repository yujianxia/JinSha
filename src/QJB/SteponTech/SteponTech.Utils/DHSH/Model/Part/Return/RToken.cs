/*********************************
 * 作者：xzf
 * 日期：2017-6-16 9:25:26
 * 操作：创建
 * ********************************/
using SteponTech.Utils.DHSH.Model.Part.Return.SmallReturnPart;
namespace SteponTech.Utils.DHSH.Model.Part.Return
{
    /// <summary>
    /// RToken
    /// </summary>
    public class RToken
    {
        public RToken(){
            token=new RTokenInfo();
        }
        public RTokenInfo token { get; set;}
    }
}
