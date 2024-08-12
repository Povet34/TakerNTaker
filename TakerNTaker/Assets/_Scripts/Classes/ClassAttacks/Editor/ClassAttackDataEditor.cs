using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(ClassAttackData))]
public class ClassAttackDataEditor : Editor
{
    ClassAttackData data;
    string fileName = "";

    void OnEnable()
    {
        data = (ClassAttackData)target;
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
                var points = JsonUtility.FromJson<Vector2ToFloat2WrapperList>(json).GetData();

                data.SetTrajectories(points);
            }
        }
    }
}

#endif