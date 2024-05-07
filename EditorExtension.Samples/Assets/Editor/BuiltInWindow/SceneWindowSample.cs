using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class SceneWindowSample 
{
    static SceneWindowSample()
    {
        SceneView.duringSceneGui += SceneViewOnduringSceneGui;
    }
    
    [InitializeOnLoadMethod]
    private static void Initalize()
    {
        // InitializeOnLoad + static コンストラクタ、もしくは InitializeOnLoadMethod で初期化する
        //SceneView.duringSceneGui += SceneViewOnduringSceneGui;
    }

    private static void SceneViewOnduringSceneGui(SceneView view)
    {
        // 3D GUIの描画はそのまま処理
        var obj = Selection.activeGameObject;
        Handles.Label(obj.transform.position + Vector3.up * 3, obj.name);

        // 2D GUIの描画を拡張する場合は、BeginGUI/EndGUIで宣言する
        Handles.BeginGUI();
        // ここに2D GUIの表示や処理
        Handles.EndGUI();
    }
}
