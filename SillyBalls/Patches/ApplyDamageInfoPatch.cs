using Aki.Reflection.Patching;
using Comfort.Common;
using EFT;
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
            if (Plugin.fikaNetworking.Value)
                NetworkSpawnSillyBall(damageInfo.HitPoint, Plugin.sillyballSpawnCount.Value);
            else
                SpawnSillyBall(damageInfo.HitPoint, Plugin.sillyballSpawnCount.Value);
        }

        private static void NetworkSpawnSillyBall(Vector3 spawnPosition, int spawnCount)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                CreateSillyBallGameObject(spawnPosition);

                SpawnSillyBallPacket sillyBallPacket = new SpawnSillyBallPacket();
                sillyBallPacket.spawnPosition = spawnPosition;

                //Playing as server send packet to all clients
                if (Singleton<FikaServer>.Instantiated)
                    Singleton<FikaServer>.Instance.SendDataToAll(Plugin.writer, ref sillyBallPacket, DeliveryMethod.Unreliable);

                //Playing as client send packet to server
                if (Singleton<FikaClient>.Instantiated)
                    Singleton<FikaClient>.Instance.SendData(Plugin.writer, ref sillyBallPacket, DeliveryMethod.Unreliable);
            }
        }

        private static void SpawnSillyBall(Vector3 spawnPosition, int spawnCount)
        {
            for (int i = 0; i < spawnCount; i++)
                CreateSillyBallGameObject(spawnPosition);
        }

        private static void CreateSillyBallGameObject(Vector3 spawnPosition)
        {
            GameObject sillyBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sillyBall.AddComponent<SillyBallComponent>();
            sillyBall.transform.position = spawnPosition + (Vector3.up * 2.0f);
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

        public static void SpawnBallFromServerOnClient(Vector3 position)
        {
            SpawnSillyBall(position, Plugin.sillyballSpawnCount.Value);
        }
    }
}
