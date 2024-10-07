using api.Data;
using api.DTOs.Responses;
using api.DTOs.Users;
using api.Repositories.Implementations.Mutations;
using api.Repositories.Implementations.Queries;
using api.Repositories.Interfaces;

namespace api.Handlers
{
  public class UserHandler(ApiContext context) : IUserRepo
  {
    public async Task<GeneralResponse> CreateUserAsync(ModifyUser user)
    {
      var mutation = new MutationUsersAsync(context);
      return await mutation.CreateAsync(user);
    }

    public async Task<GeneralResponse> DeleteUserAsync(int id)
    {
      var mutation = new MutationUsersAsync(context);
      return await mutation.DeleteAsync(id);
    }

    public async Task<DataResponses<UserDetail>> GetUserByIdAsync(int id)
    {
      var query = new QueryUserAsync(context);
      return await query.GetByIdAsync(id);
    }

    public async Task<DataResponses<List<UserDetail>>> GetUsersAsync(UserRequestOptions requestOptions)
    {
      var query = new QueryUsersAsync(context);
      return await query.GetListAsync(requestOptions);
    }

    public async Task<GeneralResponse> UpdateUserAsync(int userID, ModifyUser user)
    {
      var mutation = new MutationUsersAsync(context);
      return await mutation.UpdateAsync(userID, user);
    }
  }
}