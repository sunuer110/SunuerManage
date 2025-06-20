using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net;

public static class HttpContextHelper
{
    /// <summary>
    /// Project:Sunuer Manage
    /// Description:获取IP
    /// Author：HaiDong
    /// Site:https://www.sunuer.com
    /// Version: 1.0
    /// License：Apache License 2.0
    /// </summary>
    private static IHttpContextAccessor _httpContextAccessor;

    public static void Configure(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public static HttpContext Current => _httpContextAccessor?.HttpContext;

    /// <summary>
    /// 获取用户的 IP 地址（优先外网 IP）
    /// </summary>
    /// <returns>IP 地址字符串</returns>
    public static string GetUserIp()
    {
        var httpContext = Current;
        if (httpContext == null)
            return "Unknown";

        // 优先从 X-Forwarded-For 获取
        if (httpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
        {
            var header = httpContext.Request.Headers["X-Forwarded-For"].ToString();
            if (!string.IsNullOrWhiteSpace(header))
            {
                var ips = header.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var ip in ips)
                {
                    var trimmedIp = ip.Trim();
                    if (IsValidIpAddress(trimmedIp) && !IsPrivateIp(trimmedIp))
                        return trimmedIp;
                }
            }
        }

        // 退而取 RemoteIp
        var remoteIp = httpContext.Connection.RemoteIpAddress?.ToString();
        return !string.IsNullOrEmpty(remoteIp) && IsValidIpAddress(remoteIp)
            ? remoteIp
            : "Unknown";
    }

    private static bool IsValidIpAddress(string ip)
    {
        return IPAddress.TryParse(ip, out _);
    }

    private static bool IsPrivateIp(string ip)
    {
        if (!IPAddress.TryParse(ip, out var address))
            return false;

        byte[] bytes = address.GetAddressBytes();
        if (bytes.Length == 4) // IPv4
        {
            switch (bytes[0])
            {
                case 10:
                case 127:
                    return true;
                case 172:
                    return bytes[1] >= 16 && bytes[1] <= 31;
                case 192:
                    return bytes[1] == 168;
            }
        }
        else if (bytes.Length == 16) // IPv6 loopback or local
        {
            return address.IsIPv6LinkLocal || address.IsIPv6SiteLocal;
        }

        return false;
    }
}