using Aki.Reflection.Patching;
using EFT;
using EFT.UI;
using System.Reflection;

namespace SillyBalls.Patches
{
    public class ApplyDamageInfoPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(Player).GetMethod(nameof(Player.ApplyDamageInfo));
        }

        [PatchPrefix]
        public static void Prefix(DamageInfo damageInfo, EBodyPart bodyPartType, EBodyPartColliderType colliderType, float absorbed)
        {
            ConsoleScreen.Log($"ApplyDamageInfo: {damageInfo.Player.Nickname}");
            //I think this is where I check if they died and if they did spawn some sillyballs
        }
    }
}
