using api.Constants.Exceptions;
using api.Constants.Messages;
using api.Data;
using api.DTOs;
using api.DTOs.Responses;
using api.DTOs.Users;
using api.Handlers;
using api.Repositories.Implementations.Utils;
using api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Implementations.Get
{
  public class GetAsync(ApiContext context) : IQueryHandler<List<UserDetail>, UserRequestOptions>
  {
    public async Task<DataResponses<List<UserDetail>>> GetListAsync(UserRequestOptions queryOptions)
    {
      var initialMetadata = new Metadata
      {
        TotalCount = 0,
        TotalPages = 0,
        Page = 0,
        PageSize = 0,
      };

      try
      {
        var limit = queryOptions.Limit;
        var page = queryOptions.Page;

        initialMetadata.PageSize = limit;
        initialMetadata.Page = page;

        var rawData = await context.Users
            .AsNoTracking()
            .AsQueryable()
            .Include(x => x.Events)
            .ToListAsync();

        var filters = new Dictionary<string, object?>
        {
            { nameof(UserDetail.UserID), queryOptions.Query.UserID },
            { nameof(UserDetail.UserCode), queryOptions.Query.UserCode },
            { nameof(UserDetail.FullName), queryOptions.Query.FullName },
            { nameof(UserDetail.CCCD), queryOptions.Query.CCCD },
            { nameof(UserDetail.Phone), queryOptions.Query.Phone },
            { nameof(UserDetail.Facility), queryOptions.Query.Facility },
            { nameof(UserDetail.Office), queryOptions.Query.Office },
            { nameof(UserDetail.Email), queryOptions.Query.Email },
            { nameof(UserDetail.IsCheck), queryOptions.Query.IsCheck },
            { nameof(UserDetail.Description), queryOptions.Query.Description },
            { nameof(UserDetail.UserCreated), queryOptions.Query.UserCreated },
            { nameof(UserDetail.UserUpdated), queryOptions.Query.UserUpdated },
            { nameof(UserDetail.DateCreated), queryOptions.Query.DateCreated },
            { nameof(UserDetail.DateUpdated), queryOptions.Query.DateUpdated },
            { nameof(UserDetail.Event.EventID), queryOptions.Query.EventID },
            { nameof(UserDetail.Event.EventCode), queryOptions.Query.EventCode },
            { nameof(UserDetail.Event.EventName), queryOptions.Query.EventName }
        };

        var sorts = new Dictionary<string, bool>
        {
            { nameof(UserDetail.UserID), queryOptions.Sort.UserID },
            { nameof(UserDetail.UserCode), queryOptions.Sort.UserCode },
            { nameof(UserDetail.FullName), queryOptions.Sort.FullName },
            { nameof(UserDetail.CCCD), queryOptions.Sort.CCCD },
            { nameof(UserDetail.Phone), queryOptions.Sort.Phone },
            { nameof(UserDetail.Facility), queryOptions.Sort.Facility },
            { nameof(UserDetail.Office), queryOptions.Sort.Office },
            { nameof(UserDetail.Email), queryOptions.Sort.Email },
            { nameof(UserDetail.IsCheck), queryOptions.Sort.IsCheck },
            { nameof(UserDetail.Description), queryOptions.Sort.Description },
            { nameof(UserDetail.UserCreated), queryOptions.Sort.UserCreated },
            { nameof(UserDetail.UserUpdated), queryOptions.Sort.UserUpdated },
            { nameof(UserDetail.DateCreated), queryOptions.Sort.DateCreated },
            { nameof(UserDetail.DateUpdated), queryOptions.Sort.DateUpdated },
            { nameof(UserDetail.Event.EventID), queryOptions.Sort.EventId },
            { nameof(UserDetail.Event.EventCode), queryOptions.Sort.EventCode },
            { nameof(UserDetail.Event.EventName), queryOptions.Sort.EventName }
        };

        RequestOptionProcess.ApplyFilters(rawData, filters);
        RequestOptionProcess.ApplySorting(rawData, sorts);

        var totalCount = rawData.Count;
        var totalPages = (int)Math.Ceiling((double)totalCount / limit);

        initialMetadata.TotalCount = totalCount;
        initialMetadata.TotalPages = totalPages;

        RequestOptionProcess.ProcessPaginationOptions(page, limit, totalPages);

        rawData = rawData.Skip((page - 1) * limit).Take(limit).ToList();

        var data = rawData.Select(x => new UserDetail
        {
          UserID = x.UserID,
          UserCode = x.UserCode,
          FullName = x.FullName,
          CCCD = x.CCCD,
          Phone = x.Phone,
          Facility = x.Facility,
          Office = x.Office,
          Email = x.Email,
          IsCheck = x.IsCheck,
          Description = x.Description,
          UserCreated = x.UserCreated,
          UserUpdated = x.UserUpdated,
          DateCreated = x.DateCreated,
          DateUpdated = x.DateUpdated,
          Event = x.Events != null ? new UserEventDetail
          {
            EventID = x.Events.EventID,
            EventCode = x.Events.EventCode,
            EventName = x.Events.EventName,
            UserCreated = x.Events.UserCreated,
            UserUpdated = x.Events.UserUpdated,
            DateCreated = x.Events.DateCreated,
            DateUpdated = x.Events.DateUpdated
          } : null
        }).ToList();

        return new DataResponses<List<UserDetail>>
        {
          Data = data,
          Message = "Success",
          Success = true,
          Metadata = initialMetadata,
          StatusCode = 200
        };
      }
      catch (RequestLimitBehind5Exception)
      {
        return ExceptionHandler<List<UserDetail>>.DataExceptionHandler(new RequestLimitBehind5Exception(ExceptionMessages.RequestLimitBehind5), metadata: initialMetadata, 400);
      }
      catch (RequestLimitExceed50Exception)
      {
        return ExceptionHandler<List<UserDetail>>.DataExceptionHandler(new RequestLimitExceed50Exception(ExceptionMessages.RequestLimitExceed50), metadata: initialMetadata, 400);
      }
      catch (RequestPageBehind1Exception)
      {
        return ExceptionHandler<List<UserDetail>>.DataExceptionHandler(new RequestPageBehind1Exception(ExceptionMessages.RequestPageBehind1), metadata: initialMetadata, 400);
      }
      catch (RequestPageExceedTotalPagesException)
      {
        return ExceptionHandler<List<UserDetail>>.DataExceptionHandler(new RequestPageExceedTotalPagesException(ExceptionMessages.RequestPageExceedTotalPages), metadata: initialMetadata, 400);
      }
      catch (Exception ex)
      {
        return ExceptionHandler<List<UserDetail>>.DataExceptionHandler(ex, metadata: initialMetadata, 500);
      }
    }
  }
}