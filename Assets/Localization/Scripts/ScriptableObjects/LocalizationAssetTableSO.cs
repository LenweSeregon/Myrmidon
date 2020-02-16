namespace Myrmidon.Localization
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[CreateAssetMenu(fileName = "LocalizationAssetTable", menuName = "Myrmidon/Localization/AssetTable")] 
	public class LocalizationAssetTableSO : ScriptableObject
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
		// Properties
		//==========================================
		#region Properties

		public string TableName;
		public List<LocalizationLocaleAssetsSO> LocalesAssets;
		
		#endregion

		//==========================================
		// Methods
		//==========================================

		#region Methods

		#region Constructors / Lifecycle

		#endregion

		#region Publics

		#region Commons

		public void Init(string tableName, List<LocalizationLocaleAssetsSO> localesAssets)
		{
			TableName = tableName;
			LocalesAssets = localesAssets;
		}

		#endregion

		#region Getters / Setters

		public bool LocaleAssetExist(SystemLanguage language)
		{
			return LocalesAssets.Find(x => x.Language == language);
		}

		/*public void SetAsset()
		{
			
		}

		public LocalizationAsset GetAsset( SystemLanguage language, string identifier)
		{
			if (_mAssets.ContainsKey(identifier) == false)
				return null;

			List<LocalizationAsset> assetsInDictionary = _mAssets[identifier];
			LocalizationAsset localeAsset = assetsInDictionary.Find(x => x.Language == language);
			return localeAsset;
		}*/

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

