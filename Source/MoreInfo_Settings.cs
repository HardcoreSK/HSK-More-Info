using UnityEngine;
using Verse;

namespace MoreInfo
{
    public class MoreInfo_Settings : ModSettings
    {
        public static bool showTextileInfo = true;
        public static bool showApparelArmorStats = true;
        public static bool showWorkTableSpeedFactor = true;
        public static bool showBedStats = true;
        public static bool showAmmoStats = true;
        public static bool showLoadedAmmoStats = true;
        public static bool showColonistSuppression = true;
        public static bool showColonistSuppressionNeeds = true;
        public static bool tinyNeedsSupperssionBar = false;
        public static bool showEnemySuppression = false;
        public static bool showMovingSpeed = true;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref showTextileInfo, "showTextileInfo", true);
            Scribe_Values.Look(ref showApparelArmorStats, "showApparelArmorStats", true);
            Scribe_Values.Look(ref showWorkTableSpeedFactor, "showWorkTableSpeedFactor", true);
            Scribe_Values.Look(ref showBedStats, "showBedStats", true);
            Scribe_Values.Look(ref showAmmoStats, "showAmmoStats", true);
            Scribe_Values.Look(ref showLoadedAmmoStats, "showLoadedAmmoStats", true);
            Scribe_Values.Look(ref showColonistSuppression, "showColonistSuppression", true);
            Scribe_Values.Look(ref showColonistSuppressionNeeds, "showColonistSuppressionNeeds", true);
            Scribe_Values.Look(ref tinyNeedsSupperssionBar, "tinyNeedsBar", false);
            Scribe_Values.Look(ref showEnemySuppression, "showEnemySuppression", false);
            Scribe_Values.Look(ref showMovingSpeed, "showMovingSpeed", true);

            base.ExposeData();
        }

        public static void DoSettingsWindowContents(Rect rect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(rect);
            listingStandard.Gap(10f);

            listingStandard.CheckboxLabeled("HMI_showTextileInfo".Translate(), ref showTextileInfo, "HMI_showTextileInfo_ToolTip".Translate());
            listingStandard.Gap(10f);

            listingStandard.CheckboxLabeled("HMI_showApparelArmorStats".Translate(), ref showApparelArmorStats, "HMI_showApparelArmorStats_ToolTip".Translate());
            listingStandard.Gap(10f);

            listingStandard.CheckboxLabeled("HMI_showWorkTableSpeedFactor".Translate(), ref showWorkTableSpeedFactor, "HMI_showWorkTableSpeedFactor_ToolTip".Translate());
            listingStandard.Gap(10f);

            listingStandard.CheckboxLabeled("HMI_showBedStats".Translate(), ref showBedStats, "HMI_showBedStats_ToolTip".Translate());
            listingStandard.Gap(10f);

            listingStandard.CheckboxLabeled("HMI_showAmmoStats".Translate(), ref showAmmoStats, "HMI_showAmmoStats_ToolTip".Translate());
            listingStandard.Gap(10f);

            listingStandard.CheckboxLabeled("HMI_showLoadedAmmoStats".Translate(), ref showLoadedAmmoStats, "HMI_showLoadedAmmoStats_ToolTip".Translate());
            listingStandard.Gap(10f);

            listingStandard.CheckboxLabeled("HMI_showColonistSuppression".Translate(), ref showColonistSuppression, "HMI_showColonistSuppression_ToolTip".Translate());
            listingStandard.Gap(10f);

            listingStandard.CheckboxLabeled("HMI_showColonistSuppressionNeeds".Translate(), ref showColonistSuppressionNeeds, "HMI_showColonistSuppressionNeeds_ToolTip".Translate());
            listingStandard.Gap(10f);

            listingStandard.CheckboxLabeled("HMI_tinyNeedsSupperssionBar".Translate(), ref tinyNeedsSupperssionBar, "HMI_tinyNeedsSupperssionBar_ToolTip".Translate());
            listingStandard.Gap(10f);

            listingStandard.CheckboxLabeled("HMI_showEnemySuppression".Translate(), ref showEnemySuppression, "HMI_showEnemySuppression_ToolTip".Translate());
            listingStandard.Gap(10f);

            listingStandard.CheckboxLabeled("HMI_showMovingSpeed".Translate(), ref showMovingSpeed, "HMI_showMovingSpeed_ToolTip".Translate());
            listingStandard.Gap(10f);

            Rect reset = listingStandard.GetRect(Text.LineHeight);
            reset.width /= 4f;
            bool resetButton = Widgets.ButtonText(reset, "HMI_ResetButton".Translate());
            if (resetButton)
            {
                showTextileInfo = true;
                showApparelArmorStats = true;
                showWorkTableSpeedFactor = true;
                showBedStats = true;
                showAmmoStats = true;
                showLoadedAmmoStats = true;
                showColonistSuppression = true;
                showColonistSuppressionNeeds = true;
                tinyNeedsSupperssionBar = false;
                showEnemySuppression = false;
                showMovingSpeed = true;
            }
            listingStandard.Gap(10f);
            listingStandard.End();
        }
    }
}
