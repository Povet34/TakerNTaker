using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IngameSkill;

    [CreateAssetMenu(fileName = "Attack", menuName = "Scriptble Object/AttackData")]
public class ClassAttackData : ScriptableObject
{

    public enum eAttackType
    {
        None,
        TEST,
        Warrior_Slash,
    }

    public Definitions.eClassType classType;
    public eAttackType attackType;

    public int attackId;
    public string attackName;
    [TextArea]
    public string attackDesc;
    public Sprite attackIcon;

    [Header("# Base Init Data")]
    public float baseDamage;
    public int baseCount;
    public float baseRange;
    public float baseCoolTime;
    public float baseDuration;

    [Header("# Weapon")]
    public GameObject projectile;
}
