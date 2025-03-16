using ApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Tools;
namespace Web.Controllers.SMSs
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:发送验证码
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SMSController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        // 通过依赖注入获取 IHttpContextAccessor
        public SMSController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // POST: api/SMS/SendCode
        //通过 POST 请求发送验证码(发送验证码，传入手机号就发手机验证码，传入邮箱就发邮箱验证码，两个必传一个)
        [HttpPost("SendCode")]
        public async Task<ActionResult<ApiResponse<int>>> SendCode(

        [FromForm] string? phone
        , [FromForm] string? Email
        , [FromForm] int ClassID

        )
        {
            //检查参数是否为空
            if (

             (string.IsNullOrWhiteSpace(phone) && string.IsNullOrWhiteSpace(Email)) || ClassID < 0

            )
            {
                return BadRequest(new ApiResponse<int>
                {
                    code = 400,
                    msg = "传入参数错误",
                    count = 0,
                    data = 0
                });
            }
            bool isPhone = !string.IsNullOrWhiteSpace(phone);
            var target = !string.IsNullOrWhiteSpace(phone) ? phone : Email;
            if (target is null)
            {
                return BadRequest(new ApiResponse<int>
                {
                    code = 400,
                    msg = "手机号和邮箱不能同时为空",
                    count = 0,
                    data = 0
                });
            }

            string IP = Tools.Tools.GetUserIp(_httpContextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null."));// 调用 GetUserIp 方法，传递当前 HttpContext
            string chkCode = Tools.Tools.MakephoneSMS(6);
            SMS.SMSDal sm = new SMS.SMSDal();
            int smid = sm.AddSMS(target, ClassID, chkCode, IP);//添加验证码到数据库并验证是否复核发送要求
            //验证验证码是否正确调用sm.CheckSMS(target, ClassID, chkCode, IP)函数进行验证
            if (smid == -3)
            {
                if (isPhone)
                {
                    await SendSMS(chkCode, phone!);//发送短信
                }
                else
                {
                    await BindEmail(chkCode, Email!);//发送邮件
                }
                return Ok(new ApiResponse<int>
                {
                    code = 0,
                    msg = "发送验证码成功",
                    count = 1,
                    data = 0
                });
            }
            else
            {
                return Ok(new ApiResponse<int>
                {
                    code = 0,
                    msg = "验证码获取失败",
                    count = 1,
                    data = 0
                });

            }


        }

        private static readonly HttpClient httpClient = new();

        /// <summary>
        // 发送手机验证码-请根据不同的短信厂商进行调整
        /// </summary>
        /// <param name="chkCode">验证码</param>
        /// <param name="phone">手机号</param>
        /// <returns></returns>
        private async Task SendSMS(string chkCode, string phone)
        {
            string posturl = $"发送短信链接";

            var postData = new Dictionary<string, string>
            {
                //{ "code", chkCode },
                //{ "phone", phone }
            };

            using var content = new FormUrlEncodedContent(postData);

            try
            {
                var response = await httpClient.PostAsync(posturl, content);
                response.EnsureSuccessStatusCode(); // 确保请求成功

                var responseContent = await response.Content.ReadAsStringAsync();
              //  Console.WriteLine(responseContent);
                // 根据 responseContent 做进一步处理
            }
            catch (HttpRequestException ex)
            {
                // 记录HTTP请求异常日志
            }
            catch (Exception ex)
            {
                // 记录其他异常日志
            }

        }

        // ************ 发送邮件-Star ************//
        private async Task BindEmail(string chkCode, string receiveEmail)
        {
            var subject = "邮箱验证标题";
            var content = $"邮箱验证内容包含验证码";

            using var client = new SmtpClient("邮箱smtp", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("邮箱", "邮箱发送密码")
            };

            ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;

            var msg = new MailMessage("邮箱", receiveEmail, subject, content) { IsBodyHtml = true };

            try
            {
                await client.SendMailAsync(msg);
            }
            catch
            {
                // 日志记录或错误处理
            }
        }

        private static bool RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            return true;
        }
        // ************ 发送邮件-End ************//

    }
}
