using KoreanTyper;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KoreanTyperDemo_Auto : MonoBehaviour
{
    public Text TargetText; // 텍스트 컴포넌트
    public RectTransform TextRect; // 텍스트의 RectTransform
    private string fullText = ""; // 전체 텍스트를 저장하는 변수

    private void Start()
    {
        StartCoroutine(TypingText());
    }

    public IEnumerator TypingText()
    {
        while (true)
        {
            string[] strings = new string[3] {
                "20XX년 X월 X일 \nX요일\n 오후\n HH시 \n MM분",
                "유니티 한글 타이퍼 오토 타이핑 데모 씬",
                "이 데모는 자동으로 작성되고 있습니다."
            };

            for (int t = 0; t < strings.Length; t++)
            {
                int strTypingLength = strings[t].GetTypingLength();

                for (int i = 0; i <= strTypingLength; i++)
                {
                    string newText = strings[t].Typing(i);

                    // 기존 텍스트와 새 텍스트를 결합
                    TargetText.text = fullText + newText;

                    // 텍스트 크기에 따라 RectTransform 높이 조정
                    AdjustTextHeight();

                    yield return new WaitForSeconds(0.03f);
                }

                // 현재 텍스트를 전체 텍스트에 추가
                fullText += strings[t] + "\n";

                yield return new WaitForSeconds(1f);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private void AdjustTextHeight()
    {
        // 텍스트의 실제 높이를 계산
        float preferredHeight = TargetText.preferredHeight;

        // RectTransform 높이 업데이트
        TextRect.sizeDelta = new Vector2(TextRect.sizeDelta.x, preferredHeight);
    }
}
