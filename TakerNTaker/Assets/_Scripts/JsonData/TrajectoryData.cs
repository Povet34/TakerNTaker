using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrajectoryData
{
    public List<TrajectoryPosition> positions = new List<TrajectoryPosition>();
    public List<int> branches = new List<int>();

    public List<Vector2> GetPositions()
    {
        var list = new List<Vector2>();
        for(int i = 0; i < positions.Count; i++)
        {
            list.Add(positions[i].GetData());
        }

        return list;
    }

    public List<int> GetBranches() 
    {
        return branches;
    }
}

[System.Serializable]
public class TrajectoryPosition
{
    public float x;
    public float y;

    public TrajectoryPosition(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public Vector2 GetData()
    {
        return new Vector2(x, y);
    }
}