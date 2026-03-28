using BrilliantSkies.Core.Logger;
using BrilliantSkies.Core.Timing;
using BrilliantSkies.Ftd.Missiles;
using BrilliantSkies.Ftd.Missiles.Blueprints;
using BrilliantSkies.Modding;
using HarmonyLib;
using System;
using UnityEngine;

namespace SagsMissiles
{
    public class FtDInterface : GamePlugin_PostLoad
    {
        public string name { get { return "SagsMissiles"; } }
        
        public Version version { get { return new Version(1, 0); } }


        [HarmonyPatch(typeof(MissilePhysics), "SetDrag")]
        public class Patch_MissilePhysics
        {
            static void Postfix(MissilePhysics __instance, float waterFactor, float velocityForward, float velocityNormal, float altitude)
            {
                Missile _missile = (Missile) typeof(MissilePhysics).GetField("_missile").GetValue(__instance);
                float _baseDrag = (float) typeof(MissilePhysics).GetField("_baseDrag").GetValue(__instance) / 2;
                float _baseAngularDrag = (float)typeof(MissilePhysics).GetField("_baseAngularDrag").GetValue(__instance) / 50;
                float airDensityModifier = MissilePhysics.GetAirDensityModifier(altitude);
                float num = Mathf.Abs(velocityForward);
                //_missile.Rigidbody.drag = Mathf.Max(0.01f, airDensityModifier * _baseDrag * waterFactor * num / 50f + _baseDrag * waterFactor * velocityNormal / 10f);
                _missile.Rigidbody.drag = 0.01f;
                _missile.Rigidbody.angularDrag = Mathf.Max(0.75f, airDensityModifier * _baseAngularDrag * waterFactor / 2f * num / 50f);
            }
        }

        public void OnLoad() 
        { 
            GameEvents.StartEvent.RegWithEvent(OnStart);
            Harmony HarmonyPatches = new Harmony("SagsMissiles");
            HarmonyPatches.PatchAll();
        }
        public void OnStart()
        {
            GameEvents.StartEvent.UnregWithEvent(OnStart);
            AdvLogger.LogEvent("[SagsMissiles] Mod Loaded!");
        }

        public void OnSave() { }
        public bool AfterAllPluginsLoaded() => true;
    }
}
