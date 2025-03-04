using System.Data;

namespace Admin
{
    /// <summary>
    /// Project:Sunuer Manage
    /// Description:AdminModel
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class AdminModel
    {
        public int AdminID { get; set; } = 0;//管理员ID
        public int CreateUserID { get; set; } = 0;//创建人ID
        public DateTime CreateDate { get; set; }//创建时间
        public DateTime UpdateDate { get; set; }//更新时间
        public int UpdateUserID { get; set; } = 0;//更新人ID
        public int Del { get; set; } = 0;//删除0未删除1删除
        public string AdminName { get; set; } = "";//用户名
        public string PassWord { get; set; } = "";//密码
        public int RoleID { get; set; } = 0;//角色ID
        public string Roles { get; set; } = "";//角色集合
        public string RolesTitle { get; set; } = "";//角色名称
        public string AdminNick { get; set; } = "";//昵称
        public int Statues { get; set; } = 0;//状态0正常1冻结
        public int LoginAttempts { get; set; } = 0;//登陆尝试次数-30分钟内连续登陆失败会记录登陆尝试次数，超过3次失败30分钟内将不允许登陆 这样会减少暴力破解概率
        public DateTime LoginAttemptsLast { get; set; }//登陆尝试最后时间-每次登陆失败会记录当前登陆尝试时间
    }
    /// <summary>
    /// AdminLoginModel登陆模型
    /// </summary>
    public class AdminLoginModel
    {
        public int AdminID { get; set; } = 0;//管理员ID
        public string AdminNick { get; set; } = "";//昵称
        public string Roles { get; set; } = "";//角色集合
        public string IP { get; set; } = "";//IP
        public DateTime DateTime { get; set; }//登陆时间
    }
}
