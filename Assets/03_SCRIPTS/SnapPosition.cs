using UnityEngine;

public class SnapPosition : MonoBehaviour
{
	public string objectType;
	public float lerpSpeed;
	public Color defaultColor;
	public Color selectedColor;

	private GameObject visual;
	private Material holoMat;
	private Color targetColor;

	private void Awake()
	{
		visual = transform.GetChild( 0 ).gameObject;
		visual.SetActive( false );
		holoMat = visual.GetComponent<Renderer>().sharedMaterial;
		targetColor = defaultColor;
	}

	private void LateUpdate()
	{
		holoMat.color = Color.Lerp( holoMat.color, targetColor, Time.deltaTime * lerpSpeed );
		targetColor = defaultColor;
	}

	public void ShowSnap()
	{
		visual.SetActive( true );
	}

	public void HideSnap()
	{
		visual.SetActive( false );
	}

	public void Select()
	{
		targetColor = selectedColor;
	}
}
