using System.Diagnostics;

namespace ArtAdmin
{
    public class UnifyResultStopwatchMiddleware
    {
        private readonly RequestDelegate _next;

        public UnifyResultStopwatchMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                App.HttpContext.Items["__StopwatchMiddlewareWithEndpointBegin"] = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                await _next(context);
            }
            catch (Exception)
            {
                sw.Stop();
                context.Items["__StopwatchMiddlewareWithEndpointExceptioned"] = sw.ElapsedMilliseconds;
                throw;
            }
        }
    }
}