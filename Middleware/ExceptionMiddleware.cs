using CompanyManagement.Helpers;
using Newtonsoft.Json;
using System.Net;

namespace CompanyManagement.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;   

                var response = new BaseResponse
                {
                    Success = false,
                    Message = "Internal server error has occurred.",
                    Code = context.Response.StatusCode 
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }

            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                context.Response.ContentType = "application/json";
                var response = new BaseResponse
                {
                    Success = false,
                    Message = "Unauthorized access. Invalid credentials provided.",
                    Code = context.Response.StatusCode
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
   
        }
    }
}
