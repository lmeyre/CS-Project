using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningForest : MonoBehaviour // Script pour que les forêts n'apparaissent pas déjà en feu mais s'allument dans l'écran
{
	#region Private
	private Animator animator;
	private float countDown, randomOffset;
	#endregion

	void Start ()
	{
		animator = GetComponent<Animator>();
		randomOffset = Random.Range(0f, 20f);
		countDown = (40f + randomOffset) / GameManager.gameManagerInstance.islandSpawner.IslandsSpeed;
		StartCoroutine("IgniteFire");
	}

	IEnumerator IgniteFire()
	{
		while (countDown > 0)
		{
			countDown--;
			yield return null;
		}
		animator.SetBool("Ignite", true);
	}
}
