﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PathCreator))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class SlashCreator : MonoBehaviour {

    [Range(.05f, 1.5f)]
    public float spacing = 1;
    public float slashWidth = 1;
    public bool autoUpdate;
    public float tiling = 1;

    [SerializeField] Vector2[] points;
    [SerializeField] int[] branches;

    private void OnValidate()
    {
        if(autoUpdate)
        {
            UpdateSlash();
        }
    }

    public void UpdateSlash()
    {
        Path path = GetComponent<PathCreator>().path;
        points = path.CalculateEvenlySpacedPoints(spacing);
        GetComponent<MeshFilter>().mesh = CreateSlashMesh(points, path.IsClosed);

        int textureRepeat = Mathf.RoundToInt(tiling * points.Length * spacing * .05f);
        GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(1, textureRepeat);
    }

    Mesh CreateSlashMesh(Vector2[] points, bool isClosed)
    {
        Vector3[] verts = new Vector3[points.Length * 2];
        Vector2[] uvs = new Vector2[verts.Length];
        int numTris = 2 * (points.Length - 1) + ((isClosed) ? 2 : 0);
        int[] tris = new int[numTris * 3];
        int vertIndex = 0;
        int triIndex = 0;

        for (int i = 0; i < points.Length; i++)
        {
            Vector2 forward = Vector2.zero;
            if (i < points.Length - 1 || isClosed)
            {
                forward += points[(i + 1)%points.Length] - points[i];
            }
            if (i > 0 || isClosed)
            {
                forward += points[i] - points[(i - 1 + points.Length)%points.Length];
            }

            forward.Normalize();
            Vector2 left = new Vector2(-forward.y, forward.x);

            verts[vertIndex] = points[i] + left * slashWidth * .5f;
            verts[vertIndex + 1] = points[i] - left * slashWidth * .5f;

            float completionPercent = i / (float)(points.Length - 1);
            float v = 1 - Mathf.Abs(2 * completionPercent - 1);
            uvs[vertIndex] = new Vector2(0, v);
            uvs[vertIndex + 1] = new Vector2(1, v);

            if (i < points.Length - 1 || isClosed)
            {
				tris[triIndex] = vertIndex;
                tris[triIndex + 1] = (vertIndex + 2) % verts.Length;
				tris[triIndex + 2] = vertIndex + 1;

				tris[triIndex + 3] = vertIndex + 1;
                tris[triIndex + 4] = (vertIndex + 2) % verts.Length;
                tris[triIndex + 5] = (vertIndex + 3)  % verts.Length;
            }

            vertIndex += 2;
            triIndex += 6;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.uv = uvs;

        return mesh;
    }

    public List<Vector2> GetPoints()
    {
        return points.ToList();
    }

    public List<int> GetBranches()
    {
        return branches.ToList();
    }
}
