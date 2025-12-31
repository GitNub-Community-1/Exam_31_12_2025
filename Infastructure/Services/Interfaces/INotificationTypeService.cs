using System.Net.Mime;
using AutoMapper;
using Domain.Models.Entity;
using Domain.Models.Filters;
using Infastructure.Data;
using Microsoft.Extensions.Caching.Memory;
using WebAPIWithJWTAndIdentity.Response;

namespace Infastructure.Services.Interfaces;

public interface INotificationTypeService
{
    public Task<Response<List<NotifTypeDto>>> GetNotifTypeAsync(NotifTypeFilter filter);
    public Task<Response<NotifTypeDto>> AddNotifTypeAsync(NotifTypeCreatDto notifTypeCreatDto);
    public Task<Response<NotifTypeDto>> UpdateNotifTypeAsync(NotifTypeDto notifTypeDto);
    public Task<Response<string>> DeleteNotifTypeAsync(int id);
    public Task<Response<NotifTypeDto>> GetNotifTypeByIdAsync(int id);
}