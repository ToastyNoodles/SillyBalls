using BepInEx;
using BepInEx.Logging;

namespace CustomPlugin
{
    [BepInPlugin("com.noodles.customplugin", "Custom Plugin", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource logger;

        void Awake()
        {
            logger = Logger;
            Logger.LogInfo("Loaded Custom Plugin");

            new CustomPluginPatch().Enable();
        }
    }
}
