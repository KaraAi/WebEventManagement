using api.DTOs.Responses;

namespace api.Repositories.Interfaces
{
  public interface IQueryHandler<T, Q>
  {
    Task<DataResponses<T>> GetListAsync(Q query);
  }
}