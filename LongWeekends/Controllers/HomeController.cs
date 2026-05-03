using Haskap.DddBase.Domain.Shared.Enums;
using LongWeekends.Models;
using Microsoft.AspNetCore.Mvc;
using Modules.SpecialDayBase.Application.Contracts;
using System.Diagnostics;

namespace LongWeekends.Controllers;

public class HomeController : Controller
{
    private readonly ISpecialDayService _specialDayService;

    public HomeController(ISpecialDayService specialDayService)
    {
        _specialDayService = specialDayService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public async Task<JsonResult> GetDayGridMonthEvents(DateTime startDateTime, DateTime endDateTime, CancellationToken cancellationToken = default)
    {
        var diff = endDateTime - startDateTime;
        if(diff > TimeSpan.FromDays(90))
        {
            throw new ArgumentException("Date range should not exceed 90 days.");
        }

        var longWeekendDays = _specialDayService.GetLongWeekendDays(startDateTime, endDateTime, [Country.Turkiye]);
        return Json(longWeekendDays);
    }
}
