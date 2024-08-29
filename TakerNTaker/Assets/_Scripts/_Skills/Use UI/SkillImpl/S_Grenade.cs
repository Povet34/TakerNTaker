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

        void OnBegin(PointerEventData eventData)
        {
            //캐릭터부터의 화살표 표시하기 실시간으로 갱신하기
            throwDir = eventData.position;

            CommonSpawner.Instance.SetUI3DArrowPosition(transform.position);
            CommonSpawner.Instance.ShowUI3DArrow(true);
        }

        void OnEnd(PointerEventData eventData)
        {
            var dir = throwDir - eventData.position;

            var granadeObj = Data.projectiles.First();
            if (granadeObj)
            {
                var go = Instantiate(granadeObj, transform.position, Quaternion.identity);
                go.GetComponent<GranadeObject>()?.Init(Data, -dir.normalized);
            }

            CommonSpawner.Instance.ShowUI3DArrow(false);
        }

        void OnMove(PointerEventData eventData)
        {
            var dir = -(throwDir - eventData.position);
            CommonSpawner.Instance.SetDirectionAndPosition_FormPlayer(dir);
        }

        #endregion
    }
}

