using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Numerics;
using SunuerManage.Pages.Shared;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SunuerManage.Pages
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:案例
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class CasesModel : PageModel
    {
        public string Phone = "";
        public List<ArticleCategory.ArticleCategoryModel> ArticleCategoryList = new List<ArticleCategory.ArticleCategoryModel>();
        public List<Articles.ArticlesModel> CaseList = new List<Articles.ArticlesModel>();
        public ArticleCategory.ArticleCategoryModel Model = new ArticleCategory.ArticleCategoryModel();
        public int TotalRecords { get; set; }
        [BindProperty(SupportsGet = true)] // 允许通过 GET 请求绑定
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 6; // 每页条数
        public int ShowSize { get; set; } = 2; // 显示几个分页按钮

        [BindProperty(SupportsGet = true)] // 允许通过 GET 请求绑定
        public int nid { get; set; } = 2;


        //传递参数布局页面
        public SunuerManage.Pages.Shared.MenuModel? Menu { get; set; }
        public void OnGet()
        {
            //传递参数布局页面
            var menu = new SunuerManage.Pages.Shared.MenuModel();
            menu.GetMenu();
            Menu = menu;  // 传递 Menu 数据
            Phone = Tools.ConfigurationHelper.GetConfigValue("ManageSet:Phone")!;
            GetMenu();
            GetCategoryModel();
            GetCase(CurrentPage);

        }

        public void GetMenu()
        {
            //菜单信息
            ArticleCategory.ArticleCategoryDal Dal = new ArticleCategory.ArticleCategoryDal();

            DataTable Datas = Dal.Get(1);
            ArticleCategoryList = Tools.DataTableToList.DatatableToList<ArticleCategory.ArticleCategoryModel>(Datas);
        }
        public void GetCase(int Page)
        {
            Console.WriteLine(Page.ToString());
            int StartRecord = (Page - 1) * PageSize + 1;

            // 计算每页显示的条数
            int EndRecord = PageSize;
            //轮显
            Articles.ArticlesDal Dal = new Articles.ArticlesDal();
            TotalRecords = Int32.Parse(Dal.GetTopCount(nid, "").Rows[0]["Num"].ToString()!);
            DataTable Datas = Dal.GetTop(nid, "", StartRecord, EndRecord);
            CaseList = Tools.DataTableToList.DatatableToList<Articles.ArticlesModel>(Datas);
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
        }
    }
}
