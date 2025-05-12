using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillData", menuName = "Battle/SkillData", order = 3)]
public class SkillData : ScriptableObject
{
    [Header("스킬 정보")]
    [SerializeField]
    private string skillName;
    [SerializeField]
    private string description;
    [SerializeField]
    private float cooldownTime;
    [SerializeField]
    private int damage;
    [Header("스킬 프리팹")]
    [SerializeField]
    private GameObject skillPrefab; // 스킬 사용 시 활성화/비활성화할 프리팹

    public GameObject SkillPrefab => skillPrefab;


    public string SkillName => skillName;
    public string Description => description;
    public float CooldownTime => cooldownTime;
    public int Damage => damage;
}