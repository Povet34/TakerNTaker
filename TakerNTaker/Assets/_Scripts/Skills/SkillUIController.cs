using IngameSkill;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillUIController : MonoBehaviour
{
    [SerializeField] Dictionary<int, SkillButton> SlotDic;

    private void Awake()
    {
        var bts = GetComponentsInChildren<SkillButton>();
        if(null != bts)
        {
            for(int i = 0; i < bts.Length; i++)
                SlotDic[i] = bts[i];
        }
    }

    public void Bind(Action<PointerEventData> action, Action<PointerEventData> actionEx = null)
    {
        var bt = GetEmptySlot();
        if(bt)
        {
            bt.BindEvent(action, actionEx);
        }
    }

    private SkillButton GetEmptySlot()
    {
        return SlotDic.Select(pair => pair.Value).FirstOrDefault(value => value != null);
    }
}
