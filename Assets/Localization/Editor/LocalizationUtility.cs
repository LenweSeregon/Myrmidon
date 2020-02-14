namespace Myrmidon.Localization.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;

    public class LocalizationUtility : MonoBehaviour
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

        public static LocalizationSO RetrieveLocalization()
        {
	        string[] guids = AssetDatabase.FindAssets("t:LocalizationSO");

	        if (guids.Length > 0)
	        {
		        string guid = guids[0];
		        string path = AssetDatabase.GUIDToAssetPath(guid);
		        return AssetDatabase.LoadAssetAtPath<LocalizationSO>(path);
	        }
	        else
	        {
		        return null;
	        }
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