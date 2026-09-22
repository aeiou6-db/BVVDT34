using GHPC.Equipment;
using ModUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BVVDT34
{
    public class Stalinium : Module
    {
        public static ArmorCodexScriptable stalinium_driver_codex;

        public static ArmorCodexScriptable stalinium_hull_codex;

        public static ArmorCodexScriptable stalinium_turret_codex;

        public static ArmorCodexScriptable[] stalinium_armor_codexes;
        public override void LoadStaticAssets()
        {
            stalinium_driver_codex = ScriptableObject.CreateInstance<ArmorCodexScriptable>();
            stalinium_driver_codex.name = "Stalinium Driver Hatch";

            stalinium_hull_codex = ScriptableObject.CreateInstance<ArmorCodexScriptable>();
            stalinium_hull_codex.name = "Stalinium Hull";

            stalinium_turret_codex = ScriptableObject.CreateInstance<ArmorCodexScriptable>();
            stalinium_turret_codex.name = "Stalinium cast turret";

////////////////////////////////////////////////////////////////////////////////
            ArmorType staliniumdriver = new ArmorType();
            staliniumdriver.Name = "Stalinium Driver Hatch";
            staliniumdriver.CanRicochet = true;
            staliniumdriver.CanShatterLongRods = true;
            staliniumdriver.NormalizesHits = true;
            staliniumdriver.ThicknessSource = ArmorType.RhaSource.Multipliers;
            staliniumdriver.SpallAngleMultiplier = 0;
            staliniumdriver.SpallPowerMultiplier = 0f;
            staliniumdriver.RhaeMultiplierCe = 999999999f;
            staliniumdriver.RhaeMultiplierKe = 999999999f;
            stalinium_driver_codex.ArmorType = staliniumdriver;
///////////////////////////////////////////////////////////////////////////////
            ArmorType staliniumhull = new ArmorType();
            staliniumhull.Name = "Stalinium Hull";
            staliniumhull.CanRicochet = true;
            staliniumhull.CanShatterLongRods = true;
            staliniumhull.NormalizesHits = true;
            staliniumhull.ThicknessSource = ArmorType.RhaSource.Multipliers;
            staliniumhull.SpallAngleMultiplier = 1;
            staliniumhull.SpallPowerMultiplier = 1f;
            staliniumhull.RhaeMultiplierCe = 9999f;
            staliniumhull.RhaeMultiplierKe = 9999f;
            stalinium_hull_codex.ArmorType = staliniumhull;
//////////////////////////////////////////////////////////////////////////////////////
            ArmorType staliniumturret = new ArmorType();
            staliniumturret.Name = "Stalinium cast turret";
            staliniumturret.CanRicochet = true;
            staliniumturret.CanShatterLongRods = true;
            staliniumturret.NormalizesHits = true;
            staliniumturret.ThicknessSource = ArmorType.RhaSource.Multipliers;
            staliniumturret.SpallAngleMultiplier = 1;
            staliniumturret.SpallPowerMultiplier = 1f;
            staliniumturret.HarderIsBetter = true;
            staliniumturret.RhaeMultiplierCe = 999999f;
            staliniumturret.RhaeMultiplierKe = 999999f;
            stalinium_turret_codex.ArmorType = staliniumturret;
///////////////////////////////////////////////////////////////////////////////////////
        }
    }
}
