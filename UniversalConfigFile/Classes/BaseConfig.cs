using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using anvlib.Xml;

namespace UniversalConfigFile.Classes
{
    public class BaseConfig
    {
        public string ApplicationName { get; set; }
        [XmlIgnore]
        public string Filename { get; set; }
        public List<GlobalConfigLocation> GlobalConfigLocations { get; set; }
        public bool UseGlobalConfigInsteadOfLocal { get; set; }

        public BaseConfig() 
        {
            Filename = GetDefaultFilename();
        }

        protected virtual string GetDefaultFilename()
        {
            return string.Format("{0}.conf", System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
        }

        public virtual void CreateDefaultConfig()
        {       
            Filename = GetDefaultFilename();
        }

        public virtual void CreateDefaultConfig(string appName)
        {
            ApplicationName = appName;
            Filename = GetDefaultFilename();
        }

        /*public List<string> GetGlobalConfigs()
        {
            List<string> res = new List<string>();
            if (GlobalConfigLocations != null && GlobalConfigLocations.Count > 0)
            {
                foreach (var conf in GlobalConfigLocations)
                    res.Add(System.IO.Path.Combine(conf.Key, conf.Value, Filename));
            }

            return null;
        }*/

        /*public List<string> GetGlobalHosts()
        {
            List<string> res = new List<string>();
            if (GlobalConfigLocations != null && GlobalConfigLocations.Count > 0)
            {
                foreach (var conf in GlobalConfigLocations)
                    res.Add(System.Text.RegularExpressions.Regex.Match(conf, "^(\\\\)(\\\\[a-zA-Z0-9-_]+)").Value.Replace("\\\\", ""));
            }

            return null;
        }*/

        //--Windows Shares Only
        public virtual bool IsGlobalConfigsAvailable()
        {            
            if (GlobalConfigLocations != null)
            {
                foreach (var conf in GlobalConfigLocations)
                {
                    if (anvlib.Network.PingSender.SendPing(conf.Host, 2))
                    {
                        if (conf.LocationType == GlobalLocationType.WindowsShare)
                        {
                            if (System.IO.File.Exists(System.IO.Path.Combine("\\\\", conf.Host, conf.Folder, System.IO.Path.GetFileName(Filename))))
                                return true;
                        }
                    }
                }
            }

            return false;
        }

        //--Windows Shares Only
        public virtual object GetGlobalWorkingConfig(Type conftype)
        {            
            object res = null;
            if (GlobalConfigLocations != null)
            {
                foreach (var conf in GlobalConfigLocations)
                {
                    if (anvlib.Network.PingSender.SendPing(conf.Host, 2))
                    {
                        if (conf.LocationType == GlobalLocationType.WindowsShare)
                        {
                            Type generic = typeof(ConfigManager<>);
                            Type CMGR = generic.MakeGenericType(conftype);
                            object[] parameters = new object[] { System.IO.Path.Combine("\\\\", conf.Host, conf.Folder, System.IO.Path.GetFileName(Filename)), null };
                            var result = CMGR.GetMethod("IsConfigHasRightFormat").Invoke(null, parameters);
                            if ((bool)result)
                            {
                                res = parameters[1];
                                return res;
                            }
                        }
                    }
                }
            }

            return res;
        }
    }
}
