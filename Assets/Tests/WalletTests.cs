using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class WalletTests
{
    PlayerWallet wallet;
    string[] currenciesIds = new string[] { "id0", "id1", "id2", "id3", "id4" };
    uint[] currenciesValues = new uint[] { uint.MinValue, 15, 240, 55, uint.MaxValue };

    public static List<string> StringArrayToList(string[] array)
    {
        var list = new List<string>();
        foreach (var str in array)
            list.Add(str);
        return list;
    }

    [SetUp]
    public void SetUp()
    {
        wallet = new PlayerWallet();
        for (var i = 0; i < currenciesIds.Length; i++)
            wallet.AddCurrency(currenciesIds[i], currenciesValues[i]);
    }

    [Test]
    public void WalletTest_Setting_And_ReceivingIds()
    {

        var receivedCurrenciesIds = wallet.GetCurrenciesId();
        Assert.AreEqual(currenciesIds.Length, receivedCurrenciesIds.Length);

        var listIds = StringArrayToList(currenciesIds);
        var contains = true;
        foreach (var receivedId in receivedCurrenciesIds)
            contains &= listIds.Contains(receivedId);
        listIds = StringArrayToList(receivedCurrenciesIds);
        foreach (var id in currenciesIds)
            contains &= listIds.Contains(id);
        Assert.IsTrue(contains);
    }

    [Test]
    public void WalletTest_Setting_And_Receiving_Values()
    {

        var valuesDict = new Dictionary<string, uint>();
        for (var i = 0; i < currenciesIds.Length; i++)
            valuesDict.Add(currenciesIds[i], currenciesValues[i]);

        var receivedCurrenciesIds = wallet.GetCurrenciesId();
        var equals = true;

        foreach (var receivedId in receivedCurrenciesIds)
            equals &= valuesDict[receivedId] == wallet.GetCurrencyValue(receivedId);

        Assert.IsTrue(equals);
    }

    [Test]
    public void WalletTest_OverflowMaxException()
    {

        var valuesDict = new Dictionary<string, uint>();
        for (var i = 0; i < currenciesIds.Length; i++)
            valuesDict.Add(currenciesIds[i], currenciesValues[i]);

        try
        {
            wallet.SetCurrencyValue(currenciesIds[currenciesIds.Length - 1], uint.MaxValue);
            wallet.IncreaseCurrencyValue(currenciesIds[currenciesIds.Length - 1], 1);
        }
        catch(OverflowException e)
        {
            Assert.Pass();
        }
        Assert.Fail();
    }

    [Test]
    public void WalletTest_OverflowMinException()
    {
        var valuesDict = new Dictionary<string, uint>();
        for (var i = 0; i < currenciesIds.Length; i++)
            valuesDict.Add(currenciesIds[i], currenciesValues[i]);

        try
        {
            wallet.SetCurrencyValue(currenciesIds[currenciesIds.Length - 1], uint.MinValue);
            wallet.DecreaseCurrencyValue(currenciesIds[currenciesIds.Length - 1], 1);
        }
        catch (OverflowException e)
        {
            Assert.Pass();
        }
        Assert.Fail();
    }

    [Test]
    public void WalletTest_Json_Serialization()
    {
        var json = wallet.SerializeToJson();

        Debug.Log(json);

        var deserializedWallet = new PlayerWallet();
        deserializedWallet.Deserialize(json);

        var equals = true;
        foreach (var currencyId in currenciesIds)
            equals &= wallet.GetCurrencyValue(currencyId) == deserializedWallet.GetCurrencyValue(currencyId);
        
        Assert.IsTrue(equals);
    }


    [Test]
    public void WalletTest_Binary_Serialization()
    {
        var binary = wallet.SerializeToBinary();

        var deserializedWallet = new PlayerWallet();
        deserializedWallet.Deserialize(binary);

        var equals = true;
        foreach (var currencyId in currenciesIds)
            equals &= wallet.GetCurrencyValue(currencyId) == deserializedWallet.GetCurrencyValue(currencyId);

        Assert.IsTrue(equals);
    }

    [Test]
    public void WalletTest_Increasing_By_1()
    {
        var id = currenciesIds[0];
        uint val = 2;
        uint add = 1;
        wallet.SetCurrencyValue(id, val);
        wallet.IncreaseCurrencyValue(id, add);
        Assert.IsTrue(wallet.GetCurrencyValue(id) == val + add);
    }


    [Test]
    public void WalletTest_Decreasing_By_1()
    {
        var id = currenciesIds[0];
        uint val = 2;
        uint sub = 1;
        wallet.SetCurrencyValue(id, val);
        wallet.DecreaseCurrencyValue(id, sub);
        Assert.IsTrue(wallet.GetCurrencyValue(id) == val - sub);
    }


    //// A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    //// `yield return null;` to skip a frame.
    //[UnityTest]
    //public IEnumerator WalletTestsWithEnumeratorPasses()
    //{
    //    // Use the Assert class to test conditions.
    //    // Use yield to skip a frame.
    //    yield return null;
    //}
}
