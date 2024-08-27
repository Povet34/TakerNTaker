using CodeMonkey.Utils;
using Goldmetal.UndeadSurvivor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Warrior_Slash : MonoBehaviour, IClassAttack, IHitdetection
{
    public Player player { get; set; }
    public ClassAttackData Data { get; set; }
    public Collider2D hitBox { get; set; }

    float timer;
    GameObject slashGo;
    List<Vector3> attackPathPositions = new List<Vector3>();

    void Awake()
    {
        player = GameManager.instance.player;
        hitBox = CreateCollider();
        gameObject.tag = IClassAttack.TAG_ATTACK;
    }

    public void Init(ClassAttackData data)
    {
        Data = data;

        name = $"{GetType().Name}";
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        slashGo = Instantiate(data.projectile, transform);
    }

    private void Update()
    {
        if(null != Data)
        {
            timer += Time.deltaTime;
            if (timer > Data.baseCoolTime)
            {
                timer = 0;
                StartCoroutine(DoSlash());
            }
        }
    }

    IEnumerator DoSlash()
    {
        for(int i = 0; i < Data.baseCount; i++)
        {
            if (slashGo)
            {
                attackPathPositions.Clear();

                List<Vector2> trajectories = new List<Vector2>();
                for (int o = 0; o < Data.trajectories.Count; o++)
                {
                    trajectories.Add(Data.trajectories[o] * Data.baseRange);
                }

                var playerAngle = UtilsClass.GetAngleFromVector(player.CurrentPlayerLookVector);
                var perFrame = Data.baseDuration / Data.trajectories.Count;

                var playerRightPos = UtilsClass.GetVectorFromAngle(UtilsClass.GetAngleFromVector(trajectories[0]) + playerAngle);

                var initPos = GetTrajectoryPosition(playerRightPos);

                Enable(true);

                transform.rotation = Quaternion.Euler(0, 0, UtilsClass.GetAngleFromVector(player.CurrentPlayerLookVector));

                //Do Slash

                if (hitBox is PolygonCollider2D polygon)
                {
                    polygon.points = trajectories.ToArray();
                }

                for (int o = 0; o < trajectories.Count; o++)
                {
                    yield return new WaitForSeconds(perFrame);
                    slashGo.transform.localPosition = trajectories[o];
                }

                yield return new WaitForSeconds(0.15f);
                Enable(false);
                slashGo.transform.position = initPos;
            }
        }
    }

    public void LevelUp()
    {
    }

    public void Enable(bool isOn)
    {
        if (slashGo)
            slashGo.SetActive(isOn);

        if (hitBox)
            hitBox.enabled = isOn;
    }

    public Vector2 GetTrajectoryPosition(Vector2 trajectory, bool muitiflyRange = false)
    {
        return trajectory * (muitiflyRange ? Data.baseRange : 1) + player.GetPosXY();
    }

    public Collider2D CreateCollider()
    {
        var collider = gameObject.AddComponent<PolygonCollider2D>();
        collider.isTrigger = true;
        return collider;
    }
}
