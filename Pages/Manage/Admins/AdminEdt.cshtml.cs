using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SunuerManage.Pages.Manage.Admins
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:用户编辑
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class AdminEdtModel : PageModel
    { 
        [BindProperty(SupportsGet = true)] // 允许通过 GET 请求绑定
        public int AdminID { get; set; } = 0;
        public Admin.AdminModel Model = new Admin.AdminModel();
        //传入http注入用于获取IP
        private readonly IHttpContextAccessor _httpContextAccessor;
        //注入获取appsettings.json字符串
        private readonly IConfiguration _configuration;
        public AdminEdtModel(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        public Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
        public void OnGet()
        {
            //判断登陆状态
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);
            //判断是否有权限
            if (AdminModel.Roles.IndexOf("|11|")  < 0)
            {
                // 重定向到登录页面
                Response.Redirect("/Manage/Login");
            }
            if (AdminID == 0)
            {
            // 重定向到登录页面
            Response.Redirect("/Manage/Login");
            }
            else
            {
                GetModel();
            }

            ViewData["Title"] = "Sunuer Managae";
            ViewData["KeyWords"] = "Sunuer Managae";
            ViewData["Description"] = "Sunuer Managae";
        }

        public void GetModel()
        {
            Admin.AdminDal Dal = new Admin.AdminDal();
            Model = Dal.GetModel(AdminID);
            if (Model == null)
            {
                // 重定向到登录页面
                Response.Redirect("/Manage/Login");
            }
        }
    }
}
