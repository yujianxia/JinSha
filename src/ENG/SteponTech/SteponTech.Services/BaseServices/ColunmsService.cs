/*****************************
 * 作者：xqx
 * 日期：2017年6月31日
 * 操作：创建
 * ****************************/
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using System.Collections.Generic;
using Dapper;
using SteponTech.Data.BaseModels;
using Stepon.EntityFrameworkCore;

namespace SteponTech.Services.BaseServices
{
    public class ColunmsService : ServiceContract<SteponContext>
    {
        private readonly SteponContext _context;
        public ColunmsService(IServiceProvider services) : base(services)
        {
            _context = services.GetService<SteponContext>();
        }
        /// <summary>
        /// 获取所有栏目列表
        /// </summary>
        public List<ColunmsView> GeyColunms_All()
        {
            return _context.ColunmsEnglishView.ToList();
        }
        /// <summary>
        /// 根据ID获取栏目信息
        /// </summary>
        public Colunms GetColunmsById(Guid id)
        {
            return _context.ColunmsEnglish.FirstOrDefault(e => e.Id == id);
        }
        /// <summary>
        /// 增加栏目
        /// </summary>
        public OperationResult AddColunms(Colunms Colunms)
        {
            var result = new OperationResult();

            if (_context.Create(Colunms, true, true) != null)
            {
                result.State = OperationState.Success;
                result.Message = "添加成功";
            }
            else
            {
                result.State = OperationState.Faild;
                result.Message = "添加失败";
            }

            return result;
        }


        /// <summary>
        /// 修改栏目
        /// </summary>
        public OperationResult ModifyColunms(Colunms Colunms)
        {
            var result = new OperationResult();
            var has = GetColunmsById(Colunms.Id);
            Colunms.CreationDate = has.CreationDate;
	    Colunms.Sorting = has.Sorting;
            if (has != null)
            {
                if (_context.Update(Colunms, true) > 0)
                {
                    result.State = OperationState.Success;
                    result.Message = "修改成功";
                }
                else
                {
                    result.State = OperationState.Faild;
                    result.Message = "修改失败";
                }
            }
            else
            {
                result.State = OperationState.Faild;
                result.Message = "不存在该信息";
            }



            return result;
        }
        /// <summary>
        /// 修改栏目显示上下级
        /// </summary>
        public OperationResult ModifySort(Guid id, int oldcount, int newcount)
        {
            var result = new OperationResult();

            string sql = string.Empty;
            if (oldcount > newcount)
            {
                sql = "update \"Colunms\" set \"Sorting\" = \"Sorting\"+1 where \"Sorting\">=" + newcount + " and \"Sorting\"<" + oldcount + " and \"ColunmsId\"='" + id + "'";
            }
            else
            {
                sql = "update \"Colunms\" set \"Sorting\" = \"Sorting\"-1 where \"Sorting\">" + oldcount + " and \"Sorting\"<=" + newcount + " and \"ColunmsId\"='" + id + "'";
            }
            int i = _context.Database.GetDbConnection().Execute(sql);

            if (i == 0)
            {
                result.State = OperationState.Faild;
                result.Message = "修改失败";
            }
            else
            {
                result.State = OperationState.Success;
                result.Message = "修改成功";
            }
            return result;
        }
        /// <summary>
        /// 根据ID删除栏目
        /// </summary>
        public OperationResult DelColunms(Guid id)
        {
            var result = new OperationResult();
            var nextcolunms = _context.ColunmsEnglishView.Where(x => x.ColunmsId == id).FirstOrDefault();
            if (nextcolunms == null)
            {
                if (_context.InformationEnglish.Where(x => x.ColumnId == id).FirstOrDefault() != null)
                {
                    result.State = OperationState.Faild;
                    result.Message = "该栏目下存在咨询信息";
                }
                else
                {
                    if (_context.Delete<Colunms>(id, true) > 0)
                    {
                        result.State = OperationState.Success;
                        result.Message = "操作成功";
                    }
                    else
                    {
                        result.State = OperationState.Faild;
                        result.Message = "操作失败";
                    }
                }

            }
            else
            {
                result.State = OperationState.Faild;
                result.Message = "该栏目下存在其他栏目信息";
            }
            return result;
        }

        /// <summary>
        /// 根据模块Id查询栏目
        /// </summary>
        /// <param name="modelId">模块Id</param>
        /// <returns></returns>
        public List<Colunms> GetColunmsByModelId(Guid modelId)
        {
            return _context.ColunmsEnglish.Where(e => e.ModelId == modelId&&e.ColunmsId==Guid.Empty).OrderBy(e => e.Sorting).ToList();
        }
        /// <summary>
        /// 根据上级栏目Id查询栏目
        /// </summary>
        /// <param name="colunmsId">上级栏目Id</param>
        /// <returns></returns>
        public List<Colunms> GetColunmsByColunmsId(Guid colunmsId)
        {
            return _context.ColunmsEnglish.Where(e => e.ColunmsId == colunmsId).OrderBy(e=>e.Sorting).ToList();
        }
        /// <summary>
        /// 获取所有栏目
        /// </summary>
        /// <returns></returns>
        public List<Colunms> GetAllColunmses()
        {
            return _context.ColunmsEnglish.Where(e => e.Id != null).ToList();
        }
    }
}
