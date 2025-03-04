using System.Data;
using System.Reflection;

namespace Tools
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:定义 DataTableToList 类，将 DataTable 转换为 List
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public static class DataTableToList
    {
        /// <summary>
        /// 将 DataTable 转换为 List<T>
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="dataTable">源 DataTable</param>
        /// <returns>转换后的 List<T></returns>
        public static List<T> DatatableToList<T>(this DataTable dataTable) where T : new()
        {
            // 创建一个用于保存结果的 List<T>
            List<T> list = new List<T>();

            // 获取类型的所有公共属性
            PropertyInfo[] properties = typeof(T).GetProperties();

            // 遍历 DataTable 中的每一行
            foreach (DataRow row in dataTable.Rows)
            {
                // 创建类型 T 的新实例
                T item = new T();

                // 遍历属性，将 DataTable 的列数据赋值给对象的属性
                foreach (PropertyInfo property in properties)
                {
                    // 检查 DataTable 是否包含与属性名称相匹配的列
                    if (dataTable.Columns.Contains(property.Name))
                    {
                        // 如果列数据不为 DBNull，则将值赋给属性
                        if (row[property.Name] != DBNull.Value)
                        {
                            property.SetValue(item, Convert.ChangeType(row[property.Name], property.PropertyType), null);
                        }
                    }
                }

                // 将对象添加到列表中
                list.Add(item);
            }

            return list;
        }
    }
}
