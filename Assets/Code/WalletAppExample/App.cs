using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class App : MonoBehaviour
{
    const string id0 = "id0";
    const string id1 = "id1";
    const string walletSaveKey = "wallet";
    const string coinsName = "Coins";
    const string crystalsName = "Crystals";
    string savePathText = $"{Application.streamingAssetsPath}/TextWallet.json";
    string savePathBinary = $"{Application.streamingAssetsPath}/TextWallet.bn";

    string[] currenciesIds = new string[] { id0, id1};
    Dictionary<string, string> currenciesName;
    Dictionary<string, string> nameCurrencyId;
    uint[] currenciesValues = new uint[] { 100, 15};

    PlayerWallet pWallet;

    public Dropdown currenciesMenu;
    public Text currencyValueText;

    public InputField inputField;

    string activeCurrencyId = string.Empty;


    // Start is called before the first frame update
    void Start()
    {
        currenciesName = new Dictionary<string, string>();
        currenciesName.Add(id0, coinsName);
        currenciesName.Add(id1, crystalsName);

        nameCurrencyId = new Dictionary<string, string>();
        nameCurrencyId.Add(coinsName, id0);
        nameCurrencyId.Add(crystalsName, id1);

        pWallet = new PlayerWallet();
        AddDefaultCurrencies();
        activeCurrencyId = pWallet.GetCurrenciesId()[0];

        UpdateUI();
        currenciesMenu.onValueChanged.AddListener(OnActiveCurrencyChanged);
    }
    ~App()
    {
        currenciesMenu.onValueChanged.RemoveListener(OnActiveCurrencyChanged);
    }

    private void OnActiveCurrencyChanged(int index)
    {
        var activeCurrencyName = currenciesMenu.options[index].text;
        activeCurrencyId = nameCurrencyId[activeCurrencyName];
        UpdateCurrencyValue();
    }

    public void AddDefaultCurrencies()
    {
        for(var i = 0; i < currenciesIds.Length; i++)
            pWallet.AddCurrency(currenciesIds[i], currenciesValues[i]);
    }

    public void SaveWalletToPlayerPrefs() =>
        DataSaver.SaveToPrefs(walletSaveKey, pWallet);


    public void ReadWalletFromPlayerPrefs()
    {
        var newWallet = new PlayerWallet();
        DataSaver.ReadFromPrefs(walletSaveKey, newWallet);
        pWallet = newWallet;
        UpdateUI();
    }

    public void SaveWalletToTextFile() =>
    DataSaver.SaveToTextFile(savePathText, pWallet);


    public void ReadWalletFromTextFile()
    {
        var newWallet = new PlayerWallet();
        DataSaver.ReadFromTextFile(savePathText, newWallet);
        pWallet = newWallet;
        UpdateUI();
    }

    public void SaveWalletToBinaryFile() =>
    DataSaver.SaveToBinaryFile(savePathBinary, pWallet);


    public void ReadWalletFromBinaryFile()
    {
        var newWallet = new PlayerWallet();
        DataSaver.ReadFromBinaryFile(savePathBinary, newWallet);
        pWallet = newWallet;
        UpdateUI();
    }

    private void UpdateUI()
    {
        UpdateDropDownList();
    }

    private void UpdateDropDownList()
    {
        currenciesMenu.options.Clear();
        foreach (var currency in pWallet.GetCurrenciesId())
            currenciesMenu.options.Add(new Dropdown.OptionData(currenciesName[currency]));

        currenciesMenu.captionText.text = currenciesMenu.options[currenciesMenu.value].text;
        UpdateCurrencyValue();
    }

    private void UpdateCurrencyValue()
    {
        currencyValueText.text = pWallet.GetCurrencyValue(activeCurrencyId).ToString();
    }

    public void SetValue()
    {
        if (uint.TryParse(inputField.text, out uint result))
        {
            pWallet.SetCurrencyValue(activeCurrencyId, result);
            UpdateUI();
        }
        else
            inputField.text = string.Empty;
    }

    public void IncreaseValue()
    {
        if (uint.TryParse(inputField.text, out uint result))
        {
            pWallet.IncreaseCurrencyValue(activeCurrencyId, result);
            UpdateUI();
        }
        else
            inputField.text = string.Empty;
    }

    public void DecreaseValue()
    {
        if (uint.TryParse(inputField.text, out uint result))
        {
            pWallet.DecreaseCurrencyValue(activeCurrencyId, result);
            UpdateUI();
        }
        else
            inputField.text = string.Empty;
    }

}
