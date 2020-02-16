namespace Myrmidon.Localization.Editor
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class LocalizationTexturesEditor : LocalizationTableEditor
	{
		//==========================================
		// Constantes
		//==========================================
		#region Constantes
		#endregion


		//==========================================
		// Fields
		//==========================================
		#region Fields
		
		#region Serialized Fields
		#endregion
		
		#region Internal Fields
		#endregion
		
		#endregion
		
		//==========================================
		// Methods
		//==========================================
		#region Methods
		
		#region Constructors / Lifecycle
		#endregion
		
		#region Publics
		
		#region Commons
		#endregion
		#region Getters / Setters
		#endregion
		#region Abstracts / Virtuals / Overrides

		public override void DrawScrollRectAssets(Rect rect)
		{
			GUILayout.BeginHorizontal(GUI.skin.box);

			_mScrollViewEditAssetsPosition = GUILayout.BeginScrollView(_mScrollViewEditAssetsPosition, false, false);
			
			GUILayout.EndScrollView();
			
			GUILayout.EndHorizontal();
		}

		public override void DrawScrollRectAdditionals(Rect rect)
		{
			GUILayout.BeginHorizontal(GUI.skin.box);

			_mScrollViewAdditionalsPosition = GUILayout.BeginScrollView(_mScrollViewAdditionalsPosition, false, false);
			
			GUILayout.EndScrollView();
			
			GUILayout.EndHorizontal();
		}
		
		#endregion
		
		#endregion
		
		#region Protected / Privates
		
		#region Commons
		#endregion		
		#region Abstract / Virtuals / Overrides
		#endregion
		
		#endregion
		
		#endregion
	}
}
