namespace Tools
{
    public class ConfigurationHelper
    {
        /// <summary>
        /// Project:Sunuer Manage
        /// Description:获取appsettings.json中字段值
        /// Author：HaiDong
        /// Site:https://www.sunuer.com
        /// Version: 1.0
        /// License：Apache License 2.0
        /// </summary>
        private static IConfiguration _configuration;

        // 初始化时设置 IConfiguration
        public static void SetConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // 静态方法获取配置项
        public static string GetConfigValue(string key)
        {

            // 使用 GetValue 方法，若没有找到对应的key，则返回空字符串
            return _configuration.GetValue<string>(key) ?? string.Empty;
        }
    }
}
