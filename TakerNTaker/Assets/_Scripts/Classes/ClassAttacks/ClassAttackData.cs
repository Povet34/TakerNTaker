using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "Attack", menuName = "Scriptble Object/AttackData")]
public class ClassAttackData : ScriptableObject
{
    public enum eAttackType
    {
        None,
        TEST,
        Warrior_Slash,      //그냥 베기
        Warrior_DragSlash,  //끌어 베기
        Warrior_DubbleSlash,
    }

    public enum eCollisionType
    {
        None,
        Trajectory,     //Tail의 궤적
        Range,          //공격하는 모든 범위
        Point           //특정 지점
    }

    public Definitions.eClassType classType;
    public eAttackType attackType;
    public eCollisionType collisionType;

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
    public List<Vector2> trajectories;

    public void SetTrajectories(List<Vector2> points)
    {
        trajectories = points;
    }
}
