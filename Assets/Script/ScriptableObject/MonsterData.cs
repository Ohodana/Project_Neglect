using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMonsterData", menuName = "Battle/MonsterData", order = 2)]
public class MonsterData : ScriptableObject
{
    [Header("기본 정보")]
    public string monsterName;
    public GameObject monsterPrefab;  // 몬스터 프리팹
    [TextArea] public string description;  // 몬스터 설명, 스토리, 기타 정보

    [Header("스탯 정보")]
    public int maxHP;
    public int giveEXP;
    public int defaultAttackCooldown;
    public int defaultAttack;
    public int defaultDefense;

    [Header("기타 설정")]
    public Sprite icon;           // UI에 표시할 아이콘
    public RuntimeAnimatorController animatorController;  // 몬스터 애니메이터
    public AudioClip voiceClip;    // 몬스터 음성(공격 시 효과음 등)
    public bool isPlaying = false; // 몬스터가 현재 플레이 중인지 여부

    // 필요한 경우, 여기서 스킬 목록 등을 참조할 수도 있음
    public List<SkillData> skills;
}
