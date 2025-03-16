using System;
using DBHelper;
using Microsoft.Data.SqlClient;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;

namespace SMS
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:SMSDal
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class SMSDal
    {
        // <summary>
        /// 添加验证码
        /// </summary>
        /// <param name="Phone">手机号/邮箱</param>
        /// <param name="ClassId">来源ID1注册 2找回密码 3登录</param>
        /// <param name="Number">验证码</param>
        /// <param name="IP">IP地址</param>
        /// <returns>返回-3发送成功，-2用户发送条数大于设置数，-4IP大于设置数，-1 60秒内不能重复发送</returns>
        public int AddSMS(string Phone,int ClassID,string Number,string IP)
        {
            DbHelper SQLRUN = new DbHelper();
            SqlParameter[] parameters =
            {
                new SqlParameter("@Phone", SqlDbType.NVarChar, 50) { Value = Phone },
                new SqlParameter("@ClassID", SqlDbType.Int, 4) { Value = ClassID },
                new SqlParameter("@Number", SqlDbType.NVarChar, 6) { Value = Number },
                new SqlParameter("@IP", SqlDbType.NVarChar, 50) { Value = IP },
              
            };
            return SQLRUN.ExecuteStoredProcedureReturnValue("SMS_Add", parameters);
        }
        /// <summary>
        /// 验证验证码   
        /// </summary>
        /// <param name="Phone">手机号/邮箱</param>
        /// <param name="ClassId">来源ID1注册 2找回密码 3登录</param>
        /// <param name="Number">验证码</param>
        /// <param name="IP">IP地址</param>
        /// <returns>返回-1完全符合要求，-2不符合检索</returns>
        public int CheckSMS(string Phone, int ClassID, string Number, string IP)
        {
            DbHelper SQLRUN = new DbHelper();
            DataTable DR = null;
            // 准备参数
            SqlParameter[] parameters =
            {
               new SqlParameter("@Phone", SqlDbType.NVarChar, 50) { Value = Phone },
                new SqlParameter("@ClassID", SqlDbType.Int, 4) { Value = ClassID },
                new SqlParameter("@Number", SqlDbType.NVarChar, 6) { Value = Number },
                new SqlParameter("@IP", SqlDbType.NVarChar, 50) { Value = IP },


            };
            return SQLRUN.ExecuteStoredProcedureReturnValue("SMS_Check", parameters);

        }


    }

}
