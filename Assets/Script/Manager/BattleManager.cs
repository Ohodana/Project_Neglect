using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BattleManager : MonoBehaviour
{
    private UIManager uiManager;
    private float timer = 0f;
    public float Y_CoolDonwTimer = 0f;

    [SerializeField]
    private CharacterData playerCharSO;

    [SerializeField]
    private GameObject yuiSpawnPosition;
    private MainChar mainChar;

    [SerializeField]
    private GameObject[] EnemySpawnPosition;
    [SerializeField]
    private MonsterData[] monsterData;

    [Header("테스트용 변수")]
    // Inspector를 통한 Enemy 드래그 앤 드롭 제거
    // [SerializeField]
    // private Enemy enemy;

    // 스폰된 Enemy들을 관리할 리스트 (최대 3마리)
    [SerializeField]
    private List<Enemy> activeEnemies = new List<Enemy>();

    // 각 스킬에 대한 개별 쿨다운 타이머 배열
    private float[] skillCooldownTimers;

    // 현재 스폰 위치 인덱스를 추적하기 위한 변수 추가
    private int currentSpawnIndex = 0;

    [Header("몬스터 스폰 설정")]
    [SerializeField]
    private float enemyEntranceOffset = 5f; // 목표 위치에서 오른쪽으로 떨어진 거리(화면 밖에서 생성)
    [SerializeField]
    private float enemyMoveSpeed = 1f;      // 백그라운드와 동일한 이동 속도

    private Animator characterAnimator;  // 캐릭터 애니메이터 참조 추가

    [SerializeField]
    private ParallaxBackground_Type01[] parallaxBackgrounds; // 여러 배경 스크롤링 컴포넌트

    private void Awake()
    {
        uiManager = GameManager.Instance.UIManager;
    }

    private void Start()
    {
        if (playerCharSO.characterPrefab != null)
        {
            GameObject spawnedChar = Instantiate(playerCharSO.characterPrefab, yuiSpawnPosition.transform.position, Quaternion.identity);
            mainChar = spawnedChar.GetComponent<MainChar>();

            // 애니메이터 컴포넌트 가져오기 및 컨트롤러 설정
            characterAnimator = spawnedChar.GetComponent<Animator>();
            if (characterAnimator != null && playerCharSO.animatorController != null)
            {
                characterAnimator.runtimeAnimatorController = playerCharSO.animatorController;
            }
            else
            {
                Debug.LogWarning("애니메이터 컴포넌트나 컨트롤러가 없습니다!");
            }

            // 스킬이 있을 경우, 스킬별 쿨다운 타이머 배열 초기화
            if (mainChar.skills != null && mainChar.skills.Count > 0)
            {
                skillCooldownTimers = new float[mainChar.skills.Count];
                for (int i = 0; i < skillCooldownTimers.Length; i++)
                {
                    skillCooldownTimers[i] = 0f;
                }
            }
        }
        else
        {
            Debug.LogError("프리팹이 설정되지 않았습니다!");
        }

        // 몬스터 랜덤 스폰 호출
        SpawnRandomEnemy();
    }

    private void Update()
    {
        timer += Time.deltaTime; // 경과 시간 누적

        // 삭제된 Enemy 인스턴스가 있으면 리스트에서 제거
        activeEnemies.RemoveAll(e => e == null);

        // 모든 몬스터가 제거되었다면 배경 이동 재개
        if (activeEnemies.Count == 0 && parallaxBackgrounds != null && parallaxBackgrounds.Length > 0)
        {
            foreach (var background in parallaxBackgrounds)
            {
                background.ResumeMovement();
            }
        }

        if (timer >= 2f) // 2초가 지나면
        {
            timer = 0f; // 타이머 초기화
            DecreaseHP(1);
            IncreaseEXP(230);

            if (mainChar.HP <= 0)
            {
                Debug.Log("Game Over!");
                // 추가적인 게임 오버 로직 작성
            }
        }

        // 스킬 쿨다운 처리: 모든 스킬에 대해 개별적으로 업데이트
        if (mainChar != null && uiManager != null && mainChar.skills != null)
        {
            for (int i = 0; i < mainChar.skills.Count; i++)
            {
                ProcessSkillCooldown(i);
            }
        }
    }

    private void ProcessSkillCooldown(int skillIndex)
    {
        if (skillIndex < 0 || skillIndex >= mainChar.skills.Count)
        {
            Debug.LogError($"유효하지 않은 스킬 인덱스: {skillIndex}");
            return;
        }

        float skillCooldown = mainChar.skills[skillIndex].CooldownTime;

        if (skillCooldown <= 0f)
        {
            Debug.LogError("Cooldown 값이 유효하지 않습니다!");
            return;
        }

        // 개별 타이머를 사용하여 쿨다운 관리
        if (skillCooldownTimers[skillIndex] <= 0f)
        {
            // 스킬 사용: 타이머를 쿨다운 시간으로 초기화하고 공격 실행
            skillCooldownTimers[skillIndex] = skillCooldown;

            // 첫 번째 살아있는 몬스터 찾아서 공격
            for (int i = 0; i < activeEnemies.Count; i++)
            {
                if (activeEnemies[i] != null)
                {
                    activeEnemies[i].DecreaseHP(mainChar.skills[skillIndex].Damage);
                    break; // 첫 번째 살아있는 몬스터를 찾으면 반복문 종료
                }
            }
        }
        else
        {
            // 남은 쿨다운 타이머 감소
            skillCooldownTimers[skillIndex] -= Time.deltaTime;
            // 쿨다운 UI를 해당 스킬 인덱스로 업데이트
            uiManager.SetBattleCoolDownUI(skillIndex, skillCooldown, skillCooldownTimers[skillIndex]);
        }
    }

    private void DecreaseHP(int damage)
    {
        mainChar.DecreaseHP(damage);
        uiManager.SetBattleUIText(1, $"{mainChar.maxHP} / {mainChar.HP}");
        uiManager.SetBattleUIText(2, $"{Math.Truncate(((float)mainChar.HP / mainChar.maxHP) * 100)}%");
        uiManager.SetBattleSliderValue(1, (float)mainChar.HP / mainChar.maxHP);
    }

    private void IncreaseHP(int heal)
    {
        mainChar.IncreaseHP(heal);
        uiManager.SetBattleUIText(1, $"{mainChar.maxHP} / {mainChar.HP}");
        uiManager.SetBattleUIText(2, $"{Math.Truncate(((float)mainChar.HP / mainChar.maxHP) * 100)}%");
        uiManager.SetBattleSliderValue(1, (float)mainChar.HP / mainChar.maxHP);
    }

    private void IncreaseEXP(int exp)
    {
        uiManager.SetBattleSliderValue(0, (float)mainChar.IncreaseEXP(exp) / mainChar.maxEXP);
    }

    private void IncreaseAttack(int amount)
    {
        uiManager.SetBattleUIText(3, mainChar.IncreaseAttack(amount).ToString());
    }

    private void IncreaseDefense(int amount)
    {
        uiManager.SetBattleUIText(4, mainChar.IncreaseDefense(amount).ToString());
    }

    private void SpawnRandomEnemy()
    {
        if (EnemySpawnPosition == null || EnemySpawnPosition.Length == 0 ||
            monsterData == null || monsterData.Length == 0)
        {
            Debug.LogError("Enemy spawn positions 또는 monster data가 올바르게 설정되지 않았습니다!");
            return;
        }

        // 최대 3마리까지만 스폰
        if (activeEnemies.Count >= 3)
        {
            Debug.Log("최대 스폰 양(3마리)에 도달했습니다.");
            return;
        }

        Transform spawnTransform = EnemySpawnPosition[currentSpawnIndex].transform;

        // 다음 스폰을 위해 인덱스 업데이트
        currentSpawnIndex = (currentSpawnIndex + 1) % EnemySpawnPosition.Length;

        // 목표(스폰) 위치: spawnTransform의 위치
        Vector3 targetPos = spawnTransform.position;

        // 화면 밖에서 생성하기 위해 목표 위치에서 오른쪽으로 오프셋 적용
        Vector3 spawnPos = targetPos + Vector3.right * enemyEntranceOffset;

        int monsterIndex = UnityEngine.Random.Range(0, monsterData.Length);

        // enemy prefab을 spawnPos에서 world space에 인스턴스화 (부모 지정 X)
        GameObject enemyInstance = Instantiate(monsterData[monsterIndex].monsterPrefab,
            spawnPos,
            Quaternion.identity);

        Enemy newEnemy = enemyInstance.GetComponent<Enemy>();
        if (newEnemy != null)
        {
            activeEnemies.Add(newEnemy);
            Debug.Log("Spawned enemy: " + monsterData[monsterIndex].monsterName);

            EnemyMover mover = enemyInstance.GetComponent<EnemyMover>();
            if (mover != null)
            {
                // 몬스터 스폰 시 캐릭터 달리기 애니메이션 시작
                if (characterAnimator != null)
                {
                    characterAnimator.SetBool("isRun", true);
                }

                mover.SetTargetPosition(targetPos);
                mover.SetMoveSpeed(enemyMoveSpeed);
                mover.OnTargetReached += HandleEnemyReachedTarget;  // 이벤트 구독
            }
            else
            {
                Debug.LogWarning("EnemyMover 컴포넌트가 없습니다!");
            }
        }
        else
        {
            Debug.LogWarning("스폰된 몬스터에 Enemy 컴포넌트가 없습니다!");
        }
    }

    // 몬스터가 목표 위치에 도달했을 때 호출될 메서드
    private void HandleEnemyReachedTarget(EnemyMover mover)
    {
        if (characterAnimator != null)
        {
            characterAnimator.SetBool("isRun", false);
        }

        // 몬스터가 목표 위치에 도달했을 때 배경 스크롤링 정지
        if (parallaxBackgrounds != null && parallaxBackgrounds.Length > 0)
        {
            foreach (var background in parallaxBackgrounds)
            {
                background.StopMovement();
            }
        }

        // 이벤트 구독 해제
        mover.OnTargetReached -= HandleEnemyReachedTarget;
    }
}
