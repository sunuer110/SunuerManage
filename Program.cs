using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using SunuerManage.Data;
using Tools;

namespace SunuerManage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // 注册 ApplicationDbContext 并配置使用 SQL Server
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            // Add services to the container.
            builder.Services.AddRazorPages();

            // 获取当前应用的根目录
            var environment = builder.Environment;
            var appRootPath = environment.ContentRootPath; // 获取应用根目录
            var keysPath = Path.Combine(appRootPath, "keys");
            if (!Directory.Exists(keysPath))
            {
                Directory.CreateDirectory(keysPath);  // 创建文件夹（如果不存在）
            }
            // 将密钥存储路径设置为应用根目录下的 "keys" 文件夹
            builder.Services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(appRootPath, "keys")))  // 动态配置密钥存储路径
                .SetApplicationName("SunuerApp");  // 可选：设置应用名称来隔离密钥
            // 设置 IConfiguration 到 ConfigurationHelper
            ConfigurationHelper.SetConfiguration(builder.Configuration);

            // 添加服务，包括 IHttpContextAccessor
            builder.Services.AddHttpContextAccessor();
            //此全局配置将确保所有的 JSON 序列化行为都保持 C# 属性名称的原始大小写。
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            // 注册 API 控制器
            builder.Services.AddControllers();  // 添加对 API 控制器的支持
            builder.Services.AddDistributedMemoryCache(); // 添加分布式内存缓存服务
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(5); // 设置 Session 的过期时间
                options.Cookie.HttpOnly = true; // 仅允许通过 HTTP 访问 Cookie，防止客户端脚本访问
                options.Cookie.IsEssential = true; // 确保 Cookie 在隐私策略下也是必须的
            });
            //注册HttpContext
            builder.Services.AddHttpContextAccessor();
            var app = builder.Build();

            // 初始化 HttpContextHelper
            var httpContextAccessor = app.Services.GetRequiredService<IHttpContextAccessor>();
            HttpContextHelper.Configure(httpContextAccessor);
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            // 映射 API 控制器
            app.MapControllers();  // 映射 API 控制器路由

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
