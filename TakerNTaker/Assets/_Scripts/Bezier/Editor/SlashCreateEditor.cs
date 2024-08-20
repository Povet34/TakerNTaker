using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(SlashCreator))]
public class SlashCreateEditor : Editor
{
    SlashCreator creator;
    string saveName = "Points";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        saveName = GUILayout.TextField(saveName, 25);

        if (GUILayout.Button("Save Points"))
        {
            var list = creator.GetPoints();
            var data = new TrajectoryData();
            if(null != list)
            {
                for (int i = 0; i < list.Count; i++) 
                {
                    data.positions.Add(new TrajectoryPosition(list[i].x, list[i].y));
                }
                data.branches = creator.GetBranches();
            }

            var json = JsonUtility.ToJson(data, true);
            string path = Application.dataPath + $"/_Data/Points/{saveName}.json";

            File.WriteAllText(path, json);
        }
    }
    void OnEnable()
    {
        creator = (SlashCreator)target;
    }
}

#endif