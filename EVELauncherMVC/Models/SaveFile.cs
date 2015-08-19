using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVELauncherMVC.Models
{
    class SaveFile
    {
        public string GamePath { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsExitAfterLaunched { get; set; }
        public bool UseDX9 { get; set; }
    }
}
