using System.Linq;

namespace Myrmidon.Localization
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[CreateAssetMenu(fileName = "Localization", menuName = "Myrmidon/Localization/Localization")] 
	public class LocalizationSO : ScriptableObject
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

		[SerializeField] private List<LocalizationLocaleSO> _mLocales = default;
		[SerializeField] private List<LocalizationAssetTableSO> _mTables = default;
		
		#endregion

		#region Internal Fields
		
		#endregion

		#endregion

		//==========================================
		// Properties
		//==========================================
		
		public List<LocalizationLocaleSO> Locales => _mLocales;
		public List<LocalizationAssetTableSO> Tables => _mTables;
		
		//==========================================
		// Methods
		//==========================================

		#region Methods

		#region Constructors / Lifecycle

		#endregion

		#region Publics

		#region Commons
        public void AddLocale(LocalizationLocaleSO locale)
        {
	        if (_mLocales == null)
		        _mLocales = new List<LocalizationLocaleSO>();
	        
            if(_mLocales.Find(x => (x.Language == locale.Language)) == null)
            {
                _mLocales.Add(locale);
            }
        }

        public void AddAssetTable(LocalizationAssetTableSO table)
        {
	        if (_mTables == null)
		        _mTables = new List<LocalizationAssetTableSO>();
	        
	        if (_mTables.Find(x => x.TableName  == table.TableName) == null)
	        {
		        _mTables.Add(table);
	        }
		}

        public void RemoveAssetTable(LocalizationAssetTableSO table)
        {
	        _mTables?.Remove(table);
        }
        
		#endregion

		#region Getters / Setters

        public bool LocaleExists(SystemLanguage language)
        {
            return _mLocales != null && (_mLocales.Find(x => (x.Language == language)) != null);
        }

        public bool AssetsTableExists(string identifier)
        {
	        return _mTables != null && (_mTables.Find(x => x.TableName == identifier) != null);
        }

        public List<string> GetTablesIdentifiers()
        {
	        List<string> identifiers = new List<string>();
	        foreach (LocalizationAssetTableSO table in _mTables)
	        {
		        identifiers.Add(table.TableName);
	        }
	        
	        return identifiers;
        }
        
        public LocalizationLocaleSO GetLocale(SystemLanguage language)
        {
	        return _mLocales.Find(x => x.Language == language);
        }
        
        /*public LocalizationAsset GetAsset(SystemLanguage language, string identifierTable, string identifierAsset)
        {
	        _mTables.TryGetValue(identifierTable, out var table);
	        if (table == null)
	        {
		        return null;
	        }

	        return null;
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
