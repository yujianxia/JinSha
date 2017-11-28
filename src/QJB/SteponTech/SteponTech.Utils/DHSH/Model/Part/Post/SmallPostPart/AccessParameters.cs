/*********************************
* 作者：xzf
* 日期：2017-06-16 15:27:49
* 操作：创建
* ********************************/

namespace SteponTech.Utils.DHSH.Model.Part.Post.SmallPostPart
{
    /// <summary>
    /// 获取数据参数实体body部分的参数集实体
    /// </summary>
    public class AccessParameters
    {
        public AccessParameters(){
            token=new PTokenInfo();
            parameters=new ParameterInfo();
        }
        /// <summary>
        /// Token信息
        /// </summary>
        public PTokenInfo token{get;set;}
        
        /// <summary>
        /// 参数信息
        /// </summary>
        public ParameterInfo parameters{get;set;}
    }
}