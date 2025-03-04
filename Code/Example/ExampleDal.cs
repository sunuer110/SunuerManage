using DBHelper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Example
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:演示示例Dal
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class ExampleDal
    {
       

        /// <summary>
        /// 添加用户-SQL方式
        /// Model.UserName 用户名
        /// Model.Phone 手机号
        /// Model.PassWord 密码
        /// Model.IP IP
        /// </summary>
        /// <param name="Model">ExampleModel</param>
        /// <returns>影响行数</returns>
        public int AddSQL(ExampleModel Model)
        {
            // 执行插入操作并返回 @@IDENTITY 值
            string Sql = "INSERT INTO Example (UserName, Phone,PassWord,IP) VALUES (@UserName, @Phone,@PassWord,@IP); SELECT @@IDENTITY;";
            DbHelper SQLRUN = new DbHelper();
            SqlParameter[] parameters =
           {
                new SqlParameter("@UserName", SqlDbType.NVarChar,50) { Value = Model.UserName },
                new SqlParameter("@Phone", SqlDbType.NVarChar,50) { Value = Model.Phone },
                new SqlParameter("@PassWord", SqlDbType.NVarChar,200) { Value = Model.PassWord },
                new SqlParameter("@IP", SqlDbType.NVarChar, 200) { Value = Model.IP }
            };
            // 执行SQL
            return (Convert.ToInt32(SQLRUN.ExecuteScalar(Sql, CommandType.Text, parameters)));
        }
        /// <summary>
        /// 添加用户-存储过程
        /// Model.UserName 用户名
        /// Model.Phone 手机号
        /// Model.PassWord 密码
        /// Model.IP IP
        /// </summary>
        /// <param name="Model">ExampleModel</param>
        /// <returns>影响行数</returns>
        public int Add(ExampleModel Model)
        {
            DbHelper SQLRUN = new DbHelper();
            // 准备存储过程参数
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserName", SqlDbType.NVarChar,50) { Value = Model.UserName },
                new SqlParameter("@Phone", SqlDbType.NVarChar,50) { Value = Model.Phone },
                new SqlParameter("@PassWord", SqlDbType.NVarChar,200) { Value = Model.PassWord },
                new SqlParameter("@IP", SqlDbType.NVarChar, 200) { Value = Model.IP }
            };
            return (SQLRUN.ExecuteStoredProcedureReturnValue("Example_ADD", parameters));
        }

        /// <summary>
        /// 更新用户-SQL方式
        /// Model.UserID 用户ID
        /// Model.UserName 用户名
        /// Model.Phone 手机号
        /// </summary>
        /// <param name="Model">ExampleModel</param>
        /// <returns>影响行数</returns>
        public int UpdateSQL(ExampleModel Model)
        {
            // 执行更新操作并返回 @@rowcount 值 -影响行数（更新后会有受影响行数）
            string Sql = "update  Example set UserName=@UserName, Phone=@Phone where UserID=@UserID; SELECT @@rowcount;";
            DbHelper SQLRUN = new DbHelper();
            SqlParameter[] parameters =
           {
                new SqlParameter("@UserName", SqlDbType.NVarChar,50) { Value = Model.UserName },
                new SqlParameter("@Phone", SqlDbType.NVarChar,50) { Value = Model.Phone },
                new SqlParameter("@UserID", SqlDbType.Int,4) { Value = Model.UserID }
            };
            // 执行SQL
            return (Convert.ToInt32(SQLRUN.ExecuteScalar(Sql, CommandType.Text, parameters)));
        }
        /// <summary>
        /// 更新用户-存储过程
        /// Model.UserID 用户ID
        /// Model.UserName 用户名
        /// Model.Phone 手机号
        /// </summary>
        /// <param name="Model">ExampleModel</param>
        /// <returns>影响行数</returns>
        public int Update(ExampleModel Model)
        {
            DbHelper SQLRUN = new DbHelper();
            // 准备存储过程参数
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserName", SqlDbType.NVarChar,50) { Value = Model.UserName },
                new SqlParameter("@Phone", SqlDbType.NVarChar,50) { Value = Model.Phone },
                new SqlParameter("@UserID", SqlDbType.Int,4) { Value = Model.UserID }
            };
            return (SQLRUN.ExecuteStoredProcedureReturnValue("Example_Update", parameters));
        }

        /// <summary>
        /// 删除用户-SQL方式
        /// </summary>
        /// <param name="Model">ExampleModel</param>
        /// <returns>影响行数</returns>
        public int DeleteSQL(int UserID)
        {
            // 执行更新操作并返回 @@rowcount 值 -影响行数（更新后会有受影响行数）
            string Sql = "delete  Example  where UserID=@UserID; SELECT @@rowcount;";
            DbHelper SQLRUN = new DbHelper();
            SqlParameter[] parameters =
           {
                new SqlParameter("@UserID", SqlDbType.Int,4) { Value = UserID }
            };
            // 执行SQL
            return (Convert.ToInt32(SQLRUN.ExecuteScalar(Sql, CommandType.Text, parameters)));
        }
        /// <summary>
        /// 删除用户-存储过程
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns>影响行数</returns>
        public int Delete(int UserID)
        {
            DbHelper SQLRUN = new DbHelper();
            // 准备存储过程参数
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", SqlDbType.Int,4) { Value = UserID }
            };
            return (SQLRUN.ExecuteStoredProcedureReturnValue("Example_Delete", parameters));
        }
        /// <summary>
        /// 获取单条数据模型
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns>ExampleModel</returns>
        public ExampleModel GetModelSQL(int UserID)
        {
            string Sql = $"SELECT * FROM [Example] WHERE Statues=0 and  UserID=@UserID;";
            ExampleModel Model = new ExampleModel();
            DbHelper SQLRUN = new DbHelper();
            // 准备参数
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", SqlDbType.Int,4) { Value = Model.UserID }
            };
            DataTable dataTable = SQLRUN.ExecuteDataTable(Sql, CommandType.Text, parameters);
            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["UserID"].ToString() != "")
                {
                    Model.UserID = int.Parse(dataTable.Rows[0]["UserID"].ToString());
                }
                 Model.UserName = dataTable.Rows[0]["UserName"].ToString();
                 Model.Phone = dataTable.Rows[0]["Phone"].ToString();
                Model.PassWord = dataTable.Rows[0]["PassWord"].ToString();
                Model.IP = dataTable.Rows[0]["IP"].ToString();

                if (dataTable.Rows[0]["CreateDate"].ToString() != "")
                {
                    Model.CreateDate = DateTime.Parse(dataTable.Rows[0]["CreateDate"].ToString());
                }
                if (dataTable.Rows[0]["Statues"].ToString() != "")
                {
                    Model.Statues = int.Parse(dataTable.Rows[0]["Statues"].ToString());
                }

                return Model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取用户数量-存储过程
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="Phone">手机号</param>
        /// <returns>DataTable</returns>
        public DataTable GetCount(string UserName, string Phone)
        {
            DbHelper SQLRUN = new DbHelper();
            DataTable DR = null;

            // 准备存储过程参数
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserName", SqlDbType.NVarChar,50) { Value = UserName },
                new SqlParameter("@Phone", SqlDbType.NVarChar,50) { Value = Phone }
            };
            DR=SQLRUN.ExecuteStoredProcedureWithDataTable("Example_GetCount", parameters);
            return DR;
        }
        /// <summary>
        /// 获取用户列表-存储过程
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="Phone">手机号</param>
        /// <param name="StartRecord">开始条数</param>
        /// <param name="EndRecord">结束条数</param>
        /// <returns>DataTable</returns>
        public DataTable Get(string UserName, string Phone, int StartRecord, int EndRecord)
        {
            DbHelper SQLRUN = new DbHelper();
            DataTable DR = null;
            // 准备存储过程参数
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserName", SqlDbType.NVarChar,50) { Value = UserName },
                new SqlParameter("@Phone", SqlDbType.NVarChar,50) { Value = Phone },
                new SqlParameter("@StartRecord", SqlDbType.Int,4) { Value = StartRecord },
                new SqlParameter("@EndRecord", SqlDbType.Int,4) { Value = EndRecord }
            };
            DR= SQLRUN.ExecuteStoredProcedureWithDataTable("Example_GetList", parameters);
            return DR;
        }
        /// <summary>
        /// 获取用户数量--SQL
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="Phone">手机号</param>
        /// <returns>DataSet</returns>
        public DataTable GetCountSQL(string UserName, string Phone)
        {
            string searchs = "";
            if (UserName != "")
            {
                searchs = searchs + $" and charindex('{UserName}',n.UserName)>0";
            }
            if (Phone != "")
            {
                searchs = searchs + $" and charindex('{Phone}',n.Phone)>0";
            }
            string Sql = $"SELECT * FROM [Example] WHERE Statues=0 {searchs};";
            DbHelper SQLRUN = new DbHelper();
            DataTable DR = null;

            // 准备存储过程参数
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserName", SqlDbType.NVarChar,50) { Value = UserName },
                new SqlParameter("@Phone", SqlDbType.NVarChar,50) { Value = Phone }
            };
            DR = SQLRUN.ExecuteDataTable(Sql, CommandType.Text, parameters);

            return DR;
        }
        /// <summary>
        /// 获取用户列表-SQL
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="Phone">手机号</param>
        /// <param name="StartRecord">开始条数</param>
        /// <param name="EndRecord">结束条数</param>
        /// <returns>DataSet</returns>
        public DataTable GetSQL(string UserName, string Phone, int StartRecord, int EndRecord)
        {
            string searchs = "";
            if(UserName!="")
            {
                searchs = searchs + $" and charindex('{UserName}',n.UserName)>0";
            }
            if (Phone != "")
            {
                searchs = searchs + $" and charindex('{Phone}',n.Phone)>0";
            }
            string Sql = $"select a.* from(select row_number() over(order by n.UserID desc) as aid ,n.* FROM [Example] as n  n.Statues=0 {searchs}) as a   where a.aid between @StartRecord and (@StartRecord+@EndRecord-1) order by aid asc";
            DbHelper SQLRUN = new DbHelper();
            DataTable DR;
            // 准备存储过程参数
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserName", SqlDbType.NVarChar,50) { Value = UserName },
                new SqlParameter("@Phone", SqlDbType.NVarChar,50) { Value = Phone },
                new SqlParameter("@StartRecord", SqlDbType.Int,4) { Value = StartRecord },
                new SqlParameter("@EndRecord", SqlDbType.Int,4) { Value = EndRecord }
            };
            DR = SQLRUN.ExecuteDataTable(Sql, CommandType.Text, parameters);
            return DR;
        }
    }
}
