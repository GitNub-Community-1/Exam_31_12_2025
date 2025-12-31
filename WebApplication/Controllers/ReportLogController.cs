using System.Net;
using Domain.Models.Entity_s;
using Infastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIWithJWTAndIdentity.Response;


namespace WebApplication.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ReportLogController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<ReportLog>>> GetReportLogs()
    {
        var reportLogs = await context.ReportLogs.ToListAsync();
        return new Response<List<ReportLog>>(HttpStatusCode.OK, "Reports Log List is here", reportLogs);
    }
}