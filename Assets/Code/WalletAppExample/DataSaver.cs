using System;
using System.IO;
using UnityEngine;

static class DataSaver
{
    public static void SaveToPrefs(string key, ISerializable valueToSerialize)
    {
        PlayerPrefs.SetString(key,valueToSerialize.SerializeToJson());
        PlayerPrefs.Save();
    }

    public static void ReadFromPrefs(string key, ISerializable valueToDeserialize)
    {
        if (!PlayerPrefs.HasKey(key)) throw new Exception($"Key {key} was not found by DataSaver.");
        var json = PlayerPrefs.GetString(key);
        valueToDeserialize.Deserialize(json);
    }

    public static void SaveToTextFile(string path, ISerializable valueToSerialize)
    {
        var json = valueToSerialize.SerializeToJson();
        File.WriteAllText(path, json);
    }

    public static void ReadFromTextFile(string path, ISerializable valueToDeserialize)
    {
        if (!File.Exists(path)) throw new Exception($"File {path} was not found by DataSaver.");
        var json = File.ReadAllText(path);
        valueToDeserialize.Deserialize(json);
    }

    public static void SaveToBinaryFile(string path, ISerializable valueToSerialize)
    {
        var binary = valueToSerialize.SerializeToBinary();
        File.WriteAllBytes(path, binary);
    }

    public static void ReadFromBinaryFile(string path, ISerializable valueToDeserialize)
    {
        if (!File.Exists(path)) throw new Exception($"File {path} was not found by DataSaver.");
        var binary = File.ReadAllBytes(path);
        valueToDeserialize.Deserialize(binary);
    }
}