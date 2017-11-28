using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace SteponTech.Areas.Admin.Controllers
{
    //现在区域必须加上这个才能进行路由
    [Area("Admin")]
    public class MasterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
