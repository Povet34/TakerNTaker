using IngameSkill;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestSkill : MonoBehaviour, ISkill
{
    public SkillUIController Controller { get; set; }

    public SkillData Data { get; set; }

    public void Init(SkillData data)
    {
        Data = data;
        Controller = FindObjectOfType<SkillUIController>();

        Action<PointerEventData> action = (data) => { Debug.Log("BeginClick!"); };
        Action<PointerEventData> actionEx = (data) => { Debug.Log("EndClick!"); };

        Controller.Bind(action, actionEx);
    }

    public void LevelUp()
    {
        throw new NotImplementedException();
    }
}
