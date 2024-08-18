using IngameSkill;
using System.Collections;
using UnityEngine;

namespace Goldmetal.UndeadSurvivor
{
    public class Enemy : MonoBehaviour
    {
        public float speed;
        public float health;
        public float maxHealth;
        public RuntimeAnimatorController[] animCon;
        public Rigidbody2D target;

        bool isLive;

        Rigidbody2D rigid;
        Collider2D coll;
        //Animator anim;
        SpriteRenderer spriter;
        WaitForFixedUpdate wait;

        void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            coll = GetComponent<Collider2D>();
            //anim = GetComponent<Animator>();
            spriter = GetComponent<SpriteRenderer>();
            wait = new WaitForFixedUpdate();
        }

        void FixedUpdate()
        {
            //if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            //    return;

            Vector2 dirVec = target.position - rigid.position;
            Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);
            rigid.velocity = Vector2.zero;
        }

        void LateUpdate()
        {
            if (!GameManager.instance.isLive)
                return;

            if (!isLive)
                return;

            spriter.flipX = target.position.x < rigid.position.x;
        }

        void OnEnable()
        {
            isLive = true;
            coll.enabled = true;
            rigid.simulated = true;
            spriter.sortingOrder = 2;
            //anim.SetBool("Dead", false);
            health = maxHealth;
        }

        public void Init(SpawnData data)
        {
            //anim.runtimeAnimatorController = animCon[data.spriteType];
            speed = data.speed;
            maxHealth = data.health;
            health = data.health;
        }


        void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Attack"))
                return;

            health -= collision.GetComponent<ISkill>()?.Data.baseDamage ?? 0;
            health -= collision.GetComponent<IClassAttack>()?.Data.baseDamage ?? 0;
            StartCoroutine(KnockBack());

            if (health > 0) {
                //anim.SetTrigger("Hit");
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
            }
            else {
                isLive = false;
                coll.enabled = false;
                rigid.simulated = false;
                spriter.sortingOrder = 1;
                //anim.SetBool("Dead", true);
                GameManager.instance.kill++;
                GameManager.instance.GetExp();

                if (GameManager.instance.isLive)
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);
            }
        }

        IEnumerator KnockBack()
        {
            spriter.color = Color.white;
            yield return wait; // 다음 하나의 물리 프레임 딜레이
            Vector3 playerPos = GameManager.instance.player.transform.position;
            Vector3 dirVec = transform.position - playerPos;
            rigid.AddForce(dirVec.normalized * 30, ForceMode2D.Impulse);

            yield return new WaitForSeconds(0.1f);
            spriter.color = Color.red;
        }

        void Dead()
        {
            gameObject.SetActive(false);
        }
    }
}
