using UnityEngine;

public class JsonSerializer : ISerializer
{
    string ISerializer.Serialize<T>(T obj) => JsonUtility.ToJson(obj, true);
    T ISerializer.Deserialize<T>(string json) => JsonUtility.FromJson<T>(json);
}
