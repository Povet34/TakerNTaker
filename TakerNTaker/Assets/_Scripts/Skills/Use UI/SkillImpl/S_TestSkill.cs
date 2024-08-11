using Goldmetal.UndeadSurvivor;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace IngameSkill
{
    public class S_TestSkill : MonoBehaviour, ISkill
    {
        int level;

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

            // Basic Set
            name = $"{GetType().Name}{++level}";
            transform.parent = player.transform;
            transform.localPosition = Vector3.zero;

            Controller = FindObjectOfType<SkillUIController>();

            Action<PointerEventData> action = (data) => { Debug.Log("S_TestSkill BeginClick!"); };
            Action<PointerEventData> actionEx = (data) => { Debug.Log("S_TestSkill EndClick!"); };

            Controller.BindSkill(data, action, actionEx);
        }

        public void LevelUp()
        {
            name = $"{nameof(S_TestSkill)}{++level}";
        }
    }
}