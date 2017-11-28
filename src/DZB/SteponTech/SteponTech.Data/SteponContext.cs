using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SteponTech.Data.BaseModel;
using SteponTech.Data.BaseModels;
using SteponTech.Data.CommonModel;
using SteponTech.Data.CommonModels;

namespace SteponTech.Data
{
    public class SteponContext : IdentityDbContext<ApplicationUser>
    {
        public SteponContext(DbContextOptions<SteponContext> options) : base(options)
        {
        }

        /// <summary>
        /// 人员
        /// </summary>
        public DbSet<UserRolesView> UserRolesView { get; set; }

        /// <summary>
        /// Banner
        /// </summary>
        public DbSet<Banner> Banner { get; set; }

        /// <summary>
        /// BannerView
        /// </summary>
        public DbSet<BannerView> BannerView { get; set; }

        /// <summary>
        /// 权限映射
        /// </summary>
        public DbSet<Mapping> Mapping { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public DbSet<Permission> Permission { get; set; }

        /// <summary>
        /// 资讯
        /// </summary>
        public DbSet<Information> Information { get; set; }

        /// <summary>
        /// 板块
        /// </summary>
        public DbSet<Models> Models { get; set; }

        /// <summary>
        /// 栏目
        /// </summary>
        public DbSet<Colunms> Colunms { get; set; }

        /// <summary>
        /// 栏目试图
        /// </summary>
        public DbSet<ColunmsView> ColunmsView { get; set; }

        /// <summary>
        /// 资源
        /// </summary>
        public DbSet<Resource> Resource { get; set; }

        /// <summary>
        /// 网站配置
        /// </summary>
        public DbSet<WebConfig> WebConfig { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public DbSet<Link> Link { get; set; }

        /// <summary>
        /// 系统日志
        /// </summary>
        public DbSet<SystemLog> SystemLog { get; set; }

        /// <summary>
        /// 登陆日志
        /// </summary>
        public DbSet<LoginLog> LoginLog { get; set; }

        /// <summary>
        /// 请求统计
        /// </summary>
        public DbSet<RequestStatistical> RequestStatistical { get; set; }


        /// <summary>
        /// 资讯信息
        /// </summary>
        public DbSet<InformationAll> InformationAll { get; set; }

        /// <summary>
        ///     需要在这里设置数据库相关的映射配置
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}