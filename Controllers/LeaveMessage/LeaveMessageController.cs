using ApiResponse;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Web.Controllers.LeaveMessages
{
    /// <summary>
    /// Project:Sunuer Manage
    /// Description:LeaveMessage API
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveMessageController : ControllerBase
    {
        // 通过依赖注入获取 IHttpContextAccessor- 获取实际IP地址使用
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LeaveMessageController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        // POST: api/LeaveMessage/Add
        //通过 POST 请求添加新数据，参数接收 int型如果没有该参数 默认是0
        [HttpPost("Add")]
        public ActionResult<ApiResponse<int>> Add(
       
        [FromForm] string Title
        , [FromForm] string? Phone
        , [FromForm] string? UserName
        , [FromForm] string Content
        , [FromForm] string? Email
        
        )
        {
            //检查参数是否为空
            if (
           
             string.IsNullOrWhiteSpace(Title)
            
            || string.IsNullOrWhiteSpace(Content)
         
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
            //后端参数判断是否为空
            //Phone校验
            if (string.IsNullOrWhiteSpace(Phone))
            {
                Phone = "";
            }
            //UserName校验
            if (string.IsNullOrWhiteSpace(UserName))
            {
                UserName = "";
            }
            //Email校验
            if (string.IsNullOrWhiteSpace(Email))
            {
                Email = "";
            }
           
                var Model = new LeaveMessage.LeaveMessageModel
                {
                    //数据模型赋值
                    CreateUserID = 0,// 创建人ID默认0
                    Title = Title,// 标题 
                    Phone = Phone,// 手机号 
                    UserName = UserName,// 姓名 
                    Content = Content,// 留言内容 
                    Email = Email,// 邮箱 

                };
                // 实例化数据访问层对象，负责数据库访问的类
                LeaveMessage.LeaveMessageDal Dal = new LeaveMessage.LeaveMessageDal();
                int ID = Dal.Add(Model);
                if (ID > 0)
                {
                    LeaveMessageSet.LeaveMessageSetDal Dal2 = new LeaveMessageSet.LeaveMessageSetDal();
                    LeaveMessageSet.LeaveMessageSetModel Model2 = Dal2.GetModel(1);
                    // 留言添加成功之后再发送邮件通知管理员
                    bindEmail(Model, Model2.SystemEmail);



                    return Ok(new ApiResponse<int>
                    {
                        code = 0,
                        msg = "添加成功",
                        count = 1,
                        data = ID
                    });
                }
                else
                {
                    return Ok(new ApiResponse<int>
                    {
                        code = 404,
                        msg = "添加失败",
                        count = 0,
                        data = 0
                    });
                }
           
        }

        // Get: api/LeaveMessage/Get
        //通过 GET 请求获取分页数据，通过 Title、Page 和 PageSize 参数控制分页。
        [HttpGet("Get")]
        public ActionResult<ApiResponse<int>> Get(
        [FromQuery] string? Title, [FromQuery] string? UserName,[FromQuery] int? AuditStatus, [FromQuery] int Page, [FromQuery] int PageSize
        )
        {
            //如果 Title 为 null 或空，设置为 ""，表示不进行筛选 
            if (string.IsNullOrEmpty(Title))
            {
                Title = ""; //不传递或传递空字符串时，进行查询时不做筛选
            }
            //如果 UserName 为 null 或空，设置为 ""，表示不进行筛选 
            if (string.IsNullOrEmpty(UserName))
            {
                UserName = ""; //不传递或传递空字符串时，进行查询时不做筛选
            }
            // 如果 AuditStatus 为 null 或空，设置为 ""，表示不进行筛选
            if (string.IsNullOrEmpty(AuditStatus.ToString()) || AuditStatus < 0)
            {
                AuditStatus = -1; // 不传递或传递空字符串时，进行查询时不做名字筛选
            }
            // 检查分页参数是否有效
            if (
            Page <= 0 || PageSize <= 0
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
            //计算开始记录：分页的第一条数据的索引，Page 是当前页码，PageSize 是每页显示的数据条数 
            int StartRecord = (Page - 1) * PageSize + 1;
            //计算每页显示的条数 
            int EndRecord = PageSize;
            Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
            //判断登陆状态
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);
            //判断是否有权限
            if (AdminModel.Roles.IndexOf("|402|") < 0)
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
                int UpdateUserID = AdminModel.AdminID;
                // 实例化数据访问层对象，负责数据库访问的类
                LeaveMessage.LeaveMessageDal Dal = new LeaveMessage.LeaveMessageDal();
                //调用 Get 方法从数据库中获取数据，传入分页参数：Title、开始记录、每页条数
                DataTable Datas = Dal.Get(StartRecord, EndRecord,Title, UserName, (int)AuditStatus);
                // 使用工具方法 DataTableToList 将 DataTable 转换为 List<T>，此处 T 是 AdminModel 类的实例
                List<LeaveMessage.LeaveMessageModel> List = Tools.DataTableToList.DatatableToList<LeaveMessage.LeaveMessageModel>(Datas);
                DataTable Data = Dal.GetCount(Title, UserName, (int)AuditStatus);
                int Number = Int32.Parse(Data.Rows[0]["Num"].ToString()!);
                // 返回分页的数据，使用统一的 API 响应格式，包含状态码、消息、总条数和数据集合
                return Ok(new ApiResponse<IEnumerable<LeaveMessage.LeaveMessageModel>>
                {
                    code = 0,
                    msg = "成功",
                    count = Number,
                    data = List
                });
            }
        }

        // POST: api/LeaveMessage/Audit
        // 通过 POST 请求根据ID 审核，并返回审核的记录数。
        [HttpPost("Audit")]
        public ActionResult<ApiResponse<int>> Audit(
        [FromForm] string LeaveMessageIDs,
        [FromForm] int AuditStatus,
        [FromForm] string? AuditDesn
        )
        {
            // 检查参数是否为空
            if (
            string.IsNullOrWhiteSpace(LeaveMessageIDs)
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

            if (string.IsNullOrEmpty(AuditDesn))
            {
                AuditDesn = "";
            }

            Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
            //判断登陆状态
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);
            //判断是否有权限
            if (AdminModel.Roles.IndexOf("|403|") < 0)
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
                int UpdateUserID = AdminModel.AdminID;
                // 实例化数据访问层对象，负责数据库访问的类
                LeaveMessage.LeaveMessageDal Dal = new LeaveMessage.LeaveMessageDal();
                //将逗号分隔的字符串转换为整数列表
                var idList = LeaveMessageIDs.Split(',').Where(id => int.TryParse(id.Trim(), out _)).Select(id => int.Parse(id.Trim())).ToList();
                //初始化变量，用于保存删除的 ID
                var removedIds = new List<int>();
                int ID = 0;
                // 遍历每个 ID，查找并删除对应的项目
                foreach (var id in idList)
                {
                    // 循环审核数据
                    ID = Dal.Audit(id, UpdateUserID, AuditStatus, AuditDesn);
                }
                if (ID > 0)
                {
                    return Ok(new ApiResponse<int>
                    {
                        code = 0,
                        msg = "审核成功",
                        count = ID,
                        data = ID
                    });
                }
                else
                {
                    return Ok(new ApiResponse<int>
                    {
                        code = 404,
                        msg = "审核失败",
                        count = 0,
                        data = 0
                    });
                }
            }
        }
        // POST: api/LeaveMessage/Delete
        //通过 POST 请求根据ID 删除，并返回删除的记录数。
        [HttpPost("Delete")]
        public ActionResult<ApiResponse<int>> Delete(
        [FromForm] string LeaveMessageIDs
        )
        {
            // 检查参数是否为空
            if (
            string.IsNullOrWhiteSpace(LeaveMessageIDs)
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
            if (AdminModel.Roles.IndexOf("|404|") < 0)
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
                int UpdateUserID = AdminModel.AdminID;
                // 实例化数据访问层对象，负责数据库访问的类
                LeaveMessage.LeaveMessageDal Dal = new LeaveMessage.LeaveMessageDal();
                //将逗号分隔的字符串转换为整数列表
                var idList = LeaveMessageIDs.Split(',').Where(id => int.TryParse(id.Trim(), out _)).Select(id => int.Parse(id.Trim())).ToList();
                //初始化变量，用于保存删除的 ID
                var removedIds = new List<int>();
                int ID = 0;
                // 遍历每个 ID，查找并删除对应的项目
                foreach (var id in idList)
                {
                    ID = Dal.Delete(id, UpdateUserID);
                }
                if (ID > 0)
                {
                    return Ok(new ApiResponse<int>
                    {
                        code = 0,
                        msg = "删除成功",
                        count = ID,
                        data = ID
                    });
                }
                else
                {
                    return Ok(new ApiResponse<int>
                    {
                        code = 404,
                        msg = "删除失败",
                        count = 0,
                        data = 0
                    });
                }
            }
        }

        // ************ 发送邮件-Star ************//
        private static bool RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            //为了通过证书验证，总是返回true
            return true;
        }
        // 发送邮件
        public void bindEmail(LeaveMessage.LeaveMessageModel model, string receiveEmail)
        {
            string Subject = "Sunner Manage--留言提醒";
            string Content = $"收到一条新留言:<br/>留言人:{model.UserName}<br/>手机号:{model.Phone}<br/>邮箱:{model.Email}<br/>留言标题:{model.Title}<br/>留言内容:{model.Content}";

            string Receiver = receiveEmail;

            SmtpClient client = new SmtpClient("邮件的smtp", 587);
            MailMessage msg = new MailMessage("发送邮箱", Receiver, Subject, Content);
            msg.IsBodyHtml = true;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
            System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("发送邮箱", "发送邮箱的发送密码");
            client.Credentials = basicAuthenticationInfo;
            client.Send(msg);

        }
        // ************ 发送邮件-End ************//

    }
}