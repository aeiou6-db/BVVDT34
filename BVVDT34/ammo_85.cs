using GHPC.Effects;
using GHPC.Effects.Voices;
using GHPC.State;
using GHPC.Weaponry;
using GHPC.Weapons;
using MelonLoader;
using ModUtil;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace BVVDT34
{
    public class ammo_85 : Module
    {
        public static AmmoClipCodexScriptable clip_codex_ap;
        public static AmmoType.AmmoClip clip_ap;
        public static AmmoCodexScriptable ammo_codex_ap;
        public static AmmoType ammo_ap;
        public static GameObject ammo_ap_vis = null;

        public static AmmoClipCodexScriptable clip_codex_heat;
        public static AmmoType.AmmoClip clip_heat;
        public static AmmoCodexScriptable ammo_codex_heat;
        public static AmmoType ammo_heat;
        public static GameObject ammo_heat_vis = null;

        public static AmmoClipCodexScriptable clip_codex_mininuke;
        public static AmmoType.AmmoClip clip_mininuke;
        public static AmmoCodexScriptable ammo_codex_mininuke;
        public static AmmoType ammo_mininuke;
        public static GameObject ammo_mininuke_vis = null;
        public static AmmoType mininuke_forward_frag = new AmmoType();

        public static AmmoClipCodexScriptable clip_codex_missile;
        public static AmmoType.AmmoClip clip_missile;
        public static AmmoCodexScriptable ammo_codex_missile;
        public static AmmoType ammo_missile;
        public static GameObject ammo_missile_vis = null;

        public static AmmoClipCodexScriptable clip_codex_legacy;
        public static AmmoType.AmmoClip clip_legacy;
        public static AmmoCodexScriptable ammo_codex_legacy;
        public static AmmoType ammo_legacy;
        public static GameObject ammo_kegacy_vis = null;

        public static AmmoClipCodexScriptable clip_codex_mg;
        public static AmmoType.AmmoClip clip_mg;
        public static AmmoCodexScriptable ammo_codex_mg;
        public static AmmoType ammo_mg;
        public static GameObject ammo_mg_vis = null;

        public static AmmoClipCodexScriptable clip_codex_mghe;
        public static AmmoType.AmmoClip clip_mghe;
        public static AmmoCodexScriptable ammo_codex_mghe;
        public static AmmoType ammo_mghe;
        public static GameObject ammo_mghe_vis = null;

        public override void UnloadDynamicAssets()
        {
            GameObject.DestroyImmediate(ammo_ap_vis);
            GameObject.DestroyImmediate(ammo_heat_vis);
            GameObject.DestroyImmediate(ammo_mininuke_vis);
            GameObject.DestroyImmediate(ammo_missile_vis);
            GameObject.DestroyImmediate(ammo_mg_vis);
        }
        public static void CreateCompositeOptimizations()
        {
            var mg_optimize = new List<AmmoType.ArmorOptimization>() { };

            string[] composite_names = new string[] {
                "Reinforced Mantlet Roof",
                "Stalinium Hull",
                "Reinforced Gun Mantlet",
                "Stalinium-Alloyed Gun Barrel",
                "Reinforced Gun Breach",
                "Stalinium Track",
                "Stalinium Driver's Hatch",
                "Stalinium Front Plate",
                "Stalinium-Reinforced Cast Turret",
                "Vorschlaghammer heavy composite",
                "Vorschlaghammer medium composite",
                "Vorschlaghammer light composite",
                "Eber heavy composite",
                "Eber medium composite",
                "Eber light composite",
                "Abrams special armor gen 1 hull front",
                "Abrams special armor gen 1 mantlet",
                "Abrams special armor gen 1 turret cheeks",
                "Abrams special armor gen 1 turret sides",
                "Abrams special armor gen 0 turret cheeks",
                "Corundum ball armor",
                "Kvartz",
        };

            ArmorCodexScriptable[] armours = Resources.FindObjectsOfTypeAll<ArmorCodexScriptable>();

            foreach (string name in composite_names)
            {
                IEnumerable<ArmorCodexScriptable> possible_armours = armours.Where(o => o.name == name);

                if (possible_armours.Count() == 0) continue;

                ArmorCodexScriptable armour = possible_armours.First();

                AmmoType.ArmorOptimization optimization_mg = new AmmoType.ArmorOptimization();
                optimization_mg.Armor = armour;
                optimization_mg.RhaRatio = 0.45f;
                mg_optimize.Add(optimization_mg);
            }
            ammo_mg.ArmorOptimizations = mg_optimize.ToArray<AmmoType.ArmorOptimization>();
            ammo_mghe.ArmorOptimizations = ammo_mg.ArmorOptimizations;
        }
        public static IEnumerator SetupEraOptimizations(GameState _)
        {
            ArmorCodexScriptable[] armor_codices = Resources.FindObjectsOfTypeAll<ArmorCodexScriptable>();
            ArmorCodexScriptable[] k1_k5_m1_codices = armor_codices.Where
            (
                o =>
                o.name.Contains("Kontakt-1") ||
                o.name.Contains("Kontakt-5") ||
                o.name.Contains("M1 ERA")
            ).ToArray();

            ArmorCodexScriptable[] relikt_codices = armor_codices.Where(o => o.name.Contains("Relikt")).ToArray();

            List<AmmoType.ArmorOptimization> optimizations = new List<AmmoType.ArmorOptimization>();

            foreach (ArmorCodexScriptable codex in k1_k5_m1_codices)
            {
                optimizations.Add(Util.CreateArmourOptimization(codex, 0.002f));
            }

            foreach (ArmorCodexScriptable relikt_codex in relikt_codices)
            {
                optimizations.Add(Util.CreateArmourOptimization(relikt_codex, 0.08f));
            }

            ammo_missile.ArmorOptimizations = optimizations.ToArray();
            ammo_heat.ArmorOptimizations = ammo_missile.ArmorOptimizations;

            yield break;
        }
        public override void LoadDynamicAssets()
        {
            ammo_ap = new AmmoType();
            Util.ShallowCopy(ammo_ap, SharedAssets.ammo_3bm32);
            ammo_ap.Name = "BR999PM APHEFSDS-T";
            ammo_ap.Category = AmmoType.AmmoCategory.Explosive;
            ammo_ap.ShortName = AmmoType.AmmoShortName.Sabot;
            ammo_ap.DetonateEffect = SharedAssets.ammo_3bk18m.DetonateEffect;
            ammo_ap.TerrainImpactEffect = SharedAssets.ammo_3bm32.TerrainImpactEffect;
            ammo_ap.Guidance = AmmoType.GuidanceType.Unguided;
            ammo_ap.ImpactAudio = GHPC.Audio.ImpactAudioType.MainGunHighExplosive;
            ammo_ap.AlwaysProduceBlast = true;
            ammo_ap.Caliber = 85;
            ammo_ap.Coeff = 0.25f;
            ammo_ap.TntEquivalentKg = 1.75f;
            ammo_ap.CertainRicochetAngle = 0f;
            ammo_ap.ArmingDistance = 0f;
            ammo_ap.RhaToFuse = 25f;
            ammo_ap.ImpactFuseTime = 0.0013f;
            ammo_ap.RhaPenetration = 3000f;
            ammo_ap.Mass = 23.3f;
            ammo_ap.MuzzleVelocity = 2500f;
            ammo_ap.SpallMultiplier = 5f;
            ammo_ap.DetonateSpallCount = 60;
            ammo_ap.SphericalSpall = true;
            ammo_ap.Normalize = true;
            ammo_ap.IgnoreSlat = true;
            ammo_ap.MaxSpallRha = 70f;
            ammo_ap.MinSpallRha = 22f;
            ammo_ap.ImpactEffectDescriptor = new ParticleEffectsManager.ImpactEffectDescriptor()
            {
                HasImpactEffect = true,
                EffectSize = ParticleEffectsManager.EffectSize.Rocket,
                ImpactCategory = ParticleEffectsManager.Category.HighExplosive,
                Flags = ParticleEffectsManager.ImpactModifierFlags.Small,
                MinFilterStrictness = ParticleEffectsManager.FilterStrictness.Medium,
                RicochetType = ParticleEffectsManager.RicochetType.NormalTracer
            };

            Util.Coalesce(ref ammo_codex_ap);
            ammo_codex_ap.AmmoType = ammo_ap;
            ammo_codex_ap.name = "ammo_ap";

            clip_ap = new AmmoType.AmmoClip();
            clip_ap.Capacity = 1;
            clip_ap.Name = "BR999PM APHEFSDS-T";
            clip_ap.MinimalPattern = new AmmoCodexScriptable[1];
            clip_ap.MinimalPattern[0] = ammo_codex_ap;

            Util.Coalesce(ref clip_codex_ap);
            clip_codex_ap.name = "clip_ap";
            clip_codex_ap.ClipType = clip_ap;

            ammo_ap_vis = GameObject.Instantiate(SharedAssets.ammo_3bm32.VisualModel);
            ammo_ap_vis.name = "ap visual";
            ammo_ap.VisualModel = ammo_ap_vis;
            ammo_ap.VisualModel.GetComponent<AmmoStoredVisual>().AmmoType = ammo_ap;
            ammo_ap.VisualModel.GetComponent<AmmoStoredVisual>().AmmoScriptable = ammo_codex_ap;
            ammo_ap.ShotVisual.transform.localScale = new Vector3(1.6f, 1.6f, 3.5f);

            /*****************************************************************************************************/

            ammo_heat = new AmmoType();
            Util.ShallowCopy(ammo_heat, SharedAssets.ammo_3bk18m);
            ammo_heat.Name = "UBP-988M SAPHEAT-FS";
            ammo_heat.Guidance = AmmoType.GuidanceType.Unguided;
            ammo_heat.ShortName = AmmoType.AmmoShortName.Heat;
            ammo_heat.Caliber = 85;
            ammo_heat.Coeff = 0.65f;
            ammo_heat.TntEquivalentKg = 4.23f;
            ammo_heat.RhaToFuse = 25f;
            ammo_heat.ImpactFuseTime = 0f;
            ammo_heat.RhaPenetration = 5500f;
            ammo_heat.ImpactAudio = GHPC.Audio.ImpactAudioType.ArtilleryGun;
            ammo_heat.DetonateEffect = SharedAssets.ammo_3OF26.DetonateEffect;
            ammo_heat.TerrainImpactEffect = Resources.FindObjectsOfTypeAll<GameObject>().Where(o => o.name == "Artillery 155mm Terrain").First();
            ammo_heat.Mass = 47.5f;
            ammo_heat.ShotVisual.transform.localScale = new Vector3(2.5f, 2.5f, 3f);
            ammo_heat.MuzzleVelocity = 1850f;
            ammo_heat.ArmingDistance = 0f;
            ammo_heat.Tandem = true;
            ammo_heat.IgnoreSlat = true;
            ammo_heat.CertainRicochetAngle = 0f;
            ammo_heat.MaxSpallRha = 90f;
            ammo_heat.MinSpallRha = 12f;
            ammo_heat.SpallMultiplier = 3f;
            ammo_heat.ImpactEffectDescriptor = new ParticleEffectsManager.ImpactEffectDescriptor()
            {
                HasImpactEffect = true,
                EffectSize = ParticleEffectsManager.EffectSize.Missile,
                ImpactCategory = ParticleEffectsManager.Category.Heat,
                Flags = ParticleEffectsManager.ImpactModifierFlags.Medium,
                MinFilterStrictness = ParticleEffectsManager.FilterStrictness.Medium,
                RicochetType = ParticleEffectsManager.RicochetType.NormalTracer,
            };

            Util.Coalesce(ref ammo_codex_heat);
            ammo_codex_heat.AmmoType = ammo_heat;
            ammo_codex_heat.name = "ammo_heat";

            clip_heat = new AmmoType.AmmoClip();
            clip_heat.Capacity = 1;
            clip_heat.Name = "UBP-988M SAPHEAT-FS";
            clip_heat.MinimalPattern = new AmmoCodexScriptable[1];
            clip_heat.MinimalPattern[0] = ammo_codex_heat;

            Util.Coalesce(ref clip_codex_heat);
            clip_codex_heat.name = "clip_heat";
            clip_codex_heat.ClipType = clip_heat;

            ammo_heat_vis = GameObject.Instantiate(SharedAssets.ammo_3bk18m.VisualModel);
            ammo_heat_vis.name = "heat visual";
            ammo_heat.VisualModel = ammo_heat_vis;
            ammo_heat.VisualModel.GetComponent<AmmoStoredVisual>().AmmoType = ammo_heat;
            ammo_heat.VisualModel.GetComponent<AmmoStoredVisual>().AmmoScriptable = ammo_codex_heat;

            /*****************************************************************************************************/

            ammo_mininuke = new AmmoType();
            Util.ShallowCopy(ammo_mininuke, SharedAssets.ammo_3OF26);
            ammo_mininuke.Name = "3BV85 Micronuke";
            ammo_mininuke.ShortName = AmmoType.AmmoShortName.He;
            ammo_mininuke.Guidance = AmmoType.GuidanceType.Unguided;
            ammo_mininuke.Caliber = 85;
            ammo_mininuke.TntEquivalentKg = 86f;
            ammo_mininuke.RhaPenetration = 60f;
            ammo_mininuke.ImpactFuseTime = 0.0001f;
            ammo_mininuke.RhaToFuse = 5f;
            ammo_mininuke.DetonateEffect = SharedAssets.ammo_3OF26.DetonateEffect;
            ammo_mininuke.TerrainImpactEffect = Resources.FindObjectsOfTypeAll<GameObject>().Where(o => o.name == "250kg Bomb Dirt").First();
            ammo_mininuke.AlwaysProduceBlast = true;
            ammo_mininuke.SphericalSpall = true;
            ammo_mininuke.Coeff = 2.5f;
            ammo_mininuke.MuzzleVelocity = 550f;
            ammo_mininuke.Mass = 350f;
            ammo_mininuke.SpallMultiplier = 5f;
            ammo_mininuke.MaxSpallRha = 200f;
            ammo_mininuke.MinSpallRha = 30f;
            ammo_mininuke.DetonateSpallCount = 235;
            ammo_mininuke.ImpactEffectDescriptor = new ParticleEffectsManager.ImpactEffectDescriptor()
            {
                HasImpactEffect = true,
                EffectSize = ParticleEffectsManager.EffectSize.Bomb,
                ImpactCategory = ParticleEffectsManager.Category.HighExplosive,
                Flags = ParticleEffectsManager.ImpactModifierFlags.Huge,
                MinFilterStrictness = ParticleEffectsManager.FilterStrictness.Medium,
                RicochetType = ParticleEffectsManager.RicochetType.LargeTracer
            };
            ammo_mininuke.ImpactAudio = GHPC.Audio.ImpactAudioType.Bomb;
            Util.Coalesce(ref ammo_codex_mininuke);
            ammo_codex_mininuke.AmmoType = ammo_mininuke;
            ammo_codex_mininuke.name = "ammo_mininuke";

            clip_mininuke = new AmmoType.AmmoClip();
            clip_mininuke.Capacity = 1;
            clip_mininuke.Name = "3BV85 Micronuke";
            clip_mininuke.MinimalPattern = new AmmoCodexScriptable[1];
            clip_mininuke.MinimalPattern[0] = ammo_codex_mininuke;

            Util.Coalesce(ref clip_codex_mininuke);
            clip_codex_mininuke.name = "clip_mininuke";
            clip_codex_mininuke.ClipType = clip_mininuke;

            ammo_mininuke_vis = GameObject.Instantiate(SharedAssets.ammo_3OF26.VisualModel);
            ammo_mininuke_vis.name = "mininuke visual";
            ammo_mininuke.VisualModel = ammo_mininuke_vis;
            ammo_mininuke.VisualModel.GetComponent<AmmoStoredVisual>().AmmoType = ammo_mininuke;
            ammo_mininuke.VisualModel.GetComponent<AmmoStoredVisual>().AmmoScriptable = ammo_codex_mininuke;
            ammo_mininuke.ShotVisual.transform.localScale = new Vector3(1.5f, 1.5f, 4f);
            /////////////////////////////////////////////////////////////////////////////////////////////
            ammo_missile = new AmmoType();
            Util.ShallowCopy(ammo_missile, SharedAssets.ammo_kobra);
            ammo_missile.Name = "9M94ML Velikan";
            ammo_missile.Caliber = 85;
            ammo_missile.SpiralAngularRate = 0f;
            ammo_missile.SpiralPower = 0f;
            ammo_missile.MuzzleVelocity = 550f;
            ammo_missile.RhaPenetration = 200000f;
            ammo_missile.TntEquivalentKg = 50.5f;
            ammo_missile.Mass = 9025f;
            ammo_missile.Guidance = AmmoType.GuidanceType.Laser;
            ammo_missile.Flight = AmmoType.FlightPattern.Direct;
            ammo_missile.Category = AmmoType.AmmoCategory.Explosive;
            ammo_missile.Tandem = true;
            ammo_missile.IgnoreSlat = true;
            ammo_missile.DetonateEffect = SharedAssets.ammo_3OF26.DetonateEffect;
            ammo_missile.TerrainImpactEffect = Resources.FindObjectsOfTypeAll<GameObject>().Where(o => o.name == "250kg Bomb Dirt").First();
            ammo_missile.SphericalSpall = true;
            ammo_missile.TurnSpeed = 12.5f;
            ammo_missile.GuidanceLockoutTime = 0f;
            ammo_missile.Coeff = 0.5f;
            ammo_missile.AimPointMarch = -2f;
            ammo_missile.RangedFuseTime = 35f;
            ammo_missile.NoisePowerX = 0f;
            ammo_missile.NoisePowerY = 0f;
            ammo_missile.NoiseTimeScale = 1f;
            ammo_missile.SpallMultiplier = 10;
            ammo_missile.MinSpallRha = 25f;
            ammo_missile.MaxSpallRha = 120f;
            ammo_missile.DetonateSpallCount = 50;
            ammo_missile.RhaToFuse = 12f;
            ammo_missile.ImpactFuseTime = 0.05f;
            ammo_missile.TerrainImpactEffect = SharedAssets.ammo_3OF26.DetonateEffect;
            ammo_missile.DetonateEffect = SharedAssets.ammo_3OF26.DetonateEffect;
            ammo_missile.ImpactEffectDescriptor = new ParticleEffectsManager.ImpactEffectDescriptor()
            {
                HasImpactEffect = true,
                EffectSize = ParticleEffectsManager.EffectSize.Bomb,
                ImpactCategory = ParticleEffectsManager.Category.HighExplosive,
                Flags = ParticleEffectsManager.ImpactModifierFlags.Large,
                MinFilterStrictness = ParticleEffectsManager.FilterStrictness.Medium,
                RicochetType = ParticleEffectsManager.RicochetType.LargeTracer
            };
            Util.Coalesce(ref ammo_codex_missile);
            ammo_codex_missile.AmmoType = ammo_missile;
            ammo_codex_missile.name = "ammo_missile";

            clip_missile = new AmmoType.AmmoClip();
            clip_missile.Capacity = 1;
            clip_missile.Name = "9M94ML Velikan";
            clip_missile.MinimalPattern = new AmmoCodexScriptable[1];
            clip_missile.MinimalPattern[0] = ammo_codex_missile;

            Util.Coalesce(ref clip_codex_missile);
            clip_codex_missile.name = "clip_missile";
            clip_codex_missile.ClipType = clip_missile;

            ammo_missile_vis = GameObject.Instantiate(SharedAssets.ammo_kobra.VisualModel);
            ammo_missile_vis.name = "missile visual";
            ammo_missile.VisualModel = ammo_missile_vis;
            ammo_missile.VisualModel.GetComponent<AmmoStoredVisual>().AmmoType = ammo_missile;
            ammo_missile.VisualModel.GetComponent<AmmoStoredVisual>().AmmoScriptable = ammo_codex_missile;
            ammo_missile.ShotVisual = Resources.FindObjectsOfTypeAll<GameObject>().Where(o => o.name == "MILAN 1 visual").First();
            ammo_missile.ShotVisual.transform.localScale = new Vector3(2f, 2f, 2f);
            /////////////////////////////////////////////////////////////////////////////////////////////
            ammo_mghe = new AmmoType();
            Util.ShallowCopy(ammo_mghe, SharedAssets.ammo_3ubr6);
            ammo_mghe.Name = "20mm HEI-T";
            ammo_mghe.ShortName = AmmoType.AmmoShortName.Coax;
            ammo_mghe.Category = AmmoType.AmmoCategory.Explosive;
            ammo_mghe.UseTracer = true;
            ammo_mghe.Mass = 0.52f;
            ammo_mghe.Caliber = 20f;
            ammo_mghe.Coeff = 0.08f;
            ammo_mghe.TntEquivalentKg = 0.34f;
            ammo_mghe.ImpactFuseTime = 0;
            ammo_mghe.RhaToFuse = 0;
            ammo_mghe.MuzzleVelocity = 1955f;
            ammo_mghe.DetonateSpallCount = 15;
            ammo_mghe.MicroFragScaling = 1.8f;
            ammo_mghe.RhaPenetration = 23f;
            ammo_mghe.SpallMultiplier = 2f;
            ammo_mghe.MinSpallRha = 6;
            ammo_mghe.MaxSpallRha = 12;
            ammo_mghe.CertainRicochetAngle = 23.5f;
            ammo_mghe.TerrainImpactEffect = Resources.FindObjectsOfTypeAll<GameObject>().Where(o => o.name == "HE Terrain Impact").First();
            ammo_mghe.DetonateEffect = Resources.FindObjectsOfTypeAll<GameObject>().Where(o => o.name == "HEAT Impact").First();
            ammo_mghe.VisualType = SharedAssets.ammo_3bm22.VisualType;
            ammo_mghe.ImpactAudio = GHPC.Audio.ImpactAudioType.AutocannonExplosive;
            ammo_mghe.ImpactEffectDescriptor = new ParticleEffectsManager.ImpactEffectDescriptor()
            {
                HasImpactEffect = true,
                EffectSize = ParticleEffectsManager.EffectSize.Autocannon,
                ImpactCategory = ParticleEffectsManager.Category.HighExplosive,
                Flags = ParticleEffectsManager.ImpactModifierFlags.Small,
                MinFilterStrictness = ParticleEffectsManager.FilterStrictness.Medium,
                RicochetType = ParticleEffectsManager.RicochetType.SmallTracer
            };
            ammo_mghe.ImpactDecalDescriptor = new ImpactDecalsManager.ImpactDecalDescriptor()
            {
                HasImpactDecal = true,
                DecalCategory = ImpactDecalsManager.DecalCategory.Explosion,
                DecalType = ImpactDecalsManager.DecalType.Dent,
                DecalImpactAngle = ImpactDecalsManager.DecalImpactAngle.High,
                Flags = ImpactDecalsManager.DecalModifierFlags.Small,
                MinFilterStrictness = ImpactDecalsManager.DecalFilterStrictness.Low,
            };
            Util.Coalesce(ref ammo_codex_mghe);
            ammo_codex_mghe.AmmoType = ammo_mghe;
            ammo_codex_mghe.name = "ammo_mghe";

            clip_mghe = new AmmoType.AmmoClip();
            clip_mghe.Capacity = 400;
            clip_mghe.Name = "20mm APFSDS/HE Mixed Belt";
            clip_mghe.MinimalPattern = new AmmoCodexScriptable[1];
            clip_mghe.MinimalPattern[0] = ammo_codex_mghe;

            Util.Coalesce(ref clip_codex_mghe);
            clip_codex_mghe.name = "clip_mghe";
            clip_codex_mghe.ClipType = clip_mghe;

            ammo_mghe_vis = GameObject.Instantiate(SharedAssets.ammo_3bm22.VisualModel);
            ammo_mghe_vis.name = "mghe visual";
            ammo_mghe.VisualModel = ammo_mghe_vis;
            ammo_mghe.ShotVisual = SharedAssets.ammo_3bm22.ShotVisual;
            ammo_mghe.ShotVisual.transform.localScale = new Vector3(0.45f, 0.45f, 0.55f);
            ammo_mghe.VisualModel.GetComponent<AmmoStoredVisual>().AmmoType = ammo_mghe;
            ammo_mghe.VisualModel.GetComponent<AmmoStoredVisual>().AmmoScriptable = ammo_codex_mghe;
            /////////////////////////////////////////////////////////////////////////////////////////////
            ammo_mg = new AmmoType();
            Util.ShallowCopy(ammo_mg, SharedAssets.ammo_3ubr6);
            ammo_mg.Name = "20mm APFSDS-T";
            ammo_mg.ShortName = AmmoType.AmmoShortName.Coax;
            ammo_mg.UseTracer = true;
            ammo_mg.Mass = 0.35f;
            ammo_mg.Caliber = 20f;
            ammo_mg.Coeff = 0.08f;
            ammo_mg.MuzzleVelocity = 2000f;
            ammo_mg.RhaPenetration = 89f;
            ammo_mg.SpallMultiplier = 2f;
            ammo_mg.MinSpallRha = 6;
            ammo_mg.MaxSpallRha = 24;
            ammo_mg.CertainRicochetAngle = 18.3f;
            ammo_mg.VisualType = SharedAssets.ammo_3bm22.VisualType;
            ammo_mg.ImpactAudio = GHPC.Audio.ImpactAudioType.AutocannonKinetic;
            ammo_mg.ImpactEffectDescriptor = new ParticleEffectsManager.ImpactEffectDescriptor()
            {
                HasImpactEffect = true,
                EffectSize = ParticleEffectsManager.EffectSize.Autocannon,
                ImpactCategory = ParticleEffectsManager.Category.Kinetic,
                Flags = ParticleEffectsManager.ImpactModifierFlags.Small,
                MinFilterStrictness = ParticleEffectsManager.FilterStrictness.Medium,
                RicochetType = ParticleEffectsManager.RicochetType.SmallTracer
            };
            ammo_mg.ImpactDecalDescriptor = new ImpactDecalsManager.ImpactDecalDescriptor()
            {
                HasImpactDecal = true,
                DecalCategory = ImpactDecalsManager.DecalCategory.APFSDS,
                DecalType = ImpactDecalsManager.DecalType.Penetration,
                DecalImpactAngle = ImpactDecalsManager.DecalImpactAngle.High,
                Flags = ImpactDecalsManager.DecalModifierFlags.Small,
                MinFilterStrictness = ImpactDecalsManager.DecalFilterStrictness.Low,
            };
            Util.Coalesce(ref ammo_codex_mg);
            ammo_codex_mg.AmmoType = ammo_mg;
            ammo_codex_mg.name = "ammo_mg";

            clip_mg = new AmmoType.AmmoClip();
            clip_mg.Capacity = 650;
            clip_mg.Name = "20mm APFSDS-T/HEI-T";
            clip_mg.MinimalPattern = new AmmoCodexScriptable[] {
                ammo_codex_mg,
                ammo_codex_mg,
                ammo_codex_mghe,
            };
            clip_mg.MinimalPattern[0] = ammo_codex_mg;

            Util.Coalesce(ref clip_codex_mg);
            clip_codex_mg.name = "clip_mg";
            clip_codex_mg.ClipType = clip_mg;

            ammo_mg_vis = GameObject.Instantiate(SharedAssets.ammo_3bm22.VisualModel);
            ammo_mg_vis.name = "mg visual";
            ammo_mg.VisualModel = ammo_mg_vis;
            ammo_mg.ShotVisual = SharedAssets.ammo_3bm22.ShotVisual;
            ammo_mg.VisualModel.GetComponent<AmmoStoredVisual>().AmmoType = ammo_mg;
            ammo_mg.VisualModel.GetComponent<AmmoStoredVisual>().AmmoScriptable = ammo_codex_mg;
            /*****************************************************************************************************/
        }
    }
}