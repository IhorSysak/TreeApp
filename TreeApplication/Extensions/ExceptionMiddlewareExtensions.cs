using TreeApplication.Utility;

namespace TreeApplication.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExeptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<SecureExceptionMiddleware>();
        }
    }
}
