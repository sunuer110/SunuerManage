using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SunuerManage.Pages.Manage.Articless
{
    /// <summary>
    /// Project:Sunuer Manage
    /// Description:�������
    /// Author��HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License��Apache License 2.0
    /// </summary>
    public class ArticleCategoryAddModel : PageModel
    {

        [BindProperty(SupportsGet = true)] // ����ͨ�� GET �����
        public int ParentID { get; set; } = 0;

        //����httpע�����ڻ�ȡIP
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ArticleCategoryAddModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
        public void OnGet()
        {
            //�жϵ�½״̬
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);
            //�ж��Ƿ���Ȩ��
            if (AdminModel.Roles.IndexOf("|18|") < 0)
            {
                // �ض��򵽵�¼ҳ��
                Response.Redirect("/Manage/Login");
            }
            if (ParentID < 0)
            {
                // �ض��򵽵�¼ҳ��
                Response.Redirect("/Manage/Login");
            }

            ViewData["Title"] = "Sunuer Managae";
            ViewData["KeyWords"] = "Sunuer Managae";
            ViewData["Description"] = "Sunuer Managae";
        }
    }
}
