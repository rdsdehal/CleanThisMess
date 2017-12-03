using System.Collections.Generic;
using UnityEngine;


public class GlowingObject : MonoBehaviour
{
	public Color outlineColor;
	public Renderer[] renderers { get; private set; }

	private void Awake()
	{
		List<Renderer> remders = new List<Renderer>();
		remders.AddRange( GetComponentsInChildren<MeshRenderer>() );
		remders.AddRange( GetComponentsInChildren<SkinnedMeshRenderer>() );
		renderers = remders.ToArray();
	}
}