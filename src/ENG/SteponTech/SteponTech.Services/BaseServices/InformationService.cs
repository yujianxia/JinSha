/*****************************
 * 作者：xqx
 * 日期：2017年7月11日
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
    public class InformationService : ServiceContract<SteponContext>
    {
        private readonly SteponContext _context;
        public InformationService(IServiceProvider services) : base(services)
        {
            _context = services.GetService<SteponContext>();
        }
        /// <summary>
        /// 获取所有链接列表
        /// </summary>
        public List<Information> GeyInformation_All()
        {
            return _context.InformationEnglish.ToList();
        }
        /// <summary>
        /// 根据ID获取链接信息
        /// </summary>
        public Information GetInformationById(Guid id)
        {
            return _context.InformationEnglish.FirstOrDefault(e => e.Id == id);
        }
        /// <summary>
        /// 增加链接
        /// </summary>
        public OperationResult AddInformation(Information Information)
        {
            var result = new OperationResult();

            if (_context.Create(Information, true, true) != null)
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
        /// 修改链接
        /// </summary>
        public OperationResult ModifyInformation(Information InformationEnglish)
        {
            var result = new OperationResult();
            var has = GetInformationById(InformationEnglish.Id);
            InformationEnglish.CreationDate = has.CreationDate;
            if (has != null)
            {
                if (_context.Update(InformationEnglish, true) > 0)
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
        /// 根据ID删除链接
        /// </summary>
        public OperationResult DelInformation(Guid id)
        {
            var result = new OperationResult();
            if (_context.Delete<Information>(id, true) > 0)
            {
                result.State = OperationState.Success;
                result.Message = "操作成功";
            }
            else
            {
                result.State = OperationState.Faild;
                result.Message = "操作失败";
            }
            return result;
        }
        /// <summary>
        /// 根据栏目Id获取资讯信息
        /// </summary>
        /// <param name="columnId">栏目Id</param>
        /// <returns></returns>
        public List<Information> GetInformationByColumnId(Guid columnId)
        {
            return _context.InformationEnglish.Where(e => e.ColumnId == columnId).OrderBy(e => e.CreationDate).ToList();
        }
        /// <summary>
        /// 根据栏目Id获取资讯信息
        /// </summary>
        /// <param name="ids">栏目Id</param>
        /// <returns></returns>
        public List<Information> GetInformationByColumnIds(List<Guid> ids)
        {
            return _context.InformationEnglish.Where(e => ids.Contains(e.ColumnId)).OrderBy(e => e.CreationDate).ToList();
        }
        /// <summary>
        /// 根据栏目名称获取资讯信息
        /// </summary>
        /// <param name="columName">栏目名称</param>
        /// <returns></returns>
        public InformationAll GetInformationAllByColumName(string columName)
        {
            return _context.InformationEnglishAll.FirstOrDefault(e => e.ColumName == columName);
        }
    }
}
