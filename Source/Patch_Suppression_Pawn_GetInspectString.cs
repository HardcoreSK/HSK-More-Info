using System.Text;
using CombatExtended;
using HarmonyLib;
using RimWorld;
using Verse;

namespace MoreInfo
{
    [HarmonyPatch(typeof(Pawn), nameof(Pawn.GetInspectString))]
    public class Patch_Suppression_Pawn_GetInspectString
    {
        public static void Postfix(Pawn __instance, ref string __result)
        {
            if ((__instance.IsColonist && MoreInfo_Settings.showColonistSuppression)
                || (__instance.RaceProps.Humanlike && __instance.HostileTo(Find.FactionManager.OfPlayer) && MoreInfo_Settings.showEnemySuppression))
            {
                var compSuppressable = __instance.TryGetComp<CompSuppressable>();
                if (compSuppressable != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(__result);

                    var supAmount = string.Format("{0}: {1:0}", "MWI_Accumulated_suppression_amount".Translate(), compSuppressable.CurrentSuppression);
                    sb.AppendInNewLine(supAmount);
                    if (compSuppressable.isSuppressed)
                        sb.Append(string.Format("({0})", "MWI_Suppressed".Translate()));

                    var thresh = compSuppressable.SuppressionThreshold;
                    var supThresh = string.Format("{0}: {1:0.00}", "MWI_Suppression_threshold".Translate(), thresh);
                    sb.AppendInNewLine(supThresh);

                    var suppresab = __instance.GetStatValue(CE_StatDefOf.Suppressability);
                    var suppresabText = string.Format("{0}: {1:P0}", "MWI_Pawn_suppressability".Translate(), suppresab);
                    sb.AppendInNewLine(suppresabText);

                    __result = sb.ToString().TrimEndNewlines();
                }
            }
        }
    }
}
