using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(ClassAttackData))]
public class ClassAttackDataEditor : Editor
{
    ClassAttackData attackData;
    string fileName = "";

    void OnEnable()
    {
        attackData = (ClassAttackData)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        fileName = GUILayout.TextField(fileName, 25);

        if (GUILayout.Button("Get Points"))
        {
            string path = Application.dataPath + $"/_Data/Points/{fileName}.json";

            string json = File.ReadAllText(path);

            if(!string.IsNullOrEmpty(json)) 
            {
                var data = JsonUtility.FromJson<TrajectoryData>(json);
                if (null != data)
                {
                    attackData.SetTrajectories(data.GetPositions());
                    attackData.SetBranches(data.GetBranches());
                }
            }
        }
    }
}

#endif