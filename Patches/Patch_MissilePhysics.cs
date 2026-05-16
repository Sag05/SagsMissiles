using System.Linq;
using System.Reflection;
using BrilliantSkies.Core.Logger;
using BrilliantSkies.Ftd.Missiles;
using BrilliantSkies.Ftd.Missiles.Blueprints;
using HarmonyLib;
using UnityEngine;

namespace SagsMissiles
{
    [HarmonyPatch(typeof(MissilePhysics))]
    public class Patch_MissilePhysics
    {
        private static FieldInfo missileField = AccessTools.Field(typeof(MissilePhysics), "_missile");
        
        [HarmonyPatch("SetDrag")]
        [HarmonyPrefix]
        private static bool Prefix_SetDrag(MissilePhysics __instance, 
            float waterFactor,
            float velocityForward,
            float velocityNormal,
            float altitude)
        {
            Missile missile = (Missile)missileField.GetValue(__instance);

            if (!missile.Blueprint.Components.Any(c => c is ISagsMissileComponent))
                return true;
            
            missile.Rigidbody.constraints = RigidbodyConstraints.None;
            
            missile.Rigidbody.drag = 0;
            missile.Rigidbody.angularDrag = 0;
            
            missile.Rigidbody.mass = 450f;
            Vector3 v = missile.Rigidbody.linearVelocity;
            float speed = v.magnitude;

            float diameter = 0.180f;
            
            float rho = 1.225f;        // air density at sea level
            float Cd  = 3.2f;          // drag coefficient (tune this)
            float A   = MissileMath.CircleArea(diameter);         // reference area (tune this)

            float dragMag = MissileMath.DragEquation(speed, rho, Cd, A);

            Vector3 drag = -v.normalized * dragMag;

            missile.Rigidbody.AddForce(drag, ForceMode.Force);
            AdvLogger.LogInfo("Drag (N): " + dragMag);
            return false;
            // //Missile _missile = (Missile)typeof(MissilePhysics).GetField("_missile").GetValue(__instance);
            // //float _baseDrag = (float)typeof(MissilePhysics).GetField("_baseDrag").GetValue(__instance) / 2;
            // //float _baseAngularDrag = (float)typeof(MissilePhysics).GetField("_baseAngularDrag").GetValue(__instance) / 50;
            // var airDensityModifier = MissilePhysics.GetAirDensityModifier(altitude);
            // var num = Mathf.Abs(velocityForward);
            // //_missile.Rigidbody.drag = Mathf.Max(0.01f, airDensityModifier * _baseDrag * waterFactor * num / 50f + _baseDrag * waterFactor * velocityNormal / 10f);
            // ____missile.Rigidbody.drag = 0.01f;
            // ____missile.Rigidbody.angularDrag = Mathf.Max(0.75f,
            //     airDensityModifier * ____baseAngularDrag * waterFactor / 2f * num / 50f);
        }
    }
}