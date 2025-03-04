namespace ApiResponse
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:定义 ApiResponse 类，统一 API 返回格式
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class ApiResponse<T>
    {
        public string? html { get; set; }  // html：返回的额外参数
        public int code { get; set; }  // code：表示 API 操作的状态码 0 表示成功，其他值表示失败
        public string msg { get; set; }  // msg：结果说明成功或者失败
        public int count { get; set; }  // count：数据的数量或影响的记录数，在查询操作中，count 是返回的数据总数,在插入、删除、更新操作中，count 是受影响的行数（例如删除了 1 条记录，更新了 2 条记录等）
        public T data { get; set; }  // data：泛型 T 表示实际返回的数据,T 可以是任何类型，如单个对象（例如单个用户）、集合（例如用户列表）、简单的数据类型（如 int）。

    }
}
