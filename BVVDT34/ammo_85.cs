using GHPC.Effects;
using GHPC.Weaponry;
using GHPC.Weapons;
using MelonLoader;
using ModUtil;
using System.Collections.Generic;
using System.Linq;
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
            ammo_ap.Guidance = AmmoType.GuidanceType.Unguided;
            ammo_ap.Caliber = 85;
            ammo_ap.Coeff = 0f;
            ammo_ap.TntEquivalentKg = 5f;
            ammo_ap.CertainRicochetAngle = 0f;
            ammo_ap.ArmingDistance = 0f;
            ammo_ap.RhaToFuse = 25f;
            ammo_ap.ImpactFuseTime = 0.002f;
            ammo_ap.RhaPenetration = 3000f;
            ammo_ap.Mass = 9.3f;
            ammo_ap.MuzzleVelocity = 2000f;
            ammo_ap.SpallMultiplier = 5f;
            ammo_ap.DetonateSpallCount = 100;
            ammo_ap.SphericalSpall = true;
            ammo_ap.IgnoreSlat = true;
            ammo_ap.MaxSpallRha = 100f;
            ammo_ap.MinSpallRha = 10f;
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
            ammo_heat.Category = AmmoType.AmmoCategory.ShapedCharge;
            ammo_heat.Guidance = AmmoType.GuidanceType.Unguided;
            ammo_heat.Caliber = 85;
            ammo_heat.Coeff = 0f;
            ammo_heat.TntEquivalentKg = 12f;
            ammo_heat.RhaToFuse = 10f;
            ammo_heat.ImpactFuseTime = 0.0008f;
            ammo_heat.RhaPenetration = 1500f;
            ammo_heat.Mass = 15.43f;
            ammo_heat.MuzzleVelocity = 3000f;
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
            Util.ShallowCopy(ammo_mininuke, SharedAssets.ammo_3bk18m);
            ammo_mininuke.Name = "3BV85 MicroNuke";
            ammo_mininuke.Category = AmmoType.AmmoCategory.Explosive;
            ammo_mininuke.ShortName = AmmoType.AmmoShortName.He;
            ammo_mininuke.Guidance = AmmoType.GuidanceType.Unguided;
            ammo_mininuke.Caliber = 85;
            ammo_mininuke.TntEquivalentKg = 1000f;
            ammo_mininuke.RhaPenetration = 615f;
            ammo_mininuke.ImpactFuseTime = 0.0001f;
            ammo_mininuke.RhaToFuse = 5f;
            ammo_mininuke.Coeff = 0f;
            ammo_mininuke.Mass = 4.85f;
            ammo_mininuke.MuzzleVelocity = 3000f;
            ammo_mininuke.SpallMultiplier = 100f;
            ammo_mininuke.MaxSpallRha = 540f;
            ammo_mininuke.MinSpallRha = 240f;
            ammo_mininuke.ImpactEffectDescriptor = new ParticleEffectsManager.ImpactEffectDescriptor()
            {
                HasImpactEffect = true,
                EffectSize = ParticleEffectsManager.EffectSize.Artillery,
                ImpactCategory = ParticleEffectsManager.Category.HighExplosive,
                Flags = ParticleEffectsManager.ImpactModifierFlags.Huge,
                MinFilterStrictness = ParticleEffectsManager.FilterStrictness.Medium,
                RicochetType = ParticleEffectsManager.RicochetType.LargeTracer
            };

            Util.Coalesce(ref ammo_codex_mininuke);
            ammo_codex_mininuke.AmmoType = ammo_mininuke;
            ammo_codex_mininuke.name = "ammo_mininuke";

            clip_mininuke = new AmmoType.AmmoClip();
            clip_mininuke.Capacity = 1;
            clip_mininuke.Name = "3BV85 MicroNuke";
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
            ammo_missile.Category = AmmoType.AmmoCategory.ShapedCharge;
            ammo_missile.ShortName = AmmoType.AmmoShortName.Missile;
            ammo_missile.SpiralAngularRate = 65f;
            ammo_missile.SpiralPower = 12f;
            ammo_missile.Guidance = AmmoType.GuidanceType.Laser;
            ammo_missile.Flight = AmmoType.FlightPattern.Hump;
            ammo_missile.ClimbAngle = 18f;
            ammo_missile.DiveAngle = 45f;
            ammo_missile.CachedIndex = -1;
            ammo_missile.LoiterEndDistance = 120f;
            ammo_missile.LoiterAltitude = 10f;
            ammo_missile.GuidanceLockoutTime = 60f;
            ammo_missile.GuidanceNoLockoutRange = 99999999999999f;
            ammo_missile.GuidanceNoLoiterRange = 0f;
            ammo_missile.Caliber = 85;
            ammo_missile.TntEquivalentKg = 25f;
            ammo_missile.RhaPenetration = 9999899f;
            ammo_missile.ImpactFuseTime = 0.0001f;
            ammo_missile.RhaToFuse = 5f;
            ammo_missile.AimPointMarch = -1f;
            ammo_missile.Coeff = 0f;
            ammo_missile.Mass = 4.85f;
            ammo_missile.MuzzleVelocity = 600f;
            ammo_missile.MaximumRange = 10000f;
            ammo_missile.SpallMultiplier = 100f;
            ammo_missile.MaxSpallRha = 540f;
            ammo_missile.MinSpallRha = 240f;
            ammo_missile.TurnSpeed = 3f;
            ammo_missile.ImpactEffectDescriptor = new ParticleEffectsManager.ImpactEffectDescriptor()
            {
                HasImpactEffect = true,
                EffectSize = ParticleEffectsManager.EffectSize.Artillery,
                ImpactCategory = ParticleEffectsManager.Category.HighExplosive,
                Flags = ParticleEffectsManager.ImpactModifierFlags.Huge,
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
            Util.ShallowCopy(ammo_mg, SharedAssets.ammo_3bm32);
            ammo_mg.Name = "7.62mm SAPHE";
            ammo_mg.ShortName = AmmoType.AmmoShortName.Mg;
            ammo_mg.Mass = 0.008f;
            ammo_mg.Caliber = 7.62f;
            ammo_mg.Coeff = 0.0f;
            ammo_mg.MinSpallRha = 10f;
            ammo_mg.MaxSpallRha = 100f;
            ammo_mg.MuzzleVelocity = 2300f;
            ammo_mg.TntEquivalentKg = 0.05f;
            ammo_mg.RhaToFuse = 6f;
            ammo_mg.ImpactFuseTime = 0.0016f;
            ammo_mg.RhaPenetration = 63f;
            ammo_mg.VisualType = GHPC.Weapons.LiveRoundMarshaller.LiveRoundVisualType.Custom;

            Util.Coalesce(ref ammo_codex_mg);
            ammo_codex_mg.AmmoType = ammo_mg;
            ammo_codex_mg.name = "ammo_mg";

            clip_mg = new AmmoType.AmmoClip();
            clip_mg.Capacity = 3000;
            clip_mg.Name = "7.62mm SAPHE";
            clip_mg.MinimalPattern = new AmmoCodexScriptable[1];
            clip_mg.MinimalPattern[0] = ammo_codex_mg;

            Util.Coalesce(ref clip_codex_mg);
            clip_codex_mg.name = "clip_mg";
            clip_codex_mg.ClipType = clip_mg;

            /*****************************************************************************************************/

        }
    }
}
