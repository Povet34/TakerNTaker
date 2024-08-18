using IngameSkill;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillUIController : MonoBehaviour
{
    [SerializeField] Dictionary<int, SkillButton> SlotDic = new Dictionary<int, SkillButton>();

    private void Awake()
    {
        var bts = GetComponentsInChildren<SkillButton>();
        if(null != bts)
        {
            for(int i = 0; i < bts.Length; i++)
                SlotDic[i] = bts[i];
        }
    }

    public void ChangeSkill(
        SkillData data = null,
        Action<PointerEventData> action = null,
        Action<PointerEventData> actionEx = null)
    {

    }

    public void UpdateSkill(
        SkillData data = null ,
        Action<PointerEventData> action = null,
        Action<PointerEventData> actionEx = null)
    {

    }

    public void BindSkill(
        SkillData data,
        Action<PointerEventData> action, 
        Action<PointerEventData> actionEx = null,
        Action<PointerEventData> actionMo = null)
    {
        var bt = GetEmptySlot();
        if(bt)
        {
            bt.BindEvent(data, action, actionEx, actionMo);
        }
    }

    private SkillButton GetEmptySlot()
    {
        foreach (var pair in SlotDic)
        {
            if (pair.Value.IsEmpty())
                return pair.Value;
        }

        return null; 
    }
}
