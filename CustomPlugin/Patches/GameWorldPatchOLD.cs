using Aki.Reflection.Patching;
using EFT;
using System.Reflection;
using UnityEngine;

namespace SillyBalls
{
    public class GameWorldPatchOLD : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(GameWorld).GetMethod(nameof(GameWorld.OnGameStarted));
        }

        [PatchPrefix]
        public static void Prefix()
        {
            GameObject pluginHook = new GameObject("PluginHook");
            pluginHook.AddComponent<CustomPluginComponent>();
            Object.DontDestroyOnLoad(pluginHook);
        }
    }
}
