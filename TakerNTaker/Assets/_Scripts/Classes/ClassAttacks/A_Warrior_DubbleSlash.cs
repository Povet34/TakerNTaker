using CodeMonkey.Utils;
using Goldmetal.UndeadSurvivor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Warrior_DubbleSlash : MonoBehaviour, IClassAttack, IHitdetection
{
    public Player player { get; set; }
    public ClassAttackData attackData { get; set; }

    float timer;
    GameObject slashGo;

    [SerializeField] float attackAngle = 30;
    [SerializeField] float attackOnceCount = 10;

    List<Vector3> attackPathPositions = new List<Vector3>();
    public Collider2D collider { get; set; }

    void Awake()
    {
        player = GameManager.instance.player;
        collider = CreateCollider();
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

                var perFrame = attackData.baseDuration / attackOnceCount;
                slashGo.transform.position = GetTrajectoryPosition(attackData.trajectories[0]);
                
                slashGo.SetActive(true);

                //Do Slash
                List<Vector2> trajectories = new List<Vector2>();
                for(int o = 0; o < attackData.trajectories.Count; o++)
                {
                    trajectories.Add(attackData.trajectories[o] * attackData.baseRange);
                }

                if(collider is PolygonCollider2D polygon)
                {
                    polygon.points = trajectories.ToArray();
                }
                
                for (int o = 0; o < attackData.trajectories.Count; o++)
                {
                    transform.rotation = Quaternion.Euler(0,0, UtilsClass.GetAngleFromVector(player.CurrentPlayerLookVector));

                    yield return new WaitForSeconds(perFrame);
                    slashGo.transform.localPosition = trajectories[o];
                }


                slashGo.transform.position = GetTrajectoryPosition(attackData.trajectories[0]);

                yield return new WaitForSeconds(0.15f);
                slashGo.SetActive(false);
            }
        }
    }
    public Vector2 GetTrajectoryPosition(Vector2 trajectory, bool muitiflyRange = false)
    {
        return trajectory * (muitiflyRange ? attackData.baseRange : 1) + player.GetPosXY();
    }

    public void LevelUp()
    {
    }

    public Rect GetBounds(List<Vector2> points)
    {
        if (points == null || points.Count == 0)
        {
            return new Rect();
        }

        // Initialize min and max points
        Vector2 min = points[0];
        Vector2 max = points[0];

        // Iterate through the points to find the min and max x and y
        foreach (var point in points)
        {
            if (point.x < min.x)
                min.x = point.x;
            if (point.y < min.y)
                min.y = point.y;
            if (point.x > max.x)
                max.x = point.x;
            if (point.y > max.y)
                max.y = point.y;
        }

        return new Rect(min.x, min.y, max.x - min.x, max.y - min.y);
    }

    public Collider2D CreateCollider()
    {
        var collider = gameObject.AddComponent<PolygonCollider2D>();
        collider.isTrigger = true;
        return collider;
    }
}
