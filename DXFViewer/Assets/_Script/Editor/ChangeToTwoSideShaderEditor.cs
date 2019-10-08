using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChangeToTwoSideShader))]
public class ChangeToTwoSideShaderEditor : Editor {

    ChangeToTwoSideShader Instance;
    bool IsOpenOriginInspector = false;
    private void OnEnable()
    {
        Instance = (ChangeToTwoSideShader)target;
    }

    public override void OnInspectorGUI()
    {
        IsOpenOriginInspector = GUILayout.Toggle(IsOpenOriginInspector, new GUIContent("開啟原始面板"));
        if (IsOpenOriginInspector)
        {
            base.OnInspectorGUI();

            GUILayout.Space(20f);
        }

        GUILayout.Label("需要輸出Obj之後才能使用",EditorStyles.boldLabel);
        GUILayout.Space(20f);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("toMat"),new GUIContent("要更換的材質"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ObjParent"), new GUIContent("Obj母物件"));

        GUILayout.Space(30);

        GUILayout.Label("01");
        if (GUILayout.Button(" 載入obj", GUILayout.Height(50)))
        {
            Instance.LoadObjInScene();
        }
        GUILayout.Space(30);

        GUILayout.Label("02");
        if (GUILayout.Button("更換shader", GUILayout.Height(50)))
        {
            Instance.ChangeShader();
        }
        GUILayout.Space(30);

        GUILayout.Label("03");
        if (GUILayout.Button("輸出prefab", GUILayout.Height(50)))
        {
            Instance.OutputToPerfab();
        }

        serializedObject.ApplyModifiedProperties();

    }
}
