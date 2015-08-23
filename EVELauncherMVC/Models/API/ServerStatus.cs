using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVELauncherMVC.Models.API
{
    class ServerStatus
    {
        public ServerStatus()
        {
            eveApi.cachedUntil = "";
            eveApi.result.serverOpen = "";
            eveApi.result.onlinePlayers = "";
        }
        public ServerStatus_eveApi eveApi = new ServerStatus_eveApi();
    }
    class ServerStatus_xml
    {
        public string version { get; set; }
        public string encoding { get; set; }
    }
    class ServerStatus_eveApi
    {
        public string version { get; set; }
        public string currentTime { get; set; }
        public ServerStatus_result result = new ServerStatus_result();
        public string cachedUntil { get; set; }
    }
    class ServerStatus_result
    {
        public string serverOpen { get; set; }
        public string onlinePlayers { get; set; }
    }
}
