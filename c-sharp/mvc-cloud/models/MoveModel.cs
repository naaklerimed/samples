using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserLoginApp.Models
{
    public class MoveModel

    {
        public SelectedModel selected { get; set; }
        public List<FolderModel> FolderList { get; set; }

        public MoveModel(SelectedModel s, List<FolderModel> f)
        {
            selected = s;
            FolderList = f;
        }

        public MoveModel() { }
    }
}