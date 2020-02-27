using UnityEngine;
using Maestro;

[CreateAssetMenu(menuName = "Maestro Example/Example Command Registry")]
public sealed class ExampleCommandRegistry : MaestroCommandRegistry
{
	public override void RegisterCommands(Engine engine)
	{
		engine.RegisterCommand("delay", () => new DelayCommand());
		engine.RegisterCommand("trigger", () => new TriggerCommand());
		engine.RegisterCommand("change-color", () => new ChangeColorCommand());
	}
}