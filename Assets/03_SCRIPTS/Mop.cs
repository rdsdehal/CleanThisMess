using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mop : MonoBehaviour
{
	public ParticleSystem fx;

	private void OnTriggerEnter( Collider other )
	{
		var vomit = other.GetComponentInParent<VomitPile>();

		if ( vomit )
		{
			vomit.Clean();
			fx.transform.position = other.transform.position;
			fx.Play();
		}
	}
}
