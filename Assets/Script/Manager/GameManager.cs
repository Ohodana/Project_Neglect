using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // 싱글톤 
    public static GameManager Instance { get; private set; }

    private SaveManager saveManager;
    private DataManager dataManager;

    public DataManager DataManager => dataManager; // Getter 제공

    [SerializeField]
    private int increasedActsTime = 5;

    [Header("Managers")]
    [SerializeField]
    private UIManager uiManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        InitializeManagers();
    }

    void Start()
    {
        InvokeRepeating("IncreaseActionPoints", increasedActsTime, increasedActsTime);
    }

    void IncreaseActionPoints()
    {
        dataManager.AddActs(1);
        Debug.Log("Action Points increased by 1.");
    }

    private void InitializeManagers()
    {
        if (uiManager == null)
        {
            Debug.LogError("UIManager가 할당되지 않았습니다.");
            return;
        }
        saveManager = new SaveManager();
        dataManager = new DataManager(saveManager);

    }


    private void OnApplicationQuit()
    {
        dataManager.SaveCurrency();
    }

    // 혹은 씬 이동 시나 게임 종료 시에 호출하는 별도의 함수:
    private void OnDestroy()
    {
        // Hierarchy 상에서 GameManager가 파괴된다면 저장
        // 단, OnDestroy는 종료 직전에 불리지 않을 수도 있으므로 OnApplicationQuit와 함께 쓰거나 상황에 맞게 사용
        dataManager.SaveCurrency();
    }
}
