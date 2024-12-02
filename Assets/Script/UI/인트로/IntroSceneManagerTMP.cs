using UnityEngine;
using TMPro;
using UnityEngine.UI; // UI 요소를 위한 네임스페이스
using System.Collections;

public class IntroSceneManagerTMP : MonoBehaviour
{
    public TypewriterEffectTMP typewriter; // 타이핑 효과 스크립트
    public GameObject nextButton;        // "다음" 버튼
    public CanvasGroup fadeGroup;        // 페이드 효과
    public float fadeDuration = 1.0f;    // 페이드 지속 시간

    private int currentStep = 0; // 현재 대사 단계
    private string[] dialogues = new string[3]
    {
        "기타누락자\n신의 변덕 또는 기적으로 죽어야 할 사람이 살았을때 그 사람을 일컫는 말.",
        "큰일났습니다..!\n여행자 명단에 인원이 맞지 않습니다!\n\n뭐!?\n명단에 인원이 맞지 않으면 더 큰 희생이 따른단걸 모른단 말이냐!?\n\n죄송합니다..! 반드시 찾아내겠습니다!",
        "그들은 여행자 명단에 누락된 자를 찾고 있다.\n\n누락된 자를 반드시 찾아야 해!"
    };

    private void Start()
    {
        nextButton.SetActive(false); // 버튼 초기 비활성화
        StartDialogueStep();
    }

    private void StartDialogueStep()
    {
        if (currentStep < dialogues.Length)
        {
            nextButton.SetActive(false); // 버튼 숨기기
            typewriter.StartTyping(dialogues[currentStep]); // 현재 대사 출력
            Invoke(nameof(ShowNextButton), 1.5f); // 타이핑 효과 후 버튼 활성화
        }
        else
        {
            StartCoroutine(FadeToBlack()); // 마지막 페이드 아웃
        }
    }

    private void ShowNextButton()
    {
        nextButton.SetActive(true);
    }

    public void OnNextButtonClicked()
    {
        currentStep++;
        StartDialogueStep();
    }

    private IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }

        Debug.Log("인트로 완료!");
    }
}

