using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Myproducts.Middlewares
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // Capture exception details
            var exception = context.Exception;
            var message = exception.Message;
            var stackTrace = exception.StackTrace;

            // Log the exception (optional — can use a file, DB, or console)
            Console.WriteLine($"❌ Exception caught: {message}\n{stackTrace}");

            // Create a standard response
            var result = new ObjectResult(new
            {
                StatusCode = 500,
                Error = "An unexpected error occurred.",
                Details = message
            })
            {
                StatusCode = 500
            };

            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}