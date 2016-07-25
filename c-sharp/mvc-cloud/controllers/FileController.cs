using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserLoginApp.Models;
using System.IO;

namespace UserLoginApp.Controllers
{
    public class FileController : Controller
    {
        

        public ActionResult Index()
        {


            return View();
        }

        public ActionResult Upload()
        {

            return View();


        }
        [HttpPost]
        public ActionResult Upload(FileModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                model.addFile(file, Session["CurrentDirectory"].ToString());

                return RedirectToAction("Index", "Explorer");
            }
            return View();



        }
        public ActionResult Move()
        {
            return View();
        }
        public ActionResult Copy()
        {
            return View();
        }
        public ActionResult Share()
        {
            return View();
        }
        public ActionResult Download()
        {
            return View();
        }
        public ActionResult Rename()
        {
            return View();
        }
        public ActionResult Delete(ExplorerModel model)
        {
            IEnumerable<string> foldersToDelete = model.FolderList.Where(f => f.isChecked).Select(f => f.Name);
            IEnumerable<string> filesToDelete = model.FileList.Where(f => f.isChecked).Select(f => f.Name);

            //do deleting
            return RedirectToAction("Index", "Explorer");
        }
    }
}
