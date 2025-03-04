using System;
using DBHelper;
using Microsoft.Data.SqlClient;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Articles
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:ArticlesDal
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class ArticlesDal
    {
        /// <summary>
        /// 添加 Articles文章 
        /// Model.CreateUserID 创建人
        /// Model.BigID 分类
        /// Model.ArticleTitle 标题
        /// Model.Articlekey 关键字
        /// Model.ArticleDesn 描述
        /// Model.Statues 显示0是1否
        /// Model.Sorts 排序
        /// Model.NavSites 跳转地址
        /// Model.ReleaseTime 发布时间
        /// Model.Hits 点击率
        /// Model.Image 头图
        /// Model.Images 图片集合
        /// Model.Desn 详情
        /// </summary>
        public int Add(ArticlesModel Model)
        {
            DbHelper SQLRUN = new DbHelper();
            SqlParameter[] parameters =
            {
                new SqlParameter("@CreateUserID", SqlDbType.Int, 4) { Value = Model.CreateUserID },
                new SqlParameter("@BigID", SqlDbType.Int, 4) { Value = Model.BigID },
                new SqlParameter("@ArticleTitle", SqlDbType.NVarChar, 200) { Value = Model.ArticleTitle },
                new SqlParameter("@Articlekey", SqlDbType.NVarChar, 200) { Value = Model.Articlekey },
                new SqlParameter("@ArticleDesn", SqlDbType.NVarChar, 500) { Value = Model.ArticleDesn },
                new SqlParameter("@Statues", SqlDbType.Int, 4) { Value = Model.Statues },
                new SqlParameter("@Sorts", SqlDbType.Int, 4) { Value = Model.Sorts },
                new SqlParameter("@NavSites", SqlDbType.NVarChar, 200) { Value = Model.NavSites },
                new SqlParameter("@ReleaseTime", SqlDbType.DateTime, 8) { Value = Model.ReleaseTime },
                new SqlParameter("@Hits", SqlDbType.Int, 4) { Value = Model.Hits },
                new SqlParameter("@Image", SqlDbType.NVarChar, 500) { Value = Model.Image },
                new SqlParameter("@Images", SqlDbType.NVarChar, 500) { Value = Model.Images },
                new SqlParameter("@Desn", SqlDbType.NVarChar, -1) { Value = Model.Desn }
            };
            return SQLRUN.ExecuteStoredProcedureReturnValue("Articles_Add", parameters);
        }


        /// <summary>
        /// 更新 Articles文章 
        /// Model.ArticleID 
        /// Model.UpdateUserID 更新人
        /// Model.BigID 分类
        /// Model.ArticleTitle 标题
        /// Model.Articlekey 关键字
        /// Model.ArticleDesn 描述
        /// Model.Statues 显示0是1否
        /// Model.Sorts 排序
        /// Model.NavSites 跳转地址
        /// Model.ReleaseTime 发布时间
        /// Model.Hits 点击率
        /// Model.Image 头图
        /// Model.Images 图片集合
        /// Model.Desn 详情
        /// </summary>
        public int Update(ArticlesModel Model)
        {
            DbHelper SQLRUN = new DbHelper();
            SqlParameter[] parameters =
            {
                new SqlParameter("@ArticleID", SqlDbType.Int, 4) { Value = Model.ArticleID },
                new SqlParameter("@UpdateUserID", SqlDbType.Int, 4) { Value = Model.UpdateUserID },
                new SqlParameter("@BigID", SqlDbType.Int, 4) { Value = Model.BigID },
                new SqlParameter("@ArticleTitle", SqlDbType.NVarChar, 200) { Value = Model.ArticleTitle },
                new SqlParameter("@Articlekey", SqlDbType.NVarChar, 200) { Value = Model.Articlekey },
                new SqlParameter("@ArticleDesn", SqlDbType.NVarChar, 500) { Value = Model.ArticleDesn },
                new SqlParameter("@Statues", SqlDbType.Int, 4) { Value = Model.Statues },
                new SqlParameter("@Sorts", SqlDbType.Int, 4) { Value = Model.Sorts },
                new SqlParameter("@NavSites", SqlDbType.NVarChar, 200) { Value = Model.NavSites },
                new SqlParameter("@ReleaseTime", SqlDbType.DateTime, 8) { Value = Model.ReleaseTime },
                new SqlParameter("@Hits", SqlDbType.Int, 4) { Value = Model.Hits },
                new SqlParameter("@Image", SqlDbType.NVarChar, 500) { Value = Model.Image },
                new SqlParameter("@Images", SqlDbType.NVarChar, 500) { Value = Model.Images },
                new SqlParameter("@Desn", SqlDbType.NVarChar, -1) { Value = Model.Desn }
            };
            return SQLRUN.ExecuteStoredProcedureReturnValue("Articles_Update", parameters);
        }


        /// <summary>
        /// 删除 Articles文章 
        /// <param name="ArticleID"></param>
        /// <param name="UpdateUserID"></param>
        /// <returns>影响行数</returns>
        /// </summary>
        public int Delete(int ArticleID,int UpdateUserID)
        {
            DbHelper SQLRUN = new DbHelper();
            SqlParameter[] parameters =
            {
                new SqlParameter("@ArticleID", SqlDbType.Int, 4) { Value = ArticleID },
                new SqlParameter("@UpdateUserID", SqlDbType.Int, 4) { Value = UpdateUserID }
            };
            return SQLRUN.ExecuteStoredProcedureReturnValue("Articles_Delete", parameters);
        }


        /// <summary>
        /// 获取单条数据模型Articles文章 
        /// </summary>
        /// <param name="ArticleID">主键值</param>
        /// <returns>ArticlesModel</returns>
        public ArticlesModel GetModel(int ArticleID)
        {
            ArticlesModel model = new ArticlesModel();
            DbHelper SQLRUN = new DbHelper();

            // 准备参数
            SqlParameter[] parameters =
            {
                new SqlParameter("@ArticleID", SqlDbType.Int, 4) { Value = ArticleID }
             };

            DataTable dataTable = SQLRUN.ExecuteStoredProcedureWithDataTable("Articles_GetModel", parameters);
            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["ArticleID"].ToString() != "")
                {
                    model.ArticleID = int.Parse(dataTable.Rows[0]["ArticleID"].ToString());
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

                if (dataTable.Rows[0]["BigID"].ToString() != "")
                {
                    model.BigID = int.Parse(dataTable.Rows[0]["BigID"].ToString());
                }

                model.ArticleTitle = dataTable.Rows[0]["ArticleTitle"].ToString();

                model.Articlekey = dataTable.Rows[0]["Articlekey"].ToString();

                model.ArticleDesn = dataTable.Rows[0]["ArticleDesn"].ToString();

                if (dataTable.Rows[0]["Statues"].ToString() != "")
                {
                    model.Statues = int.Parse(dataTable.Rows[0]["Statues"].ToString());
                }

                if (dataTable.Rows[0]["Sorts"].ToString() != "")
                {
                    model.Sorts = int.Parse(dataTable.Rows[0]["Sorts"].ToString());
                }

                model.NavSites = dataTable.Rows[0]["NavSites"].ToString();

                if (dataTable.Rows[0]["ReleaseTime"].ToString() != "")
                {
                    model.ReleaseTime = DateTime.Parse(dataTable.Rows[0]["ReleaseTime"].ToString());
                }

                if (dataTable.Rows[0]["Hits"].ToString() != "")
                {
                    model.Hits = int.Parse(dataTable.Rows[0]["Hits"].ToString());
                }

                model.Image = dataTable.Rows[0]["Image"].ToString();

                model.Images = dataTable.Rows[0]["Images"].ToString();

                model.Desn = dataTable.Rows[0]["Desn"].ToString();

                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取 Articles文章 列表
        /// </summary>
        /// <param name="BigID"></param>
        /// <param name="ArticleTitle"></param>
        /// <param name="StartRecord"></param>
        /// <param name="EndRecord"></param>
        /// <returns>DataTable</returns>
        public DataTable Get(int BigID,string ArticleTitle, int StartRecord, int EndRecord)
        {
            DbHelper SQLRUN = new DbHelper();
            DataTable DR = null;
            // 准备参数
            SqlParameter[] parameters =
            {
                new SqlParameter("@BigID", SqlDbType.Int,4) { Value = BigID },
                new SqlParameter("@ArticleTitle", SqlDbType.NVarChar,50) { Value = ArticleTitle },
                new SqlParameter("@StartRecord", SqlDbType.Int,4) { Value = StartRecord },
                new SqlParameter("@EndRecord", SqlDbType.Int,4) { Value = EndRecord }
            };
            DR = SQLRUN.ExecuteStoredProcedureWithDataTable("Articles_Get", parameters);
            return DR;
        }

        /// <summary>
        /// 获取 Articles文章列表总数
        /// </summary>
        /// <param name="BigID"></param>
        /// <param name="ArticleTitle"></param>
        /// <returns>DataTable</returns>
        public DataTable GetCount(int BigID, string ArticleTitle)
        {
            DbHelper SQLRUN = new DbHelper();
            DataTable DR = null;
            // 准备参数
            SqlParameter[] parameters =
            {
                new SqlParameter("@BigID", SqlDbType.Int,4) { Value = BigID },
                new SqlParameter("@ArticleTitle", SqlDbType.NVarChar,50) { Value = ArticleTitle },
            };
            DR = SQLRUN.ExecuteStoredProcedureWithDataTable("Articles_GetCount", parameters);
            return DR;
        }

        /// <summary>
        /// 获取 Articles文章 列表
        /// </summary>
        /// <param name="BigID"></param>
        /// <param name="ArticleTitle"></param>
        /// <param name="StartRecord"></param>
        /// <param name="EndRecord"></param>
        /// <returns>DataTable</returns>
        public DataTable GetTop(int BigID, string ArticleTitle, int StartRecord, int EndRecord)
        {
            DbHelper SQLRUN = new DbHelper();
            DataTable DR = null;
            // 准备参数
            SqlParameter[] parameters =
            {
                new SqlParameter("@BigID", SqlDbType.Int,4) { Value = BigID },
                new SqlParameter("@ArticleTitle", SqlDbType.NVarChar,50) { Value = ArticleTitle },
                new SqlParameter("@StartRecord", SqlDbType.Int,4) { Value = StartRecord },
                new SqlParameter("@EndRecord", SqlDbType.Int,4) { Value = EndRecord }
            };
            DR = SQLRUN.ExecuteStoredProcedureWithDataTable("Articles_GetTop", parameters);
            return DR;
        }

        /// <summary>
        /// 获取 Articles文章列表总数
        /// </summary>
        /// <param name="BigID"></param>
        /// <param name="ArticleTitle"></param>
        /// <returns>DataTable</returns>
        public DataTable GetTopCount(int BigID, string ArticleTitle)
        {
            DbHelper SQLRUN = new DbHelper();
            DataTable DR = null;
            // 准备参数
            SqlParameter[] parameters =
            {
                new SqlParameter("@BigID", SqlDbType.Int,4) { Value = BigID },
                new SqlParameter("@ArticleTitle", SqlDbType.NVarChar,50) { Value = ArticleTitle },
            };
            DR = SQLRUN.ExecuteStoredProcedureWithDataTable("Articles_GetTopCount", parameters);
            return DR;
        }
        /// <summary>
        /// 获取 Articles文章列表30天每天数量
        /// </summary>
        /// <param name="StarTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <returns>DataTable</returns>
        public DataTable GetCount30(DateTime StarTime,DateTime EndTime)
        {
            DbHelper SQLRUN = new DbHelper();
            DataTable DR = null;
            // 准备参数
            SqlParameter[] parameters =
            {
                new SqlParameter("@StarTime", SqlDbType.DateTime,8) { Value = StarTime },
                new SqlParameter("@EndTime", SqlDbType.DateTime,8) { Value = EndTime },
            };
            DR = SQLRUN.ExecuteStoredProcedureWithDataTable("Articles_GetCount30", parameters);
            return DR;
        }

        /// <summary>
        /// 获取 Article下一篇文章
        /// </summary>
        /// <param name="ArticleID">文章ID</param>
        /// <returns>DataTable</returns>
        public DataTable GetNext(int ArticleID)
        {
            DbHelper SQLRUN = new DbHelper();
            DataTable DR = null;
            // 准备参数
            SqlParameter[] parameters =
            {
                new SqlParameter("@ArticleID", SqlDbType.Int,4) { Value = ArticleID },
            };
            DR = SQLRUN.ExecuteStoredProcedureWithDataTable("Articles_GetNext", parameters);
            return DR;
        }
        /// <summary>
        /// 获取 Article上一篇文章
        /// </summary>
        /// <param name="ArticleID">文章ID</param>
        /// <returns>DataTable</returns>
        public DataTable GetLast(int ArticleID)
        {
            DbHelper SQLRUN = new DbHelper();
            DataTable DR = null;
            // 准备参数
            SqlParameter[] parameters =
            {
                new SqlParameter("@ArticleID", SqlDbType.Int,4) { Value = ArticleID },
            };
            DR = SQLRUN.ExecuteStoredProcedureWithDataTable("Articles_GetLast", parameters);
            return DR;
        }
    }

}
