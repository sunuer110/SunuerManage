using System;
using System.ComponentModel.DataAnnotations;

namespace AdminRoles
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:AdminRolesModel
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class AdminRolesModel
    {
        public int RoleID { get; set; } = 0; // 
        public int CreateUserID { get; set; } = 0; // 创建人
        public DateTime CreateDate { get; set; } // 创建时间
        public int UpdateUserID { get; set; } = 0; // 更新人
        public DateTime UpdateDate { get; set; } // 更新时间
        public int Del { get; set; } = 0; // 删除0否1是
        public string RolesTitle { get; set; } = ""; // 角色名称
        public string Powers { get; set; } = ""; // 权限集合
    }
}