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
    public class LinkService : ServiceContract<SteponContext>
    {
        private readonly SteponContext _context;
        public LinkService(IServiceProvider services) : base(services)
        {
            _context = services.GetService<SteponContext>();
        }
        /// <summary>
        /// 获取所有链接列表
        /// </summary>
        public List<Link> GeyLink_All()
        {
            return _context.Link.OrderByDescending(x => x.LastUpdate).ToList();
        }
        /// <summary>
        /// 根据ID获取链接信息
        /// </summary>
        public Link GetLinkById(Guid id)
        {
            return _context.Link.FirstOrDefault(e => e.Id == id);
        }
        /// <summary>
        /// 增加链接
        /// </summary>
        public OperationResult AddLink(Link Link)
        {
            var result = new OperationResult();

            if (_context.Create(Link, true, true) != null)
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
        public OperationResult ModifyLink(Link Link)
        {
            var result = new OperationResult();
            var has = GetLinkById(Link.Id);
            Link.CreationDate = has.CreationDate;
            if (has != null)
            {
                if (_context.Update(Link, true) > 0)
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
        public OperationResult DelLink(Guid id)
        {
            var result = new OperationResult();
            if (_context.Delete<Link>(id, true) > 0)
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
