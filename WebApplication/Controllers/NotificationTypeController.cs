using Domain.Models.Entity;
using Domain.Models.Filters;
using Infastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationTypeController(INotificationTypeService _service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll( [FromQuery]  NotifTypeFilter filter)
    {
        var result = await _service.GetNotifTypeAsync(filter);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetNotifTypeByIdAsync(id);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(NotifTypeCreatDto dto)
    {
        var result = await _service.AddNotifTypeAsync(dto);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut]
    public async Task<IActionResult> Update( NotifTypeDto dto)
    {
        var result = await _service.UpdateNotifTypeAsync(dto);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteNotifTypeAsync(id);
        return StatusCode(result.StatusCode, result);
    }
}