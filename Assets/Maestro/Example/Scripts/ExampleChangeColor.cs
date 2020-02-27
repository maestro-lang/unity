using System.Collections.Generic;
using UnityEngine;

public sealed class ExampleChangeColor : MonoBehaviour
{
	public static HashSet<ExampleChangeColor> all = new HashSet<ExampleChangeColor>();

	public Renderer targetRenderer;

	public void SetColor(Color color)
	{
		targetRenderer.material.color = color;
	}

	private void Awake()
	{
		all.Add(this);
	}

	private void OnDestroy()
	{
		all.Remove(this);
	}
}
