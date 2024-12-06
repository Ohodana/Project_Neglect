using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    private void Start()
    {
        InitializeUI();
    }

    private void InitializeUI()
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

    private void testActs()
    {
        GameManager.Instance.DataManager.CurrentCurrency.SpendActs(1);
        UpdateCurrencyUI();
    }

}
