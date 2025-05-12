using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainChar : MonoBehaviour
{
    [SerializeField]
    private CharacterData playerCharSO;


    public int maxHP { get; private set; }
    public int HP { get; private set; }
    public int maxEXP { get; private set; }
    public int EXP { get; private set; }
    public int level { get; private set; }
    public int AttackCooldown { get; private set; }
    public int Attack { get; private set; }
    public int Defense { get; private set; }
    public List<SkillData> skills { get; private set; }

    private UIManager uiManager;


    private void Awake()
    {
        if (!playerCharSO.isPlaying)
        {
            maxHP = playerCharSO.maxHP;
            HP = maxHP;
            maxEXP = playerCharSO.maxEXP;
            EXP = 0;
            level = playerCharSO.level;
            AttackCooldown = playerCharSO.defaultAttackCooldown;
            Attack = playerCharSO.defaultAttack;
            Defense = playerCharSO.defaultDefense;
            skills = new List<SkillData>(playerCharSO.skills);
        }
        uiManager = GameManager.Instance.UIManager;
    }

    public int DecreaseHP(int damage)
    {
        HP -= damage;
        return HP;
    }

    public int IncreaseHP(int heal)
    {
        HP += heal;
        if (HP > maxHP)
        {
            HP = maxHP;
        }

        return HP;
    }

    public int IncreaseEXP(int exp)
    {
        // 람다식으로 경험치 처리
        Func<int, int> AddExp = (addedExp) =>
        {
            EXP += addedExp; // 경험치 추가
            if (maxEXP <= 0)
            {
                Debug.LogError("maxEXP 값이 유효하지 않습니다!");
                return EXP;
            }

            while (EXP >= maxEXP)
            {
                EXP -= maxEXP; // 초과 경험치를 다음 레벨로 이월
                level++;       // 레벨업
                IncreaseAttack(7);
                IncreaseDefense(3);
                uiManager.SetBattleUIText(3, Attack.ToString());
                uiManager.SetBattleUIText(4, Defense.ToString());
            }

            // 경험치 진행도 반환
            return EXP;
        };

        // 경험치 업데이트
        int remainingEXP = AddExp(exp);

        EXP = remainingEXP;

        return EXP;
    }

    public int IncreaseAttack(int amount)
    {
        Attack += amount;
        return Attack;
    }

    public int IncreaseDefense(int amount)
    {
        Defense += amount;
        return Defense;
    }
}
