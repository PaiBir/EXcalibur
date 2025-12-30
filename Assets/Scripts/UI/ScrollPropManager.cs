using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollPropManager : MonoBehaviour
{
	public UI_Manager manager;
	public Property_Display.ActiveState type;
	public Property_Display.InfluenceVariable variable;
	public string PropName;
	TMP_InputField Input;
	TMP_Dropdown Dropdown;
	Button Button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.GetChild(0).GetComponent<TMP_Text>().text = PropName;
		Input = GetComponentInChildren<TMP_InputField>(true);
		Dropdown = GetComponentInChildren<TMP_Dropdown>(true);
		Button = GetComponentInChildren<Button>(true);
		switch (type)
		{
			case Property_Display.ActiveState.IntInput:
				Input.gameObject.SetActive(true);
				Dropdown.gameObject.SetActive(false);
				Button.gameObject.SetActive(false);
				Input.contentType = TMP_InputField.ContentType.IntegerNumber;
				Input.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "Enter whole number...";
				Input.onValueChanged.AddListener(SetFromInputField);
				break;
			case Property_Display.ActiveState.FloatInput:
				Input.gameObject.SetActive(true);
				Dropdown.gameObject.SetActive(false);
				Button.gameObject.SetActive(false);
				Input.contentType = TMP_InputField.ContentType.DecimalNumber;
				Input.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "Enter number...";
				Input.onValueChanged.AddListener(SetFromInputField);
				break;
			case Property_Display.ActiveState.TextInput:
				Input.gameObject.SetActive(true);
				Dropdown.gameObject.SetActive(false);
				Button.gameObject.SetActive(false);
				Input.contentType = TMP_InputField.ContentType.Alphanumeric;
				Input.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "Enter text...";
				Input.onValueChanged.AddListener(SetFromInputField);
				break;
			case Property_Display.ActiveState.Dropdown:
				Input.gameObject.SetActive(false);
				Dropdown.gameObject.SetActive(true);
				Button.gameObject.SetActive(false);
				Dropdown.onValueChanged.AddListener(SetFromDropdown);
				break;
			case Property_Display.ActiveState.Button:
				Input.gameObject.SetActive(false);
				Dropdown.gameObject.SetActive(false);
				Button.gameObject.SetActive(true);
				Button.onClick.AddListener(SetFromButton);
				break;
			case Property_Display.ActiveState.Multibutton:
				Input.gameObject.SetActive(false);
				Dropdown.gameObject.SetActive(false);
				Button.gameObject.SetActive(true);
				break;
			default:
				break;
		}
		//To be customized per variable. Could probably be done simpler, but this works for now
		switch (variable)
		{
			case Property_Display.InfluenceVariable.StarRadius:
				Input.text = manager.starMass.ToString();
				break;
			case Property_Display.InfluenceVariable.StarLuminosity:
				Input.text = manager.starLum.ToString();
				break;
			case Property_Display.InfluenceVariable.StarTemperature:
				Input.text = manager.starTemp.ToString();
				break;
			case Property_Display.InfluenceVariable.PlanetDistance:
				Input.text = manager.distance.ToString();
				break;
			case Property_Display.InfluenceVariable.PlanetResolution:
				Input.text = manager.resolution.ToString();
				break;
			default:
				break;
		}
	}

	public void SetFromInputField(string input)
	{
		Debug.Log(input);
		switch (variable)
		{
			case Property_Display.InfluenceVariable.StarRadius:
				manager.starMass = float.Parse(input);
				break;
			case Property_Display.InfluenceVariable.StarLuminosity:
				manager.starLum = float.Parse(input);
				break;
			case Property_Display.InfluenceVariable.StarTemperature:
				manager.starTemp = int.Parse(input);
				break;
			case Property_Display.InfluenceVariable.PlanetDistance:
				manager.distance = float.Parse(input);
				break;
			case Property_Display.InfluenceVariable.PlanetResolution:
				int InputVal = int.Parse(input);
				if(InputVal < 0)
				{
					Input.text = "0";
					manager.resolution = InputVal;
				} else if(InputVal > manager.Boss.planet_admin.Subdivisions.Length-1)
				{
					Input.text = (manager.Boss.planet_admin.Subdivisions.Length - 1).ToString();
					manager.resolution = InputVal;
				} else
				{
					manager.resolution = InputVal;
				}
					break;
			default:
				break;
		}
	}

	public void SetFromDropdown(int index)
	{
		switch (variable)
		{
			case Property_Display.InfluenceVariable.StarRadius:
				break;
			case Property_Display.InfluenceVariable.StarLuminosity:
				break;
			case Property_Display.InfluenceVariable.StarTemperature:
				break;
			case Property_Display.InfluenceVariable.PlanetDistance:
				break;
			case Property_Display.InfluenceVariable.PlanetResolution:
				break;
			default:
				break;
		}
	}

	public void SetFromButton()
	{
		switch (variable)
		{
			case Property_Display.InfluenceVariable.StarRadius:
				break;
			case Property_Display.InfluenceVariable.StarLuminosity:
				break;
			case Property_Display.InfluenceVariable.StarTemperature:
				break;
			case Property_Display.InfluenceVariable.PlanetDistance:
				break;
			case Property_Display.InfluenceVariable.PlanetResolution:
				break;
			default:
				break;
		}
	}

	// Update is called once per frame
	/*void Update()
	{
	}*/
}
