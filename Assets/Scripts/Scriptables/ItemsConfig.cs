using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "ItemsConfig")]
public class ItemsConfig : ScriptableObject
{
    public List<Item2> items;

    private void OnValidate()
    {
        var distinctIds = items.Select(x => x.Id).Distinct();

        if (distinctIds.Count() < items.Count) throw new Exception($"Ids in {this.name} must be unique positive int!");

        EditorUtility.SetDirty(this);
    }
}

[Serializable]
public class Item2
{
    public uint Id;
    public string Name;
    public Bodypart Bodypart;
}

//#if UNITY_EDITOR
//[CustomPropertyDrawer(typeof(ReadonlyInEditor))]
//public class ScriptableObjectIdAttributeDrawer : PropertyDrawer
//{
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        GUI.enabled = false;
//        EditorGUI.PropertyField(position, property, label, true);
//        GUI.enabled = true;
//    }
//}
//#endif

//public class ReadonlyInEditor : PropertyAttribute { }

