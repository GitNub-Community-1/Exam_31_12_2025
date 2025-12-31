using Domain.Models.Entity;
using Domain.Models.Filters;
using WebAPIWithJWTAndIdentity.Response;

namespace Infastructure.Services.Interfaces;

public interface IUserNotificationService
{
    public Task<Response<List<UserNotifDto>>> GetUserNotifsAsync(UserNotifFilter filter);
    public Task<Response<UserNotifDto>> AddUserNotifAsync(UserNotifCreateDto userNotifCreateDto);
    public Task<Response<UserNotifDto>> UpdateUserNotifAsync(UserNotifDto userNotifDto);
    public Task<Response<string>> DeleteUserNotifAsync(int id);
    public Task<Response<UserNotifDto>> GetUserNotifByIdAsync(int id);
}