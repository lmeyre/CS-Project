using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSpawner : MonoBehaviour
{
	#region Properties
	public float IslandsSpeed {get;set;}
	#endregion

	#region Private
	[SerializeField]private GameObject[] islandsList;
	private float difficultyTimer, nextDifficultyCap;//Permet a la difficulté d'augmenter rapidemment au début, puis plus lentement par la suite
	private float delayBetweenIslands, currentDelay;
	private float randomDelay, maxRandomDelay;//Donne un léger délai aléatoire pour que l'apparition des iles soit irrégulière
	private Vector2 spawnPosition;
	#endregion

	void Start ()
	{
		float islandMaxHeight = 7f;
		spawnPosition = new Vector2(0, (GameManager.gameManagerInstance.ScreenHeight / 2 + islandMaxHeight) * -1);

		IslandsSpeed = 0.4f;	
		delayBetweenIslands = 15f;
		currentDelay = 0f;
		difficultyTimer = 0f;
		nextDifficultyCap = 15f;
		randomDelay = 0f;
		maxRandomDelay = 1f;
	}

	void FixedUpdate()
	{
		difficultyTimer += 0.1f;
		if (difficultyTimer >= nextDifficultyCap)
			InecreaseDifficulty();
		currentDelay -= 0.1f;
		if (currentDelay <= 0f)
			CreateIsland();
	}

	void CreateIsland()
	{
		float randomScale;
		float spriteOffset;
		float randomXAxis;
		Vector2 spawn;
		GameObject islandType;
		
		islandType = islandsList[Random.Range(0, islandsList.Length)];
		randomScale = Random.Range(0.4f, 1f);
		spriteOffset = islandType.GetComponent<BoxCollider2D>().size.x / 2 * randomScale;
		randomXAxis = Random.Range(-GameManager.gameManagerInstance.ScreenWidth / 2 + spriteOffset, GameManager.gameManagerInstance.ScreenWidth / 2 - spriteOffset);
		// L'ile apparaitra sur un axe X aléatoire, mais en prenant en compte sa taille aléatoire pour qu'elle ne depasse pas de l'écran
		spawn = new Vector2(spawnPosition.x + randomXAxis, spawnPosition.y);

		GameObject newIsland = Instantiate(islandType, spawn, Quaternion.identity);
		newIsland.GetComponent<Island>().Initialize(IslandsSpeed, randomScale);
		randomDelay = Random.Range(0f, maxRandomDelay);
		currentDelay = delayBetweenIslands + randomDelay;
	}

	void InecreaseDifficulty()
	{
		difficultyTimer = 0f;
		nextDifficultyCap += 3f;// Augmente le temps entre chaque niveau de difficulté
		IslandsSpeed += 0.07f;
		delayBetweenIslands -= delayBetweenIslands / 6f;
		if (delayBetweenIslands <= 1.4f)//La fréquence d'apparition des iles a un maximum, mais pas leur vitesse
			delayBetweenIslands = 1.5f;
	}
}
