using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SunuerManage.Pages.Manage.Articless
{
    /// <summary>
    /// Project:Sunuer Manage
    /// Description:����༭
    /// Author��HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License��Apache License 2.0
    /// </summary>
    public class ArticleCategoryEdtModel : PageModel
    {
        [BindProperty(SupportsGet = true)] // ����ͨ�� GET �����
        public int BigID { get; set; } = 0;
        public ArticleCategory.ArticleCategoryModel Model = new ArticleCategory.ArticleCategoryModel();
        //����httpע�����ڻ�ȡIP
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ArticleCategoryEdtModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
        public void OnGet()
        {
            //�жϵ�½״̬
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);
            //�ж��Ƿ���Ȩ��
            if (AdminModel.Roles.IndexOf("|19|") < 0)
            {
                // �ض��򵽵�¼ҳ��
                Response.Redirect("/Manage/Login");
            }
            if (BigID == 0)
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
            ArticleCategory.ArticleCategoryDal Dal = new ArticleCategory.ArticleCategoryDal();
            Model = Dal.GetModel(BigID);
            if (Model == null)
            {
                // �ض��򵽵�¼ҳ��
                Response.Redirect("/Manage/Login");
            }
        }
    }
}
