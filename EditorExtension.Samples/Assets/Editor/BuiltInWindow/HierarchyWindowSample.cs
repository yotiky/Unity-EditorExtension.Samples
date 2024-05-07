using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class HierarchyWindowSample 
{
    static HierarchyWindowSample()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
    }
    
    [InitializeOnLoadMethod]
    private static void Initalize()
    {
        // InitializeOnLoad + static コンストラクタ、もしくは InitializeOnLoadMethod で初期化する
        //EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
    }

    private static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
    }
}
