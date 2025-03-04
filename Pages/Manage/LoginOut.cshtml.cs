using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace SunuerManage.Pages.Manage
{
    /// <summary>
    /// Project:Sunuer Manage
    /// Description:退出登陆
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class LoginOutModel : PageModel
    {
       
        public void OnGet()
        {
            
            // 设置 Cookie 选项
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, // 防止客户端脚本访问                                
                SameSite = SameSiteMode.Strict, // 防止 CSRF
                Expires = DateTime.Now.AddHours(-1) // 根据 Hours 设置过期时间
                ,Path = "/" // 确保路径匹配
            };
            // 保存 JSON 字符串到 Cookie 中
            Response.Cookies.Append("SunuerCookie", "", cookieOptions);

            // 重定向到登录页面
            Response.Redirect("/Manage/Login");

            ViewData["Title"] = "Sunuer Managae";
            ViewData["KeyWords"] = "Sunuer Managae";
            ViewData["Description"] = "Sunuer Managae";
        }
    }
}
