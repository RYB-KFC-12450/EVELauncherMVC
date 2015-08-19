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
    class BearerAccessToken
    {
        public string Value = null;
        public bool Get(string UserName, string Password, CookieContainer CookieContainer)
        {
            try
            {
                //第一个是POST请求，用于提交表单设置Cookie
                string loginPostString = "UserName=" + WebUtility.UrlEncode(UserName) + "&Password=" + WebUtility.UrlEncode(Password) + "&CaptchaToken=&Captcha=";
                byte[] loginPostByte = Encoding.UTF8.GetBytes(loginPostString);
                string BearerLoginResponse;
                HttpWebRequest BearerLoginPostRequest = (HttpWebRequest)WebRequest.Create(new Uri("https://auth.eve-online.com.cn/Account/LogOn?ReturnUrl=%2Foauth%2Fauthorize%3Fclient_id%3DeveLauncherSerenity%26lang%3Dzh%26response_type%3Dtoken%26redirect_uri%3Dhttps%3A%2F%2Fauth.eve-online.com.cn%2Flauncher%3Fclient_id%3DeveLauncherSerenity%26scope%3DeveClientToken%2520user"));
                BearerLoginPostRequest.CookieContainer = CookieContainer;
                BearerLoginPostRequest.Method = "POST";
                BearerLoginPostRequest.ContentType = "application/x-www-form-urlencoded";
                BearerLoginPostRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                BearerLoginPostRequest.Headers.Add("Cache-Control", "max-age=0");
                BearerLoginPostRequest.Headers.Add("Origin", "https://auth.eve-online.com.cn");
                BearerLoginPostRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.15 Safari/535.11";
                BearerLoginPostRequest.Headers.Add("Refer", "https://auth.eve-online.com.cn/Account/LogOn?ReturnUrl=%2foauth%2fauthorize%3fclient_id%3deveLauncherSerenity%26lang%3dzh%26response_type%3dtoken%26redirect_uri%3dhttps%3a%2f%2fauth.eve-online.com.cn%2flauncher%3fclient_id%3deveLauncherSerenity%26scope%3deveClientToken%2520user&client_id=eveLauncherSerenity&lang=zh&response_type=token&redirect_uri=https://auth.eve-online.com.cn/launcher?client_id=eveLauncherSerenity&scope=eveClientToken%20user");
                BearerLoginPostRequest.ContentLength = loginPostByte.Length;
                Stream BearerloginPostStream = BearerLoginPostRequest.GetRequestStream();
                BearerloginPostStream.Write(loginPostByte, 0, loginPostByte.Length);
                BearerloginPostStream.Close();
                HttpWebResponse loginResponse = (HttpWebResponse)BearerLoginPostRequest.GetResponse();
                StreamReader BearerLoginResponseStreamReader = new StreamReader(loginResponse.GetResponseStream(), Encoding.UTF8);
                BearerLoginResponse = BearerLoginResponseStreamReader.ReadToEnd();
                BearerLoginResponseStreamReader.Close();
                loginResponse.Close();
                //第二个是Get请求，用于获取Access-Token。
                HttpWebRequest BearerLoginGetRequest = (HttpWebRequest)WebRequest.Create(new Uri("https://auth.eve-online.com.cn/oauth/authorize?client_id=eveLauncherSerenity&lang=zh&response_type=token&redirect_uri=https://auth.eve-online.com.cn/launcher?client_id=eveLauncherSerenity&scope=eveClientToken%20user"));
                BearerLoginGetRequest.CookieContainer = CookieContainer;
                BearerLoginGetRequest.Method = "GET";
                BearerLoginGetRequest.Accept = "text/html, application/xhtml+xml, */*";
                BearerLoginGetRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko";
                BearerLoginGetRequest.AllowAutoRedirect = false;
                BearerLoginGetRequest.Referer = "https://auth.eve-online.com.cn/Account/LogOn?ReturnUrl=%2foauth%2fauthorize%3fclient_id%3deveLauncherSerenity%26lang%3dzh%26response_type%3dtoken%26redirect_uri%3dhttps%3a%2f%2fauth.eve-online.com.cn%2flauncher%3fclient_id%3deveLauncherSerenity%26scope%3deveClientToken%2520user&client_id=eveLauncherSerenity&lang=zh&response_type=token&redirect_uri=https://auth.eve-online.com.cn/launcher?client_id=eveLauncherSerenity&scope=eveClientToken%20user";
                HttpWebResponse BearerGetResponse = (HttpWebResponse)BearerLoginGetRequest.GetResponse();
                StreamReader BearerSecondLoginResponseStreamReader = new StreamReader(BearerGetResponse.GetResponseStream(), Encoding.UTF8);
                string BearerSecondLoginResponse = BearerSecondLoginResponseStreamReader.ReadToEnd();
                BearerSecondLoginResponseStreamReader.Close();
                BearerGetResponse.Close();
                Match BearerAccessTokenMatch = Regex.Match(BearerSecondLoginResponse, @"access.token.(.*).amp.token.type");
                Value = BearerAccessTokenMatch.Groups[1].Value;
                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }
    }
}
