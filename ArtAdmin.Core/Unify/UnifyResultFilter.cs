using Microsoft.AspNetCore.Mvc.Filters;

namespace ArtAdmin
{
    public class UnifyResultFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.HttpContext.WebSockets.IsWebSocketRequest)
                return;

            if (context.Result is ObjectResult)
            {
                var objectResult = context.Result as ObjectResult;
                if (objectResult.DeclaredType.IsGenericType && objectResult.DeclaredType?.GetGenericTypeDefinition() == typeof(UnifyResult<>))
                {
                    //TODO : 处理返回值,添加耗时
                    return;
                }
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}