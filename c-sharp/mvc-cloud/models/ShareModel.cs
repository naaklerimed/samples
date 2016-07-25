using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserLoginApp.Models
{
    public class ShareModel
    {

        public SelectedModel selected { get; set; }

        public string[] userlist { get; set; }

        public ShareModel() { }

        public ShareModel(SelectedModel s, string[] u)
        {
            selected = s;
            userlist = u;
        }
    }
}