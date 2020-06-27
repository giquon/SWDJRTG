using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Platform : MonoBehaviour
{
	private GameManager			_gameManager;

	private	float				_moveSpeed;
	private float				_destroyZ;
	
	private List<GameObject>	_nodesList;

	private	Vector3				_currentDestination;
	private int					_nodeIndex;

	private float				_xCoord;

	private bool				_hasLanded			= false;

	private void Start()
	{
		_gameManager	= GameManager.instance;
		_moveSpeed		= _gameManager.platformSpeed;
		_destroyZ		= _gameManager.destroyZ;
	}

	private void FixedUpdate()
	{
		if (_nodesList == null)
			return;

		Vector3 moveDirection = (new Vector3(0, transform.position.y, transform.position.z) - new Vector3(0, _currentDestination.y, _currentDestination .z)).normalized;
		transform.position -= moveDirection * _gameManager.platformSpeed * Time.deltaTime;

		if (transform.position.z <= _destroyZ)
			Destroy(this.gameObject);

		if (transform.position.z <= _currentDestination.z)
		{
			if (_nodeIndex < _nodesList.Count - 1)
				_currentDestination = _nodesList[++_nodeIndex].transform.position;
			else
				_currentDestination = new Vector3(0, 0, _destroyZ - 1);
		}

		transform.position = new Vector3(_xCoord, transform.position.y, transform.position.z);
	}

	public void Initialize(List<GameObject> aNodesList, float aXCoord, Vector3 aSize)
	{
		_nodesList				= aNodesList;
		_currentDestination		= _nodesList[0].transform.position;
		_xCoord					= aXCoord;
		transform.position		= new Vector3(_xCoord, _currentDestination.y, _currentDestination.z);
        transform.localScale    = aSize;
        _nodeIndex				= 0;
	}

	public void PlayerLanded()
	{
		if (_hasLanded)
			return;

		_gameManager.AddScore();
	}
}
