using CloupardTask.Api.Commons.Helpers;
using CloupardTask.Api.Commons.Utils;

namespace CloupardTask.Api.Commons.Extensions
{
	public static class CollectionExtensions
	{
		public static IEnumerable<TSource> ToPagedAsEnumerable<TSource>(this IQueryable<TSource> sources,
			PaginationParams? @params)
		{
			var totalCount = sources.Count();
			SetTotalCountHeader(totalCount);

			return @params is { PageSize: > 0, PageIndex: > 0 }
				? sources.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize)
				: sources;
		}

		public static IQueryable<TSource> ToPagedAsQueryable<TSource>(this IQueryable<TSource> sources,
			PaginationParams? @params)
		{
			var totalCount = sources.Count();
			SetTotalCountHeader(totalCount);

			return @params is { PageSize: > 0, PageIndex: > 0 }
				? sources.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize)
				: sources;
		}

		private static void SetTotalCountHeader(int totalCount)
		{
			var responseHeaders = HttpContextHelper.ResponseHeaders;
			const string totalCountHeader = "total-count";

			if (responseHeaders.ContainsKey(totalCountHeader))
			{
				responseHeaders[totalCountHeader] = totalCount.ToString();
			}
			else
			{
				responseHeaders.Add(totalCountHeader, totalCount.ToString());
			}
		}

		public static IEnumerable<TSource> ToPagedAsEnumerable<TSource>(this IEnumerable<TSource> sources,
			PaginationParams? @params)
		{
			var totalCount = sources.Count();
			SetTotalCountHeader(totalCount);

			return @params is { PageSize: > 0, PageIndex: > 0 }
				? sources.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize)
				: sources;
		}
	}
}
