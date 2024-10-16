using CodeMonkey.Utils;
using Goldmetal.UndeadSurvivor;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace IngameSkill
{
    public class S_Laser : MonoBehaviour, ISkill, IGauge
    {
        public int level { get; set; }
        public Player owner { get; set; }
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
            owner = GameManager.instance.player;
        }

        public void Init(SkillData data)
        {
            Data = data;

            // Basic Set
            name = $"{GetType().Name}{++level}";
            transform.parent = owner.transform;
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
        LaserMuzzelEffect muzzelEffect;

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

            //��¡ ����Ʈ
            var chObj = Data.projectiles.First();
            if (chObj)
            {
                chargingEffectGo = Instantiate(chObj, transform.position, Quaternion.identity);
                muzzelEffect = chargingEffectGo.GetComponentInChildren<LaserMuzzelEffect>();
                muzzelEffect.Show(false);
            }

            owner.IsMovable = false;
        }

        void OnEnd(PointerEventData eventData)
        {
            if (chargingEffectGo)
            {
                if (IsCharingDone())
                {
                    var line = chargingEffectGo.GetComponentInChildren<LineRenderer>();
                    var dir = -(throwDir - eventData.position).normalized;

                    line.SetPosition(0, owner.GetPosXY() + dir);
                    muzzelEffect.Show(true);

                    RaycastHit2D wallHit = Physics2D.Raycast(owner.GetPosXY(), dir, 100, Definitions.OBJECT_LAYER); 
                    if (null != wallHit.collider)
                    {
                        var hitEffectPrefab = Data.projectiles[1];

                        line.SetPosition(1, wallHit.point);

                        var endHitDir = owner.GetPosXY() - wallHit.point;
                        var endEffect = Instantiate(hitEffectPrefab, wallHit.point, Quaternion.Euler(endHitDir));

                        LaserHitEffect.Data eeData = new LaserHitEffect.Data();
                        eeData.duration = Data.baseDuration;
                        eeData.scale = 1f;
                        endEffect.GetComponent<LaserHitEffect>()?.Init(eeData);

                        float distance = Vector2.Distance(owner.GetPosXY(), wallHit.point);
                        StartCoroutine(ApplyDamage(dir, distance));
                    }
                    else
                    {
                        line.SetPosition(1, owner.GetPosXY() + dir * 100);
                    }

                    wasFire = true;
                    Invoke(nameof(DestroyLaser), Data.baseDuration);
                }
                else
                {
                    DestroyLaser();
                    owner.IsMovable = true;
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
                chargingEffectGo.transform.position = owner.GetPosXY() + dir;
            }

            Charge();

            //��¡ ������
        }

        void DestroyLaser()
        {
            wasFire = false;
            owner.IsMovable = true;

            Destroy(chargingEffectGo);
        }

        IEnumerator ApplyDamage(Vector2 dir, float distance)
        {
            float interval = Data.baseDuration / Data.baseAttackCount;

            //for(int i = 0; i < Data.baseAttackCount; i++)
            //{
            //    yield return new WaitForSeconds(interval);
            //    yield return new WaitForFixedUpdate();
            //}

            var hitEffectPrefab = Data.projectiles[1];
            float elapsedTime = 0f;

            for (int i = 0; i < Data.baseAttackCount; i++)
            {
                RaycastHit2D[] damagableHit = Physics2D.RaycastAll(owner.GetPosXY(), dir, distance, Definitions.DAMAGABLE_LAYER);

                foreach (var hit in damagableHit)
                {
                    if (hit.collider.gameObject == owner.gameObject)
                        continue;

                    var hitDir = owner.GetPosXY() - hit.point;
                    var effect = Instantiate(hitEffectPrefab, hit.point, Quaternion.Euler(hitDir));

                    LaserHitEffect.Data eData = new LaserHitEffect.Data();
                    eData.duration = interval;
                    eData.scale = 0.5f;
                    effect.GetComponent<LaserHitEffect>()?.Init(eData);

                    var damagable = hit.collider.GetComponent<IDamagable>();
                    if (null != damagable)
                    {
                        Vector3 playerPos = owner.transform.position;
                        Vector3 dirVec = transform.position - playerPos;

                        DamageMsg msg = new DamageMsg();
                        msg.owner = owner.gameObject;
                        msg.amount = Data.baseDamage;
                        msg.direction = dirVec;

                        damagable.TakeDamage(msg);
                    }
                }

                while (elapsedTime < interval)
                {
                    elapsedTime += Time.fixedDeltaTime;
                    yield return new WaitForFixedUpdate();
                }

                elapsedTime = 0f; // Reset elapsed time after each attack
            }
        }


        #endregion
    }
}

