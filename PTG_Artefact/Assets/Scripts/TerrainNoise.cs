using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainNoise : MonoBehaviour
{
    // Reference to a mesh and the currect objects transform
    public Mesh mesh;
    public GameObject settlements;
    public Transform terrain;
    
    // The size and heights of the Perlin noise map
    float scale = 10f;
    float height = 2.5f;

    public Vector3[] vertexHeight;

    // Use this for initialization
    void Start()
    {
        // Access the mesh within a mesh filter. Suggested by Unity devs when working with procedural interfaces
        mesh = GetComponent<MeshFilter>().mesh;

        // An array to store each of the positions of the vertices from the current mesh
        vertexHeight = mesh.vertices;

        // Loop through each of the vertices in the mesh and use the PerlinNoise function to generate new positions for the mesh vertices.
        // Adding on the terrains current x and y positions means that the terrain will join seamlessly after new ones have been generated (Hopefully)        
        for (int i = 0; i < vertexHeight.Length; i++)
        {
            vertexHeight[i].y = Mathf.PerlinNoise((vertexHeight[i].x + terrain.position.x) / scale, (vertexHeight[i].z + terrain.position.z) / scale) * height;
                   
        }
        mesh.vertices = vertexHeight; // Place the newly positioned vertices back into the objects mesh
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        this.gameObject.AddComponent<MeshCollider>(); // No idea. It only works like this for some reason
    }

}
