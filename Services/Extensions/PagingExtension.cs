using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Extensions
{
    public static class PagingExtension
    {
        const int DefaultPageSize = 10;

        public async static Task<PagedResult<T>> Paginate<T>(this IQueryable<T> query, int page, int perPage)
        {
            if (page <= 0) page = 1;
            if (perPage <= 0) perPage = DefaultPageSize;

            var resultCount = query.Count();
            var pageCount = (double)resultCount / perPage;
            pageCount = Math.Ceiling(pageCount);
            var skipCount = (page - 1) * perPage;

            return new PagedResult<T>
            {
                Paging = new Paging
                {
                    CurrentPage = page,
                    ResultCount = resultCount,
                    PageCount = (int)pageCount
                },
                Results = await query.Skip(skipCount).Take(perPage).ToListAsync()
            };
        }
    }
}
