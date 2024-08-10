using UnityEngine;

namespace IngameSkill
{
    [CreateAssetMenu(fileName = "Skill", menuName = "Scriptble Object/SkillData")]
    public class SkillData : ScriptableObject
    {
        public enum eSkillType 
        {
            None,
            W_RUSH,
        }

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
