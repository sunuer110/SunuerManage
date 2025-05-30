using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SunuerManage.Pages.Manage.Articless
{
    /// <summary>
    /// Project:Sunuer Manage
    /// Description:文章编辑
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class ArticlesEdtModel : PageModel
    {
        [BindProperty(SupportsGet = true)] // 允许通过 GET 请求绑定
        public int ArticleID { get; set; } = 0;
        public Articles.ArticlesModel Model = new Articles.ArticlesModel();
        //传入http注入用于获取IP
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ArticlesEdtModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
        public void OnGet()
        {

            //判断登陆状态
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);

            if (AdminModel.Roles.IndexOf("|22|") < 0)
            {
                // 重定向到登录页面
                Response.Redirect("/Manage/Login");
            }
            if (ArticleID == 0)
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
            Articles.ArticlesDal Dal = new Articles.ArticlesDal();
            Model = Dal.GetModel(ArticleID);
            if (Model == null)
            {
                // 重定向到登录页面
                Response.Redirect("/Manage/Login");
            }
        }
    }
}
