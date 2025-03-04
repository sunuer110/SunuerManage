using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace SunuerManage.Pages
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:关于我们
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class AboutModel : PageModel
    {

        public string Phone = "";
        public string Next = "";
        public string Last = "";
        public Articles.ArticlesModel NewsList = new Articles.ArticlesModel();
        public ArticleCategory.ArticleCategoryModel Model = new ArticleCategory.ArticleCategoryModel();

        private int nid { get; set; } = 5;
        //传递参数布局页面
        public SunuerManage.Pages.Shared.MenuModel? Menu { get; set; }
        public void OnGet()
        {
            //传递参数布局页面
            var menu = new SunuerManage.Pages.Shared.MenuModel();
            menu.GetMenu();
            Menu = menu;  // 传递 Menu 数据

            Phone = Tools.ConfigurationHelper.GetConfigValue("ManageSet:Phone")!;
            GetModel();

        }
        public void GetModel()
        {
            Articles.ArticlesDal Dal = new Articles.ArticlesDal();
            NewsList = Dal.GetModel(nid);
            if (NewsList == null)
            {
                //重定向首页
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
                //重定向首页
                RedirectToPage("/");
            }
        }

       
    }
}
