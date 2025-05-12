using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("하단 버튼 UI")]
    // 0: 캐릭터, 1:동료, 2: 모험, 3: 레벨, 4: 상점
    [SerializeField]
    private GameObject[] bottomButtonUI;
    [SerializeField]
    private Button[] bottomButton;

    [Header("재화 UI")]
    [SerializeField]
    // 0: 행동력, 1: 다이아, 2: 골드
    private TextMeshProUGUI[] CurrencyTexts;

    [SerializeField]
    private Button testButton;

    private MainUIAllocation allocation;

    [Header("전투 관련 UI")]
    // 0: EXP, 1: HP, 2:HPRate, 3: Attack, 4: Defense
    [SerializeField] private TextMeshProUGUI[] battleTexts;
    [SerializeField] private Button[] battleButtons;
    // 0: EXP, 1: HP, 3:ProgressBar
    [SerializeField] private Image[] battleSliders;

    private void Start()
    {
        //InitializeUI();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameManager.Instance.DataManager.OnCurrencyChanged -= UpdateCurrencyUI;
        // 씬 안에 배치된 MainUIAllocation 오브젝트 찾기
        allocation = FindObjectOfType<MainUIAllocation>();
        if (allocation != null)
        {
            // allocation에서 UI 요소를 가져와 UIManager 내부 로직에 맞게 세팅
            SetupUIFromAllocation(allocation);
        }
    }


    private void SetupUIFromAllocation(MainUIAllocation allocation)
    {

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Main_001")
        {
            this.bottomButtonUI = allocation.BottomButtonUI;
            this.bottomButton = allocation.BottomButton;
            this.CurrencyTexts = allocation.CurrencyTexts;
            GameManager.Instance.DataManager.OnCurrencyChanged += UpdateCurrencyUI;
            InitializeMainUI();
        }

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Battle_001")
        {
            // 배틀 씬에서 필요한 UI 요소를 가져와서 세팅
            this.battleTexts = allocation.BattleTexts;
            this.battleButtons = allocation.BattleButtons;
            this.battleSliders = allocation.BattleSliders;

        }
    }


    private void InitializeMainUI()
    {
        ChangeMainUI(2);

        for (int i = 0; i < bottomButton.Length; i++)
        {
            int index = i;
            bottomButton[i].onClick.AddListener(() => ChangeMainUI(index));
        }

        UpdateCurrencyUI();

        testButton.onClick.AddListener(() => testActs());
    }
    private void InitializeUI()
    {
        // test
    }


    private void ChangeMainUI(int index)
    {
        for (int i = 0; i < bottomButtonUI.Length; i++)
        {
            if (i == index)
            {
                bottomButtonUI[i].SetActive(true);
                bottomButton[i].interactable = false;
            }
            else
            {
                bottomButtonUI[i].SetActive(false);
                bottomButton[i].interactable = true;
            }
        }
    }
    private void UpdateCurrencyUI()
    {
        var currency = GameManager.Instance.DataManager.CurrentCurrency;

        CurrencyTexts[0].text = $"{currency.maxActs}/{currency.Acts}";
        CurrencyTexts[1].text = currency.Dia.ToString();
        CurrencyTexts[2].text = currency.Gold.ToString();
    }

    public void SetBattleUIText(int index, string newText)
    {
        if (index < 0 || index >= battleTexts.Length)
        {
            Debug.LogWarning($"잘못된 텍스트 인덱스: {index}");
            return;
        }

        battleTexts[index].text = newText;
    }

    public void SetBattleSliderValue(int index, float newValue)
    {
        if (index < 0 || index >= battleSliders.Length)
        {
            Debug.LogWarning($"잘못된 슬라이더 인덱스: {index}");
            return;
        }

        battleSliders[index].fillAmount = newValue;
    }

    public void SetBattleCoolDownUI(int index, float maxTime, float currentTime)
    {
        if (index < 0 || index >= allocation.BattleCooldowns.Length)
        {
            Debug.LogWarning($"잘못된 쿨다운 UI 인덱스: {index}");
            return;
        }
        allocation.BattleCooldowns[index].fillAmount = currentTime / maxTime;
    }



    private void testActs()
    {
        // GameManager.Instance.DataManager.CurrentCurrency.SpendActs(1);
        // UpdateCurrencyUI();
        //GameManager.Instance.DataManager.SpendActs(1);
        SceneTransitionManager.ChangeScene("Battle_001");
    }

}
