using BrilliantSkies.Core.Logger;
using BrilliantSkies.Core.Timing;
using BrilliantSkies.Ftd.Missiles;
using BrilliantSkies.Ftd.Missiles.Blueprints;
using BrilliantSkies.Modding;
using BrilliantSkies.Ui.Tips;
using HarmonyLib;
using System;
using System.Collections;
using UnityEngine;

namespace SagsMissiles
{
    public class SagsMissilePlugin : GamePlugin_PostLoad
    {
        public string name => "SagsMissiles";

        public Version version => new Version(1, 0);

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
