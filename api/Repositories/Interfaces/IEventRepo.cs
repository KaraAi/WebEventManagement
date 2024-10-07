using api.DTOs.Events;
using api.DTOs.Responses;

namespace api.Repositories.Interfaces
{
  public interface IEventRepo
  {
    Task<DataResponses<List<EventDetail>>> GetEventsAsync(EventRequestOptions requestOptions);
    Task<DataResponses<EventDetail>> GetEventByIdAsync(int id);
    Task<GeneralResponse> CreateEventAsync(ModifyEvent dto);
    Task<GeneralResponse> UpdateEventAsync(int id, ModifyEvent dto);
    Task<GeneralResponse> DeleteEventAsync(int id);
  }
}