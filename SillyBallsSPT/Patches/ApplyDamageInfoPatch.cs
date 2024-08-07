﻿using SPT.Reflection.Patching;
using EFT;
using System.Reflection;

namespace SillyBalls
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
            if (Plugin.EnableOnHit.Value)
            {
                for (int i = 0; i < Plugin.SillyBallsAmount.Value; i++)
                {
                    SillyballSpawner.SpawnSillyBall(damageInfo);
                }
            }
        }
    }
}
