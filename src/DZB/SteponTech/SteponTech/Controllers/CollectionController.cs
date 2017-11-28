using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stepon.Mvc.Controllers;
using SteponTech.Data;
using SteponTech.Data.BaseModels;
using SteponTech.Utils;
using SteponTech.Utils.DHSH;
using SteponTech.Utils.DHSH.Model;
using SteponTech.Utils.DHSH.Model.Config;
using SteponTech.Utils.DHSH.Model.Message;
using SteponTech.Utils.DHSH.Model.Part.Return.SmallReturnPart;
using SteponTech.Utils.DHSH.Utils;
using SteponTech.Utils.ImageControl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SteponTech.Controllers
{
    /// <summary>
    /// 典藏藏品
    /// </summary>
    public class CollectionController : BaseController<CollectionController, SteponContext>
    {
        public IConfigurationRoot Configuration { get; }
        private IHostingEnvironment Environment { get; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="env"></param>
        public CollectionController(IHostingEnvironment env)
        {
            Environment = env;
        }

        /// <summary>
        /// 典藏珍品首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();


            //Banner
            ViewBag.Banner = Context.BannerView.FirstOrDefault(x => x.ModelName == "典藏珍品");
            //典藏珍品
            var cols = Context.ColunmsView.Where(x => x.Name == "典藏珍品" || x.Name == "金器" || x.Name == "青铜器" || x.Name == "玉器"
                     || x.Name == "石器" || x.Name == "陶器" || x.Name == "漆木器" || x.Name == "开馆时间" || x.Name == "交通信息" ||
                     x.Name == "服务信息" || x.Name == "参观须知").ToList();



            //页脚跳转信息
            ViewBag.JumpLink = cols.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知").ToList();

            ViewBag.Colect = cols.Find(e => e.Name == "典藏珍品");
            ViewBag.GoldCol = cols.Find(e => e.Name == "金器");
            ViewBag.BronzeCol = cols.Find(e => e.Name == "青铜器");
            ViewBag.JadeCol = cols.Find(e => e.Name == "玉器");
            ViewBag.StoneCol = cols.Find(e => e.Name == "石器");
            ViewBag.PotteryCol = cols.Find(e => e.Name == "陶器");
            ViewBag.LacquerCol = cols.Find(e => e.Name == "漆木器");



            var res = WenwuList("1", "1");
            //解析数据列表 
            var wenwulist = new List<WenWu>();
            foreach (var data in res)
            {
                wenwulist.Add(new WenWu()
                {
                    SinNo=data.column[0].value,
                    Name=data.column[1].value,
                    Type = data.column[2].value,
                    Year = data.column[3].value,
                    Character = data.column[4].value,
                    Complete = data.column[5].value,
                    Size = data.column[6].value,
                    Weight = data.column[7].value,
                    Source = data.column[8].value,
                    Level = data.column[9].value,
                    Pic = data.column[10].value,
                    TDPic = data.column[11].value,
                    TDObj = data.column[12].value,
                });
            }

            //藏品视频
            var video = Context.InformationAll.Where(x => x.Address != "" && x.Address != null && (x.ColumName == "金器" || x.ColumName == "青铜器" || x.ColumName == "玉器"
                       || x.ColumName == "石器" || x.ColumName == "陶器" || x.ColumName == "漆木器")).ToList();


            foreach(var wenwu in wenwulist)
            {
                foreach(var v in video)
                {
                    if (v.Name == wenwu.Name)
                    {
                        wenwu.Video="/upload/Information/" + v.Id + "/" + v.FileName;
                    }
                }
            }



            //金器
            ViewBag.Gold = wenwulist.Where(e => e.Type == "金银器").ToList();
            //青铜器
            ViewBag.Bronze = wenwulist.Where(e => e.Type == "铜器").ToList();
            //玉器
            ViewBag.Jade = wenwulist.Where(e => e.Type == "玉器").ToList();
            //石器
            ViewBag.Stone = wenwulist.Where(e => e.Type == "石器、石刻、砖瓦").ToList();
            //陶器
            ViewBag.Pottery = wenwulist.Where(e => e.Type == "陶器").ToList();
            //漆木器
            ViewBag.Lacquer = wenwulist.Where(e => e.Type == "竹木器").ToList();


         
         




            //获取壁纸Information
            ViewBag.Wallpapers = Context.InformationAll.Where(x => x.ColumName == "精美桌面").OrderByDescending(e => e.CreationDate).Take(9).ToList();

            //获取新增栏目
            var newcol = Context.ColunmsView.Where(x => x.ModelName == "典藏珍品" && x.IsNew == true).ToList();
            if (newcol.Count > 0)
            {
                var newinfo = new List<InformationAll>();
                foreach (var col in newcol)
                {
                    var info = Context.InformationAll.FirstOrDefault(x => x.ColumName == col.Name);
                    if (info != null)
                    {
                        newinfo.Add(info);
                    }

                }
                if (newinfo.Count > 0)
                {
                    ViewBag.NewCol = newinfo;
                }

            }


            return View();
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <returns></returns>
        public IActionResult Intro(Guid id)
        {
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");

            var information = Context.InformationAll.Find(id);
            RelatedLink rl = new RelatedLink(Services);
            if (!String.IsNullOrEmpty(information.InformationId))
            {
                ViewBag.Aside = rl.GetLink(information.InformationId);
            }

            List<string> filelist = new List<string>();
            var filepath = Environment.WebRootPath;  //文件存储路径
            filepath = Path.Combine(filepath, "upload\\Information\\" + information.Id);
            if (Directory.Exists(filepath))
            {
                DirectoryInfo theFolder = new DirectoryInfo(filepath);
                FileInfo[] Files = theFolder.GetFiles();
                foreach (FileInfo NextFolder in Files)
                {
                    string ex = NextFolder.Extension.ToLower();
                    if (ex == ".jpg" || ex == ".png" || ex == ".bmp" || ex == ".gif" || ex == ".jpeg")
                    {
                        if (NextFolder.Name != information.Photo)
                        {
                            string iil = "/upload/Information/" + information.Id + "/" + NextFolder.Name;
                            filelist.Add(iil);
                        }
                    }
                }
            }
            ViewBag.ImgList = filelist;

            return View(information);
        }

        /// <summary>
        /// 详情列表
        /// </summary>
        /// <returns></returns>
        public IActionResult IntroList(Guid id)
        {
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");

            ViewBag.Id = id;
            return View();
        }

        /// <summary>
        /// 更多壁纸
        /// </summary>
        /// <returns></returns>
        public IActionResult Background()
        {
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");

            ViewBag.Wallpapers = Context.InformationAll.Where(x => x.ColumName == "精美桌面").Take(9).ToList();
            return View();
        }
        /// <summary>
        /// 壁纸详情
        /// </summary>
        /// <returns></returns>
        public IActionResult Wallpaper(Guid id)
        {
            //是否登录总线
            ViewBag.IsLogin = HttpContext.Session.GetString("loginname");
            //母版页头数据
            ViewBag.Head = Context.Models.Where(x => x.IsShow == true).OrderBy(x => x.CreationDate);
            //母版页footer数据
            ViewBag.Footer = Context.WebConfig.FirstOrDefault();
            //页脚跳转信息
            ViewBag.JumpLink = Context.ColunmsView.Where(x => x.Name == "开馆时间" || x.Name == "交通信息" || x.Name == "服务信息" || x.Name == "参观须知");

            ViewBag.Wallpapers = Context.InformationAll.FirstOrDefault(x => x.Id == id);
            return View();
        }

        /// <summary>
        /// 壁纸下载
        /// </summary>
        /// <returns></returns>
        public IActionResult WallpaperDownload(int width, int height, Guid id)
        {
            var paper = Context.InformationAll.FirstOrDefault(x => x.Id == id);
            var pathstring = Path.Combine(Environment.WebRootPath, "upload", "information", id.ToString());
            var img = new ImageControl();
            var byt = img.resizeImage(paper.Photo, pathstring, width, height);
            FileContentResult fs = File(byt, "application/octet-stream", "wallpaper.jpg");
            return fs;
        }

        /// <summary>
        /// 馆藏文物类
        /// </summary>
        public class WenWu
        {
            ///藏品总登记号
            public string SinNo { get; set; }
            ///名称
            public string Name { get; set; }
            ///类别
            public string Type { get; set; }
            ///年代
            public string Year { get; set; }
            ///形态特征
            public string Character { get; set; }
            ///完残程度
            public string Complete { get; set; }
            ///尺寸
            public string Size { get; set; }
            ///质量
            public  string Weight { get; set; }
            ///来源
            public string Source { get; set; }
            ///藏品级别
            public string Level { get; set; }
            ///略缩图地址
            public string Pic { get; set; }
            ///3d图地址
            public string TDPic { get; set; }
            ///3Dobj文件地址
            public string TDObj { get; set; }
            ///视频文件
            public string Video { get; set; }

        }

        /// <summary>
        /// 获取精品馆藏文物
        /// </summary>
        /// <param name="pageIndex">起始页 最低为1</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        public List<RDataRow> WenwuList(string pageIndex, string pageSize)
        {
            try
            {
                //数据获取对象
                var ob = new Obtainer();
                Error error;
                ReturnToken token;
                //获取token
                var flag = ob.GetToken(out token, out error);
                //if (!flag) return Json(new { code = 2, message = "获取TOKEN失败！" });//获取token失败
                if (!flag) return null;//获取token失败

                //创建并初始化获取数据的配置对象
                var config = new DataConfig(ConfigHelper.GetValue("WenWuList"));
                //设置token信息
                config.postData.setToken(token);
                //设置参数信息
                config.postData.setParameters(new List<Utils.DHSH.Model.Part.Post.SmallPostPart.Parameter> {
                });
                ReturnData data;
                //获取数据
                flag = ob.GetData(config, out data, out error);
                if (!flag)
                {
                    //return Json(new { code = 3, message = "操作失败", data = error.message });//获取数据失败
                    return null;//获取数据失败
                }
                else
                {

                    //return Json(new { code = 1, message = "查询数据成功！", data = data.body.dataTable.dataRow });//获取数据失败 
                    return data.body.dataTable.dataRow;//获取数据失败 

                }


            }
            catch (Exception e)
            {
                //return Json(new { code = 6, message = "数据获取失败！", data = e.Message });//获取数据失败
                return null;//获取数据失败
            }
        }

        /// <summary>
        /// 文物主体数据
        /// </summary>
        /// <param name="name">文物名称</param>
        /// <param name="sss">sss描述</param>
        /// <returns></returns>
        [HttpGet("[action]/{name}/{sss}")]
        public JsonResult SubjectCultural(string name, string sss)
        {
            try
            {
                //数据获取对象
                var ob = new Obtainer();
                Error error;
                ReturnToken token;
                //获取token
                var flag = ob.GetToken(out token, out error);
                if (!flag) return Json(new { code = 2, message = "获取TOKEN失败！" });//获取token失败

                //创建并初始化获取数据的配置对象
                var config = new DataConfig(ConfigHelper.GetValue("WenWuList"));
                //设置token信息
                config.postData.setToken(token);
                //设置参数信息
                config.postData.setParameters(new List<Utils.DHSH.Model.Part.Post.SmallPostPart.Parameter> {
                    new Utils.DHSH.Model.Part.Post.SmallPostPart.Parameter{ name="name",value=name},
                    new Utils.DHSH.Model.Part.Post.SmallPostPart.Parameter{ name="sss",value=sss}
                });
                ReturnData data;
                //获取数据
                flag = ob.GetData(config, out data, out error);
                if (!flag)
                {
                    return Json(new { code = 3, message = "操作失败", data = error.message });//获取数据失败
                }
                else
                {
                    var meassgeResult = data.body.dataTable.dataListMap.list[0];
                    if (meassgeResult.column[0].value == "FAIL")
                    {
                        return Json(new { code = 4, message = meassgeResult.column[1].value });//获取数据失败 
                    }
                    else
                    {
                        return Json(new { code = 1, message = "查询数据成功！", data = Newtonsoft.Json.JsonConvert.SerializeObject(data) });//获取数据失败 
                    }
                }


            }
            catch (Exception e)
            {
                return Json(new { code = 6, message = "数据获取失败！", data = e.Message });//获取数据失败
            }
        }


























    }
}