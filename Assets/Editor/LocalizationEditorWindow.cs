namespace Myrmidon.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;

    public class LocalizationEditorWindow : MyrmidonEditorWindow
    {
        #region Constantes
        private const string WINDOW_NAME = "Localization";
        #endregion
        
        #region Fields
        #endregion

        [MenuItem("Window/Myrmidon/Localization")]
        private static void OpenLocalizationWindow()
        {
            MyrmidonEditorWindow window = GetWindow<LocalizationEditorWindow>();
            window.titleContent = new GUIContent(WINDOW_NAME);
            window.InitializeWindow();
        }

        public override void InitializeWindow()
        {
            base.InitializeWindow();
            
            MyrmidonEditorLayoutElement panel01 = new MyrmidonEditorLayoutElement(0, 0, 1, 10); //20
            MyrmidonEditorLayoutElement panel02 = new MyrmidonEditorLayoutElement(0, 0, 1, 10); //30
            MyrmidonEditorLayoutElement panel03 = new MyrmidonEditorLayoutElement(0, 0, 1, 10); //50
            MyrmidonEditorLayoutElement panel04 = new MyrmidonEditorLayoutElement(0, 0, 1, 70); //50

            panel01.AssignBackgroundColor(Color.blue);
            panel02.AssignBackgroundColor(Color.red);
            panel03.AssignBackgroundColor(Color.green);
            panel04.AssignBackgroundColor(Color.yellow);

            MyrmidonEditorLayout panelLayout = new MyrmidonEditorHorizontalLayout(new List<MyrmidonEditorLayoutElement> { panel01, panel02, panel03, panel04 }, false, false, true);
            panelLayout.SetRect(new Rect(0, 0, position.width, position.height));
            panelLayout.SetPadding(30, 30, 30, 30);
            panelLayout.AssignBackgroundColor(Color.cyan);
            panelLayout.ComputeRects();

            _mWindowContainer = panelLayout;
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
            MyrmidonEditorLayoutElement panel01 = new MyrmidonEditorLayoutElement(0, 0, 10, 1); //20
            MyrmidonEditorLayoutElement panel02 = new MyrmidonEditorLayoutElement(0, 0, 10, 1); //30
            MyrmidonEditorLayoutElement panel03 = new MyrmidonEditorLayoutElement(0, 0, 10, 1); //50
            MyrmidonEditorLayoutElement panel04 = new MyrmidonEditorLayoutElement(0, 0, 70, 1); //50

            panel01.AssignBackgroundColor(Color.blue);
            panel02.AssignBackgroundColor(Color.red);
            panel03.AssignBackgroundColor(Color.green);
            panel04.AssignBackgroundColor(Color.yellow);

            MyrmidonEditorLayout panelLayout = new MyrmidonEditorHorizontalLayout(new List<MyrmidonEditorLayoutElement>{panel01, panel02, panel03, panel04}, false, false, true);
            panelLayout.SetRect(new Rect(0, 0,position.width, position.height));
            panelLayout.SetPadding(30, 30, 30, 30);
            panelLayout.AssignBackgroundColor(Color.cyan);

            panelLayout.ComputeRects();
            panelLayout.Draw();
        }

        private void TestLayoutVerticalThenHorizontal()
        {
            MyrmidonEditorLayoutElement panel04_01 = new MyrmidonEditorLayoutElement(0,0, 1, 10);
            MyrmidonEditorLayoutElement panel04_02 = new MyrmidonEditorLayoutElement(0,0, 1, 10);
            MyrmidonEditorLayoutElement panel04_03 = new MyrmidonEditorLayoutElement(0,0, 1, 10);
            panel04_01.AssignBackgroundColor(Color.magenta);
            panel04_02.AssignBackgroundColor(Color.black);
            panel04_03.AssignBackgroundColor(Color.grey);


            MyrmidonEditorLayoutElement panel01 = new MyrmidonEditorLayoutElement(0, 0, 1, 1); //20
            MyrmidonEditorLayoutElement panel02 = new MyrmidonEditorLayoutElement(0, 0, 1, 1); //30
            MyrmidonEditorLayoutElement panel03 = new MyrmidonEditorLayoutElement(0, 0, 1, 1); //50
            MyrmidonEditorLayout panel04 = new MyrmidonEditorHorizontalLayout(new List<MyrmidonEditorLayoutElement>{panel04_01, panel04_02, panel04_03}, true, true, false, 0, 0, 1, 1);
            panel04.SetPadding(10, 10, 10, 10);
            panel04.Spacing = 5f;

            panel01.AssignBackgroundColor(Color.blue);
            panel02.AssignBackgroundColor(Color.red);
            panel03.AssignBackgroundColor(Color.green);
            panel04.AssignBackgroundColor(Color.yellow);

            MyrmidonEditorLayout panelLayout = new MyrmidonEditorVerticalLayout(new List<MyrmidonEditorLayoutElement>{panel01, panel02, panel03, panel04}, false, false, false);
            panelLayout.Spacing = 10f;
            panelLayout.SetRect(new Rect(0, 0,position.width, position.height));
            panelLayout.SetPadding(30, 30, 30, 30);
            panelLayout.AssignBackgroundColor(Color.cyan);

            panelLayout.Draw();
        }
    }
}