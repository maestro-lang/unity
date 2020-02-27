using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(MaestroSource))]
public sealed class MaestroSourceDrawer : PropertyDrawer
{
	private GUIContent[] paths = null;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		if (paths == null)
		{
			var guids = AssetDatabase.FindAssets("", new string[] { MaestroScriptCreator.ScriptsPath });
			paths = new GUIContent[guids.Length];

			var pathsIndex = 0;
			foreach (var guid in guids)
			{
				const string extension = ".maestro";
				var path = AssetDatabase.GUIDToAssetPath(guid);
				if (!path.EndsWith(extension) || !path.StartsWith(MaestroScriptCreator.ScriptsPath))
					continue;

				var name = path.Substring(
					MaestroScriptCreator.ScriptsPath.Length + 1,
					path.Length - extension.Length - MaestroScriptCreator.ScriptsPath.Length - 1
				);
				paths[pathsIndex++] = new GUIContent(name);
			}

			if (pathsIndex < paths.Length)
			{
				var old = paths;
				paths = new GUIContent[pathsIndex];
				System.Array.Copy(old, paths, paths.Length);
			}
		}

		if (paths.Length == 0)
		{
			var labelPosition = position;
			labelPosition.width = EditorGUIUtility.labelWidth;
			EditorGUI.LabelField(labelPosition, label);

			var helpBoxPosition = position;
			helpBoxPosition.xMin += labelPosition.width;
			EditorGUI.HelpBox(helpBoxPosition, $"No Maestro script in '{MaestroScriptCreator.ScriptsPath}'", MessageType.Warning);
		}
		else
		{
			var pathProperty = property.FindPropertyRelative("path");

			var index = -1;
			for (var i = 0; i < paths.Length; i++)
			{
				if (paths[i].text == pathProperty.stringValue)
				{
					index = i;
					break;
				}
			}

			index = EditorGUI.Popup(position, label, index, paths);

			if (index >= 0)
				pathProperty.stringValue = paths[index].text;
		}
	}
}
