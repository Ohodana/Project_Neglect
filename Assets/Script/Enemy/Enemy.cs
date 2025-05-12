using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("적 정보SO")]
    [SerializeField]
    private MonsterData monsterData;
    private UIManager uiManager;
    [SerializeField]
    private Animator animator; // 애니메이터 컴포넌트

    [Header("체력바")]
    [SerializeField]
    private EnemyHealthBar enemyHealthBar;  // 슬라임 위에 띄울 체력바 프리팹(또는 자식 오브젝트에 있는 컴포넌트)

    [Header("피격 파티클 이펙트")]
    [SerializeField]
    private ParticleSystem hitParticleEffect;

    public int maxHP { get; private set; }
    public int HP { get; private set; }
    public int giveEXP { get; private set; }
    public int AttackCooldown { get; private set; }
    public int Attack { get; private set; }
    public int Defense { get; private set; }

    private void Awake()
    {
        // 몬스터 데이터 초기화
        maxHP = monsterData.maxHP;
        HP = maxHP;
        giveEXP = monsterData.giveEXP;
        AttackCooldown = monsterData.defaultAttackCooldown;
        Attack = monsterData.defaultAttack;
        Defense = monsterData.defaultDefense;

        if (enemyHealthBar != null)
        {
            Debug.Log("체력바 설정됨");
            enemyHealthBar.SetMaxHealth(maxHP);
        }
        animator = GetComponentInChildren<Animator>();
        uiManager = GameManager.Instance.UIManager;


    }

    public int DecreaseHP(int damage)
    {
        HP -= damage;

        // 체력바 업데이트
        if (enemyHealthBar != null)
        {
            enemyHealthBar.SetHealth(HP);
        }

        // 데미지 입음 애니메이션 실행 ("Hurt" 트리거)
        if (animator != null)
        {
            animator.SetTrigger("Hurt");
            StartCoroutine(ResetTriggerAfterAnimation("Hurt"));
        }

        // 파티클 이펙트 실행 (체력 감소 시)
        if (hitParticleEffect != null)
        {
            // 한 번에 burstCount 만큼 입자 방출하거나, Play()를 사용하여 전체 효과 실행
            // hitParticleEffect.Play();
            hitParticleEffect.Emit(4);  // 1개의 입자 방출
        }

        if (HP <= 0)
        {
            HP = 0;

            // 사망 애니메이션 실행 ("Die" 트리거)
            if (animator != null)
            {
                animator.SetTrigger("Die");
            }

            StartCoroutine(DieAndDestroy());
        }
        return HP;
    }


    IEnumerator DieAndDestroy()
    {
        // Die 애니메이션 길이에 맞춰 딜레이 (예: 2초)
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    public int IncreaseHP(int heal)
    {
        HP += heal;
        if (HP > maxHP)
        {
            HP = maxHP;
        }

        // 체력바 업데이트
        if (enemyHealthBar != null)
        {
            enemyHealthBar.SetHealth(HP);
        }
        return HP;
    }

    public void AttackMainChar(MainChar mainChar)
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
            StartCoroutine(ResetTriggerAfterAnimation("Hurt"));
        }

        int damage = Attack - mainChar.Defense;
        if (damage < 0)
        {
            damage = 0;
        }

        mainChar.DecreaseHP(damage);
        uiManager.SetBattleUIText(1, damage.ToString());
    }

    /// <summary>
    /// 애니메이션이 끝난 후 Trigger를 비활성화하여 Idle로 전환
    /// </summary>
    IEnumerator ResetTriggerAfterAnimation(string animationState)
    {
        yield return new WaitForEndOfFrame(); // 상태 정보 갱신 대기

        if (animator != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName(animationState))
            {
                float animationLength = stateInfo.length;
                yield return new WaitForSeconds(animationLength);

                // Trigger를 리셋하여 Idle 상태로 복귀
                animator.ResetTrigger(animationState);
            }
        }
    }
}
