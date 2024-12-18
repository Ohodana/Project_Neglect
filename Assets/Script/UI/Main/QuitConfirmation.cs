using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuitConfirmation : MonoBehaviour
{
    [SerializeField] private GameObject quitPanel; // 종료 확인 패널 (Inspector에서 할당)
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    private void Awake()
    {
        // 버튼에 리스너 등록
        yesButton.onClick.AddListener(OnYesButtonClick);
        noButton.onClick.AddListener(OnNoButtonClick);

        // 시작 시 패널은 비활성화
        quitPanel.SetActive(false);
    }

    private void Update()
    {
        // 안드로이드 뒤로가기 버튼 감지 (에디터에서도 ESC로 테스트 가능)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 패널이 이미 떠 있다면(사용자가 취소를 누르지 않고 또 뒤로가기 누른 경우)
            // 여기서 바로 종료 처리할 수도 있지만, 일반적으로는 Panel을 띄우는 로직으로 처리
            if (!quitPanel.activeSelf)
            {
                // 패널 활성화 (종료 여부 묻기)
                quitPanel.SetActive(true);
            }
            else
            {
                // 패널이 이미 열려있는데 다시 뒤로를 눌렀을 경우
                // 여기서는 그냥 패널을 닫거나, 바로 종료하거나, 상황에 맞게 처리.
                quitPanel.SetActive(false);
            }
        }
    }

    private void OnYesButtonClick()
    {
        // 실제 디바이스 빌드 시 앱 종료
        Application.Quit();

        // 에디터 환경에서 테스트 시
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void OnNoButtonClick()
    {
        // 취소 시 패널 닫기
        quitPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
}
