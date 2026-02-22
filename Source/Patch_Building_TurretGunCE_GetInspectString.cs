using System.Text;
using CombatExtended;
using HarmonyLib;
using Verse;

namespace MoreInfo
{
    [HarmonyPatch(typeof(Building_TurretGunCE), nameof(Building_TurretGunCE.GetInspectString))]
    public class Patch_Building_TurretGunCE_GetInspectString
    {
        public static void Postfix(Building_TurretGunCE __instance, ref string __result)
        {
            if (MoreInfo_Settings.showLoadedAmmoStats && __instance != null && __instance.Gun != null)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(__result);
                var ammoComp = __instance.Gun.TryGetComp<CompAmmoUser>();
                if (ammoComp != null)
                {
                    builder.AppendInNewLine(ammoComp.CurrentAmmo.LabelCap);
                    builder.AppendInNewLine(ammoComp.CurAmmoProjectile.GetProjectileReadout(__instance));
                }
                __result = builder.ToString().TrimEndNewlines();
            }
        }
    }
}
