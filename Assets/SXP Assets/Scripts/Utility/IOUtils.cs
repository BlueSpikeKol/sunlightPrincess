using System.IO;
using System.Net.Mime;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class IOUtils
{
    public static byte[] SerializeToByteArray(object input)
    {
        byte[] result;
        BinaryFormatter serializer = new BinaryFormatter();
        using(MemoryStream memStream = new MemoryStream())
        {
            serializer.Serialize(memStream, input);
            result = memStream.GetBuffer();
        }
        return result;
    }

    public static object DeserializeFromByteArray(byte[] buffer)
    {
        BinaryFormatter deserializer = new BinaryFormatter();
        using(MemoryStream memStream = new MemoryStream(buffer))
        {
            object newobj = deserializer.Deserialize(memStream);
            return newobj;
        }
    }
}
