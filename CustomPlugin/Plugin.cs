using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;

namespace CustomPlugin
{
    [BepInPlugin("com.noodles.customplugin", "Custom Plugin", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource logger;
        public static ConfigEntry<float> scaleDownSpeed { get; set; }

        void Awake()
        {
            logger = Logger;
            Logger.LogInfo("Loaded Custom Plugin");

            scaleDownSpeed = Config.Bind<float>("Settings", "Despawn Speed", 5.0f);

            new CustomPluginPatch().Enable();
        }
    }
}
