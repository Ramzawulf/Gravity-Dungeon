﻿using System.Collections;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class SceneItems : Editor {

	protected static string generatedFileName = "GeneratedSceneMenu";
	protected static string fileExtension = ".cs";
	protected static string scenePath = "/_RBS6Nations/Scenes/";

	static SceneItems(){
		//EditorApplication.update += GenerateMenuItems;
	}

	private static string[] GetSceneNames(){
		string fullStringPath = Application.dataPath + scenePath;
		string[] filePaths = System.IO.Directory.GetFiles (fullStringPath, "*.unity");
		string[] sceneNames = new string[filePaths.Length];

		for (int i = 0; i < filePaths.Length; i++) {
			sceneNames [i] = Path.GetFileNameWithoutExtension (filePaths [i]);
		}
		return sceneNames;
	}

	private static void GenerateMenuItems(){

		EditorApplication.update -= GenerateMenuItems;
		string scriptFile = Application.dataPath + "/Editor/"+ generatedFileName + fileExtension;

		string[] sceneNames = GetSceneNames ();

		StringBuilder sb = new StringBuilder ();
		sb.AppendLine ("// This is an autogenerated class");
		sb.AppendLine ("using UnityEngine;");
		sb.AppendLine ("using UnityEditor;");
		sb.AppendLine ("using UnityEditor.SceneManagement;");
		sb.AppendLine ("public class "+generatedFileName+" : Editor{");
		sb.AppendLine ("");
		//Generate the menu items

		for (int i = 0; i < sceneNames.Length; i++) {
			sb.AppendLine ("[MenuItem(\"Scenes/" + sceneNames[i]+"\")]");
			sb.AppendLine ("static void OpenScene"+i+"(){");
			sb.AppendLine ("if(EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()){");
			sb.AppendLine ("EditorSceneManager.OpenScene (\"Assets" +scenePath+ sceneNames [i] + ".unity\");");
			sb.AppendLine ("}");//Close the if
			sb.AppendLine ("}");//Close the method
			sb.AppendLine ("");
		}

		sb.AppendLine ("");
		sb.AppendLine ("}");//Close the class

		if(System.IO.File.Exists (scriptFile))
			System.IO.File.Delete (scriptFile);
		System.IO.File.WriteAllText (scriptFile, sb.ToString (), System.Text.Encoding.UTF8);
		//AssetDatabase.ImportAsset ("Assets/Editor/"+generatedFileName+fileExtension);
	}
}
