using UnityEngine;

namespace SillyBalls
{
    public static class SillyballSpawner
    {
        public static void SpawnSillyBall(DamageInfo damage)
        {
            GameObject sillyBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sillyBall.AddComponent<SillyBallComponent>();
            
            sillyBall.name = "SillyballObject";
            sillyBall.transform.position = damage.HitPoint + (-damage.HitNormal * 1.5f);
            sillyBall.transform.localScale = Vector3.one * Random.Range(Plugin.SillyBallsMinSize.Value, Plugin.SillyBallsMaxSize.Value);

            Rigidbody sillyBallRigidbody = sillyBall.AddComponent<Rigidbody>();
            sillyBallRigidbody.AddForce(-damage.HitNormal * Plugin.SillyBallsForce.Value, ForceMode.Impulse);

            MeshRenderer sillyBallMeshRenderer = sillyBall.GetComponent<MeshRenderer>();
            sillyBallMeshRenderer.material.color = new Color(Random.value, Random.value, Random.value);
            sillyBallMeshRenderer.material.SetFloat("_Metallic", Random.value);
            sillyBallMeshRenderer.material.SetFloat("_Glossiness", Random.value);

            Collider sillyBallCollider = sillyBall.GetComponent<Collider>();
            sillyBallCollider.material.bounciness = Plugin.SillyBallsBounciness.Value;
        }
    }
}
