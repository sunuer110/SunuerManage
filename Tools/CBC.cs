using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace Tools
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:CBC加密解密
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    public class CBC
    {
        public const string key = "qsy4w2mr35biedi04zf8foq1k0lhw181"; // 32字节密钥
        public const string iv = "0102037405060701"; // 16字节IV

        /// <summary>
        /// 接收HtmlDecode并Decrypt解密
        /// </summary>
        /// <param name="toDecrypt">需解密字符串</param>
        /// <returns></returns>
        public static string DecryptHtmlDecode(string toDecrypt)
        {
            return Decrypt(WebUtility.HtmlDecode(toDecrypt).Replace(" ", "+"));
        }

        /// <summary>
        /// 接收字符串并加密HtmlEncode
        /// </summary>
        /// <param name="toEncrypt">需加密字符串</param>
        /// <returns></returns>
        public static string HtmlEncodeEncrypt(string toEncrypt)
        {
            return WebUtility.HtmlEncode(Encrypt(toEncrypt));
        }

        /// <summary>
        /// 接收UrlEncode并Decrypt解密
        /// </summary>
        /// <param name="toDecrypt">需解密字符串</param>
        /// <returns></returns>
        public static string DecryptUrlDecode(string toDecrypt)
        {
            return Decrypt(WebUtility.UrlDecode(toDecrypt).Replace(" ", "+"));
        }

        /// <summary>
        /// 接收字符串并加密
        /// </summary>
        /// <param name="toEncrypt">需加密字符串</param>
        /// <returns></returns>
        public static string UrlEncodeEncrypt(string toEncrypt)
        {
            return WebUtility.UrlEncode(Encrypt(toEncrypt));
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="toEncrypt">需加密字符串</param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt)
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(key);
            byte[] ivArray = Encoding.UTF8.GetBytes(iv);
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

            using (var aes = Aes.Create())
            {
                aes.Key = keyArray;
                aes.IV = ivArray;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var encryptor = aes.CreateEncryptor())
                {
                    byte[] resultArray = encryptor.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                    return Convert.ToBase64String(resultArray);
                }
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="toDecrypt">需解密字符串</param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt)
        {
            try
            {
                byte[] keyArray = Encoding.UTF8.GetBytes(key);
                byte[] ivArray = Encoding.UTF8.GetBytes(iv);
                byte[] toDecryptArray = Convert.FromBase64String(toDecrypt);

                using (var aes = Aes.Create())
                {
                    aes.Key = keyArray;
                    aes.IV = ivArray;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    using (var decryptor = aes.CreateDecryptor())
                    {
                        byte[] resultArray = decryptor.TransformFinalBlock(toDecryptArray, 0, toDecryptArray.Length);
                        return Encoding.UTF8.GetString(resultArray);
                    }
                }
            }
            catch (Exception ex)
            {
                // 记录错误日志（可以替换为实际的日志记录方法）
                Console.WriteLine($"解密失败: {ex.Message}");
                return string.Empty; // 返回空字符串或其他错误处理
            }
        }
    }
}
