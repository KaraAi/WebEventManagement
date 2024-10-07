using api.Constants.Exceptions;
using api.DTOs.Users;
using api.Models;

namespace api.Repositories.Implementations.Utils
{
  public class RequestOptionProcess
  {
    public static void ProcessPaginationOptions(int page, int limit, int totalPages)
    {
      if (limit < 5)
        throw new RequestLimitBehind5Exception();
      if (limit > 50)
        throw new RequestLimitExceed50Exception();
      if (page < 1)
        throw new RequestPageBehind1Exception();
      if (page > totalPages)
        throw new RequestPageExceedTotalPagesException();
    }

    // Generic filtering method
    public static List<T> ApplyFilters<T>(List<T> query, Dictionary<string, object?> filters)
    {
      foreach (var filter in filters)
      {
        var property = typeof(T).GetProperty(filter.Key);
        if (property == null || filter.Value == null) continue;

        query = query.Where(item =>
          property.GetValue(item)?.ToString()?.Equals(filter.Value.ToString(), StringComparison.CurrentCultureIgnoreCase) == true
        ).ToList();
      }

      return query;
    }

    // Generic sorting method
    public static List<T> ApplySorting<T>(List<T> query, Dictionary<string, bool> sortOptions)
    {
      IOrderedEnumerable<T>? orderedQuery = null;

      foreach (var sortOption in sortOptions)
      {
        var property = typeof(T).GetProperty(sortOption.Key);
        if (property == null) continue;

        if (orderedQuery == null)
        {
          orderedQuery = sortOption.Value
            ? query.OrderBy(item => property.GetValue(item))
            : query.OrderByDescending(item => property.GetValue(item));
        }
        else
        {
          orderedQuery = sortOption.Value
            ? orderedQuery.ThenBy(item => property.GetValue(item))
            : orderedQuery.ThenByDescending(item => property.GetValue(item));
        }
      }

      return orderedQuery?.ToList() ?? query;
    }
  }
}