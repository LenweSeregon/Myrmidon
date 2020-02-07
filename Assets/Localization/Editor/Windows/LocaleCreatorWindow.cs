namespace Myrmidon.Localization.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    using Myrmidon.Editor;


    public class LocaleCreatorWindow : EditorWindow
    {
        private const string WINDOW_NAME = "Locale Creator";

        private MyrmidonEditorLayout mainLayout;
        private float m_lastWidthRegistered;
        private float m_lastHeightRegistered;

        [MenuItem("Window/Myrmidon/Localization/Locale Creator")]
        private static void OpenLocaleCreatorWindow()
        {
            LocaleCreatorWindow window = GetWindow<LocaleCreatorWindow>();
            window.titleContent = new GUIContent(WINDOW_NAME);
            window.m_lastWidthRegistered = window.position.width;
            window.m_lastHeightRegistered = window.position.height;
            window.InitializeWindow();
        }

        private void OnValidate()
        {
            InitializeWindow();
        }

        private void Update()
        {
            float currentWidth = position.width;
            float currentHeight = position.height;

            if (currentWidth != m_lastWidthRegistered || currentHeight != m_lastHeightRegistered)
            {
                float deltaWidth = currentWidth - m_lastWidthRegistered;
                float deltaHeight = currentHeight - m_lastHeightRegistered;
                m_lastWidthRegistered = currentWidth;
                m_lastHeightRegistered = currentHeight;
                Resizing(deltaWidth, deltaHeight);
                Repaint();
            }
        }

        private void InitializeWindow()
        {
            float standardHeight = EditorGUIUtility.singleLineHeight;

            MyrmidonEditorLayoutElement searchBar = new MyrmidonEditorLayoutElement(0, standardHeight, 1, 0);
            searchBar.AssignDrawAction(SearchBar);
            //searchBar.AssignBackgroundColor(Color.red);
            MyrmidonEditorLayoutElement empty = new MyrmidonEditorLayoutElement(0,0,1,1);
            empty.AssignBackgroundColor(Color.blue);

            MyrmidonEditorLayout layout = new MyrmidonEditorVerticalLayout(new List<MyrmidonEditorLayoutElement> { searchBar, empty }, true, true, false);
            layout.AssignRect(new Rect(0, 0, position.width, position.height));
            layout.Spacing = 0f;
            layout.SetPadding(10, 10, 10, 10);
            layout.ComputeRects();
            mainLayout = layout;
            /*MyrmidonEditorLayoutElement panel01 = new MyrmidonEditorLayoutElement(0, 0, 1, 10); //20
            MyrmidonEditorLayoutElement panel02 = new MyrmidonEditorLayoutElement(0, 0, 1, 10); //30
            MyrmidonEditorLayoutElement panel03 = new MyrmidonEditorLayoutElement(0, 0, 1, 10); //50
            MyrmidonEditorLayoutElement panel04 = new MyrmidonEditorLayoutElement(0, 0, 1, 70); //50

            panel01.AssignBackgroundColor(Color.blue);
            panel02.AssignBackgroundColor(Color.red);
            panel03.AssignBackgroundColor(Color.green);
            panel04.AssignBackgroundColor(Color.yellow);

            MyrmidonEditorLayout panelLayout = new MyrmidonEditorHorizontalLayout(new List<MyrmidonEditorLayoutElement> { panel01, panel02, panel03, panel04 }, false, false, true);
            panelLayout.AssignRect(new Rect(0, 0, position.width, position.height));
            panelLayout.SetPadding(30, 30, 30, 30);
            panelLayout.AssignBackgroundColor(Color.cyan);
            panelLayout.ComputeRects();

            mainLayout = panelLayout;*/
        }

        private void Resizing(float deltaWidth, float deltaHeight)
        {
            if (mainLayout != null)
            {
                mainLayout.ProcessResizing(deltaWidth, deltaHeight);
            }
        }

        private void OnGUI()
        {
            if (mainLayout != null)
            {
                mainLayout.Draw();
                bool repaintEvents = mainLayout.ProcessEvents(Event.current);
                if (repaintEvents || GUI.changed)
                {
                    Repaint();
                }
            }

            //TestLayoutVertical();
            //TestLayoutVerticalThenHorizontal();
        }

        private void SearchBar(Rect rect)
        {
            string searchString = "";

            EditorGUIUtility.labelWidth = 0;
            EditorGUIUtility.fieldWidth = 0;

            GUILayout.BeginHorizontal(/*GUI.skin.FindStyle("Toolbar")*/);
            searchString = GUILayout.TextField(searchString, GUI.skin.FindStyle("ToolbarSeachTextField"));
            if (GUILayout.Button("", GUI.skin.FindStyle("ToolbarSeachCancelButton")))
            {
                // Remove focus if cleared
                searchString = "";
                GUI.FocusControl(null);
            }
            GUILayout.EndHorizontal();
        }
    }
}

