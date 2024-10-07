using api.Data;
using api.DTOs.Events;
using api.DTOs.Responses;
using api.Repositories.Implementations.Mutations;
using api.Repositories.Implementations.Queries;
using api.Repositories.Interfaces;

namespace api.Handlers
{
  public class EventHandler(ApiContext context) : IEventRepo
  {
    public async Task<GeneralResponse> CreateEventAsync(ModifyEvent dto)
    {
      var mutation = new MutationEventssAsync(context);
      return await mutation.CreateAsync(dto);
    }

    public async Task<GeneralResponse> DeleteEventAsync(int id)
    {
      var mutation = new MutationEventssAsync(context);
      return await mutation.DeleteAsync(id);
    }

    public async Task<DataResponses<EventDetail>> GetEventByIdAsync(int id)
    {
      var query = new QueryEventAsync(context);
      return await query.GetByIdAsync(id);
    }

    public async Task<DataResponses<List<EventDetail>>> GetEventsAsync(EventRequestOptions requestOptions)
    {
      var query = new QueryEventsAsync(context);
      return await query.GetListAsync(requestOptions);
    }

    public async Task<GeneralResponse> UpdateEventAsync(int id, ModifyEvent dto)
    {
      var mutation = new MutationEventssAsync(context);
      return await mutation.UpdateAsync(id, dto);
    }
  }
}