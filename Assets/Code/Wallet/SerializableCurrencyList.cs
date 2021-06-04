using System;
using System.Collections.Generic;

[Serializable]
class SerializableCurrencyList
{
    public SerializableCurrency[] currencies;

    public SerializableCurrencyList(List<Currency> currenciesToSerialize)
    {
        var currenciesTmp = new List<SerializableCurrency>();
        foreach(var currency in currenciesToSerialize)
        {
            var serializableCurency = new SerializableCurrency(currency);
            currenciesTmp.Add(serializableCurency);
        }
        currencies = currenciesTmp.ToArray();
    }
}