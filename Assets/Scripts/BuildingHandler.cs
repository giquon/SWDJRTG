using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHandler : MonoBehaviour
{
    private GameManager		_gameManager;

	public	float			buildingWidth;

	public	GameObject		leftSpawnPoint;
	public	GameObject		rightSpawnPoint;

	public	GameObject[]	buildingList;

	private void Start()
	{
		_gameManager = GameManager.instance;

		StartCoroutine("SpawnCycle");
	}

	public IEnumerator SpawnCycle()
	{
		do
		{
			SpawnBuilding(true);
			SpawnBuilding(false);
			float waitTime = buildingWidth / _gameManager.platformSpeed;
			yield return new WaitForSeconds(waitTime);
		} while (true);
	}

	private void SpawnBuilding(bool isLeft)
	{
		GameObject newBuilding				= (GameObject)Instantiate(buildingList[Random.Range(0, buildingList.Length)]);
		newBuilding.transform.parent		= transform;
		newBuilding.transform.position		= (isLeft) ? leftSpawnPoint.transform.position : rightSpawnPoint.transform.position;
		
		if (!isLeft)
			newBuilding.transform.eulerAngles	= new Vector3(0, 180, 0);
	}
}
