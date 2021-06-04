using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
partial class PlayerWallet : ISerializable
{
    void SetCurrencyListIntoDictionary(SerializableCurrencyList currencyList)
    {
        foreach (var serialazableCurrency in currencyList.currencies)
        {
            var currency = new Currency(serialazableCurrency.id, serialazableCurrency.value);
            currencies.Add(currency.Id, currency);
        }
    }

    #region Json
    public string SerializeToJson()
    {
        var serializableCurrencies = new SerializableCurrencyList(currencies.Values.ToList());
        var json = JsonUtility.ToJson(serializableCurrencies);
        return json;
    }

    public void Deserialize(string json)
    {
        var serializableCurrencies = JsonUtility.FromJson<SerializableCurrencyList>(json);
        SetCurrencyListIntoDictionary(serializableCurrencies);
    }
    #endregion

    #region Binary
    public byte[] SerializeToBinary()
    {
        var serializableCurrencies = new SerializableCurrencyList(currencies.Values.ToList());
        byte[] binary;
        using (var stream = new MemoryStream())
        {
            var serializator = new BinaryFormatter();
            serializator.Serialize(stream, serializableCurrencies);
            binary = stream.ToArray();
        }

        return binary;
    }

    public void Deserialize(byte[] binary)
    {
        var deserializer = new BinaryFormatter();
        SerializableCurrencyList serializableCurrencies;
        using (var stream = new MemoryStream(binary))
        {
            serializableCurrencies = deserializer.Deserialize(stream) as SerializableCurrencyList;
        }

        SetCurrencyListIntoDictionary(serializableCurrencies);
    }
    #endregion
}
