using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

public partial class InspectorSampleComponent2 
{
    [CustomEditor((typeof(InspectorSampleComponent2)))]
    private class InspectorWindowSample2 : Editor
    {
        private InspectorSampleComponent2 obj;

        private void OnEnable()
        {
            obj = target as InspectorSampleComponent2;
        }

        public override void OnInspectorGUI()
        {
            // 標準的な表示
            base.OnInspectorGUI();
            
            // private field
            obj.message = EditorGUILayout.TextField("メッセージ", obj.message);
        }
    }
}
#endif
