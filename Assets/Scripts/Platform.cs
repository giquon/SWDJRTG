using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Platform : MonoBehaviour
{
	private GameManager			_gameManager;

	private	float				_moveSpeed;
	
	private List<GameObject>	_nodesList;

	private	Vector3				_currentDestination;
	private int					_nodeIndex;

	private float				_xCoord;

	private void Start()
	{
		_gameManager = GameManager.instance;
		_moveSpeed = _gameManager.platformSpeed;
	}

	private void FixedUpdate()
	{
		if (_nodesList == null)
			return;

		Vector3 moveDirection = (new Vector3(0, transform.position.y, transform.position.z) - new Vector3(0, _currentDestination.y, _currentDestination .z)).normalized;
		transform.position -= moveDirection * _gameManager.platformSpeed * Time.deltaTime;

		if (transform.position.z <= 0)
			Destroy(this.gameObject);

		if (transform.position.z <= _currentDestination.z)
		{
			if (_nodeIndex < _nodesList.Count - 1)
				_currentDestination = _nodesList[++_nodeIndex].transform.position;
			else
				_currentDestination = new Vector3(0, 0, -10);
		}

		transform.position = new Vector3(_xCoord, transform.position.y, transform.position.z);
	}

	public void Initialize(List<GameObject> aNodesList, float aXCoord)
	{
		_nodesList			= aNodesList;
		_currentDestination	= _nodesList[0].transform.position;
		_xCoord				= aXCoord;
		transform.position	= new Vector3(_xCoord, _currentDestination.y, _currentDestination.z);
		_nodeIndex			= 0;
	}
}
