using UnityEngine;

public class worldbase : MonoBehaviour
{
	[Header("Star Properties")]
	public float starLum = 1.0f; //1 Earth sun worth of luminosity
	[Header("Planet Properties")]
	public float distance = 1.0f; //1 AU away
	public GameObject PlanetBase; //Put in prefab to use
	[Header("UI")]
	public Canvas canvas; //Connect to UI
	public Camera inUseCam; //Position camera
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
