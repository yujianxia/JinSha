/*********************************
 * 作者：xzf
 * 日期：2017/7/26 14:49:23
 * 操作：创建
 * ********************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace SteponTech.Utils.DHSH.Model.Part.Post.SmallPostPart
{
	/// <summary>
    /// DHAccessParameters
    /// </summary>
    public class DHAccessParameters
    {
        public DHAccessParameters()
        {
            parameters = new ParameterInfo();
        }
        /// <summary>
        /// 参数信息
        /// </summary>
        public ParameterInfo parameters { get; set; }
    }
}
