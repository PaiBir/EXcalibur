using UnityEngine;

[CreateAssetMenu(fileName = "New Planet Model", menuName = "Scriptable Objects/Planet Models")]
public class PlanetHolder : ScriptableObject
{
	[SerializeField]
	public Mesh[] Subdivisions = new Mesh[1];

}
