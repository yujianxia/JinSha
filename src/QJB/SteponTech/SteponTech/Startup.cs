using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using SteponTech.Data;
using Swashbuckle.Swagger.Model;
using System;
using System.IO;
using System.Threading.Tasks;
using UEditorNetCore;

namespace SteponTech
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Environment = env;
        }

        public IConfigurationRoot Configuration { get; }

        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureBasic(services);
            ConfigureIdentity(services);
            ConfigureExtra(services);
        }

        /// <summary>
        /// 配置基础服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureBasic(IServiceCollection services)
        {
            //添加配置文件注入
            services.AddSingleton<IConfiguration>(Configuration);

            //添加session支持
            //services.AddDistributedRedisCache(options =>
            //{
            //    options.InstanceName = "cache";
            //    options.Configuration = "localhost";
            //}); //Redis分布式缓存，一般用于大型网站
            services.AddDistributedMemoryCache(); //内存缓存一般用于小型网站或开发
            services.AddSession();

            //添加EntityFramework支持
            services.AddDbContext<SteponContext>(
                options => { options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")); });

            //添加基础支撑服务
            services.AddSteponBasicService(Configuration.GetSection("WebSettings"));

            //添加MVC支持
            services.AddMvc();

            if (Environment.IsDevelopment())
            {
                //添加应用程序监控服务，主要用于开发
                services.AddApplicationInsightsTelemetry(Configuration);

                //添加WebApi自动生成接口文档，目前配置进在开发模式下启用
                services.AddSwaggerGen(options =>
                {
                    var docPath = Path.Combine(Environment.ContentRootPath, "bin", "Debug", "netcoreapp1.1", "SteponTech.xml");
                    var docDataPath = Path.Combine(Environment.ContentRootPath, "bin", "Debug", "netcoreapp1.1", "SteponTech.Data.xml");

                    options.SingleApiVersion(new Info
                    {
                        Version = "v1",
                        Title = "Stepon Web Api",
                        Description = "Stepon web api for all common use",
                        Contact = new Contact
                        {
                            Name = "Stepon",
                            Email = "it@stepon.tech",
                            Url = "we.stepon.tech"
                        }
                    });

                    options.IncludeXmlComments(docPath);
                    options.IncludeXmlComments(docDataPath);
                });
            }
        }

        /// <summary>
        /// 配置身份认证服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureIdentity(IServiceCollection services)
        {
            //添加身份认证服务，根据实际情况进行调整
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                //要求密码至少有大写
                options.Password.RequireUppercase = false;
                //要求密码至少有非字符数字符号
                options.Password.RequireNonAlphanumeric = false;
                //要求密码至少6位
                options.Password.RequiredLength = 6;
                //配置为false
                options.Lockout.AllowedForNewUsers = false;
                //设置登录地址
                options.Cookies.ApplicationCookie.LoginPath = "/Master/Login";
                //设置登出地址
                options.Cookies.ApplicationCookie.LogoutPath = "/Master/Logout";
                //设置Cookie过期时间，12小时
                options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromHours(12);
                //设置Cookie名字
                options.Cookies.ApplicationCookie.CookieName = "stepon_cookie";
                options.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents
                {
                    OnSigningIn = ApplicationUser.OnSigningIn
                };
            })
                .AddEntityFrameworkStores<SteponContext>()
                .AddDefaultTokenProviders();
        }

        /// <summary>
        /// 这里配置额外的服务，比如消息发送服务，Web安全服务等
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureExtra(IServiceCollection services)
        {
            //读取邮件配置
            services.AddSteponEmailSender(Configuration.GetSection("Email"));
            //UEidetor
            services.AddUEditorService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                //开发环境下，日志文件输出到控制台（在VS中的输出中查看）
                loggerFactory.AddConsole(Configuration.GetSection("Logging"));

                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
               // app.UseBrowserLink();
                //开发环境下，使用默认内置的状态页面
                app.UseStatusCodePages();
            }
            else
            {
                //非开发环境下，日志采用NLog输出到指定文件中
                loggerFactory.AddNLog();
                app.UseExceptionHandler("/Error/Unhandled");
                app.UseStatusCodePagesWithRedirects("~/Error/Status/{0}");
            }

            //如果需要重定向或重写功能，可以在此处配置，另外，也可以在Nginx上进行配置
            //var rewrite = new RewriteOptions();
            //rewrite.AddRewrite("^personal$|^home$","Home/Index", true);
            //app.UseRewriter(rewrite);

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseSession();

            app.Map("/api/UEditor/Do", HandlerBranch);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "areaRoute",
                    "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });

            if (env.IsDevelopment())
            {
                app.UseSwagger().UseSwaggerUi();
            }
        }
        private static void HandlerBranch(IApplicationBuilder app)
        {
            app.Run(h =>
            {
                h.RequestServices.GetService<UEditorService>().DoAction(h);
                return Task.CompletedTask;
            });
        }
    }
}
