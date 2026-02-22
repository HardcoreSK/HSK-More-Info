using CombatExtended;
using HarmonyLib;
using RimWorld;
using Verse;

namespace MoreInfo
{
    [HarmonyPatch(typeof(Pawn_NeedsTracker), nameof(Pawn_NeedsTracker.ShouldHaveNeed))]
    public class Patch_Pawn_NeedsTracker_ShouldHaveNeed
    {
        public static void Postfix(Pawn_NeedsTracker __instance, NeedDef nd, ref bool __result)
        {
            if (__result && nd == MoreNeedDefOf.SuppressionCE)
            {
                if (MoreInfo_Settings.showColonistSuppressionNeeds)
                {
                    var pawn = __instance.pawn;
                    if (pawn != null)
                    {
                        if (pawn.TryGetComp<CompSuppressable>() == null)
                            __result = false;
                    }
                }
                else
                    __result = false;
            }
        }
    }
}
