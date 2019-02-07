using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	#region Properties
	public float ScreenWidth {get;set;}
	public float ScreenHeight {get;set;}
	#endregion
	
	#region Public
	public static GameManager gameManagerInstance;
	[HideInInspector]public IslandSpawner islandSpawner;
	[HideInInspector]public Temperature temperature;
	#endregion

	#region Private
	[SerializeField]private Temperature Thermometer;
	private Vector2 screenSize;
	private float score;
	#endregion

	void Awake()
	{
		MakeSingleton();
		screenSize = (Vector2)(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)) - Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)));
		ScreenWidth = screenSize.x;
		ScreenHeight = screenSize.y;
	}

	void MakeSingleton()
	{
	 	if (gameManagerInstance == null)
			gameManagerInstance = this;
		else
			Destroy(gameManagerInstance);
	}

	void Start ()
	{
		islandSpawner = GetComponent<IslandSpawner>();
		temperature = Thermometer.GetComponent<Temperature>();
	}
	
	void Update ()
	{
		score += Time.deltaTime;
	}

	void OnDestroy()
	{
		PlayerPrefs.SetInt("CurrentScore", (int)score);
		if ((int)score > PlayerPrefs.GetInt("HighScore", 0))
			PlayerPrefs.SetInt("HighScore", (int)score);
	}
}
