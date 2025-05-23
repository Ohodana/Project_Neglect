using UnityEngine;
using System;

public class DataManager
{
    private SaveManager saveManager;
    private const string CurrencyFileName = "GameCurrency";
    private const string LastSaveTimeKey = "LastSaveTime";

    public GameCurrency CurrentCurrency { get; private set; }

    public event Action OnCurrencyChanged;

    public DataManager(SaveManager saveManager)
    {
        this.saveManager = saveManager;
        LoadCurrency(); // 데이터 로드
    }

    // 데이터 로드
    public void LoadCurrency()
    {
        CurrentCurrency = saveManager.LoadData<GameCurrency>(CurrencyFileName);
        if (CurrentCurrency == null)
        {
            // 기본값 설정
            CurrentCurrency = new GameCurrency(10, 10, 200, 4000);
            SaveCurrency();
        }
        Debug.Log($"Currency Loaded - Gold: {CurrentCurrency.Gold}, Dia: {CurrentCurrency.Dia}, Acts: {CurrentCurrency.Acts}");
    }

    // 데이터 저장
    public void SaveCurrency()
    {
        saveManager.SaveData(CurrencyFileName, CurrentCurrency);

    }

    public void SaveLastSaveTime(DateTime time)
    {
        string timeString = time.ToString("o");
        saveManager.SaveData(LastSaveTimeKey, timeString);
    }

    public DateTime LoadLastSaveTime()
    {
        string timeString = saveManager.LoadData<string>(LastSaveTimeKey);
        if (!string.IsNullOrEmpty(timeString))
        {
            if (DateTime.TryParse(timeString, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime saveTime))
            {
                return saveTime;
            }
        }

        return DateTime.MinValue;
    }


    public void AddGold(int amount)
    {
        CurrentCurrency.AddGold(amount);
        //SaveCurrency();
        OnCurrencyChanged?.Invoke();
    }

    public void SpendGold(int amount)
    {
        if (CurrentCurrency.SpendGold(amount))
        {
            //SaveCurrency();
            OnCurrencyChanged?.Invoke();
        }
    }

    public void AddDia(int amount)
    {
        CurrentCurrency.AddDia(amount);
        //SaveCurrency();
        OnCurrencyChanged?.Invoke();
    }

    public void SpendDia(int amount)
    {
        if (CurrentCurrency.SpendDia(amount))
        {
            //SaveCurrency();
            OnCurrencyChanged?.Invoke();
        }
    }

    public void AddActs(int amount)
    {
        CurrentCurrency.AddActs(amount);
        //SaveCurrency();
        OnCurrencyChanged?.Invoke();
    }

    public void SpendActs(int amount)
    {
        if (CurrentCurrency.SpendActs(amount))
        {
            //SaveCurrency();
            OnCurrencyChanged?.Invoke();
        }
    }
}
