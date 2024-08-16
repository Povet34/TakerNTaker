using System.Collections.Generic;
using UnityEngine;

public interface IHitdetection
{
    Collider2D collider { get; set; }
    Collider2D CreateCollider();
    Rect GetBounds(List<Vector2> points);
}
