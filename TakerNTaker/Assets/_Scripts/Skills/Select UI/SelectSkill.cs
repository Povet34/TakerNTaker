using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace IngameSkill
{
    public class SelectSkill : MonoBehaviour
    {
        public SkillData data;
        public int level;
        public ISkill skill;

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
                    textDesc.text = string.Format(data.skillDesc, data.damages[level] * 100, data.counts[level]);
                    break;
                default:
                    textDesc.text = string.Format(data.skillDesc);
                    break;
            }
        }

        public void OnClick(UnityAction selectCallback)
        {
            switch (data.skillType)
            {
                case SkillData.eSkillType.None:
                case SkillData.eSkillType.W_RUSH:
                    if (level == 0)
                    {
                        skill = new GameObject("TestSkill").AddComponent<TestSkill>();
                        skill.Init(data);
                    }
                    else
                    {
                        skill.LevelUp();
                    }

                    level++;
                    break;
            }

            if (level == data.damages.Length)
            {
                GetComponent<Button>().interactable = false;
            }

            selectCallback?.Invoke();
        }
    }

}


