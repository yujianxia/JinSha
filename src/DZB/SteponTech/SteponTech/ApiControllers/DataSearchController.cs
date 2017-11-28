using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stepon.Data;
using Stepon.Data.PgSql;
using Stepon.Mvc.Controllers;
using SteponTech.Data;

namespace SteponTech.ApiControllers
{
    /// <summary>
    /// 通用数据搜索API
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DataSearchController : BaseController<DataSearchController, SteponContext>
    {
        /// <summary>
        /// 搜索数据
        /// </summary>
        /// <param name="parameter">搜索参数</param>
        /// <returns></returns>
        [HttpPost]
        public SearchResult Search([FromBody] SearchParameter parameter)
        {
            return PgSqlDataSearchCore.Search(parameter, Context.Database.GetDbConnection());
        }
    }
}