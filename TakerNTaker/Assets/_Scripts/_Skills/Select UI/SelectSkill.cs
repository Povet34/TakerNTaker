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
                case SkillData.eSkillType.W_RUSH:
                case SkillData.eSkillType.W_RANGE_ATTACK:
                case SkillData.eSkillType.W_FOOTS_UNDER_FIELD:
                case SkillData.eSkillType.W_GRANADE:
                case SkillData.eSkillType.W_LASER:
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

            ISkill sk = null;

            switch (data.skillType)
            {
                case SkillData.eSkillType.TEST:
                    {
                        sk = player.FindEquipedSkill(SkillData.eSkillType.TEST);
                        if (null == sk)
                        {
                            sk = new GameObject(nameof(S_TestSkill)).AddComponent<S_TestSkill>();
                            sk.Init(data);
                            player.AddEquipSkill(sk);
                        }
                        else
                        {
                            sk.LevelUp();
                        }
                        break;
                    }
                case SkillData.eSkillType.W_FOOTS_UNDER_FIELD:
                    {
                        sk = player.FindEquipedSkill(SkillData.eSkillType.W_FOOTS_UNDER_FIELD);
                        if (null == sk)
                        {
                            sk = new GameObject(nameof(S_FootsUnderField)).AddComponent<S_FootsUnderField>();
                            sk.Init(data);
                            player.AddEquipSkill(sk);
                        }
                        else
                        {
                            sk.LevelUp();
                        }
                        break;
                    }
                case SkillData.eSkillType.W_GRANADE:
                    {
                        sk = player.FindEquipedSkill(SkillData.eSkillType.W_GRANADE);
                        if (null == sk)
                        {
                            sk = new GameObject(nameof(S_Grenade)).AddComponent<S_Grenade>();
                            sk.Init(data);
                            player.AddEquipSkill(sk);
                        }
                        else
                        {
                            sk.LevelUp();
                        }
                        break;
                    }
                case SkillData.eSkillType.W_LASER:
                    {
                        sk = player.FindEquipedSkill(SkillData.eSkillType.W_GRANADE);
                        if (null == sk)
                        {
                            sk = new GameObject(nameof(S_Laser)).AddComponent<S_Laser>();
                            sk.Init(data);
                            player.AddEquipSkill(sk);
                        }
                        else
                        {
                            sk.LevelUp();
                        }
                        break;
                    }
            }

            if (level == data.damages.Length)
            {
                GetComponent<Button>().interactable = false;
            }

            selectCallback?.Invoke();
        }
    }
}


