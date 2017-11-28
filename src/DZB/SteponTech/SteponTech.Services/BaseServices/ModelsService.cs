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
    public class ModelsService : ServiceContract<SteponContext>
    {
        private readonly SteponContext _context;
        public ModelsService(IServiceProvider services) : base(services)
        {
            _context = services.GetService<SteponContext>();
        }
        /// <summary>
        /// 获取所有板块列表
        /// </summary>
        public List<Models> GeyModels_All()
        {
            return _context.Models.OrderByDescending(x => x.LastUpdate).ToList();
        }
        /// <summary>
        /// 根据ID获取板块信息
        /// </summary>
        public Models GetModelsById(Guid id)
        {
            return _context.Models.FirstOrDefault(e => e.Id == id);
        }
        /// <summary>
        /// 根据板块名称获取板块信息
        /// </summary>
        /// <param name="modelName">板块名称</param>
        /// <returns></returns>
        public Models GetModelsByModelName(string modelName)
        {
            return _context.Models.FirstOrDefault(e => e.ModelName == modelName);
        }
        /// <summary>
        /// 获取所有要显示的板块信息
        /// </summary>
        /// <returns></returns>
        public List<Models> GetAllIsShowModels()
        {
            return _context.Models.Where(e => e.IsShow).OrderBy(e => e.CreationDate).ToList();
        }

        /// <summary>
        /// 增加板块
        /// </summary>
        public OperationResult AddModels(Models models)
        {
            var result = new OperationResult();

            if (_context.Create(models, true, true) != null)
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
        /// 修改板块
        /// </summary>
        public OperationResult ModifyModels(Models models)
        {
            var result = new OperationResult();
            var has = GetModelsById(models.Id);
            models.CreationDate = has.CreationDate;
            if (has != null)
            {
                if (_context.Update(models, true) > 0)
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
        /// 根据ID删除板块
        /// </summary>
        public OperationResult DelModels(Guid id)
        {
            var result = new OperationResult();
            if (_context.Delete<Models>(id, true) > 0)
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
    }
}
