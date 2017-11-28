/*****************************
 * 作者：xqx
 * 日期：2017年6月26日
 * 操作：创建
 * ****************************/
using Stepon.EntityFrameworkCore;

namespace SteponTech.Data.BaseModels
{
    /// <summary>
    /// 资源
    /// </summary>
    public class Resource : BaseEntity
    {
        /// <summary>
        /// 提交人
        /// </summary>
        public System.String UploadMan { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public System.String CheckMan { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public System.String State { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public System.String Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public System.String Content { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public System.String Flie { get; set; }

        /// <summary>
        /// 置顶
        /// </summary>
        public System.Boolean IsUp { get; set; } = false;

        /// <summary>
        /// 备注
        /// </summary>
        public System.String Mark { get; set; }

    
    }
}
