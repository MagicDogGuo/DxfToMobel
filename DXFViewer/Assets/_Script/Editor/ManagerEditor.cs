using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Manager))]
public class ManagerEditor : Editor {

    Manager Instance;
    bool IsOpenOriginInspector = false;

    private void OnEnable()
    {
        Instance = (Manager)target;
    }

    public override void OnInspectorGUI()
    {
        IsOpenOriginInspector = GUILayout.Toggle(IsOpenOriginInspector, new GUIContent("開啟原始面板"));
        if (IsOpenOriginInspector)
        {
            base.OnInspectorGUI();

            GUILayout.Space(20f);
        }

        GUILayout.Label("====以下需要在play模式才能使用====", EditorStyles.boldLabel);
        GUILayout.Space(20f);


        GUILayout.Label("步驟1");
        if (GUILayout.Button("載入DXF",GUILayout.Height(50)))
        {
            Instance.Generate2DLine();
        }
        GUILayout.Space(30);

        GUILayout.Label("步驟2");
        if (GUILayout.Button("找到所有的GenerateMesh", GUILayout.Height(50)))
        {
            Instance.FindAllGenerateMesh();
        }
        GUILayout.Space(30);

        GUILayout.Label("步驟3");
        if (GUILayout.Button("產生3D模型", GUILayout.Height(50)))
        {
            Instance.Generate3DMesh();
        }
        GUILayout.Space(30);

        GUILayout.Label("步驟4");
        if (GUILayout.Button("轉換成Obj", GUILayout.Height(50)))
        {
            Instance.OutputToObj();
        }
    }
}
