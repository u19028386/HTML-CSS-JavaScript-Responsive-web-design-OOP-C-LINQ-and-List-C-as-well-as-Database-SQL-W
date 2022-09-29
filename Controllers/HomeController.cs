using CDUDB1INF272.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CDUDB1INF272.Controllers
{
    public class HomeController : Controller
    {

        private DataService dataService = DataService.getDataService();

              
        public ActionResult Index()            
        {
               
            List<DestinationModel> databaseDest = dataService.getDest();
            if (databaseDest.Count == 0)
            {
                ViewBag.Message = "Database not found";
            }
                 return View(databaseDest);
        }

        

       
      public ActionResult BookIndex()
       {
            return View();
       }




    }


}



