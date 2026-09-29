using GHPC.Effects;
using GHPC.Weaponry;
using GHPC.Weapons;
using MelonLoader;
using ModUtil;
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

        public static AmmoClipCodexScriptable clip_codex_mg;
        public static AmmoType.AmmoClip clip_mg;
        public static AmmoCodexScriptable ammo_codex_mg;
        public static AmmoType ammo_mg;
        public static GameObject ammo_mg_vis = null;


        public override void UnloadDynamicAssets()
        {
            GameObject.DestroyImmediate(ammo_ap_vis);
            GameObject.DestroyImmediate(ammo_heat_vis);
            GameObject.DestroyImmediate(ammo_mininuke_vis);
            GameObject.DestroyImmediate(ammo_missile_vis);
            GameObject.DestroyImmediate(ammo_mg_vis);
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
            ammo_heat.TerrainImpactEffect = SharedAssets.ammo_3OF26.TerrainImpactEffect;
            ammo_heat.Mass = 47.5f;
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
            ammo_mininuke.TntEquivalentKg = 39.5f;
            ammo_mininuke.CachedIndex = -1;
            ammo_mininuke.RhaPenetration = 12f;
            ammo_mininuke.ArmingDistance = 40;
            ammo_mininuke.ImpactFuseTime = 1f;
            ammo_mininuke.RhaToFuse = 12f;
            ammo_mininuke.MicroFragScaling = 3f;
            ammo_mininuke.DetonateEffect = SharedAssets.ammo_3OF26.DetonateEffect;
            ammo_mininuke.TerrainImpactEffect = SharedAssets.ammo_3OF26.TerrainImpactEffect;
            ammo_mininuke.VisualModel = SharedAssets.ammo_3bk18m.VisualModel;
            ammo_mininuke.Coeff = 1.5f;
            ammo_mininuke.Mass = 133f;
            ammo_mininuke.MuzzleVelocity = 550f;
            ammo_mininuke.SpallMultiplier = 1f;
            ammo_mininuke.MaxSpallRha = 30f;
            ammo_mininuke.MinSpallRha = 10f;
            ammo_mininuke.DetonateSpallCount = 135;
            ammo_mininuke.NoisePowerX = 24;
            ammo_mininuke.NoisePowerY = 26;
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

            ammo_mininuke_vis = GameObject.Instantiate(SharedAssets.ammo_3bk18m.VisualModel);
            ammo_mininuke_vis.name = "mininuke visual";
            ammo_mininuke.VisualModel = ammo_mininuke_vis;
            ammo_mininuke.VisualModel.GetComponent<AmmoStoredVisual>().AmmoType = ammo_mininuke;
            ammo_mininuke.VisualModel.GetComponent<AmmoStoredVisual>().AmmoScriptable = ammo_codex_mininuke;
            /////////////////////////////////////////////////////////////////////////////////////////////
            ammo_missile = new AmmoType();
            Util.ShallowCopy(ammo_missile, SharedAssets.ammo_kobra);
            ammo_missile.Name = "9M94ML Velikan";
            ammo_missile.Caliber = 85;
            ammo_missile.SpiralAngularRate = 10800f;
            ammo_missile.SpiralPower = 150f;
            ammo_missile.MuzzleVelocity = 750f;
            ammo_missile.RhaPenetration = 20000f;
            ammo_missile.TntEquivalentKg = 10.5f;
            ammo_missile.Mass = 43.5f;
            ammo_missile.Guidance = AmmoType.GuidanceType.Laser;
            ammo_missile.Flight = AmmoType.FlightPattern.Direct;
            ammo_missile.Category = AmmoType.AmmoCategory.ShapedCharge;
            ammo_missile.Tandem = true;
            ammo_missile.IgnoreSlat = true;
            ammo_missile.TurnSpeed = 12f;
            ammo_missile.GuidanceLockoutTime = 0f;
            ammo_missile.Coeff = 0.5f;
            ammo_missile.AimPointMarch = -2f;
            ammo_missile.RangedFuseTime = 35f;
            ammo_missile.NoisePowerX = 0f;
            ammo_missile.NoisePowerY = 0f;
            ammo_missile.NoiseTimeScale = 1f;
            ammo_missile.SpallMultiplier = 10;
            ammo_missile.MinSpallRha = 20f;
            ammo_missile.MaxSpallRha = 100f;
            ammo_missile.DetonateSpallCount = 50;
            ammo_missile.RhaToFuse = 12f;
            ammo_missile.ImpactFuseTime = 0.05f;
            ammo_missile.TerrainImpactEffect = SharedAssets.ammo_3OF26.TerrainImpactEffect;
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


            /////////////////////////////////////////////////////////////////////////////////////////////
            ammo_mg = new AmmoType();
            Util.ShallowCopy(ammo_mg, SharedAssets.ammo_3ubr6);
            ammo_mg.Name = "20mm APFSDS-T";
            ammo_mg.ShortName = AmmoType.AmmoShortName.Coax;
            ammo_mg.UseTracer = true;
            ammo_mg.Mass = 0.25f;
            ammo_mg.Caliber = 20f;
            ammo_mg.Coeff = 0.0f;
            ammo_mg.MinSpallRha = 10f;
            ammo_mg.MaxSpallRha = 100f;
            ammo_mg.MuzzleVelocity = 3200f;
            ammo_mg.RhaPenetration = 89f;
            ammo_mg.SpallMultiplier = 2f;
            ammo_mg.MinSpallRha = 6;
            ammo_mg.MaxSpallRha = 24;
            ammo_mg.CertainRicochetAngle = 15.3f;
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
            Util.Coalesce(ref ammo_codex_mg);
            ammo_codex_mg.AmmoType = ammo_mg;
            ammo_codex_mg.name = "ammo_mg";

            clip_mg = new AmmoType.AmmoClip();
            clip_mg.Capacity = 400;
            clip_mg.Name = "20mm APFSDS-T";
            clip_mg.MinimalPattern = new AmmoCodexScriptable[1];
            clip_mg.MinimalPattern[0] = ammo_codex_mg;

            Util.Coalesce(ref clip_codex_mg);
            clip_codex_mg.name = "clip_mg";
            clip_codex_mg.ClipType = clip_mg;

            ammo_mg_vis = GameObject.Instantiate(SharedAssets.ammo_3bm32.VisualModel);
            ammo_mg_vis.name = "mg visual";
            ammo_mg.VisualModel = ammo_mg_vis;
            ammo_mg.ShotVisual = SharedAssets.ammo_3bm32.ShotVisual;    
            ammo_mg.VisualModel.GetComponent<AmmoStoredVisual>().AmmoType = ammo_mg;
            ammo_mg.VisualModel.GetComponent<AmmoStoredVisual>().AmmoScriptable = ammo_codex_mg;
            /*****************************************************************************************************/
        }
    }
}
