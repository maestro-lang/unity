using UnityEngine;

public sealed class MaestroRunner : MonoBehaviour
{
	public MaestroEngine engine;
	[Multiline]
	public string source;

	private void Start()
	{
		engine.TryCompile("source", source, out var executable);
		engine.TryExecute(executable);
	}
}
