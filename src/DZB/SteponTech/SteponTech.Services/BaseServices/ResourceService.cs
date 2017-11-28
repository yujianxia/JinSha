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
using SteponTech.Data.CommonModels;

namespace SteponTech.Services.BaseServices
{
    public class ResourceService : ServiceContract<SteponContext>
    {
        private readonly SteponContext _context;
        public ResourceService(IServiceProvider services) : base(services)
        {
            _context = services.GetService<SteponContext>();
        }
        /// <summary>
        /// 获取所有资源列表
        /// </summary>
        public List<Resource> GeyResource_All()
        {
            return _context.Resource.ToList();
        }
        /// <summary>
        /// 根据ID获取资源信息
        /// </summary>
        public Resource GetResourceById(Guid id)
        {
            return _context.Resource.FirstOrDefault(e => e.Id == id);
        }
        /// <summary>
        /// 增加资源
        /// </summary>
        public OperationResult AddResource(Resource Resource)
        {
            var result = new OperationResult();

            if (_context.Create(Resource, true, true) != null)
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
        /// 修改资源
        /// </summary>
        public OperationResult ModifyResource(Resource Resource)
        {
            var result = new OperationResult();
            var has = GetResourceById(Resource.Id);
            Resource.CreationDate = has.CreationDate;
            if (has != null)
            {
                if (_context.Update(Resource, true) > 0)
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
        /// 根据ID删除资源
        /// </summary>
        public OperationResult DelResource(Guid id)
        {
            var result = new OperationResult();
            if (_context.Delete<Resource>(id, true) > 0)
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
        /// 清空资源列表
        /// </summary>
        public OperationResult ClearResource()
        {
            var result = new OperationResult();

            if (_context.Database.GetDbConnection().Execute("DELETE FROM \"Resource\"") > 0)
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
        /// 导入资源
        /// </summary>
        public OperationResult ImportResource(string sql)
        {
            var result = new OperationResult();

            if (_context.Database.GetDbConnection().Execute(sql) > 0)
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
