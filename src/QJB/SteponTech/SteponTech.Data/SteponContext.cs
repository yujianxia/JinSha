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
        /// 权限映射
        /// </summary>
        public DbSet<Mapping> Mapping { get; set; }

        /// <summary>
        /// 权限映射
        /// </summary>
        public DbSet<WebConfig> WebConfigYoung { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public DbSet<Permission> Permission { get; set; }

        /// <summary>
        /// 资讯
        /// </summary>
        public DbSet<InformationYoung> InformationYoung { get; set; }

        /// <summary>
        /// 板块
        /// </summary>
        public DbSet<ModelsYoung> ModelsYoung { get; set; }

        /// <summary>
        /// 栏目
        /// </summary>
        public DbSet<ColunmsYoung> ColunmsYoung { get; set; }

        /// <summary>
        /// 栏目试图
        /// </summary>
        public DbSet<ColunmsYoungView> ColunmsYoungView { get; set; }

        /// <summary>
        /// 系统日志
        /// </summary>
        public DbSet<SystemLog> SystemLog { get; set; }

        /// <summary>
        /// 登陆日志
        /// </summary>
        public DbSet<LoginLog> LoginLog { get; set; }


        /// <summary>
        /// 资讯信息
        /// </summary>
        public DbSet<InformationYoungView> InformationYoungView { get; set; }

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