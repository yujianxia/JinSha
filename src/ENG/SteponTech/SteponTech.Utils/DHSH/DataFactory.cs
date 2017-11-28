/*********************************
 * 作者：xzf
 * 日期：2017-06-16 21:40:10
 * 操作：创建
 * ********************************/
using SteponTech.Utils.DHSH.Model;
using SteponTech.Utils.DHSH.Model.Message;
using Newtonsoft.Json;

namespace SteponTech.Utils.DHSH
{
    /// <summary>
    /// 数据总线正常消息的工厂
    /// </summary>
    public class DataFactory
    {
        /// <summary>
        /// 数据总线正常消息创建
        /// </summary>
        public Model.Data CreateData(DataType type, string content)
        {
            var flag = string.IsNullOrEmpty(content);
            Model.Data data;
            switch (type)
            {
                //获取token的提交数据
                case DataType.Post_Token:
                    {
                        data = flag ? new PostToken() : JsonConvert.DeserializeObject<PostToken>(content);
                        break;
                    }
                //获取token的返回数据
                case DataType.Return_Token:
                    {
                        data = flag ? new ReturnToken() : JsonConvert.DeserializeObject<ReturnToken>(content);
                        break;
                    }
                //获取数据的提交数据
                case DataType.Post_Data:
                    {
                        data = flag ? new PostData() : JsonConvert.DeserializeObject<PostData>(content);
                        break;
                    }
                //获取数据的返回数据
                case DataType.Return_Data:
                    {
                        data = flag ? new ReturnData() : JsonConvert.DeserializeObject<ReturnData>(content);
                        break;
                    }
                case DataType.DH_Post_Data:
                    {
                        data = flag ? new DHPostData() : JsonConvert.DeserializeObject<DHPostData>(content);
                        break;
                    }
                default:
                    {
                        data = null;
                        break;
                    }
            }
            return data;
        }
    }
}
