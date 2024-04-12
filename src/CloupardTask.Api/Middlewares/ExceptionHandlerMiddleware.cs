using CloupardTask.Api.Commons.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace CloupardTask.Api.Commons.Middlewares
{
	public class ExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		public ExceptionHandlerMiddleware(RequestDelegate next)
		{
			_next = next;
		}
		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (StatusCodeException statusCodeException)
			{
				await HandlerAsync(statusCodeException, httpContext);
			}
			catch (Exception exception)
			{
				await HandlerOtherAsync(exception, httpContext);
			}
		}
		public async Task HandlerAsync(StatusCodeException statusCodeException, HttpContext httpContext)
		{
			httpContext.Response.StatusCode = (int)statusCodeException.StatusCode;
			httpContext.Response.ContentType = "application/json";
			string json = JsonConvert.SerializeObject(
				new { statusCodeException.StatusCode, statusCodeException.Message });

			await httpContext.Response.WriteAsync(json);
		}
		public async Task HandlerOtherAsync(Exception exception, HttpContext httpContext)
		{
			httpContext.Response.StatusCode = 500;
			httpContext.Response.ContentType = "application/json";
			string json = JsonConvert.SerializeObject(
				new { StatusCode = HttpStatusCode.InternalServerError, exception.Message });

			await httpContext.Response.WriteAsync(json);
		}
	}
}
