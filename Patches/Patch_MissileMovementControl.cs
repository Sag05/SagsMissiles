using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using BrilliantSkies.Ftd.Missiles.Blueprints;
using HarmonyLib;
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
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Modify_ConvertVelocityWithFins(
            IEnumerable<CodeInstruction> instructions)
        {
            /* REMOVE:
             * IL_0129: ldarg.0      // missile
             * IL_012a: callvirt     instance class [UnityEngine.PhysicsModule]UnityEngine.Rigidbody [Core]BrilliantSkies.Core.UniverseRepresentation.ThreadedGameObject::get_Rigidbody()
             * IL_012f: ldloc.s      vector3_3
             * IL_0131: ldarg.0      // missile
             * IL_0132: callvirt     instance class [UnityEngine.PhysicsModule]UnityEngine.Rigidbody [Core]BrilliantSkies.Core.UniverseRepresentation.ThreadedGameObject::get_Rigidbody()
             * IL_0137: callvirt     instance float32 [UnityEngine.PhysicsModule]UnityEngine.Rigidbody::get_mass()
             * IL_013c: call         valuetype [UnityEngine.CoreModule]UnityEngine.Vector3 [UnityEngine.CoreModule]UnityEngine.Vector3::op_Multiply(valuetype [UnityEngine.CoreModule]UnityEngine.Vector3, float32)
             * IL_0141: callvirt     instance void [UnityEngine.PhysicsModule]UnityEngine.Rigidbody::AddForce(valuetype [UnityEngine.CoreModule]UnityEngine.Vector3)
             * IL_0146: nop
             *
             *
             */
            var codes = instructions.ToList();


            var i = 0;
            var addForceEncounter = 0;
            while (i < codes.Count)
            {
                if (PatchHelper.SlidingWindowMatch(codes, AddForceCodeInstructions, i))
                    if (++addForceEncounter == 2)
                    {
                        i += AddForceCodeInstructions.Length;
                        continue;
                    }

                if (codes[i].opcode == Call &&
                    codes[i].operand is MethodInfo { Name: "TwoPoints" } &&
                    (codes.ElementAtOrDefault(i + 1)?.IsLdloc() ?? false))
                {
                    yield return codes[i++];
                    yield return codes[i++];
                    yield return codes[i++];

                    yield return new CodeInstruction(Ldc_R4, 10f);
                    yield return new CodeInstruction(Mul);
                }

                yield return codes[i++];
            }
        }
    }
}