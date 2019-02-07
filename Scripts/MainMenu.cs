using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MenuController
{
	#region Private
	[SerializeField]private GameObject mainPanel;
	[SerializeField]private GameObject optionsPanel;
	[SerializeField]private GameObject scorePanel;
	[SerializeField]private GameObject difficultyButton;
	[SerializeField]private GameObject ScoreButton;
	private TextMeshProUGUI highScoreText, difficultyText;
	#endregion

	void Start()
	{
		difficultyText = difficultyButton.GetComponent<TextMeshProUGUI>();
		UpdateDifficulty();
		UpdateScore();
	}

	public void UpdateDifficulty(int difference = 0)
	{	
		int currentDifficuty = PlayerPrefs.GetInt("Difficulty", 2) + difference;

		if (currentDifficuty == 1 || currentDifficuty == 4)
		{
			PlayerPrefs.SetInt("Difficulty", 1);
			difficultyText.text = "Easy";
		}
		else if (currentDifficuty == 3 || currentDifficuty == 0)
		{
			PlayerPrefs.SetInt("Difficulty", 3);
			difficultyText.text = "Hard";
		}
		else
		{
			PlayerPrefs.SetInt("Difficulty", 2);
			difficultyText.text = "Normal";
		}
	}

	private void UpdateScore()
	{
		ScoreButton.GetComponent<TextMeshProUGUI>().text = "High score:\n" + PlayerPrefs.GetInt("HighScore", 0).ToString() + " seconds";
	}

	public void Reset()
	{
		PlayerPrefs.DeleteKey("HighScore");
		UpdateScore();
	}
}
