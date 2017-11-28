/*********************************
 * 作者：xzf
 * 日期：2017-6-16 9:34:07
 * 操作：创建
 * ********************************/
using Newtonsoft.Json;
using SteponTech.Utils.DHSH.Model.Message;
using SteponTech.Utils.DHSH.Model;
using SteponTech.Utils.DHSH.Model.Config;
using SteponTech.Utils.DHSH.Utils;

namespace SteponTech.Utils.DHSH
{
    /// <summary>
    /// 总线系统数据获取类
    /// </summary>
    public class Obtainer
    {
        /// <summary>
        /// 获取token(默认为配置文件内的配置信息)
        /// </summary>
        /// <param name="returnToken">传出的返回的token信息</param>
        /// <param name="error">传出的返回的错误</param>
        /// <returns>获取是否成功</returns>
        public bool GetToken(out ReturnToken returnToken, out Error error)
        {
            returnToken = null;
            error = new Error()
            {
                message = "未到达总线已经发生错误！",
                code = "0"
            };
            var config = new TokenConfig();
            var aesHelper = new AesHelper();
            var data = aesHelper.Encrypt(config.postToken.ToString(), config.aesConfig);
            var webHelper = new WebHelper();
            var result = webHelper.PostData(config.URL, config.aesConfig.Id, data);
            result = result.Replace("\0", "");
            if (result.Contains("{"))
            {
                error = JsonConvert.DeserializeObject<Error>(result);
                if (error.code != "0")
                    return false;
            }
            result = aesHelper.Decrypt(result, config.aesConfig);
            result = result.Replace("\0", "");
            if (result.Contains("\"code\"")&& result.Contains("\"message\"") && result.Contains("\"content\""))
            {
                error = JsonConvert.DeserializeObject<Error>(result);
                if (error.code != "0")
                    return false;
            }
            var dataFactory = new DataFactory();
            returnToken = (ReturnToken)dataFactory.CreateData(DataType.Return_Token, result);
            return !string.IsNullOrEmpty(returnToken.header.senderCode);
        }

        /// <summary>
        /// 获取token（传入配置信息）
        /// </summary>
        /// <param name="config">传入配置信息</param>
        /// <param name="returnToken">传出的返回的token信息</param>
        /// <param name="error">传出的返回的错误</param>
        /// <returns>获取是否成功</returns>
        public bool GetToken(TokenConfig config, out ReturnToken returnToken, out Error error)
        {

            returnToken = null;
            error = new Error()
            {
                message = "未到达总线已经发生错误！",
                code = "0"
            };
            var aesHelper = new AesHelper();
            var data = aesHelper.Encrypt(config.postToken.ToString(), config.aesConfig);
            var webHelper = new WebHelper();
            var result = webHelper.PostData(config.URL, config.aesConfig.Id, data);
            result = result.Replace("\0", "");
            if (result.Contains("{"))
            {
                error = JsonConvert.DeserializeObject<Error>(result);
                if (error.code != "0")
                    return false;
            }
            result = aesHelper.Decrypt(result, config.aesConfig);
            result = result.Replace("\0", "");
            if (result.Contains("code") && result.Contains("message") && result.Contains("content"))
            {
                error = JsonConvert.DeserializeObject<Error>(result);
                if (error.code != "0")
                    return false;
            }
            var dataFactory = new DataFactory();
            returnToken = (ReturnToken)dataFactory.CreateData(DataType.Return_Token, result);
            return !string.IsNullOrEmpty(returnToken.header.senderCode);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="config">传入的配置信息</param>
        /// <param name="returnData">传出的返回的数据</param>
        /// <param name="error">传出的返回的错误</param>
        /// <returns>是否成功</returns>
        public bool GetData(DataConfig config, out ReturnData returnData, out Error error)
        {

            returnData = null;
            error = new Error()
            {
                message = "未到达总线已经发生错误！",
                code = "0"
            };
            var aesHelper = new AesHelper();
            var temp = config.postData.ToString();
            var data = aesHelper.Encrypt(config.postData.ToString(), config.aesConfig);
            var webHelper = new WebHelper();
            var result = webHelper.PostData(config.URL, config.aesConfig.Id, data);
            result = result.Replace("\0", "");
            if (result.Contains("{"))
            {
                error = JsonConvert.DeserializeObject<Error>(result);
                if (error.code != "0")
                    return false;
            }
            result = aesHelper.Decrypt(result, config.aesConfig);
            result = result.Replace("\0", "");
            if (result.Contains("\"code\"") && result.Contains("\"message\"") && result.Contains("\"content\""))
            {
                error = JsonConvert.DeserializeObject<Error>(result);
                if (error.code != "0")
                    return false;
            }
            var dataFactory = new DataFactory();
            returnData = (ReturnData)dataFactory.CreateData(DataType.Return_Data, result);
            return !string.IsNullOrEmpty(returnData.header.senderCode);
        }
    }
}
