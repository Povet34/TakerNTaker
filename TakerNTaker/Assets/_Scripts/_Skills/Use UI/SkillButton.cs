using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace IngameSkill 
{
    public class SkillButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
    {
        public enum Type
        {
            Hold,
            Press,
        }

        public Action<PointerEventData> OnDownHandler = null;
        public Action<PointerEventData> OnUpHandler = null;
        public Action<PointerEventData> OnMoveHandler = null;
        [SerializeField] Image skillImage;

        SkillData data;

        bool isBegin;
        PointerEventData moveEventData;

        public bool IsEmpty()
        {
            return data == null;
        }

        public void BindEvent(SkillData data, Action<PointerEventData> actionEn, Action<PointerEventData> actionEx = null, Action<PointerEventData> actionMove = null)
        {
            this.data = data;
            skillImage.sprite = data.skillIcon;

            OnDownHandler -= actionEn;
            OnUpHandler -= actionEx;
            OnMoveHandler -= actionMove;

            OnDownHandler += actionEn;
            OnUpHandler += actionEx;
            OnMoveHandler += actionMove;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (OnDownHandler != null)
            {
                isBegin = true;
                OnDownHandler.Invoke(eventData);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (OnUpHandler != null)
            {
                isBegin = false;
                OnUpHandler.Invoke(eventData);

                moveEventData = null;
            }
        }

        private void Update()
        {
            if (isBegin)
            {
                if (null != OnMoveHandler && null != moveEventData)
                {
                    OnMoveHandler.Invoke(moveEventData);
                }
            }
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            moveEventData = eventData;
        }
    }
}

