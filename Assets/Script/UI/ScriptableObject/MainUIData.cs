using UnityEngine;
using TMPro;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "UIMainData", menuName = "ScriptableObjects/UIMainData", order = 1)]
public class MainUIData : ScriptableObject
{
    [Header("Bottom Buttons UI")]
    public GameObject[] bottomButtonUI; // 0: 캐릭터, 1: 동료, 2: 모험, 3: 레벨, 4: 상점
    public Button[] bottomButton;

    [Header("Currency UI")]
    public TextMeshProUGUI[] currencyTexts; // 0: 행동력, 1: 다이아, 2: 골드

    [Header("Test Button")]
    public Button testButton;
}
