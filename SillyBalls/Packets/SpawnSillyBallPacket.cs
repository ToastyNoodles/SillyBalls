using Fika.Core.Networking;
using LiteNetLib.Utils;
using UnityEngine;

namespace SillyBalls.Packets
{
    public class SpawnSillyBallPacket : INetSerializable
    {
        public Vector3 spawnPosition;

        public void Deserialize(NetDataReader reader)
        {
            spawnPosition = reader.GetVector3();
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(spawnPosition);
        }
    }
}
