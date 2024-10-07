using api.DTOs.Responses;

namespace api.Repositories.Interfaces
{
  public interface IMutationHandler<T, M>
  {
    Task<GeneralResponse> CreateAsync(M model);
    Task<GeneralResponse> UpdateAsync(int id, M model);
    Task<GeneralResponse> DeleteAsync(int id);
  }
}