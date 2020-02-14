namespace Myrmidon.Localization.Editor
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class LocaleLanguageEditor
	{
		public SystemLanguage _mLanguage;
		public string _mName;
		public string _mCode;
		public bool _mSelected;

		public LocaleLanguageEditor(SystemLanguage language, string code)
		{
			_mLanguage = language;
			_mName = language.ToString();
			_mCode = code;
			_mSelected = false;
		}

		public LocaleLanguageEditor(SystemLanguage language)
		{
			_mLanguage = language;
			_mName = language.ToString();
			_mSelected = false;
			_mCode = null;
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

		private List<LocaleLanguageEditor> _mLanguages;
		private string _mSearchBarText;
		
		#endregion
		
		#endregion
		
		//==========================================
		// Properties
		//==========================================

		public List<LocaleLanguageEditor> Languages => _mLanguages;
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
			_mLanguages = new List<LocaleLanguageEditor>();
			_mLanguages.Add(new LocaleLanguageEditor(SystemLanguage.French, "fr"));
			_mLanguages.Add(new LocaleLanguageEditor(SystemLanguage.English, "en"));
			_mLanguages.Add(new LocaleLanguageEditor(SystemLanguage.Spanish, "sp"));
			_mLanguages.Add(new LocaleLanguageEditor(SystemLanguage.Italian, "it"));
			_mLanguages.Add(new LocaleLanguageEditor(SystemLanguage.German, "de"));
			_mLanguages.Add(new LocaleLanguageEditor(SystemLanguage.Greek, "el"));
			_mLanguages.Add(new LocaleLanguageEditor(SystemLanguage.Arabic, "ar"));
			_mLanguages.Add(new LocaleLanguageEditor(SystemLanguage.Afrikaans, "af"));
			_mLanguages.Add(new LocaleLanguageEditor(SystemLanguage.Japanese, "ja"));
			_mLanguages.Add(new LocaleLanguageEditor(SystemLanguage.Korean, "ko"));
			_mLanguages.Add(new LocaleLanguageEditor(SystemLanguage.Basque, "eu"));
			_mLanguages.Add(new LocaleLanguageEditor(SystemLanguage.Belarusian, "be"));
			_mLanguages.Add(new LocaleLanguageEditor(SystemLanguage.Bulgarian, "bg"));
			_mLanguages.Add(new LocaleLanguageEditor(SystemLanguage.Catalan, "ca"));
			_mLanguages.Add(new LocaleLanguageEditor(SystemLanguage.Czech, "cs"));
			_mLanguages.Add(new LocaleLanguageEditor(SystemLanguage.Danish, "da"));
			_mLanguages.Add(new LocaleLanguageEditor(SystemLanguage.Estonian, "et"));
			_mLanguages.Add(new LocaleLanguageEditor(SystemLanguage.Faroese, "fo"));
			_mLanguages.Add(new LocaleLanguageEditor(SystemLanguage.Finnish, "fi"));
		}
		
		#endregion
		
		#region Publics
		
		#region Commons

		#endregion
		#region Getters / Setters
		
		public void SelectAllLanguages(bool select)
		{
			foreach (LocaleLanguageEditor language in _mLanguages)
			{
				language.SetSelected(select);
			}
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

		public List<LocaleLanguageEditor> GetLanguagesMatching(string text)
		{
			List<LocaleLanguageEditor> languages = new List<LocaleLanguageEditor>();
			foreach (LocaleLanguageEditor language in _mLanguages)
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
