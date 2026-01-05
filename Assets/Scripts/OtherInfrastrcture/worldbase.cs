using System;
using UnityEngine;

public class worldbase : MonoBehaviour
{
	[Serializable]
	public struct Vector2d //Probably move this out to a more general thing at some point
	{
		public double x;
		public double y;

		public Vector2d(double _x, double _y)
		{
			x = _x;
			y = _y;
		}

		public Vector2d Lerp(Vector2d point, double t)
		{
			return new Vector2d(
				((point.x - x) * t) + x,
				((point.y - y) * t) + y
				);
		}
	}
	[Serializable]
	public struct bezierSegment //Probably move this out to a more general thing at some point
	{
		public Vector2d pointa;
		public Vector2d pointb;
		public Vector2d pointc;
		public Vector2d pointd;

		public double xBound1;
		public double xBound2;

		public bezierSegment(Vector2d a, Vector2d b, Vector2d c, Vector2d d)
		{
			pointa = a;
			pointb = b;
			pointc = c;
			pointd = d;
			xBound1 = pointa.x;
			xBound2 = pointd.x;
		}

		public bezierSegment(double a_x, double a_y, double b_x, double b_y, double c_x, double c_y, double d_x, double d_y)
		{
			pointa = new Vector2d(a_x,a_y);
			pointb = new Vector2d(b_x, b_y);
			pointc = new Vector2d(c_x, c_y);
			pointd = new Vector2d(d_x, d_y);
			xBound1 = pointa.x;
			xBound2 = pointd.x;
		}

		public double SampleCurve(double X)
		{
			double SamplePoint = (X - xBound1) / (xBound2 - xBound1);

			Vector2d Lerp1 = pointa.Lerp(pointb,SamplePoint);
			Vector2d Lerp2 = pointb.Lerp(pointc,SamplePoint);
			Vector2d Lerp3 = pointc.Lerp(pointd,SamplePoint);
			Vector2d Lerp4 = Lerp1.Lerp(Lerp2,SamplePoint);
			Vector2d Lerp5 = Lerp2.Lerp(Lerp3,SamplePoint);
			return Lerp4.Lerp(Lerp5,SamplePoint).y;
		}
	}

	bezierSegment[] BolometricCorrectionBezier = //V1?
	{
		new bezierSegment(0.18,-4.2,0.18,-2.9164,0.38453,0,1.27,0),
		new bezierSegment(1.27,0,2.3729,0,4.9383,-1.5,4.8,-1.5)
	};

	[Header("Star Properties")]
	public double starMass = 1.0f; //1 Sol worth of mass
	public double starLum = 1.0f; //1 Sol worth of luminosity
	public double starTemp = 5770; //Temperature of our sun
	public double starDiameter = 1.0f; //1 Sol worth of diameter
	public double starAbsMagnitude = 0.0f;
	public double bolometricCorrectionFactor = 0.0f;
	public Color starColor;
	public Light realLight; //For game
	[Header("Planet Properties")]
	public double distance = 1.0f; //1 AU away
	public double orbitspeed = 1.0f; //24 hr/day
	public int resolution = 1; //planet subdivisions
	public GameObject PlanetBase; //Put in prefab to use
	public PlanetHolder planet_admin;
	[Header("UI")]
	public UI_Manager canvas; //Connect to UI
	public Camera inUseCam; //Position camera

	GameObject PlanetObject;
	PlanetManager planetManager;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		canvas.Boss = this;
		PlanetObject = Instantiate(PlanetBase);
		PlanetObject.transform.parent = transform;
		planetManager = PlanetObject.GetComponent<PlanetManager>();
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
		inUseCam.transform.position = new Vector3((float)distance * 10 + 5, 0, 5);
		inUseCam.transform.LookAt(new Vector3((float)distance * 10 - 5, 0, 0));
		PlanetObject.transform.position = new Vector3((float)distance *10, 0, 0);
		PlanetObject.GetComponent<PlanetManager>().subdivLevel = resolution;
		PlanetObject.GetComponent<PlanetManager>().orbitspeed = orbitspeed;

		//Star Characteristics
		realLight.intensity = (float)starLum;
		realLight.color = starColor;
		starColor = TemperatureToColor(starTemp);
		double bolometricValue = 0;
		foreach (bezierSegment bSeg in BolometricCorrectionBezier)
		{
			if(starMass <= bSeg.xBound2 && starMass >= bSeg.xBound1)
			{
				bolometricValue = bSeg.SampleCurve(starMass);
				break;
			}
		}
		double bolometricLuminocity = starLum/Math.Pow(2.52,bolometricValue*bolometricCorrectionFactor);
		starDiameter = 5770.0 / starTemp * Math.Sqrt(bolometricLuminocity);
		starAbsMagnitude = Math.Log(bolometricLuminocity, 2.52) + 4.85;
	}

	//Based on Mitchell Charity's function as reported by Dan Bruton (https://web.archive.org/web/20241106151814/https://www.physics.sfasu.edu/astro/color/blackbody.html)
	Color TemperatureToColor(double temperature)
	{
		Color reColor = new Color();
		reColor.r = Mathf.Max(Mathf.Min((56100000f * Mathf.Pow((float)temperature, -3f / 2f) + 148f) / 255f, 1.0f), 0.0f);
		reColor.g = Mathf.Max(Mathf.Min((100.04f * Mathf.Log((float)temperature) - 623.6f) / 255f, 1.0f), 0.0f);
		reColor.b = Mathf.Max(Mathf.Min((194.18f * Mathf.Log((float)temperature) - 1448.6f) / 255f, 1.0f), 0.0f);
		if (temperature > 6500)
		{
			reColor.g = Mathf.Max(Mathf.Min((35200000 * Mathf.Pow((float)temperature, -3f / 2f) + 148f) / 255f, 1.0f), 0.0f);
		}
		return reColor;
	}
}
