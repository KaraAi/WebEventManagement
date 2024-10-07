using api.Constants.Exceptions;
using api.Constants.Messages;
using api.Data;
using api.DTOs.Responses;
using api.DTOs.Users;
using api.Handlers;
using api.Models;
using api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Implementations.Mutations
{
  public class MutationUsersAsync(ApiContext context) : IMutationHandler<UserDetail, ModifyUser>
  {
    public async Task<GeneralResponse> CreateAsync(ModifyUser model)
    {
      try
      {
        var existingUser = await context.Users.Where(u => u.UserCode == model.UserCode).FirstOrDefaultAsync();

        if (existingUser != null)
          throw new RequestGeneralException($"User {model.UserCode} already exists");

        var existingEmail = await context.Users.Where(u => u.Email == model.Email).FirstOrDefaultAsync();

        if (existingEmail != null)
          throw new RequestGeneralException($"Email {model.Email} already exists");

        var existingCCCD = await context.Users.Where(u => u.CCCD == model.CCCD).FirstOrDefaultAsync();

        if (existingCCCD != null)
          throw new RequestGeneralException($"CCCD {model.CCCD} already exists");

        var existingPhone = await context.Users.Where(u => u.Phone == model.Phone).FirstOrDefaultAsync();

        if (existingPhone != null)
          throw new RequestGeneralException($"Phone {model.Phone} already exists");

        var existingEvent = await context.Events.Where(e => e.EventID == model.EventID).FirstOrDefaultAsync();

        if (existingEvent == null)
          throw new RequestGeneralException($"Event {model.EventID} not found");

        var user = new User
        {
          UserCode = model.UserCode,
          FullName = model.FullName,
          CCCD = model.CCCD,
          Phone = model.Phone,
          Facility = model.Facility,
          Office = model.Office,
          Email = model.Email,
          IsCheck = model.IsCheck,
          Description = model.Description,
          UserCreated = model.UserCreated,
          UserUpdated = model.UserUpdated,
          DateCreated = model.DateCreated,
          DateUpdated = model.DateUpdated,
          EventID = model.EventID
        };

        await context.Users.AddAsync(user);

        await context.SaveChangesAsync();

        return new GeneralResponse
        {
          Message = $"User {model.FullName} has been created successfully",
          Success = true,
          StatusCode = 201
        };
      }
      catch (Exception ex)
      {
        return ExceptionHandler<User>.MutationExceptionHandler(ex, 500);
      }
    }

    public async Task<GeneralResponse> DeleteAsync(int id)
    {
      try
      {
        var user = await context.Users.Where(u => u.UserID == id).FirstOrDefaultAsync() ?? throw new RequestGeneralException(ExceptionMessages.NotFound);

        context.Users.Remove(user);

        await context.SaveChangesAsync();

        return new GeneralResponse
        {
          Message = $"User {user.FullName} has been deleted successfully",
          Success = true,
          StatusCode = 200
        };
      }
      catch (Exception ex)
      {
        return ExceptionHandler<User>.MutationExceptionHandler(ex, 500);
      }
    }

    public async Task<GeneralResponse> UpdateAsync(int UserID, ModifyUser model)
    {
      try
      {
        var user = await context.Users.Where(u => u.UserID == UserID).FirstOrDefaultAsync() ?? throw new RequestGeneralException(ExceptionMessages.NotFound);

        var existingUser = await context.Users.Where(u => u.UserCode == model.UserCode).FirstOrDefaultAsync() ?? throw new RequestGeneralException($"User {model.UserCode} already exists");

        var existingEmail = await context.Users.Where(u => u.Email == model.Email).FirstOrDefaultAsync() ?? throw new RequestGeneralException($"Email {model.Email} already exists");

        var existingCCCD = await context.Users.Where(u => u.CCCD == model.CCCD).FirstOrDefaultAsync() ?? throw new RequestGeneralException($"CCCD {model.CCCD} already exists");

        var existingPhone = await context.Users.Where(u => u.Phone == model.Phone).FirstOrDefaultAsync() ?? throw new RequestGeneralException($"Phone {model.Phone} already exists");

        var existingEvent = await context.Events.Where(e => e.EventID == model.EventID).FirstOrDefaultAsync() ?? throw new RequestGeneralException($"Event {model.EventID} not found");

        user.UserCode = model.UserCode;
        user.FullName = model.FullName;
        user.CCCD = model.CCCD;
        user.Phone = model.Phone;
        user.Facility = model.Facility;
        user.Office = model.Office;
        user.Email = model.Email;
        user.IsCheck = model.IsCheck;
        user.Description = model.Description;
        user.UserCreated = model.UserCreated;
        user.UserUpdated = model.UserUpdated;
        user.DateCreated = model.DateCreated;
        user.DateUpdated = model.DateUpdated;
        user.EventID = model.EventID;

        await context.SaveChangesAsync();

        return new GeneralResponse
        {
          Message = $"User {model.FullName} has been updated successfully",
          Success = true,
          StatusCode = 200
        };
      }
      catch (Exception ex)
      {
        return ExceptionHandler<User>.MutationExceptionHandler(ex, 500);
      }
    }
  }
}