using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace SunuerManage.Pages
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:����ҳ
    /// Author��HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License��Apache License 2.0
    /// </summary>
    public class newsviewModel : PageModel
    {
        public string Phone = "";
        public string Next = "";
        public string Last = "";
        public Articles.ArticlesModel NewsList = new Articles.ArticlesModel();
        public ArticleCategory.ArticleCategoryModel Model = new ArticleCategory.ArticleCategoryModel();
       
        [BindProperty(SupportsGet = true)] // ����ͨ�� GET �����
        public int ArticleID { get; set; } = 3;
        //���ݲ�������ҳ��
        public SunuerManage.Pages.Shared.MenuModel? Menu { get; set; }
        public void OnGet()
        {
            //���ݲ�������ҳ��
            var menu = new SunuerManage.Pages.Shared.MenuModel();
            menu.GetMenu();
            Menu = menu;  // ���� Menu ����

            Phone =Tools.ConfigurationHelper.GetConfigValue("ManageSet:Phone")!;
            GetModel();
            GetNext();

        }
        public void GetModel()
        {
            Articles.ArticlesDal Dal = new Articles.ArticlesDal();
            NewsList = Dal.GetModel(ArticleID);
            if (NewsList == null)
            {
                //�ض�����ҳ
                RedirectToPage("/");
            }
            else
            {
                GetCategoryModel(NewsList.BigID);
                ViewData["Title"] = NewsList.ArticleTitle;
                ViewData["KeyWords"] = NewsList.Articlekey;
                ViewData["Description"] = NewsList.ArticleDesn;
            }
        }

        public void GetCategoryModel(int nid)
        {
            ArticleCategory.ArticleCategoryDal Dal = new ArticleCategory.ArticleCategoryDal();
            Model = Dal.GetModel(nid);
            if (Model == null)
            {
                //�ض�����ҳ
                RedirectToPage("/");
            }
        }

        public void GetNext()
        {
            Articles.ArticlesDal Dal = new Articles.ArticlesDal();
            DataTable Datas = Dal.GetNext(ArticleID);
            if(Datas.Rows.Count> 0)
            {
               Next = $"<a href=\"newsview?ArticleID={Datas.Rows[0]["ArticleID"]}\" title=\"{Datas.Rows[0]["ArticleTitle"]}\">{Datas.Rows[0]["ArticleTitle"]}</a>";
            }
            else
            {
                Next = $"<a href=\"/\" title=\"{Tools.ConfigurationHelper.GetConfigValue("ManageSet:ManageKey")}\">��</a>";
            }

            DataTable DatasLast = Dal.GetLast(ArticleID);
            if (DatasLast.Rows.Count > 0)
            {
                Last = $"<a href=\"newsview?ArticleID={DatasLast.Rows[0]["ArticleID"]}\" title=\"{DatasLast.Rows[0]["ArticleTitle"]}\">{DatasLast.Rows[0]["ArticleTitle"]}</a>";
            }
            else
            {
                Last = $"<a href=\"/\" title=\"{Tools.ConfigurationHelper.GetConfigValue("ManageSet:ManageKey")}\">��</a>";
            }
        }
    }
}
