using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SunuerManage.Pages
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:新闻页
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class NewsModel : PageModel
    {
        public string Phone = "";
        public List<Articles.ArticlesModel> NewsList = new List<Articles.ArticlesModel>();
        public ArticleCategory.ArticleCategoryModel Model = new ArticleCategory.ArticleCategoryModel();
        public List<ArticleCategory.ArticleCategoryModel> ModelList = new List<ArticleCategory.ArticleCategoryModel>();
        public int TotalRecords { get; set; }
        [BindProperty(SupportsGet = true)] // 允许通过 GET 请求绑定
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 6; // 每页条数
        public int ShowSize { get; set; } = 2; // 显示几个分页按钮

        [BindProperty(SupportsGet = true)] // 允许通过 GET 请求绑定
        public int nid { get; set; } = 3;
        //传递参数布局页面
        public SunuerManage.Pages.Shared.MenuModel? Menu { get; set; }
        public void OnGet()
        {
            //传递参数布局页面
            var menu = new SunuerManage.Pages.Shared.MenuModel();
            menu.GetMenu();
            Menu = menu;  // 传递 Menu 数据

            Phone =Tools.ConfigurationHelper.GetConfigValue("ManageSet:Phone");
            GetCategoryModel();
            GetCase(CurrentPage);

        }

        public void GetCase(int Page)
        {
            int StartRecord = (Page - 1) * PageSize + 1;

            // 计算每页显示的条数
            int EndRecord = PageSize;
            //轮显
            Articles.ArticlesDal Dal = new Articles.ArticlesDal();
            TotalRecords = Int32.Parse(Dal.GetTopCount(nid, "").Rows[0]["Num"].ToString()!);
            DataTable Datas = Dal.GetTop(nid, "", StartRecord, EndRecord);
            NewsList = Tools.DataTableToList.DatatableToList<Articles.ArticlesModel>(Datas);
        }

        public void GetCategoryModel()
        {
            ArticleCategory.ArticleCategoryDal Dal = new ArticleCategory.ArticleCategoryDal();
            Model = Dal.GetModel(nid);
            if (Model == null)
            {
                //重定向首页
                RedirectToPage("/");
            }
            else
            {

                ViewData["Title"] = Model.KeyTitle;
                ViewData["KeyWords"] = Model.KeyWord;
                ViewData["Description"] = Model.KeyDesn;
            }
            //获取大类下子分类
            DataTable Datas = Dal.Get(3);
            ModelList = Tools.DataTableToList.DatatableToList<ArticleCategory.ArticleCategoryModel>(Datas);
        }
    }
}
