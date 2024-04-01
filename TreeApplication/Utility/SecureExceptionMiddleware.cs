using Newtonsoft.Json;
using System.Net;
using TreeApplication.Context;
using TreeApplication.Models;

namespace TreeApplication.Utility
{
    public class SecureExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public SecureExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, TreeContext dbContext)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await LogException(ex, context, dbContext);
            }
        }

        private async Task LogException(Exception ex, HttpContext context, TreeContext dbContext)
        {
            var journalRecord = new MJournal
            {
                EventId = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                Text = $"{context.Request.Method} {context.Request.Path}{context.Request.QueryString}\n" +
                       $"Headers:\n{string.Join("\n", context.Request.Headers.Select(h => $"{h.Key}: {h.Value}"))}\n"
            };

            if (context.Request.Body.CanRead)
            {
                context.Request.EnableBuffering();
                context.Request.Body.Position = 0;
                string body = await new StreamReader(context.Request.Body).ReadToEndAsync();
                context.Request.Body.Position = 0;
                journalRecord.Text += $"Body:\n{body}\n";
            }

            journalRecord.Text += $"Exception:\n{ex.Message}";

            dbContext.MJournals.Add(journalRecord);
            await dbContext.SaveChangesAsync();

            if (ex is SecureException secureException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var response = new
                {
                    type = "Secure",
                    id = journalRecord.EventId,
                    data = new { message = secureException.Message }
                };
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var response = new
                {
                    type = "Exception",
                    id = journalRecord.EventId,
                    data = new { message = $"Internal server error ID = {journalRecord.EventId}" }
                };
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
        }
    }
}
