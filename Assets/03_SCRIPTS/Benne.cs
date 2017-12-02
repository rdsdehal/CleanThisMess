using UnityEngine;

public class Benne : MonoBehaviour
{
	private void OnTriggerEnter( Collider other )
	{
		var obj = other.GetComponent<MoveableObject>();
		if ( obj )
		{
			Destroy( obj.gameObject );
		}
	}
}