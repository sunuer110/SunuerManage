using System;
using DBHelper;
using Microsoft.Data.SqlClient;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LeaveMessageSet
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:LeaveMessageSetDal
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class LeaveMessageSetDal
    {
        /// <summary>
        /// 更新 LeaveMessageSet留言配置表 
        /// Model.SetID 配置ID
        /// Model.UpdateUserID 更新人
        /// Model.PhoneRequire 手机号0非必填1必填
        /// Model.NameRequire 姓名0非必填1必填
        /// Model.EmailRequire 邮箱0非必填1必填
        /// Model.SystemEmail 留言系统邮箱
        /// </summary>
        public int Update(LeaveMessageSetModel Model)
        {
            DbHelper SQLRUN = new DbHelper();
            SqlParameter[] parameters =
            {
                new SqlParameter("@SetID", SqlDbType.Int, 4) { Value = Model.SetID },
                new SqlParameter("@UpdateUserID", SqlDbType.Int, 4) { Value = Model.UpdateUserID },
                new SqlParameter("@PhoneRequire", SqlDbType.Int, 4) { Value = Model.PhoneRequire },
                new SqlParameter("@NameRequire", SqlDbType.Int, 4) { Value = Model.NameRequire },
                new SqlParameter("@EmailRequire", SqlDbType.Int, 4) { Value = Model.EmailRequire },
                new SqlParameter("@SystemEmail", SqlDbType.NVarChar, 50) { Value = Model.SystemEmail }
            };
            return SQLRUN.ExecuteStoredProcedureReturnValue("LeaveMessageSet_Update", parameters);
        }
        /// <summary>
        /// 获取单条数据模型LeaveMessageSet留言配置表 
        /// </summary>
        /// <param name="SetID">主键值</param>
        /// <returns>LeaveMessageSetModel</returns>
        public LeaveMessageSetModel GetModel(int SetID)
        {
            LeaveMessageSetModel model = new LeaveMessageSetModel();
            DbHelper SQLRUN = new DbHelper();

            // 准备参数
            SqlParameter[] parameters =
            {
                new SqlParameter("@SetID", SqlDbType.Int, 4) { Value = SetID }
             };

            DataTable dataTable = SQLRUN.ExecuteStoredProcedureWithDataTable("LeaveMessageSet_GetModel", parameters);
            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["SetID"].ToString() != "")
                {
                    model.SetID = int.Parse(dataTable.Rows[0]["SetID"].ToString());
                }

                if (dataTable.Rows[0]["CreateUserID"].ToString() != "")
                {
                    model.CreateUserID = int.Parse(dataTable.Rows[0]["CreateUserID"].ToString());
                }

                if (dataTable.Rows[0]["CreateDate"].ToString() != "")
                {
                    model.CreateDate = DateTime.Parse(dataTable.Rows[0]["CreateDate"].ToString());
                }

                if (dataTable.Rows[0]["UpdateUserID"].ToString() != "")
                {
                    model.UpdateUserID = int.Parse(dataTable.Rows[0]["UpdateUserID"].ToString());
                }

                if (dataTable.Rows[0]["UpdateDate"].ToString() != "")
                {
                    model.UpdateDate = DateTime.Parse(dataTable.Rows[0]["UpdateDate"].ToString());
                }

                if (dataTable.Rows[0]["Del"].ToString() != "")
                {
                    model.Del = int.Parse(dataTable.Rows[0]["Del"].ToString());
                }

                if (dataTable.Rows[0]["PhoneRequire"].ToString() != "")
                {
                    model.PhoneRequire = int.Parse(dataTable.Rows[0]["PhoneRequire"].ToString());
                }

                if (dataTable.Rows[0]["NameRequire"].ToString() != "")
                {
                    model.NameRequire = int.Parse(dataTable.Rows[0]["NameRequire"].ToString());
                }

                if (dataTable.Rows[0]["EmailRequire"].ToString() != "")
                {
                    model.EmailRequire = int.Parse(dataTable.Rows[0]["EmailRequire"].ToString());
                }

                model.SystemEmail = dataTable.Rows[0]["SystemEmail"].ToString();

                return model;
            }
            else
            {
                return null;
            }
        }


    }

}
