using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlinkText : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI textMeshPro; // TMP 텍스트를 연결할 변수
    [SerializeField]
    public float blinkDuration = 1f; // 깜빡이는 주기 (초)

    private void Start()
    {
        if (textMeshPro == null)
            textMeshPro = GetComponent<TextMeshProUGUI>();

        // 코루틴 실행
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            // 텍스트 나타나기
            SetAlpha(1f);
            yield return new WaitForSeconds(blinkDuration);

            // 텍스트 사라지기
            SetAlpha(0f);
            yield return new WaitForSeconds(blinkDuration);
        }
    }

    private void SetAlpha(float alpha)
    {
        // TMP 텍스트의 알파 값 변경
        if (textMeshPro != null)
        {
            var color = textMeshPro.color;
            color.a = alpha;
            textMeshPro.color = color;
        }
    }
}
