using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using Fika.Core.Modding;
using Fika.Core.Modding.Events;
using LiteNetLib.Utils;
using SillyBalls.Packets;
using SillyBalls.Patches;

namespace SillyBalls
{
    [BepInPlugin("com.noodles.sillyballs", "Silly Balls", "1.0.0")]
    [BepInDependency("com.fika.core")]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource logger;
        public static NetDataWriter writer;
        
        //Silly Balls Config Settings
        public static ConfigEntry<int> sillyballSpawnCount { get; set; }
        public static ConfigEntry<float> sillyballSpawnForce { get; set; }
        public static ConfigEntry<float> sillyballShrinkSpeed { get; set; }
        public static ConfigEntry<float> sillyballMinSpawnSize { get; set; }
        public static ConfigEntry<float> sillyballMaxSpawnSize { get; set; }

        void Awake()
        {
            sillyballSpawnCount = Config.Bind<int>("Settings", "SillyBall Spawn Amount", 2);
            sillyballSpawnForce = Config.Bind<float>("Settings", "SillyBall Spawn Force", 1.0f);
            sillyballShrinkSpeed = Config.Bind<float>("Settings", "SillyBall Shrink Speed", 0.15f);
            sillyballMinSpawnSize = Config.Bind<float>("Settings", "SillyBall Min Spawn Size", 0.2f);
            sillyballMaxSpawnSize = Config.Bind<float>("Settings", "SillyBall Max Spawn Size", 0.8f);

            writer = new NetDataWriter();
            FikaEventDispatcher.SubscribeEvent<FikaClientCreatedEvent>(OnClientCreated);

            new ApplyDamageInfoPatch().Enable();

            Logger.LogInfo("Loaded SillyBalls");
        }

        private void OnClientCreated(FikaClientCreatedEvent @event)
        {
            @event.Client.packetProcessor.SubscribeNetSerializable<SpawnSillyBallPacket>(HandleSpawnSillyBallPacketClient);
        }

        private void HandleSpawnSillyBallPacketClient(SpawnSillyBallPacket packet)
        {
            //Received packet from server replicate on client
            ApplyDamageInfoPatch.SpawnBallFromServerOnClient(packet.spawnPosition);
        }
    }
}
