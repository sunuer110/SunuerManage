﻿using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
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
        public async Task<IActionResult> Upload(IFormFile file, [FromQuery] string? name)
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
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".doc", ".xls", ".docx", ".xlsx", ".zip", ".mp4", ".mpeg", ".mpg", ".ogv", ".webm" };
            string fileExtension = Path.GetExtension(file.FileName).ToLower();
            string[] allowedMimeTypes = {
    "image/jpeg",
    "image/png",
    "image/gif",
    "application/pdf",
    "application/msword", // 对应 .doc
    "application/vnd.ms-excel", // 对应 .xls
    "application/vnd.openxmlformats-officedocument.wordprocessingml.document", // 对应 .docx
    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", // 对应 .xlsx
    "application/zip" // 如果你要允许 .zip 文件
    , "application/octet-stream",
    "application/octet-stream", // 兼容浏览器上传
    "video/mp4",   // 对应 .mp4
    "video/mpeg",  // 对应 .mpeg 和 .mpg
    "video/ogg",   // 对应 .ogv
    "video/webm"   // 对应 .webm
};
            string FileType = "";
            FileType = fileExtension;
            // 检查文件扩展名和 MIME 类型
            if (!allowedExtensions.Contains(fileExtension) || !allowedMimeTypes.Contains(file.ContentType.ToLower()))
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
            if (!await IsValidFile(file, fileExtension))
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
                    Messge = "上传失败，文件超出大小限制！",//File size exceeds the 5MB limit.
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
            // 限制文件类型
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".doc", ".xls", ".docx", ".xlsx", ".zip", ".mp4", ".mpeg", ".mpg", ".ogv", ".webm" };
            string[] allowedMimeTypes = {
    "image/jpeg",
    "image/png",
    "image/gif",
    "application/pdf",
    "application/msword", // 对应 .doc
    "application/vnd.ms-excel", // 对应 .xls
    "application/vnd.openxmlformats-officedocument.wordprocessingml.document", // 对应 .docx
    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", // 对应 .xlsx
    "application/zip" // 如果你要允许 .zip 文件
    , "application/octet-stream",
    "application/octet-stream", // 兼容浏览器上传
    "video/mp4",   // 对应 .mp4
    "video/mpeg",  // 对应 .mpeg 和 .mpg
    "video/ogg",   // 对应 .ogv
    "video/webm"   // 对应 .webm
};
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
                if (!await IsValidFile(file, fileExtension))
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
        private async Task<bool> IsValidFile(IFormFile file, string extension)
        {
            try
            {
                Task<string> type = DetectRealFileExtension(file);
                byte[] buffer = new byte[8]; // 读前8字节
                using (var stream = file.OpenReadStream())
                {
                    await stream.ReadAsync(buffer, 0, buffer.Length);
                }
                // 根据扩展名检查文件头（magic bytes）
                if (type.Result == ".jpg" || type.Result == ".jpeg")
                    return buffer[0] == 0xFF && buffer[1] == 0xD8;
                if (type.Result == ".png")
                    return buffer[0] == 0x89 && buffer[1] == 0x50 && buffer[2] == 0x4E && buffer[3] == 0x47;
                if (type.Result == ".gif")
                    return buffer[0] == 0x47 && buffer[1] == 0x49 && buffer[2] == 0x46;
                if (type.Result == ".pdf")
                    return buffer[0] == 0x25 && buffer[1] == 0x50 && buffer[2] == 0x44 && buffer[3] == 0x46;
                if (type.Result == ".zip" || type.Result == ".docx" || type.Result == ".xlsx")
                    return buffer[0] == 0x50 && buffer[1] == 0x4B; // zip头
                if (type.Result == ".doc" || type.Result == ".xls")
                    return buffer[0] == 0xD0 && buffer[1] == 0xCF && buffer[2] == 0x11 && buffer[3] == 0xE0; // 旧版Office格式头
                                                                                                             // 视频文件头检查
                if (type.Result == ".mp4")
                    return buffer[4] == 0x66 && buffer[5] == 0x74 && buffer[6] == 0x79 && buffer[7] == 0x70; // "ftyp"
                if (type.Result == ".mpeg" || type.Result == ".mpg")
                    return buffer[0] == 0x00 && buffer[1] == 0x00 && buffer[2] == 0x01 && (buffer[3] == 0xBA || buffer[3] == 0xB3);
                if (type.Result == ".ogv")
                    return buffer[0] == 0x4F && buffer[1] == 0x67 && buffer[2] == 0x67 && buffer[3] == 0x53; // "OggS"
                if (type.Result == ".webm")
                    return buffer[0] == 0x1A && buffer[1] == 0x45 && buffer[2] == 0xDF && buffer[3] == 0xA3; // Matroska (webm是它的子集)

            }
            catch
            {
                return false; // 出错也判失败
            }

            // 其他文件默认通过
            return true;
        }

        public async Task<string> DetectRealFileExtension(IFormFile file)
        {
            byte[] buffer = new byte[12]; // 最大需要判断前 12 字节
            using (var stream = file.OpenReadStream())
            {
                await stream.ReadAsync(buffer, 0, buffer.Length);
            }

            // PNG
            if (buffer.Take(8).SequenceEqual(new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }))
                return ".png";

            // JPEG
            if (buffer[0] == 0xFF && buffer[1] == 0xD8)
                return ".jpg";

            // GIF
            if (buffer[0] == 0x47 && buffer[1] == 0x49 && buffer[2] == 0x46)
                return ".gif";

            // PDF
            if (buffer.Take(4).SequenceEqual(new byte[] { 0x25, 0x50, 0x44, 0x46 }))
                return ".pdf";

            // ZIP / DOCX / XLSX
            if (buffer[0] == 0x50 && buffer[1] == 0x4B)
                return ".zip";

            // DOC / XLS (老版 Office)
            if (buffer.Take(4).SequenceEqual(new byte[] { 0xD0, 0xCF, 0x11, 0xE0 }))
                return ".doc";

            // MP4 (查找 ftyp)
            if (buffer.Length >= 8 &&
                buffer[4] == 0x66 && buffer[5] == 0x74 && buffer[6] == 0x79 && buffer[7] == 0x70)
                return ".mp4";

            // WebM
            if (buffer.Take(4).SequenceEqual(new byte[] { 0x1A, 0x45, 0xDF, 0xA3 }))
                return ".webm";

            // OGV
            if (buffer.Take(4).SequenceEqual(new byte[] { 0x4F, 0x67, 0x67, 0x53 }))
                return ".ogv";

            // MPEG
            if (buffer[0] == 0x00 && buffer[1] == 0x00 && buffer[2] == 0x01 &&
                (buffer[3] == 0xBA || buffer[3] == 0xB3))
                return ".mpg";

            // 未识别类型
            return "unknown";
        }
    }

}
