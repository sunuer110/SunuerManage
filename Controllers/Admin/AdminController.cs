using Admin;
using ApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net.Http;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace SunuerManage.Controllers
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:Admin API
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        // 通过依赖注入获取 IHttpContextAccessor- 获取实际IP地址使用
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }



        // POST: api/Admin/Login
        //通过 POST 请求登陆，参数接收
        [HttpPost("Login")]
        public ActionResult<ApiResponse<int>> Login([FromForm] string AdminName, [FromForm] string PassWord, [FromForm] string text)
        {  // 检查参数是否为空
            if (string.IsNullOrWhiteSpace(AdminName) || string.IsNullOrWhiteSpace(PassWord) || string.IsNullOrWhiteSpace(text))
            {
                return BadRequest(new ApiResponse<int>
                {
                    code = 400,
                    msg = "传入参数不为空",
                    count = 0,
                    data = 0
                });
            }
            //前端JS加密后端进行解密保证登陆过程字符串是加密状态
            JsEncrypt.JsEncryptHelper newRSA = new JsEncrypt.JsEncryptHelper();
            //进行解密
            text = newRSA.Decrypt(text);
            AdminName = newRSA.Decrypt(AdminName);
            PassWord = newRSA.Decrypt(PassWord);

            string chkcode = Request.Cookies["CaptchaCode"] ?? string.Empty;
            if (!string.IsNullOrEmpty(chkcode) && Tools.CBC.DecryptHtmlDecode(chkcode) != "错误")
            {
                chkcode = Tools.CBC.DecryptHtmlDecode(chkcode);
                if (!chkcode.Equals(chkcode.ToUpper()))//如果系统生成的不为大写则转换成大写形式
                    chkcode.ToUpper();
                if (text.ToUpper().Trim().Equals(chkcode.Trim())) //将输入的验证码转换成大写并与系统生成的比较
                {

                    //  Console.WriteLine("AdminName"+ AdminName);
                    // 实例化数据访问层对象，AdminDal 是负责数据库访问的类
                    Admin.AdminDal Dal = new Admin.AdminDal();
                    Admin.AdminModel Model = new Admin.AdminModel();
                    Model = Dal.login(AdminName);
                    if (Model != null)
                    {
                        int LoginAttempts = Model.LoginAttempts;
                        //三次登陆锁定状态
                        if (Model.LoginAttempts >= 3 && Model.LoginAttemptsLast.AddMinutes(30) > DateTime.Now)
                        {
                            return Ok(new ApiResponse<int> { code = 404, msg = "请30分钟后尝试", count = 0, data = -2 });  // 返回登陆失败
                        }
                        else
                        {
                            if (Model.PassWord == Tools.Tools.MD5(PassWord))
                            {


                                //保存Cookies
                                // 创建一个对象来保存到 Cookie 中
                                var cookieData = new
                                {
                                    AdminID = Model.AdminID,//用户ID
                                    Roles = Model.Roles, // 用户角色名
                                    AdminNick = Model.AdminNick, // 昵称
                                    IP = Tools.Tools.GetUserIp(_httpContextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null.")),// 调用 GetUserIp 方法，传递当前 HttpContext
                                    DateTime = DateTime.Now
                                };
                                // 将对象序列化为 JSON 字符串
                                string jsonString = JsonConvert.SerializeObject(cookieData);
                                //对字符串进行加密
                                jsonString = Tools.CBC.HtmlEncodeEncrypt(jsonString);
                                int Hours = 0;

                                if (Tools.ConfigurationHelper.GetConfigValue("CookieExpires") != "")
                                {
                                    Hours = Int32.Parse(Tools.ConfigurationHelper.GetConfigValue("CookieExpires")!);
                                }
                                // 设置 Cookie 选项
                                var cookieOptions = new CookieOptions
                                {
                                    HttpOnly = true, // 防止客户端脚本访问
                                    //  Secure = true, // 仅在 HTTPS 连接中传输
                                    SameSite = SameSiteMode.Strict, // 防止 CSRF
                                    Expires = Hours == 0 ? null : DateTime.Now.AddHours(Hours) // 根据 Hours 设置过期时间
                                    ,
                                    Path = "/" // 确保路径匹配
                                };
                                // 保存 JSON 字符串到 Cookie 中
                                Response.Cookies.Append("SunuerCookie", jsonString, cookieOptions);

                                //更新尝试登陆次数
                                Dal.UpdateLoginAttempts(AdminName, 0);
                                Dal.UpdateLoginLog(AdminName, 0, Tools.Tools.GetUserIp(_httpContextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null.")),0);

                                return Ok(new ApiResponse<int> { code = 0, msg = "成功", count = 1, data = 1 });  // 返回登陆成功
                            }
                            else
                            {
                                Dal.UpdateLoginAttempts(AdminName, LoginAttempts + 1);
                                Dal.UpdateLoginLog(AdminName, 1, Tools.Tools.GetUserIp(_httpContextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null.")), LoginAttempts + 1);

                                return Ok(new ApiResponse<int> { code = 404, msg = "账号或密码错误", count = 0, data = 0 });  // 返回登陆失败

                            }
                        }
                    }
                    else
                    {
                        return Ok(new ApiResponse<int> { code = 404, msg = "用户不存在", count = 0, data = -1 });  // 用户不存在
                    }
                }
                else
                {

                    return Ok(new ApiResponse<int> { code = 404, msg = "验证码错误", count = 0, data = 0 });  // 验证码错误
                }
            }
            else
            {

                return Ok(new ApiResponse<int> { code = 404, msg = "验证码错误", count = 0, data = 0 });  // 验证码错误
            }
        }
        // POST: api/Admin/Add
        //通过 POST 请求添加新，参数接收 int型如果没有该参数 默认是0 
        [HttpPost("Add")]
        public ActionResult<ApiResponse<int>> Add([FromForm] string AdminName
, [FromForm] string AdminNick
, [FromForm] string PassWord
, [FromForm] int RoleID
, [FromForm] int Statues
            )
        {  // 检查参数是否为空
            if (string.IsNullOrWhiteSpace(AdminName) || string.IsNullOrWhiteSpace(AdminNick) || string.IsNullOrWhiteSpace(PassWord))
            {
                return BadRequest(new ApiResponse<int>
                {
                    code = 400,
                    msg = "传入参数不为空",
                    count = 0,
                    data = 0
                });
            }
            Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
            //判断登陆状态
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);
            //判断是否有权限
            if (AdminModel.Roles.IndexOf("|10|") < 0)
            {
                return Ok(new ApiResponse<int> { code = 404, msg = "无权限", count = 0, data = 0 });  // 返回新的ID
            }
            else
            {
                var Model = new Admin.AdminModel
                {
                    // AdminModel 数据模型赋值
                    AdminName = AdminName,
                    AdminNick = AdminNick,
                    PassWord = PassWord,
                    RoleID = RoleID,
                    Statues = Statues,
                    CreateUserID = AdminModel.AdminID
                };

                // 实例化数据访问层对象，负责数据库访问的类
                Admin.AdminDal Dal = new Admin.AdminDal();
                int UserID = Dal.Add(Model);
                if (UserID > 0)
                {
                    return Ok(new ApiResponse<int> { code = 0, msg = "添加成功", count = 1, data = UserID });  // 返回新的ID
                }
                else
                {
                    return Ok(new ApiResponse<int> { code = 404, msg = "添加失败", count = 0, data = 0 });  // 返回新的ID

                }
            }
        }
        // POST: api/Admin/Update
        //通过 Post 请求更新，并返回影响的记录数。
        [HttpPost("Update")]//定义Update方法
        public ActionResult<ApiResponse<int>> Update([FromForm] int AdminID
           , [FromForm] string AdminName
, [FromForm] string AdminNick
, [FromForm] string? PassWord
, [FromForm] int RoleID
, [FromForm] int Statues)
        {
            // 检查参数是否为空
            if (string.IsNullOrWhiteSpace(AdminName) || string.IsNullOrWhiteSpace(AdminNick) || AdminID<=0)
            {
                return BadRequest(new ApiResponse<int>
                {
                    code = 400,
                    msg = "传入参数不为空",
                    count = 0,
                    data = 0
                });
            }
            // 如果 PassWord 为 null 或空，设置为 ""，表示不进行筛选
            if (string.IsNullOrEmpty(PassWord))
            {
                PassWord = ""; // 不传递或传递空字符串时，进行查询时不做名字筛选
            }

            Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
            //判断登陆状态
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);
            //判断是否有权限
            if (AdminModel.Roles.IndexOf("|11|") < 0)
            {
                return Ok(new ApiResponse<int> { code = 404, msg = "无权限", count = 0, data = 0 });  // 返回新的ID
            }
            else
            {
                var Model = new Admin.AdminModel
                {
                    // AdminModel 数据模型赋值
                    AdminID = AdminID,
                    AdminName = AdminName,
                    AdminNick = AdminNick,
                    PassWord = PassWord,
                    RoleID = RoleID,
                    Statues = Statues,
                    UpdateUserID = AdminModel.AdminID
                };
                // 实例化数据访问层对象，负责数据库访问的类
                Admin.AdminDal Dal = new Admin.AdminDal();
                int IsOk = Dal.Update(Model);

                if (IsOk > 0)
                {
                    return Ok(new ApiResponse<int> { code = 0, msg = "更新成功", count = IsOk, data = IsOk });  // 返回成功条数
                }
                else
                {
                    return Ok(new ApiResponse<int> { code = 404, msg = "更新失败", count = 0, data = 0 });  // 返回失败
                }
            }
        }

        // POST: api/Admin/Delete
        //通过 POST 请求根据ID 删除，并返回删除的记录数。
        [HttpPost("Delete")]//定义Delete方法
        public ActionResult<ApiResponse<int>> Delete([FromForm] string AdminIDs)
        {
            // 检查参数是否为空
            if (

             string.IsNullOrWhiteSpace(AdminIDs)
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
            Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
            //判断登陆状态
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);
            //判断是否有权限
            if (AdminModel.Roles.IndexOf("|12|") < 0)
            {
                return Ok(new ApiResponse<int> { code = 404, msg = "无权限", count = 0, data = 0 });  // 返回新的ID
            }
            else
            {
                int UpdateUserID = AdminModel.AdminID;
                // 实例化数据访问层对象，负责数据库访问的类
                Admin.AdminDal Dal = new Admin.AdminDal();
                // 将逗号分隔的字符串转换为整数列表
                var idList = AdminIDs.Split(',')
                             .Where(id => int.TryParse(id.Trim(), out _)) // 确保每个值都是有效数字
                             .Select(id => int.Parse(id.Trim()))
                             .ToList();

                // 初始化变量，用于保存删除的 ID
                var removedIds = new List<int>();
                int IsOk = 0;
                // 遍历每个 ID，查找并删除对应的项目
                foreach (var id in idList)
                {
                    IsOk = Dal.Delete(id, UpdateUserID);
                }
                if (IsOk > 0)
                {
                    return Ok(new ApiResponse<int> { code = 0, msg = "删除成功", count = IsOk, data = IsOk });  // 返回成功条数
                }
                else
                {
                    return Ok(new ApiResponse<int> { code = 404, msg = "删除失败", count = 0, data = 0 });  // 返回失败
                }
            }
        }

        // POST: api/Admin/PassWord修改密码
        //通过 POST 请求根据ID 删除，并返回删除的记录数。
        [HttpPost("PassWord")]//定义Delete方法
        public ActionResult<ApiResponse<int>> PassWord([FromForm] string OldPass,[FromForm] string Pass)
        {
            // 检查参数是否为空
            if (
             string.IsNullOrWhiteSpace(OldPass)
             ||string.IsNullOrWhiteSpace(Pass)
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
            Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
            //判断登陆状态
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);
            //判断是否有权限
            if (AdminModel.Roles.IndexOf("|15|") < 0)
            {
                return Ok(new ApiResponse<int> { code = 404, msg = "无权限", count = 0, data = 0 });  // 返回新的ID
            }
            else
            {
                int AdminID = AdminModel.AdminID;
                // 实例化数据访问层对象，负责数据库访问的类
                Admin.AdminDal Dal = new Admin.AdminDal();
                 int IsOk = Dal.UpdatePassWord(AdminID, OldPass, Pass);
               
                if (IsOk > 0)
                {
                    return Ok(new ApiResponse<int> { code = 0, msg = "更新成功", count = IsOk, data = IsOk });  // 返回成功条数
                }
                else
                {
                    return Ok(new ApiResponse<int> { code = 404, msg = "更新失败,请确认密码是否正确", count = 0, data = 0 });  // 返回失败
                }
            }
        }
        // GET: api/Admin/Get
        // 通过 GET 请求获取分页数据，通过 AdminName、Page 和 PageSize 参数控制分页。
        [HttpGet("Get")]  // 表示当客户端请求 GET 方法时，路径需要匹配 api/Admin/Get
        public ActionResult<ApiResponse<IEnumerable<Admin.AdminModel>>> Get([FromQuery] string? AdminName, [FromQuery] int Page, [FromQuery] int PageSize)
        {

            // 如果 AdminName 为 null 或空，设置为 ""，表示不进行筛选
            if (string.IsNullOrEmpty(AdminName))
            {
                AdminName = ""; // 不传递或传递空字符串时，进行查询时不做名字筛选
            }

            // 检查分页参数是否有效
            if (Page <= 0 || PageSize <= 0)
            {
                return BadRequest(new ApiResponse<int>
                {
                    code = 400,
                    msg = "传入的分页参数无效",
                    count = 0,
                    data = 0
                });
            }

            // 计算开始记录：分页的第一条数据的索引，Page 是当前页码，PageSize 是每页显示的数据条数
            int StartRecord = (Page - 1) * PageSize + 1;

            // 计算每页显示的条数
            int EndRecord = PageSize;

            Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
            //判断登陆状态
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);
            //判断是否有权限
            if (AdminModel.Roles.IndexOf("|9|") < 0)
            {
                return Ok(new ApiResponse<int> { code = 404, msg = "无权限", count = 0, data = 0 });  // 返回新的ID
            }
            else
            {
                // 实例化数据访问层对象，是负责数据库访问的类
                Admin.AdminDal Dal = new Admin.AdminDal();

                // 调用 Get 方法从数据库中获取数据，传入分页参数：AdminName、开始记录、每页条数
                DataTable Datas = Dal.Get(AdminName, StartRecord, EndRecord);

                // 使用工具方法 DataTableToList 将 DataTable 转换为 List<T>，此处 T 是 AdminModel 类的实例
                List<Admin.AdminModel> List = Tools.DataTableToList.DatatableToList<Admin.AdminModel>(Datas);

                DataTable Data = Dal.GetCount(AdminName);
                int Number = Int32.Parse(Data.Rows[0]["Num"].ToString()!);
                // 返回分页的数据，使用统一的 API 响应格式，包含状态码、消息、总条数和数据
                return Ok(new ApiResponse<IEnumerable<Admin.AdminModel>>
                {
                    code = 0,  // 状态码 0 表示成功
                    msg = "成功",  // 返回消息 "成功"
                    count = Number,  // 返回数量，表示当前页面的条数
                    data = List  // 返回分页的集合
                });
            }
        }
    }
}
