using Goldmetal.UndeadSurvivor;

public interface IClassAttack
{
    Player player { get; set; }
    ClassAttackData attackData { get; set; }
    public void Init(ClassAttackData data);
    public void LevelUp();
}
