using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserLoginApp.Models;


namespace UserLoginApp.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        
        public ActionResult Index()

        {
            if (Session["LOGGED_IN"] == null)
            {
                return RedirectToAction("LogOn", "Account");
            }
            ViewBag.Message = "Home Page";


           
            return View();
        }

    }
}
