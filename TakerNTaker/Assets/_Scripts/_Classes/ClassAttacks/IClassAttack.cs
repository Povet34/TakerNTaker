using Goldmetal.UndeadSurvivor;

public interface IClassAttack
{
    const string TAG_ATTACK = "Attack";

    Player player { get; set; }
    ClassAttackData Data { get; set; }
    public void Init(ClassAttackData data);
    public void LevelUp();
    public void Enable(bool isOn);
}
