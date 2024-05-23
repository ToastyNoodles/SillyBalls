using Aki.Reflection.Patching;
using EFT;
using System.Reflection;
using UnityEngine;

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
            Rigidbody sillyRigidbody;
            MeshRenderer sillyMeshRenderer;
            float spawnOffset = 2.0f;

            GameObject sillyBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sillyBall.AddComponent<SillyBallComponent>();
            sillyBall.transform.position = damageInfo.HitPoint + (Vector3.up * spawnOffset);
            sillyBall.transform.localScale = Vector3.one * Plugin.sillyballSize.Value;

            sillyRigidbody = sillyBall.AddComponent<Rigidbody>();
            sillyRigidbody.AddForce(Random.insideUnitSphere * Plugin.sillyballSpawnForce.Value, ForceMode.Impulse);

            sillyMeshRenderer = sillyBall.AddComponent<MeshRenderer>();
            sillyMeshRenderer.material.color = new Color(Random.value, Random.value, Random.value);
            sillyMeshRenderer.material.SetFloat("_Metallic", Random.value);
            sillyMeshRenderer.material.SetFloat("_Glossiness", Random.value);
        }
    }
}
