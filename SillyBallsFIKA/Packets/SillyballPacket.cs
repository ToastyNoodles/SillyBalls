using Fika.Core.Networking;
using LiteNetLib.Utils;
using UnityEngine;

namespace SillyBalls
{
    public class SillyballPacket : INetSerializable
    {
        public Vector3 SpawnPosition;

        public void Deserialize(NetDataReader reader)
        {
            SpawnPosition = reader.GetVector3();
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(SpawnPosition);
        }
    }
}
