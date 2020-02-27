using UnityEditor;
using System.IO;

public static class MaestroScriptCreator
{
	[MenuItem("Assets/Create/Maestro/Maestro Script", false, 10)]
	public static void CreateScript()
	{
		var path = "Assets/";
		if (Selection.activeObject != null)
		{
			path = AssetDatabase.GetAssetPath(Selection.activeObject);
			if (!AssetDatabase.IsValidFolder(path))
				path = Path.GetDirectoryName(path);
		}

		path = Path.Combine(path, "MaestroScript.maestro");
		ProjectWindowUtil.CreateAssetWithContent(path, "");
	}
}
