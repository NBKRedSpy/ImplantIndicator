using ModConfigMenu;
using ModConfigMenu.Contracts;
using ModConfigMenu.Implementations;
using ModConfigMenu.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ImplantIndicator.Mcm
{
    internal class McmConfiguration : McmConfigurationBase
    {
        private const string SplitStacksHotkeyHeader = "Split Stacks Hotkeys";

        public McmConfiguration(ModConfig config) : base (config) { }

        public override void Configure()
        {

            ModConfig defaults = new ModConfig();
            ModConfig config = (ModConfig)Config;


            List<IConfigValue> configValues =
            [
                CreateRestartMessage(),


                CreateConfigProperty(nameof(ModConfig.ImplantIndicatorColor), "The color to use for the implant indicator",
                    header: "Augment Indicator"),


            ];

            ModConfigMenuAPI.RegisterModConfig("Augment Indicator", configValues, OnSave);
        }
    }
}
