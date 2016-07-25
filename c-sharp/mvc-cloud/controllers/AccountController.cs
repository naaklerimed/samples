using Helpers;
using Postal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UserLoginApp.Models;

namespace UserLoginApp.Controllers
{
    public class AccountController : Controller
    {

        UserLoginAppEntities db = new UserLoginAppEntities();
        
        
        public ActionResult LogOn()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(AccountModel model)

        {
            var userid = (from c in db.UserList where model.Username == c.Username select c.ID).FirstOrDefault();

            var confStatus = (from c in db.UserList where model.Username == c.Username select c.ConfirmationStatus).FirstOrDefault();
           if (ModelState.IsValid)
            {
                if (model.IsTrue(model.Username, model.Password) && (confStatus == true) )
                {
                    Session["LOGGED_IN"] = "1";
                    Session["OnlineUser"] = new OnlineUser() { ID = userid };
                    OnlineUser user = (OnlineUser)(Session["OnlineUser"]);
                    
                   return RedirectToAction("Index", "Home");
                }
                else if (model.IsTrue(model.Username, model.Password) && (confStatus != true))
                {
                    ModelState.AddModelError("", "Account e-mail is not confirmed. Please click the activation link in the e-mail that we sent you.");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is incorrect. Please try again.");
                 }
            }
            return View(model);
        }

            public ActionResult Index()
             {
                 return View();
             }

            public ActionResult MyAccount()
            {
                return View();
            }
            public ActionResult ChangePassword()
            {
                return View();
            }

        [HttpPost]
        public ActionResult ChangePassword(AccountModel user) {

                user.changePassword(user.Username, user.Password, user.newPassword, user.newPasswordConfirmation);
                return RedirectToAction("LogOff");
            }
             public ActionResult Register()
             {
                 return View();
             }

             public ActionResult Update() {
                 return View();
             }

             
                
             [HttpPost]
             [ValidateAntiForgeryToken]
             public ActionResult Register(AccountModel user)
             {
                 if (ModelState.IsValid)
                 {
                     user.createUser(user.Username, user.Password, user.Mail);
                     string confToken = (from c in db.UserList where user.Username == c.Username select c.ConfirmationToken).First();
                     dynamic email = new Email("RegEmail");
                     email.To = user.Mail;
                     email.UserName = user.Username;
                     email.ConfirmationToken = confToken;
                     email.Send();

                     
                     return RedirectToAction("Confirmation");
                 }
                 else
                     {
                        ModelState.AddModelError("", "Problem in registriation.");
                        return RedirectToAction("Register");
                     }
                    
                 }

             public ActionResult Confirmation()
             {
                    return View();
             
             }

             public ActionResult ConfirmationSuccess() 
             {

                 string currentUrl = Request.Url.AbsoluteUri;
                 string tokenToConfirm = currentUrl.Split('/').Last();
                  using (UserLoginAppEntities db = new UserLoginAppEntities()){
                     var userid = (from r in db.UserList where tokenToConfirm == r.ConfirmationToken select r.ID).First();
                     string userName =(from r in db.UserList where tokenToConfirm == r.ConfirmationToken select r.Username).First();
                        var path = Path.Combine("C:/ExplorerView/"+userName);
                        Directory.CreateDirectory(path);
                        Folder f1 = new Folder();
                        f1.Name = userName;
                        f1.ParentID = null;
                        f1.UserID = userid;
                      db.Folder.Add(f1);
                      db.SaveChanges();
                        var path2 = Path.Combine("C:/ExplorerView/" + userName + "/Shared");
                        Directory.CreateDirectory(path2);
                        Folder f2 = new Folder();
                        f2.Name = "Shared";
                        f2.ParentID = f1.ID;
                        f2.UserID = userid;
                        db.Folder.Add(f2);
                      var user = db.UserList.Single(u => u.Username == userName);
                        user.ConfirmationStatus = true;
                        db.SaveChanges();
                      }

                  
                 
                 return View();
                 
                 

             }

             
    

             public ActionResult LogOff()
             {
                 Session.Abandon();
                 
                 return RedirectToAction("Index", "Home");
             }

             protected override void Dispose(bool disposing)
             {
                 db.Dispose();
                 base.Dispose(disposing);
             }
         }
     }

    
