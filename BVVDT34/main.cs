using BVVDT34;
using GHPC;
using GHPC.Audio;
using GHPC.Camera;
using GHPC.Player;
using GHPC.State;
using GHPC.Utility;
using GHPC.Vehicle;
using GHPC.Weaponry;
using GHPC.Weapons;
using MelonLoader;
using ModUtil;
using NWH.VehiclePhysics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[assembly: MelonInfo(typeof(BVVDT34Mod), "BVVD T34", "1.1", "Aeiou6")]
[assembly: MelonGame("Radian Simulations LLC", "GHPC")]

namespace BVVDT34
{
    public class BVVDT34Mod : MelonMod
    {
        private ModuleManager module_manager;
        public static Vehicle[] vics;
        private GameObject game_manager;
        internal static AudioSettingsManager audio_settings_manager;
        internal static PlayerInput player_manager;
        internal static CameraManager camera_manager;
        private int valid_scene_count = 0;
        public IEnumerator OnPlayerReady(GameState _)
        {
            game_manager = GameObject.Find("_APP_GHPC_");
            audio_settings_manager = game_manager.GetComponent<AudioSettingsManager>();
            player_manager = game_manager.GetComponent<PlayerInput>();
            camera_manager = game_manager.GetComponent<CameraManager>();
            vics = GameObject.FindObjectsByType<Vehicle>(FindObjectsSortMode.None);
            module_manager.LoadAllDynamicAssets();
            yield break;
        }
        public override void OnInitializeMelon()
        {
            module_manager = new ModuleManager("BVVDT34");
            MelonPreferences_Category cfg = MelonPreferences.CreateCategory("T34Config");
            T34.Config(cfg);
            module_manager.Add("SharedAssets", new SharedAssets());
            module_manager.Add("ammo_85", new ammo_85());
            module_manager.Add("Stalinium", new Stalinium());
            module_manager.Add("T34", new T34());
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            module_manager.UnloadAllDynamicAssets();

            if (sceneName == "MainMenu2_Scene" ||
                sceneName == "MainMenu2-1_Scene" ||
                sceneName == "t64_menu")
            {
                module_manager.LoadAllStaticAssets();
                AssetUtil.ReleaseVanillaAssets();
            }
            if (Util.menu_screens.Contains(sceneName)) return;

            valid_scene_count++;
            if (valid_scene_count == 2)
            {
                StateController.RunOrDefer(GameState.PlayerReady, new GameStateEventHandler(AssetUtil.ReleaseTempVanillaAssetsDeferred), GameStatePriority.Medium);
                StateController.RunOrDefer(GameState.PlayerReady, new GameStateEventHandler(OnPlayerReady), GameStatePriority.Medium);
                T34.Init();
                valid_scene_count = 0;
            }
        }
    }
}