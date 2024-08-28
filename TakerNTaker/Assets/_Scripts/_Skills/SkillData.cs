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
            S_TestSkill,
            S_Rush,                 //���� ����
            S_FootsUnderField,      //���� �߹� ����
            S_Grenade,              //���� �׷����̵�
            S_Laser,                //���簡...������?
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
        public float baseCharingTime;

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
