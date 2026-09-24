using GHPC.Camera;
using GHPC.Vehicle;
using GHPC.Weaponry;
using MelonLoader;
using ModUtil;
using NWH.VehiclePhysics;
using Reticle;
using System.Linq;
using TMPro;
using UnityEngine;

namespace BVVDT34
{
    internal class SharedAssets : Module
    {
        internal static AmmoType ammo_3bm32;
        internal static AmmoType ammo_3bm22;
        internal static AmmoType ammo_3bk18m;
        internal static AmmoType ammo_3OF26;
        internal static AmmoType ammo_kobra;
        internal static AmmoClipCodexScriptable clip_codex_3bm22;
        internal static AmmoClipCodexScriptable clip_codex_3bm32;

        internal static AmmoClipCodexScriptable clip_codex_3OF26;
        internal static TMP_FontAsset tpd_etch_sdf;
        internal static VehicleController abrams_vic_controller;
        internal static Material green_flir_mat;
        internal static GameObject flir_post_green;
        internal static GameObject crt_shock_go;
        internal static GameObject m2_bradley_canvas;
        internal static AmmoClipCodexScriptable clip_codex_3bk18m;
        internal static AmmoClipCodexScriptable clip_codex_3ubr6;
        internal static AmmoType ammo_3ubr6;

        public override void LoadStaticAssets()
        {
            AmmoClipCodexScriptable[] clip_codex_scriptables = Resources.FindObjectsOfTypeAll<AmmoClipCodexScriptable>();
            AmmoCodexScriptable[] codex_scriptables = Resources.FindObjectsOfTypeAll<AmmoCodexScriptable>();
            AssetUtil.LoadVanillaVehicle("BMP2_SA");
            Vehicle m1ip = AssetUtil.LoadVanillaVehicle("M1IP");
            Transform m1ip_flir = m1ip.transform.Find("Turret Scripts/GPS/FLIR");
            clip_codex_3bk18m = clip_codex_scriptables.Where(o => o.name == "clip_3BK18M").FirstOrDefault();
            clip_codex_3bm22 = clip_codex_scriptables.Where(o => o.name == "clip_3BM22").FirstOrDefault();
            clip_codex_3bm32 = clip_codex_scriptables.Where(o => o.name == "clip_3BM32").FirstOrDefault();
            ammo_3bm32 = clip_codex_3bm32.ClipType.MinimalPattern[0].AmmoType;
            ammo_3bm22 = clip_codex_3bm22.ClipType.MinimalPattern[0].AmmoType;
            ammo_3bk18m = clip_codex_3bk18m.ClipType.MinimalPattern[0].AmmoType;
            ammo_kobra = codex_scriptables.Where(o => o.name == "ammo_9M112M").FirstOrDefault().AmmoType;
            MelonLogger.Msg($"ammo_kobra = {ammo_kobra}");
            ammo_3OF26 = codex_scriptables.Where(o => o.name == "ammo_3OF26").FirstOrDefault().AmmoType;
            MelonLogger.Msg($"3of26 = {ammo_3OF26}");
            ammo_3ubr6 = codex_scriptables.Where(o => o.name == "ammo_20mm_DM63").FirstOrDefault().AmmoType;
            MelonLogger.Msg($"3UBR6 ClipType = {ammo_3ubr6}");
            crt_shock_go = m1ip_flir.Find("Scanline FOV change").gameObject;
            flir_post_green = m1ip_flir.Find("FLIR Post Processing - Green").gameObject;
            green_flir_mat = m1ip_flir.GetComponent<CameraSlot>().FLIRBlitMaterialOverride;
            Vehicle m2_bradley = AssetUtil.LoadVanillaVehicle("M2BRADLEY");
            m2_bradley_canvas = m2_bradley.transform.Find("FCS and sights/GPS Optic/M2 Bradley GPS canvas").gameObject;
            Vehicle t55a = AssetUtil.LoadVanillaVehicle("T55A");
            t55a.transform.Find("Gun Scripts/Sights (and FCS)/NVS/Reticle Mesh").GetComponent<ReticleMesh>().Load();
            t55a.WeaponsManager.Weapons[0].FCS.AuthoritativeOptic.reticleMesh.Load();
            tpd_etch_sdf = Resources.FindObjectsOfTypeAll<TMP_FontAsset>().Where(o => o.name == "TPD_Etch SDF").FirstOrDefault();

        }
        public override void LoadDynamicAssets()
        {
            Vehicle m1ip = AssetUtil.LoadVanillaVehicle("M1IP");
            abrams_vic_controller = m1ip.GetComponent<VehicleController>();
        }
    }
}
