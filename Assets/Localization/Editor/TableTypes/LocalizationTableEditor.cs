namespace Myrmidon.Localization.Editor
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEditor;

	public abstract class LocalizationTableEditor
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
		
		protected Vector2 _mScrollViewEditAssetsPosition;
		protected Vector2 _mScrollViewAdditionalsPosition;
		#endregion
		
		#endregion
		
		//==========================================
		// Methods
		//==========================================
		#region Methods
		
		#region Constructors / Lifecycle

		public LocalizationTableEditor()
		{
			_mScrollViewEditAssetsPosition = Vector2.zero;
			_mScrollViewAdditionalsPosition = Vector2.zero;
		}
		
		#endregion
		
		#region Publics
		
		#region Commons
		#endregion
		#region Getters / Setters
		#endregion
		#region Abstracts / Virtuals / Overrides

		public abstract void DrawScrollRectAssets(Rect rect);
		public abstract void DrawScrollRectAdditionals(Rect rect);
		
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
