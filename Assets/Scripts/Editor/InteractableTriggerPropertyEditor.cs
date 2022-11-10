using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InteractableTriggerProperty)), CanEditMultipleObjects]
public class InteractableTriggerPropertyEditor : Editor
{
    SerializedProperty isActivatedFromEverySide;
    SerializedProperty isBoxLeft;
    SerializedProperty isBoxRight;
    SerializedProperty isBoxFront;
    SerializedProperty isBoxBack;

    private void OnEnable()
    {
        isActivatedFromEverySide = serializedObject.FindProperty("isActivatedFromEverySide");
        isBoxFront = serializedObject.FindProperty("isBoxFront");
        isBoxLeft = serializedObject.FindProperty("isBoxLeft");
        isBoxRight = serializedObject.FindProperty("isBoxRight");
        isBoxBack = serializedObject.FindProperty("isBoxBack");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(isActivatedFromEverySide);

        if(isActivatedFromEverySide.boolValue == false)
        {            
            EditorGUILayout.PropertyField(isBoxFront);
            EditorGUILayout.PropertyField(isBoxLeft);
            EditorGUILayout.PropertyField(isBoxRight);
            EditorGUILayout.PropertyField(isBoxBack);
        }

        serializedObject.ApplyModifiedProperties();

        base.OnInspectorGUI();
    }
}
