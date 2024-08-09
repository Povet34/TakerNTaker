using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] Transform center;
    [SerializeField] float fov = 90f;
    [SerializeField] float viewDistance = 3;
    
    Mesh mesh;
    float startAngle;
    int rayCount = 50;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void UpdateSight(Vector3 parentPos)
    {
        Vector3 origin = parentPos;
        center.position = parentPos;

        float angle = startAngle;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;

        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            var hit = Physics2D.Raycast(origin, UtilsClass.GetVectorFromAngle(angle), viewDistance, 1 << LayerMask.NameToLayer("Object"));
            if (hit.collider == null)
            {
                vertex = origin + UtilsClass.GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                vertex = hit.point;
            }

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }
            vertexIndex++;

            angle -= angleIncrease;
        }

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
    }

    public void SetAimDirection(Vector3 aimDir)   
    {
        startAngle = UtilsClass.GetAngleFromVector(aimDir) + fov / 2f;
    }
}
