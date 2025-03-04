using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SunuerManage.Pages.Manage.Articless
{
    /// <summary>
    /// Project:Sunuer Manage
    /// Description:分类添加
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class ArticleCategoryAddModel : PageModel
    {

        [BindProperty(SupportsGet = true)] // 允许通过 GET 请求绑定
        public int ParentID { get; set; } = 0;

        //传入http注入用于获取IP
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ArticleCategoryAddModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
        public void OnGet()
        {
            //判断登陆状态
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);
            //判断是否有权限
            if (AdminModel.Roles.IndexOf("|18|") < 0)
            {
                // 重定向到登录页面
                Response.Redirect("/Manage/Login");
            }
            if (ParentID < 0)
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
