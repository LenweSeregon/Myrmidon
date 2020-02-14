namespace Myrmidon.Localization.Editor
{
    using System.IO;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    using Myrmidon.Editor;

    public class LocaleCreatorWindow : MyrmidonEditorWindow
    {
        //==========================================
        // Constantes
        //==========================================
        #region Constantes
        
        private const string WINDOW_NAME = "Locale Creator";
        
        #endregion
		
        //==========================================
        // Fields
        //==========================================
        #region Fields
		
        #region Serialized Fields
        #endregion
		
        #region Internal Fields
        
        private Vector2 _mScrollviewPosition;
        private LocaleCreatorInformations _mInformations;
        
        #endregion
		
        #endregion
		
        //==========================================
        // Methods
        //==========================================
        #region Methods
		
        #region Constructors / Lifecycle
        
        [MenuItem("Window/Myrmidon/Localization/Locale Creator")]
        private static void OpenLocaleCreatorWindow()
        {
	        MyrmidonEditorWindow window = GetWindow<LocaleCreatorWindow>();
	        window.titleContent = new GUIContent(WINDOW_NAME);
	        window.InitializeWindow();
        }
        
        #endregion
		
        #region Publics
		
        #region Commons
        #endregion
        #region Getters / Setters
        #endregion
        #region Abstracts / Virtuals / Overrides

        public override void InitializeWindow()
        {
	        base.InitializeWindow();

	        _mScrollviewPosition = new Vector2(0, 0);
	        _mInformations = new LocaleCreatorInformations();
	        
	        float standardHeight = EditorGUIUtility.singleLineHeight;

	        MyrmidonEditorLayoutElement searchBar = new MyrmidonEditorLayoutElement(0, standardHeight, 1, 0);
	        searchBar.SetDrawAction(SearchBar);
	        searchBar.SetResizables(true, false);
	        
	        MyrmidonEditorLayoutElement scroll = new MyrmidonEditorLayoutElement(0,0,1,1);
	        scroll.SetDrawAction(Scroll);

	        MyrmidonEditorLayoutElement buttonsBar = new MyrmidonEditorLayoutElement(0, standardHeight, 1, 0);
	        buttonsBar.SetDrawAction(Buttons);
	        buttonsBar.SetResizables(true, false);
	        
	        MyrmidonEditorLayoutElement empty = new MyrmidonEditorLayoutElement(0, standardHeight, 1, 0);
	        empty.SetResizables(true, false);
	        
	        MyrmidonEditorLayoutElement createButton = new MyrmidonEditorLayoutElement(0, standardHeight, 1, 0);
	        createButton.SetDrawAction(ButtonCreate);
	        createButton.SetResizables(true, false);
	        
	        //searchBar.AssignBackgroundColor(Color.red);
	        
	        MyrmidonEditorLayout layout = new MyrmidonEditorVerticalLayout(new List<MyrmidonEditorLayoutElement> { searchBar, scroll, buttonsBar, empty, createButton }, true, true, false);
	        layout.SetRect(new Rect(0, 0, position.width, position.height));
	        layout.Spacing = 5f;
	        layout.SetPadding(10, 10, 10, 10);
	        layout.ComputeRects();
	        _mWindowContainer = layout;
        }
        
        #endregion
		
        #endregion
		
        #region Protected / Privates
		
        #region Commons
        
        private void SearchBar(Rect rect)
        {
	        string searchString = "";

	        EditorGUIUtility.labelWidth = 0;
	        EditorGUIUtility.fieldWidth = 0;

	        GUILayout.BeginHorizontal();
	        _mInformations.SearchBarText = GUILayout.TextField(_mInformations.SearchBarText, GUI.skin.FindStyle("ToolbarSeachTextField"));
	        if (GUILayout.Button("", GUI.skin.FindStyle("ToolbarSeachCancelButton")))
	        {
		        _mInformations.SearchBarText = null;
		        GUI.FocusControl(null);
	        }
	        GUILayout.EndHorizontal();
        }

        private void ButtonCreate(Rect rect)
        {
	        GUILayout.BeginHorizontal();
	        GUILayout.FlexibleSpace();
	        if (GUILayout.Button("Create Locales"))
	        {
		        LocalizationSO localization = LocalizationUtility.RetrieveLocalization();
		        string pathLocalization = AssetDatabase.GetAssetPath(localization);
                if(localization != null && string.IsNullOrEmpty(pathLocalization) == false)
                {
                    foreach (LocaleLanguageEditor language in _mInformations.GetLanguagesSelected())
                    {
                        if (localization.LocaleExists(language._mLanguage) == false)
                        {
	                        
                            string pathLocale = Path.Combine(Path.GetDirectoryName(pathLocalization), "Locale - " + language._mName + ".asset");
                            Debug.Log(pathLocale);
                            LocalizationLocaleSO locale = ScriptableObject.CreateInstance<LocalizationLocaleSO>();
                            locale.Init(language._mLanguage, language._mName, language._mCode);
                            localization.AddLocale(locale);
                            AssetDatabase.CreateAsset(locale, pathLocale);
                            EditorUtility.SetDirty(localization);
                            AssetDatabase.SaveAssets();
                            AssetDatabase.Refresh();
                        }
                    }
                }
	        }
	        GUILayout.EndHorizontal();
        }
        
        private void Buttons(Rect rect)
        {
	        string searchString = "";

	        EditorGUIUtility.labelWidth = 0;
	        EditorGUIUtility.fieldWidth = 0;

	        GUILayout.BeginHorizontal();

	        if (GUILayout.Button("Select All"))
	        {
				_mInformations.SelectAllLanguages(true);
	        }

	        if (GUILayout.Button("Deselect All"))
	        {
		        _mInformations.SelectAllLanguages(false);
	        }

	        GUILayout.EndHorizontal();
        }

        private void Scroll(Rect rect)
        {
	        // Draw scroll header
	        GUILayout.BeginVertical();
	        {
		        GUIStyle styleBoxMarginless = GUI.skin.box;
		        styleBoxMarginless.margin = new RectOffset(0, 0, 0, 5);
		        styleBoxMarginless.padding = new RectOffset(0, 0, 0, 0);

		        GUILayout.BeginHorizontal(styleBoxMarginless);
		        {
			        GUILayout.BeginHorizontal();
			        {
				        GUILayout.BeginHorizontal();
				        GUILayout.FlexibleSpace();
				        GUILayout.Label("Name");
				        GUILayout.FlexibleSpace();
				        GUILayout.EndHorizontal();

				        GUILayout.BeginHorizontal();
				        GUILayout.Label("|");
				        GUILayout.EndHorizontal();

				        GUILayout.BeginHorizontal();
				        GUILayout.FlexibleSpace();
				        GUILayout.Label("Code");
				        GUILayout.FlexibleSpace();
				        GUILayout.EndHorizontal();
			        }
			        GUILayout.EndHorizontal();

		        }
		        GUILayout.EndHorizontal();
		        
		        GUILayout.BeginVertical(styleBoxMarginless);
		        
		        // Draw scroll content
		        _mScrollviewPosition = GUILayout.BeginScrollView(_mScrollviewPosition, false, false);
		        List<LocaleLanguageEditor> languages = _mInformations.GetLanguagesMatching(_mInformations.SearchBarText);
		        
		        for (int i = 0; i < languages.Count; i++)
		        {
			        LocaleLanguageEditor language = languages[i];
                    
			        GUIStyle styleSelected = new GUIStyle();
                    GUIStyle styleHorizontal = new GUIStyle();
			        GUIStyle styleHorizontal2 = new GUIStyle();

                    if(EditorGUIUtility.isProSkin)
                    {
                        styleHorizontal.normal.background = MakeTex(new Color(0.2f, 0.2f, 0.2f));
                        styleHorizontal2.normal.background = MakeTex(new Color(0.3f, 0.3f, 0.3f));
                    }
                    else
                    {
                        styleHorizontal.normal.background = MakeTex(new Color(0.8f, 0.8f, 0.8f));
                        styleHorizontal2.normal.background = MakeTex(new Color(0.9f, 0.9f, 0.9f));
                    }


			        GUIStyle styleHorizontal3 = new GUIStyle();
			        styleHorizontal3.normal.background = MakeTex(Color.blue);
			        GUIStyle styleHorizontal4 = new GUIStyle();
			        styleHorizontal4.normal.background = MakeTex(Color.red);
                    styleSelected = (i % 2 == 0) ? (styleHorizontal) : (styleHorizontal2);
			        
			        GUILayout.BeginHorizontal(styleSelected);
			        
			        GUILayout.BeginHorizontal(GUILayout.Width((rect.width / 2)));
			        language._mSelected = GUILayout.Toggle(language._mSelected, language._mName);
			        GUILayout.EndHorizontal();

			        GUILayout.BeginHorizontal();
			        GUILayout.Label(language._mCode);
			        GUILayout.EndHorizontal();
			        
			        GUILayout.EndHorizontal();
		        }
	        
		        GUILayout.EndScrollView();
		        GUILayout.EndVertical();
				
	        }
	        GUILayout.EndVertical();
	        

        }
        
        public static void DrawUILine(Color color, int thickness = 2, int padding = 10)
        {
	        Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding+thickness));
	        r.height = thickness;
	        r.y+=padding/2;
	        r.x-=2;
	        r.width +=6;
	        EditorGUI.DrawRect(r, color);
        }

        public static Texture2D MakeTex(Color color, int width = 1, int height = 1)
        {
	        Color[] pix = new Color[width*height];
 
	        for(int i = 0; i < pix.Length; i++)
		        pix[i] = color;
 
	        Texture2D result = new Texture2D(width, height);
	        result.SetPixels(pix);
	        result.Apply();
 
	        return result;
        }
        
        #endregion		
        #region Abstract / Virtuals / Overrides
        #endregion
		
        #endregion
		
        #endregion
    }
}

