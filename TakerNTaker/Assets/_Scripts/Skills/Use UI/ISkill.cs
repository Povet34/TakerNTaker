namespace IngameSkill
{
    public interface ISkill
    {
        SkillUIController Controller { get; }
        SkillData Data { get; }

        public void Init(SkillData data);

        public void LevelUp();
    }
}



