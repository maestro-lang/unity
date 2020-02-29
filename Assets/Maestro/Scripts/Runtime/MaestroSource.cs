using System.IO;
using UnityEngine;
using Maestro;

[System.Serializable]
public sealed class MaestroSource
{
	public string path;
	private Assembly assembly;
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
			return engine.TryCompile(path, content, out assembly);

		Debug.LogErrorFormat("Could not load source at '{0}'", FullPath);
		return false;
	}

	public bool Link(MaestroEngine engine)
	{
		if (assembly != null)
			return engine.TryLink(assembly, out executable);

		Debug.LogErrorFormat("Tried to link source '{0}' without compiling it first", path);
		return false;
	}

	public bool Execute(MaestroEngine engine)
	{
		if (executable.assembly != null)
			return engine.TryExecute(executable);

		Debug.LogErrorFormat("Tried to execute source '{0}' without compiling and linking it first", path);
		return false;
	}
}
