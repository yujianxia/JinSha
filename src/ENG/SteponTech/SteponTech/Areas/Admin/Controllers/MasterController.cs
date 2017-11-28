using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace SteponTech.Areas.Admin.Controllers
{
    /// <summary>
    /// 主页
    /// </summary>
    [Area("Admin")]
    public class MasterController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
      
    }
}
