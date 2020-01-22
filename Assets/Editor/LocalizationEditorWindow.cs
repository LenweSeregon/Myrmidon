namespace Myrmidon.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;

    public class LocalizationEditorWindow : EditorWindow
    {
        #region Constantes
        private const string WINDOW_NAME = "Localization";
        private bool init = false;
        private MyrmidonEditorLayout mainLayout;
        #endregion

        #region Internals Fields
        private float m_lastWidthRegistered;
        private float m_lastHeightRegistered;
        #endregion

        [MenuItem("Window/Myrmidon/Localization")]
        private static void OpenLocalizationWindow()
        {
            LocalizationEditorWindow window = GetWindow<LocalizationEditorWindow>();
            window.titleContent = new GUIContent(WINDOW_NAME);
            window.m_lastWidthRegistered = window.position.width;
            window.m_lastHeightRegistered = window.position.height;
            window.InitializeWindow();
        }

        private void Update()
        {
            float currentWidth = position.width;
            float currentHeight = position.height;

            if(currentWidth != m_lastWidthRegistered || currentHeight != m_lastHeightRegistered)
            {
                float deltaWidth =  currentWidth - m_lastWidthRegistered;
                float deltaHeight = currentHeight - m_lastHeightRegistered;
                m_lastWidthRegistered = currentWidth;
                m_lastHeightRegistered = currentHeight;
                Resizing(deltaWidth, deltaHeight);
                Repaint();
            }
        }

        private void InitializeWindow()
        {
            MyrmidonLayoutElement panel01 = new MyrmidonLayoutElement(0, 0, 1, 10); //20
            MyrmidonLayoutElement panel02 = new MyrmidonLayoutElement(0, 0, 1, 10); //30
            MyrmidonLayoutElement panel03 = new MyrmidonLayoutElement(0, 0, 1, 10); //50
            MyrmidonLayoutElement panel04 = new MyrmidonLayoutElement(0, 0, 1, 70); //50

            panel01.AssignBackgroundColor(Color.blue);
            panel02.AssignBackgroundColor(Color.red);
            panel03.AssignBackgroundColor(Color.green);
            panel04.AssignBackgroundColor(Color.yellow);

            MyrmidonEditorLayout panelLayout = new MyrmidonEditorVerticalLayout(new List<MyrmidonLayoutElement> { panel01, panel02, panel03, panel04 }, false, false, true);
            panelLayout.AssignRect(new Rect(0, 0, position.width, position.height));
            panelLayout.SetPadding(30, 30, 30, 30);
            panelLayout.AssignBackgroundColor(Color.cyan);
            panelLayout.ComputeRects();

            mainLayout = panelLayout;
        }

        private void Resizing(float deltaWidth, float deltaHeight)
        {
            mainLayout.ProcessResizing(deltaWidth, deltaHeight);
        }
        
        private void OnGUI() 
        {
            if(mainLayout != null)
            {
                mainLayout.Draw();
                mainLayout.ProcessEvents(Event.current);
                if(GUI.changed)
                {
                    Repaint();
                }
            }
                
            //TestLayoutVertical();
            //TestLayoutVerticalThenHorizontal();
        }

        private void TestLayoutVertical()
        {

            /*panelLayout.Draw();
            panelLayout.ProcessEvents(Event.current);
            if(GUI.changed) 
                Repaint();*/
        }

        private void TestLayoutHorizontal()
        {
            MyrmidonLayoutElement panel01 = new MyrmidonLayoutElement(0, 0, 10, 1); //20
            MyrmidonLayoutElement panel02 = new MyrmidonLayoutElement(0, 0, 10, 1); //30
            MyrmidonLayoutElement panel03 = new MyrmidonLayoutElement(0, 0, 10, 1); //50
            MyrmidonLayoutElement panel04 = new MyrmidonLayoutElement(0, 0, 70, 1); //50

            panel01.AssignBackgroundColor(Color.blue);
            panel02.AssignBackgroundColor(Color.red);
            panel03.AssignBackgroundColor(Color.green);
            panel04.AssignBackgroundColor(Color.yellow);

            MyrmidonEditorLayout panelLayout = new MyrmidonEditorHorizontalLayout(new List<MyrmidonLayoutElement>{panel01, panel02, panel03, panel04}, false, false, true);
            panelLayout.AssignRect(new Rect(0, 0,position.width, position.height));
            panelLayout.SetPadding(30, 30, 30, 30);
            panelLayout.AssignBackgroundColor(Color.cyan);

            panelLayout.ComputeRects();
            panelLayout.Draw();
        }

        private void TestLayoutVerticalThenHorizontal()
        {
            MyrmidonLayoutElement panel04_01 = new MyrmidonLayoutElement(0,0, 1, 10);
            MyrmidonLayoutElement panel04_02 = new MyrmidonLayoutElement(0,0, 1, 10);
            MyrmidonLayoutElement panel04_03 = new MyrmidonLayoutElement(0,0, 1, 10);
            panel04_01.AssignBackgroundColor(Color.magenta);
            panel04_02.AssignBackgroundColor(Color.black);
            panel04_03.AssignBackgroundColor(Color.grey);


            MyrmidonLayoutElement panel01 = new MyrmidonLayoutElement(0, 0, 1, 1); //20
            MyrmidonLayoutElement panel02 = new MyrmidonLayoutElement(0, 0, 1, 1); //30
            MyrmidonLayoutElement panel03 = new MyrmidonLayoutElement(0, 0, 1, 1); //50
            MyrmidonEditorLayout panel04 = new MyrmidonEditorHorizontalLayout(new List<MyrmidonLayoutElement>{panel04_01, panel04_02, panel04_03}, true, true, false, 0, 0, 1, 1);
            panel04.SetPadding(10, 10, 10, 10);
            panel04.Spacing = 5f;

            panel01.AssignBackgroundColor(Color.blue);
            panel02.AssignBackgroundColor(Color.red);
            panel03.AssignBackgroundColor(Color.green);
            panel04.AssignBackgroundColor(Color.yellow);

            MyrmidonEditorLayout panelLayout = new MyrmidonEditorVerticalLayout(new List<MyrmidonLayoutElement>{panel01, panel02, panel03, panel04}, false, false, false);
            panelLayout.Spacing = 10f;
            panelLayout.AssignRect(new Rect(0, 0,position.width, position.height));
            panelLayout.SetPadding(30, 30, 30, 30);
            panelLayout.AssignBackgroundColor(Color.cyan);

            panelLayout.Draw();
        }
    }
}