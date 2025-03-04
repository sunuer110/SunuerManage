using System;
using System.ComponentModel.DataAnnotations;

namespace Articles
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:ArticlesModel
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class ArticlesModel
    {
        public int ArticleID { get; set; } = 0; // 
        public int CreateUserID { get; set; } = 0; // 创建人
        public DateTime CreateDate { get; set; } // 创建时间
        public int UpdateUserID { get; set; } = 0; // 更新人
        public DateTime UpdateDate { get; set; } // 更新时间
        public int Del { get; set; } = 0; // 删除0否1是
        public int BigID { get; set; } = 0; // 分类
        public string ArticleTitle { get; set; } = ""; // 标题
        public string Articlekey { get; set; } = ""; // 关键字
        public string ArticleDesn { get; set; } = ""; // 描述
        public int Statues { get; set; } = 0; // 显示0是1否
        public int Sorts { get; set; } = 0; // 排序
        public string NavSites { get; set; } = ""; // 跳转地址
        public DateTime ReleaseTime { get; set; } // 发布时间
        public int Hits { get; set; } = 0; // 点击率
        public string Image { get; set; } = ""; // 头图
        public string Images { get; set; } = ""; // 图片集合
        public string Desn { get; set; } = ""; // 详情
        public string BigTitle { get; set; } =""; // 分类名
    }
}