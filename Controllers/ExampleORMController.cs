using ApiResponse;
using Example;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using SunuerManage.Data;
namespace SunuerManage.Controllers
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:ORM操作数据库
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleORMController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // 构造函数，注入数据库上下文
        public ExampleORMController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/ExampleORM
        // 1. GET 请求：获取所有用户
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Example.ExampleModel>>>> GetUsers()
        {
            var users = await _context.Example.ToListAsync();  // ToListAsync()：异步查询Example中的所有用户，并将其转为列表。

            // ApiResponse返回统一格式的 API 响应，T 是 IEnumerable<Example.ExampleModel>，IEnumerable<T> 是 .NET 中的一个接口，它定义了一个用于遍历集合的基础接口
            return Ok(new ApiResponse<IEnumerable<Example.ExampleModel>>
            {
                code = 0,  // 0 表示成功
                msg = "成功",
                count = users.Count,  // 返回用户数量
                data = users  // 返回用户列表
            });
        }

        // GET: api/ExampleORM/paged
        //通过 GET 请求获取分页用户数据，通过 pageNumber 和 pageSize 参数控制分页。
        [HttpGet("paged")]//表示当客户端请求 GET 方法时，路径需要匹配api/Users/paged
        public async Task<ActionResult<ApiResponse<IEnumerable<Example.ExampleModel>>>> GetUsers(int pageNumber, int pageSize)
        {
            var totalRecords = await _context.Example.CountAsync();  // 获取Users用户总数
            var users = await _context.Example
                .OrderBy(u => u.UserID)  // 按 UserID 排序
                .Skip((pageNumber - 1) * pageSize)  // 跳过前 (页数-1) * 每页条数
                .Take(pageSize)  // 获取 pageSize 条记录 Skip() 和 Take() 用于实现分页功能。
                .ToListAsync();  // 获取分页数据

            return Ok(new ApiResponse<IEnumerable<Example.ExampleModel>>
            {
                code = 0,
                msg = "成功",
                count = users.Count,  // 返回用户数量
                data = users  // 返回分页的用户集合
            });
        }

        // GET: api/ExampleORM/GetUserById?id=
        //通过 GET 请求获取指定 ID 的用户信息。
        [HttpGet("GetUserById")]
        public async Task<ActionResult<ApiResponse<Example.ExampleModel>>> GetUserById(int id)//id为需要的传参
        {
            var user = await _context.Example.FirstOrDefaultAsync(u => u.UserID == id);  // 根据用户ID查找用户

            if (user == null)
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
                data = user  // 返回用户信息
            });
        }
        // POST: api/ExampleORM/Add
        //通过 POST 请求添加新用户，并返回新用户的 UserID。
        [HttpPost("Add")]
        public async Task<ActionResult<ApiResponse<int>>> AddUser([FromBody] AddUserDto userDto)
        {
            var newUser = new Example.ExampleModel
            {// 对UsersModel 数据模型赋值
                UserName = userDto.UserName,
                PassWord = userDto.Password,
                Phone = userDto.Phone,
                CreateDate = DateTime.Now

            };

            _context.Example.Add(newUser);  // 添加新用户到数据库
            await _context.SaveChangesAsync();  // 保存更改

            return Ok(new ApiResponse<int> { code = 0, msg = "成功", count = 1, data = newUser.UserID });  // 返回新用户的 UserID
        }
        //定义接收参数模型
        public class AddUserDto
        {
            [Required]
            public string UserName { get; set; }

            [Required]
            public string Password { get; set; }

            [Required]
            public string Phone { get; set; }
        }
        // POST: api/ExampleORM/Update
        //通过 Post 请求更新用户密码，并返回影响的记录数。
        [HttpPost("Update")]//定义Update方法
        public async Task<ActionResult<ApiResponse<int>>> Update([FromBody] PutUserDto userDto)
        {
            var user = await _context.Example.FirstOrDefaultAsync(u => u.Phone == userDto.Phone);  // 根据手机号查找用户
            if (user == null)
            {
                return Ok(new ApiResponse<int> { code = 404, msg = "失败", count = 0, data = 0 });  // 用户未找到
            }

            user.PassWord = userDto.PassWord;  // 更新密码
            var affectedRows = await _context.SaveChangesAsync();  // 保存更改并返回受影响的行数
            if (affectedRows > 0)
            {
                return Ok(new ApiResponse<int> { code = 0, msg = "成功", count = affectedRows, data = affectedRows });  // 返回成功条数
            }
            else
            {
                return Ok(new ApiResponse<int> { code = 500, msg = "更新失败", count = 0, data = 0 });  // 返回失败
            }
        }
        //定义接收参数模型
        public class PutUserDto
        {

            [Required]
            public string PassWord { get; set; }

            [Required]
            public string Phone { get; set; }
        }
        // POST: api/ExampleORM/Delete
        //通过 POST 请求根据用户 ID 删除用户，并返回删除的记录数。
        [HttpPost("Delete")]//定义Delete方法
        public async Task<ActionResult<ApiResponse<int>>> Delete([FromBody] DeleteUserDto userDto)
        {
            var user = await _context.Example.FindAsync(userDto.id);  // 根据用户ID查找用户

            if (user == null)
            {
                // 如果用户未找到，返回404错误
                return Ok(new ApiResponse<int>
                {
                    code = 404,  // 错误码 404 表示未找到
                    msg = "失败",
                    count = 0,   // 没有找到用户
                    data = 0     // 数据为 0，表示没有删除的用户
                });
            }

            _context.Example.Remove(user);  // 删除用户
            var affectedRows = await _context.SaveChangesAsync();  // 保存更改并返回受影响的行数

            // 返回成功响应，表明用户已删除
            return Ok(new ApiResponse<int>
            {
                code = 0,     // 0 表示成功
                msg = "成功",
                count = affectedRows,  // 删除的记录数
                data = affectedRows    // 返回删除的记录数
            });

        }
        //定义接收参数模型
        public class DeleteUserDto
        {

            [Required]
            public int id { get; set; }

        }
    }
}
