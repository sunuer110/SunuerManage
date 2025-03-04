using System;
using System.ComponentModel.DataAnnotations;

namespace AdminPower
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:AdminPowerModel
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class AdminPowerModel
    {
        public int PowerID { get; set; } = 0; // 
        public int CreateUserID { get; set; } = 0; // 创建人
        public DateTime CreateDate { get; set; } // 创建时间
        public int UpdateUserID { get; set; } = 0; // 更新人
        public DateTime UpdateDate { get; set; } // 更新时间
        public int Del { get; set; } = 0; // 删除0否1是
        public int ParentID { get; set; } = 0; // 父级ID
        public string PowerTitle { get; set; } = ""; // 权限名称
    }
}