using KoreanTyper;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public Text TargetText; // 텍스트 컴포넌트
    public RectTransform TextRect; // 텍스트의 RectTransform
    private string fullText = ""; // 전체 텍스트를 저장하는 변수

    [SerializeField]
    private string nextSceneName;

    private void Start()
    {
        StartCoroutine(TypingText());
    }

    public IEnumerator TypingText()
    {

        string[] strings = new string[3] {
                "기타누락자\n신의 변덕 또는 기적으로 죽어야 할 사람이 살았을때 그 사람을 일컫는 말.",
                "큰일났습니다..!\n여행자 명단에 인원이 맞지 않습니다!\n\n뭐!?\n명단에 인원이 맞지 않으면 더 큰 희생이 따른단걸 모른단 말이냐!?\n\n죄송합니다..! 반드시 찾아내겠습니다!",
                "그들은 여행자 명단에 누락된 자를 찾고 있다.\n\n누락된 자를 반드시 찾아야 해!"
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
            //fullText += strings[t] + "\n";

            fullText = "";

            yield return new WaitForSeconds(2f);
        }

        yield return new WaitForSeconds(3f);

        Continue();

    }

    private void AdjustTextHeight()
    {
        // 텍스트의 실제 높이를 계산
        float preferredHeight = TargetText.preferredHeight;

        // RectTransform 높이 업데이트
        TextRect.sizeDelta = new Vector2(TextRect.sizeDelta.x, preferredHeight);
    }

    private void Continue()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            // 씬 전환
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            // 씬 이름이 없으면 콘솔에 메시지를 출력 (디버깅용)
            Debug.Log("Next scene not set!");
        }
    }
}
