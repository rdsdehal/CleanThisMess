using UnityEngine;

public class Sink : MonoBehaviour
{
	private void OnTriggerEnter( Collider other )
	{
		var plate = other.GetComponentInParent<Plate>();
		Debug.Log( "fff" );
		if ( plate != null )
		{
			plate.Clean();

		}
	}
}
