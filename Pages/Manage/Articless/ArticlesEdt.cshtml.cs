using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SunuerManage.Pages.Manage.Articless
{
    /// <summary>
    /// Project:Sunuer Manage
    /// Description:���±༭
    /// Author��HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License��Apache License 2.0
    /// </summary>
    public class ArticlesEdtModel : PageModel
    {
        [BindProperty(SupportsGet = true)] // ����ͨ�� GET �����
        public int ArticleID { get; set; } = 0;
        public Articles.ArticlesModel Model = new Articles.ArticlesModel();
        //����httpע�����ڻ�ȡIP
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ArticlesEdtModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
        public void OnGet()
        {

            //�жϵ�½״̬
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);

            if (AdminModel.Roles.IndexOf("|22|") < 0)
            {
                // �ض��򵽵�¼ҳ��
                Response.Redirect("/Manage/Login");
            }
            if (ArticleID == 0)
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
            Articles.ArticlesDal Dal = new Articles.ArticlesDal();
            Model = Dal.GetModel(ArticleID);
            if (Model == null)
            {
                // �ض��򵽵�¼ҳ��
                Response.Redirect("/Manage/Login");
            }
        }
    }
}
