using api.Constants.Exceptions;
using api.Constants.Messages;
using api.Data;
using api.DTOs;
using api.DTOs.Events;
using api.DTOs.Responses;
using api.Handlers;
using api.Repositories.Implementations.Utils;
using api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Implementations.Queries
{
  #region snippet_GetListAsync
  public class QueryEventsAsync(ApiContext context) : IQueryListHandler<List<EventDetail>, EventRequestOptions>
  {
    public async Task<DataResponses<List<EventDetail>>> GetListAsync(EventRequestOptions queryOptions)
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

        var rawData = await context.Events
            .AsNoTracking()
            .AsQueryable()
            .Include(x => x.Users)
            .ToListAsync();

        var filters = new Dictionary<string, object?>
        {
            { nameof(EventDetail.EventID), queryOptions.Query.EventID },
            { nameof(EventDetail.EventCode), queryOptions.Query.EventCode },
            { nameof(EventDetail.EventName), queryOptions.Query.EventName },
            { nameof(EventDetail.UserCreated), queryOptions.Query.UserCreated },
            { nameof(EventDetail.UserUpdated), queryOptions.Query.UserUpdated },
            { nameof(EventDetail.DateCreated), queryOptions.Query.DateCreated },
            { nameof(EventDetail.DateUpdated), queryOptions.Query.DateUpdated },
        };

        var sorts = new Dictionary<string, bool>
        {
            { nameof(EventDetail.EventID), queryOptions.Sort.EventID },
            { nameof(EventDetail.EventCode), queryOptions.Sort.EventCode },
            { nameof(EventDetail.EventName), queryOptions.Sort.EventName },
            { nameof(EventDetail.UserCreated), queryOptions.Sort.UserCreated },
            { nameof(EventDetail.UserUpdated), queryOptions.Sort.UserUpdated },
            { nameof(EventDetail.DateCreated), queryOptions.Sort.DateCreated },
            { nameof(EventDetail.DateUpdated), queryOptions.Sort.DateUpdated },
        };

        rawData = RequestOptionProcess.ApplyFilters(rawData, filters);
        rawData = RequestOptionProcess.ApplySorting(rawData, sorts);

        var totalCount = rawData.Count;
        initialMetadata.TotalCount = totalCount;

        if (totalCount == 0)
        {
          return new DataResponses<List<EventDetail>>
          {
            Data = [],
            Message = "No data found",
            Success = true,
            Metadata = initialMetadata,
            StatusCode = 200
          };
        }

        var totalPages = (int)Math.Ceiling((double)totalCount / limit);
        initialMetadata.TotalPages = totalPages;

        RequestOptionProcess.ProcessPaginationOptions(page, limit, totalPages);

        rawData = rawData.Skip((page - 1) * limit).Take(limit).ToList();

        var data = rawData.Select(x => new EventDetail
        {
          EventID = x.EventID,
          EventCode = x.EventCode,
          EventName = x.EventName,
          UserCreated = x.UserCreated,
          UserUpdated = x.UserUpdated,
          DateCreated = x.DateCreated,
          DateUpdated = x.DateUpdated,
          Users = x.Users.Select(u => new EventUserDetail
          {
            UserID = u.UserID,
            UserCode = u.UserCode,
            FullName = u.FullName,
            CCCD = u.CCCD,
            Phone = u.Phone,
            Facility = u.Facility,
            Office = u.Office,
            Email = u.Email,
            IsCheck = u.IsCheck,
            Description = u.Description,
            UserCreated = u.UserCreated,
            UserUpdated = u.UserUpdated,
            DateCreated = u.DateCreated,
            DateUpdated = u.DateUpdated,
          }).ToList()
        }).ToList();

        return new DataResponses<List<EventDetail>>
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
        return ExceptionHandler<List<EventDetail>>.QueryExceptionHandler(new RequestLimitBehind5Exception(ExceptionMessages.RequestLimitBehind5), metadata: initialMetadata, 400);
      }
      catch (RequestLimitExceed50Exception)
      {
        return ExceptionHandler<List<EventDetail>>.QueryExceptionHandler(new RequestLimitExceed50Exception(ExceptionMessages.RequestLimitExceed50), metadata: initialMetadata, 400);
      }
      catch (RequestPageBehind1Exception)
      {
        return ExceptionHandler<List<EventDetail>>.QueryExceptionHandler(new RequestPageBehind1Exception(ExceptionMessages.RequestPageBehind1), metadata: initialMetadata, 400);
      }
      catch (RequestPageExceedTotalPagesException)
      {
        return ExceptionHandler<List<EventDetail>>.QueryExceptionHandler(new RequestPageExceedTotalPagesException(ExceptionMessages.RequestPageExceedTotalPages), metadata: initialMetadata, 400);
      }
      catch (Exception ex)
      {
        return ExceptionHandler<List<EventDetail>>.QueryExceptionHandler(ex, metadata: initialMetadata, 500);
      }
    }
  }
  #endregion
  #region snippet_GetDetailAsync
  public class QueryEventAsync(ApiContext context) :
  IQueryItemHandler<EventDetail>
  {
    public async Task<DataResponses<EventDetail>> GetByIdAsync(int id)
    {
      try
      {
        var data = await context.Events
            .AsNoTracking()
            .AsQueryable()
            .Include(x => x.Users)
            .Where(x => x.EventID == id)
            .FirstOrDefaultAsync() ?? throw new RequestGeneralException(ExceptionMessages.NotFound);

        var detail = new EventDetail
        {
          EventID = data.EventID,
          EventCode = data.EventCode,
          EventName = data.EventName,
          UserCreated = data.UserCreated,
          UserUpdated = data.UserUpdated,
          DateCreated = data.DateCreated,
          DateUpdated = data.DateUpdated,
          Users = data.Users.Select(u => new EventUserDetail
          {
            UserID = u.UserID,
            UserCode = u.UserCode,
            FullName = u.FullName,
            CCCD = u.CCCD,
            Phone = u.Phone,
            Facility = u.Facility,
            Office = u.Office,
            Email = u.Email,
            IsCheck = u.IsCheck,
            Description = u.Description,
            UserCreated = u.UserCreated,
            UserUpdated = u.UserUpdated,
            DateCreated = u.DateCreated,
            DateUpdated = u.DateUpdated,
          }).ToList()
        };

        return new DataResponses<EventDetail>
        {
          Data = detail,
          Message = "Success",
          Success = true,
          StatusCode = 200
        };
      }
      catch (RequestGeneralException ex)
      {
        return ExceptionHandler<EventDetail>.QueryExceptionHandler(ex, metadata: null, 404);
      }
      catch (Exception ex)
      {
        return ExceptionHandler<EventDetail>.QueryExceptionHandler(ex, metadata: null, 500);
      }
    }
  }
  #endregion
}