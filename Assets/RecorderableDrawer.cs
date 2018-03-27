using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Linq;
using System;

[CustomPropertyDrawer(typeof(ReorderableAttribute))]
public class RecorderableDrawer : PropertyDrawer
{
    Dictionary<string, ReorderableList> reorderableMap = new Dictionary<string, ReorderableList>();

    private ReorderableList GetReorderableList(SerializedProperty property)
    {
        var path = property.propertyPath;
        if (reorderableMap.ContainsKey(path) == false)
        {
            reorderableMap.Add(path, CreateReorderableList(property));
        }
        return reorderableMap[path];
    }

    private ReorderableList CreateReorderableList(SerializedProperty property)
    {
        ReorderableList reorderableList;

        reorderableList = new ReorderableList(property.serializedObject, property, true, true, true, true)
        {
            headerHeight = 5
        };

        reorderableList.elementHeightCallback = index =>
        {
            SerializedProperty targetElement = reorderableList.serializedProperty.GetArrayElementAtIndex(index);
            return EditorGUI.GetPropertyHeight(targetElement);
        };
        reorderableList.drawElementCallback = delegate (Rect rect1, int index, bool active, bool focused)
        {
            SerializedProperty targetElement = reorderableList.serializedProperty.GetArrayElementAtIndex(index);

            bool isExpanded = targetElement.isExpanded;

            rect1.height = EditorGUIUtility.singleLineHeight;

            if (targetElement.hasVisibleChildren)
                rect1.xMin += 10;

            GUIContent propHeader = new GUIContent(targetElement.displayName);

            EditorGUI.PropertyField(rect1, targetElement, propHeader, true);
        };
        return reorderableList;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.GetParentProp().GetArrayElementAtIndex(0).propertyPath == property.propertyPath)
        {
            return GetReorderableList(GetArrayPropertyFromElement(property)).GetHeight();
        }
        return 0f;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.GetParentProp().GetArrayElementAtIndex(0).propertyPath == property.propertyPath)
        {
            GetReorderableList(GetArrayPropertyFromElement(property)).DoList(position);
        }
    }

    private static SerializedProperty GetArrayPropertyFromElement(SerializedProperty property)
    {
        return property.GetParentProp().GetParentProp();
    }

}