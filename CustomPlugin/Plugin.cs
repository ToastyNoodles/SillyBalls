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
        public static ConfigEntry<float> sillyballSize { get; set; }
        public static ConfigEntry<float> sillyballSpawnForce { get; set; }
        public static ConfigEntry<float> sillyballShrinkSpeed { get; set; }
        public static ConfigEntry<bool> sillyballsOnDeathOnly { get; set; }

        void Awake()
        {
            logger = Logger;
            logger.LogInfo("Loaded Custom Plugin");

            fikaNetworking = Config.Bind<bool>("Settings", "FIKA Networking", false, "Game MUST be restarted for this to take effect!");
            sillyballSize = Config.Bind<float>("Settings", "SillyBall Size", 1.0f);
            sillyballSpawnForce = Config.Bind<float>("Settings", "SillyBall Spawn Force", 1.0f);
            sillyballShrinkSpeed = Config.Bind<float>("Settings", "Shrink Speed", 1.0f);
            sillyballsOnDeathOnly = Config.Bind<bool>("Settings", "SillyBalls On Death Only", false);

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
