using UnityEngine;

public class CookingStation : MonoBehaviour
{
	public ParticleSystem fx;
	private void OnTriggerEnter( Collider other )
	{

		var plate = other.GetComponentInParent<Plate>();
		if ( plate != null )
		{
			plate.Cook();
			fx.transform.position = plate.transform.position;
			fx.Play();
		}

		var moveableObject = other.GetComponentInParent<MoveableObject>();
		if ( moveableObject != null && moveableObject.canBurn )
		{
			moveableObject.Burn();
			fx.transform.position = moveableObject.transform.position;
			fx.Play();
		}
	}
}
