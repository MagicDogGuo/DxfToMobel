using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToTwoSideShader : MonoBehaviour {



	void Start () {
        Material mat = new Material(Shader.Find("Standard(twoSide)"));

        MeshRenderer[] meshs = GameObject.FindObjectsOfType<MeshRenderer>() as MeshRenderer[];
        foreach (var item in meshs)
        {
            item.material = mat;
        }
    }

}
