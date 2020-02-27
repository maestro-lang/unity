using UnityEngine;
using Maestro;

public sealed class TriggerCommand : ICommand<Tuple1>
{
	private ExampleTrigger trigger;

	public void Execute(ref Context context, Tuple1 args)
	{
		if (args.value0.asObject is string name)
		{
			if (trigger == null || trigger.name != name)
			{
				foreach (var t in ExampleTrigger.all)
				{
					if (t.name == name)
					{
						trigger = t;
						break;
					}
				}
			}

			if (trigger != null)
			{
				context.PushValue(trigger.HasCollider);
				return;
			}
		}

		context.PushValue(false);
	}
}

public sealed class DelayCommand : ICommand<Tuple1>
{
	private float targetTime = Mathf.Infinity;
	private bool wasActive = false;

	public void Execute(ref Context context, Tuple1 args)
	{
		var delay = 0.0f;
		switch (args.value0.asObject)
		{
			case ValueKind.Int _: delay = args.value0.asNumber.asInt; break;
			case ValueKind.Float _: delay = args.value0.asNumber.asFloat; break;
		}

		var active = false;
		for (var i = 0; i < context.inputCount; i++)
		{
			var input = context.GetInput(i);
			if (input.IsTruthy())
			{
				active = true;
				break;
			}
		}

		if (!wasActive && active)
			targetTime = Time.time + delay;

		for (var i = 0; i < context.inputCount; i++)
			context.PushValue(targetTime < Time.time && active);

		wasActive = active;
	}
}

public sealed class ChangeColorCommand : ICommand<Tuple1>
{
	private ExampleChangeColor changeColor;

	public void Execute(ref Context context, Tuple1 args)
	{
		if (args.value0.asObject is string name)
		{
			if (changeColor == null || changeColor.name != name)
			{
				foreach (var t in ExampleChangeColor.all)
				{
					if (t.name == name)
					{
						changeColor = t;
						break;
					}
				}
			}

			if (changeColor != null)
			{
				var active = false;
				for (var i = 0; i < context.inputCount; i++)
				{
					if (context.GetInput(i).IsTruthy())
					{
						active = true;
						break;
					}
				}

				changeColor.SetColor(active ? Color.blue : Color.gray);
			}
		}
	}
}
