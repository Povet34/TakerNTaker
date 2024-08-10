using Goldmetal.UndeadSurvivor;
using IngameSkill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FootsUnderField : MonoBehaviour, ISkill
{
    public SkillUIController Controller { get; set; }
    public SkillData Data { get; set; }
    public Player player { get; set; }

    void Awake()
    {
        player = GameManager.instance.player;
    }

    public void Init(SkillData data)
    {
        Data = data;
    }

    public void LevelUp()
    {
        throw new System.NotImplementedException();
    }
}
