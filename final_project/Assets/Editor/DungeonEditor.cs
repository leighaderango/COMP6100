using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DungeonGeneration), true)]
public class DungeonEditor : Editor
{
    DungeonGeneration generator;

    private void Awake()
    {
        generator = (DungeonGeneration)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Create Dungeon"))
        {
            generator.GenerateDungeon();
        }

        if (GUILayout.Button("Clear Map"))
        {
            generator.Reset();
        }
    }
}
    
