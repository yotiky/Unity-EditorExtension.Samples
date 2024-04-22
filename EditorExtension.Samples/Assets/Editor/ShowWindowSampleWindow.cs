using System;
using System.Collections;
using System.Collections.Generic;
using Codice.Client.Common;
using UnityEditor;
using UnityEngine;

public class ShowWindowSamplesWindow : EditorWindow
{
    [MenuItem("Sample/ShowWindow/1.Show")]
    static void CallShow()
    {
        var window = CreateInstance<ShowWindowSamplesWindow>();
        window.Show();
        
        // GetWindowする場合は内部で呼ばれるのでShowを呼び出さなくても良い
        //GetWindow<ShowWindowSamplesWindow>("Show");
    }
    [MenuItem("Sample/ShowWindow/1.ShowWithParams")]
    static void CallShowWithParams()
    {
        var window = CreateInstance<ShowWindowSamplesWindow>();
        window.Show();
        window.minSize = new Vector2(100, 100);
        window.maxSize = new Vector2(1280, 960);
        // positionだけはShowの後じゃないと反映されない
        window.position = new Rect(20, 20, 400, 300);
    }
    [MenuItem("Sample/ShowWindow/2.ShowModal")]
    static void CallShowModal()
    {
        var window = CreateInstance<ShowWindowSamplesWindow>();
        window.ShowModal();
    }
    [MenuItem("Sample/ShowWindow/3.ShowModalUtility")]
    static void CallShowModalUtility()
    {
        var window = CreateInstance<ShowWindowSamplesWindow>();
        // positionはShowModalUtilityの前
        window.position = new Rect(50, 50, 400, 300);
        window.ShowModalUtility();
    }
    [MenuItem("Sample/ShowWindow/4.ShowUtility")]
    static void CallShowUtility()
    {
        var window = CreateInstance<ShowWindowSamplesWindow>();
        window.ShowUtility();
    }
    [MenuItem("Sample/ShowWindow/5.ShowAuxWindow")]
    static void CallShowAuxWindow()
    {
        var window = CreateInstance<ShowWindowSamplesWindow>();
        window.ShowAuxWindow();
    }

    private Rect dropDownButtonRect;
    private Rect popupButtonRect;
    private void OnGUI()
    {
        GUILayout.Label("Text1");
        GUILayout.Label("Text2");
        GUILayout.Label("Text3");

        if (GUILayout.Button("ShowAsDropDown"))
        {
            var popupPosition = GUIUtility.GUIToScreenPoint(dropDownButtonRect.center);
            var window = CreateInstance<PopupWindow>();
            window.ShowAsDropDown(new Rect(popupPosition, Vector2.zero), new Vector2(250, 150));
        }
        if (Event.current.type == EventType.Repaint) 
            dropDownButtonRect = GUILayoutUtility.GetLastRect();
        
        if (GUILayout.Button("ShowPopup"))
        {
            var popupPosition = GUIUtility.GUIToScreenPoint(popupButtonRect.center);
            var window = CreateInstance<PopupWindow>();
            window.position = new Rect(popupPosition, new Vector2(250, 150));
            window.ShowPopup();
        }
        if (Event.current.type == EventType.Repaint) 
            popupButtonRect = GUILayoutUtility.GetLastRect();
        
        if (GUILayout.Button("ShowNotification"))
        {
            ShowNotification(new GUIContent("ShowNotification"));
        }

        if (GUILayout.Button("Remove Notification"))
        {
            this.RemoveNotification();
        }
        
        if (GUILayout.Button("Close"))
        {
            this.Close();
        }
        
        GUILayout.Label("MousePosition" + Event.current.mousePosition.ToString());
    }

    private void Update()
    {
        Repaint();
    }
}
