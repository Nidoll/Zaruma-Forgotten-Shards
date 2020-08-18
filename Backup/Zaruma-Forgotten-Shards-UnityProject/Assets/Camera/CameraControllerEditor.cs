using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraController))]
public class CameraControllerEditor : Editor
{

    SerializedProperty speed, offset, minRotation, maxRotation, minHight, maxHight, scrollSpeed, smoothingOne, smoothingTwo, keepTime;

    void OnEnable()
    {
        speed = serializedObject.FindProperty("speed");
        offset = serializedObject.FindProperty("offset");
        minRotation = serializedObject.FindProperty("minRotation");
        maxRotation = serializedObject.FindProperty("maxRotation");
        minHight = serializedObject.FindProperty("minHight");
        maxHight = serializedObject.FindProperty("maxHight");
        scrollSpeed = serializedObject.FindProperty("scrollSpeed");
        smoothingOne = serializedObject.FindProperty("smoothingOne");
        smoothingTwo = serializedObject.FindProperty("smoothingTwo");
        keepTime = serializedObject.FindProperty("keepTime");
    }

    public override void OnInspectorGUI()
    {
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

        EditorGUILayout.LabelField("Scrolling propertys:");
        EditorGUILayout.PropertyField(scrollSpeed);
        EditorGUILayout.PropertyField(smoothingOne);
        EditorGUILayout.PropertyField(smoothingTwo);
        EditorGUILayout.PropertyField(keepTime);

        serializedObject.ApplyModifiedProperties();

        //DrawDefaultInspector();
    }
}
