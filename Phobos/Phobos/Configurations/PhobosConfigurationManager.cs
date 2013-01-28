using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phobos.Configurations.Keyboard;
using System.Configuration;
using Nini.Config;

namespace Phobos.Configurations
{
    class PhobosConfigurationManager
    {

        public static IConfigSource source;

        public static void Init(string path)
        {
            if (source == null)
            {
                source = new IniConfigSource(path);
            }
        }

        public static string get(string section, string key, string defaultValue = null)
        {
            if (PhobosConfigurationManager.source.Configs[section] != null && PhobosConfigurationManager.source.Configs[section].Get(key) != null)
            {
                return PhobosConfigurationManager.source.Configs[section].Get(key);
            }
            else
            {
                return defaultValue;
            }
        }

        public static void set(string section, string key, string value)
        {
            if (PhobosConfigurationManager.source.Configs[section] != null)
            {
                PhobosConfigurationManager.source.Configs[section].Set(key, value);
            }
            else
            {
                PhobosConfigurationManager.source.AddConfig(section);
                PhobosConfigurationManager.source.Configs[section].Set(key, value);
            }
        }

        public static void Save()
        {
            PhobosConfigurationManager.source.Save();
        }

        public static void SetAutoSave(bool autosave)
        {
            PhobosConfigurationManager.source.AutoSave = autosave;
        }

        public static bool IsAutoSaving()
        {
            return PhobosConfigurationManager.source.AutoSave;
        }
    }
}