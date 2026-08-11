using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGSC;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;
using System.Runtime.CompilerServices;

namespace ImplantIndicator.Mcm
{
    public class ModConfig : PersistentConfig<ModConfig>
    {

        public string ImplantIndicatorColor { get; set; } = "#00FF00";  //green

        [JsonIgnore]
        private Color _implantIndicatorUnityColor;

        /// <summary>
        /// Translates the hex based color required by MCM to a Unity color.
        /// </summary>
        [JsonIgnore]
        public Color ImplantIndicatorUnityColor
        {
            get { 
                if(_implantIndicatorUnityColor == default)
                {
                    if (ColorUtility.TryParseHtmlString(ImplantIndicatorColor, out Color color))
                    {
                        _implantIndicatorUnityColor = color;
                    }
                    else
                    {
                        Plugin.Logger.Log($"Unable to parse color {ImplantIndicatorColor}");
                    }
                }

                return _implantIndicatorUnityColor;
            }
        }



        public ModConfig()
        {

        }

        public ModConfig(string configPath) : base(configPath) { }

    }
}
