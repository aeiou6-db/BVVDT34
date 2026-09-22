using BVVDT34;
using GHPC;
using GHPC.Camera;
using GHPC.Effects;
using GHPC.Effects.Voices;
using GHPC.Equipment.Optics;
using GHPC.State;
using GHPC.Thermals;
using GHPC.UI.Tips;
using GHPC.Utility;
using GHPC.Vehicle;
using GHPC.Weaponry;
using GHPC.Weapons;
using HarmonyLib;
using MelonLoader;
using MelonLoader.Utils;
using ModUtil;
using NWH.VehiclePhysics;
using Reticle;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


namespace BVVDT34
{
    public class T34 : ModUtil.Module
    {
        static MelonPreferences_Entry<bool> smoothbore_cannon;
        static MelonPreferences_Entry<bool> super_armor;
        static MelonPreferences_Entry<bool> super_engine;
        static MelonPreferences_Entry<bool> use_lrf;
        static WeaponSystemCodexScriptable gun_85mmSmooth;
        static WeaponSystemCodexScriptable mg_super;

        public static void Config(MelonPreferences_Category cfg)
        {
            smoothbore_cannon = cfg.CreateEntry<bool>("Use Advanced Ammo for machine gun and cannon", true);

            super_armor = cfg.CreateEntry<bool>("Use Advanced armor blessed by stalin himself", true);

            super_engine = cfg.CreateEntry<bool>("Use Super Engine", true);

            use_lrf = cfg.CreateEntry<bool>("Fire Control System Upgrade, also adds a stabilizezr", true);
        }
        private static void HandleConversion(Vehicle vic)
        {
            if (vic == null) return;
            GameObject vic_go = vic.gameObject;

            if (vic._friendlyName != "T-34-85M") return;
            MelonLogger.Msg("T-34-85 FOUND!");
            vic._friendlyName = "Stalin's Own T-34-85";
            WeaponsManager weapons_manager = vic.GetComponent<WeaponsManager>();
            WeaponSystemInfo main_gun_info = weapons_manager.Weapons[0];
            WeaponSystem main_gun = main_gun_info.Weapon;
            WeaponSystemInfo machine_gun_info = weapons_manager.Weapons[1];
            WeaponSystem machine_gun = machine_gun_info.Weapon;
            FireControlSystem fcs = vic.GetComponentInChildren<FireControlSystem>();
            UsableOptic day_optic = Util.GetDayOptic(main_gun.FCS);
            bool cfg_smoothbore = smoothbore_cannon.Value;
            bool cfg_superengine = super_engine.Value; 
            bool cfg_superarmor = super_armor.Value; ; 
            bool cfg_lrf = use_lrf.Value; 
            MelonLogger.Msg("INITIAL BOOLS LOADED!");
            if (cfg_smoothbore)
            {
                MelonLogger.Msg("Starting Ammo!");
                GHPC.Weapons.AmmoRack cannonrack = main_gun.Feed.ReadyRack;
                GHPC.Weapons.AmmoRack mgrack = machine_gun.Feed.ReadyRack;
                AmmoType.AmmoClip ap = ammo_85.clip_ap;
                AmmoType.AmmoClip heat = ammo_85.clip_heat;
                AmmoType.AmmoClip nuke = ammo_85.clip_mininuke;
                AmmoType.AmmoClip atgm = ammo_85.clip_missile;
                AmmoType.AmmoClip mg = ammo_85.clip_mg;
                main_gun.CodexEntry = gun_85mmSmooth;
                machine_gun.CodexEntry = mg_super;
                main_gun.WeaponSound.SingleShotEventPaths[0] = "event:/Weapons/canon_125mm-2A46";
                Array.Resize(ref cannonrack._clipTypes, 4);
                cannonrack._clipTypes[0] = ap;
                cannonrack._clipTypes[1] = heat;
                cannonrack._clipTypes[2] = nuke;
                cannonrack._clipTypes[3] = atgm;
                mgrack._clipTypes[0] = mg;
                cannonrack.ClipCapacity = 65;
                MelonLogger.Msg("Set Ammo Clips!");
                // 20 AP, 20 HEAT, 10 Nukes, 6 ATGMs,
                cannonrack.StoredClips = new List<AmmoType.AmmoClip>()
                {
                    ap,ap,ap,ap,ap,ap,ap,ap,ap,ap,ap,ap,ap,ap,ap,ap,ap,ap,ap,ap,ap,
                    heat,heat,heat,heat,heat,heat,heat,heat,heat,heat,heat,heat,heat,heat,heat,heat,heat,heat,heat,heat,heat,
                    nuke,nuke,nuke,nuke,nuke,nuke,nuke,nuke,nuke,nuke,nuke,nuke,nuke,nuke,nuke,
                    atgm,atgm,atgm,atgm,atgm,atgm,
                };
                MelonLogger.Msg($"StoredClips assigned! Count = {cannonrack.StoredClips.Count}");
                mgrack.ClipCapacity = 18;
                mgrack.StoredClips = new List<AmmoType.AmmoClip>()
                {
                    mg,mg,mg,mg,mg,mg,mg,mg,mg,mg,mg,mg,mg,mg,mg,mg,mg,mg,

                };
                MelonLogger.Msg($"StoredClips assigned! Count = {mgrack.StoredClips.Count}");
                GameObject guidance_computer_obj = GameObject.Instantiate(new GameObject("guidance computer"), fcs.transform.parent);
                guidance_computer_obj.transform.localPosition = fcs.transform.localPosition + new Vector3(0, -1, 0f);
                guidance_computer_obj.transform.SetParent(day_optic.transform.parent, true);
                MissileGuidanceUnit computer = guidance_computer_obj.AddComponent<MissileGuidanceUnit>();
                computer.AimElement = guidance_computer_obj.transform;
                main_gun.GuidanceUnit = computer;

                main_gun.Feed.ReloadDuringMissileTracking = false;
                main_gun.Feed._missileGuidance = computer;
                main_gun.FireWhileGuidingMissile = false;
                MelonLogger.Msg("Ammo Stores done!");
                main_gun.Impulse = 2000;
                machine_gun.Impulse = 35;
                machine_gun.Feed._totalCycleTime = 0.0005f;
                machine_gun.BaseDeviationAngle = 0.065f / 1.2f;
                cannonrack._retrievalDelaySeconds = 1f;
                cannonrack._storageDelaySeconds = 2f;
                MelonLogger.Msg("Readjusted Main Gun!");
                main_gun.Feed.AmmoTypeInBreech = null;
                machine_gun.Feed.AmmoTypeInBreech = null;
                main_gun.Feed.Start();
                machine_gun.Feed.Start();
            }
            if (cfg_superengine)
            {
                MelonLogger.Msg("Starting Engine Conversion!");
                VehicleController this_vic_controller = vic_go.GetComponent<VehicleController>();
                NwhChassis chassis = vic_go.GetComponent<NwhChassis>();

                Util.ShallowCopy(this_vic_controller.engine, SharedAssets.abrams_vic_controller.engine);
                Util.ShallowCopy(this_vic_controller.transmission, SharedAssets.abrams_vic_controller.transmission);

                this_vic_controller.engine.vc = vic_go.GetComponent<VehicleController>();
                this_vic_controller.transmission.vc = vic_go.GetComponent<VehicleController>();
                this_vic_controller.engine.Initialize(this_vic_controller);
                this_vic_controller.engine.Start();
                this_vic_controller.transmission.Initialize(this_vic_controller);

                chassis._maxForwardSpeed = 25f;
                chassis._maxReverseSpeed = 25f;
                chassis._originalEnginePower = 5000f;
                chassis.SteerAccelerationMultiplier = 4f;
                MelonLogger.Msg("Engine Conversion Success!");
            }
            if (cfg_superarmor)
            {
                MelonLogger.Msg("locating armors");
                Transform turret = vic.transform.Find("T34_rig/T34/HULL/TURRET");
                GameObject turret_cast = turret.GetComponent<LateFollowTarget>()._lateFollowers[0].transform.Find("T34_Turret_armour/turret casting").gameObject;
                GameObject glacis = vic.GetComponent<LateFollowTarget>()._lateFollowers[0].transform.Find("T34_Hull_armour/front glacis").gameObject;
                GameObject hullside = vic.GetComponent<LateFollowTarget>()._lateFollowers[0].transform.Find("T34_Hull_armour/45mm side plate").gameObject;
                GameObject hatch = vic.GetComponent<LateFollowTarget>()._lateFollowers[0].transform.Find("T34_Hull_armour/driver's hatch").gameObject;
                MelonLogger.Msg("Armors Found! Replacing..");
                turret_cast.GetComponent<VariableArmor>()._armorType = Stalinium.stalinium_turret_codex;
                glacis.GetComponent<VariableArmor>()._armorType = Stalinium.stalinium_hull_codex;
                hatch.GetComponent<VariableArmor>()._armorType = Stalinium.stalinium_driver_codex;
                hullside.GetComponent<UniformArmor>().PrimaryHeatRha = 1000f;
                hullside.GetComponent<UniformArmor>().PrimarySabotRha = 1000f;
                hullside.GetComponent<UniformArmor>().SecondarySabotRha = 1000f;
                hullside.GetComponent<UniformArmor>().SecondaryHeatRha = 1000f;
                hullside.GetComponent<UniformArmor>()._armorType = Stalinium.stalinium_hull_codex;
                MelonLogger.Msg("Success!");

            }


            if (cfg_lrf)
            {
                AimablePlatform[] aimables = vic.AimablePlatforms;
                FieldInfo stab_mode = typeof(AimablePlatform).GetField("_stabMode", BindingFlags.Instance | BindingFlags.NonPublic);
                FieldInfo stab_active = typeof(AimablePlatform).GetField("_stabActive", BindingFlags.Instance | BindingFlags.NonPublic);
                PropertyInfo stab_FCS_active = typeof(FireControlSystem).GetProperty("StabsActive", BindingFlags.Instance | BindingFlags.Public);

                stab_FCS_active.SetValue(fcs, true);
                fcs.CurrentStabMode = StabilizationMode.Vector;
                aimables[0].SpeedPowered = 60f;
                aimables[1].SpeedPowered = 60f;
                aimables[1]._weaponAuthoritativeAimSpeedHoriz = 60f;
                aimables[0]._weaponAuthoritativeAimSpeedHoriz = 60f;
                aimables[1].Stabilized = true;
                stab_active.SetValue(aimables[1], true);
                stab_mode.SetValue(aimables[1], StabilizationMode.Vector);

                aimables[0].Stabilized = true;
                stab_active.SetValue(aimables[0], true);
                stab_mode.SetValue(aimables[0], StabilizationMode.Vector);

                day_optic.Alignment = OpticAlignment.Boresight;
                day_optic.ForceHorizontalReticleAlign = true;
                day_optic.RotateAzimuth = true;
                day_optic.slot.VibrationBlurScale = 0.01f;
                day_optic.slot.VibrationShakeMultiplier = 0f;
                day_optic.slot.DefaultFov = 10f;
                day_optic.slot.OtherFovs = new float[] { 8f, 6f, 4f, 2f, 1f };
                main_gun.FCS.MaxLaserRange = 4000f;
                main_gun.FCS._currentRange = 0f;
                main_gun.FCS.RegisteredRangeLimits = new Vector2(0, 4000);
                main_gun.FCS._autoDumpViaPalmSwitches = true;
                main_gun.FCS.WeaponAuthoritative = false;
                main_gun.FCS.InertialCompensation = false;
                main_gun.FCS.LaserAim = LaserAimMode.ImpactPoint;
                main_gun.FCS._fixParallaxForVectorMode = true;
            }
        }
        public override void LoadStaticAssets()
        {
            gun_85mmSmooth = ScriptableObject.CreateInstance<WeaponSystemCodexScriptable>();
            gun_85mmSmooth.name = "gun_85Smooth";
            gun_85mmSmooth.CaliberMm = 85f;
            gun_85mmSmooth.FriendlyName = "85mm Smoothbore Gun D-999TM";
            gun_85mmSmooth.Type = WeaponSystemCodexScriptable.WeaponType.LargeCannon;

            mg_super = ScriptableObject.CreateInstance<WeaponSystemCodexScriptable>();
            mg_super.name = "gun_supermg";
            mg_super.CaliberMm = 7.62f;
            mg_super.FriendlyName = "GShG-7.62M";
            mg_super.Type = WeaponSystemCodexScriptable.WeaponType.SmallArms;
        }



        public static IEnumerator Convert(GameState _)
        {
            foreach (Vehicle vic in BVVDT34Mod.vics)
            {
                HandleConversion(vic);
            }

            yield break;
        }

        public static void Init()
        {

            StateController.RunOrDefer(GameState.PlayerReady, new GameStateEventHandler(Convert), GameStatePriority.Medium);
        }
    }
}
