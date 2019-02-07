using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThermoBroke : MonoBehaviour
{
	#region Private
	[SerializeField]private GameObject defeatPanel;
	[SerializeField]private GameObject scoreText;
	#endregion

	void Start()
	{
		StartCoroutine("WaitForMenu");
		scoreText.GetComponent<TextMeshProUGUI>().text = "Score\n" + PlayerPrefs.GetInt("CurrentScore", 0);
	}
	
	IEnumerator WaitForMenu()
	{
		yield return new WaitForSeconds(2f);
		defeatPanel.SetActive(true);
	}

}