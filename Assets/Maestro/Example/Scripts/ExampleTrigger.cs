using System.Collections.Generic;
using UnityEngine;

public sealed class ExampleTrigger : MonoBehaviour
{
	public static HashSet<ExampleTrigger> all = new HashSet<ExampleTrigger>();

	private int colliderCount = 0;

	public bool HasCollider
	{
		get { return colliderCount > 0; }
	}

	private void Awake()
	{
		all.Add(this);
	}

	private void OnDestroy()
	{
		all.Remove(this);
	}

	private void OnTriggerEnter(Collider collider)
	{
		colliderCount += 1;
	}

	private void OnTriggerExit(Collider collider)
	{
		colliderCount -= 1;
	}
}
