using UnityEngine;

[CreateAssetMenu(fileName = "Planet_Info", menuName = "Scriptable Objects/Planet Info")]
public class Planet_Info : ScriptableObject
{
	public Vector3[] positionsCartesian;
	public Vector2[] positionsLatLon;
	public float[] AngleSun;
}
