using ApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace SunuerManage.Controllers.Articless
{
    /// <summary>
    /// Project:Sunuer Manage
    /// Description:Articles API
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        // 通过依赖注入获取 IHttpContextAccessor- 获取实际IP地址使用
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ArticlesController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }



        // POST: api/Articles/Add
        //通过 POST 请求添加新数据，参数接收 int型如果没有该参数 默认是0 
        [HttpPost("Add")]
        public ActionResult<ApiResponse<int>> Add(
         [FromForm] int BigID
        , [FromForm] string ArticleTitle
        , [FromForm] string? Articlekey
        , [FromForm] string? ArticleDesn
        , [FromForm] int Statues
        , [FromForm] int Sorts
        , [FromForm] int Hits
        , [FromForm] string? NavSites
        , [FromForm] string ReleaseTime
        , [FromForm] string? Image
        , [FromForm] string? Images
        , [FromForm] string? Desn
            )
        {  // 检查参数是否为空
            if (string.IsNullOrWhiteSpace(ArticleTitle)  || string.IsNullOrWhiteSpace(ReleaseTime) || BigID < 0 || Sorts < 0 || Statues < 0 || Hits < 0
               || !DateTime.TryParse(ReleaseTime, out DateTime releaseDate)
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

            //Articlekey校验
            if (string.IsNullOrWhiteSpace(Articlekey))
            {
                Articlekey = "";
            }
            //ArticleDesn校验
            if (string.IsNullOrWhiteSpace(ArticleDesn))
            {
                ArticleDesn = "";
            }
            //NavSites校验
            if (string.IsNullOrWhiteSpace(NavSites))
            {
                NavSites = "";
            }
            //ReleaseTime校验
            if (string.IsNullOrWhiteSpace(ReleaseTime))
            {
                ReleaseTime = "";
            }
            //Image校验
            if (string.IsNullOrWhiteSpace(Image))
            {
                Image = "";
            }
            //Images校验
            if (string.IsNullOrWhiteSpace(Images))
            {
                Images = "";
            }
            //Desn校验
            if (string.IsNullOrWhiteSpace(Desn))
            {
                Desn = "";
            }


            Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
            //判断登陆状态
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);
            //判断是否有权限
            if (AdminModel.Roles.IndexOf("|21|") < 0)
            {
                return Ok(new ApiResponse<int> { code = 404, msg = "无权限", count = 0, data = 0 });  // 返回新数据的ID
            }
            else
            {
                var Model = new Articles.ArticlesModel
                {

                    //数据模型赋值
                    BigID = BigID,// 分类 
                    ArticleTitle = ArticleTitle,// 标题 
                    Articlekey = Articlekey,// 关键字 
                    ArticleDesn = ArticleDesn,// 描述 
                    Statues = Statues,// 显示0是1否 
                    Sorts = Sorts,// 排序 
                    NavSites = NavSites,// 跳转地址 
                    ReleaseTime = DateTime.Parse(ReleaseTime),// 发布时间 
                    Image = Image,// 头图 
                    Images = Images,// 图片集合 
                    Hits = Hits,
                    Desn = Desn,// 排序大号在前 
                    CreateUserID = AdminModel.AdminID
                };

                // 实例化数据访问层对象，负责数据库访问的类
                Articles.ArticlesDal Dal = new Articles.ArticlesDal();
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
        // POST: api/Articles/Update
        //通过 Post 请求更新数据密码，并返回影响的记录数。
        [HttpPost("Update")]//定义Update方法
        public ActionResult<ApiResponse<int>> Update([FromForm] int ArticleID,
         [FromForm] int BigID
        , [FromForm] string ArticleTitle
        , [FromForm] string? Articlekey
        , [FromForm] string? ArticleDesn
        , [FromForm] int Statues
        , [FromForm] int Sorts
        , [FromForm] int Hits
        , [FromForm] string? NavSites
        , [FromForm] string ReleaseTime
        , [FromForm] string? Image
        , [FromForm] string? Images
        , [FromForm] string? Desn
            )
        {



            // 检查参数是否为空
            if (string.IsNullOrWhiteSpace(ArticleTitle) || Statues < 0 || Sorts < 0 || ArticleID < 0
             || BigID < 0 || Hits < 0
             || !DateTime.TryParse(ReleaseTime, out DateTime releaseDate)
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

            //Articlekey校验
            if (string.IsNullOrWhiteSpace(Articlekey))
            {
                Articlekey = "";
            }
            //ArticleDesn校验
            if (string.IsNullOrWhiteSpace(ArticleDesn))
            {
                ArticleDesn = "";
            }
            //NavSites校验
            if (string.IsNullOrWhiteSpace(NavSites))
            {
                NavSites = "";
            }
            //ReleaseTime校验
            if (string.IsNullOrWhiteSpace(ReleaseTime))
            {
                ReleaseTime = "";
            }
            //Image校验
            if (string.IsNullOrWhiteSpace(Image))
            {
                Image = "";
            }
            //Images校验
            if (string.IsNullOrWhiteSpace(Images))
            {
                Images = "";
            }
            //Desn校验
            if (string.IsNullOrWhiteSpace(Desn))
            {
                Desn = "";
            }


            Admin.AdminLoginModel AdminModel = new Admin.AdminLoginModel();
            //判断登陆状态
            AdminModel = Admin.AdminDal.GetAdminCheckLogin(_httpContextAccessor);
            //判断是否有权限
            if (AdminModel.Roles.IndexOf("|22|") < 0)
            {
                return Ok(new ApiResponse<int> { code = 404, msg = "无权限", count = 0, data = 0 });  // 返回新数据的ID
            }
            else
            {
                var Model = new Articles.ArticlesModel
                {
                    //数据模型赋值
                    ArticleID = ArticleID,//  
                    BigID = BigID,// 分类 
                    ArticleTitle = ArticleTitle,// 标题 
                    Articlekey = Articlekey,// 关键字 
                    ArticleDesn = ArticleDesn,// 描述 
                    Statues = Statues,// 显示0是1否 
                    Sorts = Sorts,// 排序 
                    NavSites = NavSites,// 跳转地址 
                    ReleaseTime = DateTime.Parse(ReleaseTime),// 发布时间 
                    Image = Image,// 头图 
                    Images = Images,// 图片集合 
                    Desn = Desn,// 排序大号在前 
                    Hits = Hits,// 热度
                    UpdateUserID = AdminModel.AdminID
                };
                // 实例化数据访问层对象，负责数据库访问的类
                Articles.ArticlesDal Dal = new Articles.ArticlesDal();
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

        // POST: api/Articles/Delete
        //通过 POST 请求根据ID 删除，并返回删除的记录数。
        [HttpPost("Delete")]//定义Delete方法
        public ActionResult<ApiResponse<int>> Delete([FromForm] string ArticleIDs)
        {
            // 检查参数是否为空
            if (

             string.IsNullOrWhiteSpace(ArticleIDs)
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
                Articles.ArticlesDal Dal = new Articles.ArticlesDal();
                // 将逗号分隔的字符串转换为整数列表
                var idList = ArticleIDs.Split(',')
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

        // GET: api/Admin/Get
        // 通过 GET 请求获取分页数据，通过 AdminName、Page 和 PageSize 参数控制分页。
        [HttpGet("Get")]  // 表示当客户端请求 GET 方法时，路径需要匹配 api/Admin/Get
        public ActionResult<ApiResponse<IEnumerable<Articles.ArticlesModel>>> Get([FromQuery] string? ArticleTitle, [FromQuery] int BigID, [FromQuery] int Page, [FromQuery] int PageSize)
        {

            // 如果 ArticleTitle 为 null 或空，设置为 ""，表示不进行筛选
            if (string.IsNullOrEmpty(ArticleTitle))
            {
                ArticleTitle = ""; // 不传递或传递空字符串时，进行查询时不做名字筛选
            }

            // 检查分页参数是否有效
            if (Page <= 0 || PageSize <= 0)
            {
                return BadRequest(new ApiResponse<int>
                {
                    code = 400,
                    msg = "传入的参数错误",
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
            if (AdminModel.Roles.IndexOf("|17|") < 0)
            {
                return Ok(new ApiResponse<int> { code = 404, msg = "无权限", count = 0, data = 0 });  // 返回新的ID
            }
            else
            {
                // 实例化数据访问层对象，是负责数据库访问的类
                Articles.ArticlesDal Dal = new Articles.ArticlesDal();

                // 调用 Get 方法从数据库中获取数据，传入分页参数：BigID、ArticleTitle、开始记录、每页条数
                DataTable Datas = Dal.Get(BigID, ArticleTitle, StartRecord, EndRecord);

                // 使用工具方法 DataTableToList 将 DataTable 转换为 List<T>，此处 T 是 AdminModel 类的实例
                List<Articles.ArticlesModel> List = Tools.DataTableToList.DatatableToList<Articles.ArticlesModel>(Datas);

                DataTable Data = Dal.GetCount(BigID, ArticleTitle);
                int Number = Int32.Parse(Data.Rows[0]["Num"].ToString()!);
                // 返回分页的数据，使用统一的 API 响应格式，包含状态码、消息、总条数和数据
                return Ok(new ApiResponse<IEnumerable<Articles.ArticlesModel>>
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