using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Battle/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    [Header("기본 정보")]
    public string characterName;
    public GameObject characterPrefab;  // 캐릭터 프리팹
    [TextArea] public string description;  // 캐릭터 설명, 스토리, 기타 정보

    [Header("스탯 정보")]
    public int maxHP;
    public int maxEXP;
    public int level;
    public int defaultAttackCooldown;
    public int defaultAttack;
    public int defaultDefense;

    [Header("기타 설정")]
    public Sprite icon;           // UI에 표시할 아이콘
    public RuntimeAnimatorController animatorController;  // 캐릭터 애니메이터
    public AudioClip voiceClip;    // 캐릭터 음성(공격 시 효과음 등)
    public bool isPlaying = false; // 캐릭터가 현재 플레이 중인지 여부

    // 캐릭터 스킬 정보
    [Header("스킬 정보")]
    public List<SkillData> skills = new List<SkillData>();
}