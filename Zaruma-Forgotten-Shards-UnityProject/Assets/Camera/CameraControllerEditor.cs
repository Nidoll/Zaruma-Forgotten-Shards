using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraController))]
public class CameraControllerEditor : Editor
{

    SerializedProperty speed, offset, minRotation, maxRotation;

    void OnEnable()
    {
        speed = serializedObject.FindProperty("speed");
        offset = serializedObject.FindProperty("offset");
        minRotation = serializedObject.FindProperty("minRotation");
        maxRotation = serializedObject.FindProperty("maxRotation");
    }

    public override void OnInspectorGUI()
    {
        /*
        serializedObject.Update();

        EditorGUILayout.LabelField("Camera movement speed:");
        EditorGUILayout.PropertyField(speed);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Offset to screenborder for movement:");
        EditorGUILayout.PropertyField(offset);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Minimum and maximum rotation of the camera:");
        EditorGUILayout.PropertyField(minRotation);
        EditorGUILayout.PropertyField(maxRotation);

        EditorGUILayout.Space();

        serializedObject.ApplyModifiedProperties();
        */

        DrawDefaultInspector();
    }
}
