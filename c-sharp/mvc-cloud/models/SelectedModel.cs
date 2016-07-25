using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserLoginApp.Models
{
    public class SelectedModel
    {
        public string selectedFolder { get; set; }
        public string selectedFile { get; set; }

        public SelectedModel(string selFol, string selFil)
        {
            selectedFolder = selFol;
            selectedFile = selFil;

        }

        public SelectedModel() { }

    }
}