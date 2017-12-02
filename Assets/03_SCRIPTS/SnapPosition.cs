using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnapPosition : MonoBehaviour
{
	public string objectType;

	private GameObject visual;

	private void Awake()
	{
		visual = transform.GetChild( 0 ).gameObject;
		visual.SetActive( false );
	}

	public void ShowSnap()
	{
		visual.SetActive( true );

	}

	public void HideSnap()
	{
		visual.SetActive( false );
	}
}
