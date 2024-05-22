using EFT;
using UnityEngine;
using Comfort.Common;
using BepInEx;

namespace CustomPlugin
{
    public class CustomPluginComponent : MonoBehaviour
    {
        void Update()
        {
            if (UnityInput.Current.GetKeyDown(KeyCode.I))
            {
                SpawnPhysicsObject();
            }
        }

        private void SpawnPhysicsObject()
        {
            GameWorld gameWorld = Singleton<GameWorld>.Instance;
            if (gameWorld != null)
            {
                GameObject physicsObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                physicsObject.AddComponent<Rigidbody>();
                physicsObject.transform.position = gameWorld.MainPlayer.Transform.position + (Vector3.up * 2.0f);
                float objectScale = Random.Range(0.1f, 3.0f);
                physicsObject.transform.localScale = new Vector3(objectScale, objectScale, objectScale);
                physicsObject.GetComponent<MeshRenderer>().material.color = new Color(Random.value, Random.value, Random.value);
            }
        }
    }
}
