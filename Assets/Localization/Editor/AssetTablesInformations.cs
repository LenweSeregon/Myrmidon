﻿using System.Runtime.InteropServices;

namespace Myrmidon.Localization.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AssetTablesInformations
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
        private List<LocaleLanguageEditor> _mLanguages;

        #endregion

        #endregion
	
        //==========================================
        // Properties
        //==========================================
        #region Properties

        public string EditingTableName;
        public string RegisteredTableName;
        public string TextTableName;
        public LocalizationAssetTableSO TableSelected;
        public LocalizationAssetTableSO CreateTableSelected;

        #endregion
        
        //==========================================
        // Methods
        //==========================================

        #region Methods

        #region Constructors / Lifecycle

        public AssetTablesInformations(LocalizationAssetTableSO tableSelected)
        {
	        TableSelected = tableSelected;
	        CreateTableSelected = null;
	        EditingTableName = (tableSelected != null) ? (tableSelected.TableName) : ("");
	        RegisteredTableName = (tableSelected != null) ? (tableSelected.TableName) : ("");
	        TextTableName = (tableSelected != null) ? (tableSelected.TableName) : ("");
	        _mLanguages = new List<LocaleLanguageEditor>();
        }
        
        #endregion

        #region Publics

        #region Commons

        public void SelectAllLanguages(bool select)
        {
	        foreach (LocaleLanguageEditor language in _mLanguages)
	        {
		        language.SetSelected(select);
	        }
        }
        
        public void SetTableSelected(LocalizationAssetTableSO tableSelected)
        {
	        TableSelected = tableSelected;
	        TextTableName = tableSelected.TableName;
	        RegisteredTableName = tableSelected.TableName;
	        EditingTableName = tableSelected.TableName;
        }
        
        public void SetLanguage(SystemLanguage systemLanguage, bool selected)
        {
	        LocaleLanguageEditor language = _mLanguages.Find(x => x._mLanguage == systemLanguage);
	        if (language == null)
	        {
		        language = new LocaleLanguageEditor(systemLanguage);
		        _mLanguages.Add(language);
	        }

	        language.SetSelected(selected);
        }

        #endregion

        #region Getters / Setters

        public LocaleLanguageEditor GetLanguage(SystemLanguage language)
        {
	        return _mLanguages.Find(x => x._mLanguage == language);
        }
        
        public List<LocaleLanguageEditor> GetLanguagesSelected()
        {
	        List<LocaleLanguageEditor> languages = new List<LocaleLanguageEditor>();
	        foreach (LocaleLanguageEditor language in _mLanguages)
	        {
		        if (language._mSelected)
		        {
			        languages.Add(language);
		        }
	        }

	        return languages;
        }

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

