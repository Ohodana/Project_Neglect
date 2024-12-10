using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEyeToggle : MonoBehaviour
{
    [SerializeField]
    private GameObject eyeOpenObject;     // 눈이 떠진 상태의 오브젝트
    [SerializeField]
    private GameObject eyeClosedObject;   // 눈이 감긴 상태의 오브젝트
    [SerializeField]
    [Range(0f, 1.0f)]
    private float minOpenTime = 1.0f;     // 눈 뜬 상태 최소 유지 시간
    [SerializeField]
    [Range(3.0f, 6.0f)]
    private float maxOpenTime = 3.0f;     // 눈 뜬 상태 최대 유지 시간
    private float minClosedTime = 0.1f;   // 눈 감은 상태 최소 유지 시간
    private float maxClosedTime = 0.3f;   // 눈 감은 상태 최대 유지 시간

    private void OnEnable()
    {
        // 눈 깜빡임 동작 시작
        StartCoroutine(Blink());
    }

    private void OnDisable()
    {
        // 눈 깜빡임 동작 중지
        StopCoroutine(Blink());
    }


    private System.Collections.IEnumerator Blink()
    {
        while (true)
        {
            // 눈 뜬 상태 활성화
            eyeOpenObject.SetActive(true);
            eyeClosedObject.SetActive(false);

            // 눈 뜬 상태 유지 시간 (랜덤)
            float openDuration = Random.Range(minOpenTime, maxOpenTime);
            yield return new WaitForSeconds(openDuration);

            // 눈 감은 상태 활성화
            eyeOpenObject.SetActive(false);
            eyeClosedObject.SetActive(true);

            // 눈 감은 상태 유지 시간 (랜덤)
            float closedDuration = Random.Range(minClosedTime, maxClosedTime);
            yield return new WaitForSeconds(closedDuration);
        }
    }
}

