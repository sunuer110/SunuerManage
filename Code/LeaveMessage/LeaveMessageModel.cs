using System;
using System.ComponentModel.DataAnnotations;

namespace LeaveMessage
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:LeaveMessageModel
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class LeaveMessageModel
    {
        public int LeaveMessageID { get; set; } = 0; // 留言ID
        public int CreateUserID { get; set; } = 0; // 创建人
        public DateTime CreateDate { get; set; } // 创建时间
        public int UpdateUserID { get; set; } = 0; // 更新人
        public DateTime UpdateDate { get; set; } // 更新时间
        public int Del { get; set; } = 0; // 删除0否1是
        public string Title { get; set; } = ""; // 标题
        public string Phone { get; set; } = ""; // 手机号
        public string UserName { get; set; } = ""; // 姓名
        public string Content { get; set; } = ""; // 留言内容
        public string Email { get; set; } = ""; // 邮箱
        public int AuditUserID { get; set; } = 0; // 审核人
        public int AuditStatus { get; set; } = 0; // 审核状态0未审核1审核通过2审核未通过
        public DateTime AuditTime { get; set; } // 审核时间
        public string AuditDesn { get; set; } = ""; // 审核备注
        public string AuditUserName { get; set; } = ""; // 审核人名
    }
}