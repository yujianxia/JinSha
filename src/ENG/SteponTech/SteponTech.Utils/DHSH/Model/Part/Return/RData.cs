/*********************************
 * 作者：xzf
 * 日期：2017-6-16 9:59:54
 * 操作：创建
 * ********************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteponTech.Utils.DHSH.Model.Part.Return.SmallReturnPart;
namespace SteponTech.Utils.DHSH.Model.Part.Return
{
    /// <summary>
    /// 正常情况下的返回数据的实体的body部分
    /// </summary>
    public class RData
    {
        public RData(){
            dataTable = new RDataTable();
        }
        public RDataTable dataTable {get;set;}
    }
}
