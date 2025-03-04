using System;
using DBHelper;
using Microsoft.Data.SqlClient;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;

namespace ArticleCategory
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:ArticleCategoryDal
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class ArticleCategoryDal
    {
        /// <summary>
        /// 添加 ArticleCategory文章分类 
        /// Model.CreateUserID 创建人
        /// Model.ParentID 父级ID
        /// Model.Statues 导航0是1否
        /// Model.BigTitle 分类名称
        /// Model.KeyTitle 优化标题
        /// Model.KeyWord 关键词
        /// Model.KeyDesn 描述
        /// Model.Images 图片
        /// Model.ImagesPhone 图片-手机
        /// Model.Sorts 排序大号在前
        /// </summary>
        public int Add(ArticleCategoryModel Model)
        {
            DbHelper SQLRUN = new DbHelper();
            SqlParameter[] parameters =
            {
                new SqlParameter("@CreateUserID", SqlDbType.Int, 4) { Value = Model.CreateUserID },
                new SqlParameter("@ParentID", SqlDbType.Int, 4) { Value = Model.ParentID },
                new SqlParameter("@Statues", SqlDbType.Int, 4) { Value = Model.Statues },
                new SqlParameter("@BigTitle", SqlDbType.NVarChar, 50) { Value = Model.BigTitle },
                new SqlParameter("@KeyTitle", SqlDbType.NVarChar, 200) { Value = Model.KeyTitle },
                new SqlParameter("@KeyWord", SqlDbType.NVarChar, 200) { Value = Model.KeyWord },
                new SqlParameter("@KeyDesn", SqlDbType.NVarChar, 500) { Value = Model.KeyDesn },
                new SqlParameter("@Images", SqlDbType.NVarChar, 200) { Value = Model.Images },
                new SqlParameter("@ImagesPhone", SqlDbType.NVarChar, 200) { Value = Model.ImagesPhone },
                new SqlParameter("@SiteUrl", SqlDbType.NVarChar, 200) { Value = Model.SiteUrl },
                new SqlParameter("@Sorts", SqlDbType.Int, 4) { Value = Model.Sorts }
            };
            return SQLRUN.ExecuteStoredProcedureReturnValue("ArticleCategory_Add", parameters);
        }

        
        /// <summary>
        /// 更新 ArticleCategory文章分类 
        /// Model.BigID 文章分类
        /// Model.UpdateUserID 更新人
        /// Model.ParentID 父级ID
        /// Model.Statues 导航0是1否
        /// Model.BigTitle 分类名称
        /// Model.KeyTitle 优化标题
        /// Model.KeyWord 关键词
        /// Model.KeyDesn 描述
        /// Model.Images 图片
        /// Model.ImagesPhone 图片-手机
        /// Model.Sorts 排序大号在前
        /// </summary>
        public int Update(ArticleCategoryModel Model)
        {
            DbHelper SQLRUN = new DbHelper();
            SqlParameter[] parameters =
            {
                new SqlParameter("@BigID", SqlDbType.Int, 4) { Value = Model.BigID },
                new SqlParameter("@UpdateUserID", SqlDbType.Int, 4) { Value = Model.UpdateUserID },
                new SqlParameter("@ParentID", SqlDbType.Int, 4) { Value = Model.ParentID },
                new SqlParameter("@Statues", SqlDbType.Int, 4) { Value = Model.Statues },
                new SqlParameter("@BigTitle", SqlDbType.NVarChar, 50) { Value = Model.BigTitle },
                new SqlParameter("@KeyTitle", SqlDbType.NVarChar, 200) { Value = Model.KeyTitle },
                new SqlParameter("@KeyWord", SqlDbType.NVarChar, 200) { Value = Model.KeyWord },
                new SqlParameter("@KeyDesn", SqlDbType.NVarChar, 500) { Value = Model.KeyDesn },
                new SqlParameter("@Images", SqlDbType.NVarChar, 200) { Value = Model.Images },
                new SqlParameter("@ImagesPhone", SqlDbType.NVarChar, 200) { Value = Model.ImagesPhone },
                new SqlParameter("@SiteUrl", SqlDbType.NVarChar, 200) { Value = Model.SiteUrl },
                new SqlParameter("@Sorts", SqlDbType.Int, 4) { Value = Model.Sorts }
            };
            return SQLRUN.ExecuteStoredProcedureReturnValue("ArticleCategory_Update", parameters);
        }


        /// <summary>
        /// 删除 ArticleCategory文章分类 
        /// <param name="BigID">文章分类</param>
        /// <returns>影响行数</returns>
        /// </summary>
        public int Delete(int BigID,int UpdateUserID)
        {
            DbHelper SQLRUN = new DbHelper();
            SqlParameter[] parameters =
            {
                new SqlParameter("@BigID", SqlDbType.Int, 4) { Value = BigID },
                new SqlParameter("@UpdateUserID", SqlDbType.Int, 4) { Value = UpdateUserID }
            };
            return SQLRUN.ExecuteStoredProcedureReturnValue("ArticleCategory_Delete", parameters);
        }

        /// <summary>
        /// 获取单条数据模型ArticleCategory文章分类 
        /// </summary>
        /// <param name="BigID">主键值</param>
        /// <returns>ArticleCategoryModel</returns>
        public ArticleCategoryModel GetModel(int BigID)
        {
            ArticleCategoryModel model = new ArticleCategoryModel();
            DbHelper SQLRUN = new DbHelper();

            // 准备参数
            SqlParameter[] parameters =
            {
                new SqlParameter("@BigID", SqlDbType.Int, 4) { Value = BigID }
             };

            DataTable dataTable = SQLRUN.ExecuteStoredProcedureWithDataTable("ArticleCategory_GetModel", parameters);
            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["BigID"].ToString() != "")
                {
                    model.BigID = int.Parse(dataTable.Rows[0]["BigID"].ToString());
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

                if (dataTable.Rows[0]["ParentID"].ToString() != "")
                {
                    model.ParentID = int.Parse(dataTable.Rows[0]["ParentID"].ToString());
                }

                if (dataTable.Rows[0]["Depths"].ToString() != "")
                {
                    model.Depths = int.Parse(dataTable.Rows[0]["Depths"].ToString());
                }

                model.ParentIDs = dataTable.Rows[0]["ParentIDs"].ToString();

                if (dataTable.Rows[0]["ParentIDFirst"].ToString() != "")
                {
                    model.ParentIDFirst = int.Parse(dataTable.Rows[0]["ParentIDFirst"].ToString());
                }

                if (dataTable.Rows[0]["Statues"].ToString() != "")
                {
                    model.Statues = int.Parse(dataTable.Rows[0]["Statues"].ToString());
                }

                model.BigTitle = dataTable.Rows[0]["BigTitle"].ToString();

                model.KeyTitle = dataTable.Rows[0]["KeyTitle"].ToString();

                model.KeyWord = dataTable.Rows[0]["KeyWord"].ToString();

                model.KeyDesn = dataTable.Rows[0]["KeyDesn"].ToString();

                model.Images = dataTable.Rows[0]["Images"].ToString();
                
                model.ImagesPhone = dataTable.Rows[0]["ImagesPhone"].ToString();
                model.SiteUrl = dataTable.Rows[0]["SiteUrl"].ToString();


                if (dataTable.Rows[0]["Sorts"].ToString() != "")
                {
                    model.Sorts = int.Parse(dataTable.Rows[0]["Sorts"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }



        /// <summary>
        /// 获取 ArticleCategory文章分类 列表
        /// </summary>
        /// <param name="ParentID">父级ID</param>
        public DataTable Get(int ParentID)
        {
            DbHelper SQLRUN = new DbHelper();
            DataTable DR = null;
            // 准备参数
            SqlParameter[] parameters =
            {
                new SqlParameter("@ParentID", SqlDbType.Int,4) { Value = ParentID }
            };
            DR = SQLRUN.ExecuteStoredProcedureWithDataTable("ArticleCategory_Get", parameters);
            return DR;
        }


        /// <summary>
        /// 获取 ArticleCategory文章分类文章数量
        /// </summary>
        /// <param name="ParentID">父级ID</param>
        public DataTable GetNumber(int ParentID)
        {
            DbHelper SQLRUN = new DbHelper();
            DataTable DR = null;
            // 准备参数
            SqlParameter[] parameters =
            {
                new SqlParameter("@ParentID", SqlDbType.Int,4) { Value = ParentID }
            };
            DR = SQLRUN.ExecuteStoredProcedureWithDataTable("ArticleCategory_GetNumber", parameters);
            return DR;
        }

        /// <summary>
        /// 获取 ArticleCategory文章分类 包含ParentID所有
        /// </summary>
        /// <param name="ParentID">父级ID</param>
        public DataTable GetParentAll(int ParentID)
        {
            DbHelper SQLRUN = new DbHelper();
            DataTable DR = null;
            // 准备参数
            SqlParameter[] parameters =
            {
                new SqlParameter("@ParentID", SqlDbType.Int,4) { Value = ParentID }
            };
            DR = SQLRUN.ExecuteStoredProcedureWithDataTable("ArticleCategory_GetParentIDAll", parameters);
            return DR;
        }

        /// <summary>
        /// 获取 ArticleCategory文章分类 全部
        /// </summary>
        public DataTable GetAll()
        {
            DbHelper SQLRUN = new DbHelper();
            DataTable DR = null;
            // 准备参数
            SqlParameter[] parameters =
            {
            };
            DR = SQLRUN.ExecuteStoredProcedureWithDataTable("ArticleCategory_GetAll", parameters);
            return DR;
        }

        /// <summary>
        /// 获取分类Select
        /// </summary>
        /// <param name="datalist">表</param>
        /// <param name="BigID">分类ID</param>
        /// <param name="ParentID">父级ID</param>
        /// <returns> List<Parent></returns>
        public List<SelectList> GetJson(DataTable datalist, int BigID, int ParentID,int Top)
        {

            List<SelectList> Models = new List<SelectList>();
            if (ParentID == 0&& Top==0)
            {
                SelectList list0 = new SelectList();
                list0.name = "顶级分类";
                list0.value = 0;
                if (BigID == 0)
                {
                    list0.selected = true;
                }
                Models.Add(list0);
            }
            if (datalist.Rows.Count > 0)
            {
                foreach (DataRow col1 in datalist.Rows)
                {
                    if (col1["ParentID"].ToString() == ParentID.ToString())
                    {
                        SelectList list = new SelectList();
                        list.name = col1["BigTitle"].ToString();
                        list.value = Int32.Parse(col1["BigID"].ToString());
                        if (BigID.ToString() == col1["BigID"].ToString())
                        {
                            list.selected = true;
                        }
                        list.children = GetJson(datalist, BigID, Int32.Parse(col1["BigID"].ToString()), Top);

                        Models.Add(list);
                    }
                }
            }
            return Models;
        }
       
    }

}