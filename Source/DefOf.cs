using RimWorld;
using Verse;

namespace MoreInfo
{
    [DefOf]
    public static class GluThingCategoryDefOf
    {
        public static ThingCategoryDef BTextiles;
        public static ThingCategoryDef HTextiles;

        static GluThingCategoryDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(GluThingCategoryDefOf));
        }
    }

    [DefOf]
    public static class MoreNeedDefOf
    {
        public static NeedDef SuppressionCE;

        static MoreNeedDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(MoreNeedDefOf));
        }
    }
}
