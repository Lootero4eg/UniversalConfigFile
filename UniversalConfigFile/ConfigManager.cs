using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using anvlib.Xml;

using UniversalConfigFile.Classes;

namespace UniversalConfigFile
{
    public static class ConfigManager<T>
    {
        public static T LoadOrCreateConfig()
        {
            T config = (T)Activator.CreateInstance(typeof(T));
            if (!System.IO.File.Exists((config as BaseConfig).Filename))
                ConfigManager<T>.SaveConfig(config);
            else
            {
                object cnf = null;
                ConfigManager<T>.IsConfigHasRightFormat((config as BaseConfig).Filename, out cnf);
                if (cnf != null)
                    config = (T)cnf;
                else
                {
                    (config as BaseConfig).CreateDefaultConfig();
                    ConfigManager<T>.SaveConfig(config);
                }
            }

            if (config != null && (config as BaseConfig).UseGlobalConfigInsteadOfLocal)
            {
                if ((config as BaseConfig).IsGlobalConfigsAvailable())
                {
                    var cnf = (config as BaseConfig).GetGlobalWorkingConfig(typeof(T));
                    try
                    {
                        config = (T)cnf;
                        return config;
                    }
                    catch
                    {
                        throw new Exception("Config file corrupted or has wrong format!");
                    }                        
                }
            }

            return config;
        }

        public static object LoadConfig(string Filename, Type classtype)
        {
            if (System.IO.File.Exists(Filename))
            {
                return XmlClassManager.ReadClassFromXmlFile(Filename, classtype);
            }

            return null;
        }

        public static void SaveConfig(object config)
        {
            if (config is BaseConfig)
            {
                var fname = (config as BaseConfig).Filename;
                if (!string.IsNullOrEmpty(fname))
                {
                    if (File.Exists(fname))
                    {
                        File.Delete(fname);
                    }
                    XmlClassManager.WriteClassToXmlFile(fname, config);
                }
                else
                    throw new Exception("Please fill Config Filename field!");
            }
            else
                throw new Exception("Object must be inherited from BaseConfig!");
        }

        public static bool IsConfigHasRightFormat(string Filename, out object config)
        {
            try
            {
                if (File.Exists(Filename))
                {
                    config = LoadConfig(Filename, typeof(T));
                    return true;
                }
                else
                {
                    config = null;
                    return false;
                }

            }
            catch
            {
                config = null;
                return false;
            }
        }
    }
}
