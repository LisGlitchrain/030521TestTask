interface IWallet
{
    /* I used uint here because I suggest that 0 is the least amount of any currency 
        and there is not any real currency used in the game directly.  */
    public void IncreaseCurrencyValue(string currencyId, uint value);

    public void DecreaseCurrencyValue(string currencyId, uint value);

    void SetCurrencyValue(string currencyId, uint value);

    public uint GetCurrencyValue(string id);

    /// <summary>
    /// Returns independent array with currencies' ids;
    /// </summary>
    /// <returns></returns>
    public string[] GetCurrenciesId();
}