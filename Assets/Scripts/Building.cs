using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
	private	GameManager	_gameManager;

	private float		_destroyZ;

	private void Start()
	{
		_gameManager	= GameManager.instance;
		_destroyZ		= _gameManager.destroyZ;
	}

	private void FixedUpdate()
	{
		Vector3 moveDirection = (new Vector3(0, 0, transform.position.z) - new Vector3(0, 0, -10)).normalized;
		transform.position -= moveDirection * _gameManager.platformSpeed * Time.deltaTime;

		if (transform.position.z <= _destroyZ)
			Destroy(this.gameObject);
	}
}
