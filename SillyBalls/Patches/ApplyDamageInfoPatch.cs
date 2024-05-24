using Aki.Reflection.Patching;
using Comfort.Common;
using EFT;
using EFT.UI;
using Fika.Core.Networking;
using LiteNetLib;
using SillyBalls.Packets;
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
            IPlayer damagePlayer = damageInfo.Player.iPlayer;
            if (Plugin.sillyballsOnDeathOnly.Value && !damagePlayer.HealthController.IsAlive)
                SpawnSillyBall(damageInfo.HitPoint, Plugin.sillyballSpawnCount.Value);
            else if (!Plugin.sillyballsOnDeathOnly.Value)
                SpawnSillyBall(damageInfo.HitPoint, Plugin.sillyballSpawnCount.Value);
        }

        private static void SpawnSillyBall(Vector3 spawnPosition, int spawnCount)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                GameObject sillyBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sillyBall.AddComponent<SillyBallComponent>();
                sillyBall.transform.position = spawnPosition;
                float randomScale = Random.Range(Plugin.sillyballMinSpawnSize.Value, Plugin.sillyballMaxSpawnSize.Value);
                sillyBall.transform.localScale = Vector3.one * randomScale;

                sillyBall.AddComponent<Rigidbody>();
                Rigidbody sillyBallRigidbody = sillyBall.GetComponent<Rigidbody>();
                sillyBallRigidbody.AddForce(Random.insideUnitSphere * Plugin.sillyballSpawnForce.Value, ForceMode.Impulse);
                sillyBallRigidbody.mass = 0.01f;

                MeshRenderer sillyBallMeshRenderer = sillyBall.GetComponent<MeshRenderer>();
                sillyBallMeshRenderer.material.color = new Color(Random.value, Random.value, Random.value);
                sillyBallMeshRenderer.material.SetFloat("_Metallic", Random.value);
                sillyBallMeshRenderer.material.SetFloat("_Glossiness", Random.value);

                Collider sillyBallCollider = sillyBall.GetComponent<Collider>();
                sillyBallCollider.material.bounciness = 1.0f;

                if (Plugin.fikaNetworking.Value)
                {
                    SpawnSillyBallPacket sillyBallPacket = new SpawnSillyBallPacket();

                    if (Singleton<FikaServer>.Instantiated)
                    {
                        ConsoleScreen.Log("Sent Packet To Clients");
                        Singleton<FikaServer>.Instance.SendDataToAll(Plugin.writer, ref sillyBallPacket, DeliveryMethod.Unreliable);
                    }
                    else
                    {
                        ConsoleScreen.Log("Sent Packet To Server");
                        Singleton<FikaClient>.Instance.SendData(Plugin.writer, ref sillyBallPacket, DeliveryMethod.Unreliable);
                    }
                }
            }
        }
    }
}
