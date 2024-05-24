using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using Comfort.Common;
using EFT.UI;
using Fika.Core.Modding;
using Fika.Core.Modding.Events;
using LiteNetLib.Utils;
using SillyBalls.Packets;
using SillyBalls.Patches;

namespace SillyBalls
{
    [BepInPlugin("com.noodles.sillyballs", "Silly Balls", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource logger;
        public static NetDataWriter writer;
        
        //Silly Balls Config Settings
        public static ConfigEntry<bool> fikaNetworking { get; set; }
        public static ConfigEntry<bool> sillyballsOnDeathOnly { get; set; }
        public static ConfigEntry<int> sillyballSpawnCount { get; set; }
        public static ConfigEntry<float> sillyballSpawnForce { get; set; }
        public static ConfigEntry<float> sillyballShrinkSpeed { get; set; }
        public static ConfigEntry<float> sillyballMinSpawnSize { get; set; }
        public static ConfigEntry<float> sillyballMaxSpawnSize { get; set; }

        void Awake()
        {
            fikaNetworking = Config.Bind<bool>("Settings", "FIKA Networking", false, "Game MUST be restarted for this to take effect!");
            sillyballsOnDeathOnly = Config.Bind<bool>("Settings", "SillyBalls On Death Only", false);
            sillyballSpawnCount = Config.Bind<int>("Settings", "SillyBall Spawn Amount", 10);
            sillyballSpawnForce = Config.Bind<float>("Settings", "SillyBall Spawn Force", 1.0f);
            sillyballShrinkSpeed = Config.Bind<float>("Settings", "SillyBall Shrink Speed", 0.15f);
            sillyballMinSpawnSize = Config.Bind<float>("Settings", "SillyBall Min Spawn Size", 0.1f);
            sillyballMaxSpawnSize = Config.Bind<float>("Settings", "SillyBall Max Spawn Size", 1.0f);

            if (fikaNetworking.Value)
            {
                writer = new NetDataWriter();
                FikaEventDispatcher.SubscribeEvent<FikaClientCreatedEvent>(OnClientCreated);
                FikaEventDispatcher.SubscribeEvent<FikaServerCreatedEvent>(OnServerCreated);
            }

            new ApplyDamageInfoPatch().Enable();

            Logger.LogInfo("Loaded SillyBalls");
        }

        private void OnClientCreated(FikaClientCreatedEvent @event)
        {
            @event.Client.packetProcessor.SubscribeNetSerializable<SpawnSillyBallPacket>(HandleSpawnSillyBallPacketClient);
        }

        private void OnServerCreated(FikaServerCreatedEvent @event)
        {
            @event.Server.packetProcessor.SubscribeNetSerializable<SpawnSillyBallPacket>(HandleSpawnSillyBallPacketServer);
        }

        private void HandleSpawnSillyBallPacketClient(SpawnSillyBallPacket packet)
        {
            ConsoleScreen.Log("Received Packet From Server");
        }

        private void HandleSpawnSillyBallPacketServer(SpawnSillyBallPacket packet)
        {
            ConsoleScreen.Log("Received Packet From Client");
        }
    }
}
