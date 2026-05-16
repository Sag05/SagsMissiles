using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using BrilliantSkies.Ftd.Missiles;
using BrilliantSkies.Ftd.Missiles.Blueprints;
using HarmonyLib;
using UnityEngine;
using static System.Reflection.Emit.OpCodes;

namespace SagsMissiles
{
    [HarmonyPatch(typeof(MissileMovementControl))]
    public class Patch_MissileMovementControl
    {
        private static readonly OpCode[] AddForceCodeInstructions =
        {
            Ldarg_0,
            Callvirt,
            Ldloc_S,
            Ldarg_0,
            Callvirt,
            Callvirt,
            Call,
            Callvirt,
            Nop
        };

        [HarmonyPatch("ConvertVelocityWithFins")]
        [HarmonyPrefix]
        private static bool Prefix_ConvertVelocityWithFins(ref Missile missile, ref float angleToTargetPoint)
        {
            if (!missile.Blueprint.Components.Any(c => c is ISagsMissileComponent))
                return true;

            const float betaCoeff = 1.2f;
            const float latDampingCoeff = 2.5f;

            const float referenceArea = 5.0f;
            const float airDensity = 1.225f;
            const float sideForceSlope = 5f;
            
            Transform transform = missile.transform;
            
            Vector3 localVelocity =
                transform.InverseTransformDirection(
                    missile.Rigidbody.linearVelocity
                );
            
            float speed = missile.Rigidbody.linearVelocity.magnitude;
            
            float dynamicPressure =
                0.5f * airDensity * speed * speed;

            // Local lateral airflow velocity
            Vector2 lateralVelocity = new Vector2(
                localVelocity.x,
                localVelocity.y
            );

            // Convert lateral velocity into aerodynamic restoring force
            Vector2 lateralForce =
                -lateralVelocity *
                dynamicPressure *
                referenceArea *
                sideForceSlope /
                Mathf.Max(speed, 1f);

            // Convert back to local 3D force
            Vector3 localForce = new Vector3(
                lateralForce.x,
                lateralForce.y,
                0f
            );

            // Convert to world force
            Vector3 worldForce =
                transform.TransformDirection(localForce);

            missile.Rigidbody.AddForce(worldForce, ForceMode.Force);

            return false;
        }

        // [HarmonyPatch("ConvertVelocityWithFins")]
        // [HarmonyTranspiler]
        // private static IEnumerable<CodeInstruction> Modify_ConvertVelocityWithFins(
        //     IEnumerable<CodeInstruction> instructions)
        // {
        //     /* REMOVE:
        //      * IL_0129: ldarg.0      // missile
        //      * IL_012a: callvirt     instance class [UnityEngine.PhysicsModule]UnityEngine.Rigidbody [Core]BrilliantSkies.Core.UniverseRepresentation.ThreadedGameObject::get_Rigidbody()
        //      * IL_012f: ldloc.s      vector3_3
        //      * IL_0131: ldarg.0      // missile
        //      * IL_0132: callvirt     instance class [UnityEngine.PhysicsModule]UnityEngine.Rigidbody [Core]BrilliantSkies.Core.UniverseRepresentation.ThreadedGameObject::get_Rigidbody()
        //      * IL_0137: callvirt     instance float32 [UnityEngine.PhysicsModule]UnityEngine.Rigidbody::get_mass()
        //      * IL_013c: call         valuetype [UnityEngine.CoreModule]UnityEngine.Vector3 [UnityEngine.CoreModule]UnityEngine.Vector3::op_Multiply(valuetype [UnityEngine.CoreModule]UnityEngine.Vector3, float32)
        //      * IL_0141: callvirt     instance void [UnityEngine.PhysicsModule]UnityEngine.Rigidbody::AddForce(valuetype [UnityEngine.CoreModule]UnityEngine.Vector3)
        //      * IL_0146: nop
        //      *
        //      *
        //      */
        //     var codes = instructions.ToList();
// 
// 
        //     var i = 0;
        //     var addForceEncounter = 0;
        //     while (i < codes.Count)
        //     {
        //         if (PatchHelper.SlidingWindowMatch(codes, AddForceCodeInstructions, i))
        //             if (++addForceEncounter == 2)
        //             {
        //                 i += AddForceCodeInstructions.Length;
        //                 continue;
        //             }
// 
        //         if (codes[i].opcode == Call &&
        //             codes[i].operand is MethodInfo { Name: "TwoPoints" } &&
        //             (codes.ElementAtOrDefault(i + 1)?.IsLdloc() ?? false))
        //         {
        //             yield return codes[i++];
        //             yield return codes[i++];
        //             yield return codes[i++];
// 
        //             yield return new CodeInstruction(Ldc_R4, 10f);
        //             yield return new CodeInstruction(Mul);
        //         }
// 
        //         yield return codes[i++];
        //     }
        // }
    }
}