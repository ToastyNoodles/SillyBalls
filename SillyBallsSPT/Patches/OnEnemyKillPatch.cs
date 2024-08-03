using SPT.Reflection.Patching;
using System.Reflection;
using EFT;

namespace SillyBalls
{
    public class OnEnemyKillPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(LocalPlayer).GetMethod(nameof(LocalPlayer.OnBeenKilledByAggressor));
        }

        [PatchPrefix]
        public static void Prefix(IPlayer aggressor, DamageInfo damageInfo, EBodyPart bodyPart, EDamageType lethalDamageType)
        {
            if (Plugin.EnableOnKill.Value)
            {
                for (int i = 0; i < Plugin.SillyBallsAmount.Value; i++)
                {
                    SillyballSpawner.SpawnSillyBall(damageInfo);
                }
            }
        }
    }
}
