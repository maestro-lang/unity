using UnityEditor;
using System.IO;
using UnityEngine;

public static class MaestroScriptCreator
{
	public const string ScriptsPath = "Assets/StreamingAssets";

	[MenuItem("Assets/Create/Maestro/Maestro Script", false, 84)]
	public static void CreateScript()
	{
		var path = ScriptsPath;
		if (Selection.activeObject != null)
		{
			path = AssetDatabase.GetAssetPath(Selection.activeObject);
			if (!AssetDatabase.IsValidFolder(path))
				path = Path.GetDirectoryName(path);
		}

		path = Path.Combine(path, "MaestroScript.maestro");
		ProjectWindowUtil.CreateAssetWithContent(path, "");

		if (!path.StartsWith(ScriptsPath))
			Debug.LogWarningFormat("Maestro script path '{0}' is outside of '{1}'", path, ScriptsPath);
	}
}
