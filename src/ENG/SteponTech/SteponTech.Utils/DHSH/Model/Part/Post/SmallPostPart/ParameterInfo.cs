/*********************************
* 作者：xzf
* 日期：2017-06-16 15:32:15
* 操作：创建
* ********************************/

using System.Collections.Generic;

namespace SteponTech.Utils.DHSH.Model.Part.Post.SmallPostPart
{
    /// <summary>
    /// 获取数据参数实体body部分的参数集内的参数数据部分
    /// </summary>
    public class ParameterInfo
    {
        public ParameterInfo(){
            parameter=new List<Parameter>();
        }
        /// <summary>
        /// 参数数组集合
        /// </summary>
        public List<Parameter> parameter { get; set; }
    }
}