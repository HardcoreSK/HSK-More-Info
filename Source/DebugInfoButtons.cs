using System.Linq;
using System.Text;
using LudeonTK;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI.Group;

namespace MoreInfo
{
    public static class DebugInfoButtons
    {
        [DebugAction("HSK Debug", "Lord Status", actionType = DebugActionType.ToolMapForPawns, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ShowLordStatus(Pawn p)
        {
            if (p.DeadOrDowned)
                return;
            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Hey! I'm {p.Name?.ToStringFull ?? p.ToString()} {p.ageTracker.AgeNumberString} years old.\n");
            var lord = p.GetLord();
            if (lord != null)
            {
                sb.AppendLine($"My current Lord is: {lord} ({lord.GetUniqueLoadID()})\n");
                
                if (!lord.Graph.lordToils.NullOrEmpty())
                {
                    var toilStr = "(current toil = NULL)";
                    if (lord.CurLordToil != null)
                        toilStr = $"About my Lord toils: current toil: {lord.Graph.lordToils.IndexOf(lord.CurLordToil)} - {lord.CurLordToil}\n";

                    sb.AppendLine($"About my Lord toils: {toilStr}");

                    int i = 0;
                    foreach (LordToil toil in lord.Graph.lordToils)
                    {
                        i++;
                        if (toil == null) continue;
                        sb.AppendLine($"{i} - toil: {toil.GetType()}");
                    }
                }

                var reportStr = lord.LordJob.GetReport(p) ?? "(lord job report not set up (NULL)";
                sb.AppendLine($"\nLord Job is: {lord.LordJob.ToString()} ({reportStr})\n");
                sb.AppendLine($"Lord debug info:\n{lord.DebugString()}");
            }
            else
            {
                sb.AppendLine("I have no active Lord with me :( (Pawn Lord == NULL)");
            }

            var str = sb.ToString();
            Log.Message(str);
            Find.WindowStack.Add(new Dialog_MessageBox(str));
        }

        [DebugAction("HSK Debug", "Job Status", actionType = DebugActionType.ToolMapForPawns, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ShowJobQueue(Pawn p)
        {
            if (p.DeadOrDowned)
                return;
            
            var sb = new StringBuilder();
            sb.AppendInNewLine($"Hey! I'm {p.Name?.ToStringFull ?? p.ToString()} and I'm busy.\n");
            sb.AppendInNewLine($"My current job is {p.CurJob} driven by {p.jobs.curDriver}.\n");
            sb.AppendInNewLine(p.jobs.curDriver.GetReport());
            if (p.jobs.curJob != null)
                sb.AppendInNewLine($"Current job have {p.jobs.curJob.count} jobs\n");
            var queue = p.jobs.jobQueue;
            if (queue != null)
            {
                sb.AppendInNewLine($"\nAlso I have {queue.Count} jobs to do in queue.");
                if (queue.Count > 0)
                {
                    sb.AppendInNewLine($"Here my queued jobs:");
                    for (int i = 0; i < queue.Count; i++)
                    {
                        var qj = queue[i];
                        if (qj != null)
                        {
                            sb.AppendInNewLine($"\n{i} - {qj.job} driven by {qj.job.GetCachedDriverDirect}");
                            sb.AppendInNewLine(qj.job.GetCachedDriverDirect.GetReport());
                        }
                    }
                }
            }

            var report = sb.ToString();
            Log.Message(report);
            Find.WindowStack.Add(new Dialog_MessageBox(report));
        }

        //Show Storyteller detailed info 
        [DebugAction("HSK Debug", "Storyteller Info", actionType = DebugActionType.Action, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void StorytellerFix_GetDebugInfo()
        {
            var sb = new StringBuilder();
            sb.AppendInNewLine($"Storyteller: {Find.Storyteller.def.label}\n");
            sb.AppendInNewLine($"Difficulty: {Find.Storyteller.difficultyDef.label}\n");
            var comps = Find.Storyteller.storytellerComps;
            sb.AppendInNewLine($"Storyteller have {comps.Count} comps:\n");
            foreach (var comp in comps)
            {
                sb.AppendInNewLine($"{comp}");
                sb.AppendInNewLine($"{comp.ToStringDeep(" - ")}");
                sb.AppendInNewLine($"{comp.props.ToStringDeep("\t")}\n");
            }
            sb.AppendInNewLine($"\nStoryteller basic debug info:\n{Find.Storyteller.DebugString()}\n");
            sb.AppendInNewLine($"Time is: {Time.frameCount} \n");
            
            Find.WindowStack.Add(new Dialog_MessageBox(sb.ToString()));
        }

        //Show Storyteller events Queue 
        [DebugAction("HSK Debug", "Show Event Queue", actionType = DebugActionType.Action, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void StorytellerFix_GetInfoButton()
        {
            string teller = Find.Storyteller.def.label;
            string difficulty = Find.Storyteller.difficultyDef.label;
            int queueLength = Find.Storyteller.incidentQueue.Count;
            string queue = Find.Storyteller.incidentQueue.DebugQueueReadout;
            Find.WindowStack.Add(new Dialog_MessageBox($"Storyteller: {teller}.\n\nDifficulty: {difficulty}.\n\nIncidents in the queue: {queueLength}.\n\nIncidents:\n\n{queue}"));
        }

        //Clear Storyteller Queue button
        [DebugAction("HSK Debug", "Clear Event Queue", actionType = DebugActionType.Action, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void StorytellerFix_ClearButton()
        {
            int currentQueueLength = Find.Storyteller.incidentQueue.Count;
            if (currentQueueLength != 0)
            {
                Find.Storyteller.incidentQueue.Clear();
            }
            var sb = new StringBuilder();
            int newQueueLength = Find.Storyteller.incidentQueue.Count;
            bool cleared = newQueueLength != currentQueueLength;

            sb.AppendLine($"Current queue length: {currentQueueLength}");
            if (cleared)
                sb.AppendLine($"\nThe queue is cleared.\n\nRemaining incidents in the storyteller queue: {newQueueLength}");
            else
                sb.AppendLine($"\nThe queue is NOT cleared.\n\nThe number of incidents has not changed.\n\nRemaining incidents in the storyteller queue: {newQueueLength}");
            Find.WindowStack.Add(new Dialog_MessageBox(sb.ToString()));
        }

        //Show current map lords statistics
        [DebugAction("HSK Debug", "Show lords statistics", actionType = DebugActionType.Action, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void LordsStatistic()
        {
            var mapName = Find.CurrentMap.ToString();
            var lordsCount = Find.CurrentMap.lordManager.lords.Count;
            var emptyLords = Find.CurrentMap.lordManager.lords
                .Where(x => x.ownedPawns.NullOrEmpty())
                .ToList();
            var emptyBuilds = emptyLords
                .Where(x => x.ownedBuildings.NullOrEmpty())
                .ToList();
            Find.WindowStack.Add(new Dialog_MessageBox($"Map: {mapName}.\n\nLord Manager have: {lordsCount} lords.\n\nThere are {emptyLords.Count} lords with null or empty pawn list and {emptyBuilds.Count} lords with null or empty buildings list."));
        }

        //Clear current map empty lords
        [DebugAction("HSK Debug", "Clear empty lords", actionType = DebugActionType.Action, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ClearEmptyLords()
        {
            var mapName = Find.CurrentMap.ToString();
            int lordsCount = 0;
            int emptyLordCount = 0;
            int cleanedLords = 0;
            if (!Find.CurrentMap.lordManager.lords.NullOrEmpty())
            {
                lordsCount = Find.CurrentMap.lordManager.lords.Count;
                var emptyLords = Find.CurrentMap.lordManager.lords
                    .Where(x => x.ownedPawns.NullOrEmpty())
                    .ToList();
                emptyLordCount = emptyLords.Count;
                foreach (var lord in emptyLords)
                {
                    Find.CurrentMap.lordManager.RemoveLord(lord);
                }
                cleanedLords = lordsCount - Find.CurrentMap.lordManager.lords.Count;
            }
            Find.WindowStack.Add(new Dialog_MessageBox($"Map: {mapName}.\n\nLord Manager has: {lordsCount} lords.\n\nThere are: {emptyLordCount} lords with null or empty pawn list.\n\nCleaned {cleanedLords} lords."));
        }

        //Current sky info
        [DebugAction("HSK Debug", "Show sky info", actionType = DebugActionType.Action, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ShowSkyInfo()
        {
            var mapName = Find.CurrentMap.ToString();
            string skyInfo = Find.CurrentMap.skyManager.DebugString();
            Find.WindowStack.Add(new Dialog_MessageBox($"Map: {mapName}.\n\n{skyInfo}"));
        }

        //Thing comps
        [DebugAction("HSK Debug", "Show Thing Comps", actionType = DebugActionType.ToolMap, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void ShowThingComps()
        {
            var items = Find.CurrentMap.thingGrid.ThingsAt(UI.MouseCell());
            if (items.EnumerableNullOrEmpty())
            {
                return;
            }
            
            var sb = new StringBuilder();
            foreach (var thing in items.Where(x => x is ThingWithComps).Cast<ThingWithComps>())
            {
                sb.AppendLine($"{thing.LabelCap} ({thing.GetType()}) have {thing.AllComps.Count} comps"); // fumo: 1 comp / x comps? I'm lazy

                if (thing.AllComps.Count == 0)
                    continue;

                foreach (var comp in thing.AllComps)
                {
                    sb.AppendLine($" - {comp.GetType()}");
                }
            }

            if (sb.Length > 0)
            {
                Find.WindowStack.Add(new Dialog_MessageBox(sb.ToString()));
            }
            else
            {
                Messages.Message("No Thing with comps are found under cursor.", MessageTypeDefOf.RejectInput, false);
            }
        }
    }
}
