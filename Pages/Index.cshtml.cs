using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace SunuerManage.Pages
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:首页
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class IndexModel : PageModel
    {
        public bool IsMobile { get; set; } = false;
        //list菜单
        public List<Articles.ArticlesModel> ArticlesList = new List<Articles.ArticlesModel>();
        public List<Articles.ArticlesModel> CaseList = new List<Articles.ArticlesModel>();
        public List<Articles.ArticlesModel> NewsList4 = new List<Articles.ArticlesModel>();
        public List<Articles.ArticlesModel> NewsList5 = new List<Articles.ArticlesModel>();
        public List<Articles.ArticlesModel> NewsList6 = new List<Articles.ArticlesModel>();


        //传递参数布局页面
        public SunuerManage.Pages.Shared.MenuModel? Menu { get; set; }
        public void OnGet()
        {
            var str = Tools.Tools.DateToDays("2025-03-01", "2025-03-06");
            Console.WriteLine(str);
            //传递参数布局页面
            var menu = new SunuerManage.Pages.Shared.MenuModel();
            menu.GetMenu();
            Menu = menu;  // 传递 Menu 数据
            if (Tools.Tools.IsMobileDevice(HttpContext.Request))
            {
                IsMobile = true;
            }
            //页面基础参数赋值
            ViewData["Title"] = Tools.ConfigurationHelper.GetConfigValue("ManageSet:ManageTitle");
            ViewData["KeyWords"] = Tools.ConfigurationHelper.GetConfigValue("ManageSet:ManageKey");
            ViewData["Description"] = Tools.ConfigurationHelper.GetConfigValue("ManageSet:ManageDesn");
          
            GetLunxian();
            GetCase();
            GetNews4();
            GetNews5();
            GetNews6();
        }
        public void GetLunxian()
        {
            //轮显
            Articles.ArticlesDal Dal = new Articles.ArticlesDal();
            if (IsMobile)
            {
                DataTable Datas = Dal.GetTop(12, "", 1, 12);
                ArticlesList = Tools.DataTableToList.DatatableToList<Articles.ArticlesModel>(Datas);
            }
            else
            {
                DataTable Datas = Dal.GetTop(11, "", 1, 12);
                ArticlesList = Tools.DataTableToList.DatatableToList<Articles.ArticlesModel>(Datas);
            }
        }
        public void GetCase()
        {
            //案例
            Articles.ArticlesDal Dal = new Articles.ArticlesDal();

            DataTable Datas = Dal.GetTop(2, "", 1, 6);
            CaseList = Tools.DataTableToList.DatatableToList<Articles.ArticlesModel>(Datas);
        }
        public void GetNews4()
        {
            //新闻ID=4
            Articles.ArticlesDal Dal = new Articles.ArticlesDal();

            DataTable Datas = Dal.GetTop(4, "", 1, 5);
            NewsList4 = Tools.DataTableToList.DatatableToList<Articles.ArticlesModel>(Datas);
        }
        public void GetNews5()
        {
            //新闻ID=5
            Articles.ArticlesDal Dal = new Articles.ArticlesDal();

            DataTable Datas = Dal.GetTop(5, "", 1, 5);
            NewsList5 = Tools.DataTableToList.DatatableToList<Articles.ArticlesModel>(Datas);
        }
        public void GetNews6()
        {
            //新闻ID=6
            Articles.ArticlesDal Dal = new Articles.ArticlesDal();

            DataTable Datas = Dal.GetTop(6, "", 1, 5);
            NewsList6 = Tools.DataTableToList.DatatableToList<Articles.ArticlesModel>(Datas);
        }
    }

}