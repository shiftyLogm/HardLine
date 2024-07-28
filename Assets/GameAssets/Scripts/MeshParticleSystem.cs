using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshParticleSystem : MonoBehaviour
{
    private const int MAX_QUAD_AMOUNT = 15000;

    private Mesh _mesh;

    private Vector3[] _vertices;
    private Vector2[] _uv;
    private int[] _triangles;

    private int _quadIndex;

    private void Awake()
    {
        _mesh = new Mesh();

        _vertices = new Vector3[4 * MAX_QUAD_AMOUNT];
        _uv = new Vector2[4 * MAX_QUAD_AMOUNT];
        _triangles = new int[6 * MAX_QUAD_AMOUNT];

        AddQuad(new Vector3(0,0));
        AddQuad(new Vector3(0, 1));

        GetComponent<MeshFilter>().mesh = _mesh;
    }

    private void AddQuad(Vector3 position)
    {
        if(_quadIndex >= MAX_QUAD_AMOUNT) return; // Max quad length

        // Relocate vertices
        int vIndex = _quadIndex * 4;
        int vIndex0 = vIndex;
        int vIndex1 = vIndex + 1;
        int vIndex2 = vIndex + 2;
        int vIndex3 = vIndex + 3;

        Vector3 quadSize = new Vector3(0.03f, 0.03f);
        float rotation = 0f;
        _vertices[vIndex0] = position + Quaternion.Euler(0, 0, rotation - 180) * quadSize;
        _vertices[vIndex1] = position + Quaternion.Euler(0, 0, rotation - 270) * quadSize;
        _vertices[vIndex2] = position + Quaternion.Euler(0, 0, rotation - 0) * quadSize;
        _vertices[vIndex3] = position + Quaternion.Euler(0, 0, rotation - 90) * quadSize;

        // UV
        _uv[vIndex0] = new Vector2(0,0);
        _uv[vIndex1] = new Vector2(0, 1);
        _uv[vIndex2] = new Vector2(1, 1);
        _uv[vIndex3] = new Vector2(1, 0);

        // Create triangles
        int tIndex = _quadIndex * 6;

        _triangles[tIndex + 0] = vIndex0;
        _triangles[tIndex + 1] = vIndex1;
        _triangles[tIndex + 2] = vIndex2;

        _triangles[tIndex + 3] = vIndex0;
        _triangles[tIndex + 4] = vIndex2;
        _triangles[tIndex + 5] = vIndex3; 

        _mesh.vertices = _vertices;
        _mesh.uv = _uv;
        _mesh.triangles = _triangles;

        _quadIndex++;
    }
}
