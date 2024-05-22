using EFT;
using UnityEngine;
using Comfort.Common;
using BepInEx;
using EFT.UI;

namespace CustomPlugin
{
    public class CustomPluginComponent : MonoBehaviour
    {
        GameWorld gameWorld;

        void Start()
        {
            gameWorld = Singleton<GameWorld>.Instance;
        }

        void Update()
        {
            if (gameWorld == null)
            {
                ConsoleScreen.Log("Custom Plugin failed to get GameWorld and will try again.");
                gameWorld = Singleton<GameWorld>.Instance;
            }

            SpawnPhysicsObject();
        }

        private void SpawnPhysicsObject()
        {
            if (gameWorld != null)
            {
                GameObject physicsObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                physicsObject.AddComponent<Rigidbody>();
                physicsObject.transform.position = gameWorld.MainPlayer.Transform.position + (Vector3.up * 2.0f);
                float objectScale = Random.Range(0.1f, 1.0f);
                physicsObject.transform.localScale = new Vector3(objectScale, objectScale, objectScale);
                physicsObject.GetComponent<MeshRenderer>().material.color = new Color(Random.value, Random.value, Random.value);
            }
        }
    }
}
