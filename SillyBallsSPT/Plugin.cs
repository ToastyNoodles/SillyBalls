using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using Comfort.Common;
using EFT;

namespace SillyBalls
{
    [BepInPlugin("com.noodles.sillyballs", "SillyBalls", "1.1")]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource logger;
        
        public static ConfigEntry<bool> EnableOnHit { get; set; }
        public static ConfigEntry<bool> EnableOnKill { get; set; }
        public static ConfigEntry<bool> EnablePhysics { get; set; }
        public static ConfigEntry<int> SillyBallsAmount { get; set; }
        public static ConfigEntry<float> SillyBallsForce { get; set; }
        public static ConfigEntry<float> SillyBallsMinSize { get; set; }
        public static ConfigEntry<float> SillyBallsMaxSize { get; set; }
        public static ConfigEntry<float> SillyBallsShrinkSpeed { get; set; }
        public static ConfigEntry<float> SillyBallsBounciness { get; set; }

        void Awake()
        {
            EnablePhysics = Config.Bind("1. Toggles", "Enable Constant Physics", true, "likely impacts performance");
            EnableOnHit = Config.Bind("1. Toggles", "Spawn On Hit", true);
            EnableOnKill = Config.Bind("1. Toggles", "Spawn On Kill", true);
            SillyBallsAmount = Config.Bind("2. SillyBalls", "Spawn Amount", 2);
            SillyBallsForce = Config.Bind("2. SillyBalls", "Spawn Force", 1.0f);
            SillyBallsMinSize = Config.Bind("2. SillyBalls", "Minimum Size (Despawn Size)", 0.2f);
            SillyBallsMaxSize = Config.Bind("2. SillyBalls", "Maximum Size (Spawn Size)", 0.8f);
            SillyBallsShrinkSpeed = Config.Bind("2. SillyBalls", "Shrink Multiplier", 0.15f);
            SillyBallsBounciness = Config.Bind("2. SillyBalls", "Bounciness (0 - 1)", 0.5f);

            new ApplyDamageInfoPatch().Enable();
            new OnEnemyKillPatch().Enable();

            Logger.LogInfo("Loaded SillyBalls");
        }

        void Update()
        {
            if (EnablePhysics.Value && Singleton<GameWorld>.Instance != null)
            {
                EFTPhysicsClass.GClass650.UpdateEnabled = true;
            }
        }
    }
}
