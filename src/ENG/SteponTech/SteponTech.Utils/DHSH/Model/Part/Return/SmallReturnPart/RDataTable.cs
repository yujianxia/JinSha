/*********************************
* 作者：xzf
* 日期：2017-06-16 20:56:50
* 操作：创建
* ********************************/
using System;
using System.Collections.Generic;
namespace SteponTech.Utils.DHSH.Model.Part.Return.SmallReturnPart
{
    /// <summary>
    /// 获取token的返回数据的Body的主体
    /// </summary>
    public class RDataTable
    {
        public RDataTable(){
            dataRow=new List<RDataRow>();
            dataListMap=new RDataTableList();
        }
        /// <summary>
        /// 数据数量（目前来说一般是不正常的）
        /// </summary>
        public int datacount { get; set; }
        
        /// <summary>
        /// 数据表数据行数据
        /// </summary>
        public List<RDataRow> dataRow { get; set; }

        /// <summary>
        /// 数据表备注
        /// </summary>
        public string remark { get; set; }
        
        /// <summary>
        /// 数据表名称
        /// </summary>
        public string name { get; set; }
        
        /// <summary>
        /// 附加数据
        /// </summary>
        public RDataTableList dataListMap { get; set; }
    }
}