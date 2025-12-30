using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//THIS SCRIPT IS DECPRECIATED FOR NOW
//Ideally, this will be updated to work, however it is currently broken, and I cannot fix it at this moment
//Feel free to take a stab at it though
public class WorldBase1 : MonoBehaviour
{
	PlanetComponents.GoldenRectangle rectangleBase = new();
	PlanetComponents.Converter converter = new();
	public bool UpdateMesh;
	public int subdivisions;
	public Material mat;
	public Vector2[] seeingEye2;
	public int[] seeingEye3;
	public float precision = 0.0001f;

	//Vector2Mod
	Vector2 V2mod(Vector2 target, Vector2 modulo, Vector2 LoopsNegative)
	{
		Vector2 output = Vector2.zero;

		if (LoopsNegative.x == 1f)
		{
			output.x = ((target.x + modulo.x) % (modulo.x * 2f)) - modulo.x;
		} else
		{
			output.x = target.x % modulo.x;
		}
		if (LoopsNegative.y == 1f)
		{
			output.y = ((target.y + modulo.y) % (modulo.y * 2f)) - modulo.y;
		} else
		{
			output.y = target.y % modulo.y;
		}
		//Debug.Log("Modulo Vector2: Input: " + target + ", Output: " + output + ", Modulo conditions: " + modulo + ", Looping: " + LoopsNegative);
		return output;
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent<MeshRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		if (UpdateMesh)
		{
			UpdateMesh = false;
			//StartCoroutine(MeshMaker());
		}
	}

	IEnumerator<WaitForSeconds> MeshMaker()
	{
		//Create Mesh
		Mesh mesh = new Mesh();
		//Assign material
		GetComponent<MeshRenderer>().material = mat;
		//Grab core verticies for generating an icosphere
		Vector3[] baseVerts1 = rectangleBase.CreateRectangle(PlanetComponents.GoldenRectangle.Direction.x);
		Vector3[] baseVerts2 = rectangleBase.CreateRectangle(PlanetComponents.GoldenRectangle.Direction.y);
		Vector3[] baseVerts3 = rectangleBase.CreateRectangle(PlanetComponents.GoldenRectangle.Direction.z);
		//Write verticies to a list
		List<Vector3> baseVerts = baseVerts1.ToList();
		baseVerts.AddRange(baseVerts2);
		baseVerts.AddRange(baseVerts3);

		//Rotate the verticies to ensure verticies at the poles. Creates iconic D20 shape.
		for (int i = 0; i < baseVerts.Count; i++)
		{
			baseVerts[i] = Quaternion.AngleAxis(30, Vector3.forward) * baseVerts[i];
		}

		//Create spherical coordinates (without distance) for points
		Vector2[] ToPolar = converter.ConvertCartesiantoSpherical(baseVerts.ToArray());

		//Setup arrays for verticies and faces. Verticies are in spherical to allow for perfect distribution. Faces don't need to be.
		Vector2[] activeVerts = new Vector2[(10 * (subdivisions * subdivisions)) + (20 * subdivisions) + 12];
		int[] activeFaces = new int[3 * ((20 * (subdivisions * subdivisions)) + (40 * subdivisions) + 20)];
		Vector3[] cartesianVerts = new Vector3[activeVerts.Length];
		Vector3[] meshNorms = new Vector3[cartesianVerts.Length];
		Color[] vertcols = new Color[cartesianVerts.Length];

		//Store each triangle as a vector3int for indicies. Each index points to a vertex in ToPolar. By wrapping them, theoretically generation should go a bit easier (fingers crossed).
		Vector3Int[] Extriangles = new Vector3Int[] {
				new Vector3Int(4, 1, 10),
				new Vector3Int(4, 0, 1),
				new Vector3Int(1, 0, 6),
				new Vector3Int(6, 0, 9),
				new Vector3Int(8, 9, 0),
				new Vector3Int(4, 8, 0),
				new Vector3Int(4, 5, 8),
				new Vector3Int(5, 4, 10),
				new Vector3Int(11, 10, 1),
				new Vector3Int(11, 1, 6),
				new Vector3Int(6, 7, 11),
				new Vector3Int(7, 6, 9),
				new Vector3Int(9, 2, 7),
				new Vector3Int(9, 8, 2),
				new Vector3Int(2, 8, 5),
				new Vector3Int(3, 2, 5),
				new Vector3Int(3, 5, 10),
				new Vector3Int(10, 11, 3),
				new Vector3Int(7, 3, 11),
				new Vector3Int(2, 3, 7)
			};

		//Begin generating subdivisions after this message.
		Debug.Log("Generating Subdivisions!");

		//fvC, or face vertex count, stores the current index to add verticies to the vertex array (faceVerts) for this face
		int fvC = 0;
		//numFaces stores how many faces have been generated, for iterating through the face indicies array
		int numFaces = 0;

		//Iterating through each triangle, pulling verticies, and creating new ones into the arrays declared earlier.
		for (int i = 0; i < Extriangles.Length; i++)
		{
			List<Vector2> liveVerts = new List<Vector2>();
			Vector3Int growTri = Extriangles[i];
			Vector2 rootVertex = ToPolar[growTri.x];
			Vector2 v2 = ToPolar[growTri.y];
			Vector2 v3 = ToPolar[growTri.z];

			Debug.Log("Starting on face " + i + "! Root: " + rootVertex + ", V2: " + v2 + ", V3: " + v3);

			//Repair faces that intersect with the switch between -PI and +PI 
			if (Mathf.Abs(v2.y - v3.y) > Mathf.PI)
			{
				if (v2.y > v3.y)
				{
					v3.y += Mathf.PI * 2;
				}
				else
				{
					v2.y += Mathf.PI * 2;
				}
			}
			else if (Mathf.Abs(rootVertex.y - v2.y) > Mathf.PI)
			{
				if (rootVertex.y > v2.y)
				{
					v2.y += Mathf.PI * 2;
				}
				else
				{
					rootVertex.y += Mathf.PI * 2;
				}
			}
			else if (Mathf.Abs(rootVertex.y - v3.y) > Mathf.PI)
			{
				if (rootVertex.y > v3.y)
				{
					v3.y += Mathf.PI * 2;
				}
				else
				{
					rootVertex.y += Mathf.PI * 2;
				}
			}

			//Compile directions
			Vector2 dir1 = (v2 - rootVertex) / (subdivisions + 1f);
			Vector2 dir2 = (v3 - rootVertex) / (subdivisions + 1f);

			//Address poles
			if (v3.x < precision)
			{
				dir2.y = 0f;
			}
			else if (v2.x < precision)
			{
				dir1.y = 0f;
			}

			//dir1.y = 0;

			Debug.Log("[" + i + "] Dir1: " + dir1 + ", Dir2: " + dir2);

				//Adds the root vertex for this icosohedron face to the array of vertecies to run through
				liveVerts.Add(rootVertex);
			for (int j = 0; j <= subdivisions; j++)
			{
				Debug.Log("[" + i + "] Beginning generation " + j + " iterations");
				Vector2[] thisGen = liveVerts.ToArray();
				liveVerts.Clear();
				for (int k = 0; k < thisGen.Length; k++)
				{
					//Store face verticies
					Vector2 NRoot = V2mod(thisGen[k], new Vector2(Mathf.PI, Mathf.PI), new Vector2(0, 1));
					Vector2 NDir1 = V2mod(thisGen[k] + dir1, new Vector2(Mathf.PI, Mathf.PI), new Vector2(0, 1));
					Vector2 NDir2 = V2mod(thisGen[k] + dir2, new Vector2(Mathf.PI, Mathf.PI), new Vector2(0, 1));
					//Repair if holes
					if(dir1.y == 0f)
					{
						NDir2 = V2mod(thisGen[k] + (dir2 * new Vector2(1, j + 1)), new Vector2(Mathf.PI, Mathf.PI), new Vector2(0, 1));
					}
					if(dir2.y == 0f)
					{
						NDir1 = V2mod(thisGen[k] + (dir1 * new Vector2(1, j + 1)), new Vector2(Mathf.PI, Mathf.PI), new Vector2(0, 1));
					}
					//Create cartesian versions
					Vector3 NRootC = new Vector3(
						Mathf.Sin(NRoot.x) * Mathf.Cos(NRoot.y),
						Mathf.Cos(NRoot.x),
						Mathf.Sin(NRoot.x) * Mathf.Sin(NRoot.y)
					);
					Vector3 NDir1C = new Vector3(
						Mathf.Sin(NDir1.x) * Mathf.Cos(NDir1.y),
						Mathf.Cos(NDir1.x),
						Mathf.Sin(NDir1.x) * Mathf.Sin(NDir1.y)
					);
					Vector3 NDir2C = new Vector3(
						Mathf.Sin(NDir2.x) * Mathf.Cos(NDir2.y),
						Mathf.Cos(NDir2.x),
						Mathf.Sin(NDir2.x) * Mathf.Sin(NDir2.y)
					);
					//Assign next generation
					if (thisGen[k] + dir1 != v2)
					{
						liveVerts.Add(thisGen[k] + dir1);
					}
					if (thisGen[k] + dir2 != v3)
					{
						liveVerts.Add(thisGen[k] + dir2);
					}

					//Run Checks
					bool IsRootIn = false;
					int Rindex = -1;
					bool IsDir1In = false;
					int D1index = -1;
					bool IsDir2In = false;
					int D2index = -1;
					for (int l = 0; l < activeVerts.Length; l++)
					{
						if (activeVerts[l] == Vector2.zero)
						{
							break;
						}
						if ((activeVerts[l] - NRoot).magnitude < precision)
						{
							IsRootIn = true;
							Rindex = l;
						}
						if ((activeVerts[l] - NDir1).magnitude < precision)
						{
							IsDir1In = true;
							D1index = l;
						}
						if ((activeVerts[l] - NDir2).magnitude < precision)
						{
							IsDir2In = true;
							D2index = l;
						}
					}

					//Create faces

					//If there is a prexisting vertex here, use it instead
					if (IsRootIn)
					{
						Debug.Log("[" + i +", " + j +"] Root vertex  " + NRoot + " already exists " + activeVerts[Rindex] + ". Index: " + Rindex);
						activeFaces[numFaces * 3] = Rindex;
					}
					else
					{
						activeVerts[fvC] = NRoot;
						cartesianVerts[fvC] = NRootC;
						meshNorms[fvC] = NRootC.normalized;
						vertcols[fvC] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
						Debug.Log("[" + i +", " + j +"] Root vertex " + NRoot + " does not exist. Creating new index: " + fvC);
						activeFaces[numFaces * 3] = fvC;
						fvC++;
					}
					//If there is a prexisting vertex here, use it instead
					if (IsDir1In)
					{
						Debug.Log("[" + i + ", " + j + "] Dir1 vertex " + NDir1 + " already exists " + activeVerts[D1index] + ". Index: " + D1index);
						activeFaces[(numFaces * 3) + 1] = D1index;
					}
					else
					{
						activeVerts[fvC] = NDir1;
						cartesianVerts[fvC] = NDir1C;
						meshNorms[fvC] = NDir1C.normalized;
						vertcols[fvC] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
						Debug.Log("[" + i + ", " + j + "] Dir1 vertex " + NDir1 + " does not exist. Creating new index: " + fvC);
						activeFaces[(numFaces * 3) + 1] = fvC;
						fvC++;
					}
					//If there is a prexisting vertex here, use it instead
					if (IsDir2In)
					{
						Debug.Log("[" + i + ", " + j + "] Dir2 vertex " + NDir2 + " already exists " + activeVerts[D2index] + ". Index: " + D2index);
						activeFaces[(numFaces * 3) + 2] = D2index;
					}
					else
					{
						Debug.Log("face vert count: " + fvC + ", array length: " + activeVerts.Length);
						activeVerts[fvC] = NDir2;
						cartesianVerts[fvC] = NDir2C;
						meshNorms[fvC] = NDir2C.normalized;
						vertcols[fvC] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
						Debug.Log("[" + i + ", " + j + "] Dir2 vertex " + NDir2 + " does not exist. Creating new index: " + fvC);
						activeFaces[(numFaces * 3) + 2] = fvC;
						fvC++;
					}
					numFaces++;

					//Fill holes
					if (k > 0)
					{
						activeFaces[numFaces * 3] = activeFaces[(numFaces - 1) * 3];
						activeFaces[numFaces * 3 + 2] = activeFaces[(numFaces - 1) * 3 + 1];
						activeFaces[numFaces * 3 + 1] = activeFaces[(numFaces - 2) * 3];
						numFaces++;
					}
					seeingEye2 = activeVerts;
					seeingEye3 = activeFaces;
					mesh.vertices = cartesianVerts;
					mesh.triangles = activeFaces.ToArray();
					mesh.normals = meshNorms;
					mesh.colors = vertcols;
					GetComponent<MeshFilter>().mesh = mesh;
					yield return new WaitForSeconds(1f);
				}
			}
		}

		//Calculate Colors
		Debug.Log("Generating Colors!");

		for (int i = 0; i < vertcols.Length; i++)
		{
			vertcols[i] = new Color(
				activeVerts[i].x / Mathf.PI,
				(activeVerts[i].y / (Mathf.PI * 2)) + .5f,
				0
			);
		}

		//GR rectangle testing
		/*int[] triangles = new int[] {
		0, 1, 2,
		2, 1, 3,

		4, 5, 6,
		6, 5, 7,

		8, 9, 10,
		10, 9, 11
		};*/

		//Base icosphere testing
		/*int[] triangles = new int[] {
			//Y+
			0, 1, 4,
			1, 0, 6,
			//Y-
			3, 2, 5,
			2, 3, 7,
			//Z+
			5, 4, 10,
			4, 5, 8,
			//Z-
			7, 6, 9,
			6, 7, 11,
			//X+
			8, 9, 0,
			9, 8, 2,
			//X-
			11, 10, 1,
			10, 11, 3,
			//Corners
			2, 8, 5,
			4, 8, 0,
			4, 1, 10,
			3, 5, 10,
			6, 0, 9,
			9, 2, 7,
			7, 3, 11,
			11, 1, 6
		};*/
		Debug.Log("Compiling Mesh!");
		mesh.vertices = cartesianVerts;
		mesh.triangles = activeFaces.ToArray();
		mesh.normals = meshNorms;
		mesh.colors = vertcols;
		Debug.Log("Assigning Mesh!");
		GetComponent<MeshFilter>().mesh = mesh;
		yield break;
	}
}