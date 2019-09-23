using DownTimeAlerter.Business.Service.IServices;
using DownTimeAlerter.Data.Domain.Entities;
using DownTimeAlerter.Data.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace DownTimeAlerter.Application.UI.Controllers {

    /// <summary>
    /// Monitoring Controller Authorize
    /// </summary>
    [Authorize]
    public class MonitoringController : Controller {
        public IMonitoringService Service { get; }
        public ILogger<MonitoringController> Logger { get; }

        public MonitoringController(IMonitoringService service, ILogger<MonitoringController> logger) {
            Service = service;
            Logger = logger;
        }

        #region IdentityUser
        private UserManager<User> _userManager;
        public UserManager<User> UserManager => _userManager ?? (UserManager<User>)HttpContext?.RequestServices.GetService(typeof(UserManager<User>));


        public Guid UserId {
            get {
                var userId = UserManager.GetUserId(User);
                return Guid.Parse(userId);
            }
        }
        #endregion

        /// <summary>
        /// List All Monitorings Get Action
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index() {
            try {
                var results = Service.FindBy(x => x.UserId == UserId).ToList();
                return View(results);
            }
            catch (Exception ex) {
                Logger.LogError($"Error occured at MonitoringController.Index(), {ex}");
                throw;
            }
        }

        /// <summary>
        /// Monitoring Detail Get Action
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Detail(Guid id) {
            if (id == Guid.Empty)
                return View();

            try {
                var result = Service.GetMonitoringDetail(id, UserId);
                return View(result);
            }
            catch (Exception ex) {
                Logger.LogError($"Error occured at MonitoringController.Detail(Guid id) with parameter: {id}, {ex}");
                throw;
            }
        }

        /// <summary>
        /// Create Monitoring Get Action
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create() {
            return View();
        }


        /// <summary>
        /// Create Monitoring Post Action
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(MonitoringViewModel model) {
            try {
                if (!ModelState.IsValid)
                    return View(model);

                model.UserId = UserId;
                Service.Add(model);
                Service.Save();
                return RedirectToAction("Index");

            }
            catch (Exception ex) {
                Logger.LogError($"Error occured at MonitoringController.Detail(Monitor model),  {ex}");
                throw;
            }

            return View();
        }

        /// <summary>
        /// Update Monitoring Get Action
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Update(Guid id) {
            try {
                if (id == Guid.Empty)
                    return View();

                var result = Service.GetBy(x => x.Id == id && x.UserId == UserId);
                var model = new MonitoringViewModel {
                    Id = result.Id,
                    Name = result.Name,
                    Url = result.Url,
                    Interval = result.Interval
                };
                return View(model);
            }
            catch (Exception ex) {
                Logger.LogError($"Error occured at MonitoringController.Update(Guid id) with parameter: {id}, {ex}");
                throw;
            }
        }

        /// <summary>
        /// Update Monitoring Post Action
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Update(MonitoringViewModel model) {
            try {
                if (!ModelState.IsValid)
                    return View(model);

                var dbModel = Service.GetById(model.Id);
                if (dbModel.UserId != UserId)
                    return View(model);

                Service.Edit(model);
                Service.Save();
                return RedirectToAction("Index");

            }
            catch (Exception ex) {
                Logger.LogError($"Error occured at MonitoringController.Update(MonitoringViewModel model) , {ex}");
                throw;
            }
            return View(model);
        }


        /// <summary>
        /// Delete Monitoring Post Action
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Delete(Guid id) {
            try {
                //Guid guid = Guid.Parse(input);
                if (id == Guid.Empty)
                    RedirectToAction("Index");

                var entity = Service.GetBy(x => x.Id == id && x.UserId == UserId);

                if (entity != null)
                    Service.Delete(entity);

                return RedirectToAction("Index");
            }
            catch (Exception ex) {
                Logger.LogError($"Error occured at MonitoringController.Delete(Guid id) with parameter: {id}, {ex}");
                throw;
            }
        }
    }
}
