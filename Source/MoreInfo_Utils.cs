using System.Collections;
using System.Text;
using RimWorld;
using Verse;

namespace MoreInfo
{
    public static class MoreInfo_Utils
    {
        public static bool IsTextiles(this ThingDef def)
        {
            if (def.category == ThingCategory.Item && def.thingCategories != null)
            {
                return def.thingCategories.Contains(GluThingCategoryDefOf.BTextiles)
                        || def.thingCategories.Contains(GluThingCategoryDefOf.HTextiles)
                        || def.thingCategories.Contains(ThingCategoryDefOf.Leathers);
            }
            return false;
        }

        public static string ToStringAbstract(this object obj)
        {
            var sb = new StringBuilder();
            if (obj == null)
                sb.Append("null");
            else
            {
                if (obj is IEnumerable collection)
                    sb.Append(string.Join(", ", collection));
                else
                    sb.Append(obj.ToString());
            }
            return sb.ToString();
        }

        public static string ToStringDeep(this object obj, string spacer = "")
        {
            var sb = new StringBuilder();
            var fields = obj.GetType().GetFields();
            foreach (var fieldInfo in fields)
                sb.AppendInNewLine($"{spacer}{fieldInfo.Name} = {fieldInfo.GetValue(obj).ToStringAbstract()}");
            return sb.ToString();
        }

        public static float GetMoveSpeed(Pawn pawn)
        {
            if (!pawn.pather.Moving)
                return 0f;

            var cost = pawn.pather.CostToMoveIntoCell(pawn.pather.nextCell);
            return 60 / cost;
        }
    }
}
