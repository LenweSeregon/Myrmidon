namespace Myrmidon.Localization
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class LocalizationAsset
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

		private SystemLanguage _mLanguage;
		private string _mAsset;

		#endregion

		#endregion

		//==========================================
		// Properties
		//==========================================
		#region Properties

		public SystemLanguage Language => _mLanguage;

		public string Asset
		{
			get { return _mAsset; }
			set { _mAsset = value; }
		}
		
		#endregion
		
		//==========================================
		// Methods
		//==========================================

		#region Methods

		#region Constructors / Lifecycle

		public LocalizationAsset(SystemLanguage language, string asset)
		{
			_mLanguage = language;
			_mAsset = asset;
		}
		
		#endregion

		#region Publics

		#region Commons

		#endregion

		#region Getters / Setters
		
		#endregion

		#region Abstracts / Virtuals / Overrides

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

