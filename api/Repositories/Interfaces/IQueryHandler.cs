using api.DTOs.Responses;

namespace api.Repositories.Interfaces
{
  public interface IQueryListHandler<T, Q>
  {
    Task<DataResponses<T>> GetListAsync(Q query);
  }
  public interface IQueryItemHandler<T>
  {
    Task<DataResponses<T>> GetByIdAsync(int id);
  }
}