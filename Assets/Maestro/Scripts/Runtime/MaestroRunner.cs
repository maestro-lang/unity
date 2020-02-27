using UnityEngine;

public sealed class MaestroRunner : MonoBehaviour
{
	public MaestroEngine engine;
	public MaestroSource source;

	private void Start()
	{
		source.Compile(engine);
		source.Execute(engine);
	}
}
