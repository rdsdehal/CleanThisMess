using UnityEngine;

public class CookingStation : MonoBehaviour
{
	public ParticleSystem fx;

	public AudioSource audiosource;
	public AudioClip clip;
	public AudioClip unluckyPlat;

	private void OnTriggerEnter( Collider other )
	{

		var plate = other.GetComponentInParent<Plate>();
		if ( plate != null )
		{
			plate.Cook();
			fx.transform.position = plate.transform.position;
			fx.Play();

			if ( plate.plateState == Plate.PlateState.Garbage )
			{
				audiosource.PlayOneShot( unluckyPlat );
			}
			else
			{
				audiosource.PlayOneShot( clip );
			}
		}

		var moveableObject = other.GetComponentInParent<MoveableObject>();
		if ( moveableObject != null && moveableObject.canBurn )
		{
			moveableObject.Burn();
			fx.transform.position = moveableObject.transform.position;
			fx.Play();
			audiosource.PlayOneShot( clip );
		}
	}
}
