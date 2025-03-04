using System;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient; // 使用 SQL Server 的客户端驱动程序
using Microsoft.Extensions.Configuration;

namespace DBHelper
{
    /// <summary>
    /// Project:Sunuer Manage
    /// Description:DbHelper数据库操作方法
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class DbHelper
    {
        //针对 SQL Server 的提供程序是 System.Data.SqlClient，针对 MySQL 的提供程序是 MySql.Data.MySqlClient，针对 Oracle 的提供程序是 Oracle.ManagedDataAccess.Client
        // DbProviderFactory 是 ADO.NET 的一个抽象工厂模式，可以支持不同的数据库，如 SQL Server、MySQL、Oracle 等
        // 这里我们使用 SqlClientFactory 对象，用于 SQL Server 数据库操作
        private readonly DbProviderFactory _factory = SqlClientFactory.Instance;

        // 存储数据库连接字符串
        private readonly string _connectionString;

      
        // 构造函数：通过 ConfigurationBuilder 来加载 appsettings.json 文件，并从中读取数据库连接字符串
        public DbHelper()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)  // 设置基础路径，通常为应用程序的运行目录
                .AddJsonFile("appsettings.json", optional: false)  // 强制要求加载 appsettings.json 文件
                .Build();

            // 获取连接字符串，抛出异常时提供明确信息
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("Connection string not found.");
        }

        // 执行非查询 SQL 操作（如 INSERT、UPDATE、DELETE）
        public int ExecuteNonQuery(string sql, CommandType commandType = CommandType.Text, params DbParameter[] parameters)
        {
            try
            {
                // 使用 using 确保在使用完连接后正确关闭和释放资源
                using (DbConnection connection = CreateConnection())
                using (DbCommand command = CreateCommand(connection, sql, commandType, parameters))
                {
                    connection.Open();  // 打开数据库连接（连接池会自动管理）
                    return command.ExecuteNonQuery();  // 执行 SQL 并返回受影响的行数
                }
            }
            catch (DbException ex)  // 使用更加精确的异常类型
            {
                // 捕获数据库相关的异常，重新抛出以保持原始堆栈跟踪
                throw new DbOperationException("An error occurred during ExecuteNonQuery", ex);
            }
        }

        // 执行查询并返回单一结果（如 COUNT、SUM、查询某一列的值）
        public object ExecuteScalar(string sql, CommandType commandType = CommandType.Text, params DbParameter[] parameters)
        {
            try
            {
                using (DbConnection connection = CreateConnection())
                using (DbCommand command = CreateCommand(connection, sql, commandType, parameters))
                {
                    connection.Open();
                    return command.ExecuteScalar();  // 返回查询结果的第一行第一列的值
                }
            }
            catch (DbException ex)  // 使用更加精确的异常类型
            {
                throw new DbOperationException("An error occurred during ExecuteScalar", ex);
            }
        }
        // 执行查询并返回 DataSet（适用于查询多条记录或多个结果集的场景）
        public DataSet ExecuteDataSet(string sql, CommandType commandType = CommandType.Text, params DbParameter[] parameters)
        {
            try
            {
                using (DbConnection connection = CreateConnection())
                using (DbCommand command = CreateCommand(connection, sql, commandType, parameters))
                using (DbDataAdapter adapter = _factory.CreateDataAdapter())  // 创建适配器，用于填充 DataSet
                {
                    connection.Open();
                    adapter.SelectCommand = command;  // 指定用于填充 DataSet 的命令
                    DataSet dataSet = new DataSet();  // 创建 DataSet 对象
                    adapter.Fill(dataSet);  // 填充 DataSet，执行查询
                    return dataSet;  // 返回 DataSet
                }
            }
            catch (DbException ex)  // 捕获数据库异常
            {
                throw new DbOperationException("An error occurred during ExecuteDataSet", ex);
            }
        }

        // 执行查询并返回 DataTable（适用于查询多条记录的场景）
        public DataTable ExecuteDataTable(string sql, CommandType commandType = CommandType.Text, params DbParameter[] parameters)
        {
            try
            {
                using (DbConnection connection = CreateConnection())
                using (DbCommand command = CreateCommand(connection, sql, commandType, parameters))
                using (DbDataAdapter adapter = _factory.CreateDataAdapter())  // 创建适配器，用于填充 DataTable
                {
                    connection.Open();
                    adapter.SelectCommand = command;  // 指定用于填充 DataTable 的命令
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);  // 填充 DataTable，执行查询
                    return dataTable;
                }
            }
            catch (DbException ex)  // 使用更加精确的异常类型
            {
                throw new DbOperationException("An error occurred during ExecuteDataTable", ex);
            }
        }

        // 执行存储过程（不返回结果，例如增删改操作）
        public void ExecuteStoredProcedure(string procedureName, DbParameter[] parameters)  // 修改为 DbParameter[] 类型，增强通用性
        {
            try
            {
                using (DbConnection connection = CreateConnection())
                using (DbCommand command = CreateCommand(connection, procedureName, CommandType.StoredProcedure, parameters))
                {
                    connection.Open();
                    command.ExecuteNonQuery();  // 执行存储过程
                }
            }
            catch (DbException ex)  // 使用更加精确的异常类型
            {
                throw new DbOperationException("An error occurred during ExecuteStoredProcedure", ex);
            }
        }

        // 执行存储过程（返回整数，例如增删改操作返回新增值或修改值）
        public int ExecuteStoredProcedureReturnValue(string procedureName, DbParameter[] parameters)
        {
            try
            {
                using (DbConnection connection = CreateConnection())
                using (DbCommand command = CreateCommand(connection, procedureName, CommandType.StoredProcedure, parameters))
                {
                    // 创建返回值参数，返回存储过程的整数值
                    DbParameter returnValue = command.CreateParameter();
                    returnValue.ParameterName = "@ReturnValue";  // 参数名，可以随意定义
                    returnValue.Direction = ParameterDirection.ReturnValue;  // 设置为返回值类型
                    returnValue.DbType = DbType.Int32;  // 返回值类型是整数
                    command.Parameters.Add(returnValue);

                    // 打开连接并执行存储过程
                    connection.Open();
                    command.ExecuteNonQuery();  // 执行存储过程，不返回结果集

                    // 获取返回值
                    int result = (int)returnValue.Value;  // 捕获存储过程的返回值
                    return result;  // 返回整数值
                }
            }
            catch (DbException ex)  // 捕获数据库异常
            {
                throw new DbOperationException("An error occurred during ExecuteStoredProcedure", ex);
            }
        }

        // 执行存储过程并返回 DataTable（用于返回结果集的存储过程）
        public DataTable ExecuteStoredProcedureWithDataTable(string procedureName, DbParameter[] parameters)  // 修改为 DbParameter[] 类型
        {
            try
            {
                using (DbConnection connection = CreateConnection())
                using (DbCommand command = CreateCommand(connection, procedureName, CommandType.StoredProcedure, parameters))
                using (DbDataAdapter adapter = _factory.CreateDataAdapter())
                {
                    connection.Open();
                    adapter.SelectCommand = command;
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);  // 填充结果到 DataTable 中
                    return dataTable;
                }
            }
            catch (DbException ex)  // 使用更加精确的异常类型
            {
                throw new DbOperationException("An error occurred during ExecuteStoredProcedureWithDataTable", ex);
            }
        }
        // 执行存储过程并返回 DataSet（用于返回多个结果集的存储过程）
        public DataSet ExecuteStoredProcedureWithDataSet(string procedureName, DbParameter[] parameters)  // 修改为 DbParameter[] 类型
        {
            try
            {
                using (DbConnection connection = CreateConnection())
                using (DbCommand command = CreateCommand(connection, procedureName, CommandType.StoredProcedure, parameters))
                using (DbDataAdapter adapter = _factory.CreateDataAdapter())
                {
                    connection.Open();
                    adapter.SelectCommand = command;
                    DataSet dataSet = new DataSet();  // 创建 DataSet 对象
                    adapter.Fill(dataSet);  // 将结果填充到 DataSet 中
                    return dataSet;  // 返回 DataSet
                }
            }
            catch (DbException ex)  // 使用更加精确的异常类型
            {
                throw new DbOperationException("An error occurred during ExecuteStoredProcedureWithDataSet", ex);
            }
        }

        // 创建数据库连接
        private DbConnection CreateConnection()
        {
            DbConnection connection = _factory.CreateConnection();  // 使用工厂创建 DbConnection 对象
            connection.ConnectionString = _connectionString;  // 设置连接字符串
            return connection;
        }

        // 创建命令对象，用于执行 SQL 或存储过程
        private DbCommand CreateCommand(DbConnection connection, string sql, CommandType commandType, params DbParameter[] parameters)
        {
            DbCommand command = connection.CreateCommand();  // 创建 DbCommand 对象
            command.CommandText = sql;  // 设置 SQL 语句或存储过程名称
            command.CommandType = commandType;  // 设置命令类型（文本、存储过程等）

            // 参数为空时进行检查，避免 NullReferenceException
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter ?? throw new ArgumentNullException(nameof(parameter)));
                }
            }

            return command;
        }
    }

    // 自定义异常类，用于处理数据库操作的错误
    public class DbOperationException : Exception
    {
        public DbOperationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}