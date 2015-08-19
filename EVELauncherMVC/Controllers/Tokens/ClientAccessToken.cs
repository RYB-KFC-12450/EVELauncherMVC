using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EVELauncherMVC.Controllers.Tokens
{
    class ClientAccessToken
    {
        public string Value = null;
        public bool Get(string BearerAccessToken, CookieContainer CookieContainer)
        {
            try
            {
                string publicAddress = "https://auth.eve-online.com.cn//launcher/token?accesstoken=";
                HttpWebRequest clientAccessRequest = (HttpWebRequest)WebRequest.Create(new Uri(publicAddress + BearerAccessToken));
                clientAccessRequest.CookieContainer = CookieContainer;
                clientAccessRequest.Method = "GET";
                clientAccessRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                clientAccessRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.15 Safari/535.11";
                clientAccessRequest.AllowAutoRedirect = false;
                clientAccessRequest.Referer = "https://launcher.eve-online.com.cn/zh/?mac=None";
                clientAccessRequest.ConnectionGroupName = "Keep-Alive";
                clientAccessRequest.Headers.Add("Accept-Encoding", "gzip,deflate");
                clientAccessRequest.Headers.Add("Accept-Language", "en-us,en");
                clientAccessRequest.Headers.Add("Accept-Charset", "iso-8859-1,*,utf-8");
                HttpWebResponse clientAccessResponse = (HttpWebResponse)clientAccessRequest.GetResponse();
                StreamReader responseStreamReader = new StreamReader(clientAccessResponse.GetResponseStream(), Encoding.UTF8);
                string clientAccessResponseHtml = responseStreamReader.ReadToEnd();
                responseStreamReader.Close();
                clientAccessResponse.Close();
                Match clientAccessTokenMatch = Regex.Match(clientAccessResponseHtml, @"access.token.(.*).amp.token.type");
                Value = clientAccessTokenMatch.Groups[1].Value;
                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }
    }
}
