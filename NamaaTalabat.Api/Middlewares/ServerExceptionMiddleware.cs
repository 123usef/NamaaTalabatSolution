
using Microsoft.AspNetCore.Http.Json;
using NamaaTalabat.Api.Errors;
using System.Net;
using System.Text.Json;

namespace NamaaTalabat.Api.Middlewares
{
    //by convention
    public class ServerExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;
        private readonly IHostEnvironment env;

        public ServerExceptionMiddleware(RequestDelegate next , ILogger<ServerExceptionMiddleware> logger , IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext httpcontext)
        {

            try
            {
              await  next.Invoke(httpcontext);
            }
            catch (Exception ex)
            {   
                logger.LogError(ex, "a server Exception has been happened");
                httpcontext.Response.ContentType = "application/json";
                httpcontext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = env.IsDevelopment()?
                    new ServerErrorResponse((int)(int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                    :new ServerErrorResponse((int)(int)HttpStatusCode.InternalServerError, ex.Message);
                
                
                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response , options);

                await httpcontext.Response.WriteAsync(json );

                
            }


        }
    }
}
