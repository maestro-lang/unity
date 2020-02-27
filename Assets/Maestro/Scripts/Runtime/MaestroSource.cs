using System.IO;
using UnityEngine;
using Maestro;

[System.Serializable]
public struct MaestroSource
{
	public string path;
	private Executable executable;

	public string FullPath
	{
		get { return string.Concat(Application.streamingAssetsPath, "/", path, ".maestro"); }
	}

	public bool TryLoadContents(out string content)
	{
		try
		{
			content = File.ReadAllText(FullPath);
			return true;
		}
		catch
		{
			content = string.Empty;
			return false;
		}
	}

	public bool Compile(MaestroEngine engine)
	{
		if (TryLoadContents(out var content))
			return engine.TryCompile(path, content, out executable);

		Debug.LogErrorFormat("Could not load source at '{0}'", FullPath);
		return false;
	}

	public bool Execute(MaestroEngine engine)
	{
		if (executable.assembly != null)
			return engine.TryExecute(executable);

		Debug.LogErrorFormat("Tried to execute source '{0}' without compiling it first", path);
		return false;
	}
}
