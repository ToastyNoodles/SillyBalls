using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using Fika.Core.Modding;
using Fika.Core.Modding.Events;
using LiteNetLib.Utils;
using SillyBalls.Patches;

namespace SillyBalls
{
    [BepInPlugin("com.noodles.sillyballs", "Silly Balls", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        NetDataWriter writer;
        public static ManualLogSource logger;
        public static ConfigEntry<bool> fikaNetworking { get; set; }
        public static ConfigEntry<bool> sillyballsOnDeathOnly { get; set; }
        public static ConfigEntry<int> sillyballSpawnCount { get; set; }
        public static ConfigEntry<float> sillyballSpawnForce { get; set; }
        public static ConfigEntry<float> sillyballShrinkSpeed { get; set; }
        public static ConfigEntry<float> sillyballMinSpawnSize { get; set; }
        public static ConfigEntry<float> sillyballMaxSpawnSize { get; set; }

        void Awake()
        {
            logger = Logger;
            logger.LogInfo("Loaded Custom Plugin");

            fikaNetworking = Config.Bind<bool>("Settings", "FIKA Networking", false, "Game MUST be restarted for this to take effect!");
            sillyballsOnDeathOnly = Config.Bind<bool>("Settings", "SillyBalls On Death Only", false);
            sillyballSpawnCount = Config.Bind<int>("Settings", "SillyBall Spawn Amount", 5);
            sillyballSpawnForce = Config.Bind<float>("Settings", "SillyBall Spawn Force", 1.0f);
            sillyballShrinkSpeed = Config.Bind<float>("Settings", "SillyBall Shrink Speed", 0.2f);
            sillyballMinSpawnSize = Config.Bind<float>("Settings", "SillyBall Min Spawn Size", 0.1f);
            sillyballMaxSpawnSize = Config.Bind<float>("Settings", "SillyBall Max Spawn Size", 1.0f);

            if (fikaNetworking.Value)
            {
                writer = new NetDataWriter();
                FikaEventDispatcher.SubscribeEvent<FikaClientCreatedEvent>(OnClientCreated);
                FikaEventDispatcher.SubscribeEvent<FikaServerCreatedEvent>(OnServerCreated);
                logger.LogInfo("FIKA Networking enabled");
            }

            new GameWorldPatch().Enable();
            new ApplyDamageInfoPatch().Enable();
        }

        private void OnClientCreated(FikaClientCreatedEvent clientCreatedEvent)
        {
            clientCreatedEvent.Client.packetProcessor.SubscribeNetSerializable<BallPacket>(HandleBallPacketClient);
        }

        private void OnServerCreated(FikaServerCreatedEvent serverCreatedEvent)
        {
            serverCreatedEvent.Server.packetProcessor.SubscribeNetSerializable<BallPacket>(HandleBallPacketServer);
        }

        private void HandleBallPacketClient(BallPacket packet)
        {

        }

        private void HandleBallPacketServer(BallPacket packet)
        {

        }
    }
}
