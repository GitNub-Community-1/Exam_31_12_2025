using Domain.Models.Entity;
using Domain.Models.Filters;
using Infastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPIWithJWTAndIdentity.Response;

namespace WebApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserNotificationController(IUserNotificationService _service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll( [FromQuery]  UserNotifFilter filter)
    {
        var result = await _service.GetUserNotifsAsync(filter);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetUserNotifByIdAsync(id);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(UserNotifCreateDto dto)
    {
        var result = await _service.AddUserNotifAsync(dto);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(UserNotifDto dto)
    {
        var result = await _service.UpdateUserNotifAsync(dto);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteUserNotifAsync(id);
        return StatusCode(result.StatusCode, result);
    }
}
