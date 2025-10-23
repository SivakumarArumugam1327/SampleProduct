using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IO;
using System.Security.Claims;
namespace Myproducts.Middlewares
{
    public class AuditLogsAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var username = context.HttpContext.User.Identity?.Name ?? "Anonymous";
            var actionName = context.ActionDescriptor.DisplayName;
            var timestamp = DateTime.UtcNow;

            var log = $"{timestamp:u} | User: {username} | Action: {actionName}\n";

            // Write to a file (you can replace with DB insert)
            File.AppendAllText("audit_log.txt", log);
        }
    }
}