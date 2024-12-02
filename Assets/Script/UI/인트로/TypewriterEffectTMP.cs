using UnityEngine;
using TMPro;
using System.Collections;

public class TypewriterEffectTMP : MonoBehaviour
{
    [SerializeField]
    private TMP_Text dialogueText;  // TMP_Text를 연결
    [SerializeField]
    private float typingSpeed = 0.05f; // 타이핑 속도

    private string fullText;  // 전체 텍스트
    private Coroutine typingCoroutine; // 코루틴 제어

    // 텍스트 타이핑 시작
    public void StartTyping(string text)
    {
        fullText = text;
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        dialogueText.text = ""; // 초기화
        foreach (char letter in fullText)
        {
            dialogueText.text += letter; // 한 글자씩 추가
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
