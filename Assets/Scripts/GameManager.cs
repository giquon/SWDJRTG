using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	private readonly	int			DEFAULT_SCORE	= 10;

    public static		GameManager	instance;

	public				float		platformSpeed	= 7.0f;

	public				int			playerScore;

	public				float		minXPosition	= -3.0f;
	public				float		maxXPosition	= 3.0f;

	public				float		speedIncrement	= 0.1f;
	public				float		maxSpeed		= 20.0f;

	public				float		destroyZ		= -10;

    public				Vector3		platformSize; 

	public				Text		scoreUI;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("There can be only one instance of GameManager in a scene");
			return;
		}

		instance = this;
	}

	private void Update()
	{
		if (platformSpeed < maxSpeed)
			platformSpeed += speedIncrement * Time.deltaTime;
	}

	private void Start()
	{
		playerScore = 0;
	}

	public void AddScore(int aScore)
	{
		playerScore += aScore;
		scoreUI.text = playerScore.ToString();
	}

	public void AddScore()
	{
		AddScore(DEFAULT_SCORE);
	}
}
