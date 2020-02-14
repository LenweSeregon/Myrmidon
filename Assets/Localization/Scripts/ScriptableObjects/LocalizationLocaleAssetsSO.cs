namespace Myrmidon.Localization
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[CreateAssetMenu(fileName = "LocalizationLocaleAssets", menuName = "Myrmidon/Localization/LocaleAssets")] 
	public class LocalizationLocaleAssetsSO : ScriptableObject
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

		[SerializeField] private SystemLanguage _mLanguage;
		[SerializeField] private List<LocalizationAsset> _mAsset;

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

		public void Init(SystemLanguage language)
		{
			_mLanguage = language;
			_mAsset = new List<LocalizationAsset>();
		}

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