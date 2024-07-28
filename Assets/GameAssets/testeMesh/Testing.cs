using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private void Start()
    {
        // =-=-=-=-=-= Fazendo um poligono -=-=-=-=-=-
        // Mesh mesh = new Mesh();

        // Vector3[] vertices = new Vector3[3];
        // Vector2[] uv = new Vector2[3]; // Quantidade de uvs é a mesma quantidade de vertices
        // int[] triangles = new int[3];

        // // Posição dos vertices
        // vertices[0] = new Vector3(0,0);
        // vertices[1] = new Vector3(0, 1);
        // vertices[2] = new Vector3(1, 1);

        // // É a mesma posição dos vertices, mas normalizado
        // uv[0] = new Vector2(0,0);
        // uv[1] = new Vector2(0, 1);
        // uv[2] = new Vector2(1, 1);

        // // Ordem das "linhas do triangulo", sempre em sentido horario, assim fazendo com que a face da frente fique para frente
        // triangles[0] = 0;
        // triangles[1] = 1;
        // triangles[2] = 2;

        // mesh.vertices = vertices;
        // mesh.uv = uv;
        // mesh.triangles = triangles;

        // GetComponent<MeshFilter>().mesh = mesh;

        // -=-=-=--= Fazendo um Quad -=-=-=-=-=
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4]; // Quantidade de uvs é a mesma quantidade de vertices
        int[] triangles = new int[6];

        // Posição dos vertices
        vertices[0] = new Vector3(0,0);
        vertices[1] = new Vector3(0, 1);
        vertices[2] = new Vector3(1, 1);
        vertices[3] = new Vector3(1, 0);

        // É a mesma posição dos vertices, mas normalizado
        uv[0] = new Vector2(0,0);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 1);
        uv[3] = new Vector2(1, 0);

        // Ordem das "linhas do triangulo", sempre em sentido horario, assim fazendo com que a face da frente fique para frente
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        GetComponent<MeshFilter>().mesh = mesh;
    }
}
