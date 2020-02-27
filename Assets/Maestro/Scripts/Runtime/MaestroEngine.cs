using UnityEngine;
using Maestro;
using Maestro.StdLib;
using Maestro.Debug;
using System.Text;

[CreateAssetMenu(menuName = "Maestro/Maestro Engine", order = 84)]
public sealed class MaestroEngine : ScriptableObject
{
	public Mode mode = Mode.Debug;
	public bool registerStdLib = true;
	public MaestroCommandRegistry[] commandRegistries;

	private Engine engine;
	private Debugger debugger;
	private StringBuilder cachedStringBuilder = new StringBuilder();

	public bool TryCompile(string sourceName, string sourceContent, out Executable executable)
	{
		var source = new Source(sourceName, sourceContent);
		var compileResult = engine.CompileSource(source, mode);
		if (compileResult.TryGetExecutable(out executable))
		{
			return true;
		}
		else
		{
			cachedStringBuilder.Clear();
			compileResult.FormatErrors(cachedStringBuilder);
			Debug.LogError(cachedStringBuilder);

			return false;
		}
	}

	public bool TryExecute(Executable executable)
	{
		using (var scope = engine.ExecuteScope())
		{
			var executeResult = scope.Execute(executable);
			if (executeResult.error.isSome)
			{
				cachedStringBuilder.Clear();
				executeResult.FormatError(cachedStringBuilder);
				executeResult.FormatCallStackTrace(cachedStringBuilder);
				Debug.LogError(cachedStringBuilder);

				return false;
			}

			return true;
		}
	}

	private void OnEnable()
	{
		engine = new Engine();

		if (registerStdLib)
		{
			engine.RegisterStandardCommands(Debug.Log);
			engine.RegisterOperationCommands();
			engine.RegisterTypeCommands();

			foreach (var registry in commandRegistries)
				registry.RegisterCommands(engine);
		}

		if (mode == Mode.Debug)
		{
			debugger = new Debugger();
			debugger.Start(47474);
			engine.SetDebugger(debugger);
		}
	}

	private void OnDisable()
	{
		if (debugger != null)
			debugger.Stop();
	}
}
