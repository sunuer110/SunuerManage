using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SunuerManage.Pages.Manage.Articless
{
    /// <summary>
    /// Project:Sunuer Manage
    /// Description:文章列表
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class ArticlesListModel : PageModel
    {
        [BindProperty(SupportsGet = true)] // 允许通过 GET 请求绑定
        public int BigID { get; set; } = -1;
        //传入http注入用于获取IP
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ArticlesListModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
        public void OnGet()
        {
            //判断登陆状态
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);
            //判断是否有权限
            if (AdminModel.Roles.IndexOf("|17|") < 0)
            {
                // 重定向到登录页面
                Response.Redirect("/Manage/Login");
            }

            ViewData["Title"] = "Sunuer Managae";
            ViewData["KeyWords"] = "Sunuer Managae";
            ViewData["Description"] = "Sunuer Managae";
        }
    }
}
