using api.Repositories.Interfaces;

namespace api.Routes
{
  public static class EventRoutes
  {
    public static void MapEventRoutes(this WebApplication app, IEventRepo eventHandler)
    {
      var route = app.MapGroup("/events");

      route.MapPost("/", eventHandler.GetEventsAsync)
        .WithSummary("Get all events")
        .WithName("GetEvents")
        .WithDescription("Get all events");

      route.MapPost("/{id}", eventHandler.GetEventByIdAsync)
        .WithSummary("Get event by id")
        .WithName("GetEventById")
        .WithDescription("Get event by id");

      route.MapPost("/create", eventHandler.CreateEventAsync)
        .WithSummary("Create event")
        .WithName("CreateEvent")
        .WithDescription("Create event");

      route.MapPut("/{id}", eventHandler.UpdateEventAsync)
        .WithSummary("Update event")
        .WithName("UpdateEvent")
        .WithDescription("Update event");

      route.MapDelete("/{id}", eventHandler.DeleteEventAsync)
        .WithSummary("Delete event")
        .WithName("DeleteEvent")
        .WithDescription("Delete event");
    }
  }
}