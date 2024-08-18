using IngameSkill;
using UnityEngine;

public class GranadeObject : MonoBehaviour
{
    SkillData data;
    float timer;

    public void Init(SkillData data, Vector2 dir)
    {
        this.data = data;

        var rigid = GetComponent<Rigidbody2D>();
        if(rigid)
        {
            rigid.AddForce(dir * 200);
        }
    }

    private void Update()
    {
        if (timer < data.baseDuration)
            timer += Time.deltaTime;
        else
        {
            Explosion();
        }
    }

    void Explosion()
    {
        Debug.Log("Boom!!");
        var explosionObj = data.projectiles[1];
        if(explosionObj)
        {
            var go = Instantiate(explosionObj, transform.position, Quaternion.identity);
            go.GetComponent<GranadeExplosionField>().Init(data);
            Destroy(gameObject);
        }
    }
}
