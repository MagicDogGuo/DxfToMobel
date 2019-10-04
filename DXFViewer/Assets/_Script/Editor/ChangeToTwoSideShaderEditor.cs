using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChangeToTwoSideShader))]
public class ChangeToTwoSideShaderEditor : Editor {

    ChangeToTwoSideShader Instance;

    private void OnEnable()
    {
        Instance = (ChangeToTwoSideShader)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("toMat"),new GUIContent("要更換的材質"));

        GUILayout.Space(30);
        if (GUILayout.Button("更換shader"))
        {
            Instance.ChangeShader();
        }
        serializedObject.ApplyModifiedProperties();

    }
}
