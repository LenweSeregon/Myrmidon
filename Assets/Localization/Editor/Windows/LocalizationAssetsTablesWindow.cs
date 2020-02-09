using Myrmidon.Editor;
using Myrmidon.Localization;

namespace Myrmidon
{
	using System;
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

			_mScrollviewPosition = new Vector2();
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
		
		#endregion
		
		#endregion
		
		#region Protected / Privates
		
		#region Commons

		private void Buttons(Rect rect)
		{
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Create Table"))
			{
				
			}

			if (GUILayout.Button(("Edit Tables")))
			{
				
			}
			GUILayout.EndHorizontal();
		}

		private void Scroll(Rect rect)
		{
			GUILayout.BeginHorizontal(GUI.skin.box);
			
			_mScrollviewPosition = GUILayout.BeginScrollView(_mScrollviewPosition, false, false);
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
			GUILayout.TextField("");
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
