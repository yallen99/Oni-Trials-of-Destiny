using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

[System.Serializable]
public class DayColors
{
	public Color skyColor;
	public Color equatorColor;
	public Color horizonColor;
}

public class DayNightControl : MonoBehaviour
{
	public bool startDay; 

	[Header("Links")]
	public Light directionalLight;
	public GameObject starDome;
	public GameObject moonState;
	public GameObject moon;
	public GameObject[] nightLight;
	
	
	[Header("Colors")]
	public DayColors dawnColors;
	public DayColors dayColors;
	public DayColors nightColors;
	

	public float secondsInAFullDay = 120f; 

	[Range(0, 1)]
	public float currentTime = 0;

	[HideInInspector]
	public float timeMultiplier = 1f; 
	float _lightIntensity; 
	Material _starMat;
	Camera _targetCam;

	
	void Start()
	{
		foreach (Camera c in GameObject.FindObjectsOfType<Camera>())
		{
			if (c.isActiveAndEnabled)
			{
				_targetCam = c;
			}
		}

		_lightIntensity = directionalLight.intensity; 
		_starMat = starDome.GetComponentInChildren<MeshRenderer>().material;
		if (startDay)
		{
			currentTime = 0.3f; //start at morning
			_starMat.color = new Color(1f, 1f, 1f, 0f);
		}
	}


	void Update()
	{
		UpdateLight();
		if (IsNight())
		{
			foreach (var lightSource in nightLight)
			{
				lightSource.SetActive(true);
			}
		}
		else
		{
			foreach (var light in nightLight)
			{
				light.SetActive(false);
			}
		}
		
		currentTime += (Time.deltaTime / secondsInAFullDay) * timeMultiplier;
		if (currentTime >= 1)
		{
			currentTime = 0; 
		}
	}

	void UpdateLight()
	{
		starDome.transform.Rotate(new Vector3(-2f * Time.deltaTime, 0f, 0));
		moon.transform.LookAt(_targetCam.transform);
		directionalLight.transform.localRotation = Quaternion.Euler((currentTime * 360f) - 90, 170, 0);
		moonState.transform.localRotation = Quaternion.Euler((currentTime * 360f) - 100, 170, 0);
	
		float intensityMultiplier = 1;

		if (currentTime <= 0.23f || currentTime >= 0.75f)
		{
			intensityMultiplier =
				0; 
			_starMat.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, Time.deltaTime));
		}
		else if (currentTime <= 0.25f)
		{
			intensityMultiplier = Mathf.Clamp01((currentTime - 0.23f) * (1 / 0.02f));
			_starMat.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, Time.deltaTime));
		}
		else if (currentTime <= 0.73f)
		{
			intensityMultiplier = Mathf.Clamp01(1 - ((currentTime - 0.73f) * (1 / 0.02f)));
		}


	

		if (currentTime <= 0.2f)
		{
			RenderSettings.ambientSkyColor = nightColors.skyColor;
			RenderSettings.ambientEquatorColor = nightColors.equatorColor;
			RenderSettings.ambientGroundColor = nightColors.horizonColor;
		}

		if (currentTime > 0.2f && currentTime < 0.4f)
		{
			RenderSettings.ambientSkyColor = dawnColors.skyColor;
			RenderSettings.ambientEquatorColor = dawnColors.equatorColor;
			RenderSettings.ambientGroundColor = dawnColors.horizonColor;
		}

		if (currentTime > 0.4f && currentTime < 0.75f)
		{
			RenderSettings.ambientSkyColor = dayColors.skyColor;
			RenderSettings.ambientEquatorColor = dayColors.equatorColor;
			RenderSettings.ambientGroundColor = dayColors.horizonColor;
		}

		if (currentTime > 0.75f)
		{
			RenderSettings.ambientSkyColor = dayColors.skyColor;
			RenderSettings.ambientEquatorColor = dayColors.equatorColor;
			RenderSettings.ambientGroundColor = dayColors.horizonColor;
		}

		directionalLight.intensity = _lightIntensity * intensityMultiplier;
	}
	
	public bool IsNight ()
	{
		if (currentTime >= 0f && currentTime < 0.3f ||
		    currentTime > 0.7 && currentTime < 1f)
		{
			return true;
		}

		return false;
	}
}


