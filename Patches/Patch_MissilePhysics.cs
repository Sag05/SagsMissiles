using BrilliantSkies.Ftd.Missiles;
using BrilliantSkies.Ftd.Missiles.Blueprints;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SagsMissiles
{
    [HarmonyPatch(typeof(MissilePhysics), "SetDrag")]
    public class Patch_MissilePhysics
    {
        [HarmonyPostfix]
        static void Postfix(MissilePhysics __instance, Missile ____missile, float ____baseAngularDrag, float waterFactor, float velocityForward, float velocityNormal, float altitude)
        {
            //Missile _missile = (Missile)typeof(MissilePhysics).GetField("_missile").GetValue(__instance);
            //float _baseDrag = (float)typeof(MissilePhysics).GetField("_baseDrag").GetValue(__instance) / 2;
            //float _baseAngularDrag = (float)typeof(MissilePhysics).GetField("_baseAngularDrag").GetValue(__instance) / 50;
            float airDensityModifier = MissilePhysics.GetAirDensityModifier(altitude);
            float num = Mathf.Abs(velocityForward);
            //_missile.Rigidbody.drag = Mathf.Max(0.01f, airDensityModifier * _baseDrag * waterFactor * num / 50f + _baseDrag * waterFactor * velocityNormal / 10f);
            ____missile.Rigidbody.drag = 0.01f;
            ____missile.Rigidbody.angularDrag = Mathf.Max(0.75f, airDensityModifier * ____baseAngularDrag * waterFactor / 2f * num / 50f);
        }
    }
}
