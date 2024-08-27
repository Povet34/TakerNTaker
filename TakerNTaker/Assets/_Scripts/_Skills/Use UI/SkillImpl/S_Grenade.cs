using CodeMonkey.Utils;
using Goldmetal.UndeadSurvivor;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace IngameSkill
{
    public class S_Grenade : MonoBehaviour, ISkill
    {
        public int level { get ; set ; }
        public Player player { get; set; }
        public SkillUIController Controller { get; set; }
        public SkillData Data { get; set; }
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
            Controller.BindSkill(data, OnBegin, OnEnd, OnMove);
        }

        public void LevelUp()
        {
            name = $"{nameof(S_Grenade)}{++level}";
        }

        #region Projectile 

        Vector2 throwDir;
        GameObject uiArrow;

        void OnBegin(PointerEventData eventData)
        {
            //캐릭터부터의 화살표 표시하기 실시간으로 갱신하기
            throwDir = eventData.position;

            if(!uiArrow)
            {
                uiArrow = Instantiate(Data.projectiles[2], transform.position, Quaternion.identity);
            }
            uiArrow.transform.position = transform.position;
            uiArrow.SetActive(true);
        }

        void OnEnd(PointerEventData eventData)
        {
            var dir = throwDir - eventData.position;
            Debug.Log($"throwDir : {dir}");

            var granadeObj = Data.projectiles.First();
            if (granadeObj)
            {
                var go = Instantiate(granadeObj, transform.position, Quaternion.identity);
                go.GetComponent<GranadeObject>()?.Init(Data, -dir.normalized);
            }

            uiArrow.SetActive(false);
        }

        void OnMove(PointerEventData eventData)
        {
            if (uiArrow)
            {
                var dir = throwDir - eventData.position;
                uiArrow.transform.rotation = Quaternion.Euler(new Vector3(0,0, UtilsClass.GetAngleFromVector(dir) - 180));
            }
        }

        #endregion
    }
}

