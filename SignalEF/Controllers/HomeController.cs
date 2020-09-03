using SignalEF.Hubs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignalEF.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            notificationHub = new NotificationHub();
        }
        public NotificationHub notificationHub { get; set; }
        public ActionResult Index()
        {
            string conID = string.Empty;

            if (notificationHub.Context != null)
                conID = notificationHub.Context.ConnectionId;

            return View();
        }

        public ActionResult About()
        {
            NotificationHub notificationHub = new NotificationHub();
            notificationHub.sendNotification();

            return View();
        }
        public JsonResult SendNot(string connctionId, string message)
        {
            NotificationHub notificationHub = new NotificationHub();
            notificationHub.sendToUser(connctionId, message);
            var json = Json(true, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 2147483644;
            return json;
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}