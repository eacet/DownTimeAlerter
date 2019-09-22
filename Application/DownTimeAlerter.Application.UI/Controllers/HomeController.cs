using DownTimeAlerter.Application.UI.Models;
using DownTimeAlerter.Business.Service.IServices;
using DownTimeAlerter.Data.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DownTimeAlerter.Application.UI.Controllers {
    public class HomeController : Controller {
        public IBaseService<Monitor> Service { get; }

        public HomeController(IBaseService<Monitor> service) {
            Service = service;
        }

        public IActionResult Index() {
            return View();
        }


        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
