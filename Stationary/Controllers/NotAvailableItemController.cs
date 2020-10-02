using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stationary.Models;
namespace Stationary.Controllers
{
    public class NotAvailableItemController : Controller
    {
        web_stationaryEntities db = new web_stationaryEntities();
        // GET: NotAvailableItem
        public ActionResult Index()
        {
            var data = db.tbl_stock.Where(obj => obj.s_status == "NotAvailable").ToList();
            return View(data);
        }
    }
}