using UnityEngine;

public class LaserHitEffect : MonoBehaviour
{
    public void Init(float duration)
    {
        Invoke(nameof(Destroy), duration);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
