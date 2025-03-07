using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Manage.LeaveMessages
{
    /// <summary>
    /// Project:Sunuer Manage
    /// Description:记账审核
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class LeaveMessageAuditModel : PageModel
    {
        public LeaveMessageSet.LeaveMessageSetModel ModelSet = new LeaveMessageSet.LeaveMessageSetModel();
        public LeaveMessage.LeaveMessageModel Model = new LeaveMessage.LeaveMessageModel();
        [BindProperty(SupportsGet = true)] // 允许通过 GET 请求绑定
        public string LeaveMessageIDs { get; set; } = "";
        //传入http注入用于获取IP
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LeaveMessageAuditModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
        public void OnGet()
        {
            //判断登陆状态
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);
            //判断是否有权限
            if (AdminModel.Roles.IndexOf("|403|") < 0)
            {
                // 重定向到登录页面
                Response.Redirect("/Manage/Login");
            }
            if (string.IsNullOrEmpty(LeaveMessageIDs)) {
                // 重定向到登录页面
                Response.Redirect("/Manage/Login");
            }
            GetModel();
            GetModelSet();
            ViewData["Title"] = "Sunuer Managae";
            ViewData["KeyWords"] = "Sunuer Managae";
            ViewData["Description"] = "Sunuer Managae";
        }
        public void GetModelSet()
        {
            LeaveMessageSet.LeaveMessageSetDal Dal = new LeaveMessageSet.LeaveMessageSetDal();
            ModelSet = Dal.GetModel(1);
            if (ModelSet == null)
            {
                //没找到配置信息 重定向首页
                Response.Redirect("/");
            }
        }
        public void GetModel()
        {
            LeaveMessage.LeaveMessageDal Dal = new LeaveMessage.LeaveMessageDal();
            Model = Dal.GetModel(Int32.Parse(LeaveMessageIDs.Replace(",","").Trim()));
            if (Model == null)
            {
                //没找到配置信息 重定向首页
                Response.Redirect("/");
            }
        }
    }
}
