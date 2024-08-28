using Goldmetal.UndeadSurvivor;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace IngameSkill
{
    public class SelectSkill : MonoBehaviour
    {
        public SkillData data;
        public int level;

        Image icon;
        Text textLevel;
        Text textName;
        Text textDesc;

        public void Init(SkillData data, UnityAction selectCallback)
        {
            GetComponent<Button>().onClick.AddListener(
                () => 
                {
                    OnClick(selectCallback);
                });

            this.data = data;

            icon = GetComponentsInChildren<Image>()[1];
            icon.sprite = data.skillIcon;

            Text[] texts = GetComponentsInChildren<Text>();
            textLevel = texts[0];
            textName = texts[1];
            textDesc = texts[2];
            textName.text = data.skillName;

            textLevel.text = "Lv." + (level + 1);

            switch (data.skillType)
            {
                case SkillData.eSkillType.None:
                case SkillData.eSkillType.S_Rush:
                case SkillData.eSkillType.S_FootsUnderField:
                case SkillData.eSkillType.S_Grenade:
                case SkillData.eSkillType.S_Laser:
                    textDesc.text = string.Format(data.skillDesc, data.damages[level], data.counts[level]);
                    break;
                default:
                    textDesc.text = string.Format(data.skillDesc);
                    break;
            }
        }

        public void OnClick(UnityAction selectCallback)
        {
            var player = GameManager.instance.player;
            if (null == player)
                return;

            var name = data.skillType.ToSafeString();
            ISkill sk = player.FindEquipedSkill(data.skillType);

            if (null == sk)
            {
                var go = new GameObject(name);
                var type = Type.GetType($"{Definitions.NAMESPACE_INGAMESKILL}.{name}");
                var goo = go.AddComponent(type);
                sk = goo.GetComponent<ISkill>();
                sk.Init(data);
                player.AddEquipSkill(sk);
            }
            else
            {
                sk.LevelUp();
            }

            if (level == data.damages.Length)
            {
                GetComponent<Button>().interactable = false;
            }

            selectCallback?.Invoke();
        }
    }
}


