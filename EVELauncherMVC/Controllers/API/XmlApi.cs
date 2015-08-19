using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EVELauncherMVC.Models.API;

namespace EVELauncherMVC.Controllers.API
{
    class XmlApi
    {
        public string Get(string URL)
        {
            try
            {
                HttpWebRequest GetRequest = (HttpWebRequest)WebRequest.Create(new Uri(URL));
                GetRequest.Method = "GET";
                GetRequest.Accept = "text/html, application/xhtml+xml, */*";
                GetRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko";
                HttpWebResponse getResponse = (HttpWebResponse)GetRequest.GetResponse();
                StreamReader ResponseStreamReader = new StreamReader(getResponse.GetResponseStream(), Encoding.UTF8);
                string XMLResponse = ResponseStreamReader.ReadToEnd();
                ResponseStreamReader.Close();
                getResponse.Close();
                return XMLResponse;
            }
            catch (WebException)
            {
                return "netErr";
            }

        }
    }
}
