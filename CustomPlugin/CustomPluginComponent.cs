using EFT;
using UnityEngine;
using Comfort.Common;
using BepInEx;
using EFT.UI;

namespace CustomPlugin
{
    public class CustomPluginComponent : MonoBehaviour
    {
        private GameWorld gameWorld;
        private bool toggleSpawnPhysicsObject = false;

        void Start()
        {
            gameWorld = Singleton<GameWorld>.Instance;
        }

        void FixedUpdate()
        {
            if (gameWorld == null)
            {
                ConsoleScreen.Log("Custom Plugin failed to get GameWorld and will try again.");
                gameWorld = Singleton<GameWorld>.Instance;
                return;
            }

            if (UnityInput.Current.GetKeyDown(KeyCode.I))
                toggleSpawnPhysicsObject = !toggleSpawnPhysicsObject;

            if (toggleSpawnPhysicsObject)
                SpawnPhysicsObject();
        }

        private void SpawnPhysicsObject()
        {
            if (gameWorld != null)
            {
                GameObject physicsObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                physicsObject.AddComponent<Rigidbody>();
                physicsObject.transform.position = gameWorld.MainPlayer.Transform.position + (Vector3.up * 5.0f);
                float objectScale = Random.Range(0.1f, 1.0f);
                physicsObject.transform.localScale = new Vector3(objectScale, objectScale, objectScale);
                physicsObject.GetComponent<MeshRenderer>().material.color = new Color(Random.value, Random.value, Random.value);
                physicsObject.GetComponent<Collider>().material.bounciness = 5.0f;
                physicsObject.GetComponent<Rigidbody>().mass = 0.1f;
            }
        }
    }
}
