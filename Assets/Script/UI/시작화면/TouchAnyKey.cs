using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchToContinue : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;
    private bool isSceneChanging = false;

    void Update()
    {
        if (isSceneChanging == false)
        {
            // 화면 터치 감지
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Continue();
                Debug.Log("Skip1");
            }

            // 에디터 테스트를 위한 마우스 클릭 처리 (선택 사항)
            if (Application.isEditor && Input.GetMouseButtonDown(0))
            {
                Continue();
                Debug.Log("Skip2");
            }
        }
    }

    void Continue()
    {
        isSceneChanging = true;
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            // 씬 전환
            SceneTransitionManager.ChangeScene(nextSceneName);
        }
        else
        {
            // 씬 이름이 없으면 콘솔에 메시지를 출력 (디버깅용)
            Debug.Log("Next scene not set!");
        }
    }
}