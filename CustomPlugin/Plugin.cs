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
        public static ConfigEntry<float> ballDespawnSpeed { get; set; }

        void Awake()
        {
            logger = Logger;
            logger.LogInfo("Loaded Custom Plugin");

            fikaNetworking = Config.Bind<bool>("Settings", "FIKA Networking", false, "Game MUST be restarted for this to take effect!");
            ballDespawnSpeed = Config.Bind<float>("Settings", "Despawn Speed", 5.0f);

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
