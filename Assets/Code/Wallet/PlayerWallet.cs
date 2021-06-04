using System;
using System.Collections.Generic;

public partial class PlayerWallet : IWallet
{
    Dictionary<string, Currency> currencies = new Dictionary<string, Currency>();

    public void AddCurrency(string currencyId, uint value)
    {
        var currency = new Currency(currencyId, value);
        currencies.Add(currencyId, currency);
    }

    public void IncreaseCurrencyValue(string currencyId, uint value)
    {
        CheckIfCurrencyExists(currencyId);
        ChangeCurrencyValue(currencyId, value, (x, y) => x + y );
    }

    public void DecreaseCurrencyValue(string currencyId, uint value)
    {
        CheckIfCurrencyExists(currencyId);
        ChangeCurrencyValue(currencyId, value, (x, y) => x - y);
    }

    void ChangeCurrencyValue(string currencyId, uint value, Func<uint, uint, uint> op)
    {
        checked
        {
            op(currencies[currencyId].Value, value);
        }
    }

    public void SetCurrencyValue(string currencyId, uint value)
    {
        CheckIfCurrencyExists(currencyId);
        checked { currencies[currencyId].Value = value; }
    }

    void CheckIfCurrencyExists(string currencyId)
    {
        if (!currencies.ContainsKey(currencyId)) throw new CurrencyWasNotFoundException();
    }

    public uint GetCurrencyValue(string id)
    {
        CheckIfCurrencyExists(id);
        return currencies[id].Value;
    }

    public string[] GetCurrenciesId()
    {
        var idList = new string[currencies.Count];
        currencies.Keys.CopyTo(idList, 0);
        return idList;
    }
}
