using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using BrilliantSkies.Core.Logger;
using HarmonyLib;

namespace SagsMissiles
{
    public class PatchHelper
    {
        public static bool SlidingWindowMatch(List<CodeInstruction> codes, OpCode[] reference, int startIndex)
        {
            return SlidingWindowMatch(codes.ToArray(), reference, startIndex);
        }

        public static bool SlidingWindowMatch(CodeInstruction[] codes, OpCode[] reference, int startIndex)
        {
            if (startIndex + reference.Length > codes.Length)
                return false;

            for (var i = 0; i < reference.Length; i++)
                if (codes[startIndex + i].opcode != reference[i])
                    return false;

            AdvLogger.LogInfo(
                $"[PatchHelper.SlidingWindowMatch] MATCH:\n{string.Join('\n', reference.Select(e => e.Name + ";"))}" +
                $"\n\nAGAINST:\n{string.Join('\n', codes.Select(e => e.opcode.Name + ";"))}\n\nINDEX: {startIndex}"
            );
            return true;
        }
    }
}