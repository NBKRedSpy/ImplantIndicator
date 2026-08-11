using HarmonyLib;
using ImplantIndicator.Mcm;
using MGSC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ImplantIndicator.Utils;

namespace ImplantIndicator
{
    public static class Plugin
    {

        public static ConfigDirectories ConfigDirectories = new ConfigDirectories();

        public static ModConfig Config { get; private set; }

        public static Logger Logger = new Logger();

        internal static McmConfiguration McmConfiguration { get; private set; }

        [Hook(ModHookType.AfterConfigsLoaded)]
        public static void AfterConfig(IModContext context)
        {

            Directory.CreateDirectory(ConfigDirectories.ModPersistenceFolder);

            Config = new ModConfig(ConfigDirectories.ConfigPath).LoadConfig();

            McmConfiguration = new McmConfiguration(Config);
            McmConfiguration.TryConfigure();

            new Harmony("NBK_RedSpy_" + ConfigDirectories.ModAssemblyName).PatchAll();
        }
     
    }
}
