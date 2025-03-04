namespace Example
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:演示示例Model
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class ExampleModel
    {
        public int UserID { get; set; } = 0;  // 用户ID，主键，用于唯一标识每个用户
        public string UserName { get; set; } = ""; // 用户名
        public string Phone { get; set; } = "";  // 手机号
        public string PassWord { get; set; } = "";  // 用户密码
        public string IP { get; set; } = "";  // 用户IP地址
        public DateTime CreateDate { get; set; }  // 用户创建时间
        public int Statues { get; set; } = 0;     // 状态（0表示正常，1表示冻结
    }
}
