using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [Header("목표(스폰) 위치 - 이곳에 도달하면 멈춤")]
    [SerializeField]
    private Vector3 targetPosition;

    [Header("이동 속도 (백그라운드와 동일)")]
    [SerializeField]
    private float moveSpeed = 1f;

    [Header("이동 방향 (기본은 왼쪽)")]
    [SerializeField]
    private Vector3 moveDirection = Vector3.left;

    [Header("목표 도달 허용 오차")]
    [SerializeField]
    private float threshold = 0.1f;

    private bool reachedTarget = false;
    public bool ReachedTarget => reachedTarget;

    // 목표 지점 도달 이벤트
    public event System.Action<EnemyMover> OnTargetReached;

    public void SetTargetPosition(Vector3 target)
    {
        targetPosition = target;
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    private void Update()
    {
        if (reachedTarget)
            return;

        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPosition) <= threshold)
        {
            reachedTarget = true;
            transform.position = targetPosition;
            OnTargetReached?.Invoke(this);  // 목표 지점 도달 시 이벤트 발생
        }
    }
}