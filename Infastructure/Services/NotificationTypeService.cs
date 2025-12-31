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

public class NotificationTypeService(ApplicationDbContext context, IMapper mapper,IMemoryCache _cache ) : INotificationTypeService
{
    public async Task<Response<List<NotifTypeDto>>> GetNotifTypeAsync(NotifTypeFilter filter)
    {
        /*try
        {*/
        string cacheKey = $"NotificationType_test";

        if (!_cache.TryGetValue(cacheKey, out List<NotifTypeDto> cachedResult))
        {
            var query = context.NotificationTypes.AsQueryable();

            if (filter.Id.HasValue)
            {
                query = query.Where(x => x.Id == filter.Id.Value);
            }
            if (!string.IsNullOrEmpty(filter.Type))
            {
                query = query.Where(x => x.Type.Contains(filter.Type));
            }

            var todoitem = await query.AsNoTracking().ToListAsync();
            cachedResult = mapper.Map<List<NotifTypeDto>>(todoitem);

            _cache.Set(cacheKey, cachedResult);
        }

        return new Response<List<NotifTypeDto>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Advertisements retrieved successfully!",
            Data = cachedResult
        };
        /*}*/
        /*catch (Exception ex)
        {
            return new Response<List<TodoItemDto>>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }*/
    }


    public  async  Task<Response<NotifTypeDto>> AddNotifTypeAsync(NotifTypeCreatDto notifTypeCreatDto)
    {
        /*try
        {*/
        var ad = mapper.Map<NotificationType>(notifTypeCreatDto);
        _cache.Remove("NotificationType_test");
        context.NotificationTypes.Add(ad);
        await context.SaveChangesAsync();
        var result = mapper.Map<NotifTypeDto>(ad);
        return new Response<NotifTypeDto>(HttpStatusCode.Created, "NotifType created successfully!", result);
        /*}*/
        /*catch (Exception ex)
        {
            return new Response<TodoItemDto>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }*/
    }

    public  async Task<Response<NotifTypeDto>> UpdateNotifTypeAsync(NotifTypeDto notifTypeDto)
    {
        /*try
      {*/
        var check = await context.NotificationTypes.FindAsync(notifTypeDto.Id);
        if (check == null)
            return new Response<NotifTypeDto>(HttpStatusCode.NotFound, "NotifType not found");
            
        check.Type = notifTypeDto.Type;
        _cache.Remove("NotificationType_test");
        _cache.Remove($"NotificationType_{notifTypeDto.Id}");
        await context.SaveChangesAsync();
        var result = mapper.Map<NotifTypeDto>(check);
        return new Response<NotifTypeDto>(HttpStatusCode.OK, "NotifType updated successfully!", result);
        /*}*/
        /*catch (Exception ex)
        {
            return new Response<TodoItemDto>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }*/
    }

    public  async Task<Response<string>> DeleteNotifTypeAsync(int id)
    {
        /*try
      {*/
        var notificationType = await context.NotificationTypes.FindAsync(id);
        if (notificationType == null)
            return new Response<string>(HttpStatusCode.NotFound, "NotificationType not found");
        _cache.Remove("NotificationType_test");
        context.NotificationTypes.Remove(notificationType);
        await context.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK, "NotificationType deleted successfully!");

        /*catch (Exception ex)
            {
                return new Response<string>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
            }*/
    }

    public async  Task<Response<NotifTypeDto>> GetNotifTypeByIdAsync(int id)
    {
        /*try
        {*/
        string cacheKey = $"NotificationType_{id}";
        if (!_cache.TryGetValue(cacheKey, out NotifTypeDto cachedNotifType)) 
        {
            var notificationTypeList = await context.NotificationTypes.ToListAsync();
            if (notificationTypeList == null || notificationTypeList.Count == 0) 
                return new Response<NotifTypeDto>(HttpStatusCode.NotFound, "NotifType not found");
            var notificationType = notificationTypeList.FirstOrDefault(n => n.Id == id); 
            if (notificationType == null) return new Response<NotifTypeDto>(HttpStatusCode.NotFound, "NotifType not found"); 
            cachedNotifType = mapper.Map<NotifTypeDto>(notificationType); 
            _cache.Set(cacheKey, cachedNotifType); 
        } 
        else 
        { 
            var currentCount = await context.NotificationTypes.CountAsync(); 
            var cachedCount = context.NotificationTypes.Local.Count; 
            if (currentCount != cachedCount)
            {
                _cache.Remove(cacheKey);
                var notificationType = await context.NotificationTypes.FirstOrDefaultAsync(n => n.Id == id); 
                if (notificationType == null) 
                    return new Response<NotifTypeDto>(HttpStatusCode.NotFound, "NotifType not found"); 
                cachedNotifType = mapper.Map<NotifTypeDto>(notificationType); 
                _cache.Set(cacheKey, cachedNotifType);
            } 
        } return new Response<NotifTypeDto>(HttpStatusCode.OK, "NotifType retrieved successfully!", cachedNotifType);
        /*catch (Exception ex)
        {
            return new Response<TodoItemDto>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }*/
    }
}