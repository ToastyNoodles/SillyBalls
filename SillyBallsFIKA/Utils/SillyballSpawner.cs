using Comfort.Common;
using Fika.Core.Networking;
using LiteNetLib;
using UnityEngine;

namespace SillyBalls
{
    public static class SillyballSpawner
    {
        public static void NetworkSillyBall(Vector3 position)
        {
            for (int i = 0; i < Plugin.SillyBallsAmount.Value; i++)
            {
                SpawnSillyBall(position);

                SillyballPacket packet = new SillyballPacket();
                packet.SpawnPosition = position;

                if (Singleton<FikaServer>.Instantiated)
                {
                    Singleton<FikaServer>.Instance.SendDataToAll(Plugin.writer, ref packet, DeliveryMethod.Unreliable);
                }

                if (Singleton<FikaClient>.Instantiated)
                {
                    Singleton<FikaClient>.Instance.SendData(Plugin.writer, ref packet, DeliveryMethod.Unreliable);
                }
            }
        }

        public static void SpawnSillyBall(Vector3 position)
        {
            for (int i = 0; i < Plugin.SillyBallsAmount.Value; i++)
            {
                GameObject sillyBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sillyBall.AddComponent<SillyBallComponent>();

                sillyBall.name = "SillyballObject";
                sillyBall.transform.position = position;
                sillyBall.transform.localScale = Vector3.one * Random.Range(Plugin.SillyBallsMinSize.Value, Plugin.SillyBallsMaxSize.Value);

                Rigidbody sillyBallRigidbody = sillyBall.AddComponent<Rigidbody>();
                sillyBallRigidbody.AddForce(Random.insideUnitSphere * Plugin.SillyBallsForce.Value, ForceMode.Impulse);

                MeshRenderer sillyBallMeshRenderer = sillyBall.GetComponent<MeshRenderer>();
                sillyBallMeshRenderer.material.color = new Color(Random.value, Random.value, Random.value);
                sillyBallMeshRenderer.material.SetFloat("_Metallic", Random.value);
                sillyBallMeshRenderer.material.SetFloat("_Glossiness", Random.value);

                Collider sillyBallCollider = sillyBall.GetComponent<Collider>();
                sillyBallCollider.material.bounciness = Plugin.SillyBallsBounciness.Value;
            }
        }
    }
}
