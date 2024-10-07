using api.DTOs.Responses;
using api.DTOs.Users;

namespace api.Repositories.Interfaces
{
  public interface IUserRepo
  {
    Task<DataResponses<List<UserDetail>>> GetUsersAsync(UserRequestOptions requestOptions);
  }
}