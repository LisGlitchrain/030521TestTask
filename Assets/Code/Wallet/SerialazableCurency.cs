using System;

[Serializable]
public class SerializableCurrency
{
    public string id;
    public uint value;

    public SerializableCurrency(Currency currency)
    {
        id = currency.Id;
        value = currency.Value;
    }
}
