using UnityEngine;

public class worldbase : MonoBehaviour
{
	[Header("Star Properties")]
	public float starMass = 1.0f; //1 Sol worth of mass
	public float starLum = 1.0f; //1 Sol worth of luminosity
	public float starTemp = 5776; //Temperature of our sun
	public GameObject StarBase;
	public Light realLight; //For game
	[Header("Planet Properties")]
	public float distance = 1.0f; //1 AU away
	public float orbitspeed = 1.0f; //24 hr/day
	public int resolution = 1; //planet subdivisions
	public GameObject PlanetBase; //Put in prefab to use
	public PlanetHolder planet_admin;
	[Header("UI")]
	public UI_Manager canvas; //Connect to UI
	public Camera inUseCam; //Position camera

	GameObject starObject;
	SunManager starManager;
	GameObject PlanetObject;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		canvas.Boss = this;
		PlanetObject = Instantiate(PlanetBase);
		starObject = Instantiate(StarBase);
		PlanetObject.transform.parent = transform;
		starObject.transform.parent = transform;
		starManager = starObject.GetComponent<SunManager>();
	}

	// Update is called once per frame
	void Update()
	{

		if(planet_admin == null)
		{
			planet_admin = PlanetObject.GetComponent<PlanetManager>().planet_admin;
		}
		if(resolution > planet_admin.Subdivisions.Length)
		{
			resolution = planet_admin.Subdivisions.Length;
		}
		if(resolution < 1)
		{
			resolution = 1;
		}
		inUseCam.transform.position = new Vector3(distance * 10 + 5, 0, 5);
		inUseCam.transform.LookAt(new Vector3(distance * 10 - 5, 0, 0));
		PlanetObject.transform.position = new Vector3(distance*10, 0, 0);
		PlanetObject.GetComponent<PlanetManager>().subdivLevel = resolution;
		PlanetObject.GetComponent<PlanetManager>().orbitspeed = orbitspeed;
		realLight.intensity = starLum;
		realLight.color = starManager.starColor;
		
		if(starMass != starManager.starMass)
		{
			starManager.starMass = starMass;
		}
		if (starLum != starManager.starLum)
		{
			starManager.starLum = starLum;
		}
		if (starTemp != starManager.starTemp)
		{
			if (starTemp < 1)
			{
				starTemp = 1;
			}
			starManager.starTemp = starTemp;
		}
	}
}
