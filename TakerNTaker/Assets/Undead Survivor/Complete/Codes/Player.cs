using InGameInteractable;
using IngameSkill;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Goldmetal.UndeadSurvivor
{
    public class Player : MonoBehaviour
    {
        public Vector2 inputVec;
        public float speed;
        public Scanner scanner;
        public Hand[] hands;
        public RuntimeAnimatorController[] animCon;
        public FieldOfView viewSight;

        Rigidbody2D rigid;
        PlayerInput playerInput;
        SpriteRenderer spriter;
        Animator anim;

        private List<ISkill> equipedSkills = new List<ISkill>();
        public Vector3 CurrentPlayerLookVector { get; private set; }

        void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            spriter = GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();
            scanner = GetComponent<Scanner>();
            hands = GetComponentsInChildren<Hand>(true);
            playerInput = GetComponent<PlayerInput>();
        }

        void OnEnable()
        {
            speed *= Character.Speed;
            anim.runtimeAnimatorController = animCon[GameManager.instance.playerId];

            viewSight.SetAimwDirection(Vector2.right);
            viewSight.UpdateSight(rigid.position);
        }

        void Update()
        {
            if (!GameManager.instance.isLive)
                return;
        }

        void FixedUpdate()
        {
            if (!GameManager.instance.isLive)
                return;

            inputVec = playerInput.actions["Move"].ReadValue<Vector2>();

            Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);

            if (Vector2.zero != inputVec)
            {
                CurrentPlayerLookVector = inputVec.normalized;

                viewSight.SetAimwDirection(CurrentPlayerLookVector);
                viewSight.UpdateSight(rigid.position);
            }
        }

        void LateUpdate()
        {
            if (!GameManager.instance.isLive)
                return;

            anim.SetFloat("Speed", inputVec.magnitude);

            if (inputVec.x != 0) {
                spriter.flipX = inputVec.x < 0;
            }
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            if (!GameManager.instance.isLive)
                return;

            if (collision.collider.gameObject.layer.Equals(LayerMask.NameToLayer("Object")))
                return;

            //Take Item
            {
                if(collision.collider.gameObject.CompareTag("InGameInteractable"))
                {
                    var interactable = collision.collider.GetComponent<IInGameInteractable>();
                    if(null != interactable)
                    {
                        interactable.Use(this);
                        return;
                    }
                }
            }

            //TakeDamage
            {
                GameManager.instance.health -= Time.deltaTime * 10;

                if (GameManager.instance.health < 0)
                {
                    for (int index = 2; index < transform.childCount; index++)
                    {
                        transform.GetChild(index).gameObject.SetActive(false);
                    }

                    anim.SetTrigger("Dead");
                    GameManager.instance.GameOver();
                }

                return;
            }
        }

        void OnMove(InputValue value)
        {
            inputVec = value.Get<Vector2>();
        }

        public ISkill FindEquipedSkill(SkillData.eSkillType skillType)
        {
            if (null == equipedSkills)
            {
                equipedSkills = new List<ISkill>();
                return null;
            }

            foreach(var sk in equipedSkills)
            {
                if (sk.Data.skillType == skillType)
                    return sk;
            }

            return null;
        }

        public void AddEquipSkill(ISkill addSkill)
        {
            if (null == equipedSkills)
            {
                equipedSkills = new List<ISkill>();
            }

            if(equipedSkills.Count == 0)
            {
                FirstSkill(addSkill.Data.classType);
            }

            equipedSkills.Add(addSkill);
        }

        /// <summary>
        /// If first skill.. you decided class
        /// </summary>
        /// <param name="type"></param>
        void FirstSkill(Definitions.eClassType type)
        {
            //spriter 바꿔주고..
            //class 정해주고
            //평타 넣어준다.

            var datas = AttackDataMngr.instance.GetClassCategory(type);
            if(datas.Count > 0)
            {
                var data = datas.First();

                IClassAttack ca = null;
                switch (data.attackType)
                {
                    case ClassAttackData.eAttackType.None:
                    case ClassAttackData.eAttackType.TEST:
                        {

                        }break;
                    case ClassAttackData.eAttackType.Warrior_Slash:
                        {
                            ca = new GameObject(nameof(A_Warrior_Slash)).AddComponent<A_Warrior_Slash>();
                            ca.Init(data);
                        }
                        break;
                    case ClassAttackData.eAttackType.Warrior_DragSlash:
                        {
                            ca = new GameObject(nameof(A_Warrior_DragSlash)).AddComponent<A_Warrior_DragSlash>();
                            ca.Init(data);
                        }
                        break;
                }
            }
        }

        public void RemoveEquipSkill(SkillData.eSkillType skillType)
        {
            if (null == equipedSkills)
            {
                equipedSkills = new List<ISkill>();
                return;
            }

            ISkill removeTarget = null;
            foreach (var sk in equipedSkills)
            {
                if (sk.Data.skillType == skillType)
                {
                    removeTarget = sk;
                }
            }

            if(null != removeTarget)
                equipedSkills.Remove(removeTarget);
        }
    }
}
