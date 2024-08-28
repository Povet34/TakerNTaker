using CodeMonkey.Utils;
using Goldmetal.UndeadSurvivor;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace IngameSkill
{
    public class S_Laser : MonoBehaviour, ISkill, IGauge
    {
        public int level { get; set; }
        public Player player { get; set; }
        public SkillUIController Controller { get; set; }
        public SkillData Data { get; set; }
        public float ChangingTime { get; set; }
        public bool CanChangeWithMove { get; set; }
        public bool CanActivateWithMove { get; set; }
        public bool IsAsSoonAsCasted { get; set; }
        public bool CanEndurable { get; set; }

        private float charingTimer;

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

        public void Charge()
        {
            Debug.Log($"charingTimer : {charingTimer}");
            charingTimer += Time.deltaTime;
        }

        public bool IsCharingDone()
        {
            return Data.baseCharingTime < charingTimer;
        }

        public void LevelUp()
        {
        }

        #region Projectile

        Vector2 throwDir;
        GameObject uiArrowGo;
        GameObject chargingEffectGo;

        void OnBegin(PointerEventData eventData)
        {
            if (chargingEffectGo)
                return;

            charingTimer = 0;
            throwDir = eventData.position;

            if (!uiArrowGo)
            {
                uiArrowGo = Instantiate(Data.projectiles[2], transform.position, Quaternion.identity);
            }
            uiArrowGo.transform.position = transform.position;
            uiArrowGo.SetActive(true);

            //Â÷Â¡ ÀÌÆåÆ®
            var chObj = Data.projectiles.First();
            if (chObj)
            {
                chargingEffectGo = Instantiate(chObj, transform.position, Quaternion.identity);
            }
        }

        void OnEnd(PointerEventData eventData)
        {
            if (chargingEffectGo)
            {
                if (IsCharingDone())
                {
                    var line = chargingEffectGo.GetComponentInChildren<LineRenderer>();
                    var dir = -(throwDir - eventData.position).normalized;

                    line.SetPosition(0, player.GetPosXY() + dir);
                    line.SetPosition(1, player.GetPosXY() + dir * 10);

                    Invoke(nameof(DestroyLaser), 1f);
                }
                else
                {
                    DestroyLaser();
                }
            }
        }

        void OnMove(PointerEventData eventData)
        {
            var dir = -(throwDir - eventData.position).normalized;
            if (uiArrowGo)
            {
                uiArrowGo.transform.rotation = Quaternion.Euler(new Vector3(0, 0, UtilsClass.GetAngleFromVector(dir) - 180));
            }

            if(chargingEffectGo)
            {
                chargingEffectGo.transform.position = player.GetPosXY() + dir;
            }

            Charge();

            //Â÷Â¡ °ÔÀÌÁö
        }

        void DestroyLaser()
        {
            Destroy(chargingEffectGo);
            uiArrowGo.SetActive(false);
        }

        #endregion
    }
}

