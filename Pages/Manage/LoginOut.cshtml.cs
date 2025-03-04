using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace SunuerManage.Pages.Manage
{
    /// <summary>
    /// Project:Sunuer Manage
    /// Description:�˳���½
    /// Author��HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License��Apache License 2.0
    /// </summary>
    public class LoginOutModel : PageModel
    {
       
        public void OnGet()
        {
            
            // ���� Cookie ѡ��
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, // ��ֹ�ͻ��˽ű�����                                
                SameSite = SameSiteMode.Strict, // ��ֹ CSRF
                Expires = DateTime.Now.AddHours(-1) // ���� Hours ���ù���ʱ��
                ,Path = "/" // ȷ��·��ƥ��
            };
            // ���� JSON �ַ����� Cookie ��
            Response.Cookies.Append("SunuerCookie", "", cookieOptions);

            // �ض��򵽵�¼ҳ��
            Response.Redirect("/Manage/Login");

            ViewData["Title"] = "Sunuer Managae";
            ViewData["KeyWords"] = "Sunuer Managae";
            ViewData["Description"] = "Sunuer Managae";
        }
    }
}
