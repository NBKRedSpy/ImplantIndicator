using HarmonyLib;
using ImplantIndicator.Utils;
using MGSC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace ImplantIndicator
{
    [HarmonyPatch]
    public static class Multiple_RefreshImplantsWarning_Patch
    {
        private static MaskedSprite MaskedSprite { get; set; } = null;

        static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(CorpseInspectWindow), nameof(CorpseInspectWindow.RefreshImplantsWarning));
            yield return AccessTools.Method(typeof(MonsterInspectWindow), nameof(MonsterInspectWindow.RefreshImplantsWarning));
        }

        public static void Postfix(object __instance)
        {
            try
            {
                CreatureData creatureData;
                Image image;

                if (__instance is CorpseInspectWindow corpseWindow)
                {
                    creatureData = corpseWindow._corpseStorage.CreatureData;
                    image = corpseWindow._implantsWarning;
                }
                else if (__instance is MonsterInspectWindow monsterWindow)
                {
                    creatureData = monsterWindow._inspectedCreature.CreatureData;
                    image = monsterWindow._implantsWarning;
                }
                else
                {
                    throw new InvalidDataException("Instance is neither CorpseInspectWindow nor MonsterInspectWindow");
                }

                bool hasImplants = AugmentationSystem.HasAnyInstalledImplants(creatureData);

                if (MaskedSprite == null)
                {
                    MaskedSprite = new MaskedSprite(image, Plugin.Config.ImplantIndicatorUnityColor);
                }

                MaskedSprite.ApplyMask(image, !hasImplants);
            }
            catch (Exception ex)
            {
                Plugin.Logger.LogError($"Error in RefreshImplantsWarning.Postfix: {ex}");
            }
        }
    }
}
