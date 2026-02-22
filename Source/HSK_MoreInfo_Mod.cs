using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace MoreInfo
{
    [StaticConstructorOnStartup]
    public static class Start
    {
        static Start()
        {
            Log.Message("HSK More Info loaded successfully!");
            var harmony = new Harmony("glucocorticoid.moreinfo.patch");
            harmony.PatchAll();
        }
    }

    public class MoreInfo_Mod : Mod
    {
        public static MoreInfo_Settings Settings { get; private set; }

        public MoreInfo_Mod(ModContentPack content) : base(content)
        {
            Settings = GetSettings<MoreInfo_Settings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            MoreInfo_Settings.DoSettingsWindowContents(inRect);
        }
        public override string SettingsCategory()
        {
            return "HSK More Info";
        }
    }
}
