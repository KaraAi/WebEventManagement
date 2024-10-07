using api.DTOs.Responses;
using api.DTOs.Users;

namespace api.Repositories.Interfaces
{
  public interface IUserRepo
  {
    Task<DataResponses<List<UserDetail>>> GetUsersAsync(UserRequestOptions requestOptions);
    Task<DataResponses<UserDetail>> GetUserByIdAsync(int id);
    Task<GeneralResponse> CreateUserAsync(ModifyUser user);
    Task<GeneralResponse> UpdateUserAsync(int id, ModifyUser user);
    Task<GeneralResponse> DeleteUserAsync(int id);
  }
}