using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
    public class NewsModel : PageModel
    {
        public string Phone = "";
        public List<Articles.ArticlesModel> NewsList = new List<Articles.ArticlesModel>();
        public ArticleCategory.ArticleCategoryModel Model = new ArticleCategory.ArticleCategoryModel();
        public List<ArticleCategory.ArticleCategoryModel> ModelList = new List<ArticleCategory.ArticleCategoryModel>();
        public int TotalRecords { get; set; }
        [BindProperty(SupportsGet = true)] // ����ͨ�� GET �����
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 6; // ÿҳ����
        public int ShowSize { get; set; } = 2; // ��ʾ������ҳ��ť

        [BindProperty(SupportsGet = true)] // ����ͨ�� GET �����
        public int nid { get; set; } = 3;
        //���ݲ�������ҳ��
        public SunuerManage.Pages.Shared.MenuModel? Menu { get; set; }
        public void OnGet()
        {
            //���ݲ�������ҳ��
            var menu = new SunuerManage.Pages.Shared.MenuModel();
            menu.GetMenu();
            Menu = menu;  // ���� Menu ����

            Phone =Tools.ConfigurationHelper.GetConfigValue("ManageSet:Phone");
            GetCategoryModel();
            GetCase(CurrentPage);

        }

        public void GetCase(int Page)
        {
            int StartRecord = (Page - 1) * PageSize + 1;

            // ����ÿҳ��ʾ������
            int EndRecord = PageSize;
            //����
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
                //�ض�����ҳ
                RedirectToPage("/");
            }
            else
            {

                ViewData["Title"] = Model.KeyTitle;
                ViewData["KeyWords"] = Model.KeyWord;
                ViewData["Description"] = Model.KeyDesn;
            }
            //��ȡ�������ӷ���
            DataTable Datas = Dal.Get(3);
            ModelList = Tools.DataTableToList.DatatableToList<ArticleCategory.ArticleCategoryModel>(Datas);
        }
    }
}
