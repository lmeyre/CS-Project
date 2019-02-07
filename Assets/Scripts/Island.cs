using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
	#region Properties
	public float Speed {get;set;}
	#endregion

	#region Private
	[SerializeField]private bool smartTown;
	private float maximumHeight, spriteSize;
	private bool clicked;
	#endregion

	public void Initialize(float speed, float scale)
	{
		Speed = speed;
		transform.localScale = new Vector3(scale, scale, 1f);
		spriteSize = 4f * scale;
		maximumHeight = GameManager.gameManagerInstance.ScreenHeight / 2 + spriteSize;// La taille de l'écran + du sprite, pour s'assurer qu'il est hors de l'écran quand supprimé
		PerspectiveLayer(scale);
		clicked = false;
	}

	void PerspectiveLayer(float scale)// Les iles plus petites seront derrière les grandes pour donner une impression de distance
	{
		Component[] allLayers;
		int layerOrder =  (int)(scale * 1000);// On caste la valeur du scale après l'avoir multiplié par 1000 pour garder les décimales

		allLayers = GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer layer in allLayers)
			layer.sortingOrder = layerOrder;
	}

	void Update ()
	{
		if (clicked == false)// L'ile arrete de s'élever si cliquée
			transform.Translate(transform.up * Speed / 5);
		if (transform.position.y > maximumHeight)
		{
			if (smartTown == false)
				GameManager.gameManagerInstance.temperature.CurrentTemperature += 2f;
			Destroy(gameObject);
		}
	}

	void OnMouseDown()
	{
		if (smartTown == true)
			GameManager.gameManagerInstance.temperature.CurrentTemperature += 1f;
		StartCoroutine("IslandClicked");
	}

	IEnumerator IslandClicked()
	{
		float despawnTime = 0.1f;
		Vector2 endScale = transform.localScale / 10f;

		clicked = true;
		while (despawnTime > 0)
		{
			transform.localScale = Vector3.Lerp(transform.localScale, endScale, 1f / (0.1f / Time.deltaTime));
			despawnTime -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		Destroy(gameObject);
	}
}
