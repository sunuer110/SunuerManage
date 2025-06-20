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
            // ע�� ApplicationDbContext ������ʹ�� SQL Server
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            // Add services to the container.
            builder.Services.AddRazorPages();

            // ��ȡ��ǰӦ�õĸ�Ŀ¼
            var environment = builder.Environment;
            var appRootPath = environment.ContentRootPath; // ��ȡӦ�ø�Ŀ¼
            var keysPath = Path.Combine(appRootPath, "keys");
            if (!Directory.Exists(keysPath))
            {
                Directory.CreateDirectory(keysPath);  // �����ļ��У���������ڣ�
            }
            // ����Կ�洢·������ΪӦ�ø�Ŀ¼�µ� "keys" �ļ���
            builder.Services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(appRootPath, "keys")))  // ��̬������Կ�洢·��
                .SetApplicationName("SunuerApp");  // ��ѡ������Ӧ��������������Կ
            // ���� IConfiguration �� ConfigurationHelper
            ConfigurationHelper.SetConfiguration(builder.Configuration);

            // ��ӷ��񣬰��� IHttpContextAccessor
            builder.Services.AddHttpContextAccessor();
            //��ȫ�����ý�ȷ�����е� JSON ���л���Ϊ������ C# �������Ƶ�ԭʼ��Сд��
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            // ע�� API ������
            builder.Services.AddControllers();  // ��Ӷ� API ��������֧��
            builder.Services.AddDistributedMemoryCache(); // ��ӷֲ�ʽ�ڴ滺�����
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(5); // ���� Session �Ĺ���ʱ��
                options.Cookie.HttpOnly = true; // ������ͨ�� HTTP ���� Cookie����ֹ�ͻ��˽ű�����
                options.Cookie.IsEssential = true; // ȷ�� Cookie ����˽������Ҳ�Ǳ����
            });
            //ע��HttpContext
            builder.Services.AddHttpContextAccessor();
            var app = builder.Build();

            // ��ʼ�� HttpContextHelper
            var httpContextAccessor = app.Services.GetRequiredService<IHttpContextAccessor>();
            HttpContextHelper.Configure(httpContextAccessor);
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            // ӳ�� API ������
            app.MapControllers();  // ӳ�� API ������·��

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
