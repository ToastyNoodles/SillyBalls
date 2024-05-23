using Aki.Reflection.Patching;
using Comfort.Common;
using EFT;
using EFT.UI;
using System.Reflection;

namespace SillyBalls.Patches
{
    public class GameWorldPatch : ModulePatch
    {
        public static GameWorld currentGameWorldInstance { get; private set; }

        protected override MethodBase GetTargetMethod()
        {
            return typeof(GameWorld).GetMethod(nameof(GameWorld.OnGameStarted));
        }

        [PatchPrefix]
        public static void Prefix()
        {
            currentGameWorldInstance = Singleton<GameWorld>.Instance;
            if (currentGameWorldInstance == null)
                ConsoleScreen.Log("Failed to find EFT GameWorld Instance");
        }
    }
}
