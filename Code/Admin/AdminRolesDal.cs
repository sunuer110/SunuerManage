using System;
using DBHelper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AdminRoles
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:AdminRolesDal
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class AdminRolesDal
    {

        /// <summary>
        /// 添加AdminRoles-SQL方式
        /// Model.CreateUserID 创建人
        /// Model.RolesTitle 角色名称
        /// Model.Powers 权限集合
        /// </summary>
        /// <param name="Model">AdminRolesModel</param>
        /// <returns>影响行数</returns>
        public int Add(AdminRolesModel Model)
        {
            string Sql = "insert into AdminRoles (CreateUserID, RolesTitle, Powers) VALUES ( @CreateUserID, @RolesTitle, @Powers); select @@IDENTITY;";
            DbHelper SQLRUN = new DbHelper();
            SqlParameter[] parameters =
            {
                new SqlParameter("@CreateUserID", SqlDbType.Int, 4) { Value = Model.CreateUserID },
                new SqlParameter("@RolesTitle", SqlDbType.NVarChar, 50) { Value = Model.RolesTitle },
                new SqlParameter("@Powers", SqlDbType.NVarChar, 2000) { Value = Model.Powers }
            };
            return (Convert.ToInt32(SQLRUN.ExecuteScalar(Sql, CommandType.Text, parameters)));
        }


        /// <summary>
        /// 编辑AdminRoles-SQL方式
        /// Model.RoleID 
        /// Model.UpdateUserID 更新人
        /// Model.RolesTitle 角色名称
        /// Model.Powers 权限集合
        /// </summary>
        /// <param name="Model">AdminRolesModel</param>
        /// <returns>影响行数</returns>
        public int Update(AdminRolesModel Model)
        {
            string Sql = "UPDATE AdminRoles SET  UpdateUserID = @UpdateUserID, UpdateDate = getdate(), RolesTitle = @RolesTitle, Powers = @Powers WHERE RoleID = @RoleID;select @@rowcount;";
            DbHelper SQLRUN = new DbHelper();
            SqlParameter[] parameters =
            {
                new SqlParameter("@RoleID", SqlDbType.Int, 4) { Value = Model.RoleID },
                new SqlParameter("@UpdateUserID", SqlDbType.Int, 4) { Value = Model.UpdateUserID },
                new SqlParameter("@RolesTitle", SqlDbType.NVarChar, 50) { Value = Model.RolesTitle },
                new SqlParameter("@Powers", SqlDbType.NVarChar, 2000) { Value = Model.Powers },
            };
            return (Convert.ToInt32(SQLRUN.ExecuteScalar(Sql, CommandType.Text, parameters)));
        }


        /// <summary>
        /// 删除AdminRoles-SQL方式
        /// <param name="RoleID"></param>
        /// <returns>影响行数</returns>
        /// </summary>
        public int Delete(int RoleID,int UpdateUserID)
        {
            string Sql = "update  AdminRoles set del=1,UpdateUserID=@UpdateUserID, UpdateDate = getdate() where RoleID = @RoleID;select @@rowcount;";
            DbHelper SQLRUN = new DbHelper();
            SqlParameter[] parameters =
            {
                new SqlParameter("@RoleID", SqlDbType.Int,4) { Value = RoleID },
                new SqlParameter("@UpdateUserID", SqlDbType.Int,4) { Value = UpdateUserID }
            };
            return Convert.ToInt32(SQLRUN.ExecuteScalar(Sql, CommandType.Text, parameters));
        }


        /// <summary>
        /// 获取单条AdminRoles信息-SQL方式
        /// <param name="RoleID"></param>
        /// <returns>返回单条记录模型</returns>
        /// </summary>
        public AdminRolesModel GetModel(int RoleID)
        {
            string Sql = "select * from AdminRoles where RoleID = @RoleID;";
            AdminRolesModel Model = new AdminRolesModel();
            DbHelper SQLRUN = new DbHelper();
            SqlParameter[] parameters =
            {
                new SqlParameter("@RoleID", SqlDbType.Int) { Value = RoleID }
    };
            DataTable dataTable = SQLRUN.ExecuteDataTable(Sql, CommandType.Text, parameters);
            if (dataTable.Rows.Count > 0)
            {
                var row = dataTable.Rows[0];
                if (dataTable.Rows[0]["RoleID"].ToString() != "")
                {
                    Model.RoleID = int.Parse(dataTable.Rows[0]["RoleID"].ToString());
                }

                if (dataTable.Rows[0]["CreateUserID"].ToString() != "")
                {
                    Model.CreateUserID = int.Parse(dataTable.Rows[0]["CreateUserID"].ToString());
                }

                if (dataTable.Rows[0]["CreateDate"].ToString() != "")
                {
                    Model.CreateDate = DateTime.Parse(dataTable.Rows[0]["CreateDate"].ToString());
                }

                if (dataTable.Rows[0]["UpdateUserID"].ToString() != "")
                {
                    Model.UpdateUserID = int.Parse(dataTable.Rows[0]["UpdateUserID"].ToString());
                }

                if (dataTable.Rows[0]["UpdateDate"].ToString() != "")
                {
                    Model.UpdateDate = DateTime.Parse(dataTable.Rows[0]["UpdateDate"].ToString());
                }

                if (dataTable.Rows[0]["Del"].ToString() != "")
                {
                    Model.Del = int.Parse(dataTable.Rows[0]["Del"].ToString());
                }

                Model.RolesTitle = dataTable.Rows[0]["RolesTitle"].ToString();

                Model.Powers = dataTable.Rows[0]["Powers"].ToString();

                return Model;
            }
            else
            {
                return null;
            }
        }



        /// <summary>
        /// 获取 角色列表
        /// <param name="RolesTitle"></param>
        /// <param name="StartRecord"></param>
        /// <param name="EndRecord"></param>
        /// <returns>返回DataTable记录</returns>
        /// </summary>
        public DataTable Get(string RolesTitle,int StartRecord, int EndRecord)
        {
            string Search = "";
            if (RolesTitle != "")
            {
                Search = Search + $" and charindex('{RolesTitle}',n.RolesTitle)>0";
            }
            string Sql = "select a.*from(select row_number() over(order by  n.RoleID desc) as aid ,n.* FROM  [dbo].[AdminRoles] as n where n.del=0 "+ Search + ") as a where a.aid between @StartRecord and (@EndRecord+@StartRecord-1) order by aid asc";
            DbHelper SQLRUN = new DbHelper();
            DataTable DR = null;
            // 准备参数
            SqlParameter[] parameters =
            {
                new SqlParameter("@StartRecord", SqlDbType.Int,4) { Value = StartRecord },
                new SqlParameter("@EndRecord", SqlDbType.Int,4) { Value = EndRecord },
                new SqlParameter("@RolesTitle", SqlDbType.NVarChar, 50) { Value = RolesTitle }
            };
            DR = SQLRUN.ExecuteDataTable(Sql, CommandType.Text, parameters);
            return DR;
        }

        /// <summary>
        /// 获取 角色总数
        /// <param name="RolesTitle"></param>
        /// <returns>返回DataTable记录</returns>
        /// </summary>
        public DataTable GetCount(string RolesTitle)
        {
            string Search = "";
            if (RolesTitle != "")
            {
                Search = Search + $" and charindex('{RolesTitle}',n.RolesTitle)>0";
            }
            string Sql = "select Count(n.RoleID) as Num from  [dbo].[AdminRoles] as n where n.del=0"+ Search;
            DbHelper SQLRUN = new DbHelper();
            DataTable DR = null;
            // 准备参数
            SqlParameter[] parameters =
            {
                new SqlParameter("@RolesTitle", SqlDbType.NVarChar, 50) { Value = RolesTitle }
            };
            DR = SQLRUN.ExecuteDataTable(Sql, CommandType.Text, parameters);
            return DR;
        }

    }

}