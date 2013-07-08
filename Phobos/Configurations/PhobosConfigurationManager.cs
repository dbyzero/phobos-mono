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

        public static IConfigSource Source { get; set; }

        public static void Init(string path)
        {
            if (Source == null)
            {
                Source = new IniConfigSource(path);
            }
        }

        public static string get(string section, string key, string defaultValue = null)
        {
            if (PhobosConfigurationManager.Source.Configs[section] != null && PhobosConfigurationManager.Source.Configs[section].Get(key) != null)
            {
                return PhobosConfigurationManager.Source.Configs[section].Get(key);
            }
            else
            {
                return defaultValue;
            }
        }

        public static void set(string section, string key, string value)
        {
            if (PhobosConfigurationManager.Source.Configs[section] != null)
            {
                PhobosConfigurationManager.Source.Configs[section].Set(key, value);
            }
            else
            {
                PhobosConfigurationManager.Source.AddConfig(section);
                PhobosConfigurationManager.Source.Configs[section].Set(key, value);
            }
        }

        public static void Save()
        {
            PhobosConfigurationManager.Source.Save();
        }

        public static void SetAutoSave(bool autosave)
        {
            PhobosConfigurationManager.Source.AutoSave = autosave;
        }

        public static bool IsAutoSaving()
        {
            return PhobosConfigurationManager.Source.AutoSave;
        }
    }
}