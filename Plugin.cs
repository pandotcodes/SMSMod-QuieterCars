using BepInEx;
using BepInEx.Configuration;
using DG.Tweening;
using HarmonyLib;
using TurnTheGameOn.SimpleTrafficSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QuieterCars
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static ConfigEntry<float> volumeMultiplier;
        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            volumeMultiplier = Config.Bind("QuieterCars", "volumeMultiplier", 0.25f, "What value to modify the volume of the car engines by.");
            Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
        }
    }
    public static class QuieterCarsPatch
    {
        [HarmonyPatch(typeof(CarSpawnListener), "OnEnable")]
        public static class CarSpawnListener_OnEnable_Patch
        {
            public static void Prefix(CarSpawnListener __instance)
            {
                __instance.gameObject.GetComponent<AudioSource>().volume = 0.1f * Plugin.volumeMultiplier.Value;
            }
        }
    }
}
