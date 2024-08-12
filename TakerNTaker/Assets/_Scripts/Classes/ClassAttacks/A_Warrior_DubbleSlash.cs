using CodeMonkey.Utils;
using Goldmetal.UndeadSurvivor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Warrior_DubbleSlash : MonoBehaviour, IClassAttack
{
    public Player player { get; set; }
    public ClassAttackData attackData { get; set; }

    float timer;
    GameObject slashGo;

    [SerializeField] float attackAngle = 30;
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
        if (null != attackData)
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
        for (int i = 0; i < attackData.baseCount; i++)
        {
            if (slashGo)
            {
                attackPathPositions.Clear();

                var playerForwardAngle = UtilsClass.GetAngleFromVector(player.CurrentPlayerLookVector);
                var angleIncrese = attackAngle * 2 / attackOnceCount;
                var perFrame = attackData.baseDuration / attackOnceCount;

                slashGo.transform.position = attackData.trajectories[0];

                slashGo.SetActive(true);

                //Do Slash
                for (int o = 0; o < attackData.trajectories.Count; o++)
                {
                    yield return new WaitForSeconds(perFrame);
                    slashGo.transform.position = attackData.trajectories[o] * attackData.baseRange + player.GetPosXY();
                }

                slashGo.transform.position = attackData.trajectories[0];

                yield return new WaitForSeconds(0.15f);
                slashGo.SetActive(false);
            }
        }
    }

    public void LevelUp()
    {
    }
}
