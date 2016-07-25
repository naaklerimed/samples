using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace UserLoginApp.Models
{
    public class ExplorerModel
    {
        public List<FolderModel> FolderList;
        public List<FileModel> FileList;
        

        public ExplorerModel(List<FolderModel> FolderListt, List<FileModel> FileListt)
        {
            FolderList = FolderListt;
            FileList = FileListt;
        }

}

    
    public class FileModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int UserID { get; set; }
        public int FolderID { get; set; }
        public bool isChecked { get; set; }



        public void addFile(HttpPostedFileBase file, string currentUrl)
        {
            string[] currentDirectory = currentUrl.Split('/');
            string currentDirectory1 = currentDirectory.ElementAt(currentDirectory.Length - 2);
            string curpath = currentUrl.Split(new string[] { "Explorer/" }, StringSplitOptions.None).Last();
            using (UserLoginAppEntities db = new UserLoginAppEntities())
            {
                var currdir = (from r in db.Folder where r.Name == currentDirectory1 select r.ID).First();
                OnlineUser user = (OnlineUser)(HttpContext.Current.Session["OnlineUser"]);
                File f1 = new File();
                f1.Name = file.FileName;
                f1.UserID = user.ID;
                f1.FolderID = currdir;
                db.File.Add(f1);
                db.SaveChanges();
                var username = (from r in db.UserList where r.ID == user.ID select r.Username).First();
                

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine("C:/ExplorerView/" + curpath, fileName);
                    file.SaveAs(path);
                }
            }


        }


    }

    public class FolderModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int UserID { get; set; }
        public int ParentID { get; set; }
        public bool isChecked { get; set; }

        public void addFolder(string folderName, string currentUrl)
        {
            using (UserLoginAppEntities db = new UserLoginAppEntities())
            {
                OnlineUser user = (OnlineUser)(HttpContext.Current.Session["OnlineUser"]);
                var username = (from r in db.UserList where r.ID == user.ID select r.Username).First();
                
                string[] currentDirectory = currentUrl.Split('/');
                string currentDirectory1 = currentDirectory.ElementAt(currentDirectory.Length - 2);
                string curpath = currentUrl.Split(new string[] { "Explorer/" }, StringSplitOptions.None).Last();
                var currdir = (from r in db.Folder where r.Name == currentDirectory1 select r.ID).First();
                
                Folder f1 = new Folder();
                f1.Name = folderName;
                f1.UserID = user.ID;
                f1.ParentID = currdir;
                db.Folder.Add(f1);
                db.SaveChanges();
                
                     
                var path = Path.Combine("C:/ExplorerView/" + curpath, folderName);
                Directory.CreateDirectory(path);

            }
        }

   

    


}
    }
