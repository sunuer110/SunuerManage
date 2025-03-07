using ApiResponse;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace Web.Controllers.LeaveMessageSets
{
    /// <summary>
    /// Project:Sunuer Manage
    /// Description:LeaveMessageSet API
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveMessageSetController : ControllerBase
    {
        // 通过依赖注入获取 IHttpContextAccessor- 获取实际IP地址使用
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LeaveMessageSetController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }



        // POST: api/LeaveMessageSet/Update
        //通过 POST 请求修改数据，参数接收 int型如果没有该参数 默认是0
        [HttpPost("Update")]
        public ActionResult<ApiResponse<int>> Update(
        [FromForm] int SetID

        , [FromForm] int PhoneRequire
        , [FromForm] int NameRequire
        , [FromForm] int EmailRequire
        , [FromForm] string SystemEmail
        )
        {
            // 检查参数是否为空
            if (
            SetID < 0


            || PhoneRequire < 0
            || NameRequire < 0
            || EmailRequire < 0
            || string.IsNullOrWhiteSpace(SystemEmail)
            )
            {
                return BadRequest(new ApiResponse<int>
                {
                    code = 400,
                    msg = "传入参数错误",
                    count = 0,
                    data = 0
                });
            }

            Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
            //判断登陆状态
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);
            //判断是否有权限
            if (AdminModel.Roles.IndexOf("|401|") < 0)
            {
                return Ok(new ApiResponse<int>
                {
                    code = 404,
                    msg = "无权限",
                    count = 0,
                    data = 0
                });
            }
            else
            {
                var Model = new LeaveMessageSet.LeaveMessageSetModel
                {
                    // 数据模型赋值
                    SetID = SetID,// 配置ID 
                    UpdateUserID = AdminModel.AdminID,// 更新人 
                    PhoneRequire = PhoneRequire,// 手机号0非必填1必填 
                    NameRequire = NameRequire,// 姓名0非必填1必填 
                    EmailRequire = EmailRequire,// 邮箱0非必填1必填 
                    SystemEmail = SystemEmail,// 留言系统邮箱 
                };
                // 实例化数据访问层对象，负责数据库访问的类
                LeaveMessageSet.LeaveMessageSetDal Dal = new LeaveMessageSet.LeaveMessageSetDal();
                int ID = Dal.Update(Model);
                if (ID > 0)
                {
                    return Ok(new ApiResponse<int>
                    {
                        code = 0,
                        msg = "更新成功",
                        count = ID,
                        data = ID
                    });
                }
                else
                {
                    return Ok(new ApiResponse<int>
                    {
                        code = 404,
                        msg = "更新失败",
                        count = 0,
                        data = 0
                    });
                }
            }
        }
    }
}