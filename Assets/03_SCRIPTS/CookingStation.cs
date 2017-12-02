using UnityEngine;

public class CookingStation : MonoBehaviour
{
	private void OnTriggerEnter( Collider other )
	{
		var plate = other.GetComponentInParent<Plate>();
		if ( plate != null )
		{
			plate.Cook();
		}
	}
}
