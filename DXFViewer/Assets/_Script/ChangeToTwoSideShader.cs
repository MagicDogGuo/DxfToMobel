using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToTwoSideShader : MonoBehaviour {

    [SerializeField]
    Material toMat;

    public void ChangeShader()
    {
        Material mat = toMat;//new Material(Shader.Find("Standard(twoSide)"));

        MeshRenderer[] meshs = GameObject.FindObjectsOfType<MeshRenderer>() as MeshRenderer[];
        foreach (var item in meshs)
        {
            item.material = mat;
        }
    }
}
