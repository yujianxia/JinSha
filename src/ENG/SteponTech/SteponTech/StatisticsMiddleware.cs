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
using SteponTech.Data.TrunkSystem;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SteponTech
{
    public class StatisticsMiddleware : ServiceContract<SteponContext>
    {
        private readonly RequestDelegate next;
        private readonly SteponContext _context;
        public StatisticsMiddleware(IServiceProvider services, RequestDelegate next) : base(services)
        {
            _context = services.GetService<SteponContext>();
            this.next = next;
        }
        public async Task Invoke(HttpContext Context)
        {
            try
            {
                //BackgroundJob.Enqueue(() => AysncPublic(Context));
                await next(Context);
            }
            catch(Exception e)
            {
                await next(Context);
            }

        }
        public void AysncPublic(HttpContext Context)
        {
            if (!Context.Request.Path.ToString().Contains("/api"))
            {
                RequestStatistical rs = new RequestStatistical();
                rs.Id = Guid.NewGuid();

                var ip = Context.Connection.RemoteIpAddress.ToString();
                if (ip == "::1")
                {
                    ip = "127.0.0.1";
                }

                rs.RequestIp = ip;
                rs.RequestUrl = Context.Request.Host + Context.Request.Path;

                System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
                System.Net.Http.HttpResponseMessage response = httpClient.GetAsync("http://ip.taobao.com/service/getIpInfo.php?ip=" + ip).Result;
                String resultweqweqwe = response.Content.ReadAsStringAsync().Result;
                var converResult = (RequestIpContent)Newtonsoft.Json.JsonConvert.DeserializeObject<RequestIpContent>(resultweqweqwe);
                if (converResult.data["city"].ToString() == "")
                {
                    rs.RequestCountry = converResult.data["country"].ToString();
                }
                else
                {
                    rs.RequestCountry = converResult.data["city"].ToString();
                }

                //await Task.Factory.StartNew(() =>
                //{
                //    _context.Create(rs, true, true);
                //});
                _context.Create(rs, true, true);
            }
        }
    }
}
