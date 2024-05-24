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
            for (int i = 0; i < Plugin.sillyballSpawnCount.Value; i++)
            {
                GameObject sillyBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sillyBall.AddComponent<SillyBallComponent>();
                sillyBall.transform.position = damageInfo.HitPoint + (Vector3.up * 2.0f);
                float randomScale = Random.Range(Plugin.sillyballMinSpawnSize.Value, Plugin.sillyballMaxSpawnSize.Value);
                sillyBall.transform.localScale = Vector3.one * randomScale;

                sillyBall.AddComponent<Rigidbody>();
                Rigidbody sillyBallRigidbody = sillyBall.GetComponent<Rigidbody>();
                sillyBallRigidbody.AddForce(Random.insideUnitSphere + (Vector3.up * Plugin.sillyballSpawnForce.Value), ForceMode.Impulse);
                sillyBallRigidbody.mass = 0.01f;

                MeshRenderer sillyBallMeshRenderer = sillyBall.GetComponent<MeshRenderer>();
                sillyBallMeshRenderer.material.color = new Color(Random.value, Random.value, Random.value);
                sillyBallMeshRenderer.material.SetFloat("_Metallic", Random.value);
                sillyBallMeshRenderer.material.SetFloat("_Glossiness", Random.value);

                Collider sillyBallCollider = sillyBall.GetComponent<Collider>();
                sillyBallCollider.material.bounciness = 1.0f;
            }
        }
    }
}
