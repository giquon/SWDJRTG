using System.Collections.Generic;
using UnityEngine;

public class PlatformCurveHandler : MonoBehaviour
{
	public	GameObject			startObject;
	public	GameObject			endObject;

	////////// Curve Settings //////////
	public	int					resolution;
	public	float				flatlineOffset;
	public	float				curveExponent	= 2;

	public	List<GameObject>	nodesList;

	public void GenerateNodes()
	{
		ClearNodesList();

		Vector3 start					= startObject.transform.position;
		Vector3 end						= endObject.transform.position;

		float flatlineEnd				= (start.z - end.z) * (flatlineOffset / 100);
		Vector3 flatline				= new Vector3(0, 0, flatlineEnd);

		float curveLength				= start.z - flatline.z;
		float stepSize					= curveLength / resolution;

		for (int i = 0; i <= resolution; i++)
		{
			float newNodeZ = i * stepSize;
			newNodeZ += flatline.z;
			float newNodeY = Mathf.Pow(curveExponent, i) - 1;

			CreateNode(new Vector3(0, newNodeY, newNodeZ));
		}
	}

	private void CreateNode(Vector3 aNodePosition)
	{
		GameObject newNode			= (GameObject)Instantiate(Resources.Load("CurveNode"));
		newNode.transform.position	= aNodePosition;
		newNode.transform.parent	= this.transform;
		nodesList.Add(newNode);
	}

	public void ClearNodesList()
	{
		if (nodesList		== null ||
			nodesList.Count	== 0)
			return;

		foreach(GameObject node in nodesList)
		{
			DestroyImmediate(node);
		}

		nodesList.Clear();
	}
}