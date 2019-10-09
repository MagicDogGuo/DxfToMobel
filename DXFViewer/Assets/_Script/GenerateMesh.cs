using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class GenerateMesh : MonoBehaviour
{
    [SerializeField]
    Vector3 LineStartPosition;
    [SerializeField]
    Vector3 LineEndPosition;

    float wallHeight = 1500f;


    static Color color = new Color(255, 0, 0, 255);

    Vector3[] vertices = new Vector3[4];
    Color[] colors = { color, color, color, color };
    Vector2[] uvs = new Vector2[4];
    int[] tris = { 2 , 1, 0, 1, 2, 3 };

    GoLine goLines;
    LineRenderer lineRenderer;

    void Start()
    {
        goLines = GetComponent<GoLine>();
        //lineRenderer = GetComponent<LineRenderer>();
        //lineRenderer.enabled = false;
        float heightWeight = Vector3.Distance(goLines.PositionStart, goLines.PositionEnd) / wallHeight  ;
        //Debug.Log("==============="+heightWeight);
        uvs[0] = new Vector2(0, heightWeight);
        uvs[1] = new Vector2(0, 0);
        uvs[2] = new Vector2(1, heightWeight);
        uvs[3] = new Vector2(1, 0);


    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.G)){
            Generate3DMesh();
        }

        if (Input.GetKey(KeyCode.F))
        {
            OutputToObj();
        }

    }


    /// <summary>
    /// 產生3D模型
    /// </summary>
    public void Generate3DMesh()
    {
        Debug.Log(goLines.PositionStart + "==" + goLines.PositionEnd);

        LineStartPosition = goLines.PositionStart;
        LineEndPosition = goLines.PositionEnd;


        //座標 X 或 Y 一定要從少變多(為了讓UV方向一致)
        if (LineStartPosition.x > LineEndPosition.x)
        {
            float tempX = LineStartPosition.x;
            LineStartPosition.x = LineEndPosition.x;
            LineEndPosition.x = tempX;
        }
        if (LineStartPosition.y > LineEndPosition.y)
        {
            float tempY = LineStartPosition.y;
            LineStartPosition.y = LineEndPosition.y;
            LineEndPosition.y = tempY;
        }


        Vector3 Vertex1Position = new Vector3(LineStartPosition.x, 0, LineStartPosition.y);
        Vector3 Vertex2Position = new Vector3(LineEndPosition.x, 0, LineEndPosition.y);
        Vector3 Vertex3Position = new Vector3(LineStartPosition.x, wallHeight, LineStartPosition.y);
        Vector3 Vertex4Position = new Vector3(LineEndPosition.x, wallHeight, LineEndPosition.y);


        GameObject gameObject = new GameObject("Quad");
        gameObject.transform.SetParent(transform, false);
        var mf = gameObject.AddComponent<MeshFilter>();
        var mr = gameObject.AddComponent<MeshRenderer>();


        vertices[0] = Vertex1Position;
        vertices[1] = Vertex2Position;
        vertices[2] = Vertex3Position;
        vertices[3] = Vertex4Position;

        Material mat = new Material(Shader.Find("Standard(twoSide)"));//

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.colors = colors;
        mesh.uv = uvs;
        mesh.triangles = tris;
        mf.mesh = mesh;

        mat.color = Color.white;

        mr.material = mat;

    }

    /// <summary>
    /// 轉換成Obj
    /// </summary>
    public void OutputToObj()
    {
        Debug.Log(MeshToString(this.GetComponentInChildren<MeshFilter>(), Vector3.one));

        using (StreamWriter streamWriter = new StreamWriter(string.Format("{0}{1}.obj", Application.dataPath + "/_Obj/", this.gameObject.name)))
        {
            streamWriter.Write(MeshToString(this.GetComponentInChildren<MeshFilter>(), new Vector3(-1f, 1f, 1f)));
            streamWriter.Close();
        }
        AssetDatabase.Refresh();
    }



    /// <summary>
    /// 轉換成obj
    /// </summary>
    /// <param name="mf"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    private string MeshToString(MeshFilter mf, Vector3 scale)
    {
        Mesh mesh = mf.mesh;
        Material[] sharedMaterials = mf.GetComponent<Renderer>().sharedMaterials;
        Vector2 textureOffset = mf.GetComponent<Renderer>().material.GetTextureOffset("_MainTex");
        Vector2 textureScale = mf.GetComponent<Renderer>().material.GetTextureScale("_MainTex");

        StringBuilder stringBuilder = new StringBuilder().Append("mtllib design.mtl")
            .Append("\n")
            .Append("g ")
            .Append(mf.name)
            .Append("\n");

        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vector = vertices[i];
            stringBuilder.Append(string.Format("v {0} {1} {2}\n", vector.x * scale.x, vector.y * scale.y, vector.z * scale.z));
        }

        stringBuilder.Append("\n");

        Dictionary<int, int> dictionary = new Dictionary<int, int>();

        if (mesh.subMeshCount > 1)
        {
            int[] triangles = mesh.GetTriangles(1);

            for (int j = 0; j < triangles.Length; j += 3)
            {
                if (!dictionary.ContainsKey(triangles[j]))
                {
                    dictionary.Add(triangles[j], 1);
                }

                if (!dictionary.ContainsKey(triangles[j + 1]))
                {
                    dictionary.Add(triangles[j + 1], 1);
                }

                if (!dictionary.ContainsKey(triangles[j + 2]))
                {
                    dictionary.Add(triangles[j + 2], 1);
                }
            }
        }

        for (int num = 0; num != mesh.uv.Length; num++)
        {
            Vector2 vector2 = Vector2.Scale(mesh.uv[num], textureScale) + textureOffset;

            if (dictionary.ContainsKey(num))
            {
                stringBuilder.Append(string.Format("vt {0} {1}\n", mesh.uv[num].x, mesh.uv[num].y));
            }
            else
            {
                stringBuilder.Append(string.Format("vt {0} {1}\n", vector2.x, vector2.y));
            }
        }

        for (int k = 0; k < mesh.subMeshCount; k++)
        {
            stringBuilder.Append("\n");

            if (k == 0)
            {
                stringBuilder.Append("usemtl ").Append("Material_design").Append("\n");
            }

            if (k == 1)
            {
                stringBuilder.Append("usemtl ").Append("Material_logo").Append("\n");
            }

            int[] triangles2 = mesh.GetTriangles(k);

            for (int l = 0; l < triangles2.Length; l += 3)
            {
                stringBuilder.Append(string.Format("f {0}/{0} {1}/{1} {2}/{2}\n", triangles2[l] + 1, triangles2[l + 2] + 1, triangles2[l + 1] + 1));
            }
        }

        return stringBuilder.ToString();
    }


}
