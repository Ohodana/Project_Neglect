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
}
