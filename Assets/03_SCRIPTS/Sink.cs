using UnityEngine;

public class Sink : MonoBehaviour
{
	public ParticleSystem splash;

	public AudioSource audioSource;
	public AudioClip clip;

	private void OnTriggerEnter( Collider other )
	{
		var plate = other.GetComponentInParent<Plate>();
		if ( plate != null )
		{
			plate.Clean();
			splash.transform.position = plate.transform.position;
			splash.Play();
			audioSource.PlayOneShot( clip );
		}
	}
}
