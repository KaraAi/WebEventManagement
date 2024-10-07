using api.Data;
using api.DTOs.Responses;
using api.DTOs.Users;
using api.Repositories.Implementations.Get;
using api.Repositories.Interfaces;

namespace api.Handlers
{
  public class UserHandler(ApiContext context) : IUserRepo
  {
    public async Task<DataResponses<List<UserDetail>>> GetUsersAsync(UserRequestOptions requestOptions)
    {
      var query = new GetAsync(context);
      return await query.GetListAsync(requestOptions);
    }
  }
}