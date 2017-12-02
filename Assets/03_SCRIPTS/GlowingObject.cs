using UnityEngine;


public class GlowingObject : MonoBehaviour
{
	public Color outlineColor;
	public Renderer[] renderers { get; private set; }

	private void Awake()
	{
		renderers = GetComponentsInChildren<Renderer>();
	}
}