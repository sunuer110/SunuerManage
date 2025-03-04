using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SunuerManage.Pages.Manage.Admins
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:��ɫ�༭
    /// Author��HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License��Apache License 2.0
    /// </summary>
    public class AdminRolesEdtModel : PageModel
    {
        [BindProperty(SupportsGet = true)] // ����ͨ�� GET �����
        public int RoleID { get; set; } = 0;
        public AdminRoles.AdminRolesModel Model = new AdminRoles.AdminRolesModel();
        //����httpע�����ڻ�ȡIP
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AdminRolesEdtModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
        public void OnGet()
        {
            //�жϵ�½״̬
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);
            //�ж��Ƿ���Ȩ��
            if (AdminModel.Roles.IndexOf("|7|") < 0)
            {
                // �ض��򵽵�¼ҳ��
                Response.Redirect("/Manage/Login");
            }
            if (RoleID == 0)
            {

                // �ض��򵽵�¼ҳ��
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
            AdminRoles.AdminRolesDal Dal = new AdminRoles.AdminRolesDal();
            Model = Dal.GetModel(RoleID);
            if (Model == null)
            {
                // �ض��򵽵�¼ҳ��
                Response.Redirect("/Manage/Login");
            }
        }
    }
}
