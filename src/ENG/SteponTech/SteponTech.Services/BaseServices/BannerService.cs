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
    public class BannerService : ServiceContract<SteponContext>
    {
        private readonly SteponContext _context;
        public BannerService(IServiceProvider services) : base(services)
        {
            _context = services.GetService<SteponContext>();
        }
        /// <summary>
        /// 获取所有Banner列表
        /// </summary>
        public List<Banner> GeyBanner_All()
        {
            return _context.BannerEnglish.ToList();
        }
        /// <summary>
        /// 根据Id获取Banner信息
        /// </summary>
        public Banner GetBrandById(Guid id)
        {
            return _context.BannerEnglish.FirstOrDefault(e => e.Id == id);
        }
        /// <summary>
        /// 增加资源
        /// </summary>
        public OperationResult AddBrand(Banner banner)
        {
            var result = new OperationResult();

            if (_context.Create(banner, true, true) != null)
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
        public OperationResult ModifyBrand(Banner banner)
        {
            var result = new OperationResult();
            var has = GetBrandById(banner.Id);
            if (has != null)
            {
                banner.CreationDate = has.CreationDate;
                if (_context.Update(banner, true) > 0)
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
        /// 根据Id删除资源
        /// </summary>
        public OperationResult DelBrand(Guid id)
        {
            var result = new OperationResult();
            if (_context.Delete<Banner>(id, true) > 0)
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
        /// 根据栏目Id获取Banner信息
        /// </summary>
        /// <param name="modelsId">栏目Id</param>
        /// <returns></returns>
        public BannerView GetBannerViewByModelName(Guid modelsId)
        {
            return _context.BannerEnglishView.FirstOrDefault(e => e.ModelsId == modelsId);
        }
    }
}