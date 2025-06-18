using UnityEditor;
using UnityEngine;


public class ShowIfAttribute : PropertyAttribute
{
    public string ConditionField;

    public ShowIfAttribute(string conditionField)
    {
        ConditionField = conditionField;
    }
}

[CustomPropertyDrawer(typeof(ShowIfAttribute))]
public class ShowIfDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ShowIfAttribute showIf = (ShowIfAttribute)attribute;
        SerializedProperty condition = property.serializedObject.FindProperty(showIf.ConditionField);

        if (condition != null && condition.propertyType == SerializedPropertyType.Boolean && condition.boolValue)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ShowIfAttribute showIf = (ShowIfAttribute)attribute;
        SerializedProperty condition = property.serializedObject.FindProperty(showIf.ConditionField);

        if (condition != null && condition.propertyType == SerializedPropertyType.Boolean && condition.boolValue)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        // Return 0 to hide the field
        return 0f;
    }
}

