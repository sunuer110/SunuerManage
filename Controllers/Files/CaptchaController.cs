using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkiaSharp;
using System.IO;

namespace SunuerManage.Controllers
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:图片验证码
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CaptchaController : ControllerBase
    {
        [HttpGet("Generate")]
        public IActionResult Generate()
        {
            string captchaText = GenerateCaptchaText();

            using (var bitmap = new SKBitmap(100, 40))
            using (var canvas = new SKCanvas(bitmap))
            {
                canvas.Clear(SKColors.White);

                // 设置文本样式
                var paint = new SKPaint
                {
                    Color = SKColors.Black,
                    TextSize = 20,
                    IsAntialias = true,
                    Typeface = SKTypeface.Default
                };

                // 绘制验证码文本
                canvas.DrawText(captchaText, 10, 30, paint);

                // 添加干扰线
                var random = new Random();
                for (int i = 0; i < 5; i++)
                {
                    var linePaint = new SKPaint
                    {
                        Color = SKColors.Gray,
                        StrokeWidth = 1
                    };
                    float startX = random.Next(0, 100);
                    float startY = random.Next(0, 40);
                    float endX = random.Next(0, 100);
                    float endY = random.Next(0, 40);
                    canvas.DrawLine(startX, startY, endX, endY, linePaint);
                }

                // 添加随机的彩色噪点
                for (int i = 0; i < 100; i++)
                {
                    var dotPaint = new SKPaint
                    {
                        IsAntialias = true
                    };

                    // 随机生成颜色
                    dotPaint.Color = new SKColor(
                        (byte)random.Next(0, 256), // 红色分量
                        (byte)random.Next(0, 256), // 绿色分量
                        (byte)random.Next(0, 256)  // 蓝色分量
                    );

                    float x = random.Next(0, 100);
                    float y = random.Next(0, 40);
                    canvas.DrawPoint(x, y, dotPaint);
                }

                using (var memoryStream = new MemoryStream())
                {
                    bitmap.Encode(memoryStream, SKEncodedImageFormat.Png, 100);

                    // 将验证码保存到 Cookie
                    var cookieOptions = new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddMinutes(5), // 设定有效期
                        HttpOnly = true, // 防止客户端脚本访问
                      //  Secure = true, // 仅在 HTTPS 连接中传输
                        SameSite = SameSiteMode.Strict // 防止 CSRF
                    };
                    HttpContext.Response.Cookies.Append("CaptchaCode", Tools.CBC.HtmlEncodeEncrypt(captchaText), cookieOptions);

                    return File(memoryStream.ToArray(), "image/png");
                }
            }
        }

        private string GenerateCaptchaText()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var captcha = new char[5];
            for (int i = 0; i < 5; i++)
            {
                captcha[i] = chars[random.Next(chars.Length)];
            }
            return new string(captcha);
        }
    }
}
