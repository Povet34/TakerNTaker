using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IngameSkill
{
    [CreateAssetMenu(fileName = "Skill", menuName = "Scriptble Object/Skill")]
    public class SkillData : MonoBehaviour
    {
        public enum eSkillType { None, }

        [Header("# Main Info")]
        public eSkillType skillType;
        public int skillId;
        public string skillName;
        [TextArea]
        public string skillDesc;
        public Sprite skillIcon;

        [Header("# Level Data")]
        public float baseDamage;
        public int baseCount;
        public float[] damages;
        public int[] counts;
        public float coolTime;
        public float duration;
        public float requiredMana;

        [Header("# Weapon")]
        public GameObject projectile;
    }
}
