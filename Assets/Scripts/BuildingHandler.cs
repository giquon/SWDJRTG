using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHandler : MonoBehaviour
{
    private GameManager		_gameManager;

	public	float			buildingWidth;

	public	Transform		leftSpawnPoint;
	public	Transform		rightSpawnPoint;

	public	GameObject[]	buildingList;

	private void Start()
	{
		_gameManager = GameManager.instance;
		InitializeFirstBuildings();
		StartCoroutine("SpawnCycle");
	}

	private void InitializeFirstBuildings()
	{
		int amountOfBuildings = (int)(leftSpawnPoint.position.z - _gameManager.destroyZ);

		for (int i = 0; i < amountOfBuildings; i++)
		{
			Vector3 leftPosition = leftSpawnPoint.position;
			leftPosition.z -= i * buildingWidth;

			Vector3 rightPosition = rightSpawnPoint.position;
			rightPosition.z -= i * buildingWidth;

			SpawnBuilding(true, leftPosition);
			SpawnBuilding(false, rightPosition);
		}
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
		SpawnBuilding(isLeft, (isLeft) ? leftSpawnPoint.position : rightSpawnPoint.position);
	}
	
	private void SpawnBuilding(bool isLeft, Vector3 aPosition)
	{
		GameObject newBuilding				= (GameObject)Instantiate(buildingList[Random.Range(0, buildingList.Length)]);
		newBuilding.transform.parent		= transform;
		newBuilding.transform.position		= aPosition;
		
		if (!isLeft)
			newBuilding.transform.eulerAngles	= new Vector3(0, 180, 0);
	}
}
