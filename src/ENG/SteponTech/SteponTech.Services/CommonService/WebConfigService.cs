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

namespace SteponTech.Services.CommonService
{
    public class WebConfigService : ServiceContract<SteponContext>
    {
        private readonly SteponContext _context;
        public WebConfigService(IServiceProvider services) : base(services)
        {
            _context = services.GetService<SteponContext>();
        }
        /// <summary>
        /// 获取所有Config信息
        /// </summary>
        public List<WebConfig> GeyWebConfig_All()
        {
            return _context.WebConfigEnglish.ToList();
        }
        /// <summary>
        /// 获取Config信息
        /// </summary>
        /// <returns></returns>
        public WebConfig GetWebConfig()
        {
            return _context.WebConfigEnglish.FirstOrDefault();
        }
        /// <summary>
        /// 根据ID获取Config信息
        /// </summary>
        public WebConfig GetWebConfigById(Guid id)
        {
            return _context.WebConfigEnglish.FirstOrDefault(e => e.Id == id);
        }
        /// <summary>
        /// 增加Config
        /// </summary>
        public OperationResult AddWebConfig(WebConfig WebConfig)
        {
            var result = new OperationResult();

            if (_context.Create(WebConfig, true, true) != null)
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
        /// 修改Config
        /// </summary>
        public OperationResult ModifyWebConfig(WebConfig WebConfig)
        {
            var result = new OperationResult();
            var has = GetWebConfigById(WebConfig.Id);
            WebConfig.CreationDate = has.CreationDate;
            if (has != null)
            {
                if (_context.Update(WebConfig, true) > 0)
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
        /// 根据ID删除Config
        /// </summary>
        //public OperationResult DelWebConfig(Guid id)
        //{
        //    var result = new OperationResult();
        //    if (_context.Delete<WebConfig>(id, true) > 0)
        //    {
        //        result.State = OperationState.Success;
        //        result.Message = "操作成功";
        //    }
        //    else
        //    {
        //        result.State = OperationState.Faild;
        //        result.Message = "操作失败";
        //    }
        //    return result;
        //}
    }
}

