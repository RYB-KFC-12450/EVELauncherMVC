using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml;
using EVELauncherMVC.Controllers.Tokens;
using EVELauncherMVC.Controllers.API;
using EVELauncherMVC.Models.API;
using Newtonsoft.Json;

namespace EVELauncherMVC.Controllers
{
    class Networking
    {
        CookieContainer CookieContainer = new CookieContainer();
        BearerAccessToken BearerAccessToken = new BearerAccessToken();
        ClientAccessToken ClientAccessToken = new ClientAccessToken();
        XmlApi XmlApi = new XmlApi();


        /// <summary>
        /// 登录并获取Bearer Access Token 的内容。请判断返回值是否为空或为NetErr或AccountErr。
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public string GetBearerAccessToken(string UserName, string Password)
        {
            if (String.IsNullOrEmpty(BearerAccessToken.Value))
            {
                BearerAccessToken.SetCookieContainer(CookieContainer);
                if (BearerAccessToken.Get(UserName, Password))
                {
                    CookieContainer = BearerAccessToken.GetCookieContainer();
                    if (!String.IsNullOrEmpty(BearerAccessToken.Value))
                    {
                        return BearerAccessToken.Value;
                    }
                    else return "AccountErr";
                }
                else return "NetErr";
            }
            else return BearerAccessToken.Value;
        }

        /// <summary>
        /// 返回Bearer Access Token字符串。需要确认登录状态。
        /// </summary>
        /// <returns></returns>
        public string GetBearerAccessToken()
        {
            return BearerAccessToken.Value;
        }

        /// <summary>
        /// 返回ClientAccessToken字符串，需要先确认BearerAccessToken的存在。判断是否返回NetErr。
        /// </summary>
        /// <param name="BearerAccessToken"></param>
        /// <returns></returns>
        public string GetClientAccessToken(string BearerAccessToken)
        {
            if (String.IsNullOrEmpty(ClientAccessToken.Value))
            {
                ClientAccessToken.SetCookieContainer(CookieContainer);
                if (ClientAccessToken.Get(BearerAccessToken))
                {
                    CookieContainer = ClientAccessToken.GetCookieContainer();
                    if (!String.IsNullOrEmpty(ClientAccessToken.Value))
                    {
                        return ClientAccessToken.Value;
                    }
                    else return "NetErr";
                }
                else return "NetErr";
            }
            else return ClientAccessToken.Value;
        }

        /// <summary>
        /// 这个应该也没啥用。
        /// </summary>
        /// <returns></returns>
        public string GetClientAccessToken()
        {
            return ClientAccessToken.Value;
        }
        /// <summary>
        /// 注销，泥萌懂的。
        /// </summary>
        public void Logout()
        {
            BearerAccessToken.Value = null;
            ClientAccessToken.Value = null;
        }

        public ServerStatus GetServerStatus()
        {
            string XML = XmlApi.Get("https://api.eve-online.com.cn/server/ServerStatus.xml.aspx");
            if (XML != "NetErr")
            {
                XmlDocument x = new XmlDocument();
                x.LoadXml(XML);
                string JSON = JsonConvert.SerializeXmlNode(x);
                ServerStatus SS = JsonConvert.DeserializeObject<ServerStatus>(JSON);
                return SS;
            }
            else
            {
                ServerStatus SS = new ServerStatus();
                SS.eveApi.result.serverOpen = "Network Error";
                return SS;
            }
        }
    }
}