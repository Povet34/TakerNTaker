using UnityEngine;

public struct DamageMsg
{
    public GameObject owner;
    public float amount;
    public Vector2 direction;
}

public interface IDamagable
{
    void TakeDamage(DamageMsg msg);
}
