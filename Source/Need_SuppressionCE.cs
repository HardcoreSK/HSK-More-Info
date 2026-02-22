using System.Collections.Generic;
using System.Text;
using CombatExtended;
using RimWorld;
using UnityEngine;
using Verse;

namespace MoreInfo
{
    public class Need_SuppressionCE : Need
    {
        public Need_SuppressionCE(Pawn newPawn) : base(newPawn)
        {
        }

        public CompSuppressable SuppressionTracker => this.pawn.TryGetComp<CompSuppressable>();

        public float SuppressionThreshold => SuppressionTracker != null ? SuppressionTracker.SuppressionThreshold : 1f;

        public override float MaxLevel => SuppressionTracker != null ? CompSuppressable.maxSuppression : 1f;

        public override void SetInitialLevel()
        {
            this.CurLevel = SuppressionTracker != null ? SuppressionTracker.CurrentSuppression : 1f;
        }

        public override float CurLevel => SuppressionTracker != null ? this.SuppressionTracker.CurrentSuppression : 1f;

        public override void DrawOnGUI(Rect rect, int maxThresholdMarkers = int.MaxValue, float customMargin = -1, bool drawArrows = true, bool doTooltip = true, Rect? rectForTooltip = null, bool drawLabel = true)
        {
            if (this.threshPercents == null)
            {
                this.threshPercents = new List<float>();
            }
            this.threshPercents.Clear();
            this.threshPercents.Add(SuppressionThreshold / this.MaxLevel);
            this.threshPercents.Add(SuppressionThreshold * 10 > this.MaxLevel ? 1f : SuppressionThreshold * 10 / this.MaxLevel);
            base.DrawOnGUI(rect, maxThresholdMarkers, customMargin, drawArrows, doTooltip, rectForTooltip, drawLabel);
        }

        public override string GetTipString()
        {
            var sb = new StringBuilder();
            sb.AppendInNewLine($"{base.LabelCap}: {base.CurLevelPercentage.ToStringPercent()} ({this.CurLevel:0.##} / {this.MaxLevel:0.##})\n");
            sb.AppendInNewLine($"{"MWI_Pawn_suppressability".Translate()}: {pawn.GetStatValue(CE_StatDefOf.Suppressability):P0}");
            sb.AppendInNewLine($"{"MWI_Suppression_threshold".Translate()}: {SuppressionThreshold:0.00}");
            sb.AppendInNewLine($"{"MWI_Accumulated_suppression_amount".Translate()}: {SuppressionTracker.CurrentSuppression:0}\n");
            sb.AppendInNewLine(this.def.description);
            return sb.ToString();
        }

        public override void NeedInterval()
        {
            this.def.major = !MoreInfo_Settings.tinyNeedsSupperssionBar;
        }
    }
}
