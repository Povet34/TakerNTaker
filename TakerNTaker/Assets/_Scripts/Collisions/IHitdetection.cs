using System.Collections.Generic;
using UnityEngine;

public interface IHitdetection
{
    Collider2D hitBox { get; set; }
    Collider2D CreateCollider();
}
