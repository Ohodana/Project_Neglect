using UnityEngine;

public class DataManager
{
    private SaveManager saveManager;
    private const string CurrencyFileName = "GameCurrency";

    public GameCurrency CurrentCurrency { get; private set; }

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
        Debug.Log("Currency saved.");
    }
}
