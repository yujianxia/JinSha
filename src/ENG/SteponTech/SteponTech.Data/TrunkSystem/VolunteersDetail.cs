/*****************************
 * 作者：xqx
 * 日期：2017年7月12日
 * 操作：创建
 * ****************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace SteponTech.Data.TrunkSystem
{
    /// <summary>
    /// 志愿者系统实体
    /// </summary>
    /// <returns></returns>
    public class VolunteersDetail
    {
        /// <summary>
        /// ID唯一标示
        /// </summary>
        public System.String systemId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public System.String name { get; set; }

        /// <summary>
        /// 性别，只能给LADIES或者GENTLEME
        /// </summary>
        public System.String sex { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public System.String nation { get; set; }

        /// <summary>
        /// 出生日期 ****-**-**
        /// </summary>
        public System.String birthday { get; set; }

        /// <summary>
        /// 身高（厘米）
        /// </summary>
        public System.String height { get; set; }

        /// <summary>
        /// 政治面貌
        /// </summary>
        public System.String political { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        public System.String occupation { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        public System.String qualifications { get; set; }

        /// <summary>
        /// 身体状况
        /// </summary>
        public System.String health { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public System.String mobile { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public System.String email { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public System.String idNumber { get; set; }

        /// <summary>
        /// 所属单位/学校
        /// </summary>
        public System.String unit { get; set; }

        /// <summary>
        /// 专业及特长
        /// </summary>
        public System.String specialty { get; set; }

        /// <summary>
        /// 申请原因及期望
        /// </summary>
        public System.String rAndE { get; set; }

        /// <summary>
        /// 预计服务时间
        /// </summary>
        public System.String serviceTime { get; set; }

        /// <summary>
        /// 预计服务期限
        /// </summary>
        public System.String serviceTerm { get; set; }

        /// <summary>
        /// 意向服务岗位
        /// </summary>
        public System.String servicePost { get; set; }

        /// <summary>
        /// 志愿者类型 志愿者相关选项中 直接传type
        /// </summary>
        public System.String type { get; set; }

        /// <summary>
        /// 监护人
        /// </summary>
        public System.String guardian { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public System.String img { get; set; }
    }
}
