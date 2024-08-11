using CodeMonkey.Utils;
using Goldmetal.UndeadSurvivor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class A_Warrior_Slash : MonoBehaviour, IClassAttack
{
    public Player player { get; set; }
    public ClassAttackData attackData { get; set; }

    float timer;
    GameObject slashGo;

    [SerializeField] float attackAngle = 90;
    [SerializeField] float attackOnceCount = 10;

    List<Vector3> attackPathPositions = new List<Vector3>();

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
                attackPathPositions.Clear();

                var playerForwardAngle = UtilsClass.GetAngleFromVector(player.CurrentPlayerLookVector);
                var angleIncrese = attackAngle * 2 / attackOnceCount;
                var perFrame = attackData.baseDuration / attackOnceCount;

                //초기세팅
                var firstAngle =  playerForwardAngle - attackAngle;
                var firstPos = UtilsClass.GetVectorFromAngle(firstAngle);
                slashGo.transform.position = firstPos * attackData.baseRange + player.transform.position;

                slashGo.SetActive(true);

                //Set position
                for (int o = 0; o < attackOnceCount; o++)
                {
                    var targetVector = UtilsClass.GetVectorFromAngle(firstAngle + (angleIncrese * o));
                    attackPathPositions.Add(targetVector);
                }

                //Do Slash
                for(int o = 0; o < attackPathPositions.Count; o++)
                {
                    slashGo.transform.position = attackPathPositions[o] * attackData.baseRange + player.transform.position;

                    yield return new WaitForSeconds(perFrame);
                }

                yield return new WaitForSeconds(0.15f);
                slashGo.SetActive(false);
            }
        }
    }

    public void LevelUp()
    {
    }
}
