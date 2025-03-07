using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Manage.LeaveMessages
{
    /// <summary>
    /// Project:Sunuer Manage
    /// Description:�������
    /// Author��HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License��Apache License 2.0
    /// </summary>
    public class LeaveMessageAuditModel : PageModel
    {
        public LeaveMessageSet.LeaveMessageSetModel ModelSet = new LeaveMessageSet.LeaveMessageSetModel();
        public LeaveMessage.LeaveMessageModel Model = new LeaveMessage.LeaveMessageModel();
        [BindProperty(SupportsGet = true)] // ����ͨ�� GET �����
        public string LeaveMessageIDs { get; set; } = "";
        //����httpע�����ڻ�ȡIP
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LeaveMessageAuditModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
        public void OnGet()
        {
            //�жϵ�½״̬
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);
            //�ж��Ƿ���Ȩ��
            if (AdminModel.Roles.IndexOf("|403|") < 0)
            {
                // �ض��򵽵�¼ҳ��
                Response.Redirect("/Manage/Login");
            }
            if (string.IsNullOrEmpty(LeaveMessageIDs)) {
                // �ض��򵽵�¼ҳ��
                Response.Redirect("/Manage/Login");
            }
            GetModel();
            GetModelSet();
            ViewData["Title"] = "Sunuer Managae";
            ViewData["KeyWords"] = "Sunuer Managae";
            ViewData["Description"] = "Sunuer Managae";
        }
        public void GetModelSet()
        {
            LeaveMessageSet.LeaveMessageSetDal Dal = new LeaveMessageSet.LeaveMessageSetDal();
            ModelSet = Dal.GetModel(1);
            if (ModelSet == null)
            {
                //û�ҵ�������Ϣ �ض�����ҳ
                Response.Redirect("/");
            }
        }
        public void GetModel()
        {
            LeaveMessage.LeaveMessageDal Dal = new LeaveMessage.LeaveMessageDal();
            Model = Dal.GetModel(Int32.Parse(LeaveMessageIDs.Replace(",","").Trim()));
            if (Model == null)
            {
                //û�ҵ�������Ϣ �ض�����ҳ
                Response.Redirect("/");
            }
        }
    }
}
