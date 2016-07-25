using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserLoginApp.Models;

namespace UserLoginApp.Controllers
{
    public class FolderController : Controller
    {
        //
        // GET: /Folder/

        public ActionResult Index()
        {
            

            return View();
        }

        public ActionResult Add() {

            return View();
        
        
        }
        [HttpPost]
        public ActionResult Add(FolderModel model)
        {
            if (ModelState.IsValid)
            {
                
                model.addFolder(model.Name, Session["CurrentDirectory"].ToString());
                return RedirectToAction("Index", "Explorer");
            }
            return View();
            
            //previous 

        }

        
    }
}
