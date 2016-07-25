using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Data.Linq;
using System.Data.Entity;
using Helpers;
using System.ComponentModel.DataAnnotations;


namespace UserLoginApp.Models
{
    public class AccountModel
    {
        
        public int ID { get; set; }
        
        public string Username { get; set; }
        
        public string Password { get; set; }
       
        public string Mail { get; set; }

        public bool ConfirmationStatus { get; set; }

        public string ConfirmationToken { get; set; }
       
        public string newPassword { get; set; }
        
        public string newPasswordConfirmation { get; set; }

        public string PasswordSalt { get; set; }

     

       
        public bool IsTrue(string username, string password)
        {
            using (UserLoginAppEntities db = new UserLoginAppEntities())
            {
                PasswordSalt = (from s in db.UserList where username == s.Username select s.PasswordSalt).FirstOrDefault();
            
                password = SHA1.Encode(PasswordSalt + password);
                if ((from r in db.UserList where username == r.Username && password == r.Password select r).Count() != 0 )
                {
                    
                    return true;
                }
                else 
                { 
                    return false;
                }
            }
        }       
        
        public void createUser(string username, string password, string Mail) {
            string salt = SHA1.SaltGenerator();
            string confirmationToken = SHA1.Encode(username);
            password = SHA1.Encode(salt + password);
            using (UserLoginAppEntities db = new UserLoginAppEntities())
            {
                UserList u1 = new UserList();
                u1.Username = username;
                u1.Password = password;
                u1.Mail = Mail;
                u1.PasswordSalt = salt;
                u1.ConfirmationToken = confirmationToken;
                u1.ConfirmationStatus = false;
                db.UserList.Add(u1);
                db.SaveChanges();

                 }
            
        }
        public void changePassword(string username, string password, string newPassword, string newPasswordConfirmation){

                
                if (newPassword == newPasswordConfirmation)
                {
                    using (UserLoginAppEntities db = new UserLoginAppEntities())
                    {
                        string userSalt = (from r in db.UserList where username == r.Username select r.PasswordSalt).First();
                        string userPass = SHA1.Encode(userSalt + password);
                        string salt = SHA1.SaltGenerator();
                        string newPasswordEnc = SHA1.Encode(salt + newPassword);
                        var userT = db.UserList.Single(u => u.Password == userPass);
                        userT.Password = newPasswordEnc;
                        userT.PasswordSalt = salt;
                        db.SaveChanges();
                    }
                }

        }


         
            

    }}