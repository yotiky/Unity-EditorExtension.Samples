using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneWindowSample 
{
    [InitializeOnLoadMethod]
    private static void Initalize()
    {
        // InitializeOnLoad + static コンストラクタ、もしくは InitializeOnLoadMethod で初期化する
        SceneView.duringSceneGui += SceneViewOnduringSceneGui;
    }

    private static void SceneViewOnduringSceneGui(SceneView view)
    {
        var obj = Selection.activeGameObject;
        if (obj == null)
        {
            return;
        }

        // 3D GUIの描画はそのまま処理
        Handles.Label(obj.transform.position + Vector3.up * 3, obj.name);

        // 2D GUIの描画を拡張する場合は、BeginGUI/EndGUIで宣言する
        Handles.BeginGUI();
        // ここに2D GUIの表示や処理
        Handles.EndGUI();
    }
}
