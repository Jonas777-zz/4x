using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour {

    Mesh hexMesh;
    List<Vector3> verts;
    List<int> tris;
    List<Color> colors;

    MeshCollider meshCollider;

    void Awake()
    {
        GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
        meshCollider = gameObject.AddComponent<MeshCollider>();
        hexMesh.name = "Hex Mesh";
        verts = new List<Vector3>();
        tris = new List<int>();
        colors = new List<Color>();
    }

    public void Triangulate(Cell[] cells)
    {
        hexMesh.Clear();
        verts.Clear();
        tris.Clear();
        colors.Clear();

        for (int i = 0; i < cells.Length; i++)
        {
            Triangulate(cells[i]);
        }

        hexMesh.vertices = verts.ToArray();
        hexMesh.triangles = tris.ToArray();
        hexMesh.colors = colors.ToArray();
        hexMesh.RecalculateNormals();

        meshCollider.sharedMesh = hexMesh;
    }

    void Triangulate(Cell cell)
    {
        Vector3 center = cell.transform.localPosition;
        for (int i = 0; i < 6; i++)
        {
            AddTriangle(
                center,
                center + HexUtilities.corners[i],
                center + HexUtilities.corners[i == 5 ? 0 : i + 1]
                );
            AddTriangleColor(cell.color);
        }
    }

    void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        int vertInd = verts.Count;
        verts.Add(v1);
        verts.Add(v2);
        verts.Add(v3);
        tris.Add(vertInd);
        tris.Add(vertInd + 1);
        tris.Add(vertInd + 2);
    }

    void AddTriangleColor(Color color)
    {
        colors.Add(color);
        colors.Add(color);
        colors.Add(color);
    }
}
