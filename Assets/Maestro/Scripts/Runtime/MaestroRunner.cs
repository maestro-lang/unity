using UnityEngine;

public sealed class MaestroRunner : MonoBehaviour
{
	public MaestroEngine engine;
	public MaestroSource[] sources;

	private void Start()
	{
		foreach (var source in sources)
		{
			if (!source.Compile(engine))
				enabled = false;
		}

		if (!enabled)
			return;

		foreach (var source in sources)
		{
			if (!source.Link(engine))
				enabled = false;
		}
	}

	private void Update()
	{
		foreach (var source in sources)
			source.Execute(engine);
	}
}
