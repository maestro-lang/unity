using UnityEngine;
using Maestro;
using Maestro.StdLib;
using System.Text;

public sealed class MaestroRunner : MonoBehaviour
{
	[Multiline]
	public string source;

	private Engine engine = new Engine();
	private StringBuilder cachedStringBuilder = new StringBuilder();

	private void Start()
	{
		engine.RegisterStandardCommands(t => Debug.Log(t));
		var compileResult = engine.CompileSource(new Source("source", source), Mode.Release);
		if (compileResult.TryGetExecutable(out var executable))
		{
			using (var scope = engine.ExecuteScope())
			{
				var executeResult = scope.Execute(executable);
				if (executeResult.error.isSome)
				{
					executeResult.FormatError(cachedStringBuilder);
					executeResult.FormatCallStackTrace(cachedStringBuilder);
					Debug.LogError(cachedStringBuilder);
				}
			}
		}
		else
		{

			compileResult.FormatErrors(cachedStringBuilder);
			Debug.LogError(cachedStringBuilder);
		}
	}
}
