using UnityEngine;

public sealed class MaestroRunner : MonoBehaviour
{
	public MaestroEngine engine;
	public MaestroSource source;

	private void Start()
	{
		if (!source.Compile(engine))
			enabled = false;
	}

	private void Update()
	{
		source.Execute(engine);
	}
}
