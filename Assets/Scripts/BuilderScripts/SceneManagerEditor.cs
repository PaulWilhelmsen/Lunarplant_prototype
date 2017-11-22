using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneManager))]
public class SceneManagerEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SceneManager myScript = (SceneManager)target;
        if (GUILayout.Button("Plant on all tiles"))
        {
            myScript.WaterandSeedEverything();
        }
        if (GUILayout.Button("Water all tiles"))
        {
            myScript.WaterAllTiles();
        }
    }
}