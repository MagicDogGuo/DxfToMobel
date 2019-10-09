using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ChangeToTwoSideShader : MonoBehaviour {

    [SerializeField]
    Material toMat;

    [SerializeField]
    string parentGoName;

    GameObject parentGo;

    /// <summary>
    /// 載入obj
    /// </summary>
    public void LoadObjInScene()
    {
        parentGo = new GameObject();
        parentGo.name = parentGoName;

        string[] sdFiles = Directory.GetFiles(Application.dataPath + "/_Obj/", "*.obj", SearchOption.AllDirectories);

        foreach (var item in sdFiles)
        {
            string assetPath = "Assets" + item.Replace(Application.dataPath, ""); //.Replace('\\', '/');

            GameObject go = (GameObject)AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject));

            Instantiate(go, parentGo.transform);
        }
    }

    /// <summary>
    /// 更換材質
    /// </summary>
    public void ChangeShader()
    {
        Material mat = toMat;//new Material(Shader.Find("Standard(twoSide)"));

        MeshRenderer[] meshs = GameObject.FindObjectsOfType<MeshRenderer>() as MeshRenderer[];
        foreach (var item in meshs)
        {
            item.material = mat;
        }
    }

    /// <summary>
    /// 輸出成prefab
    /// </summary>
    public void OutputToPerfab()
    {
        string path = string.Format("{0}{1}.prefab", "Assets/_Prefab/", parentGoName);
        Debug.Log(path);
        PrefabUtility.CreatePrefab(path, GameObject.Find(parentGoName));
        AssetDatabase.Refresh();
    }
}
