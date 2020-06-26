using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawnHandler : MonoBehaviour
{
	private readonly string			PLATFORM_RESOURCE		= "TestPlatform";

	private GameManager				_gameManager;
	private	PlatformCurveHandler	_curveHandler;

	public	float					minJumpDis;
	public	float					maxJumpDis;

	public	float					defaultYDistanceOffset	= 1.0f;

	private float					minX;
	private float					maxX;

	private List<GameObject>		curve;

	private float					previousPlatformX;

    public Vector2                  randomSizeBounds;

	private void Start()
	{
		_gameManager	= GameManager.instance;

		_curveHandler	= GetComponent<PlatformCurveHandler>();
		curve			= _curveHandler.nodesList;

		minX			= _gameManager.minXPosition;
		maxX			= _gameManager.maxXPosition;

		StartCoroutine("SpawnCycle");
	}

	public IEnumerator SpawnCycle()
	{
		float newXPosition = Random.Range(minX, maxX);

		do
		{
			SpawnPlatform(newXPosition, Random.Range(randomSizeBounds.x, randomSizeBounds.y));
			newXPosition		= Random.Range(minX, maxX);
			float randomDis		= Random.Range(minJumpDis, maxJumpDis);
			float xDistance		= Mathf.Abs(newXPosition - previousPlatformX);
			xDistance			= Mathf.Clamp(xDistance, minJumpDis, randomDis);
			float yDistance		= Mathf.Sqrt((randomDis * randomDis) - (xDistance * xDistance));
			float waitTime		= yDistance / _gameManager.platformSpeed;
			
			yield return new WaitForSeconds(waitTime);
		} while (true);
	}

	public void SpawnPlatform(float platformXPosition, float platformWidth)
	{
		GameObject newPlatform = (GameObject)Instantiate(Resources.Load(PLATFORM_RESOURCE));
		newPlatform.GetComponent<Platform>().Initialize(curve, platformXPosition, platformWidth);
		previousPlatformX		= platformXPosition;
	}
}
