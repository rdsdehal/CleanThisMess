using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoExit : MonoBehaviour
{
	int childCount = 0;

	private void OnTriggerEnter( Collider other )
	{
		var child = other.GetComponentInParent<ChildBehaviour>();

		if ( child )
		{
			childCount++;

			if ( childCount >= 3 )
			{
				FindObjectOfType<TutoManager>().EndTuto();
			}
		}
	}
}
