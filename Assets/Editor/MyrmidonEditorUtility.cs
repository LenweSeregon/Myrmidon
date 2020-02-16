using System.IO;

namespace Myrmidon.Editor
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEditor;
	
	public class MyrmidonEditorUtility : MonoBehaviour
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

		public static void RenameAsset(UnityEngine.Object asset, string toReplace, string replacingText)
		{
			string path = AssetDatabase.GetAssetPath(asset);
			string directoryPart = Path.GetDirectoryName(path);
			string filePart = Path.GetFileName(path);

			if (string.IsNullOrEmpty(directoryPart) == false && string.IsNullOrEmpty(filePart) == false)
			{			
				string filePartRenamed = filePart.Replace(toReplace, replacingText);
				Debug.Log(AssetDatabase.RenameAsset(path, filePartRenamed));
				AssetDatabase.SaveAssets();
			}
		}

		public static void DeleteAsset(UnityEngine.Object asset)
		{
			string path = AssetDatabase.GetAssetPath(asset);
			if (string.IsNullOrEmpty(path) == false)
			{
				AssetDatabase.DeleteAsset(path);
				AssetDatabase.SaveAssets();
			}
		}

		public static void DeleteFolder(string folderPath)
		{
			if (string.IsNullOrEmpty(folderPath) == true)
				return;

			Directory.Delete(folderPath, true);
			AssetDatabase.Refresh();
		}
		
		public static void RenameFolderAt(string folderPath, string folderName, string newFolderName)
		{
			if (string.IsNullOrEmpty(folderPath) == true)
				return;
			
			string folderPathParent = Path.GetDirectoryName(folderPath);
			string directoryName = Path.GetFileName(folderPath);
			if (string.IsNullOrEmpty(folderPathParent) == false && string.IsNullOrEmpty(directoryName) == false)
			{
				string directoryNameNew = directoryName.Replace(folderName, newFolderName);
				string newPath = Path.Combine(folderPathParent, directoryNameNew);
				AssetDatabase.MoveAsset(folderPath, newPath);
				AssetDatabase.SaveAssets();
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
