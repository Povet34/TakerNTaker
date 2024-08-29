using Goldmetal.UndeadSurvivor;

namespace IngameSkill
{
    public interface ISkill
    {
        int level { get; set; }
        Player owner { get; set; }
        SkillUIController Controller { get; set; }
        SkillData Data { get; set; }
        public void Init(SkillData data);
        public void LevelUp();
    }
}



