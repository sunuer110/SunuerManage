using System;
using System.ComponentModel.DataAnnotations;

namespace ArticleCategory
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:ArticleCategoryModel
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class ArticleCategoryModel
    {
        public int BigID { get; set; } = 0; // 文章分类
        public int CreateUserID { get; set; } = 0; // 创建人
        public DateTime CreateDate { get; set; } // 创建时间
        public int UpdateUserID { get; set; } = 0; // 更新人
        public DateTime UpdateDate { get; set; } // 更新时间
        public int Del { get; set; } = 0; // 删除0否1是
        public int ParentID { get; set; } = 0; // 父级ID
        public int Depths { get; set; } = 0; // 深度默认为0顶级是1
        public string ParentIDs { get; set; } = ""; // 所有父级ID用,隔开
        public int ParentIDFirst { get; set; } = 0; // 顶级父ID
        public int Statues { get; set; } = 0; // 导航0是1否
        public string BigTitle { get; set; } = ""; // 分类名称
        public string KeyTitle { get; set; } = ""; // 优化标题
        public string KeyWord { get; set; } = ""; // 关键词
        public string KeyDesn { get; set; } = ""; // 描述
        public string Images { get; set; } = ""; // 图片
        public string ImagesPhone { get; set; } = ""; // 图片-手机
        public string SiteUrl { get; set; } = ""; // 跳转链接
        public int Sorts { get; set; } = 0; // 排序大号在前
    }
    public class SelectList
    {
        public string name { get; set; }//属性编号
        public int value { get; set; }  //分类编号
        public bool selected { get; set; }  //是否选中
        public List<SelectList> children { get; set; }  //分类编号
    }
}