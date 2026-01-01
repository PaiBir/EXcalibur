using UnityEngine;

public class UI_Manager : MonoBehaviour
{
	public Color PrimaryColor = Color.white;
	public Color FadeTo = Color.clear;
	public int activeScreen;
	int currentScreen = -1;

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
