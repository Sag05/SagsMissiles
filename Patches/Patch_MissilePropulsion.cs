using BrilliantSkies.Ftd.Missiles.Components;
using HarmonyLib;

namespace SagsMissiles
{
    [HarmonyPatch(typeof(MissilePropulsion))]
    public class Patch_MissilePropulsion
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MissilePropulsion.Run))]
        private static bool Prefix_Run(MissilePropulsion __instance)
        {
            if (!(__instance is IMissilePropulsion propulsion)) return true;

            propulsion.Propel(__instance);
            return false;
        }
    }
}