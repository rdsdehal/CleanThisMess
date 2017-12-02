using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Benne : MonoBehaviour
{
	private void OnTriggerEnter( Collider other )
	{
		var obj = other.GetComponent<MoveableObject>();
		if ( obj )
		{
			Debug.Log( "IL A JETAY SON OBJEY" );
		}
	}
}
