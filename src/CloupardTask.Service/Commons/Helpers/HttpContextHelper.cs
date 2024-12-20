using Microsoft.AspNetCore.Http;

namespace CloupardTask.Api.Commons.Helpers
{
    public class HttpContextHelper
    {
        public static IHttpContextAccessor Accessor = null!;
        public static HttpResponse Response => Accessor.HttpContext.Response;
        public static HttpRequest Request => Accessor.HttpContext.Request;
        public static IHeaderDictionary ResponseHeaders => Response.Headers;
        public static IHeaderDictionary RequestHeaders => Response.Headers;
    }
}
