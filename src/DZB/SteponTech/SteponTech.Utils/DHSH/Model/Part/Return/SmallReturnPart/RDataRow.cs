/*********************************
 * 作者：xzf
 * 日期：2017/6/18 11:50:06
 * 操作：创建
 * ********************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteponTech.Utils.DHSH.Model.Part.Return.SmallReturnPart
{
    /// <summary>
    /// RDataRow
    /// </summary>
    public class RDataRow
    {
        /// <summary>
        /// 获取数据的返回数据的数据部分
        /// </summary>
        public List<RDataColumn> column { get; set; }
    }
}
