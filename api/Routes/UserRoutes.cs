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
    }
  }
}