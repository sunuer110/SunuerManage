using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace SunuerManage.Pages.Shared
{
    public class MenuModel : PageModel
    {
        //��������
        public string Logo { get; set; } = "";
        public string ManageTitle { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
        public string Address { get; set; } = "";
        public string CopyRight { get; set; } = "";
        public string ManageUrl { get; set; } = "";
        //list�˵�
        public List<ArticleCategory.ArticleCategoryModel> ArticleCategoryList = new List<ArticleCategory.ArticleCategoryModel>();
        

        public void GetMenu()
        {
            //ҳ�����������ֵ
            Logo =Tools.ConfigurationHelper.GetConfigValue("ManageSet:Logo");
            ManageTitle = Tools.ConfigurationHelper.GetConfigValue("ManageSet:ManageTitle");
            Phone = Tools.ConfigurationHelper.GetConfigValue("ManageSet:Phone");
            Email = Tools.ConfigurationHelper.GetConfigValue("ManageSet:Email");
            Address = Tools.ConfigurationHelper.GetConfigValue("ManageSet:Address");
            CopyRight = Tools.ConfigurationHelper.GetConfigValue("ManageSet:CopyRight");
            ManageUrl = Tools.ConfigurationHelper.GetConfigValue("ManageSet:ManageUrl");
            //�˵���Ϣ
            ArticleCategory.ArticleCategoryDal Dal = new ArticleCategory.ArticleCategoryDal();

            DataTable Datas = Dal.Get(1);
            ArticleCategoryList = Tools.DataTableToList.DatatableToList<ArticleCategory.ArticleCategoryModel>(Datas);
        }
    }


}