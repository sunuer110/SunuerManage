using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SunuerManage.Pages.Manage.ManageSets
{
    /// <summary>
    /// Project:Sunuer Manage
    /// Description:系统设置
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class ManageSetEdtModel : PageModel
    {

        public ManageSet.ManageSetModel Model = new ManageSet.ManageSetModel();
        //传入http注入用于获取IP
        private readonly IHttpContextAccessor _httpContextAccessor;
        //注入获取appsettings.json字符串
        private readonly IConfiguration _configuration;
        public ManageSetEdtModel(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        public Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
        public void OnGet()
        {

            //判断登陆状态
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);

            if (AdminModel.Roles.IndexOf("|24|") < 0)
            {
                // 重定向到登录页面
                Response.Redirect("/Manage/Login");
            }
           
                GetModel();

            ViewData["Title"] = "Sunuer Managae";
            ViewData["KeyWords"] = "Sunuer Managae";
            ViewData["Description"] = "Sunuer Managae";
        }

        public void GetModel()
        {
            Model.ManageTitle= _configuration["ManageSet:ManageTitle"]!;
            Model.ManageKey = _configuration["ManageSet:ManageKey"]!;
            Model.ManageDesn = _configuration["ManageSet:ManageDesn"]!;
            Model.ManageUrl = _configuration["ManageSet:ManageUrl"]!;
            Model.Phone = _configuration["ManageSet:Phone"]!;
            Model.Email = _configuration["ManageSet:Email"]!;
            Model.Address = _configuration["ManageSet:Address"]!;
            Model.CopyRight = _configuration["ManageSet:CopyRight"]!;
            Model.ImageUrl = _configuration["ManageSet:ImageUrl"]!;
            Model.About = _configuration["ManageSet:About"]!;
            Model.Logo = _configuration["ManageSet:Logo"]!;
        }
        
    }
}
