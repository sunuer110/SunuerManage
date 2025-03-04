using System;
using System.ComponentModel.DataAnnotations;

namespace ManageSet
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:ManageSetModel
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class ManageSetModel
    {
        public int ID { get; set; } = 0; // 
        public string ManageTitle { get; set; } = ""; // 系统名称
        public string ManageKey { get; set; } = ""; // 系统关键字
        public string ManageDesn { get; set; } = ""; // 系统描述
        public string ManageUrl { get; set; } = ""; // 系统链接
        public string Phone { get; set; } = ""; // 手机号
        public string Email { get; set; } = ""; // 邮箱
        public string Address { get; set; } = ""; // 地址
        public string CopyRight { get; set; } = ""; // 版权信息
        public string ImageUrl { get; set; } = ""; // 图片网址
        public string About { get; set; } = ""; // 系统描述
        public string Logo { get; set; } = ""; // Logo
    }
}
