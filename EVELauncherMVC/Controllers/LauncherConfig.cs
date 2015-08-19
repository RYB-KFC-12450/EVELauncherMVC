using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVELauncherMVC.Models;
using Newtonsoft.Json;
using System.IO;

namespace EVELauncherMVC.Controllers
{
    class LauncherConfig
    {
        private string ConfPath;
        public LauncherConfig()
        {
            ConfPath = Path.GetTempPath() + @"\fakeEveLauncher.json";
        }

        /// <summary>
        /// 创建一个空配置文件，不知道可以用来干什么，先放在这好了。创建成功返回true，失败返回false。
        /// </summary>
        /// <returns></returns>
        public bool Create()
        {
            try
            {
                SaveFile EmptyConf = new SaveFile();
                File.WriteAllText(ConfPath, JsonConvert.SerializeObject(EmptyConf));
                return true;
            }
            catch (IOException)
            {
                return false;
            }
            catch (JsonException)
            {
                return false;
            }
        }

        /// <summary>
        /// 保存配置，此操作将立即写入文件。保存成功返回true，失败返回false。
        /// </summary>
        /// <param name="Conf"></param>
        /// <returns></returns>
        public bool Save(SaveFile Conf)
        {
            try
            {
                File.WriteAllText(ConfPath, JsonConvert.SerializeObject(Conf));
                return true;
            }
            catch (IOException)
            {
                return false;
            }
            catch (JsonException)
            {
                return false;
            }
        }

        /// <summary>
        /// 读取配置文件，需提前判断文件是否存在。
        /// </summary>
        /// <returns></returns>
        public SaveFile Load()
        {
            SaveFile Conf = JsonConvert.DeserializeObject<SaveFile>(File.ReadAllText(ConfPath));
            return Conf;
        }
        /// <summary>
        /// 删除已存在的配置文件，若成功返回true，失败返回false。
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            try
            {
                File.Delete(ConfPath);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        /// <summary>
        /// 检查是否存在配置文件，若存在则返回true，否则返回false。此操作应在程序启动时执行，若存在则加载现有配置文件。
        /// </summary>
        /// <returns></returns>
        public bool Check()
        {
            if (File.Exists(ConfPath))
            {
                return true;
            }
            else return false;
        }
    }
}
