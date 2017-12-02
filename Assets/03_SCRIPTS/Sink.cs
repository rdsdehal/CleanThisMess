using UnityEngine;

public class Sink : MonoBehaviour
{
	private void OnTriggerEnter( Collider other )
	{
		var plate = other.GetComponentInParent<Plate>();
		if ( plate != null )
		{
			plate.Clean();

		}
	}
}
