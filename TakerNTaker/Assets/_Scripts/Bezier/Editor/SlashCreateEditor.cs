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
            var wrapper = new Vector2ToFloat2WrapperList();
            if(null != list)
            {
                for (int i = 0; i < list.Count; i++) 
                {
                    wrapper.positions.Add(new Vector2ToFloat2Wrapper(list[i].x, list[i].y));
                }
            }

            var json = JsonUtility.ToJson(wrapper, true);
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