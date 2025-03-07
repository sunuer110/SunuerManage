using ApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace Web.Controllers.ManageSets
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:系统设置
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ManageSetController : ControllerBase
    {

        // 通过依赖注入获取 IHttpContextAccessor- 获取实际IP地址使用
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ManageSetController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        // POST: api/ManageSet/Update
        //通过 Post 请求更新数据密码，并返回影响的记录数。
        [HttpPost("Update")]//定义Update方法
        public ActionResult<ApiResponse<int>> Update(
              [FromForm] string ManageTitle
            , [FromForm] string? ManageKey
            , [FromForm] string? ManageDesn
            , [FromForm] string? ManageUrl
            , [FromForm] string? Phone
            , [FromForm] string? Email
            , [FromForm] string? Address
            , [FromForm] string? CopyRight
            , [FromForm] string? ImageUrl
            , [FromForm] string? About
            , [FromForm] string? Logo
            )
        {

            // 检查参数是否为空
            if (string.IsNullOrWhiteSpace(ManageTitle)
             )
            {
                return BadRequest(new ApiResponse<int>
                {
                    code = 400,
                    msg = "传入参数不为空",
                    count = 0,
                    data = 0
                });
            }

            //后端参数判断是否为空
            //ManageKey校验
            if (string.IsNullOrWhiteSpace(ManageKey))
            {
                ManageKey = "";
            }
            //ManageDesn校验
            if (string.IsNullOrWhiteSpace(ManageDesn))
            {
                ManageDesn = "";
            }
            //ManageUrl校验
            if (string.IsNullOrWhiteSpace(ManageUrl))
            {
                ManageUrl = "";
            }
            //Phone校验
            if (string.IsNullOrWhiteSpace(Phone))
            {
                Phone = "";
            }
            //Email校验
            if (string.IsNullOrWhiteSpace(Email))
            {
                Email = "";
            }
            //Address校验
            if (string.IsNullOrWhiteSpace(Address))
            {
                Address = "";
            }
            //CopyRight校验
            if (string.IsNullOrWhiteSpace(CopyRight))
            {
                CopyRight = "";
            }
            //ImageUrl校验
            if (string.IsNullOrWhiteSpace(ImageUrl))
            {
                ImageUrl = "";
            }
            //About校验
            if (string.IsNullOrWhiteSpace(About))
            {
                About = "";
            }
            //Logo校验
            if (string.IsNullOrWhiteSpace(Logo))
            {
                Logo = "";
            }
            Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
            //判断登陆状态
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);
            //判断是否有权限
            if (AdminModel.Roles.IndexOf("|24|") < 0)
            {
                return Ok(new ApiResponse<int> { code = 404, msg = "无权限", count = 0, data = 0 });  // 返回新数据的ID
            }
            else
            {
                // 读取现有的 appsettings.json 文件
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                var json = System.IO.File.ReadAllText(filePath);
                // 解析 JSON 内容
                var jsonObj = JObject.Parse(json);

                // 更新指定的键值对
                var appSettings = jsonObj["ManageSet"];
                if (appSettings != null)
                {
                    appSettings["ManageTitle"] = ManageTitle;
                    appSettings["ManageKey"] = ManageKey;
                    appSettings["ManageDesn"] = ManageDesn;
                    appSettings["ManageUrl"] = ManageUrl;
                    appSettings["Phone"] = Phone;
                    appSettings["Email"] = Email;
                    appSettings["Address"] = Address;
                    appSettings["CopyRight"] = CopyRight;
                    appSettings["ImageUrl"] = ImageUrl;
                    appSettings["About"] = About;
                    appSettings["Logo"] = Logo;
                }

                // 将更新后的 JSON 内容重新写入到 appsettings.json 文件
                System.IO.File.WriteAllText(filePath, jsonObj.ToString());
                return Ok(new ApiResponse<int> { code = 0, msg = "更新成功", count = 1, data = 1 });  // 返回成功条数
                
            }
        }

    }
}
