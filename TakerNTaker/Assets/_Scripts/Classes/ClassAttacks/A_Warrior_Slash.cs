using CodeMonkey.Utils;
using Goldmetal.UndeadSurvivor;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class A_Warrior_Slash : MonoBehaviour, IClassAttack
{
    public Player player { get; set; }
    public ClassAttackData attackData { get; set; }

    float timer;
    GameObject slashGo;

    [SerializeField] float attackAngle = 45;
    [SerializeField] float attackOnceCount = 2;

    void Awake()
    {
        player = GameManager.instance.player;
    }

    public void Init(ClassAttackData data)
    {
        attackData = data;

        name = $"{GetType().Name}";
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        slashGo = Instantiate(data.projectile, transform);
    }

    private void Update()
    {
        if(null != attackData)
        {
            timer += Time.deltaTime;
            if (timer > attackData.baseCoolTime)
            {
                timer = 0;
                StartCoroutine(DoSlash());
            }
        }
    }

    IEnumerator DoSlash()
    {
        for(int i = 0; i < attackData.baseCount; i++)
        {
            if (slashGo)
            {
                var playerForwardAngle = UtilsClass.GetAngleFromVector(player.CurrentPlayerLookVector);
                var angleIncrese = attackAngle * 2 / attackOnceCount;
                var perFrame = attackData.baseDuration / attackOnceCount;

                //초기세팅
                var firstAngle =  playerForwardAngle - attackAngle;
                var firstPos = UtilsClass.GetVectorFromAngle(firstAngle);
                slashGo.transform.position = firstPos * attackData.baseRange + player.transform.position;

                slashGo.SetActive(true);

                for (int o = 0; o < attackOnceCount; o++)
                {
                    var targetVector = UtilsClass.GetVectorFromAngle(firstAngle + (angleIncrese * o));
                    slashGo.transform.position = targetVector * attackData.baseRange + player.transform.position;

                    yield return new WaitForSeconds(perFrame);
                }

                yield return new WaitForSeconds(0.05f);

                slashGo.SetActive(false);
            }
        }
    }

    public void LevelUp()
    {
    }
}
