using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Temperature : MonoBehaviour
{
	#region Properties
	private float currentTemperature;
	public float CurrentTemperature
	{
		get
		{
			return currentTemperature;
		}
		set
		{
			currentTemperature = value;
			UpdateTemperature();
		}
	}
	public float MaxTemperature {get;set;}
	public float MinTemperature {get;set;}
	#endregion

	#region Private
	[SerializeField]private Sprite[] heatLevel;
	[SerializeField]private GameObject temperatureTextObject;
	[SerializeField]private GameObject background;
	private Image thermImg, backgroundImage;
	private TextMeshProUGUI temperatureText;
	#endregion

	void Start ()
	{
		thermImg = GetComponent<Image>();
		currentTemperature = 20;
		MaxTemperature = 100 - (PlayerPrefs.GetInt("Difficulty", 2) * 20);// En mode facile, on peut monter jusqu'à 80° avant de perdre, et 40 en difficile
		MinTemperature = 20;
		temperatureText = temperatureTextObject.GetComponent<TextMeshProUGUI>();
		backgroundImage = background.GetComponent<Image>();
	}

	void UpdateTemperature()
	{
		int imageIndex = (int)(((currentTemperature - MinTemperature) / (MaxTemperature - MinTemperature)) * heatLevel.Length);

		if (currentTemperature >= MaxTemperature)
			thermImg.sprite = heatLevel[heatLevel.Length - 1];
		else
			thermImg.sprite = heatLevel[imageIndex];
		temperatureText.text = currentTemperature.ToString() + "°C";
		StopCoroutine("SmoothColorChange");//Interrompts les anciennes transitions de couleurs

		//Recommence une transition en fixant une nouvelle couleur cible, et en reprenant la couleur en cours de transition si une coroutine avait lieu
		StartCoroutine("SmoothColorChange", Color.Lerp(Color.white, Color.red,((currentTemperature - MinTemperature) / (MaxTemperature - MinTemperature)) / 2f));//Calcul permettant d'avoir 0% de rouge a la température Min, et 50% de rouge à la température Max
		if (currentTemperature >= MaxTemperature)
			SceneManager.LoadScene("DefeatScene");
	}

	IEnumerator SmoothColorChange(Color targetColor)
	{
		float colorPercent = 0f;
		Color startColor = backgroundImage.color;

		while (colorPercent < 1f)
		{
			colorPercent += 0.033f;
			backgroundImage.color = Color.Lerp(startColor, targetColor, colorPercent);
			yield return new WaitForEndOfFrame();
		}
	}
}
