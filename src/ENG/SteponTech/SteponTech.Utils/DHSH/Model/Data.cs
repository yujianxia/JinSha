/*********************************
 * 作者：xzf
 * 日期：2017-6-14 00:19:07
 * 操作：创建
 * ********************************/
using Newtonsoft.Json;
using SteponTech.Utils.DHSH.Model.Part;
namespace SteponTech.Utils.DHSH.Model
{
    /// <summary>
    /// 正常获取的返回信息
    /// </summary>
    public class Data
    {
        public Data(){
            header=new Header();
        }
        /// <summary>
        /// 消息头
        /// </summary>
        public Part.Header header { get; set; }

        /// <summary>
        /// 重写ToString方法，转换为Json字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}