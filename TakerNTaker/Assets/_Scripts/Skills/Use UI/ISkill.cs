using Goldmetal.UndeadSurvivor;

namespace IngameSkill
{
    public interface ISkill
    {
        Player player { get; set; }
        SkillUIController Controller { get; set; }
        SkillData Data { get; set; }

        public void Init(SkillData data);

        public void LevelUp();
    }
}



