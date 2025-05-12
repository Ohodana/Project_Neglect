using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUIAllocation : MonoBehaviour
{
    [Header("메인메뉴 UI")]
    [SerializeField] private GameObject[] bottomButtonUI;
    [SerializeField] private Button[] bottomButton;
    [SerializeField] private TextMeshProUGUI[] currencyTexts;

    public GameObject[] BottomButtonUI => bottomButtonUI;
    public Button[] BottomButton => bottomButton;
    public TextMeshProUGUI[] CurrencyTexts => currencyTexts;

    [Header("전투 관련 UI")]
    [SerializeField] private TextMeshProUGUI[] battleTexts;
    [SerializeField] private Button[] battleButtons;
    [SerializeField] private Image[] battleSliders;
    [SerializeField] private Image[] battleCooldowns;

    public TextMeshProUGUI[] BattleTexts => battleTexts;
    public Button[] BattleButtons => battleButtons;
    public Image[] BattleSliders => battleSliders;
    public Image[] BattleCooldowns => battleCooldowns;


}
