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
        Warrior_SnakeSlash,
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

    public string attackName;
    [TextArea]
    public string attackDesc;

    [Header("# Base Init Data")]
    public float baseDamage;
    public int baseCount;
    public float baseRange;
    public float baseCoolTime;
    public float baseDuration;

    [Header("# Weapon")]
    public GameObject projectile;
    public List<Vector2> trajectories;
    public List<int> brenches;

    public void SetTrajectories(List<Vector2> points)
    {
        trajectories = points;
    }

    public void SetBranches(List<int> brenches)
    {
        this.brenches = brenches;
    }
}
