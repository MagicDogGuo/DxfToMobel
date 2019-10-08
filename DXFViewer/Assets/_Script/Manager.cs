using UnityEngine;
using System.Collections;
using Loader;
using System.Collections.Generic;
using System.IO;

public class Manager : MonoBehaviour
{
    public GoView GoView;
    GenerateMesh[] generateMeshes;


    private void LoadDXF(string path)
    {
        try
        {
            DiskFile iLoader = new DiskFile(path);
            DXFConvert.DXFStructure dxfStructure = new DXFConvert.DXFStructure(iLoader);
            dxfStructure.Load();
            iLoader.Dispose();
            GoView.Set(dxfStructure);
            Debug.Log("OK:" + path);
        }
        catch (System.Exception ex)
        {
            Debug.Log("Error:" + path);
            Debug.LogError(ex.ToString());
        }

    }

    string path = "";

    public void OnGUI()
    {
        GUI.Label(new Rect(10, 25, 100, 20), "DXF文件地址");
        path = GUI.TextField(new Rect(10, 50, 300, 20), path);

        if (GUI.Button(new Rect(10, 75, 75, 20), "载入文件"))
        {
            LoadDXF(path);
        }

        if (GUI.Button(new Rect(10, 100, 75, 20), "测试文件1"))
        {
            LoadDXF(Application.dataPath + "/TestData/Test1.dxf");
        }
        if (GUI.Button(new Rect(90, 100, 75, 20), "测试文件2"))
        {
            LoadDXF(Application.dataPath + "/TestData/Test2.dxf");
        }
        if (GUI.Button(new Rect(170, 100, 75, 20), "测试文件3"))
        {
            LoadDXF(Application.dataPath + "/TestData/Test3.dxf");
        }
        if (GUI.Button(new Rect(250, 100, 75, 20), "测试文件4"))
        {
            LoadDXF(Application.dataPath + "/TestData/Test4.dxf");
        }
    }

    /// <summary>
    /// 載入DXF
    /// </summary>
    public void Generate2DLine()
    {   
     
        LoadDXF(Application.dataPath + "/TestData/Test1.dxf");
        
    }

    /// <summary>
    /// 找到所有的GenerateMesh 
    /// </summary>
    public void FindAllGenerateMesh()
    {
        if (FindObjectsOfType<GenerateMesh>().Length != 0)
        {
            generateMeshes = FindObjectsOfType<GenerateMesh>();
        }
        else
        {
            Debug.LogWarning("========沒有載入2D DXF!==========");
        }
    }


    /// <summary>
    /// 產生3D模型
    /// </summary>
    public void Generate3DMesh()
    {
        if (generateMeshes != null)
        {
            foreach (var item in generateMeshes)
            {
                item.Generate3DMesh();
            }
        }
        else
        {
            Debug.LogWarning("==========沒有產生3D模型!==========");

        }

    }

    /// <summary>
    /// 轉換成Obj
    /// </summary>
    public void OutputToObj()
    {
        if (generateMeshes != null)
        {
            foreach (var item in generateMeshes)
            {
                item.OutputToObj();
            }
        }
        else
        {
            Debug.LogWarning("==========沒有產生3D模型!==========");
        }
    }
}
