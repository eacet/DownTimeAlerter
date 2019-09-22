using AutoMapper;
using DownTimeAlerter.Business.Service.IServices;
using DownTimeAlerter.Data.CommonModels.ViewModels;
using DownTimeAlerter.Data.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DownTimeAlerter.Application.UI.Controllers {

    [Authorize]
    public class MonitoringController : Controller {
        public IMonitoringService Service { get; }
        public IMapper Mapper { get; }
        public ILogger<MonitoringController> Logger { get; }

        public MonitoringController(IMonitoringService service, IMapper mapper, ILogger<MonitoringController> logger) {
            Service = service;
            Mapper = mapper;
            Logger = logger;
        }

        private UserManager<User> _userManager;
        public UserManager<User> UserManager => _userManager ?? (UserManager<User>)HttpContext?.RequestServices.GetService(typeof(UserManager<User>));


        public Guid UserId {
            get {
                var userId = UserManager.GetUserId(User);
                return Guid.Parse(userId);
            }
        }


        [HttpGet]
        public IActionResult Index() {
            try {
                var results = Service.FindBy(x => x.UserId == UserId);
                return View(results);
            }
            catch (Exception ex) {
                throw;
            }
        }

        [HttpGet]
        public IActionResult Detail(Guid id) {
            if (id == Guid.Empty)
                return View();

            try {
                var result = Service.GetMonitoringDetail(id, UserId);
                return View(result);
            }
            catch (Exception ex) {
                throw;
            }
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Monitor model) {
            try {
                if (!ModelState.IsValid)
                    return View(model);

                model.UserId = UserId;
                Service.Add(model);
                if (Service.Save()) {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex) {
                throw;
            }

            return View();
        }

        [HttpGet]
        public IActionResult Update(Guid id) {
            try {
                if (id == Guid.Empty)
                    return View();

                var result = Service.GetBy(x => x.Id == id && x.UserId == UserId);
                return View(result);
            }
            catch (Exception ex) {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Update(MonitoringViewModel model) {
            try {
                if (!ModelState.IsValid)
                    return View(model);

                var dbModel = Service.GetById(model.Id);
                if (dbModel.UserId != UserId)
                    return View();
                
                Service.Edit(model);
                if (Service.Save()) {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex) {
                throw;
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(Guid id) {
            try {
                var entity = Service.GetBy(x => x.Id == id && x.UserId == UserId);

                if (entity != null)
                    Service.Delete(entity);

                return RedirectToAction("Index");
            }
            catch (Exception ex) {
                throw;
            }
        }
    }
}
