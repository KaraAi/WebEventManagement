using api.Repositories.Interfaces;

namespace api.Routes
{
  public static class UserRoutes
  {
    public static void MapUserRoutes(this WebApplication app, IUserRepo userHandler)
    {
      var route = app.MapGroup("/users");

      route.MapPost("/", userHandler.GetUsersAsync)
        .WithSummary("Get all users")
        .WithName("GetUsers")
        .WithDescription("Get all users");

      route.MapPost("/{id}", userHandler.GetUserByIdAsync)
        .WithSummary("Get user by id")
        .WithName("GetUserById")
        .WithDescription("Get user by id");

      route.MapPost("/create", userHandler.CreateUserAsync)
        .WithSummary("Create user")
        .WithName("CreateUser")
        .WithDescription("Create user");

      route.MapPut("/{id}", userHandler.UpdateUserAsync)
        .WithSummary("Update user")
        .WithName("UpdateUser")
        .WithDescription("Update user");

      route.MapDelete("/{id}", userHandler.DeleteUserAsync)
        .WithSummary("Delete user")
        .WithName("DeleteUser")
        .WithDescription("Delete user");
    }
  }
}