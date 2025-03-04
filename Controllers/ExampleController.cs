using ApiResponse;
using Example;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Numerics;
namespace SunuerManage.Controllers
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:ADO.NET操作数据库
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        // 通过依赖注入获取 IHttpContextAccessor
        public ExampleController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/Users/paged
        // 通过 GET 请求获取分页用户数据，通过 pageNumber 和 pageSize 参数控制分页。
        [HttpGet("Get")]  // 表示当客户端请求 GET 方法时，路径需要匹配 api/Users/Get
        public  ActionResult<ApiResponse<IEnumerable<Example.ExampleModel>>> Get(string UserName, string Phone, int pageNumber, int pageSize)
        {
            // 计算开始记录：分页的第一条数据的索引，pageNumber 是当前页码，pageSize 是每页显示的数据条数
            int StartRecord = (pageNumber - 1) * pageSize + 1;

            // 计算每页显示的条数
            int EndRecord = pageSize;

            // 实例化数据访问层对象，ExampleDal 是负责数据库访问的类
            Example.ExampleDal Dal = new Example.ExampleDal();

            // 调用 GetSQL 方法从数据库中获取用户数据，传入分页参数：UserName、Phone、开始记录、每页条数
            DataTable Datas = Dal.GetSQL(UserName, Phone, StartRecord, EndRecord);

            // 使用工具方法 DataTableToList 将 DataTable 转换为 List<T>，此处 T 是 ExampleModel 类的实例
            List<Example.ExampleModel> List = Tools.DataTableToList.DatatableToList<Example.ExampleModel>(Datas);

            // 返回分页的用户数据，使用统一的 API 响应格式，包含状态码、消息、总条数和数据
            return Ok(new ApiResponse<IEnumerable<Example.ExampleModel>>
            {
                code = 0,  // 状态码 0 表示成功
                msg = "成功",  // 返回消息 "成功"
                count = List.Count,  // 返回用户数量，表示当前页面的条数
                data = List  // 返回分页的用户集合
            });
        }

        // GET: api/Users/GetById?id=
        //通过 GET 请求获取指定 ID 的用户信息。
        [HttpGet("GetById")]
        public ActionResult<ApiResponse<Example.ExampleModel>> GetById(int id)//id为需要的传参
        {
            // 实例化数据访问层对象，ExampleDal 是负责数据库访问的类
            Example.ExampleDal Dal = new Example.ExampleDal();
            Example.ExampleModel Model = new Example.ExampleModel();

            // 调用 GetSQL 方法从数据库中获取用户数据，传入分页参数：UserName、Phone、开始记录、每页条数
            Model = Dal.GetModelSQL(id);
            if (Model == null)
            {
                // 如果用户未找到，返回404错误
                return Ok(new ApiResponse<Example.ExampleModel>
                {
                    code = 404,  // 错误码 404 表示未找到
                    msg = "失败",
                    count = 0,   // 没有找到用户
                    data = null  // 数据为空
                });
            }

            // 如果用户找到，返回找到的用户信息
            return Ok(new ApiResponse<Example.ExampleModel>
            {
                code = 0,    // 0 表示成功
                msg = "成功",
                count = 1,   // 找到一个用户
                data = Model  // 返回用户信息
            });
        }
       
        // POST: api/Users/Add
        //通过 POST 请求添加新用户，并返回新用户的 UserID。
        [HttpPost("Add")]
        public ActionResult<ApiResponse<int>> Add([FromBody] AddUserDto userDto)
        {
            var newExample = new Example.ExampleModel
            {// 对UsersModel 数据模型赋值
                UserName = userDto.UserName,
                PassWord = userDto.PassWord,
                Phone = userDto.Phone,
               IP = Tools.Tools.GetUserIp(_httpContextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null.")),// 调用 GetUserIp 方法，传递当前 HttpContext
            };

            // 实例化数据访问层对象，ExampleDal 是负责数据库访问的类
            Example.ExampleDal Dal = new Example.ExampleDal();
            int UserID = Dal.AddSQL(newExample);
            if (UserID > 0) {
                return Ok(new ApiResponse<int> { code = 0, msg = "成功", count = 1, data = UserID });  // 返回新用户的 UserID
            }
            else
            {
                return Ok(new ApiResponse<int> { code = 404, msg = "失败", count = 0, data = 0 });  // 返回新用户的 UserID

            }
        }
        //定义接收参数模型
        public class AddUserDto
        {
            [Required]
            public string UserName { get; set; } = "";

            [Required]
            public string PassWord { get; set; } = "";

            [Required]
            public string Phone { get; set; } = "";
        }
        // POST: api/Users/AddFrom
        //通过 POST 请求添加新用户，参数接收
        [HttpPost("AddFrom")]
        public ActionResult<ApiResponse<int>> AddFrom([FromForm] string UserName, [FromForm] string PassWord, [FromForm] string Phone, [FromForm] int Sorts)
        {  // 检查参数是否为空
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(PassWord) || string.IsNullOrWhiteSpace(Phone) || Sorts < 0)
            {
                return BadRequest(new ApiResponse<int>
                {
                    code = 400,
                    msg = "传入参数不为空，并且 UserID 必须为正整数",
                    count = 0,
                    data = 0
                });
            }
            var newExample = new Example.ExampleModel
            {// 对UsersModel 数据模型赋值
                UserName = UserName,
                PassWord = PassWord,
                Phone = Phone,
                IP = Tools.Tools.GetUserIp(_httpContextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null.")),// 调用 GetUserIp 方法，传递当前 HttpContext
            };

            // 实例化数据访问层对象，ExampleDal 是负责数据库访问的类
            Example.ExampleDal Dal = new Example.ExampleDal();
            int UserID = Dal.AddSQL(newExample);
            if (UserID > 0)
            {
                return Ok(new ApiResponse<int> { code = 0, msg = "成功", count = 1, data = UserID });  // 返回新用户的 UserID
            }
            else
            {
                return Ok(new ApiResponse<int> { code = 404, msg = "失败", count = 0, data = 0 });  // 返回新用户的 UserID

            }
        }
        // POST: api/Users/Update
        //通过 Post 请求更新用户密码，并返回影响的记录数。
        [HttpPost("Update")]//定义Update方法
        public ActionResult<ApiResponse<int>> Update([FromBody] UpdateUserDto userDto)
        {
            var newExample = new Example.ExampleModel
            {// 对UsersModel 数据模型赋值
                PassWord = userDto.PassWord,
                Phone = userDto.Phone,
            };
            // 实例化数据访问层对象，ExampleDal 是负责数据库访问的类
            Example.ExampleDal Dal = new Example.ExampleDal();
            int IsOk = Dal.UpdateSQL(newExample);
            
            if (IsOk > 0)
            {
                return Ok(new ApiResponse<int> { code = 0, msg = "成功", count = IsOk, data = IsOk });  // 返回成功条数
            }
            else
            {
                return Ok(new ApiResponse<int> { code = 404, msg = "更新失败", count = 0, data = 0 });  // 返回失败
            }
        }
        //定义接收参数模型
        public class UpdateUserDto
        {

            [Required]
            public string PassWord { get; set; } = "";

            [Required]
            public string Phone { get; set; } = "";
        }
        // POST: api/Users/Delete
        //通过 POST 请求根据用户 ID 删除用户，并返回删除的记录数。
        [HttpPost("Delete")]//定义Delete方法
        public ActionResult<ApiResponse<int>> Delete([FromBody] DeleteUserDto userDto)
        {
            // 实例化数据访问层对象，ExampleDal 是负责数据库访问的类
            Example.ExampleDal Dal = new Example.ExampleDal();
            int IsOk = Dal.Delete(userDto.id);
            if (IsOk > 0)
            {
                return Ok(new ApiResponse<int> { code = 0, msg = "成功", count = IsOk, data = IsOk });  // 返回成功条数
            }
            else
            {
                return Ok(new ApiResponse<int> { code = 404, msg = "删除失败", count = 0, data = 0 });  // 返回失败
            }
        }
        //定义接收参数模型
        public class DeleteUserDto
        {

            [Required]
            public int id { get; set; }

        }
    }
}
