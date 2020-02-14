using Myrmidon.Editor;
using Myrmidon.Localization;
using Myrmidon.Localization.Editor;

namespace Myrmidon
{
	using System;
	using System.IO;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEditor;

	public enum LocalizationAssetType
	{
		Texture,
		Audio,
		ScriptableObject,
		String
	}

	public enum LocalizationAssetsTableWindowType
	{
		Create,
		Edit
	}
	
	public class LocalizationAssetsTablesWindow : MyrmidonEditorWindow
	{
		//==========================================
		// Constantes
		//==========================================
		#region Constantes
		
		private const string WINDOW_NAME = "Assets Tables";
		
		#endregion
		
		//==========================================
		// Fields
		//==========================================
		#region Fields
		
		#region Serialized Fields
		#endregion
		
		#region Internal Fields

		private LocalizationAssetsTableWindowType _mType;
		private LocalizationSO _mLocalization;
		private AssetTablesInformations _mInformations;
		private Vector2 _mScrollviewPosition;
		
		#endregion
		
		#endregion
		
		//==========================================
		// Methods
		//==========================================
		#region Methods
		
		#region Constructors / Lifecycle
		
		[MenuItem("Window/Myrmidon/Localization/Assets Tables")]
		private static void OpenLocaleCreatorWindow()
		{
			MyrmidonEditorWindow window = GetWindow<LocalizationAssetsTablesWindow>();
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

			_mLocalization = LocalizationUtility.RetrieveLocalization();
			_mInformations = new AssetTablesInformations(_mLocalization.Tables[0]);
			_mScrollviewPosition = new Vector2();

			foreach (LocalizationLocaleSO locale in _mLocalization.Locales)
			{
				_mInformations.SetLanguage(locale.Language, false);
			}
			
			InitializeWindowAsCreateTable();	
		}
		
		#endregion
		
		#endregion
		
		#region Protected / Privates
		
		#region Commons

		private void InitializeWindowAsCreateTable()
		{
			_mType = LocalizationAssetsTableWindowType.Create;
			
			float standardHeight = EditorGUIUtility.singleLineHeight;

			MyrmidonEditorLayoutElement buttons = new MyrmidonEditorLayoutElement(0, standardHeight, 1, 0);
			buttons.SetDrawAction(Buttons);
			buttons.SetResizables(true, false);
			
			MyrmidonEditorLayoutElement scroll = new MyrmidonEditorLayoutElement(0, 0, 1, 50);
			scroll.SetDrawAction(Scroll);
			scroll.SetResizables(true, true);
	        
			MyrmidonEditorLayoutElement buttonsSelections = new MyrmidonEditorLayoutElement(0, standardHeight, 1, 0);
			buttonsSelections.SetDrawAction(ButtonsSelection);
			buttonsSelections.SetResizables(true, false);
			
			MyrmidonEditorLayoutElement empty = new MyrmidonEditorLayoutElement(0, standardHeight, 1, 0);
			empty.SetResizables(true, false);
	        
			MyrmidonEditorLayoutElement footer = new MyrmidonEditorLayoutElement(0, standardHeight * 5, 1, 0);
			footer.SetDrawAction(Footer);
			footer.SetResizables(true, false);

			MyrmidonEditorLayout layout = new MyrmidonEditorVerticalLayout(new List<MyrmidonEditorLayoutElement> {buttons, scroll, buttonsSelections, empty, footer }, true, true, false);
			layout.SetRect(new Rect(0, 0, position.width, position.height));
			layout.Spacing = 5f;
			layout.SetPadding(10, 10, 10, 10);
			layout.ComputeRects();
			
			_mWindowContainer = layout;
		}

		private void InitializeWindowAsEditTable()
		{
			_mType = LocalizationAssetsTableWindowType.Edit;
			
			float standardHeight = EditorGUIUtility.singleLineHeight;

			MyrmidonEditorLayoutElement buttons = new MyrmidonEditorLayoutElement(0, standardHeight, 1, 0);
			buttons.SetDrawAction(Buttons);
			buttons.SetResizables(true, false);
			
			MyrmidonEditorLayoutElement empty = new MyrmidonEditorLayoutElement(0, standardHeight, 1, 0);
			empty.SetResizables(true, false);
			
			MyrmidonEditorLayoutElement tableSelection = new MyrmidonEditorLayoutElement(0, standardHeight, 1, 0);
			tableSelection.SetDrawAction(TableSelection);
			tableSelection.SetResizables(true, false);
			
			MyrmidonEditorLayoutElement empty2 = new MyrmidonEditorLayoutElement(0, 0, 1, 1);
			empty.SetResizables(true, true);
			
			MyrmidonEditorLayout layout = new MyrmidonEditorVerticalLayout(new List<MyrmidonEditorLayoutElement> {buttons, empty, tableSelection, empty2}, true, true, false);
			layout.SetRect(new Rect(0, 0, position.width, position.height));
			layout.Spacing = 5f;
			layout.SetPadding(10, 10, 10, 10);
			layout.ComputeRects();
			
			_mWindowContainer = layout;
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
		
		private void Buttons(Rect rect)
		{
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Create Table") && _mType == LocalizationAssetsTableWindowType.Edit)
			{
				InitializeWindowAsCreateTable();
				Repaint();
			}

			if (GUILayout.Button("Edit Tables") && _mType == LocalizationAssetsTableWindowType.Create)
			{
				InitializeWindowAsEditTable();
				Repaint();
			}
			GUILayout.EndHorizontal();
		}

		private void TableSelection(Rect rect)
		{
			GUILayout.BeginVertical();

			LocalizationSO localization = LocalizationUtility.RetrieveLocalization();
			List<string> tablesIdentifiers = localization.GetTablesIdentifiers();
			int indexSelected = tablesIdentifiers.IndexOf(_mInformations.TextTableName);
			Debug.Log("INDEX SELECTED : " + indexSelected);
			
			if (indexSelected < 0)
				indexSelected = 0;
			
			GUILayout.BeginHorizontal();
			
			GUILayout.Label("Table", GUILayout.MaxWidth(100));
			indexSelected = EditorGUILayout.Popup("", indexSelected, tablesIdentifiers.ToArray());
			_mInformations.SetTableSelected(localization.Tables[indexSelected]);
			GUILayout.EndHorizontal();

			GUILayout.EndVertical();
		}

		private void Scroll(Rect rect)
		{
			GUILayout.BeginHorizontal(GUI.skin.box);
			
			_mScrollviewPosition = GUILayout.BeginScrollView(_mScrollviewPosition, false, false);

			List<LocalizationLocaleSO> locales = _mLocalization.Locales;
			for (int i = 0; i < locales.Count; i++)
			{
				LocalizationLocaleSO locale = locales[i];
                    
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
				
				bool selected = GUILayout.Toggle(_mInformations.GetLanguage(locale.Language)._mSelected, locale.Name);
				_mInformations.SetLanguage(locale.Language, selected);
				
				GUILayout.EndHorizontal();
			        
				GUILayout.EndHorizontal();
			}
			
			GUILayout.EndScrollView();
			
			GUILayout.EndHorizontal();
		}

		private void ButtonsSelection(Rect rect)
		{
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Select All", GUILayout.MaxWidth(100)))
			{
				
			}

			if (GUILayout.Button(("Select None"), GUILayout.MaxWidth(100)))
			{
				
			}
			
			GUILayout.EndHorizontal();
		}

		private void Footer(Rect rect)
		{
			GUILayout.BeginVertical();
			
			GUILayout.BeginHorizontal();
			GUILayout.Label("Table name", GUILayout.MaxWidth(100));
			_mInformations.TextTableName = GUILayout.TextField(_mInformations.TextTableName);
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			GUILayout.Label("Key Mapper", GUILayout.MaxWidth(100));
			EditorGUILayout.ObjectField(null, typeof(LocalizationKeyMapperSO), false);
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal();
			GUILayout.Label("Type", GUILayout.MaxWidth(100));
			LocalizationAssetType type = LocalizationAssetType.Audio;
			type = (LocalizationAssetType) EditorGUILayout.EnumPopup(type);
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Create", GUILayout.MaxWidth(100)))
			{
				if (_mLocalization != null && string.IsNullOrEmpty(_mInformations.TextTableName) == false && _mLocalization.AssetsTableExists(_mInformations.TextTableName) == false)
				{
					List<LocalizationLocaleAssetsSO> localesAssets = new List<LocalizationLocaleAssetsSO>();
					string pathLocalization = AssetDatabase.GetAssetPath(_mLocalization);
					
					// Create the sub asset for locale
					foreach (LocaleLanguageEditor language in _mInformations.GetLanguagesSelected())
					{
						LocalizationLocaleSO locale = _mLocalization.GetLocale(language._mLanguage);
						if (locale != null)
						{
							string path = AssetDatabase.GetAssetPath(locale);
							LocalizationLocaleAssetsSO localeAssets = ScriptableObject.CreateInstance<LocalizationLocaleAssetsSO>();
							localeAssets.Init(language._mLanguage);
							localeAssets.name = _mInformations.TextTableName + " - " + language._mName;
							localesAssets.Add(localeAssets);
							AssetDatabase.AddObjectToAsset(localeAssets, locale);
							EditorUtility.SetDirty(localeAssets);
							AssetDatabase.SaveAssets();
							AssetDatabase.Refresh();
						}
					}
					
					// Create the asset table
					string pathAssetTable = Path.Combine(Path.GetDirectoryName(pathLocalization), _mInformations.TextTableName + " - Mapper.asset");
					LocalizationAssetTableSO table = ScriptableObject.CreateInstance<LocalizationAssetTableSO>();
					table.Init(_mInformations.TextTableName, localesAssets);
					
					_mLocalization.AddAssetTable(table);
					AssetDatabase.CreateAsset(table, pathAssetTable);
					EditorUtility.SetDirty(_mLocalization);
					AssetDatabase.SaveAssets();
					AssetDatabase.Refresh();
				}
			}
			GUILayout.EndHorizontal();
			
			GUILayout.EndVertical();
		}
		
		#endregion		
		#region Abstract / Virtuals / Overrides
		#endregion
		
		#endregion
		
		#endregion
	}
}
