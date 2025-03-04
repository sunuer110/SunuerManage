using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SunuerManage.Pages.Manage.Admins
{/// <summary>
 /// Project:Sunuer Manage
 /// Description:�޸�����
 /// Author��HaiDong
 /// Site:https://www.sunuer.com
 /// Version: 1.0
 /// License��Apache License 2.0
 /// </summary>
    public class AdminPassWordModel : PageModel
    {
        [BindProperty(SupportsGet = true)] // ����ͨ�� GET �����
        public int RoleID { get; set; } = 0;
        public Admin.AdminModel Model = new Admin.AdminModel();
        //����httpע�����ڻ�ȡIP
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AdminPassWordModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
        public void OnGet()
        {
            //�жϵ�½״̬
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);
            //�ж��Ƿ���Ȩ��
            if (AdminModel.Roles.IndexOf("|14|") < 0)
            {
                // �ض��򵽵�¼ҳ��
                Response.Redirect("/Manage/Login");
            }
            GetModel();

            ViewData["Title"] = "Sunuer Managae";
            ViewData["KeyWords"] = "Sunuer Managae";
            ViewData["Description"] = "Sunuer Managae";
        }

        public void GetModel()
        {
            Admin.AdminDal Dal = new Admin.AdminDal();
            Model = Dal.GetModel(AdminModel.AdminID);
            if (Model == null)
            {
                // �ض��򵽵�¼ҳ��
                Response.Redirect("/Manage/Login");
            }
        }
    }
}
