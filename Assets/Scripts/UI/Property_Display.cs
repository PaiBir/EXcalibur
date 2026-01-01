using System.Collections.Generic;
using UnityEngine;

public class Property_Display : MonoBehaviour
{
	public enum ActiveState
	{
		IntInput,
		FloatInput,
		TextInput,
		Dropdown,
		Button,
		Multibutton
	}
	public enum InfluenceVariable
	{
		StarRadius,
		StarLuminosity,
		StarTemperature,
		PlanetDistance,
		PlanetResolution,
		OrbitSpeed
	}
	[System.Serializable]
	public struct PropGuide
	{
		public ActiveState Input;
		public InfluenceVariable Target;
		public int[] MultibuttonChannels;
		public string Name;
	}
	[System.Serializable]
	public struct DisplayScreen
	{
		public string Name;
		public int[] propindicies;
	}
	public PropGuide[] Properties;
	public DisplayScreen[] screens;
	public GameObject propBlock;
	public RectTransform scrollbar;
	public RectTransform proparea;
	float upperbound = 0;
	float lowerbound = 0;
	public float lerping = 0;
	UI_Manager manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = transform.parent.parent.GetComponent<UI_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
		
		RectTransform propspace = transform.GetChild(0).GetComponent<RectTransform>();
		proparea.sizeDelta = new Vector2(propspace.rect.width - 20, (proparea.transform.childCount)*60);
		upperbound = -proparea.rect.height / 2;
		if (propspace.rect.height < proparea.rect.height)
		{
			lowerbound = (proparea.rect.height - propspace.rect.height) - (proparea.rect.height/2);
		}
		else
		{
			lowerbound = upperbound;
		}
		proparea.anchoredPosition = new Vector2(0,Mathf.Lerp(upperbound,lowerbound,lerping));
    }

	public void setLerp(float value)
	{
		lerping = value;
	}

	public void ClearDisplay()
	{
		GameObject scroller = transform.GetChild(0).GetChild(1).gameObject;
		int NumChildren = scroller.transform.childCount;

		for (int i = 0; i < NumChildren; i++)
		{
			Destroy(scroller.transform.GetChild(NumChildren-i-1).gameObject);
		}
	}

	public void PopulateDisplay(int display)
	{
		if (display < 0 || display >= screens.Length)
		{
			Debug.LogError("Display value outside range of screens! (" + display + ", " + screens.Length + ")");
			return;
		}
		GameObject scroller = transform.GetChild(0).GetChild(1).gameObject;
		for (int i = 0; i < screens[display].propindicies.Length; i++)
		{
			GameObject currentProp = Instantiate(propBlock);
			currentProp.transform.SetParent(scroller.transform, false);
			RectTransform rc = currentProp.GetComponent<RectTransform>();
			rc.anchoredPosition = new Vector2(0, -(rc.sizeDelta.y + 10) * i);
			ScrollPropManager propmanager =  currentProp.GetComponent<ScrollPropManager>();
			propmanager.manager = manager;
			propmanager.PropName = Properties[screens[display].propindicies[i]].Name;
			currentProp.name = Properties[screens[display].propindicies[i]].Name;
			propmanager.type = Properties[screens[display].propindicies[i]].Input;
			propmanager.variable = Properties[screens[display].propindicies[i]].Target;
		}
	}
}
