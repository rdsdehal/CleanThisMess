using UnityEngine;

public class Benne : MonoBehaviour
{
	private void OnTriggerEnter( Collider other )
	{
		var obj = other.GetComponentInParent<MoveableObject>();
		if ( obj && obj.benneable )
		{
			Destroy( obj.gameObject );
		}
	}
}