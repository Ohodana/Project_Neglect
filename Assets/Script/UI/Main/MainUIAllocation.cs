using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUIAllocation : MonoBehaviour
{
    [SerializeField] private GameObject[] bottomButtonUI;
    [SerializeField] private Button[] bottomButton;
    [SerializeField] private TextMeshProUGUI[] currencyTexts;

    public GameObject[] BottomButtonUI => bottomButtonUI;
    public Button[] BottomButton => bottomButton;
    public TextMeshProUGUI[] CurrencyTexts => currencyTexts;
}
