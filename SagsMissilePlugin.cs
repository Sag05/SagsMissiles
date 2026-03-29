using System;
using BrilliantSkies.Core.Logger;
using BrilliantSkies.Core.Timing;
using BrilliantSkies.Modding;
using HarmonyLib;

namespace SagsMissiles
{
    public class SagsMissilePlugin : GamePlugin_PostLoad
    {
        public string name => "SagsMissiles";

        public Version version => new Version(1, 0);

        public void OnLoad()
        {
            GameEvents.StartEvent.RegWithEvent(OnStart);
            var HarmonyPatches = new Harmony("SagsMissiles");
            HarmonyPatches.PatchAll();
        }

        public void OnSave()
        {
        }

        public bool AfterAllPluginsLoaded()
        {
            return true;
        }

        public void OnStart()
        {
            GameEvents.StartEvent.UnregWithEvent(OnStart);
            AdvLogger.LogEvent("[SagsMissiles] Mod Loaded!");
        }
    }
}