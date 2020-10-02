using Stationary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stationary.Controllers
{
    public class AvailableItemController : Controller
    {
        web_stationaryEntities db = new web_stationaryEntities();
        // GET: AvailableItem
        public ActionResult Index()
        {
            var data = db.tbl_stock.Where(obj => obj.s_status == "Available").ToList();
            return View(data);
        }
    }
}