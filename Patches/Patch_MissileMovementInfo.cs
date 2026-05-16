using System.Linq;
using BrilliantSkies.Ftd.Missiles.Blueprints;
using HarmonyLib;

namespace SagsMissiles
{
    [HarmonyPatch(typeof(MissileMovementInfo))]
    public class Patch_MissileMovementInfo
    {
        [HarmonyPatch("TurnRate")]
        [HarmonyPostfix]
        private static void Postfix_TurnRate(MissileMovementInfo __instance, MissileBlueprint ____blueprint,
            ref float __result)
        {
            __result += ____blueprint.Components.Count(c => c is SM_SuperWings) * 50;
        }
    }
}