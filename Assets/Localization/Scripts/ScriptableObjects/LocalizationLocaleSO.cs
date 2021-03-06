﻿namespace Myrmidon.Localization
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[CreateAssetMenu(fileName = "LocalizationLocale", menuName = "Myrmidon/Localization/Locale")] 
	public class LocalizationLocaleSO : ScriptableObject
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
        [SerializeField] private SystemLanguage _mLanguage = default;
        [SerializeField] private string _mName = default;
        [SerializeField] private string _mIdentifier = default;
        #endregion

        #region Internal Fields

        #endregion

        #endregion

        //==========================================
        // Properties
        //==========================================
        #region Properties

        public SystemLanguage Language => _mLanguage;
        public string Name => _mName;
        public string Identifier => _mIdentifier;
        
        #endregion

        //==========================================
        // Methods
        //==========================================
        #region Methods

        #region Constructors / Lifecycle
        #endregion

        #region Publics

        #region Commons
        public void Init(SystemLanguage language, string name, string identifier)
        {
	        _mLanguage = language;
            _mName = name;
            _mIdentifier = identifier;
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
