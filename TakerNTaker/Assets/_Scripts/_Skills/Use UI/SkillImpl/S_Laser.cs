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
        GameObject chargingEffectGo;

        bool wasFire;

        void OnBegin(PointerEventData eventData)
        {
            if (chargingEffectGo)
                return;

            wasFire = false;

            charingTimer = 0;
            throwDir = eventData.position;

            CommonSpawner.Instance.SetUI3DArrowPosition(transform.position);
            CommonSpawner.Instance.ShowUI3DArrow(true);

            //Â÷Â¡ ÀÌÆåÆ®
            var chObj = Data.projectiles.First();
            if (chObj)
            {
                chargingEffectGo = Instantiate(chObj, transform.position, Quaternion.identity);
            }

            player.IsMovable = false;
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

                    RaycastHit2D wallHit = Physics2D.Raycast(player.GetPosXY(), dir, 100, LayerMask.GetMask("Object"));
                    if (null != wallHit.collider)
                    {
                        line.SetPosition(1, wallHit.point);

                        float distance = Vector2.Distance(player.GetPosXY(), wallHit.point);
                        RaycastHit2D[] damagableHit = Physics2D.RaycastAll(player.GetPosXY(), dir, distance);
                        foreach(var hit in damagableHit)
                        {
                            //Instantiate(new GameObject("¿©±â ÆÄÆ¼Å¬!"), hit.point, );
                        }
                    }
                    else
                    {
                        line.SetPosition(1, player.GetPosXY() + dir * 100);
                    }

                    wasFire = true;
                    Invoke(nameof(DestroyLaser), 1f);
                }
                else
                {
                    DestroyLaser();
                    player.IsMovable = true;
                }
            }

            CommonSpawner.Instance.ShowUI3DArrow(false);
        }

        void OnMove(PointerEventData eventData)
        {
            var dir = -(throwDir - eventData.position).normalized;
            CommonSpawner.Instance.SetDirectionAndPosition_FormPlayer(dir);

            if(chargingEffectGo)
            {
                chargingEffectGo.transform.position = player.GetPosXY() + dir;
            }

            Charge();

            //Â÷Â¡ °ÔÀÌÁö
        }

        void DestroyLaser()
        {
            wasFire = false;
            player.IsMovable = true;

            Destroy(chargingEffectGo);
        }

        #endregion
    }
}

