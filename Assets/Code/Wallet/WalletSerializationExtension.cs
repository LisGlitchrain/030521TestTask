using System;
using System.Linq;
using UnityEngine;
using System.IO;
using SerializationExtensions;
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
        byte[] result;
        byte[] buffer;
        using (MemoryStream ms = new MemoryStream())
        {
            buffer = currencies.Count.ToBytes();
            SerializationHelper.WriteBytesToStream(buffer, ms, false);
            foreach (var currency in currencies.Values)
            {
                buffer = currency.Id.ToBytes();
                SerializationHelper.WriteBytesToStream(buffer, ms);
                buffer = currency.Value.ToBytes();
                SerializationHelper.WriteBytesToStream(buffer, ms, false);
            }
            result = ms.ToArray();
        }

        return result;
    }

    public void Deserialize(byte[] binary)
    {
        var offset = 0;
        var length = Serialization.GetInt32PredefinedSize();
        var lengthUInt = Serialization.GetInt32PredefinedSize();
        byte[] buffer = new byte[length];
        byte[] bufferUInt = new byte[lengthUInt];

        Buffer.BlockCopy(binary, offset, buffer, 0, length);
        var currencyCount = buffer.GetInt32();
        offset += length;
        using (var stream = new MemoryStream(binary))
        {
            for(var i = 0; i < currencyCount; i++)
            {
                var id = SerializationHelper.GetStringFromBytesIncrementOffset(binary, ref offset);
                Buffer.BlockCopy(binary, offset, bufferUInt, 0, lengthUInt);
                offset += lengthUInt;
                var value = bufferUInt.GetUInt32();
                AddCurrency(id, value);
            }
        }
    }
    #endregion
}
