using BrilliantSkies.Ftd.Missiles;
using BrilliantSkies.Ftd.Missiles.Blueprints;
using HarmonyLib;
using UnityEngine;

namespace SagsMissiles
{
    [HarmonyPatch(typeof(MissilePhysics))]
    public class Patch_MissilePhysics
    {
        [HarmonyPatch("SetDrag")]
        [HarmonyPostfix]
        private static void Postfix_SetDrag(MissilePhysics __instance, Missile ____missile, float ____baseAngularDrag,
            float waterFactor, float velocityForward, float velocityNormal, float altitude)
        {
            //Missile _missile = (Missile)typeof(MissilePhysics).GetField("_missile").GetValue(__instance);
            //float _baseDrag = (float)typeof(MissilePhysics).GetField("_baseDrag").GetValue(__instance) / 2;
            //float _baseAngularDrag = (float)typeof(MissilePhysics).GetField("_baseAngularDrag").GetValue(__instance) / 50;
            var airDensityModifier = MissilePhysics.GetAirDensityModifier(altitude);
            var num = Mathf.Abs(velocityForward);
            //_missile.Rigidbody.drag = Mathf.Max(0.01f, airDensityModifier * _baseDrag * waterFactor * num / 50f + _baseDrag * waterFactor * velocityNormal / 10f);
            ____missile.Rigidbody.drag = 0.01f;
            ____missile.Rigidbody.angularDrag = Mathf.Max(0.75f,
                airDensityModifier * ____baseAngularDrag * waterFactor / 2f * num / 50f);
        }
    }
}