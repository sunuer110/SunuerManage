using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SunuerManage.Controllers
{

    /// <summary>
    /// Project:Sunuer Manage
    /// Description:上传文件API
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    { 
        // 通过依赖注入获取 IHttpContextAccessor- 获取实际IP地址使用
        private readonly IHttpContextAccessor _httpContextAccessor;
        //注入获取appsettings.json字符串
        private readonly IConfiguration _configuration;

        public FilesController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile file,[FromQuery] string? name)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded or file is empty.");
            }
            //About校验
            if (string.IsNullOrWhiteSpace(name))
            {
                name = "";
            }
            // 限制文件类型
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".docx", ".xlsx" };
            string fileExtension = Path.GetExtension(file.FileName).ToLower();
            string[] allowedMimeTypes = { "image/jpeg", "image/png", "image/gif", "application/pdf", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" };

            string FileType = "";
            FileType = fileExtension;
            // 检查文件扩展名和 MIME 类型
            if (!allowedExtensions.Contains(fileExtension) || !allowedMimeTypes.Contains(file.ContentType))
            {
                return Ok(new
                {
                    Code = "401",
                    Messge = "上传失败，格式错误！",//Invalid file type,
                    FileUrl = ""
                });
            }
            // 确保上传文件路径不包含 ".." 来防止路径遍历
            if (file.FileName.Contains(".."))
            {
                return Ok(new
                {
                    Code = "402",
                    Messge = "上传失败，格式错误！",//Invalid file name
                    FileUrl = ""
                });
            }
            // 验证文件的 Magic Number
            if (!IsValidFile(file))
            {

                return Ok(new
                {
                    Code = "403",
                    Messge = "上传失败，格式错误！",//Invalid file content.
                    FileUrl = ""
                });
            }

            // 文件大小限制 (5MB)
            const long maxFileSize = 5 * 1024 * 1024; // 5MB
            if (file.Length > maxFileSize)
            {
                return Ok(new
                {
                    Code = "404",
                    Messge = "上传失败，格式错误！",//File size exceeds the 5MB limit.
                    FileUrl = ""
                });
            }

            // 保存文件
            try
            {
                if (name != "")
                {
                    string baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploadfile");


                    string uniqueFileName = $"{name}{fileExtension}";
                    string filePath = Path.Combine(baseFolder, uniqueFileName);
                    Console.WriteLine(filePath);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    string fileUrl = $"/Uploadfile/{uniqueFileName}";
                    string FileName = uniqueFileName;
                    return Ok(new
                    {
                        Code = "0",
                        Messge = "上传成功",

                        FileType = FileType,
                        FileUrl = fileUrl,
                        FileName = FileName
                    });
                }
                else
                {
                    string baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploadfile");
                    string yearMonthFolder = Path.Combine(baseFolder, DateTime.Now.ToString("yyyy/MM").Replace("/", Path.DirectorySeparatorChar.ToString()));
                    if (!Directory.Exists(yearMonthFolder))
                    {
                        Directory.CreateDirectory(yearMonthFolder);
                    }

                    string uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                    string filePath = Path.Combine(yearMonthFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    string fileUrl = $"/Uploadfile/{DateTime.Now:yyyy}/{DateTime.Now:MM}/{uniqueFileName}";
                    string FileName = file.FileName;
                    return Ok(new
                    {
                        Code = "0",
                        Messge = "上传成功",

                        FileType = FileType,
                        FileUrl = fileUrl,
                        FileName = FileName
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Code = "500",
                    Messge = "上传失败",//An error occurred while uploading the file.
                    FileUrl = ""
                });
              
            }
        }
        [HttpPost("Uploads")]
        public async Task<IActionResult> Uploads(IFormFile[] files)
        {
            if (files == null || files.Length == 0)
            {
                return BadRequest(new
                {
                    Code = "405",
                    Messge = "没有上传文件或文件为空。",
                    FileUrls = new List<string>()
                });
            }

            // 限制文件类型
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".docx", ".xlsx"};
            string[] allowedMimeTypes = { "image/jpeg", "image/png", "image/gif", "application/pdf", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" };
            List<string> fileUrls = new List<string>(); // 用于存储所有上传文件的 URL

            foreach (var file in files)
            {
                string fileExtension = Path.GetExtension(file.FileName).ToLower();
                string FileType = "";
                FileType = file.ContentType;
                // 检查文件扩展名和 MIME 类型
                if (!allowedExtensions.Contains(fileExtension) || !allowedMimeTypes.Contains(file.ContentType))
                {
                    return Ok(new
                    {
                        Code = "401",
                        Messge = "上传失败，格式错误！", // Invalid file type
                        FileUrls = fileUrls
                    });
                }

                // 确保上传文件路径不包含 ".." 来防止路径遍历
                if (file.FileName.Contains(".."))
                {
                    return Ok(new
                    {
                        Code = "402",
                        Messge = "上传失败，文件名错误！", // Invalid file name
                        FileUrls = fileUrls
                    });
                }

                // 验证文件的 Magic Number
                if (!IsValidFile(file))
                {
                    return Ok(new
                    {
                        Code = "403",
                        Messge = "上传失败，文件内容格式错误！", // Invalid file content
                        FileUrls = fileUrls
                    });
                }

                // 文件大小限制 (5MB)
                const long maxFileSize = 5 * 1024 * 1024; // 5MB
                if (file.Length > maxFileSize)
                {
                    return Ok(new
                    {
                        Code = "404",
                        Messge = "上传失败，文件超出大小限制！", // File size exceeds the 5MB limit.
                        FileUrls = fileUrls
                    });
                }

                // 保存文件
                try
                {
                    string baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploadfile");
                    string yearMonthFolder = Path.Combine(baseFolder, DateTime.Now.ToString("yyyy/MM").Replace("/", Path.DirectorySeparatorChar.ToString()));

                    // 如果文件夹不存在则创建
                    if (!Directory.Exists(yearMonthFolder))
                    {
                        Directory.CreateDirectory(yearMonthFolder);
                    }

                    string uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                    string filePath = Path.Combine(yearMonthFolder, uniqueFileName);

                    // 保存文件
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // 文件的公开 URL
                    string fileUrl = $"{Request.Scheme}://{Request.Host}/Uploadfile/{DateTime.Now:yyyy}/{DateTime.Now:MM}/{uniqueFileName}";
                    fileUrls.Add(fileUrl); // 添加到文件 URL 列表
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = "上传文件时发生错误", Error = ex.Message });
                }
            }

            // 返回所有上传的文件 URL
            return Ok(new
            {
                Code = "200",
                Messge = "文件上传成功",
                FileUrls = fileUrls
            });
        }


        //文件类型安全判断
        public bool IsValidFile(IFormFile file)
        {
            try
            {
                byte[] buffer = new byte[4];  // 读取前4个字节（Magic Number）
                using (var stream = file.OpenReadStream())
                {
                    // 读取文件的前4个字节
                    stream.Read(buffer, 0, buffer.Length);
                }

                string hex = BitConverter.ToString(buffer).Replace("-", string.Empty);
                Console.WriteLine(hex);
                // 验证文件类型的 Magic Number
                if (hex.StartsWith("FFD8FF")) return true; // JPEG
                if (hex.StartsWith("89504E47")) return true; // PNG
                if (hex.StartsWith("47494638")) return true; // GIF
                if (hex.StartsWith("25504446") && file.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase)) return true; // PDF
                if (file.FileName.EndsWith(".docx", StringComparison.OrdinalIgnoreCase) || file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    // DOCX 和 XLSX 文件是 ZIP 格式，检查文件开头是否是 "PK"
                    byte[] zipBuffer = new byte[2];
                    using (var stream = file.OpenReadStream())
                    {
                        stream.Read(zipBuffer, 0, zipBuffer.Length);
                    }
                    string zipHex = BitConverter.ToString(zipBuffer).Replace("-", string.Empty);
                    if (zipHex.StartsWith("504B")) return true; // ZIP (DOCX, XLSX)
                }

                return false; // 不符合已知格式
            }
            catch (Exception)
            {
                return false; // 如果读取失败，说明文件无效
            }
        }


    }
}
