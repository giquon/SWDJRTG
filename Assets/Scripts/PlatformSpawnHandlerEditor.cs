using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlatformSpawHandler))]
[CanEditMultipleObjects]
public class PlatformSpawnHandlerEditor : Editor
{
	////////// MACROS //////////
	private readonly float		MIN_FLAT_OFFSET	= 0;
	private readonly float		MAX_FLAT_OFFSET	= 100;

	private	PlatformSpawHandler	_target;

	private	SerializedProperty	_startObject;
	private	SerializedProperty	_endObject;

	private SerializedProperty	_resolution;
	private SerializedProperty	_curveExponent;

	private SerializedProperty	_nodesList;

	private void OnEnable()
	{
		_target			= (PlatformSpawHandler)target;
		_startObject	= serializedObject.FindProperty("startObject");
		_endObject		= serializedObject.FindProperty("endObject");
		_resolution		= serializedObject.FindProperty("resolution");
		_nodesList		= serializedObject.FindProperty("nodesList");
		_curveExponent	= serializedObject.FindProperty("curveExponent");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.LabelField("Curve Sides", EditorStyles.boldLabel);
		EditorGUILayout.PropertyField(_startObject);
		EditorGUILayout.PropertyField(_endObject);

		_target.flatlineOffset	= EditorGUILayout.Slider("Flatline Offset (%)", _target.flatlineOffset, MIN_FLAT_OFFSET, MAX_FLAT_OFFSET);
		EditorGUILayout.PropertyField(_resolution);
		EditorGUILayout.PropertyField(_curveExponent);

		if (GUILayout.Button("Generate Curve Nodes"))
		{
			_target.GenerateNodes();
		}

		EditorGUILayout.PropertyField(_nodesList);

		if (GUILayout.Button("Clear Curve Nodes"))
		{
			_target.ClearNodesList();
		}

		serializedObject.ApplyModifiedProperties();
	}
}