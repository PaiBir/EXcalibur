using UnityEngine;

public class UI_Manager : MonoBehaviour
{
	public Color PrimaryColor = Color.white;
	public Color FadeTo = Color.clear;
	public int activeScreen;
	int currentScreen = -1;

	//STAR PROPS
	public float starMass = 1.0f; //1 Sol worth of mass
	public float starLum = 1.0f; //1 Sol worth of luminosity
	public float starTemp = 5776; //Temperature of our sun

	//PLANET PROPS
	public float distance = 1.0f; //1 AU away
	public int resolution = 1; //planet subdivisions

	public worldbase Boss;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (activeScreen != currentScreen)
		{
			currentScreen = activeScreen;
			transform.GetChild(0).GetChild(0).GetComponent<Property_Display>().ClearDisplay();
			transform.GetChild(0).GetChild(0).GetComponent<Property_Display>().PopulateDisplay(activeScreen);
		}
    }
}
