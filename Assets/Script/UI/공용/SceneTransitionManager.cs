using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] private Image fadeImage; // UI Image
    [SerializeField] private float fadeDuration = 1.5f;

    private static SceneTransitionManager instance;

    public static event Action OnFadeInComplete; // 페이드 인 완료 이벤트

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 변경되어도 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void ChangeScene(string sceneName)
    {
        if (instance != null)
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                instance.StartCoroutine(instance.FadeOutAndChangeScene(sceneName));
            }
            else
            {
                Debug.LogError("Scene name is invalid!");
            }
        }
        else
        {
            Debug.LogError("SceneTransitionManager instance is not available!");
        }
    }

    private IEnumerator FadeOutAndChangeScene(string sceneName)
    {
        if (fadeImage == null)
        {
            Debug.LogError("Fade image is not assigned!");
            yield break;
        }

        float elapsedTime = 0f;
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        // 페이드 아웃
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        // 씬 변경
        Debug.Log("Changing scene to!! : " + sceneName);
        SceneManager.LoadScene(sceneName);

        // 페이드 인
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            fadeImage.color = color;
            yield return null;
        }

        // 페이드 인 완료 알림
        OnFadeInComplete?.Invoke();
    }
}
