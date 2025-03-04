using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebSite.Pages.Manage
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:ºóÌ¨µÇÂ½
    /// Author£ºHaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License£ºApache License 2.0
    /// </summary>
    public class LoginModel : PageModel
    {
        public bool IsMobile { get; set; }
        public void OnGet()
        {
            ViewData["Title"] = "Sunuer Manage";
            ViewData["KeyWords"] = "Sunuer Manage";
            ViewData["Description"] = "Sunuer Manage";
            IsMobile = IsMobileDevice(Request);
            //if(IsMobile)
            //{

            //    Response.Redirect("/");
            //}
        }
        private bool IsMobileDevice(HttpRequest request)
        {
            var userAgent = request.Headers["User-Agent"].ToString().ToLower();
            return userAgent.Contains("mobi") || userAgent.Contains("android") || userAgent.Contains("iphone");
        }
    }
}
