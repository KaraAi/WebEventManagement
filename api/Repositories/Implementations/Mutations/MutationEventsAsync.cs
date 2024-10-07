using api.Constants.Exceptions;
using api.Data;
using api.DTOs.Events;
using api.DTOs.Responses;
using api.Handlers;
using api.Models;
using api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Implementations.Mutations
{
  public class MutationEventssAsync(ApiContext context) :
  IMutationHandler<EventDetail, ModifyEvent>
  {
    #region snippet_CreateAsync
    public async Task<GeneralResponse> CreateAsync(ModifyEvent model)
    {
      try
      {
        var existingEvent = await context.Events.Where(e => e.EventCode == model.EventCode).FirstOrDefaultAsync();

        if (existingEvent != null)
          throw new RequestGeneralException($"Event {model.EventCode} already exists");

        var existingName = await context.Events.Where(e => e.EventName == model.EventName).FirstOrDefaultAsync();

        if (existingName != null)
          throw new RequestGeneralException($"Event {model.EventName} already exists");

        foreach (var user in model.UserIDs)
        {
          var existingUser = await context.Users.Where(u => u.UserID == user).FirstOrDefaultAsync() ?? throw new RequestGeneralException($"User {user} not found");
        }

        var @event = new Events
        {
          EventCode = model.EventCode,
          EventName = model.EventName,
          UserCreated = model.UserCreated,
          UserUpdated = model.UserUpdated,
          DateCreated = model.DateCreated,
          DateUpdated = model.DateUpdated
        };

        await context.Events.AddAsync(@event);

        await context.SaveChangesAsync();

        return new GeneralResponse
        {
          Success = true,
          Message = $"Event {model.EventName} created successfully",
          StatusCode = 201
        };
      }
      catch (Exception ex)
      {
        return ExceptionHandler<Events>.MutationExceptionHandler(ex, 500);
      }
    }
    #endregion

    #region snippet_DeleteAsync
    public async Task<GeneralResponse> DeleteAsync(int id)
    {
      try
      {
        var existingEvent = await context.Events.Where(e => e.EventID == id).FirstOrDefaultAsync() ?? throw new RequestGeneralException($"Event {id} not found");

        context.Events.Remove(existingEvent);

        await context.SaveChangesAsync();

        return new GeneralResponse
        {
          Success = true,
          Message = $"Event {existingEvent.EventName} deleted successfully",
          StatusCode = 201
        };
      }
      catch (RequestGeneralException ex)
      {
        return ExceptionHandler<Events>.MutationExceptionHandler(ex, 404);
      }
      catch (Exception ex)
      {
        return ExceptionHandler<Events>.MutationExceptionHandler(ex, 500);
      }
    }
    #endregion

    #region snippet_UpdateAsync
    public async Task<GeneralResponse> UpdateAsync(int eventID, ModifyEvent model)
    {
      try
      {
        var existingEvent = await context.Events
          .Where(e => e.EventID == eventID)
          .Include(e => e.Users)
          .FirstOrDefaultAsync() ?? throw new RequestGeneralException($"Event {eventID} not found");

        var existingCode = await context.Events.Where(e => e.EventCode == model.EventCode).FirstOrDefaultAsync();

        if (existingCode != null && existingCode.EventID != eventID)
          throw new RequestGeneralException($"Event {model.EventCode} already exists");

        var existingName = await context.Events.Where(e => e.EventName == model.EventName).FirstOrDefaultAsync();

        if (existingName != null)
          throw new RequestGeneralException($"Event {model.EventName} already exists");

        foreach (var user in model.UserIDs)
        {
          var existingUser = await context.Users.Where(u => u.UserID == user).FirstOrDefaultAsync() ?? throw new RequestGeneralException($"User {user} not found");
        }

        existingEvent.EventCode = model.EventCode;
        existingEvent.EventName = model.EventName;
        existingEvent.DateCreated = model.DateCreated;
        existingEvent.DateUpdated = model.DateUpdated;
        existingEvent.UserCreated = model.UserCreated;
        existingEvent.UserUpdated = model.UserUpdated;

        var existingUserIds = existingEvent.Users.Select(u => u.UserID).ToList();
        var incomingUserIds = model.UserIDs;
        var usersToRemove = existingUserIds.Except(incomingUserIds).ToList();

        foreach (var user in usersToRemove)
        {
          var existingUser = await context.Users.Where(u => u.UserID == user).FirstOrDefaultAsync() ?? throw new RequestGeneralException($"User {user} not found");

          existingEvent.Users.Remove(existingUser);
        }

        foreach (var userId in model.UserIDs)
        {
          var tempId = userId;
          if (userId == 0)
          {
            tempId = Guid.NewGuid().GetHashCode(); // Using a temporary negative ID
          }

          var existingUser = await context.Users.Where(u => u.UserID == tempId).FirstOrDefaultAsync() ?? throw new RequestGeneralException($"User id:{tempId} not found");

          existingEvent.Users.Add(existingUser);
        }

        await context.SaveChangesAsync();

        return new GeneralResponse
        {
          Success = true,
          Message = $"Event {model.EventName} updated successfully",
          StatusCode = 201
        };
      }
      catch (Exception ex)
      {
        return ExceptionHandler<User>.MutationExceptionHandler(ex, 500);
      }
    }
    #endregion
  }
}