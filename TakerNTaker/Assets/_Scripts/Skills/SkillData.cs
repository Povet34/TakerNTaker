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
            W_RUSH,                 //전사 돌진
            W_FOOTS_UNDER_FIELD,    //전사 발밑 장판
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

        [Header("# Level Data")]
        public float[] damages;
        public int[] counts;
        public float[] ranges;
        public float[] coolTime;
        public float[] duration;
        public float[] requiredMana;

        [Header("# Weapon")]
        public GameObject projectile;
    }
}
