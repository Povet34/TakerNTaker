using UnityEngine;

    [CreateAssetMenu(fileName = "Attack", menuName = "Scriptble Object/AttackData")]
public class ClassAttackData : ScriptableObject
{
    public enum eAttackType
    {
        None,
        TEST,
        Warrior_Slash,      //그냥 베기
        Warrior_DragSlash   //끌어 베기
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
