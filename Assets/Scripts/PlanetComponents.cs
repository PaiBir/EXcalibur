using Unity.VisualScripting;
using UnityEngine;

namespace PlanetComponents
{
	//Allows the creation of intersecting golden rectangles with minimum difficulty
	public class GoldenRectangle
	{
		public GoldenRectangle()
		{
		}

		public enum Direction { x, y, z }

		//Creates a given rectangle based on the goleden ratio. Directions chosen to ensure proper configuration for Icosphere
		public Vector3[] CreateRectangle(Direction dir)
		{
			Vector3[] rectanglepoints = new Vector3[4];
			switch (dir)
			{
				case Direction.x:
					rectanglepoints[0] = new Vector3(1, 89f / 55f, 0).normalized / 2f;
					rectanglepoints[1] = new Vector3(-1, 89f / 55f, 0).normalized / 2f;
					rectanglepoints[2] = new Vector3(1, -89f / 55f, 0).normalized / 2f;
					rectanglepoints[3] = new Vector3(-1, -89f / 55f, 0).normalized / 2f;
					break;
				case Direction.y:
					rectanglepoints[0] = new Vector3(0, 1, 89f / 55f).normalized / 2f;
					rectanglepoints[1] = new Vector3(0, -1, 89f / 55f).normalized / 2f;
					rectanglepoints[2] = new Vector3(0, 1, -89f / 55f).normalized / 2f;
					rectanglepoints[3] = new Vector3(0, -1, -89f / 55f).normalized / 2f;
					break;
				case Direction.z:
					rectanglepoints[0] = new Vector3(89f / 55f, 0, 1).normalized / 2f;
					rectanglepoints[1] = new Vector3(89f / 55f, 0, -1).normalized / 2f;
					rectanglepoints[2] = new Vector3(-89f / 55f, 0, 1).normalized / 2f;
					rectanglepoints[3] = new Vector3(-89f / 55f, 0, -1).normalized / 2f;
					break;
				default:
					rectanglepoints[0] = new Vector3(0, 0, 0);
					rectanglepoints[1] = new Vector3(0, 0, 0);
					rectanglepoints[2] = new Vector3(0, 0, 0);
					rectanglepoints[3] = new Vector3(0, 0, 0);
					break;
			}
			return rectanglepoints;
		}

	}

	//Allows conversion between various technical components. Not to be used for scientific values
	public class Converter
	{
		public Converter()
		{
		}

		//SPHERICAL COORDINATES
		/*
		 * Spherical Coordinates define points in 3D space based off of
		 * r: distance from orgin
		 * theta: angle vertically from origin
		 * psi: angle horizontally from origin
		 * 
		 * For our purposes, r does not matter, as our display model can be
		 * scaled as necessary after the fact with more control
		 * 
		 * XYZ is redefined as XZY, where Y is height, and X & Z are the 2D plane
		 */
		public Vector2[] ConvertCartesiantoSpherical(Vector3[] points)
		{
			Vector2[] sPoints = new Vector2[points.Length];
			for (int i = 0; i < points.Length; i++)
			{
				Vector2 sPoint = Vector2.zero;
				//Assign theta
				if (points[i].y > 0)
				{
					sPoint.x = Mathf.Atan(Mathf.Sqrt((points[i].x * points[i].x) + (points[i].z * points[i].z)) / points[i].y);
				} else if (points[i].y < 0)
				{
					sPoint.x = Mathf.PI + Mathf.Atan(Mathf.Sqrt((points[i].x * points[i].x) + (points[i].z * points[i].z)) / points[i].y);
				} else if (points[i].y == 0 && Mathf.Sqrt((points[i].x * points[i].x) + (points[i].z * points[i].z)) != 0)
				{
					sPoint.x = Mathf.PI / 2;
				} else
				{
					sPoint.x = 0f;
				}

				//Assign psi
				if (points[i].x > 0)
				{
					sPoint.y = Mathf.Atan(points[i].z / points[i].x);
				} else if (points[i].x < 0 && points[i].z >= 0)
				{
					sPoint.y = Mathf.Atan(points[i].z / points[i].x) + Mathf.PI;
				} else if (points[i].x < 0 && points[i].z < 0)
				{
					sPoint.y = Mathf.Atan(points[i].z / points[i].x) - Mathf.PI;
				} else if (points[i].x == 0 && points[i].z > 0)
				{
					sPoint.y = Mathf.PI / 2f;
				} else if (points[i].x == 0 && points[i].z < 0)
				{
					sPoint.y = -Mathf.PI / 2f;
				} else
				{
					sPoint.y = 0;
				}

				/*
				 * sPoints[i] = new Vector2(
					Mathf.Acos(points[i].y/Mathf.Sqrt((points[i].x* points[i].x)+(points[i].z* points[i].z)+(points[i].y* points[i].y))),
					(points[i].z == 0 ? 0f : (Mathf.Abs(points[i].z) / points[i].z)) * Mathf.Acos(points[i].x/Mathf.Sqrt((points[i].x * points[i].x) + (points[i].z * points[i].z))));
				*/
				sPoints[i] = sPoint;
			}
			return sPoints;
		}

		public Vector3[] ConvertSphericaltoCartesian(Vector2[] sPoints)
		{
			Vector3[] points = new Vector3[sPoints.Length];
			for (int i = 0; i < sPoints.Length; i++)
			{
				points[i] = new Vector3(
					Mathf.Sin(sPoints[i].x) * Mathf.Cos(sPoints[i].y),
					Mathf.Cos(sPoints[i].x),
					Mathf.Sin(sPoints[i].x) * Mathf.Sin(sPoints[i].y)
					
					);
			}
			return points;
		}
	}

	public class UVHandler
	{
		public UVHandler()
		{
		}
	}
}
