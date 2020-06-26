using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static	GameManager instance;

	public	float				platformSpeed	= 7.0f;
	public	float				minXPosition	= -3.0f;
	public	float				maxXPosition	= 3.0f;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("There can be only one instance of GameManager in a scene");
			return;
		}

		instance = this;
	}

}
