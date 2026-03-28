using BrilliantSkies.Ui.Tips;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagsMissiles
{
    [HarmonyPatch(typeof(ShieldProjector), "AppendToolTip")] // Indicates a class/method to modify
    public class Patch_ShieldProjector
    {
        [HarmonyPostfix]
        static void Postfix(ShieldProjector __instance, ProTip tip)
        {
            tip.Add("We have modified FtD's code directly");
        }
    }
}
