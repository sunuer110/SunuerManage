using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace SunuerManage.Pages.Manage.Admins
{
    /// <summary>
    /// Project:Sunuer Manage
    /// Description:权限编辑
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class AdminPowerEdtModel : PageModel
    {
        [BindProperty(SupportsGet = true)] // 允许通过 GET 请求绑定
        public int PowerID { get; set; } = 0;
        public  AdminPower.AdminPowerModel Model = new AdminPower.AdminPowerModel();
        //传入http注入用于获取IP
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AdminPowerEdtModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
        public void OnGet()
        {

            //判断登陆状态
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);
            //判断是否有权限
            if (AdminModel.Roles.IndexOf("|3|") < 0)
            {
                // 重定向到登录页面
                Response.Redirect("/Manage/Login");
            }
            if (PowerID == 0)
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
            AdminPower.AdminPowerDal Dal = new AdminPower.AdminPowerDal();
            Model= Dal.GetModel(PowerID);
            if(Model==null)
            {
                // 重定向到登录页面
                Response.Redirect("/Manage/Login");
            }
        }
    }
}
