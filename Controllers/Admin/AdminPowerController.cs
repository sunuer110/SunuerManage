using ApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace SunuerManage.Controllers
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:AdminPower API
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdminPowerController : ControllerBase
    {
        // 通过依赖注入获取 IHttpContextAccessor- 获取实际IP地址使用
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminPowerController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }



        // POST: api/AdminPower/Add
        //通过 POST 请求添加，参数接收 int型如果没有该参数 默认是0 
        [HttpPost("Add")]
        public ActionResult<ApiResponse<int>> Add([FromForm] int ParentID
, [FromForm] string PowerTitle
            )
        {  // 检查参数是否为空
            if (string.IsNullOrWhiteSpace(PowerTitle) || ParentID < 0)
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
            if (AdminModel.Roles.IndexOf("|2|") < 0)
            {
                return Ok(new ApiResponse<int> { code = 404, msg = "无权限", count = 0, data = 0 });  // 返回新的ID
            }
            else
            {
                var Model = new AdminPower.AdminPowerModel
                {
                    // AdminModel 数据模型赋值
                    ParentID = ParentID,
                    PowerTitle = PowerTitle,
                    CreateUserID = AdminModel.AdminID
                };

                // 实例化数据访问层对象，负责数据库访问的类
                AdminPower.AdminPowerDal Dal = new AdminPower.AdminPowerDal();
                int IsOk = Dal.Add(Model);
                if (IsOk > 0)
                {
                    return Ok(new ApiResponse<int> { code = 0, msg = "添加成功", count = 1, data = IsOk });  // 返回新的ID
                }
                else
                {
                    return Ok(new ApiResponse<int> { code = 404, msg = "添加失败", count = 0, data = 0 });  // 返回新的ID

                }
            }
        }
        // POST: api/AdminPower/Update
        //通过 Post 请求更新，并返回影响的记录数。
        [HttpPost("Update")]//定义Update方法
        public ActionResult<ApiResponse<int>> Update([FromForm] int PowerID
, [FromForm] string PowerTitle
            )
        {

            // 检查参数是否为空
            if (
             string.IsNullOrWhiteSpace(PowerTitle)
             || PowerID <= 0
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
            if (AdminModel.Roles.IndexOf("|3|") < 0)
            {
                return Ok(new ApiResponse<int> { code = 404, msg = "无权限", count = 0, data = 0 });  // 返回新的ID
            }
            else
            {
                var Model = new AdminPower.AdminPowerModel
                {
                    // AdminModel 数据模型赋值
                    PowerID = PowerID,
                    PowerTitle = PowerTitle,
                    UpdateUserID = AdminModel.AdminID
                };
                // 实例化数据访问层对象，负责数据库访问的类
                AdminPower.AdminPowerDal Dal = new AdminPower.AdminPowerDal();
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

        // POST: api/AdminPower/Delete
        //通过 POST 请求根据ID 删除，并返回删除的记录数。
        [HttpPost("Delete")]//定义Delete方法
        public ActionResult<ApiResponse<int>> Delete([FromForm] int PowerID)
        {
            // 检查参数是否为空
            if (PowerID <= 0)
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
            if (AdminModel.Roles.IndexOf("|4|") < 0)
            {
                return Ok(new ApiResponse<int> { code = 404, msg = "无权限", count = 0, data = 0 });  // 返回新的ID
            }
            else
            {
                int UpdateUserID = AdminModel.AdminID;
                // 实例化数据访问层对象，负责数据库访问的类
                AdminPower.AdminPowerDal Dal = new AdminPower.AdminPowerDal();
                int IsOk = Dal.Delete(PowerID, UpdateUserID);
                if (IsOk > 0)
                {
                    return Ok(new ApiResponse<int> { code = 0, msg = "成功", count = IsOk, data = IsOk });  // 返回成功条数
                }
                else
                {
                    return Ok(new ApiResponse<int> { code = 404, msg = "删除失败", count = 0, data = 0 });  // 返回失败
                }
            }
        }
        // Post: api/AdminPower/Get
        // 通过 Post。
        [HttpPost("Get")]  // 表示当客户端请求 GET 方法时，路径需要匹配 api/AdminPower/Get
        public ActionResult<ApiResponse<IEnumerable<AdminPower.AdminPowerModel>>> Get([FromForm] int ParentID)
        {
           

            // 检查分页参数是否有效
            if (ParentID<0)
            {
                return BadRequest(new ApiResponse<int>
                {
                    code = 400,
                    msg = "传入的参数无效",
                    count = 0,
                    data = 0
                });
            }


            // 实例化数据访问层对象，负责数据库访问的类
            AdminPower.AdminPowerDal Dal = new AdminPower.AdminPowerDal();

            // 调用 Get 方法从数据库中获取数据
            DataTable Datas = Dal.Get(ParentID);

            // 使用工具方法 DataTableToList 将 DataTable 转换为 List<T>
            List<AdminPower.AdminPowerModel> List = Tools.DataTableToList.DatatableToList<AdminPower.AdminPowerModel>(Datas);

            // 返回分页的数据，使用统一的 API 响应格式，包含状态码、消息、总条数和数据
            return Ok(new ApiResponse<IEnumerable<AdminPower.AdminPowerModel>>
            {
                code = 0,  // 状态码 0 表示成功
                msg = "成功",  // 返回消息 "成功"
                count = List.Count,  // 返回数量，表示当前页面的条数
                data = List  // 返回分页的集合
            });
        }
        // Post: api/AdminPower/Get
        // 通过 Post。
        [HttpPost("GetAll")]  // 表示当客户端请求 GET 方法时，路径需要匹配 api/AdminPower/Get
        public ActionResult<ApiResponse<IEnumerable<AdminPower.AdminPowerModel>>> GetAll()
        {


            // 实例化数据访问层对象，负责数据库访问的类
            AdminPower.AdminPowerDal Dal = new AdminPower.AdminPowerDal();

            // 调用 Get 方法从数据库中获取数据
            DataTable Datas = Dal.Get();

            List<AdminPower.AdminPowerModel> List = Tools.DataTableToList.DatatableToList<AdminPower.AdminPowerModel>(Datas);

            // 返回分页的数据，使用统一的 API 响应格式，包含状态码、消息、总条数和数据
            return Ok(new ApiResponse<IEnumerable<AdminPower.AdminPowerModel>>
            {
                code = 0,  // 状态码 0 表示成功
                msg = "成功",  // 返回消息 "成功"
                count = List.Count,  // 返回数量，表示当前页面的条数
                data = List  // 返回分页的集合
            });
        }
    }
}
