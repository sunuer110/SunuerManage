using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SunuerManage.Pages
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:��ϵ����
    /// Author��HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License��Apache License 2.0
    /// </summary>
    public class ContactModel : PageModel
    {
        public string Next = "";
        public string Last = "";
        public Articles.ArticlesModel NewsList = new Articles.ArticlesModel();
        public ArticleCategory.ArticleCategoryModel Model = new ArticleCategory.ArticleCategoryModel();

        private int nid { get; set; } = 6;

        //���ݲ�������ҳ��
        public SunuerManage.Pages.Shared.MenuModel? Menu { get; set; }
        public void OnGet()
        {
            //���ݲ�������ҳ��
            var menu = new SunuerManage.Pages.Shared.MenuModel();
            menu.GetMenu();
            Menu = menu;  // ���� Menu ����

            GetModel();

        }
        public void GetModel()
        {
            Articles.ArticlesDal Dal = new Articles.ArticlesDal();
            NewsList = Dal.GetModel(nid);
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


    }
}
