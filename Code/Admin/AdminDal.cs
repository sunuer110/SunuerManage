using DBHelper;
using Admin;
using Microsoft.Data.SqlClient;
using System.Data;
using Example;
using Tools;
using Azure.Core;
using Newtonsoft.Json;

namespace Admin
{
    /// <summary>
    /// Project:Sunuer Manage
    /// Description:AdminDal
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class AdminDal
    {
        /// <summary>
        /// 添加Admin-SQL方式
        /// Model.CreateUserID 创建人
        /// Model.AdminName 用户名
        /// Model.AdminNick 昵称
        /// Model.PassWord 密码
        /// Model.RoleID 角色ID
        /// Model.Statues 状态0正常1冻结
        /// </summary>
        /// <param name="Model">AdminModel</param>
        /// <returns>影响行数</returns>
        public int Add(AdminModel Model)
        {
            //密码加密
            string PassWord = Tools.Tools.MD5(Model.PassWord);
            string Sql = "insert into Admin ( CreateUserID,  AdminName, AdminNick, PassWord, RoleID, Statues) VALUES (@CreateUserID, @AdminName, @AdminNick, @PassWord, @RoleID, @Statues); select @@IDENTITY;";
            DbHelper SQLRUN = new DbHelper();
            SqlParameter[] parameters =
            {
        new SqlParameter("@CreateUserID", SqlDbType.Int, 4) { Value = Model.CreateUserID },
        new SqlParameter("@AdminName", SqlDbType.NVarChar, 50) { Value = Model.AdminName },
        new SqlParameter("@AdminNick", SqlDbType.NVarChar, 50) { Value = Model.AdminNick },
        new SqlParameter("@PassWord", SqlDbType.NVarChar, 200) { Value = PassWord },
        new SqlParameter("@RoleID", SqlDbType.Int, 4) { Value = Model.RoleID },
        new SqlParameter("@Statues", SqlDbType.Int, 4) { Value = Model.Statues }
    };
            return (Convert.ToInt32(SQLRUN.ExecuteScalar(Sql, CommandType.Text, parameters)));
        }


        /// <summary>
        /// 编辑Admin-SQL方式
        /// Model.AdminID 
        /// Model.UpdateUserID 更新人
        /// Model.AdminName 用户名
        /// Model.AdminNick 昵称
        /// Model.PassWord 密码
        /// Model.RoleID 角色ID
        /// Model.Statues 状态0正常1冻结
        /// </summary>
        /// <param name="Model">AdminModel</param>
        /// <returns>影响行数</returns>
        public int Update(AdminModel Model)
        {
            string PassWord = "";
            string Search = "";
            
            if (Model.PassWord!="")
            {
                //密码加密
                PassWord = Tools.Tools.MD5(Model.PassWord);
                Search = Search + $" , PassWord = @PassWord";
            }

            string Sql = "UPDATE Admin SET UpdateUserID = @UpdateUserID, UpdateDate = getdate(),  AdminName = @AdminName, AdminNick = @AdminNick, RoleID = @RoleID, Statues = @Statues "+ Search + " WHERE AdminID = @AdminID;select @@rowcount;";
            DbHelper SQLRUN = new DbHelper();
            SqlParameter[] parameters =
            {
        new SqlParameter("@AdminID", SqlDbType.Int, 4) { Value = Model.AdminID },
        new SqlParameter("@UpdateUserID", SqlDbType.Int, 4) { Value = Model.UpdateUserID },
        new SqlParameter("@AdminName", SqlDbType.NVarChar, 50) { Value = Model.AdminName },
        new SqlParameter("@AdminNick", SqlDbType.NVarChar, 50) { Value = Model.AdminNick },
        new SqlParameter("@PassWord", SqlDbType.NVarChar, 200) { Value = PassWord },
        new SqlParameter("@RoleID", SqlDbType.Int, 4) { Value = Model.RoleID },
        new SqlParameter("@Statues", SqlDbType.Int, 4) { Value = Model.Statues }
    };
            return (Convert.ToInt32(SQLRUN.ExecuteScalar(Sql, CommandType.Text, parameters)));
        }


        /// <summary>
        /// 删除Admin-SQL方式
        /// <param name="AdminID"></param>
        /// <param name="UpdateUserID"></param>
        /// <returns>影响行数</returns>
        /// </summary>
        public int Delete(int AdminID, int UpdateUserID)
        {
            string Sql = "update  Admin set Del=1,UpdateUserID=@UpdateUserID, UpdateDate = getdate() where AdminID = @AdminID;select @@rowcount;";
            DbHelper SQLRUN = new DbHelper();
            SqlParameter[] parameters =
            {
        new SqlParameter("@AdminID", SqlDbType.Int, 4) { Value = AdminID },
        new SqlParameter("@UpdateUserID", SqlDbType.Int, 4) { Value = UpdateUserID }
    };
            return Convert.ToInt32(SQLRUN.ExecuteScalar(Sql, CommandType.Text, parameters));
        }

        /// <summary>
        /// 修改密码
        /// <param name="AdminID"></param>
        /// <param name="UpdateUserID"></param>
        /// <returns>影响行数</returns>
        /// </summary>
        public int UpdatePassWord(int AdminID, string OldPass,string Pass)
        {

            OldPass = Tools.Tools.MD5(OldPass);
            Pass = Tools.Tools.MD5(Pass);
            string Sql = "update  Admin set PassWord=@Pass,UpdateUserID=@AdminID, UpdateDate = getdate() where AdminID = @AdminID and PassWord=@OldPass;select @@rowcount;";
            DbHelper SQLRUN = new DbHelper();
            SqlParameter[] parameters =
            {
        new SqlParameter("@AdminID", SqlDbType.Int, 4) { Value = AdminID },
        new SqlParameter("@OldPass", SqlDbType.NVarChar,50) { Value = OldPass },
        new SqlParameter("@Pass", SqlDbType.NVarChar,50) { Value = Pass }
    };
            return Convert.ToInt32(SQLRUN.ExecuteScalar(Sql, CommandType.Text, parameters));
        }

        
        /// <summary>
        /// 获取单条Admin信息-SQL方式
        /// <param name="AdminID"></param>
        /// <returns>返回单条记录模型</returns>
        /// </summary>
        public AdminModel GetModel(int AdminID)
        {
            string Sql = "select n.*,b.RolesTitle from Admin as n left join AdminRoles as b on n.RoleID=b.RoleID where n.Del=0 and n.AdminID = @AdminID;";
            AdminModel Model = new AdminModel();
            DbHelper SQLRUN = new DbHelper();
            SqlParameter[] parameters =
            {
        new SqlParameter("@AdminID", SqlDbType.Int, 4) { Value = AdminID }
    };
            DataTable dataTable = SQLRUN.ExecuteDataTable(Sql, CommandType.Text, parameters);
            if (dataTable.Rows.Count > 0)
            {
                var row = dataTable.Rows[0];
                if (dataTable.Rows[0]["AdminID"].ToString() != "")
                {
                    Model.AdminID = int.Parse(dataTable.Rows[0]["AdminID"].ToString()!);
                }

                if (dataTable.Rows[0]["CreateUserID"].ToString() != "")
                {
                    Model.CreateUserID = int.Parse(dataTable.Rows[0]["CreateUserID"].ToString()!);
                }

                if (dataTable.Rows[0]["CreateDate"].ToString() != "")
                {
                    Model.CreateDate = DateTime.Parse(dataTable.Rows[0]["CreateDate"].ToString()!);
                }

                if (dataTable.Rows[0]["UpdateUserID"].ToString() != "")
                {
                    Model.UpdateUserID = int.Parse(dataTable.Rows[0]["UpdateUserID"].ToString()!);
                }

                if (dataTable.Rows[0]["UpdateDate"].ToString() != "")
                {
                    Model.UpdateDate = DateTime.Parse(dataTable.Rows[0]["UpdateDate"].ToString()!);
                }

                if (dataTable.Rows[0]["Del"].ToString() != "")
                {
                    Model.Del = int.Parse(dataTable.Rows[0]["Del"].ToString()!);
                }

                Model.AdminName = dataTable.Rows[0]["AdminName"].ToString();
                Model.RolesTitle = dataTable.Rows[0]["RolesTitle"].ToString();
                
                Model.AdminNick = dataTable.Rows[0]["AdminNick"].ToString();

                Model.PassWord = dataTable.Rows[0]["PassWord"].ToString();

                if (dataTable.Rows[0]["RoleID"].ToString() != "")
                {
                    Model.RoleID = int.Parse(dataTable.Rows[0]["RoleID"].ToString());
                }

                if (dataTable.Rows[0]["Statues"].ToString() != "")
                {
                    Model.Statues = int.Parse(dataTable.Rows[0]["Statues"].ToString());
                }

                if (dataTable.Rows[0]["LoginAttempts"].ToString() != "")
                {
                    Model.LoginAttempts = int.Parse(dataTable.Rows[0]["LoginAttempts"].ToString());
                }

                if (dataTable.Rows[0]["LoginAttemptsLast"].ToString() != "")
                {
                    Model.LoginAttemptsLast = DateTime.Parse(dataTable.Rows[0]["LoginAttemptsLast"].ToString());
                }

                return Model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 获取 管理员 列表
        /// <param name="AdminName"></param>
        /// <param name="StartRecord"></param>
        /// <param name="EndRecord"></param>
        /// <returns>返回DataTable记录</returns>
        /// </summary>
        public DataTable Get(string AdminName,int StartRecord, int EndRecord)
        {
            string Search = "";
            if(AdminName!="")
            {
                Search = Search + $" and charindex('{AdminName}',n.AdminName)>0";
            }
            string Sql = "select a.*,b.RolesTitle from(select row_number() over(order by  n.AdminID desc) as aid ,n.* FROM  [dbo].[Admin] as n where n.del=0 " + Search + ") as a left join AdminRoles as b on a.RoleID=b.RoleID where a.aid between @StartRecord and (@EndRecord+@StartRecord-1) order by aid asc";
            DbHelper SQLRUN = new DbHelper();
            DataTable DR = null;
            // 准备参数
            SqlParameter[] parameters =
            {
        new SqlParameter("@StartRecord", SqlDbType.Int,4) { Value = StartRecord },
        new SqlParameter("@EndRecord", SqlDbType.Int,4) { Value = EndRecord },
        new SqlParameter("@AdminName", SqlDbType.NVarChar, 50) { Value = AdminName }
    };
            DR = SQLRUN.ExecuteDataTable(Sql, CommandType.Text, parameters);
            return DR;
        }

        /// <summary>
        /// 获取 管理员 总数
        /// <returns>返回DataTable记录</returns>
        /// </summary>
        public DataTable GetCount(string AdminName)
        {
            string Search = "";
            if (AdminName != "")
            {
                Search = Search + $" and charindex('{AdminName}',n.AdminName)>0";
            }
            string Sql = "select Count(n.AdminID) as Num from  [dbo].[Admin] as n where n.del=0"+ Search;
            DbHelper SQLRUN = new DbHelper();
            DataTable DR = null;
            // 准备参数
            SqlParameter[] parameters =
            {
        new SqlParameter("@AdminName", SqlDbType.NVarChar, 50) { Value = AdminName }
             };
            DR = SQLRUN.ExecuteDataTable(Sql, CommandType.Text, parameters);
            return DR;
        }

        /// <summary>
        /// 更新登陆尝试次数-SQL方式
        /// </summary>
        /// <param name="AdminName">用户名</param>
        /// <param name="LoginAttempts">登陆尝试次数</param>
        /// <returns>影响行数</returns>
        public int UpdateLoginAttempts(string AdminName, int LoginAttempts)
        {
            // 执行更新操作并返回 @@rowcount 值 -影响行数（更新后会有受影响行数）
            string Sql = "update  Admin set LoginAttempts=@LoginAttempts, LoginAttemptsLast=getdate() where AdminName=@AdminName; SELECT @@rowcount;";
            DbHelper SQLRUN = new DbHelper();
            SqlParameter[] parameters =
           {
                new SqlParameter("@AdminName", SqlDbType.NVarChar,50) { Value =AdminName },
                new SqlParameter("@LoginAttempts", SqlDbType.Int,4) { Value =LoginAttempts}
            };
            // 执行SQL
            return (Convert.ToInt32(SQLRUN.ExecuteScalar(Sql, CommandType.Text, parameters)));
        }
        /// <summary>
        /// 更新登录日志
        /// </summary>
        /// <param name="AdminName">用户名</param>
        /// <param name="IsOK">登陆成功0成功1失败</param>
        /// <param name="IP">IP</param>
        /// <param name="LoginAttempts">登陆尝试次数</param>
        /// <returns>影响行数</returns>
        public int UpdateLoginLog(string AdminName, int IsOK, string IP,int LoginAttempts)
        {
            // 执行更新操作并返回 @@rowcount 值 -影响行数（更新后会有受影响行数）
            string Sql = "insert into  AdminLoginLog (AdminName,IsOK,IP,LoginAttempts) values(@AdminName,@IsOK,@IP,@LoginAttempts); select @@IDENTITY;";
            DbHelper SQLRUN = new DbHelper();
            SqlParameter[] parameters =
           {
                new SqlParameter("@AdminName", SqlDbType.NVarChar,50) { Value =AdminName },
                new SqlParameter("@IsOK", SqlDbType.Int,4) { Value =IsOK},
                new SqlParameter("@IP", SqlDbType.NVarChar,50) { Value =IP },
                new SqlParameter("@LoginAttempts", SqlDbType.Int,4) { Value =LoginAttempts},

            };
            // 执行SQL
            return (Convert.ToInt32(SQLRUN.ExecuteScalar(Sql, CommandType.Text, parameters)));
        }
        /// <summary>
        /// 获取单条数据模型-登陆
        /// </summary>
        /// <param name="AdminName">用户名</param>
        /// <returns>AdminModel</returns>
        public AdminModel login(string AdminName)
        {
            string Sql = $"SELECT n.*,b.Powers as Roles  FROM [Admin] as n left join AdminRoles as b on n.RoleID=b.RoleID WHERE n.Statues=0 and  n.AdminName=@AdminName;";
            AdminModel Model = new AdminModel();
            DbHelper SQLRUN = new DbHelper();
            // 准备参数
            SqlParameter[] parameters =
            {
                new SqlParameter("@AdminName", SqlDbType.NVarChar,50) { Value =AdminName }
            };
            DataTable dataTable = SQLRUN.ExecuteDataTable(Sql, CommandType.Text, parameters);
            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["AdminID"].ToString() != "")
                {
                    Model.AdminID = int.Parse(dataTable.Rows[0]["AdminID"].ToString()!);
                }
                if (dataTable.Rows[0]["CreateUserID"].ToString() != "")
                {
                    Model.CreateUserID = int.Parse(dataTable.Rows[0]["CreateUserID"].ToString()!);
                }
                if (dataTable.Rows[0]["UpdateUserID"].ToString() != "")
                {
                    Model.UpdateUserID = int.Parse(dataTable.Rows[0]["UpdateUserID"].ToString()!);
                }
                if (dataTable.Rows[0]["CreateDate"].ToString() != "")
                {
                    Model.CreateDate = DateTime.Parse(dataTable.Rows[0]["CreateDate"].ToString()!);
                }
                if (dataTable.Rows[0]["UpdateDate"].ToString() != "")
                {
                    Model.UpdateDate = DateTime.Parse(dataTable.Rows[0]["UpdateDate"].ToString()!);
                }
                if (dataTable.Rows[0]["Del"].ToString() != "")
                {
                    Model.Del = int.Parse(dataTable.Rows[0]["Del"].ToString()!);
                }
                Model.AdminName = dataTable.Rows[0]["AdminName"].ToString()!;
                Model.PassWord = dataTable.Rows[0]["PassWord"].ToString()!;
                Model.AdminNick = dataTable.Rows[0]["AdminNick"].ToString()!;
                Model.Roles = dataTable.Rows[0]["Roles"].ToString()!;
                if (dataTable.Rows[0]["RoleID"].ToString() != "")
                {
                    Model.RoleID = int.Parse(dataTable.Rows[0]["RoleID"].ToString()!);
                }
                if (dataTable.Rows[0]["LoginAttempts"].ToString() != "")
                {
                    Model.LoginAttempts = int.Parse(dataTable.Rows[0]["LoginAttempts"].ToString()!);
                }
                if (dataTable.Rows[0]["LoginAttemptsLast"].ToString() != "")
                {
                    Model.LoginAttemptsLast = DateTime.Parse(dataTable.Rows[0]["LoginAttemptsLast"].ToString()!);
                }
                if (dataTable.Rows[0]["Statues"].ToString() != "")
                {
                    Model.Statues = int.Parse(dataTable.Rows[0]["Statues"].ToString()!);
                }

                return Model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 判断用户是否登录授权 返回结果集合
        /// </summary>
        public static AdminLoginModel GetAdminCheckLogin(IHttpContextAccessor httpContextAccessor)
        {
            AdminLoginModel model = new AdminLoginModel();

            try
            {
                if (httpContextAccessor == null)
                {
                    throw new InvalidOperationException("HttpContextAccessor is not configured.");
                }
                // 获取 HttpContext，确保不为 null
                var httpContext = httpContextAccessor.HttpContext;

                if (httpContext == null)
                {
                    throw new InvalidOperationException("HttpContext is null, ensure your middleware is properly configured.");
                }

                // 从 Cookie 中读取 JSON 字符串
                if (httpContext.Request.Cookies.TryGetValue("SunuerCookie", out var jsonFromCookie) &&
                    !string.IsNullOrEmpty(jsonFromCookie))
                {
                    // 解密 Cookie 内容
                    string decryptedCookie = Tools.CBC.DecryptHtmlDecode(jsonFromCookie);

                    if (decryptedCookie != "错误")
                    {
                        // 反序列化到 AdminLoginModel
                        model = JsonConvert.DeserializeObject<AdminLoginModel>(decryptedCookie)!;
                        if (model.IP != Tools.Tools.GetUserIp(httpContext))
                        {
                            httpContext.Response.Redirect("/Manage/Login");
                        }
                        //判定是否超时
                        if (Tools.ConfigurationHelper.GetConfigValue("CookieExpires") != "")
                            if (model.DateTime.AddHours(Int32.Parse(Tools.ConfigurationHelper.GetConfigValue("CookieExpires")!)) < DateTime.Now)
                            {
                                httpContext.Response.Redirect("/Manage/Login");
                            }
                    }
                    else
                    {
                        // 解密失败，跳转到登录页面
                        httpContext.Response.Redirect("/Manage/Login");
                    }
                }
                else
                {
                    // Cookie 不存在或为空，跳转到登录页面
                    httpContext.Response.Redirect("/Manage/Login");
                }
            }
            catch (Exception ex)
            {
                // 捕获异常并记录日志
                Console.WriteLine($"登录验证失败: {ex.Message}");
                httpContextAccessor.HttpContext?.Response.Redirect("/Manage/Login");
            }

            return model;
        }
    }
}
