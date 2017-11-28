/*********************************
* 作者：xzf
* 日期：2017-06-17 14:08:23
* 操作：创建
* ********************************/
using SteponTech.Utils.DHSH.Model.Part;
using SteponTech.Utils.DHSH.Model.Part.Return;

namespace SteponTech.Utils.DHSH.Model.Message
{
    /// <summary>
    /// 获取token的返回数据
    /// </summary>
    public class ReturnToken:Data
    {
        public ReturnToken()
        {
            header =new Header();
            body=new RToken();
        }

        public RToken body { get; set; }
    }
}