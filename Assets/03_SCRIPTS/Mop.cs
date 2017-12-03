using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mop : MonoBehaviour
{
	private void OnTriggerEnter( Collider other )
	{
		var vomit = other.GetComponentInParent<VomitPile>();

		if ( vomit )
		{
			vomit.Clean();
		}
	}
}
