using UnityEngine;

public class Sink : MonoBehaviour
{
	public ParticleSystem splash;

	public AudioSource audioSource;
	public AudioClip clip;
	public AudioClip unluckyclip;

	private void OnTriggerEnter( Collider other )
	{
		var plate = other.GetComponentInParent<Plate>();
		if ( plate != null )
		{
			if ( plate.plateState == Plate.PlateState.Garbage )
			{
				audioSource.PlayOneShot( unluckyclip );
			}
			else
			{
				plate.Clean();
				splash.transform.position = plate.transform.position;
				splash.Play();
				audioSource.PlayOneShot( clip );
			}
		}
	}
}
