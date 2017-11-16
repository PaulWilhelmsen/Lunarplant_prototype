using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SolarStorm))]
public class SolarStormEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SolarStorm myScript = (SolarStorm)target;
        if(GUILayout.Button("Invoke Solarstorm"))
        {
            myScript.InvokeSolarStorm();
        }
    }
}
