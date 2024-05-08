using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[CustomEditor(typeof(SerializedObjectSampleComponent))]
public class SerializedObjectSample : Editor
{
    [MenuItem("GameObject/Samples/Serialized Object/Reset Selected Objects Position")]
    static void ResetPosition()
    {
        var transforms = Selection.gameObjects.Select(go => go.transform).ToArray();
        var so = new SerializedObject(transforms);
        // メモリ上の値を最新化（インスタンスが複数のフレームをまたぐ場合に必要）
        so.Update();
        // 更新
        so.FindProperty("m_LocalPosition").vector3Value = Vector3.zero;
        // 更新内容を反映する（シリアライズ）
        so.ApplyModifiedProperties();
        
        // Inspectorウィンドウでプロパティを右クリックすると「Copy Property Path」が表示されるので、実行するとメンバ変数名（m_LocalPosition）をコピーできる
        // もしくはShift+右クリックすると「Print Property Path」が表示されるので実行するとConsoleに出力される
        // Inspectorウィンドウを拡張している場合は、EditorGUILayout.PropertyFieldを使っている場合にメニューが表示される
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.LabelField("==ここから Custom==");
        serializedObject.Update();

        EditorGUILayout.LabelField("Layout");
        // デフォルトの入力欄
        EditorGUILayout.LabelField("PropertyField");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("boolValue"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("floatValue"));
        
        // カスタマイズしたい場合は個別に
        EditorGUILayout.LabelField("Customize");
        serializedObject.FindProperty("boolValue").boolValue
            = EditorGUILayout.Toggle("Bool Value", serializedObject.FindProperty("boolValue").boolValue, "CircularToggle");
        serializedObject.FindProperty("floatValue").floatValue 
            = EditorGUILayout.Slider("Float Value", serializedObject.FindProperty("floatValue").floatValue, 0, 10);
        
        // SerializedProperty いろいろ
        if (GUILayout.Button("Primitive"))
        {
            // by activeGameObject
            var obj = Selection.activeGameObject;
            if (obj == null) return;

            var component = obj.GetComponent<SerializedObjectSampleComponent>();
            if (component == null) return;

            var so = new SerializedObject(component);
            so.FindProperty("boolValue").boolValue = true;
            so.ApplyModifiedProperties();

            // by target
            so = new SerializedObject(target);
            so.FindProperty("intValue").intValue = 10;
            so.ApplyModifiedProperties();

            // by serializedObject
            so = serializedObject;
            so.FindProperty("stringValue").stringValue = "stringValue";
            so.ApplyModifiedProperties();
        }

        EditorGUILayout.LabelField("List");
        using (new EditorGUILayout.HorizontalScope())
        {
            var listProperty = serializedObject.FindProperty("list");
            if (GUILayout.Button("Add"))
            {
                listProperty.InsertArrayElementAtIndex(listProperty.arraySize);
            }

            if (GUILayout.Button("Remove"))
            {
                if (listProperty.arraySize > 0)
                    listProperty.DeleteArrayElementAtIndex(listProperty.arraySize - 1);
            }

            if (GUILayout.Button("Clear"))
            {
                listProperty.ClearArray();
            }
        }

        EditorGUILayout.LabelField("enum");
        var enumValueProperty = serializedObject.FindProperty("enumValue");
        enumValueProperty.enumValueIndex = EditorGUILayout.Popup("フルーツ", enumValueProperty.enumValueIndex, enumValueProperty.enumDisplayNames);
        if (GUILayout.Button("Output enum values"))
        {
            Debug.Log($"enum value: {enumValueProperty.enumValueIndex}/{enumValueProperty.enumNames[enumValueProperty.enumValueIndex]}/{enumValueProperty.enumDisplayNames[enumValueProperty.enumValueIndex]}");
        }
        
        EditorGUILayout.LabelField("objectReference");
        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Up"))
            {
                var objRef = serializedObject.FindProperty("objectReferenceValue").objectReferenceValue as GameObject;
                if (objRef == null) return;

                var soRef = new SerializedObject(objRef.transform);
                soRef.FindProperty("m_LocalPosition").vector3Value = Vector3.up * 3;
                soRef.ApplyModifiedProperties();
            }
            if (GUILayout.Button("Down"))
            {
                var objRef = serializedObject.FindProperty("objectReferenceValue").objectReferenceValue as GameObject;
                if (objRef == null) return;

                var soRef = new SerializedObject(objRef.transform);
                soRef.FindProperty("m_LocalPosition").vector3Value = Vector3.zero;
                soRef.ApplyModifiedProperties();
            }
        }
        
        if (GUILayout.Button("Sub class reference"))
        {
            var objRefProperty = serializedObject.FindProperty("subClassReference");
            var relativeProperty = objRefProperty.FindPropertyRelative("doubleValue");
            relativeProperty.doubleValue++;
        }

        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.LabelField("by ScriptableObject");
        if (GUILayout.Button("Output"))
        {
            var scriptableObject = ScriptableObject.CreateInstance<SampleScriptableObject>();
            var so = new SerializedObject(scriptableObject);
            so.FindProperty("text").stringValue = "hogehoge";
            so.ApplyModifiedProperties();
            Debug.Log("by ScriptableObject:" + scriptableObject.Text);
        }
    }
}
