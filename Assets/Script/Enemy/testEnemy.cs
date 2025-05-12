using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testEnemy : MonoBehaviour
{
    [Header("적 정보SO")]
    [SerializeField]
    private MonsterData monsterData;
    private UIManager uiManager;

    public int maxHP { get; private set; }
    public int HP { get; private set; }
    public int giveEXP { get; private set; }
    public int AttackCooldown { get; private set; }
    public int Attack { get; private set; }
    public int Defense { get; private set; }

    private void Awake()
    {
        maxHP = monsterData.maxHP;
        HP = maxHP;
        giveEXP = monsterData.giveEXP;
        AttackCooldown = monsterData.defaultAttackCooldown;
        Attack = monsterData.defaultAttack;
        Defense = monsterData.defaultDefense;

        uiManager = GameManager.Instance.UIManager;
    }

    public int DecreaseHP(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            Destroy(gameObject);
        }
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

    public void AttackMainChar(MainChar mainChar)
    {
        int damage = Attack - mainChar.Defense;
        if (damage < 0)
        {
            damage = 0;
        }

        mainChar.DecreaseHP(damage);
        uiManager.SetBattleUIText(1, damage.ToString());
    }


}
