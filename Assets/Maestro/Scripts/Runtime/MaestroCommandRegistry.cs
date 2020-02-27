using Maestro;
using UnityEngine;

public abstract class MaestroCommandRegistry : ScriptableObject
{
	public abstract void RegisterCommands(Engine engine);
}
