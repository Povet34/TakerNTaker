using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace IngameSkill 
{
    public class SkillButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public enum Type
        {
            Hold,
            Press,
        }

        public Action<PointerEventData> OnDownHandler = null;
        public Action<PointerEventData> OnUpHandler = null;
        [SerializeField] Image skillImage;

        SkillData data;

        public bool IsEmpty()
        {
            return data == null;
        }

        public void BindEvent(SkillData data, Action<PointerEventData> action,Action<PointerEventData> actionEx = null)
        {
            this.data = data;
            skillImage.sprite = data.skillIcon;

            OnDownHandler -= action;
            OnUpHandler -= actionEx;
            OnDownHandler += action;
            OnUpHandler += actionEx;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (OnDownHandler != null)
                OnDownHandler.Invoke(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (OnUpHandler != null)
                OnUpHandler.Invoke(eventData);
        }
    }
}

