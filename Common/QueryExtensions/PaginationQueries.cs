using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using MedChart.DataTransferObjects.Pagination;

namespace MedChart.Common.QueryExtensions
{
    public static class PaginationQueries
    {
        public static (IQueryable<ProjectT> pagedQuery, Func<int> countTotal) QueryPage<T, ProjectT>(
          this IQueryable<T> baseQuery,
          PaginationParams pagination,
          Expression<Func<T, bool>> filter = null,
          Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null,
          Expression<Func<T, ProjectT>> project = null
        ) where T : class
        {
            var (pagedQuery, countTotal) =
            QueryPage(baseQuery, pagination, filter, includes);

            return (pagedQuery.Select(project), countTotal);
        }

        public static (IQueryable<T> pagedQuery, Func<int> countTotal) QueryPage<T>(
          this IQueryable<T> baseQuery,
          PaginationParams pagination,
          Expression<Func<T, bool>> filter = null,
          Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null
        ) where T : class
        {
            if (pagination.PageNumber < 0)
                throw new ArgumentNullException(nameof(pagination.PageNumber));

            if (pagination.PageSize < 0)
                throw new ArgumentNullException(nameof(pagination.PageSize));

            var filteredAndIncludedQuery = FilterAndInclude(baseQuery, filter, includes);

            var pagedFilteredAndIncludedQuery =
              Paginate(filteredAndIncludedQuery, pagination.PageSize, (pagination.PageNumber - 1) * pagination.PageSize);

            int countTotal() => filteredAndIncludedQuery.Count();

            return (pagedQuery: pagedFilteredAndIncludedQuery, countTotal);
        }

        private static IQueryable<T> Paginate<T>(IQueryable<T> baseQuery, int size, int skipCount) =>
          skipCount == 0 ?
          baseQuery.Take(size) :
          baseQuery.Skip(skipCount).Take(size);

        private static IQueryable<T> FilterAndInclude<T>(
          IQueryable<T> baseQuery,
          Expression<Func<T, bool>> filter,
          Func<IQueryable<T>, IIncludableQueryable<T, object>> includes) where T : class
        {
            var queryIncluded = Include(baseQuery, includes);

            return filter != null ?
              queryIncluded.Where(filter) :
              queryIncluded;
        }

        private static IQueryable<T> Include<T>(
            IQueryable<T> baseQuery,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes) =>
          includes?.Invoke(baseQuery) ?? baseQuery;
    }
}
