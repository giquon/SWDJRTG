using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawnHandler : MonoBehaviour
{
	private readonly string			PLATFORM_RESOURCE		= "TestPlatform";

	private GameManager				_gameManager;
	private	PlatformCurveHandler	_curveHandler;

    public  float                   yMinJumpDis;
    public  float                   yMaxJumpDis;

    public	float					defaultYDistanceOffset	= 1.0f;

	private float					minX;
	private float					maxX;

	private List<GameObject>		curve;

	private float					previousPlatformX;

    private Vector3                 platformSize; 

	private void Start()
	{
		_gameManager	= GameManager.instance;

		_curveHandler	= GetComponent<PlatformCurveHandler>();
		curve			= _curveHandler.nodesList;

		minX			= _gameManager.minXPosition;
		maxX			= _gameManager.maxXPosition;

        platformSize    = _gameManager.platformSize;


        StartCoroutine("SpawnCycle");
	}

	public IEnumerator SpawnCycle()
	{
		float newXPosition = 0;

		do
		{
			SpawnPlatform(newXPosition, platformSize);

			newXPosition		= Random.Range(minX, maxX);
            float randomDisY    = Random.Range(yMinJumpDis, yMaxJumpDis);
			float waitTime		= randomDisY / _gameManager.platformSpeed;
			
			yield return new WaitForSeconds(waitTime);
		} while (true);
	}

	public void SpawnPlatform(float platformXPosition, Vector3 aSize)
	{
		GameObject newPlatform = (GameObject)Instantiate(Resources.Load(PLATFORM_RESOURCE));
		newPlatform.GetComponent<Platform>().Initialize(curve, platformXPosition, aSize);
		previousPlatformX		= platformXPosition;
	}
}
