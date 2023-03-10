using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(NotNull))]
public class NotNullDrawer : PropertyDrawer
{
    static IDictionary<string, bool> errs = new Dictionary<string, bool>();

    public override void OnGUI(Rect inRect, SerializedProperty inProp, GUIContent label)
    {
        EditorGUI.BeginProperty(inRect, label, inProp);
        string k = label.text.ToString();
        bool isNull;
        switch (inProp.type)
        {
            case "Enum": isNull = inProp.enumValueIndex == 0; break;
            case "int": isNull = inProp.intValue == 0; break;
            case "float": isNull = System.MathF.Abs(inProp.floatValue) < Mathf.Epsilon; break;
            case "long": isNull = inProp.longValue == 0; break;
            case "double": isNull = System.Math.Abs(inProp.doubleValue) < Mathf.Epsilon; break;
            case "string": isNull = inProp.stringValue == ""; break;
            default: isNull = inProp.objectReferenceValue == null; break;
        }

        if (isNull)
        {
            if (!errs.ContainsKey(label.text) || !errs[k])
                Debug.LogError("NotNullAttr: Reference of " + k + " is null");
            label.text = "[!] " + label.text;
            GUI.color = Color.red;
            errs[k] = true;
        }
        else errs[k] = false;

        EditorGUI.PropertyField(inRect, inProp, label);
        GUI.color = Color.white;

        EditorGUI.EndProperty();
    }
}
