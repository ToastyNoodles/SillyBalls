using EFT;
using UnityEngine;
using Comfort.Common;
using BepInEx;
using System.Collections.Generic;

namespace SillyBalls
{
    public class CustomPluginComponent : MonoBehaviour
    {
        private GameWorld gameWorld;
        private bool toggleSpawnPhysicsObject = false;
        private List<GameObject> physicsObjects = new List<GameObject>();

        void Start()
        {
            gameWorld = Singleton<GameWorld>.Instance;
        }

        void Update()
        {
            if (UnityInput.Current.GetKeyDown(KeyCode.I))
                toggleSpawnPhysicsObject = !toggleSpawnPhysicsObject;
        }

        void FixedUpdate()
        {
            if (gameWorld == null)
            {
                gameWorld = Singleton<GameWorld>.Instance;
                return;
            }

            if (toggleSpawnPhysicsObject)
                SpawnPhysicsObject();
        }

        private void SpawnPhysicsObject()
        {
            if (gameWorld != null)
            {
                GameObject physicsObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                physicsObject.AddComponent<SillyBallComponent>();
                physicsObject.AddComponent<Rigidbody>();

                physicsObject.transform.position = gameWorld.MainPlayer.Transform.position + ((Vector3.up + transform.forward) * 4.0f);
                float objectScale = Random.Range(0.2f, 0.8f);
                physicsObject.transform.localScale = Vector3.one * objectScale;

                MeshRenderer physicsObjectMeshRenderer = physicsObject.GetComponent<MeshRenderer>();
                physicsObjectMeshRenderer.material.color = new Color(Random.value, Random.value, Random.value);
                physicsObjectMeshRenderer.material.SetFloat("_Glossiness", Random.value);
                physicsObjectMeshRenderer.material.SetFloat("_Metallic", Random.value);

                physicsObject.GetComponent<Collider>().material.bounciness = 1.0f;
                physicsObject.GetComponent<Rigidbody>().mass = 0.01f;

                physicsObjects.Add(physicsObject);
            }
        }
    }
}
