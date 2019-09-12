using System.Collections;
using System.Collections.Generic;
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
    Vector2[] uvs = { new Vector2(0, 1), new Vector2(0, 0), new Vector2(1, 1), new Vector2(1, 0) };
    int[] tris = { 2 , 1, 0, 1, 2, 3 };

    GoLine goLines;
    LineRenderer lineRenderer;
    void Start()
    {
        goLines = GetComponent<GoLine>();
        //lineRenderer = GetComponent<LineRenderer>();
        //lineRenderer.enabled = false;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.G)){
           
            Debug.Log(goLines.PositionStart+"=="+ goLines.PositionEnd);

            LineStartPosition = goLines.PositionStart;
            LineEndPosition = goLines.PositionEnd;
            
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

            Material mat = new Material(Shader.Find("Standard(twoSide)"));

            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.colors = colors;
            mesh.uv = uvs;
            mesh.triangles = tris;
            mf.mesh = mesh;

            mat.color = Color.white;

            mr.material = mat;

        }
    }
}
