using UnityEngine;

public class LaserHitEffect : MonoBehaviour
{
    public struct Data
    {
        public float duration;
        public float scale;
    }

    public void Init(Data data)
    {
        transform.localScale = data.scale * Vector3.one;
        Invoke(nameof(Destroy), data.duration);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
