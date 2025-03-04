using ApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace SunuerManage.Controllers
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:AdminRoles API
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdminRolesController : ControllerBase
    {
        // 通过依赖注入获取 IHttpContextAccessor- 获取实际IP地址使用
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminRolesController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }



        // POST: api/AdminRoles/Add
        //通过 POST 请求添加新数据，参数接收 int型如果没有该参数 默认是0 
        [HttpPost("Add")]
        public ActionResult<ApiResponse<int>> Add(
                        [FromForm] string RolesTitle,
                        [FromForm] string Powers
            )
        {  // 检查参数是否为空
            if (string.IsNullOrWhiteSpace(RolesTitle)|| string.IsNullOrWhiteSpace(Powers) )
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
            if (AdminModel.Roles.IndexOf("|6|") < 0)
            {
                return Ok(new ApiResponse<int> { code = 404, msg = "无权限", count = 0, data = 0 });  // 返回新数据的ID
            }
            else
            {
                var Model = new AdminRoles.AdminRolesModel
                {
                    //数据模型赋值
                    RolesTitle = RolesTitle,
                    Powers = Powers,
                    CreateUserID = AdminModel.AdminID
                };

                // 实例化数据访问层对象，ExampleDal 是负责数据库访问的类
                AdminRoles.AdminRolesDal Dal = new AdminRoles.AdminRolesDal();
                int UserID = Dal.Add(Model);
                if (UserID > 0)
                {
                    return Ok(new ApiResponse<int> { code = 0, msg = "添加成功", count = 1, data = UserID });
                }
                else
                {
                    return Ok(new ApiResponse<int> { code = 404, msg = "添加失败", count = 0, data = 0 });

                }
            }
        }
        // POST: api/AdminRoles/Update
        //通过 Post 请求更新数据密码，并返回影响的记录数。
        [HttpPost("Update")]//定义Update方法
        public ActionResult<ApiResponse<int>> Update([FromForm] int RoleID,
                        [FromForm] string RolesTitle,
                        [FromForm] string Powers
            )
        {

            // 检查参数是否为空
            if (
             string.IsNullOrWhiteSpace(RolesTitle)
             ||string.IsNullOrWhiteSpace(Powers)
             || RoleID <= 0
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
            if (AdminModel.Roles.IndexOf("|7|") < 0)
            {
                return Ok(new ApiResponse<int> { code = 404, msg = "无权限", count = 0, data = 0 });  // 返回新数据的ID
            }
            else
            {
                var Model = new AdminRoles.AdminRolesModel
                {
                    //数据模型赋值
                    RoleID = RoleID,
                    RolesTitle = RolesTitle,
                    Powers = Powers,
                    UpdateUserID = AdminModel.AdminID
                };
                // 实例化数据访问层对象，AdminDal 是负责数据库访问的类
                AdminRoles.AdminRolesDal Dal = new AdminRoles.AdminRolesDal();
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

        // POST: api/AdminRoles/Delete
        //通过 POST 请求根据ID 删除，并返回删除的记录数。
        [HttpPost("Delete")]//定义Delete方法
        public ActionResult<ApiResponse<int>> Delete([FromForm] string RoleIDs)
        {
            // 检查参数是否为空
            if (
             string.IsNullOrWhiteSpace(RoleIDs) 
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
            if (AdminModel.Roles.IndexOf("|8|") < 0)
            {
                return Ok(new ApiResponse<int> { code = 404, msg = "无权限", count = 0, data = 0 });  // 返回新数据的ID
            }
            else
            {
                int UpdateUserID = AdminModel.AdminID;
                // 实例化数据访问层对象，ExampleDal 是负责数据库访问的类
                AdminRoles.AdminRolesDal Dal = new AdminRoles.AdminRolesDal();
                // 将逗号分隔的字符串转换为整数列表
                var idList = RoleIDs.Split(',')
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
        // Get: api/AdminRoles/Get
        // 通过 Get。
        [HttpGet("Get")]  // 表示当客户端请求 GET 方法时，路径需要匹配 api/AdminRoles/Get
        public ActionResult<ApiResponse<IEnumerable<AdminRoles.AdminRolesModel>>> Get([FromQuery] string? RolesTitle, [FromQuery] int Page, [FromQuery] int PageSize)
        {

            // 如果 RolesTitle 为 null 或空，设置为 ""，表示不进行筛选
            if (string.IsNullOrEmpty(RolesTitle))
            {
                RolesTitle = ""; // 不传递或传递空字符串时，进行查询时不做名字筛选
            }

            // 检查分页参数是否有效
            if (Page <= 0 || PageSize <= 0)
            {
                return BadRequest(new ApiResponse<int>
                {
                    code = 400,
                    msg = "传入的参数无效",
                    count = 0,
                    data = 0
                });
            }

            // 计算开始记录：分页的第一条数据的索引，Page 是当前页码，PageSize 是每页显示的数据条数
            int StartRecord = (Page - 1) * PageSize + 1;

            // 计算每页显示的条数
            int EndRecord = PageSize;
            // 计算每页显示的条数

            // 实例化数据访问层对象，AdminDal 是负责数据库访问的类
            AdminRoles.AdminRolesDal Dal = new AdminRoles.AdminRolesDal();

            // 调用 Get 方法从数据库中获取数据
            DataTable Datas = Dal.Get(RolesTitle,StartRecord, EndRecord);

            // 使用工具方法 DataTableToList 将 DataTable 转换为 List<T>
            List<AdminRoles.AdminRolesModel> List = Tools.DataTableToList.DatatableToList<AdminRoles.AdminRolesModel>(Datas);

            DataTable Data = Dal.GetCount(RolesTitle);
            int Number = Int32.Parse(Data.Rows[0]["Num"].ToString()!);
            // 返回分页的数据，使用统一的 API 响应格式，包含状态码、消息、总条数和数据
            return Ok(new ApiResponse<IEnumerable<AdminRoles.AdminRolesModel>>
            {
                code = 0,  // 状态码 0 表示成功
                msg = "成功",  // 返回消息 "成功"
                count = Number,  // 返回数量，表示当前页面的条数
                data = List  // 返回分页的集合
            });
        }
    }
}
