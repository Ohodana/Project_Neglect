using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground_Type01 : MonoBehaviour
{
    [SerializeField]
    private Transform target;   //현재 배경과 이어지는 배경
    [SerializeField]
    private float scrollAmount; //이어지는 두 배경 사이의 거리
    [SerializeField]
    private float scrollEed; // 카메라를 벗어나는 위치
    [SerializeField]
    private float moveSpeed;    // 이동 속도
    [SerializeField]
    private Vector3 moveDirction;// 이동 방향

    void Start(){
        scrollAmount = scrollAmount *2;
    }

    void Update()
    {
        transform.position += moveDirction * moveSpeed * Time.deltaTime;

        if (transform.position.x <= scrollEed){
            transform.position = target.position - moveDirction * scrollAmount;
        }
    }
}
