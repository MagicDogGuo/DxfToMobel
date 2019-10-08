using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChangeToTwoSideShader : MonoBehaviour {

    [SerializeField]
    Material toMat;

    [SerializeField]
    Transform ObjParent;

    /// <summary>
    /// 載入obj
    /// </summary>
    public void LoadObjInScene()
    {
        Object[] obj = Resources.LoadAll("_Obj");
        foreach (var item in obj)
        {
            Instantiate(item, ObjParent);
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
        
        string path = string.Format("{0}{1}.prefab", "Assets/_Prefab/", ObjParent.name);
        Debug.Log(path);
        PrefabUtility.CreatePrefab(path, ObjParent.gameObject);
        AssetDatabase.Refresh();
    }
}
