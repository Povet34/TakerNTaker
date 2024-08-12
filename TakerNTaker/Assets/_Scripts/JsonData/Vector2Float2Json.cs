using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Vector2ToFloat2WrapperList
{
    public List<Vector2ToFloat2Wrapper> positions = new List<Vector2ToFloat2Wrapper>();

    public List<Vector2> GetData()
    {
        var list = new List<Vector2>();
        for(int i = 0; i < positions.Count; i++)
        {
            list.Add(positions[i].GetData());
        }

        return list;
    }
}

[System.Serializable]
public class Vector2ToFloat2Wrapper
{
    public float x;
    public float y;

    public Vector2ToFloat2Wrapper(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public Vector2 GetData()
    {
        return new Vector2(x, y);
    }
}