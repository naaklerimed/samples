using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using UserLoginApp.Models;

namespace UserLoginApp.Controllers
{
    public class ExplorerController : Controller
    {

        
        public ActionResult Index(string path)
        {
            if (path == null)
            {
                path = "";
            }

            OnlineUser user = (OnlineUser)(HttpContext.Session["OnlineUser"]);
            string pathUser = null, sharedInsidePath = "", sharedInsidePathFile = "", selectedP = null;
            using (UserLoginAppEntities db = new UserLoginAppEntities())
            {
                string username = (from r in db.UserList where r.ID == user.ID select r.Username).First();

                Uri url = Request.Url;

                List<FileModel> FileListModel = new List<FileModel>();
                List<FolderModel> FolderListModel = new List<FolderModel>();
                string realPath;


                int userID = 0;

                if (path == username + "/Shared/")
                {
                    List<string> folderNames = new List<string>();
                    List<string> fileNames = new List<string>();
                    IEnumerable<string> sharedFiles = (from r in db.Shared where r.SharedID == user.ID select r.SharedFilePath).ToList();
                    IEnumerable<string> sharedFolders = (from r in db.Shared where r.SharedID == user.ID select r.SharedFolderPath).ToList();
                    foreach (string folder in sharedFolders)
                    {
                        if (folder != null)
                        {
                            string folderName = folder.Split('/').Last();
                            folderNames.Add(folderName);
                        }
                    }
                    foreach (string file in sharedFiles)
                    {
                        if (file != null)
                        {
                            string fileName = file.Split('/').Last();
                            fileNames.Add(fileName);
                        }
                    }
                    foreach (string file in sharedFiles)
                    {

                        if (file != null)
                        {
                             string fileName = file.Split('/').Last();
                        Nullable<int> fileParent = (from r in db.File where r.Name == fileName select r.FolderID).First();
                        string parentFileName = (from r in db.Folder where r.ID == fileParent select r.Name).First();

                        if (!fileNames.Contains(parentFileName))
                        {
                            FileInfo f = new FileInfo(file);
                            FileModel fileModel = new FileModel();
                            fileModel.Name = Path.GetFileName(file);
                            FileListModel.Add(fileModel);
                        }
                        }
                    }
                    foreach (string folder in sharedFolders)
                    {
                        
                        if (folder != null)
                        {
                            string folderName = folder.Split('/').Last();
                            Nullable<int> folderParent = (from r in db.Folder where r.Name == folderName select r.ParentID).First();
                            string parentFolderName = (from r in db.Folder where r.ID == folderParent select r.Name).First();
                            
                                if (!folderNames.Contains(parentFolderName))
                                {
                                    FolderModel folderModel = new FolderModel();
                                    folderModel.Name = Path.GetFileName(folder);
                                    FolderListModel.Add(folderModel);
                                }
                            
                        }
                    }
                    ExplorerModel sharedModel = new ExplorerModel(FolderListModel, FileListModel);
                    return View(sharedModel);

                }

                else
                {
                    if (path != "" && path.Contains("Shared"))
                    {
                        string[] selPath = path.Split('/');
                        selectedP = Regex.Split(path, username + "/Shared").Last();
                        string selectedPath = selPath.ElementAt(selPath.Count() - 2);
                        int sharerid = (from r in db.Shared where r.SharedFilePath.Contains(selectedPath) || r.SharedFolderPath.Contains(selectedPath) select r.SharerID).First();
                        string selectedPFile = (from r in db.Shared where r.SharedFilePath.Contains(selectedPath) || r.SharedFolderPath.Contains(selectedPath) select r.SharedFolderPath).First();
                        pathUser = (from r in db.UserList where r.ID == sharerid select r.Username).First();
                        sharedInsidePath = "C:/ExplorerView/" + pathUser + selectedP;
                        sharedInsidePathFile = selectedPFile;
                    }
                    realPath = "C:/ExplorerView/" + path;
                    IEnumerable<string> FolderList = Enumerable.Empty<string>();
                    if (path != "")
                    {
                        if (pathUser != null)
                        {
                            FolderList = Directory.EnumerateDirectories(sharedInsidePath);
                        }
                        else
                        {
                            FolderList = Directory.EnumerateDirectories(realPath);
                        }
                    }
                    else
                    {
                        FolderList = Directory.EnumerateDirectories(realPath);
                    }
                    foreach (string folder in FolderList)
                    {
                        if (path == "")
                        {
                            string folderName = folder.Split('/').Last();
                            userID = (from r in db.Folder where r.Name == folderName select r.UserID).First();
                        }
                        else
                        {
                            userID = (from r in db.UserList where r.ID == user.ID select r.ID).First();

                        }
                        if (userID == user.ID)
                        {
                            DirectoryInfo k = new DirectoryInfo(folder);
                            FolderModel folderModel = new FolderModel();
                            folderModel.Name = Path.GetFileName(folder);
                            FolderListModel.Add(folderModel);
                        }
                    }

                    IEnumerable<string> FileList = Enumerable.Empty<string>();
                    if (path != "")
                    {
                        if (pathUser != null)
                        {

                            FileList = Directory.EnumerateFiles(sharedInsidePath);
                        }
                        else
                        {
                            FileList = Directory.EnumerateFiles(realPath);
                        }
                    }
                    else
                    {
                        FileList = Directory.EnumerateFiles(realPath);
                    }

                    foreach (string file in FileList)
                    {

                        if (path == "")
                        {
                            string fileName = file.Split('/').Last();
                            userID = (from r in db.Folder where r.Name == fileName select r.UserID).First();
                        }
                        else
                        {
                            userID = (from r in db.UserList where r.ID == user.ID select r.ID).First();

                        }
                        if (userID == user.ID)
                        {
                            FileInfo f = new FileInfo(file);
                            FileModel fileModel = new FileModel();
                            fileModel.Name = Path.GetFileName(file);
                            FileListModel.Add(fileModel);
                        }
                    }

                    ExplorerModel explorerModel = new ExplorerModel(FolderListModel, FileListModel);
                    Session["CurrentDirectory"] = Request.Url.AbsoluteUri;
                    return View(explorerModel);
        }
    }
}
        
        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Move")]
        public ActionResult Move()
        {
            string curFolder = "";
            var selectedFolders = Request.Form["selectedFolders"];
            if (selectedFolders != null)
            {
                curFolder = selectedFolders.ToString();
            }
            var selectedFiles = Request.Form["selectedFiles"];
            List<FolderModel> FolderListModel = new List<FolderModel>();
            using (UserLoginAppEntities db = new UserLoginAppEntities())
            {
                OnlineUser user = (OnlineUser)(HttpContext.Session["OnlineUser"]);
                string username = (from r in db.UserList where r.ID == user.ID select r.Username).First();



                IEnumerable<string> FolderList = Directory.GetDirectories("C:/ExplorerView/" + username, "*", SearchOption.AllDirectories);

                foreach (string folder in FolderList)
                {
                    if (curFolder != folder.Split('\\').Last())
                    {
                        DirectoryInfo k = new DirectoryInfo(folder);
                        FolderModel folderModel = new FolderModel();
                        folderModel.Name = Path.GetFileName(folder);
                        FolderListModel.Add(folderModel);
                    }
                }
                FolderModel root = new FolderModel();
                root.Name = username;
                FolderListModel.Add(root);
            }

            SelectedModel model = new SelectedModel(selectedFolders, selectedFiles);
            MoveModel move = new MoveModel(model, FolderListModel);
            return View(move);
        }
        

        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "pMove")]
        public ActionResult pMove(MoveModel model)
        {
            var selectedFolders = Request.Form["selectedFolders"];

            List<string> selectFolders = new List<string>();
            if (model.selected.selectedFolder != null)
            {
                if (model.selected.selectedFolder.Contains(","))
                {
                    selectFolders = model.selected.selectedFolder.Split(',').ToList();
                }
                else { selectFolders.Add(model.selected.selectedFolder.ToString()); }
            }
            List<string> selectFiles = new List<string>();
            if (model.selected.selectedFile != null)
            {
                if (model.selected.selectedFile.Contains(","))
                {
                    selectFiles = model.selected.selectedFile.Split(',').ToList();
                }
                else { selectFiles.Add(model.selected.selectedFile.ToString()); }
            }
            using (UserLoginAppEntities db = new UserLoginAppEntities())
            {

                string currentUrl = "C:/ExplorerView/";
                string folderPath = "";
                string folderPath2 = "";
                string folderName = "";
                string sFolder = "";
                OnlineUser user = (OnlineUser)(HttpContext.Session["OnlineUser"]);
                bool pathFound = false;
                Nullable<int> parent = 0;
                Nullable<int> firstparent = 0;
                Nullable<int> firstparentFolder = 0;
                Nullable<int> firstparentFile = 0;
                int counter = 0, newParent = 0 ;

                if (selectedFolders != null)
                {
                    sFolder = selectedFolders.ToString();
                    while (!pathFound)
                    {

                        if (counter == 0)
                        {
                            parent = (from r in db.Folder where r.Name == sFolder && r.UserID == user.ID select r.ParentID).First();
                            firstparent = parent;
                        }
                        else
                        {
                            folderName = (from r in db.Folder where r.ID == parent && r.UserID == user.ID select r.Name).First();
                            parent = (from r in db.Folder where r.Name == folderName && r.UserID == user.ID select r.ParentID).First();
                        }
                        counter++;
                        if (parent == null) { pathFound = true; }
                        else
                        {
                            string curPath = (from r in db.Folder where r.ID == parent select r.Name).First();
                            folderPath = curPath + "/" + folderPath;
                        }
                    }
                }
                pathFound = false;
                counter = 0;
                if (model.selected.selectedFolder != null)
                {
                    foreach (string sFolder1 in selectFolders)
                    {
                        while (!pathFound)
                        {

                            if (counter == 0)
                            {
                                parent = (from r in db.Folder where r.Name == sFolder1 && r.UserID == user.ID  select r.ParentID).First();
                                firstparentFolder = parent;
                            }
                            else
                            {
                                folderName = (from r in db.Folder where r.ID == parent && r.UserID == user.ID select r.Name).First();
                                parent = (from r in db.Folder where r.Name == folderName && r.UserID == user.ID select r.ParentID).First();
                            }
                            counter++;
                            if (parent == null) { pathFound = true; }
                            else
                            {
                                string curPath = (from r in db.Folder where r.ID == parent select r.Name).First();
                                folderPath2 = curPath + "/" + folderPath2;
                            }
                        }

                        newParent = (from r in db.Folder where r.Name == sFolder && r.UserID == user.ID select r.ID).First(); // needs fix
                        
                        db.Folder.Single(u => u.Name == sFolder1 && u.UserID == user.ID && u.ParentID == firstparentFolder).ParentID = newParent;
                        db.SaveChanges();
                        Directory.Move(currentUrl + folderPath2 + sFolder1, currentUrl + folderPath + sFolder + "/" + sFolder1);
                        db.SaveChanges();

                    }

                }
                pathFound = false;
                counter = 0;
                if (model.selected.selectedFile != null)
                {
                    foreach (string sFolder1 in selectFiles)
                    {
                        while (!pathFound)
                        {

                            if (counter == 0)
                            {
                                parent = (from r in db.File where r.Name == sFolder1 && r.UserID == user.ID select r.FolderID).First();
                                firstparentFile = parent;
                            }
                            else
                            {
                                folderName = (from r in db.Folder where r.ID == parent && r.UserID == user.ID select r.Name).First();
                                parent = (from r in db.Folder where r.Name == folderName && r.UserID == user.ID select r.ParentID).First();
                            }
                            counter++;
                            if (parent == null) { pathFound = true; }
                            else
                            {
                                string curPath = (from r in db.Folder where r.ID == parent select r.Name).First();
                                folderPath2 = curPath + "/" + folderPath2;
                            }
                        }

                        newParent = (from r in db.Folder where r.Name == sFolder && r.UserID == user.ID select r.ID).First();//needs fix
                        db.File.Single(u => u.Name == sFolder1 && u.UserID == user.ID && u.FolderID == firstparentFile).FolderID = newParent;
                        db.SaveChanges();
                        Directory.Move(currentUrl + folderPath2 + sFolder1, currentUrl + folderPath + sFolder + "/" + sFolder1);
                        db.SaveChanges();
                         }
                    }
                }
                    return RedirectToAction("Index", "Explorer");
} 

        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Copy")]
        public ActionResult Copy()
        {
            string curFolder = "";
            List<FolderModel> FolderListModel = new List<FolderModel>();
            var selectedFolders = Request.Form["selectedFolders"];
            var selectedFiles = Request.Form["selectedFiles"];
            if (selectedFolders != null)
            {
                curFolder = selectedFolders.ToString();
            }
            using (UserLoginAppEntities db = new UserLoginAppEntities())
            {
                OnlineUser user = (OnlineUser)(HttpContext.Session["OnlineUser"]);
                string username = (from r in db.UserList where r.ID == user.ID select r.Username).First();



                IEnumerable<string> FolderList = Directory.GetDirectories("C:/ExplorerView/" + username, "*", SearchOption.AllDirectories);

                foreach (string folder in FolderList)
                {
                    if (curFolder != folder.Split('\\').Last())
                    {
                        DirectoryInfo k = new DirectoryInfo(folder);
                        FolderModel folderModel = new FolderModel();
                        folderModel.Name = Path.GetFileName(folder);
                        FolderListModel.Add(folderModel);
                    }
                }
                FolderModel root = new FolderModel();
                root.Name = username;
                FolderListModel.Add(root);
            }

            SelectedModel model = new SelectedModel(selectedFolders, selectedFiles);
            MoveModel move = new MoveModel(model, FolderListModel);
            return View(move);
        }
        
        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "pCopy")]
        public ActionResult pCopy(MoveModel model)
        {
            var selectedFolders = Request.Form["selectedFolders"];
            List<string> selectFolders = new List<string>();
            if (model.selected.selectedFolder != null)
            {
                if (model.selected.selectedFolder.Contains(","))
                {
                    selectFolders = model.selected.selectedFolder.Split(',').ToList();
                }
                else { selectFolders.Add(model.selected.selectedFolder.ToString()); }
            }
            List<string> selectFiles = new List<string>();
            if (model.selected.selectedFile != null)
            {
                if (model.selected.selectedFile.Contains(","))
                {
                    selectFiles = model.selected.selectedFile.Split(',').ToList();
                }
                else { selectFiles.Add(model.selected.selectedFile.ToString()); }
            }

            using (UserLoginAppEntities db = new UserLoginAppEntities())
            {
                Nullable<int> curParent = 0;
                OnlineUser user = (OnlineUser)(HttpContext.Session["OnlineUser"]);
                if (model.selected.selectedFile != null)
                {
                    string selFile = model.selected.selectedFile.ToString();

                    curParent = (from r in db.File where r.Name == selFile && r.UserID == user.ID select r.FolderID).First();
                }
                string currentUrl = "C:/ExplorerView/";
                string folderPath = "";
                string folderPathFolder = "";
                string folderPathFile = "";
                string folderName = "";
                string sFolder = "";
                bool pathFound = false;
                Nullable<int> parent = 0;
                int newParent = 0;
                int counter = 0;
                Nullable<int> firstparent = 0;
                Nullable<int> firstparentFolder = 0;
                Nullable<int> firstparentFile = 0;
                if (selectedFolders != null)
                {
                    sFolder = selectedFolders.ToString();
                    while (!pathFound)
                    {

                        if (counter == 0)
                        {
                            parent = (from r in db.Folder where r.Name == sFolder && r.UserID == user.ID select r.ParentID).First();
                            firstparent = parent;
                        }
                        else
                        {
                            folderName = (from r in db.Folder where r.ID == parent && r.UserID == user.ID select r.Name).First();
                            parent = (from r in db.Folder where r.Name == folderName && r.UserID == user.ID select r.ParentID).First();
                        }
                        counter++;
                        if (parent == null) { pathFound = true; }
                        else
                        {
                            string curPath = (from r in db.Folder where r.ID == parent select r.Name).First();
                            folderPath = curPath + "/" + folderPath;
                        }
                    }

                }
                pathFound = false;
                counter = 0;
                if (model.selected.selectedFolder != null)
                {
                    foreach (string sFolder1 in selectFolders)
                    {
                        while (!pathFound)
                        {

                            if (counter == 0)
                            {
                                parent = (from r in db.Folder where r.Name == sFolder1 && r.UserID == user.ID select r.ParentID).First();
                                firstparentFolder = parent;
                            }
                            else
                            {
                                folderName = (from r in db.Folder where r.ID == parent select r.Name).First();
                                parent = (from r in db.Folder where r.Name == folderName select r.ParentID).First();
                            }
                            counter++;
                            if (parent == null) { pathFound = true; }
                            else
                            {
                                string curPath = (from r in db.Folder where r.ID == parent select r.Name).First();
                                folderPathFolder = curPath + "/" + folderPathFolder;
                            }
                        }

                        newParent = (from r in db.Folder where r.Name == sFolder && r.ParentID == firstparentFolder select r.ID).First();//need fix
                        db.Folder.Single(u => u.Name == sFolder1 && u.UserID == user.ID && u.ParentID == firstparentFolder).ParentID = newParent;
                        db.SaveChanges();
                        Directory.Move(currentUrl + folderPathFolder + sFolder1, currentUrl + folderPath + sFolder + "/" + sFolder1);
                        var path = Path.Combine(currentUrl + folderPathFolder, sFolder1);
                        Directory.CreateDirectory(path);
                        FolderModel modf = new FolderModel();
                        modf.Name = sFolder1;
                        modf.addFolder(modf.Name, folderPathFolder);
                        }
                }
                pathFound = false;
                counter = 0;
                if (model.selected.selectedFile != null)
                {
                    foreach (string sFile1 in selectFiles)
                    {
                        while (!pathFound)
                        {

                            if (counter == 0)
                            {
                                parent = (from r in db.File where r.Name == sFile1 && r.UserID == user.ID select r.FolderID).First();
                                firstparentFile = parent;
                            }
                            else
                            {
                                folderName = (from r in db.Folder where r.ID == parent select r.Name).First();
                                parent = (from r in db.Folder where r.Name == folderName select r.ParentID).First();
                            }
                            counter++;
                            if (parent == null) { pathFound = true; }
                            else
                            {
                                string curPath = (from r in db.Folder where r.ID == parent select r.Name).First();
                                folderPathFile = curPath + "/" + folderPathFile;
                            }
                        }
                        newParent = (from r in db.Folder where r.Name == sFolder select r.ID).First(); //needs fix
                        db.File.Single(u => u.Name == sFile1 && u.UserID == user.ID && u.FolderID == firstparentFile).FolderID = newParent;
                        db.SaveChanges();
                        System.IO.File.Copy(currentUrl + folderPathFile + sFile1, currentUrl + folderPath + sFolder + "/" + sFile1);
                        UserLoginApp.Models.File modfi = new UserLoginApp.Models.File();
                        modfi.Name = sFile1;
                        modfi.UserID = user.ID;
                        modfi.FolderID = (int)curParent;
                        db.File.Add(modfi);
                        db.SaveChanges();
                    }
                }
             }
               return RedirectToAction("Index", "Explorer");

        } 
        
        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Share")]
        public ActionResult Share()
        {
            using (UserLoginAppEntities db = new UserLoginAppEntities())
            {
                OnlineUser user = (OnlineUser)(HttpContext.Session["OnlineUser"]);
                string curUser = (from r in db.UserList where r.ID == user.ID select r.Username).First();
                string[] userlist = (from r in db.UserList where r.Username != curUser select r.Username).ToArray();
                var selectedFolders = Request.Form["selectedFolders"];
                var selectedFiles = Request.Form["selectedFiles"];
                SelectedModel model = new SelectedModel(selectedFolders, selectedFiles);
                ShareModel share = new ShareModel(model, userlist);
                return View(share);
            }
        }

        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "pShare")]
        public ActionResult pShare(ShareModel model)
        {
            
            string selectedUsers = Request.Form["selectedUsers"].ToString();

            List<string> selectFolders = new List<string>();
            if (model.selected.selectedFolder != null)
            {
                if (model.selected.selectedFolder.Contains(","))
                {
                    selectFolders = model.selected.selectedFolder.Split(',').ToList();
                }
                else { selectFolders.Add(model.selected.selectedFolder.ToString()); }
            }
            List<string> selectFiles = new List<string>();
            if (model.selected.selectedFile != null)
            {
                if (model.selected.selectedFile.Contains(","))
                {
                    selectFiles = model.selected.selectedFile.Split(',').ToList();
                }
                else { selectFiles.Add(model.selected.selectedFile.ToString()); }
            }
            OnlineUser user = (OnlineUser)(HttpContext.Session["OnlineUser"]);
            using (UserLoginAppEntities db = new UserLoginAppEntities())
            {
                var userid = (from r in db.UserList where r.Username == selectedUsers select r.ID).First();
                int folderid = (from r in db.Folder where r.Name == "Shared" && r.UserID == userid select r.ID).First();


                if (model.selected.selectedFile != null)
                {
                    
                    foreach (string sFolder1 in selectFiles)
                    {
                        int selFileID = (from r in db.File where r.Name == sFolder1 select r.ID).First();
                        List<int> childIds = new List<int>();
                        List<int> childIdsNew = new List<int>();
                        childIds = ChildFinder(selFileID, childIds, childIdsNew);
                        childIds.Add(selFileID);
                        UserLoginApp.Models.Shared s1 = new UserLoginApp.Models.Shared();
                        foreach (int childid in childIds)
                        {
                            s1.SharedID = userid;
                            s1.SharedFilePath = "C:/ExplorerView/" + filepathFinder(childid);
                            s1.SharerID = user.ID;
                            db.Shared.Add(s1);
                            db.SaveChanges();
                        }
                    }
                }

                if (model.selected.selectedFolder != null)
                {
                   foreach (string sFolder1 in selectFolders)
                    {
                        
                        int selFolderID = (from r in db.Folder where r.Name == sFolder1 select r.ID).First();
                        List<int> childIds = new List<int>();
                        List<int> childIdsNew = new List<int>();
                        childIds = ChildFinder(selFolderID, childIds, childIdsNew);
                        childIds.Add(selFolderID);
                        UserLoginApp.Models.Shared s1 = new UserLoginApp.Models.Shared();

                        foreach (int childid in childIds)
                        {
                            s1.SharedID = userid;
                            s1.SharedFolderPath = "C:/ExplorerView/" + folderpathFinder(childid);
                            s1.SharerID = user.ID;
                            db.Shared.Add(s1);
                            db.SaveChanges();
                        }
                    }

                }

                 
               
            }
            return RedirectToAction("Index", "Explorer");
        } 
        
        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Download")]
        public ActionResult Download()
        {

           using (UserLoginAppEntities db = new UserLoginAppEntities())
            {
                string currentUrl = "C:/ExplorerView/";
                var selectedFolders = Request.Form["selectedFolders"];
               List<string> selectFolders = new List<string>();
               List<string> folderPaths = new List<string>();
               if (selectedFolders != null) { 
                if (selectedFolders.Contains(","))
                {
                    selectFolders = selectedFolders.Split(',').ToList();
                }
                else{
                    selectFolders.Add(selectedFolders.ToString());
                }
               }
           
                var selectedFiles = Request.Form["selectedFiles"];
                bool pathFound = false;
                int counter = 0;
                Nullable<int> parent = 0;
                string folderName = "", folderPath = "", folderPath2 = "", sFolder1 = "";
                
               if (selectedFolders != null)
                {
                    foreach(string sFolder in selectFolders){
                    while (!pathFound)
                    {

                        if (counter == 0) { parent = (from r in db.Folder where r.Name == sFolder select r.ParentID).First(); }
                        else
                        {
                            folderName = (from r in db.Folder where r.ID == parent select r.Name).First();
                            parent = (from r in db.Folder where r.Name == folderName select r.ParentID).First();
                        }
                        counter++;
                        if (parent == null) { pathFound = true; }
                        else
                        {
                            string curPath = (from r in db.Folder where r.ID == parent select r.Name).First();
                            folderPath = curPath + "/" + folderPath;
                           
                        }
                    }
                    folderPath = folderPath + sFolder;
                    folderPaths.Add(folderPath);
                    folderPath = "";
                    counter = 0;
                    pathFound = false;
                    }
                }

                pathFound = false;
                counter = 0;
                if (selectedFiles != null)
                {
                    sFolder1 = selectedFiles.ToString();
                    while (!pathFound)
                    {
                        if (counter == 0) { parent = (from r in db.File where r.Name == sFolder1 select r.FolderID).First(); }
                        else
                        {
                            folderName = (from r in db.Folder where r.ID == parent select r.Name).First();
                            parent = (from r in db.Folder where r.Name == folderName select r.ParentID).First();
                        }
                        counter++;
                        if (parent == null) { pathFound = true; }
                        else
                        {
                            string curPath = (from r in db.Folder where r.ID == parent select r.Name).First();
                            folderPath2 = curPath + "/" + folderPath2;
                        }
                    }
                    return File(currentUrl + folderPath2 + sFolder1, MimeMapping.GetMimeMapping(sFolder1), sFolder1);
 }
              if (selectedFolders != null)
                {
                    using (ZipFile z = new ZipFile())
                    {
                        foreach (string fPath in folderPaths)
                        {
                            z.AddDirectory(currentUrl + fPath);
                        }
                        z.Name = "Download.zip";
                        z.Save("C:/Users/K/Desktop/" + z.Name);
                    }
                }
                return RedirectToAction("Index");
               
               
            }

        }

        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Rename")]
        public ActionResult Rename()
        {
            var selectedFolders = Request.Form["selectedFolders"];
            var selectedFiles = Request.Form["selectedFiles"];
            SelectedModel model = new SelectedModel(selectedFolders, selectedFiles);

            return View(model);
        }
       
        
        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "pRename")]
        public ActionResult pRename(string name, SelectedModel model)
        {
            using (UserLoginAppEntities db = new UserLoginAppEntities())
            {

                string currentUrl = "C:/ExplorerView/";
                string folderPath = "";
                string folderName = "";
                var selectedFolders = model.selectedFolder;
                var selectedFiles = model.selectedFile;
                OnlineUser user = (OnlineUser)(HttpContext.Session["OnlineUser"]);
                bool pathFound = false;
                Nullable<int> parent = 0;
                int counter = 0;
                Nullable<int> firstparent = 0;

                if (selectedFolders != null)
                {
                    string sFolder = selectedFolders.ToString();
                    while (!pathFound)
                    {

                        if (counter == 0)
                        {
                            parent = (from r in db.Folder where r.Name == sFolder && r.UserID == user.ID select r.ParentID).First();
                            firstparent = parent;
                        }
                        else
                        {
                            folderName = (from r in db.Folder where r.ID == parent && r.UserID == user.ID select r.Name).First();
                            parent = (from r in db.Folder where r.Name == folderName && r.UserID == user.ID select r.ParentID).First();
                        }
                        counter++;
                        if (parent == null) { pathFound = true; }
                        else
                        {
                            string curPath = (from r in db.Folder where r.ID == parent select r.Name).First();
                            folderPath = curPath + "/" + folderPath;
                        }
                    }
                    db.Folder.Single(u => u.Name == sFolder && u.ParentID == firstparent && u.UserID == user.ID).Name = name;
                    Directory.Move(currentUrl + folderPath + "/" + sFolder, currentUrl + folderPath + "/" + name);
                    db.SaveChanges();

                }

               if (selectedFiles != null)
                {
                    string sFile = selectedFiles.ToString();
                    counter = 0;

                    while (!pathFound)
                    {

                        if (counter == 0)
                        {
                            parent = (from r in db.File where r.Name == sFile && r.UserID == user.ID select r.FolderID).First();
                            firstparent = parent;
                        }
                        else
                        {
                            folderName = (from r in db.Folder where r.ID == parent && r.UserID == user.ID select r.Name).First();
                            parent = (from r in db.Folder where r.Name == folderName && r.UserID == user.ID select r.ParentID).First();
                        }
                        counter++;
                        if (parent == null) { pathFound = true; }
                        else
                        {
                            string curPath = (from r in db.Folder where r.ID == parent select r.Name).First();
                            folderPath = curPath + "/" + folderPath;
                        }
                    }
                    db.File.Single(u => u.Name == sFile && u.UserID == user.ID && u.FolderID == firstparent).Name = name;
                    Directory.Move(currentUrl + folderPath + "/" + sFile, currentUrl + folderPath + "/" + name);
                    db.SaveChanges();

                }
     }
         return RedirectToAction("Index", "Explorer");
  }

        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Delete")]
        public ActionResult Delete()
        {
            OnlineUser user = (OnlineUser)(HttpContext.Session["OnlineUser"]);
            var selectedFolders = Request.Form["selectedFolders"];
            List<string> selectFolders = new List<string>();
            if (selectedFolders != null)
            {
                if (selectedFolders.Contains(","))
                {
                    selectFolders = selectedFolders.Split(',').ToList();
                }
                else { selectFolders.Add(selectedFolders.ToString()); }
            }
            var selectedFiles = Request.Form["selectedFiles"];
            List<string> selectFiles = new List<string>();
            if (selectedFiles != null)
            {
                if (selectedFiles.Contains(","))
                {
                    selectFiles = selectedFolders.Split(',').ToList();
                }
                else { selectFiles.Add(selectedFiles.ToString()); }
            }
            using (UserLoginAppEntities db = new UserLoginAppEntities())
            {
                string currentUrl = "C:/ExplorerView/";
                bool pathFound = false;
                int counter = 0;
                Nullable<int> parent = 0;
                Nullable<int> firstparent = 0;
                string folderName = "";
                string folderPath = "";
                if (selectedFolders != null)
                {
                    foreach (string sFolder in selectFolders)
                    {
                        while (!pathFound)
                        {
                            if (counter == 0)
                            {
                                parent = (from r in db.Folder where r.Name == sFolder && r.UserID == user.ID select r.ParentID).First();
                                firstparent = parent;
                            }
                            else
                            {
                                folderName = (from r in db.Folder where r.ID == parent && r.UserID == user.ID select r.Name).First();
                                parent = (from r in db.Folder where r.Name == folderName && r.UserID == user.ID select r.ParentID).First();
                            }
                            counter++;
                            if (parent == null) { pathFound = true; }
                            else
                            {
                                string curPath = (from r in db.Folder where r.ID == parent select r.Name).First();
                                folderPath = curPath + "/" + folderPath;
                            }
                        }

                        folderPath = folderPath + sFolder;
                        counter = 0;
                        pathFound = false;
                        Directory.Delete(currentUrl + folderPath, true);
                        db.Folder.Single(u => u.Name == sFolder && u.ParentID == firstparent && u.UserID == user.ID).Name = "Deleted";
                        db.SaveChanges();
                        folderPath = "";
                    }
        }                
                counter = 0;
                pathFound = false;
                folderPath = "";
                if (selectedFiles != null)
                {
                    foreach (string sFolder in selectFolders)
                    {
                        while (!pathFound)
                        {
                            if (counter == 0)
                            {
                                parent = (from r in db.File where r.Name == sFolder && r.UserID == user.ID select r.FolderID).First();
                                firstparent = parent;
                            }
                            else
                            {
                                folderName = (from r in db.Folder where r.ID == parent && r.UserID == user.ID select r.Name).First();
                                parent = (from r in db.Folder where r.Name == folderName && r.UserID == user.ID select r.ParentID).First();
                            }
                            counter++;
                            if (parent == null) { pathFound = true; }
                            else
                            {
                                string curPath = (from r in db.Folder where r.ID == parent select r.Name).First();
                                folderPath = curPath + "/" + folderPath;
                            }
                        }
                    
                    folderPath = folderPath + sFolder;
                   
                    
                    counter = 0;
                    pathFound = false;
                    
                        System.IO.File.Delete(currentUrl + folderPath);
                        db.File.Single(u => u.Name == sFolder && u.UserID == user.ID && u.FolderID == firstparent).Name = "Deleted";
                        db.SaveChanges();
                        folderPath = "";
                    }
                    
                }
                return RedirectToAction("Index", "Explorer");
            }
        }


        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
        public class MultiButtonAttribute : ActionNameSelectorAttribute
        {
            public string MatchFormKey { get; set; }
            public string MatchFormValue { get; set; }

            public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
            {
                var isValidName = false;
                var keyValue = string.Format("{0}:{1}", MatchFormKey, MatchFormValue);
                var value = controllerContext.Controller.ValueProvider.GetValue(keyValue);

                if (value != null)
                {
                    controllerContext.Controller.ControllerContext.RouteData.Values[MatchFormKey] = MatchFormValue;
                    isValidName = true;
                }

                return isValidName;
            }

        }
        public List<int> ChildFinder(int folderID, List<int> childIds, List<int> childIdsNew)
        {
            using (UserLoginAppEntities db = new UserLoginAppEntities())
            {
                childIdsNew.AddRange((from r in db.Folder where r.ParentID == folderID select r.ID).ToList());
                childIds = (from r in db.Folder where r.ParentID == folderID select r.ID).ToList();
                foreach (int child in childIds)
                {
                    childIds = new List<int>();
                    ChildFinder(child, childIds, childIdsNew);
                }
            }
            
            return childIdsNew;
        }


        public string folderpathFinder(int id)
        {
            using (UserLoginAppEntities db = new UserLoginAppEntities())
            {
                OnlineUser user = (OnlineUser)(HttpContext.Session["OnlineUser"]);
                string folderPath = "", folderName = "";
                Nullable<int> parent = 0, firstparent = 0;
                int counter = 0;
                bool pathFound = false;
                string sFolder = (from r in db.Folder where r.ID == id select r.Name).First();
                while (!pathFound)
                {

                    if (counter == 0)
                    {
                        parent = (from r in db.Folder where r.Name == sFolder && r.UserID == user.ID select r.ParentID).First();
                        firstparent = parent;
                    }
                    else
                    {
                        folderName = (from r in db.Folder where r.ID == parent && r.UserID == user.ID select r.Name).First();
                        parent = (from r in db.Folder where r.Name == folderName && r.UserID == user.ID select r.ParentID).First();
                    }
                    counter++;
                    if (parent == null) { pathFound = true; }
                    else
                    {
                        string curPath = (from r in db.Folder where r.ID == parent select r.Name).First();
                        folderPath = curPath + "/" + folderPath;
                    }
                }
                folderPath = folderPath + sFolder;
                return folderPath;
            }



        }
        public string filepathFinder(int id)
        {
            using (UserLoginAppEntities db = new UserLoginAppEntities())
            {
                OnlineUser user = (OnlineUser)(HttpContext.Session["OnlineUser"]);
                string filePath = "", fileName = "";
                Nullable<int> parent = 0, firstparent = 0;
                int counter = 0;
                bool pathFound = false;
                string sFile = (from r in db.File where r.ID == id select r.Name).First();
                while (!pathFound)
                {

                    if (counter == 0)
                    {
                        parent = (from r in db.File where r.Name == sFile && r.UserID == user.ID select r.FolderID).First();
                        firstparent = parent;
                    }
                    else
                    {
                        fileName = (from r in db.Folder where r.ID == parent && r.UserID == user.ID select r.Name).First();
                        parent = (from r in db.Folder where r.Name == fileName && r.UserID == user.ID select r.ParentID).First();
                    }
                    counter++;
                    if (parent == null) { pathFound = true; }
                    else
                    {
                        string curPath = (from r in db.Folder where r.ID == parent select r.Name).First();
                        filePath = curPath + "/" + filePath;
                    }
                }
                filePath = filePath + sFile;
                return filePath;
            }



        }

    }
}
