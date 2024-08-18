using System.Collections.Generic;
using UnityEngine;

namespace IngameSkill
{
    [CreateAssetMenu(fileName = "Skill", menuName = "Scriptble Object/SkillData")]
    public class SkillData : ScriptableObject
    {
        public enum eSkillType 
        {
            None,
            TEST,
            W_RUSH,                 //���� ����
            W_FOOTS_UNDER_FIELD,    //���� �߹� ����
            W_RANGE_ATTACK,         //���� ���Ÿ� ����
            W_GRANADE,              //���� �׷����̵�
        }

        [Header("# Main Info")]
        public Definitions.eClassType classType;
        public eSkillType skillType;
        public int skillId;
        public string skillName;
        [TextArea]
        public string skillDesc;
        public Sprite skillIcon;

        [Header("# Base Init Data")]
        public float baseDamage;
        public int baseCount;
        public float baseRange;
        public float baseDuration;

        [Header("# Level Data")]
        public float[] damages;
        public int[] counts;
        public float[] ranges;
        public float[] coolTime;
        public float[] duration;
        public float[] requiredMana;

        [Header("# Weapon")]
        public List<GameObject> projectiles;
    }
}
