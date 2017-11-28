/*****************************
 * 作者：xqx
 * 日期：2017年6月31日
 * 操作：创建
 * ****************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using SteponTech.Data.BaseModels;
using Stepon.EntityFrameworkCore;
using SteponTech.Data.CommonModels;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SteponTech.Services.CommonService
{
    public class MappingService : ServiceContract<SteponContext>
    {
        private readonly SteponContext _context;
        public MappingService(IServiceProvider services) : base(services)
        {
            _context = services.GetService<SteponContext>();
        }
        /// <summary>
        /// 获取所有权限列表
        /// </summary>
        public List<Mapping> GeyMapping_All()
        {
            return _context.Mapping.ToList();
        }
        /// <summary>
        /// 根据ID获取权限信息
        /// </summary>
        public Mapping GetMappingById(string id)
        {
            return _context.Mapping.FirstOrDefault(e => e.Id == id);
        }
        /// <summary>
        /// 增加权限
        /// </summary>
        public OperationResult AddMapping(Mapping Mapping)
        {
            var result = new OperationResult();

            try
            {
                _context.Mapping.Add(Mapping);
                _context.SaveChanges();
                result.State = OperationState.Success;
                result.Message = "增加完成";
            }
            catch
            {
                result.State = OperationState.Faild;
                result.Message = "增加失败";
            }

            return result;
        }


        /// <summary>
        /// 修改权限
        /// </summary>
        public OperationResult ModifyMapping(Mapping Mapping)
        {
            var result = new OperationResult();
            var has = GetMappingById(Mapping.Id);
            if (has != null)
            {
                try
                {
                    var mp = _context.Mapping.Find(Mapping.Id);
                    mp.Mark = Mapping.Mark;
                    mp.Role = Mapping.Role;
                    _context.SaveChanges();
                    result.State = OperationState.Success;
                    result.Message = "修改完成";
                }
                catch
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
        /// 根据ID删除权限
        /// </summary>
        public OperationResult DelMapping(string id)
        {
            var result = new OperationResult();
            try
            {
                _context.Mapping.Remove(_context.Mapping.Find(id));
                _context.SaveChanges();
                result.State = OperationState.Success;
                result.Message = "删除完成";
            }

            catch
            {
                result.State = OperationState.Faild;
                result.Message = "删除失败";
            }
            return result;
        }



        #region  查询权限映射表信息
        /// <summary>
        /// 根据权限标识获取对应系统用户
        /// </summary>
        /// <param name="role">权限标识</param>
        /// <returns></returns>
        public List<string> GetUserByMark(string role)
        {
            //根据权限标识获取对应角色
            var mappings = _context.Mapping.Where(e => e.Role == role).FirstOrDefault();
            if (mappings != null)
            {
                var userList = _context.UserRolesView.Where(x => x.Role == mappings.Role);
                var users = userList.Select(e => e.UserName).ToList();
                return users;

            }
            else
                return null;

        }
        /// <summary>
        /// 根据Userid获取对应权限
        /// </summary>
        /// <param name="Userid">权限标识</param>
        /// <returns></returns>
        public System.Collections.Hashtable GetUserByUserid(string Userid)
        {
            //动态生成对象
            System.Collections.Hashtable ht = new System.Collections.Hashtable();
            var userinfo = _context.UserRolesView.Where(e => e.Id == Userid).FirstOrDefault();
            //根据权限标识获取对应角色
            var mappings = _context.Mapping.Where(e => e.Role == userinfo.Role).FirstOrDefault();
            if (mappings != null)
            {
                var mark = mappings.Mark;
                JArray json1 = (JArray)JsonConvert.DeserializeObject(mark);
                var aaaa = json1.ToList();
                var rusle = _context.Permission.Where(e => aaaa.Contains(e.Mark)).ToList();
                foreach (var item in rusle)
                {
                    ht.Add(item.Mark, true);
                }
                return ht;

            }
            else
                return null;

        }
        /// <summary>
        /// 根据权限标识获取对应权限
        /// </summary>
        /// <param name="Role">权限标识</param>
        /// <returns></returns>
        public List<Permission> GetUserByRole(string Role)
        {
            var mappings = _context.Mapping.Where(e => e.Role == Role).FirstOrDefault();
            if (mappings != null)
            {
                var mark = mappings.Mark;
                JArray json1 = (JArray)JsonConvert.DeserializeObject(mark);
                var aaaa= json1.ToList();
                return _context.Permission.Where(e => aaaa.Contains(e.Mark)).ToList();

            }
            else
                return null;

        }
        #endregion
    }
}
