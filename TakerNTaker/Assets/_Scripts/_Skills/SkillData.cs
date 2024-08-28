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
            S_Rush,                 //전사 돌진
            S_FootsUnderField,      //전사 발밑 장판
            S_Grenade,              //전사 그레네이드
            S_Laser,                //전사가...레이저?
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
