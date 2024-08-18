using Goldmetal.UndeadSurvivor;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace IngameSkill
{
    public class S_FootsUnderField : MonoBehaviour, ISkill
    {
        public int level { get; set; }
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

            Action<PointerEventData> action = (PointerEventData) => 
            { 
                if(Data.projectiles.First())
                {
                    var field = Instantiate(Data.projectiles.First(), transform.position, Quaternion.identity);
                    if(field)
                    {
                        field.transform.localScale = Vector3.one * Data.baseRange;
                        StartCoroutine(DoCountdown(field, data.baseDuration));
                    }
                }
            };
            Action<PointerEventData> actionEx = (PointerEventData) => 
            {
                Debug.Log("S_FootsUnderField EndClick!"); 
            };


            Controller.BindSkill(data, action, actionEx);
        }

        IEnumerator DoCountdown(GameObject target, float duration)
        {
            yield return new WaitForSeconds(duration);
            Destroy(target);
        }

        public void LevelUp()
        {
            name = $"{nameof(S_FootsUnderField)}{++level}";
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            
        }
    }
}