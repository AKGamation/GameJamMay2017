using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof(TextureCreater))]
public class texuter_creator_inspector : Editor {

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        DrawDefaultInspector();
        if (EditorGUI.EndChangeCheck() && Application.isPlaying)
        {
            (target as TextureCreater).FillTexture();
        }
    }
}
