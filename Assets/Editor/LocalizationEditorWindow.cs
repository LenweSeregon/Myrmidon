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
        #endregion

        [MenuItem("Window/Myrmidon/Localization")]
        private static void OpenLocalizationWindow()
        {
            LocalizationEditorWindow window = GetWindow<LocalizationEditorWindow>();
            window.titleContent = new GUIContent(WINDOW_NAME);
        }
        
        private void OnGUI() 
        {
            TestLayoutVertical();
            //TestLayoutVerticalThenHorizontal();
        }

        private void TestLayoutVertical()
        {
            MyrmidonLayoutElement panel01 = new MyrmidonLayoutElement(0, 0, 1, 10); //20
            MyrmidonLayoutElement panel02 = new MyrmidonLayoutElement(0, 0, 1, 10); //30
            MyrmidonLayoutElement panel03 = new MyrmidonLayoutElement(0, 0, 1, 10); //50
            MyrmidonLayoutElement panel04 = new MyrmidonLayoutElement(0, 0, 1, 70); //50

            panel01.AssignBackgroundColor(Color.blue);
            panel02.AssignBackgroundColor(Color.red);
            panel03.AssignBackgroundColor(Color.green);
            panel04.AssignBackgroundColor(Color.yellow);

            MyrmidonEditorLayout panelLayout = new MyrmidonEditorVerticalLayout(new MyrmidonLayoutElement[]{panel01, panel02, panel03, panel04}, false, false);
            panelLayout.AssignRect(new Rect(0, 0,position.width, position.height));
            panelLayout.SetPadding(30, 30, 30, 30);
            panelLayout.AssignBackgroundColor(Color.cyan);

            panelLayout.ComputeRects();
            panelLayout.Draw();
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

            MyrmidonEditorLayout panelLayout = new MyrmidonEditorHorizontalLayout(new MyrmidonLayoutElement[]{panel01, panel02, panel03, panel04}, false, false);
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
            //MyrmidonLayoutElement panel04 = new MyrmidonLayoutElement(0, 0, 10, 1); //50
            MyrmidonEditorLayout panel04 = new MyrmidonEditorHorizontalLayout(new MyrmidonLayoutElement[]{panel04_01, panel04_02, panel04_03}, true, true, 0, 0, 1, 1);
            panel04.SetPadding(10, 10, 10, 10);
            panel04.Spacing = 5f;

            panel01.AssignBackgroundColor(Color.blue);
            panel02.AssignBackgroundColor(Color.red);
            panel03.AssignBackgroundColor(Color.green);
            panel04.AssignBackgroundColor(Color.yellow);

            MyrmidonEditorLayout panelLayout = new MyrmidonEditorVerticalLayout(new MyrmidonLayoutElement[]{panel01, panel02, panel03, panel04}, false, false);
            panelLayout.Spacing = 10f;
            panelLayout.AssignRect(new Rect(0, 0,position.width, position.height));
            panelLayout.SetPadding(30, 30, 30, 30);
            panelLayout.AssignBackgroundColor(Color.cyan);

            panelLayout.Draw();
        }
    }
}