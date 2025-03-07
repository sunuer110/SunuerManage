using System;
using System.ComponentModel.DataAnnotations;

namespace LeaveMessageSet
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:LeaveMessageSetModel
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class LeaveMessageSetModel
    {
        public int SetID { get; set; } = 0; // 配置ID
        public int CreateUserID { get; set; } = 0; // 创建人
        public DateTime CreateDate { get; set; } // 创建时间
        public int UpdateUserID { get; set; } = 0; // 更新人
        public DateTime UpdateDate { get; set; } // 更新时间
        public int Del { get; set; } = 0; // 删除0否1是
        public int PhoneRequire { get; set; } = 0; // 手机号0非必填1必填
        public int NameRequire { get; set; } = 0; // 姓名0非必填1必填
        public int EmailRequire { get; set; } = 0; // 邮箱0非必填1必填
        public string SystemEmail { get; set; } = ""; // 留言系统邮箱
    }
}