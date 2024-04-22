using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Codice.Client.Common;
using UnityEditor;
using UnityEngine;

public class SamplesWindow : EditorWindow
{
    private GUIStyle header1Style;
    private GUIStyle header2Style;
    private GUIStyle header3Style;

    private bool scrollWindow = true;
    private Vector2 scrollPosWindow = Vector2.zero;

    private bool showGUILayoutParts = true;
    private bool showEditorGUILayoutUIParts = true;
    private bool showLayout = true;
    private bool showStyle = true;
    private bool showUtility = true;
    private bool showEx = true;
    
    [MenuItem("Sample/Editor Extension Samples")]
    static void OpenWindow()
    {
        var window = GetWindow<SamplesWindow>("Editor Extension Samples");
        //window.position = new Rect(0, 0, 300, 1000);
    }

    private void OnGUI()
    {
        header1Style = new GUIStyle
        {
            fontSize = 24,
            normal = new GUIStyleState { textColor = Color.cyan, },
        };
        header2Style = new GUIStyle
        {
            fontSize = 18,
            normal = new GUIStyleState { textColor = new Color(0.24f, 0.66f, 1), },
        };
        header3Style = new GUIStyle
        {
            fontSize = 14,
            normal = new GUIStyleState { textColor = new Color(0.36f, 0.46f, 1f), },
        };

        if (scrollWindow)
            using (var scope = new GUILayout.ScrollViewScope(scrollPosWindow))
            {
                scrollPosWindow = scope.scrollPosition;
                SampleIndex();
            }
        else
            SampleIndex();
    }
    private void SampleIndex()
    {
        if (showGUILayoutParts)
        {
            GUILayout.Label("GUILayout UIParts", header1Style);
            GUILayoutUIParts();
            EditorGUILayout.Space(10f);
        }

        if (showEditorGUILayoutUIParts)
        {
            GUILayout.Label("EditorGUILayout UIParts", header1Style);
            EditorGUILayoutUIParts();
            EditorGUILayout.Space(10f);
        }

        if (showLayout)
        {
            GUILayout.Label("Layout", header1Style);
            Layout();
            EditorGUILayout.Space(10f);
        }

        if (showStyle)
        {
            GUILayout.Label("Style", header1Style);
            Style();
            EditorGUILayout.Space(10f);
        }

        if (showUtility)
        {
            GUILayout.Label("Utility", header1Style);
            Utility();
            EditorGUILayout.Space(10f);
        }

        if (showEx)
        {
            GUILayout.Label("Ex", header1Style);
            Ex();
            EditorGUILayout.Space(10f);
        }
    }

    private string textField = "";
    private string textArea = "";
    private string password = "";
    private bool toggle = false;
    private int toolbarSelected = 0;
    private int selGridSelected = 0;
    private float hSliderValue = 0;
    private float vSliderValue = 0;
    private float hSbarValue = 0;
    private float vSbarValue = 0;

    private float floatField = 0;
    private bool toggleGroup = false;
    private Object objectField = null;
    private float fSliderValue = 0;
    private int iSliderValue = 0;
    private float mmSliderMinLimit = -20;
    private float mmSliderMaxLimit = 20;
    private float mmSliderMinValue = -10;
    private float mmSliderMaxValue = 10;
    private bool foldout = false;
    private Vector2 scrollPosScope = Vector2.zero;

    private string selectedPath;
    private Rect popupButtonRect;
    private int guiEventCount = -1;
    private bool cancelablePBar;

    private string focusControlPath = null;
    private string saveDataText = null;
    
    private void GUILayoutUIParts()
    {
        // Label
        GUILayout.Label("Label");
        // TextField
        textField = GUILayout.TextField(textField);
        // TextArea
        textArea = GUILayout.TextArea(textArea);
        // PasswordField
        password = GUILayout.PasswordField(password, '*');
        
        // Toggle
        toggle = GUILayout.Toggle(toggle, "Toggle");

        // Button
        if (GUILayout.Button("Button"))
            Debug.Log("Button clicked");
        
        // RepeatButton
        if(GUILayout.RepeatButton("RepeatButton"))
            Debug.Log("RepeatButton clicking");

        // Toolbar
        toolbarSelected = GUILayout.Toolbar(toolbarSelected, new[] { "Toolbar1", "Toolbar2", "Toolbar3" });
        // SelectionGrid
        selGridSelected = GUILayout.SelectionGrid(selGridSelected, new[] { "SelectionGrid1", "SelectionGrid2", "SelectionGrid3" }, 2);

        // HorizontalSlider, VerticalSlider
        hSliderValue = GUILayout.HorizontalSlider(hSliderValue, 0, 100, GUILayout.Height(20f));
        vSliderValue = GUILayout.VerticalSlider(vSliderValue, 100, 0, GUILayout.Height(50f));
        
        // HorizontalScrollbar, VerticalScrollbar
        hSbarValue = GUILayout.HorizontalScrollbar(hSbarValue, 10, 0, 10);
        vSbarValue = GUILayout.VerticalScrollbar(vSbarValue, 10, 10, 0);
    }
    private void EditorGUILayoutUIParts()
    {
        // LabelField
        EditorGUILayout.LabelField("Label");
        // SelectableLabel
        EditorGUILayout.SelectableLabel("SelectableLabel");
        // PrefixLabel
        EditorGUILayout.PrefixLabel("PrefixLabel");
        // TextField
        textField = EditorGUILayout.TextField(textField);
        // DelayedTextField
        textField = EditorGUILayout.DelayedTextField(textField);
        // TextArea
        textArea = EditorGUILayout.TextArea(textArea);
        // PasswordField
        password = EditorGUILayout.PasswordField(password);

        // Toggle
        toggle = EditorGUILayout.Toggle(toggle);
        toggle = EditorGUILayout.Toggle("ToggleRight", toggle);
        // ToggleLeft
        toggle = EditorGUILayout.ToggleLeft("ToggleLeft", toggle);

        // ToggleGroupScope: 配下のToggleを無効化できる
        using (var scope = new EditorGUILayout.ToggleGroupScope("Toggle Group Scope", toggleGroup))
        {
            toggleGroup = scope.enabled;
            toggle = EditorGUILayout.Toggle("Toggle", toggle);
            toggle = EditorGUILayout.Toggle("Toggle", toggle);
            toggle = EditorGUILayout.Toggle("Toggle", toggle);
        }
        
        // FloatField: int, logn, float, doubleがある
        floatField = EditorGUILayout.FloatField(floatField);
        // DelayedFloatField: int, float, doubleがある
        floatField = EditorGUILayout.DelayedFloatField(floatField);

        // ObjectField
        objectField = EditorGUILayout.ObjectField(objectField, typeof(Object), true);
        
        // LinkButton
        if (EditorGUILayout.LinkButton("LinkButton"))
            Debug.Log("Button clicked");
        
        // Slider
        fSliderValue = EditorGUILayout.Slider("Slider", fSliderValue, 0, 10);
        // IntSlider
        iSliderValue = EditorGUILayout.IntSlider("IntSlider", iSliderValue, 0, 10);
        // MinMaxSlider
        EditorGUILayout.MinMaxSlider("MinMaxSlider", ref mmSliderMinValue, ref mmSliderMaxValue, mmSliderMinLimit, mmSliderMaxLimit);
        
        // DisabledScope
        using (new EditorGUI.DisabledScope(true))
        {
            EditorGUILayout.IntField("IntField", 0);
        }
        
        // HelpBox
        EditorGUILayout.HelpBox("HelpBox None", MessageType.None);
        EditorGUILayout.HelpBox("HelpBox Info", MessageType.Info);
        EditorGUILayout.HelpBox("HelpBox Warning", MessageType.Warning);
        EditorGUILayout.HelpBox("HelpBox Error", MessageType.Error);
        EditorGUILayout.Toggle("Toggle", true);
        EditorGUILayout.HelpBox("HelpBox Error", MessageType.Error, false);
        
        // BoundsField
        // BoundsIntField
        // ColorField
        // CurveField
        // EnumFlagsField
        // GradientField
        // LayerField
        // MaskField
        // PropertyField
        // RectField
        // RectIntField
        // TagField
        // Vector2Field
        // Vector2IntField
        // Vector3Field
        // Vector3IntField
        // Vector4Field
        
        // DropdownButton
        // Popup
        // IntPopup
        // EnumPopup
        // InspectorTitlebar
    }
    private void Layout()
    {
        // Space
        GUILayout.Label("Space", header2Style);
        GUILayout.Label("GUILayout.Space", header3Style);
        using (new GUILayout.HorizontalScope())
        {
            GUILayout.Label("Label1");
            GUILayout.Space(20f);
            GUILayout.Label("Label2");
            GUILayout.Space(20f);
            GUILayout.Label("Label3");
        }
        GUILayout.Label("EditorGUILayout.Space", header3Style);
        using (new GUILayout.HorizontalScope())
        {
            GUILayout.Label("Label1");
            EditorGUILayout.Space(20f);
            GUILayout.Label("Label2");
            EditorGUILayout.Space(20f);
            GUILayout.Label("Label3");
        }
        
        // Box
        {
            EditorGUILayout.Space(10f);
            GUILayout.Label("Box", header2Style);
            GUILayout.Box("Box");
            // 水平線
            GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        }
        
        // VerticalScope
        EditorGUILayout.Space(10f);
        GUILayout.Label("VerticalScope", header2Style);
        GUILayout.Label("GUILayout.VerticalScope", header3Style);
        using (new GUILayout.VerticalScope())
        {
            GUILayout.Label("Label1");
            GUILayout.Label("Label2");
            GUILayout.Label("Label3");
        }
        using (new GUILayout.VerticalScope())
        {
            EditorGUILayout.LabelField("Label1");
            EditorGUILayout.LabelField("Label2");
            EditorGUILayout.LabelField("Label3");
        }
        GUILayout.Label("EditorGUILayout.VerticalScope", header3Style);
        using (new EditorGUILayout.VerticalScope())
        {
            GUILayout.Label("Label1");
            GUILayout.Label("Label2");
            GUILayout.Label("Label3");
        }
        using (new EditorGUILayout.VerticalScope())
        {
            EditorGUILayout.LabelField("Label1");
            EditorGUILayout.LabelField("Label2");
            EditorGUILayout.LabelField("Label3");
        }
        
        // HorizontalScope
        EditorGUILayout.Space(10f);
        GUILayout.Label("HorizontalScope", header2Style);
        GUILayout.Label("GUILayout.HorizontalScope", header3Style);
        using (new GUILayout.HorizontalScope())
        {
            GUILayout.Label("Label1");
            GUILayout.Label("Label2");
            GUILayout.Label("Label3");
        }
        using (new GUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField("Label1");
            EditorGUILayout.LabelField("Label2");
            EditorGUILayout.LabelField("Label3");
        }
        GUILayout.Label("EditorGUILayout.HorizontalScope", header3Style);
        using (new EditorGUILayout.HorizontalScope())
        {
            GUILayout.Label("Label1");
            GUILayout.Label("Label2");
            GUILayout.Label("Label3");
        }
        using (new EditorGUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField("Label1");
            EditorGUILayout.LabelField("Label2");
            EditorGUILayout.LabelField("Label3");
        }
        
        // Indent
        EditorGUILayout.Space(10f);
        GUILayout.Label("Indent", header2Style);
        using (new EditorGUI.IndentLevelScope())
        {
            // 引数なしでindentLevel++と同じ、引数でレベルを指定できる、デフォルト（最上位）は0
            EditorGUILayout.LabelField("indentLevelScope");
            using (new EditorGUI.IndentLevelScope())
            {
                // 入れ子にするとさらにindentLevel++
                EditorGUILayout.LabelField("indentLevelScope");
            }
        }

        {
            // デフォルトは0レベル
            EditorGUILayout.LabelField("indentLevel = default");
            EditorGUI.indentLevel = 2;
            EditorGUILayout.LabelField("indentLevel = 2");
            EditorGUI.indentLevel = 0;
            EditorGUILayout.LabelField("indentLevel = 0");

            // インクリメントでもインデントできる
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField("indentLevel++");
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField("indentLevel++");
            // GUILayoutには影響しない
            GUILayout.Label("GUILayout is not effect");
            EditorGUI.indentLevel--;
            EditorGUILayout.LabelField("indentLevel--");
            EditorGUI.indentLevel--;
            EditorGUILayout.LabelField("indentLevel--");
        }

        // Alignment
        EditorGUILayout.Space(10f);
        GUILayout.Label("Alignment", header2Style);
        using (new GUILayout.HorizontalScope())
        {
            // 左寄せ
            GUILayout.Label("Left");
            GUILayout.Label("Right");
            GUILayout.FlexibleSpace();
        }
        using (new GUILayout.HorizontalScope())
        {
            // 両端寄せ
            GUILayout.Label("Left");
            GUILayout.FlexibleSpace();
            GUILayout.Label("Right");
        }
        using (new GUILayout.HorizontalScope())
        {
            // 右寄せ
            GUILayout.FlexibleSpace();
            GUILayout.Label("Left");
            GUILayout.Label("Right");
        }
        using (new GUILayout.HorizontalScope())
        {
            // 等間隔
            GUILayout.FlexibleSpace();
            GUILayout.Label("Left");
            GUILayout.FlexibleSpace();
            GUILayout.Label("Right");
            GUILayout.FlexibleSpace();
        }
        using (new GUILayout.HorizontalScope())
        {
            // 中央寄せ
            GUILayout.FlexibleSpace();
            GUILayout.Label("Left");
            GUILayout.Label("Right");
            GUILayout.FlexibleSpace();
        }
        using (new GUILayout.HorizontalScope())
        {
            // 中央やや左寄せ
            GUILayout.FlexibleSpace();
            GUILayout.Label("Left");
            GUILayout.Label("Right");
            GUILayout.FlexibleSpace();
            GUILayout.FlexibleSpace();
        }
        
        // Foldout
        EditorGUILayout.Space(10f);
        GUILayout.Label("Foldout", header2Style);
        foldout = EditorGUILayout.Foldout(foldout, "Foldout");
        if (foldout)
        {
            GUILayout.Label("Label1");
            GUILayout.Label("Label2");
            GUILayout.Label("Label3");
        }
        
        // ScrollViewScope
        EditorGUILayout.Space(10f);
        GUILayout.Label("ScrollViewScope", header2Style);
        using (var scope = new EditorGUILayout.ScrollViewScope(scrollPosScope, GUILayout.Width(120f), GUILayout.Height(60f)))
        {
            // options でサイズや最大サイズ、最小サイズなどを指定可能
            scrollPosScope = scope.scrollPosition;
            GUILayout.Label("Label1");
            GUILayout.Label("Label2");
            GUILayout.Label("Label3");
            GUILayout.Label("Label4");
            GUILayout.Label("Label5");
        }
        
        // Layout Option
        EditorGUILayout.Space(10f);
        GUILayout.Label("Layout Option", header2Style);
        // optionsでWidthやHeightを指定できる
        GUILayout.Box("Box 100x100", GUILayout.Width(100f), GUILayout.Height(100f));
        GUILayout.Box(
            "Once upon a time, there lived an old couple in a small village. One day the old wife was washing her clothes in the river when a huge peach came tumbling down the stream.",
            GUILayout.MinWidth(100f), GUILayout.MinHeight(100f), GUILayout.MaxWidth(200f), GUILayout.MaxHeight(100f));
        GUILayout.Box("Box ExpandWidth", GUILayout.ExpandWidth(true));
        GUILayout.Box("Box ExpandHeight", GUILayout.ExpandHeight(true));
    }
    private void Style()
    {
        GUILayout.Label("FontSize", header2Style);
        // FontSizeは0を指定するとデフォルトサイズになる、12と同等
        GUILayout.Label("FontSize:0(=12)", new GUIStyle{ fontSize = 0 });
        GUILayout.Label("FontSize:12", new GUIStyle{ fontSize = 12 });
        GUILayout.Label("FontSize:18", new GUIStyle{ fontSize = 18 });
        GUILayout.Label("FontSize:24", new GUIStyle{ fontSize = 24 });
        GUILayout.Label("FontSize:30", new GUIStyle{ fontSize = 30 });
        GUILayout.Label("FontSize:36", new GUIStyle{ fontSize = 36 });
        
        
        EditorGUILayout.Space(10f);
        GUILayout.Label("FontStyle", header2Style);
        GUILayout.Label("FontStyle:Bold", new GUIStyle{ fontStyle = FontStyle.Bold });
        GUILayout.Label("FontStyle:Italic", new GUIStyle{ fontStyle = FontStyle.Italic });
        GUILayout.Label("FontStyle:BoldAndItalic", new GUIStyle{ fontStyle = FontStyle.BoldAndItalic });
        
        
        EditorGUILayout.Space(10f);
        GUILayout.Label("GUILayout:Color", header2Style);
        using (new GUILayout.HorizontalScope())
        {
            // EditorStylesからUIパーツで使われているスタイルを取得できる
            // Labelのデフォルト色は (0.769,0.769,0.769)
            GUILayout.Label("Label");
            GUILayout.Label("Label", new GUIStyle(EditorStyles.label));
            GUILayout.Label("Label", new GUIStyle { normal = new GUIStyleState { textColor = new Color(0.769f, 0.769f, 0.769f) } });
        }

        using (new GUILayout.HorizontalScope())
        {
            // EditorStylesからUIパーツで使われているスタイルを取得できる
            GUILayout.Label("boldLabel", new GUIStyle(EditorStyles.boldLabel));
            GUILayout.Label("largeLabel", new GUIStyle(EditorStyles.largeLabel));
            GUILayout.Label("linkLabel", new GUIStyle(EditorStyles.linkLabel));
            GUILayout.Label("miniButton", new GUIStyle(EditorStyles.miniButton));
            GUILayout.Label("popup", new GUIStyle(EditorStyles.popup));
        }

        using (new GUILayout.HorizontalScope())
        {
            // モノクロ
            GUILayout.Label("Label", new GUIStyle { normal = new GUIStyleState { textColor = new Color(0, 0, 0) } });
            GUILayout.Label("Label", new GUIStyle { normal = new GUIStyleState { textColor = new Color(0.25f, 0.25f, 0.25f) } });
            GUILayout.Label("Label", new GUIStyle { normal = new GUIStyleState { textColor = new Color(0.5f, 0.5f, 0.5f) } });
            GUILayout.Label("Label", new GUIStyle { normal = new GUIStyleState { textColor = new Color(0.75f, 0.75f, 0.75f) } });
            GUILayout.Label("Label", new GUIStyle { normal = new GUIStyleState { textColor = new Color(1, 1, 1) } });
        }
        using (new GUILayout.HorizontalScope())
        {
            // 定義されたColor 1
            GUILayout.Label("Label", new GUIStyle { normal = new GUIStyleState { textColor = Color.black } });
            GUILayout.Label("Label", new GUIStyle { normal = new GUIStyleState { textColor = Color.grey } });
            GUILayout.Label("Label", new GUIStyle { normal = new GUIStyleState { textColor = Color.gray } });
            GUILayout.Label("Label", new GUIStyle { normal = new GUIStyleState { textColor = Color.clear } });
            GUILayout.Label("Label", new GUIStyle { normal = new GUIStyleState { textColor = Color.white } });
        }
        using (new GUILayout.HorizontalScope())
        {
            // 定義されたColor 2
            GUILayout.Label("Label", new GUIStyle { normal = new GUIStyleState { textColor = Color.red } });
            GUILayout.Label("Label", new GUIStyle { normal = new GUIStyleState { textColor = Color.green } });
            GUILayout.Label("Label", new GUIStyle { normal = new GUIStyleState { textColor = Color.blue } });
            GUILayout.Label("Label", new GUIStyle { normal = new GUIStyleState { textColor = Color.yellow } });
            GUILayout.Label("Label", new GUIStyle { normal = new GUIStyleState { textColor = Color.magenta } });
            GUILayout.Label("Label", new GUIStyle { normal = new GUIStyleState { textColor = Color.cyan } });
        }
        
        EditorGUILayout.Space(10f);
        GUILayout.Label("EditorGUILayout:Color", header2Style);
        using (new GUILayout.HorizontalScope())
        {
            // EditorGUILayoutも同様
            EditorGUILayout.LabelField("Label", new GUIStyle { normal = new GUIStyleState { textColor = new Color(0, 0, 0) } });
            EditorGUILayout.LabelField("Label", new GUIStyle { normal = new GUIStyleState { textColor = new Color(0.25f, 0.25f, 0.25f) } });
            EditorGUILayout.LabelField("Label", new GUIStyle { normal = new GUIStyleState { textColor = new Color(0.5f, 0.5f, 0.5f) } });
            EditorGUILayout.LabelField("Label", new GUIStyle { normal = new GUIStyleState { textColor = new Color(0.75f, 0.75f, 0.75f) } });
            EditorGUILayout.LabelField("Label", new GUIStyle { normal = new GUIStyleState { textColor = new Color(1, 1, 1) } });
        }
        
        EditorGUILayout.Space(10f);
        GUILayout.Label("HorizontalScope with style", header2Style);
        using (new GUILayout.HorizontalScope("Box"))
        {
            GUILayout.Label("Box1");
            GUILayout.Label("Box2");
            GUILayout.Label("Box3");
            GUILayout.Button("Box4");
        }
        using (new GUILayout.HorizontalScope("HelpBox"))
        {
            GUILayout.Label("HelpBox1");
            GUILayout.Label("HelpBox2");
            GUILayout.Label("HelpBox3");
            GUILayout.Button("HelpBox4");
        }
    }
    private void Utility()
    {
        // Dialog
        GUILayout.Label("Dialog", header2Style);
        using (new GUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Dialog"))
            {
                // okならtrue, cancelならfalse
                var result = EditorUtility.DisplayDialog("Title", "Message.", "OK Label", "Cancel Label");
                Debug.Log(result + " selected.");
            }
            if (GUILayout.Button("DialogComplex"))
            {
                // okなら0, cancelなら1, altなら2
                var result = EditorUtility.DisplayDialogComplex("Title", "Message.", "OK Label", "Cancel Label", "3rd button");
                Debug.Log(result + " selected.");
            }
        }
        
        // OpenFolderPanel / OpenFilePanel
        GUILayout.Label("OpenFolderPanel / OpenFilePanel", header2Style);
        GUILayout.Label(selectedPath);
        using (new GUILayout.HorizontalScope())
        {
            if (GUILayout.Button("FolderPanel", GUILayout.Width(100f)))
            {
                selectedPath = EditorUtility.OpenFolderPanel("Target folder", Application.dataPath, string.Empty);
            }
            if (GUILayout.Button("FilePanel", GUILayout.Width(100f)))
            {
                selectedPath = EditorUtility.OpenFilePanel("Target file", Application.dataPath, string.Empty);
            }
            if (GUILayout.Button("FilePanelWithFilters", GUILayout.Width(150f)))
            {
                selectedPath = EditorUtility.OpenFilePanelWithFilters("Target file", Application.dataPath, new []{ "Image files", "png,jpg,jpeg", "All files", "*" });
            }
        }
        
        // SaveFolderPanel / SaveFilePanel
        GUILayout.Label("SaveFolderPanel / SaveFilePanel", header2Style);
        GUILayout.Label(selectedPath);
        using (new GUILayout.HorizontalScope())
        {
            if (GUILayout.Button("FolderPanel", GUILayout.Width(100f)))
            {
                selectedPath = EditorUtility.SaveFolderPanel("Target folder", Application.dataPath, string.Empty);
            }
            if (GUILayout.Button("FilePanel", GUILayout.Width(100f)))
            {
                // パスが作れるだけで保存は別途実装が必要
                selectedPath = EditorUtility.SaveFilePanel("Target file", Application.dataPath, "DefaultName", "txt");
            }
            if (GUILayout.Button("SaveFilePanelInProject", GUILayout.Width(150f)))
            {
                // Assetsからの相対パスが取得できる
                selectedPath = EditorUtility.SaveFilePanelInProject("Target file", "DefaultName", "txt", "message");
            }
        }
        
        // PopupMenu
        GUILayout.Label("PopupMenu", header2Style);
        if (GUILayout.Button("PopupMenu", GUILayout.Width(100f)))
        {
            var popupPosition = GUIUtility.GUIToScreenPoint(popupButtonRect.center);
            EditorUtility.DisplayPopupMenu(new Rect(popupPosition, Vector2.zero), "Assets/Create", null);
        }
        if (Event.current.type == EventType.Repaint) 
            popupButtonRect = GUILayoutUtility.GetLastRect();
        
        // プログレスバー
        GUILayout.Label("ProgressBar", header2Style);
        if (guiEventCount is >= 0 and <= 20)
        {
            if (cancelablePBar)
                EditorUtility.DisplayCancelableProgressBar("Title", "Message", (float)guiEventCount / 20);
            else
                EditorUtility.DisplayProgressBar("Title", "Message", (float)guiEventCount / 20);
            
            guiEventCount++;
        }
        else if (guiEventCount > 20)
        {
            EditorUtility.ClearProgressBar();
            guiEventCount = -1;
        }
        else
        {
            if (GUILayout.Button("Progressbar"))
            {
                guiEventCount = 0;
                cancelablePBar = false;
            }
            if (GUILayout.Button("CancelableProgressBar"))
            {
                guiEventCount = 0;
                cancelablePBar = true;
            }
        }
    }
    private void Ex()
    {
        // FocusControl
        GUILayout.Label("FocusControl", header2Style);
        using (new GUILayout.HorizontalScope())
        {
            EditorGUILayout.TextField("FocusControl", focusControlPath);
            if (GUILayout.Button("Select", GUILayout.Width(80f)))
            {
                GUI.FocusControl("");
                focusControlPath = EditorUtility.OpenFolderPanel("Root", Application.dataPath, string.Empty);
            }
        }

        // Save Data
        GUILayout.Label("Save Data", header2Style);
        saveDataText = EditorGUILayout.TextField("Text to save", saveDataText);
        using (new GUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Save"))
            {
                GUI.FocusControl("");
                var data = ScriptableObject.CreateInstance<SampleScriptableObject>();
                data.Text = saveDataText;
                SaveData(data);
            }
            if (GUILayout.Button("Load"))
            {
                GUI.FocusControl("");
                var data = LoadData();
                saveDataText = data.Text;
            }
            
        }

        // Styles
        GUILayout.Label("GUIStyle", header2Style);
        GUILayout.Button("Button", "toolbarbutton");
        GUILayout.Button("Button", "ToolbarButtonFlat");
        GUILayout.Button("Button", "toolbarbuttonLeft");
        GUILayout.Button("Button", "toolbarbuttonRight");
        
        // OutputGUIStyles();
        // OutputEditorStyles();
    }
    
    private string saveDataPath = "Assets/SamplesWindowData.asset";
    private void SaveData(SampleScriptableObject data)
    {
        if (!AssetDatabase.Contains(data))
        {
            AssetDatabase.CreateAsset(data, saveDataPath);
        }

        data.hideFlags = HideFlags.NotEditable;
        EditorUtility.SetDirty(data);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    private SampleScriptableObject LoadData()
    {
        return AssetDatabase.LoadAssetAtPath<SampleScriptableObject>(saveDataPath);
    }
    
    private void OutputEditorStyles()
    {
        var sb = new StringBuilder();
        var list = typeof(EditorStyles).GetProperties().Select(x => x.Name).OrderBy(x => x);
        foreach (var n in list)
        {
            sb.AppendLine(n);
        }

        Debug.Log("EditorStyles:\r\n" + sb.ToString());
    }
    private void OutputGUIStyles()
    {
        var skins = new List<string>();
        foreach (var obj in GUI.skin)
        {
            var skin = obj as GUIStyle;
            skins.Add(skin.name);
        }

        var sb = new StringBuilder();
        var list = skins.OrderBy(x => x);
        foreach (var n in list)
        {
            sb.AppendLine(n);
        }

        Debug.Log("GUIStyles:\r\n" + sb.ToString());
    }

}
