using System.Net;
using AutoMapper;
using Domain.Models.Entity_s;
using Domain.Models.Entity;
using Domain.Models.Filters;
using Infastructure.Data;
using Infastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebAPIWithJWTAndIdentity.Response;

namespace Infastructure.Services;

public class UserNotificationService(ApplicationDbContext context, IMapper mapper,IMemoryCache _cache ) : IUserNotificationService
{
    public async Task<Response<List<UserNotifDto>>> GetUserNotifsAsync(UserNotifFilter filter)
    {
        /*try
       {*/
        string cacheKey = $"UserNotification_test";

        if (!_cache.TryGetValue(cacheKey, out List<UserNotifDto> cachedResult))
        {
            var query = context.UserNotifications.AsQueryable();

            if (filter.Id.HasValue)
            {
                query = query.Where(x => x.Id == filter.Id.Value);
            }
            if (!string.IsNullOrEmpty(filter.Message))
            {
                query = query.Where(x => x.Message.Contains(filter.Message));
            }
            if (filter.CreatedAt.HasValue)
            {
                query = query.Where(x => x.CreatedAt == filter.CreatedAt.Value);
            }
            if (filter.InRead.HasValue)
            {
                query = query.Where(x => x.InRead == filter.InRead.Value);
            }
            if (filter.Expired.HasValue)
            {
                query = query.Where(x => x.Expired == filter.Expired.Value);
            }
            if (filter.NotificationId.HasValue)
            {
                query = query.Where(x => x.NotificationId == filter.NotificationId.Value);
            }
            var todoitem = await query.ToListAsync();
            cachedResult = mapper.Map<List<UserNotifDto>>(todoitem);

            _cache.Set(cacheKey, cachedResult);
        }

        return new Response<List<UserNotifDto>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "User Notification retrieved successfully!",
            Data = cachedResult
        };
        /*}*/
        /*catch (Exception ex)
        {
            return new Response<List<TodoItemDto>>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }*/
    }

    public async Task<Response<UserNotifDto>> AddUserNotifAsync(UserNotifCreateDto userNotifCreateDto)
    {
        /*try
      {*/
        var ad = mapper.Map<UserNotification>(userNotifCreateDto);
        _cache.Remove("UserNotification_test");
        context.UserNotifications.Add(ad);
        await context.SaveChangesAsync();
        var result = mapper.Map<UserNotifDto>(ad);
        return new Response<UserNotifDto>(HttpStatusCode.Created, "User Notification created successfully!", result);
        /*}*/
        /*catch (Exception ex)
        {
            return new Response<TodoItemDto>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }*/
    }

    public async Task<Response<UserNotifDto>> UpdateUserNotifAsync(UserNotifDto userNotifDto)
    {
        /*try
    {*/
        var check = await context.UserNotifications.FindAsync(userNotifDto.Id);
        if (check == null)
            return new Response<UserNotifDto>(HttpStatusCode.NotFound, "User Notification not found");
            
        check.Message = userNotifDto.Message;
        check.InRead = userNotifDto.InRead;
        check.Expired = userNotifDto.Expired;
        check.NotificationId = userNotifDto.NotificationId;
        _cache.Remove("UserNotification_test");
        _cache.Remove($"UserNotification_{userNotifDto.Id}");
        await context.SaveChangesAsync();
        var result = mapper.Map<NotifTypeDto>(check);
        return new Response<UserNotifDto>(HttpStatusCode.OK, "User Notification updated successfully!", result);
        /*}*/
        /*catch (Exception ex)
        {
            return new Response<TodoItemDto>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }*/
    }

    public async Task<Response<string>> DeleteUserNotifAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<UserNotifDto>> GetUserNotifByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}