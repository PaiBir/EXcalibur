using System;
using UnityEngine;

public class SunManager : MonoBehaviour
{
	public double starMass = 1.0f; //1 Sol worth of mass
	public double starLum = 1.0f; //1 Sol worth of luminosity
	public double starRadius = 1.0f;
	[Range(1500f,15000f)]
	public double starTemp = 5770; //Temperature of our sun
	public Color starColor = new Color();
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.localScale = new Vector3((float)starRadius * 10, (float)starRadius * 10, (float)starRadius * 10);
		if( starColor != TemperatureToColor(starTemp))
		{
			starColor = TemperatureToColor(starTemp);

			Mesh starm = GetComponent<MeshFilter>().mesh;

			Color[] StarColors = new Color[starm.vertexCount];

			for (int i = 0; i < StarColors.Length; i++)
			{
				StarColors[i] = starColor;
			}
			starm.colors = StarColors;
			GetComponent<MeshFilter>().mesh = starm;
		}
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
