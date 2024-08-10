using Goldmetal.UndeadSurvivor;
using System.Collections.Generic;
using UnityEngine;


namespace IngameSkill
{
    public class SkillDataMngr : MonoBehaviour
    {
        public static SkillDataMngr instance;
        [SerializeField] List<SkillData> datas;

        private void Awake()
        {
            instance = this;
        }

        public List<SkillData> GetAll() => datas;

        public List<SkillData> GetCategory()
        {
            return null;
        }
    }
}
