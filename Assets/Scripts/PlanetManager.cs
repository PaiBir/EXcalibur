using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
	[Header("Planet Properties")]
	public Planet_Info planet_prop;
	[Header("Sphere Source")]
	public PlanetHolder planet_admin;
	[Header("Sphere Properties")]
	[Range(1,6)] //Only icosphere meshes exist, and there are 6 of them
	public int subdivLevel = 0;
	public Material mat;

	int prevSubdiv = -1;
	Mesh currentMesh = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.AddComponent<MeshFilter>();
		this.AddComponent<MeshRenderer>();
		planet_prop = new Planet_Info();
    }

    // Update is called once per frame
    void Update()
    {
		if (prevSubdiv != subdivLevel)
		{
			GetComponent<MeshRenderer>().material = mat;

			Vector3[] verts = planet_admin.Subdivisions[subdivLevel - 1].vertices;
			Vector3[] norms = new Vector3[verts.Length];
			Color[] cols = new Color[verts.Length];
			int[] tris = planet_admin.Subdivisions[subdivLevel - 1].triangles;
			System.Array.Reverse(tris);
			for (int i = 0; i < verts.Length; i++)
			{
				verts[i] = new Vector3(verts[i].x, verts[i].z, verts[i].y);
				norms[i] = verts[i].normalized;
				cols[i] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
			}
			currentMesh = new Mesh();
			currentMesh.vertices = verts.ToArray();
			currentMesh.triangles = tris;
			currentMesh.normals = norms.ToArray();
			currentMesh.colors = cols.ToArray();
			prevSubdiv = subdivLevel;
		}
		GetComponent<MeshFilter>().mesh = currentMesh;
    }
}
