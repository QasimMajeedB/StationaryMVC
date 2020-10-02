using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stationary.Models;
namespace Stationary.Controllers
{
    public class ReportCalculateController : Controller
    {
        web_stationaryEntities ctx = new web_stationaryEntities();
        // GET: ReportCalculate
        public ActionResult Index()
        {
            //var query = db.tbl_stationaryRequest.SqlQuery("select sum(req_totalprice) from tbl_stationaryRequest group by user_id order by user_id").ToList();

        

            var d = ctx.tbl_stationaryRequest.ToList();

           ViewBag.ss= d.Sum(obj=>obj.req_totalprice);
          
            
            return View(d);
        }
    }
}