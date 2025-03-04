using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SunuerManage.Pages.Manage.Articless
{
    /// <summary>
    /// Project:Sunuer Manage
    /// Description:分类编辑
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class ArticleCategoryEdtModel : PageModel
    {
        [BindProperty(SupportsGet = true)] // 允许通过 GET 请求绑定
        public int BigID { get; set; } = 0;
        public ArticleCategory.ArticleCategoryModel Model = new ArticleCategory.ArticleCategoryModel();
        //传入http注入用于获取IP
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ArticleCategoryEdtModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
        public void OnGet()
        {
            //判断登陆状态
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);
            //判断是否有权限
            if (AdminModel.Roles.IndexOf("|19|") < 0)
            {
                // 重定向到登录页面
                Response.Redirect("/Manage/Login");
            }
            if (BigID == 0)
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
            ArticleCategory.ArticleCategoryDal Dal = new ArticleCategory.ArticleCategoryDal();
            Model = Dal.GetModel(BigID);
            if (Model == null)
            {
                // 重定向到登录页面
                Response.Redirect("/Manage/Login");
            }
        }
    }
}
