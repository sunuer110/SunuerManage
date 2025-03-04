using ApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace SunuerManage.Controllers
{
    /// <summary>
    /// Project:Sunuer Manage
    /// Description:ArticleCategory API
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleCategoryController : ControllerBase
    {
        // 通过依赖注入获取 IHttpContextAccessor- 获取实际IP地址使用
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ArticleCategoryController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }



        // POST: api/ArticleCategory/Add
        //通过 POST 请求添加新数据，参数接收 int型如果没有该参数 默认是0 
        [HttpPost("Add")]
        public ActionResult<ApiResponse<int>> Add(
              [FromForm] int ParentID
            , [FromForm] int Statues
            , [FromForm] string BigTitle
            , [FromForm] string? KeyTitle
            , [FromForm] string? KeyWord
            , [FromForm] string? KeyDesn
            , [FromForm] string? Images
            , [FromForm] string? ImagesPhone
            , [FromForm] string? SiteUrl
            , [FromForm] int Sorts
            )
        {  // 检查参数是否为空
            if (string.IsNullOrWhiteSpace(BigTitle) || ParentID < 0 || Sorts < 0)
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

            //BigTitle校验
            if (string.IsNullOrWhiteSpace(BigTitle))
            {
                BigTitle = "";
            }
            //KeyTitle校验
            if (string.IsNullOrWhiteSpace(KeyTitle))
            {
                KeyTitle = "";
            }
            //KeyWord校验
            if (string.IsNullOrWhiteSpace(KeyWord))
            {
                KeyWord = "";
            }
            //KeyDesn校验
            if (string.IsNullOrWhiteSpace(KeyDesn))
            {
                KeyDesn = "";
            }
            //Images校验
            if (string.IsNullOrWhiteSpace(Images))
            {
                Images = "";
            }
            
            //ImagesPhone校验
            if (string.IsNullOrWhiteSpace(ImagesPhone))
            {
                ImagesPhone = "";
            }
            //SiteUrl跳转链接
            if (string.IsNullOrWhiteSpace(SiteUrl))
            {
                SiteUrl = "";
            }
            Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
            //判断登陆状态
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);
            //判断是否有权限
            if (AdminModel.Roles.IndexOf("|18|") < 0)
            {
                return Ok(new ApiResponse<int> { code = 404, msg = "无权限", count = 0, data = 0 });  // 返回新数据的ID
            }
            else
            {
                var Model = new ArticleCategory.ArticleCategoryModel
                {
                    //数据模型赋值
                    ParentID = ParentID,// 父级ID 
                    Statues = Statues,// 导航0是1否 
                    BigTitle = BigTitle,// 分类名称 
                    KeyTitle = KeyTitle,// 优化标题 
                    KeyWord = KeyWord,// 关键词 
                    KeyDesn = KeyDesn,// 描述 
                    Images = Images,// 图片 
                    ImagesPhone = ImagesPhone,// 图片-手机 
                    SiteUrl = SiteUrl,// 跳转链接
                    Sorts = Sorts,// 排序大号在前 
                    CreateUserID = AdminModel.AdminID
                };

                // 实例化数据访问层对象，负责数据库访问的类
                ArticleCategory.ArticleCategoryDal Dal = new ArticleCategory.ArticleCategoryDal();
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
        // POST: api/ArticleCategory/Update
        //通过 Post 请求更新数据密码，并返回影响的记录数。
        [HttpPost("Update")]//定义Update方法
        public ActionResult<ApiResponse<int>> Update([FromForm] int BigID,
              [FromForm] int ParentID
            , [FromForm] int Statues
            , [FromForm] string BigTitle
            , [FromForm] string? KeyTitle
            , [FromForm] string? KeyWord
            , [FromForm] string? KeyDesn
            , [FromForm] string? Images
            , [FromForm] string? ImagesPhone
            , [FromForm] string? SiteUrl
            , [FromForm] int Sorts
            )
        {

            // 检查参数是否为空
            if (string.IsNullOrWhiteSpace(BigTitle) || ParentID < 0 || Sorts < 0
             || BigID <= 0
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

            //BigTitle校验
            if (string.IsNullOrWhiteSpace(BigTitle))
            {
                BigTitle = "";
            }
            //KeyTitle校验
            if (string.IsNullOrWhiteSpace(KeyTitle))
            {
                KeyTitle = "";
            }
            //KeyWord校验
            if (string.IsNullOrWhiteSpace(KeyWord))
            {
                KeyWord = "";
            }
            //KeyDesn校验
            if (string.IsNullOrWhiteSpace(KeyDesn))
            {
                KeyDesn = "";
            }
            //Images校验
            if (string.IsNullOrWhiteSpace(Images))
            {
                Images = "";
            }
            //ImagesPhone校验
            if (string.IsNullOrWhiteSpace(ImagesPhone))
            {
                ImagesPhone = "";
            }
            //SiteUrl跳转链接
            if (string.IsNullOrWhiteSpace(SiteUrl))
            {
                SiteUrl = "";
            }
            Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
            //判断登陆状态
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);
            //判断是否有权限
            if (AdminModel.Roles.IndexOf("|19|") < 0)
            {
                return Ok(new ApiResponse<int> { code = 404, msg = "无权限", count = 0, data = 0 });  // 返回新数据的ID
            }
            else
            {
                var Model = new ArticleCategory.ArticleCategoryModel
                {
                    //数据模型赋值
                    BigID = BigID,// 父级ID 
                    ParentID = ParentID,// 父级ID 
                    Statues = Statues,// 导航0是1否 
                    BigTitle = BigTitle,// 分类名称 
                    KeyTitle = KeyTitle,// 优化标题 
                    KeyWord = KeyWord,// 关键词 
                    KeyDesn = KeyDesn,// 描述 
                    Images = Images,// 图片 
                    ImagesPhone = ImagesPhone,// 图片-手机 
                    SiteUrl = SiteUrl,// 跳转链接
                    Sorts = Sorts,// 排序大号在前 
                    UpdateUserID = AdminModel.AdminID
                };
                // 实例化数据访问层对象，负责数据库访问的类
                ArticleCategory.ArticleCategoryDal Dal = new ArticleCategory.ArticleCategoryDal();
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

        // POST: api/ArticleCategory/Delete
        //通过 POST 请求根据ID 删除，并返回删除的记录数。
        [HttpPost("Delete")]//定义Delete方法
        public ActionResult<ApiResponse<int>> Delete([FromForm] int BigID)
        {
            // 检查参数是否为空
            if (
           BigID < 0
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
            if (AdminModel.Roles.IndexOf("|20|") < 0)
            {
                return Ok(new ApiResponse<int> { code = 404, msg = "无权限", count = 0, data = 0 });  // 返回新数据的ID
            }
            else
            {
                ArticleCategory.ArticleCategoryDal Dal = new ArticleCategory.ArticleCategoryDal();
                int UpdateUserID = AdminModel.AdminID;

                int IsOk = Dal.Delete(BigID, UpdateUserID);

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

        // Post: api/ArticleCategory/Get
        // 通过 Post。
        [HttpPost("Get")]  // 表示当客户端请求 GET 方法时，路径需要匹配 api/ArticleCategory/Get
        public ActionResult<ApiResponse<IEnumerable<ArticleCategory.ArticleCategoryModel>>> Get([FromForm] int ParentID)
        {


            // 检查分页参数是否有效
            if (ParentID < 0)
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
            ArticleCategory.ArticleCategoryDal Dal = new ArticleCategory.ArticleCategoryDal();


            // 实例化数据访问层对象，负责数据库访问的类
            // AdminPower.AdminPowerDal Dal = new AdminPower.AdminPowerDal();

            // 调用 Get 方法从数据库中获取数据
            DataTable Datas = Dal.Get(ParentID);

            // 使用工具方法 DataTableToList 将 DataTable 转换为 List<T>，此处 T 是 AdminModel 类的实例
            List<ArticleCategory.ArticleCategoryModel> List = Tools.DataTableToList.DatatableToList<ArticleCategory.ArticleCategoryModel>(Datas);

            // 返回分页的数据，使用统一的 API 响应格式，包含状态码、消息、总条数和数据
            return Ok(new ApiResponse<IEnumerable<ArticleCategory.ArticleCategoryModel>>
            {
                code = 0,  // 状态码 0 表示成功
                msg = "成功",  // 返回消息 "成功"
                count = List.Count,  // 返回数量，表示当前页面的条数
                data = List  // 返回分页的集合
            });
        }
        // Post: api/ArticleCategory/Get
        // 通过 Post。
        [HttpPost("GetAll")]  // 表示当客户端请求 GET 方法时，路径需要匹配 api/ArticleCategory/Get
        public ActionResult<ApiResponse<IEnumerable<ArticleCategory.ArticleCategoryModel>>> GetAll()
        {


            // 实例化数据访问层对象，负责数据库访问的类
            ArticleCategory.ArticleCategoryDal Dal = new ArticleCategory.ArticleCategoryDal();

            // 调用 Get 方法从数据库中获取数据
            DataTable Datas = Dal.GetAll();

            // 使用工具方法 DataTableToList 将 DataTable 转换为 List<T>，此处 T 是 AdminModel 类的实例
            List<ArticleCategory.ArticleCategoryModel> List = Tools.DataTableToList.DatatableToList<ArticleCategory.ArticleCategoryModel>(Datas);

            // 返回分页的数据，使用统一的 API 响应格式，包含状态码、消息、总条数和数据
            return Ok(new ApiResponse<IEnumerable<ArticleCategory.ArticleCategoryModel>>
            {
                code = 0,  // 状态码 0 表示成功
                msg = "成功",  // 返回消息 "成功"
                count = List.Count,  // 返回数量，表示当前页面的条数
                data = List  // 返回分页的集合
            });
        }

        // Post: api/ArticleCategory/Select
        // 通过 Post。
        [HttpPost("Select")]  // 表示当客户端请求 GET 方法时，路径需要匹配 api/ArticleCategory/Select
        public ActionResult<ApiResponse<IEnumerable<ArticleCategory.ArticleCategoryModel>>> Select([FromForm] int ParentID, [FromForm] int BigID, [FromForm] int? Top)
        {
            // 检查分页参数是否有效
            if (ParentID < 0|| BigID < 0)
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
            ArticleCategory.ArticleCategoryDal Dal = new ArticleCategory.ArticleCategoryDal();

            // 调用 Get 方法从数据库中获取数据
            DataTable Datas = Dal.GetParentAll(ParentID);

            // 使用工具方法 DataTableToList 将 DataTable 转换为 List<T>，此处 T 是 AdminModel 类的实例
            List<ArticleCategory.SelectList> List = Dal.GetJson(Datas, BigID, ParentID, Top??0);

            // 返回分页的数据，使用统一的 API 响应格式，包含状态码、消息、总条数和数据
            return Ok(new ApiResponse<IEnumerable<ArticleCategory.SelectList>>
            {
                code = 0,  // 状态码 0 表示成功
                msg = "成功",  // 返回消息 "成功"
                count = List.Count,  // 返回数量，表示当前页面的条数
                data = List  // 返回分页的集合
            });
        }
    }
}