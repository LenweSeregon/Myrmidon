using Myrmidon.Editor;
using Myrmidon.Localization;
using Myrmidon.Localization.Editor;
using NUnit.Framework.Constraints;
using UnityEditor.VersionControl;

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

		private void OnFocus()
		{
			
			if (_mLocalization != null && _mInformations != null)
			{
				foreach (LocalizationLocaleSO locale in _mLocalization.Locales)
				{
					_mInformations.SetLanguage(locale.Language, false);
				}
			}
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
			if (_mLocalization.Tables.Count > 0)
			{
				_mInformations = new AssetTablesInformations(_mLocalization.Tables[0]);
			}
			else
			{
				_mInformations = new AssetTablesInformations(null);
			}

			_mScrollviewPosition = Vector2.zero;

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
			
			MyrmidonEditorLayoutElement tableSelection = new MyrmidonEditorLayoutElement(0, standardHeight * 3, 1, 0);
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

		private void ScrollEditTable(Rect rect)
		{
			GUILayout.BeginHorizontal(GUI.skin.box);
			
			GUILayout.EndScrollView();
			
			GUILayout.EndScrollView();
			
			GUILayout.EndHorizontal();
		}

		private void ScrollAdditional(Rect rect)
		{
			GUILayout.BeginHorizontal(GUI.skin.box);

			
			GUILayout.EndScrollView();
			
			GUILayout.EndHorizontal();
		}

		private void TableSelection(Rect rect)
		{
			GUILayout.BeginVertical();

			LocalizationSO localization = LocalizationUtility.RetrieveLocalization();
			List<string> tablesIdentifiers = localization.GetTablesIdentifiers();

			if (tablesIdentifiers.Count > 0)
			{
				int indexSelected = tablesIdentifiers.IndexOf(_mInformations.TextTableName);
				if (indexSelected < 0)
					indexSelected = 0;
			
				GUILayout.BeginHorizontal();
			
				GUILayout.Label("Table", GUILayout.MaxWidth(100));
				indexSelected = EditorGUILayout.Popup("", indexSelected, tablesIdentifiers.ToArray());
				var tableSelected = localization.Tables[indexSelected];
				if (tableSelected != _mInformations.TableSelected)
				{
					_mInformations.SetTableSelected(localization.Tables[indexSelected]);
				}

				if (GUILayout.Button("Delete", GUILayout.MaxWidth(100)))
				{
					string pathLocalization = AssetDatabase.GetAssetPath(_mLocalization);
					string folderPathLocalization = Path.GetDirectoryName(pathLocalization);
					string folderToDelete = Path.Combine(folderPathLocalization, _mInformations.TableSelected.TableName);
					_mLocalization.RemoveAssetTable(_mInformations.TableSelected);
					MyrmidonEditorUtility.DeleteFolder(folderToDelete);
					Repaint();
				}
			
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				GUILayout.Label("Table name", GUILayout.MaxWidth(100));
				_mInformations.EditingTableName = GUILayout.TextField(_mInformations.EditingTableName);
				if (GUILayout.Button("Rename", GUILayout.MaxWidth(100)))
				{
					string registeredName = _mInformations.RegisteredTableName;
					string editedName = _mInformations.EditingTableName;
					if (string.IsNullOrEmpty(editedName) == false && editedName != registeredName && _mLocalization.AssetsTableExists(editedName) == false)
					{
						string pathLocalization = AssetDatabase.GetAssetPath(_mLocalization);
						string folderPathLocalization = Path.GetDirectoryName(pathLocalization);
						string folderPathTable = Path.Combine(folderPathLocalization, _mInformations.TableSelected.TableName);
						
						MyrmidonEditorUtility.RenameFolderAt(folderPathTable, registeredName, editedName);
						
						_mInformations.RegisteredTableName = _mInformations.EditingTableName;
						_mInformations.TableSelected.TableName = editedName;
						MyrmidonEditorUtility.RenameAsset(_mInformations.TableSelected, registeredName, editedName);
					
						foreach (LocalizationLocaleAssetsSO localeAssets in _mInformations.TableSelected.LocalesAssets)
						{
							MyrmidonEditorUtility.RenameAsset(localeAssets, registeredName, editedName);
						}
					
						AssetDatabase.SaveAssets();
						AssetDatabase.Refresh();
					}
				}
				GUILayout.EndHorizontal();
			}

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

				styleSelected = (i % 2 == 0) ? (styleHorizontal) : (styleHorizontal2);
			        
				GUILayout.BeginHorizontal(styleSelected);
			        
				GUILayout.BeginHorizontal(GUILayout.Width((rect.width / 2)));
				
				LocaleLanguageEditor languageAssociated = _mInformations.GetLanguage(locale.Language);
				bool selected = (languageAssociated != null) ? (languageAssociated._mSelected) : (true);
				selected = GUILayout.Toggle(selected, locale.Name);
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
				_mInformations.SelectAllLanguages(true);
			}

			if (GUILayout.Button(("Select None"), GUILayout.MaxWidth(100)))
			{
				_mInformations.SelectAllLanguages(false);
			}
			
			GUILayout.EndHorizontal();
		}

		private void Footer(Rect rect)
		{
			GUILayout.BeginVertical();
			
			GUILayout.BeginHorizontal();
			
			EditorGUI.BeginChangeCheck();
			
				GUILayout.Label("Key Mapper", GUILayout.MaxWidth(100));
				_mInformations.CreateTableSelected = (LocalizationAssetTableSO) EditorGUILayout.ObjectField(_mInformations.CreateTableSelected, typeof(LocalizationAssetTableSO), false);
				GUILayout.EndHorizontal();

			if (EditorGUI.EndChangeCheck())
			{
				Repaint();
			}

			if (_mInformations.CreateTableSelected == null)
			{
				GUILayout.BeginHorizontal();
				GUILayout.Label("Table name", GUILayout.MaxWidth(100));
				_mInformations.TextTableName = GUILayout.TextField(_mInformations.TextTableName);
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				GUILayout.Label("Type", GUILayout.MaxWidth(100));
				LocalizationAssetType type = LocalizationAssetType.Audio;
				type = (LocalizationAssetType) EditorGUILayout.EnumPopup(type);
				GUILayout.EndHorizontal();
			}

			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Create", GUILayout.MaxWidth(100)))
			{
				if (_mLocalization != null && _mInformations.CreateTableSelected)
				{
					LocalizationAssetTableSO table = _mInformations.CreateTableSelected;
					string tablePath = AssetDatabase.GetAssetPath(table);
					string tableFolderPath = Path.GetDirectoryName(tablePath);

					foreach (LocaleLanguageEditor language in _mInformations.GetLanguagesSelected())
					{
						string potentialPath = Path.Combine(tableFolderPath, table.TableName + " - " + language._mName + ".asset");
						LocalizationLocaleSO locale = _mLocalization.GetLocale(language._mLanguage);
						if (locale != null && table.LocaleAssetExist(language._mLanguage) == false && File.Exists(potentialPath) == false)
						{
							LocalizationLocaleAssetsSO localeAssets = ScriptableObject.CreateInstance<LocalizationLocaleAssetsSO>();
							localeAssets.Init(language._mLanguage);
							table.LocalesAssets.Add(localeAssets);
							
							AssetDatabase.CreateAsset(localeAssets, potentialPath);
							EditorUtility.SetDirty(table);
							AssetDatabase.SaveAssets();
							AssetDatabase.Refresh();
						}
					}
				}
				else if (_mLocalization != null && string.IsNullOrEmpty(_mInformations.TextTableName) == false && _mLocalization.AssetsTableExists(_mInformations.TextTableName) == false)
				{
					List<LocalizationLocaleAssetsSO> localesAssets = new List<LocalizationLocaleAssetsSO>();
					string pathLocalizationAsset = AssetDatabase.GetAssetPath(_mLocalization);
					string pathLocalizationFolder = Path.GetDirectoryName(pathLocalizationAsset);
					string tableName = _mInformations.TextTableName;

					AssetDatabase.CreateFolder(pathLocalizationFolder, tableName);
					
					// Create the sub asset for locale
					foreach (LocaleLanguageEditor language in _mInformations.GetLanguagesSelected())
					{
						LocalizationLocaleSO locale = _mLocalization.GetLocale(language._mLanguage);
						if (locale != null)
						{
							string pathAssetTableLocale = Path.Combine(pathLocalizationFolder, tableName, _mInformations.TextTableName + " - " + language._mName +".asset");
							LocalizationLocaleAssetsSO localeAssets = ScriptableObject.CreateInstance<LocalizationLocaleAssetsSO>();
							localeAssets.Init(language._mLanguage);
							localesAssets.Add(localeAssets);

							AssetDatabase.CreateAsset(localeAssets, pathAssetTableLocale);
						}
					}
					
					// Create the asset table
					string pathAssetTable = Path.Combine(pathLocalizationFolder, tableName , _mInformations.TextTableName + " - Mapper.asset");
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
