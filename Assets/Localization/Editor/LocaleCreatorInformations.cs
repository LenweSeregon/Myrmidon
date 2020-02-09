namespace Myrmidon.Localization.Editor
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class LocaleCreatorLanguage
	{
		public string _mName;
		public string _mCode;
		public bool _mSelected;

		public LocaleCreatorLanguage(string name, string code)
		{
			_mName = name;
			_mCode = code;
			_mSelected = false;
		}

		public void SetSelected(bool selected)
		{
			_mSelected = selected;
		}
	}
	
	public class LocaleCreatorInformations
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

		private List<LocaleCreatorLanguage> _mLanguages;
		private string _mSearchBarText;
		
		#endregion
		
		#endregion
		
		//==========================================
		// Properties
		//==========================================

		public List<LocaleCreatorLanguage> Languages => _mLanguages;
		public string SearchBarText
		{
			get { return _mSearchBarText; }
			set { _mSearchBarText = value; }
		}
		
		//==========================================
		// Methods
		//==========================================
		#region Methods
		
		#region Constructors / Lifecycle

		public LocaleCreatorInformations()
		{
			_mSearchBarText = null;
			_mLanguages = new List<LocaleCreatorLanguage>();
			_mLanguages.Add(new LocaleCreatorLanguage(SystemLanguage.French.ToString(), "fr"));
			_mLanguages.Add(new LocaleCreatorLanguage(SystemLanguage.English.ToString(), "en"));
			_mLanguages.Add(new LocaleCreatorLanguage(SystemLanguage.Spanish.ToString(), "sp"));
			_mLanguages.Add(new LocaleCreatorLanguage(SystemLanguage.Italian.ToString(), "it"));
			_mLanguages.Add(new LocaleCreatorLanguage(SystemLanguage.German.ToString(), "de"));
			_mLanguages.Add(new LocaleCreatorLanguage(SystemLanguage.Greek.ToString(), "el"));
			_mLanguages.Add(new LocaleCreatorLanguage(SystemLanguage.Arabic.ToString(), "ar"));
			_mLanguages.Add(new LocaleCreatorLanguage(SystemLanguage.Afrikaans.ToString(), "af"));
			_mLanguages.Add(new LocaleCreatorLanguage(SystemLanguage.Japanese.ToString(), "ja"));
			_mLanguages.Add(new LocaleCreatorLanguage(SystemLanguage.Korean.ToString(), "ko"));
			_mLanguages.Add(new LocaleCreatorLanguage(SystemLanguage.Basque.ToString(), "eu"));
			_mLanguages.Add(new LocaleCreatorLanguage(SystemLanguage.Belarusian.ToString(), "be"));
			_mLanguages.Add(new LocaleCreatorLanguage(SystemLanguage.Bulgarian.ToString(), "bg"));
			_mLanguages.Add(new LocaleCreatorLanguage(SystemLanguage.Catalan.ToString(), "ca"));
			_mLanguages.Add(new LocaleCreatorLanguage(SystemLanguage.Czech.ToString(), "cs"));
			_mLanguages.Add(new LocaleCreatorLanguage(SystemLanguage.Danish.ToString(), "da"));
			_mLanguages.Add(new LocaleCreatorLanguage(SystemLanguage.Estonian.ToString(), "et"));
			_mLanguages.Add(new LocaleCreatorLanguage(SystemLanguage.Faroese.ToString(), "fo"));
			_mLanguages.Add(new LocaleCreatorLanguage(SystemLanguage.Finnish.ToString(), "fi"));
		}
		
		#endregion
		
		#region Publics
		
		#region Commons

		#endregion
		#region Getters / Setters
		
		public void SelectAllLanguages(bool select)
		{
			foreach (LocaleCreatorLanguage language in _mLanguages)
			{
				language.SetSelected(select);
			}
		}

		public List<LocaleCreatorLanguage> GetLanguagesMatching(string text)
		{
			List<LocaleCreatorLanguage> languages = new List<LocaleCreatorLanguage>();
			foreach (LocaleCreatorLanguage language in _mLanguages)
			{
				if (string.IsNullOrEmpty(text) || language._mName.Contains(text) || language._mCode.Contains(text))
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
