using IngameSkill;
using UnityEngine;

public class GranadeExplosionField : MonoBehaviour
{
    SkillData data;
    float timer;

    public void Init(SkillData data)
    {
        this.data = data;
        transform.localScale = Vector3.one * data.baseRange;
    }

    private void Update()
    {
        if (timer < data.baseDuration)
            timer += Time.deltaTime;
        else
        {
            Destroy(gameObject);
        }
    }
}
