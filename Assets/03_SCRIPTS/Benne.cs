using UnityEngine;

public class Benne : MonoBehaviour
{
	public ParticleSystem fx;
	AmazonDelivery delivery;

	public AudioSource audiosource;
	public AudioClip clip;

	private void Awake()
	{
		delivery = FindObjectOfType<AmazonDelivery>();
	}

	private void OnTriggerEnter( Collider other )
	{
		var obj = other.GetComponentInParent<MoveableObject>();
		if ( obj && obj.benneable )
		{
			fx.transform.position = obj.transform.position;
			fx.Play();
			audiosource.PlayOneShot( clip );

			if ( obj.benneOmozons )
			{
				var plate = other.GetComponentInParent<Plate>();
				if ( plate != null ) plate.Clean();
				delivery.ScheduleDelivery( obj.gameObject );
				obj.gameObject.SetActive( false );
			}
			else
			{
				Destroy( obj.gameObject );
			}
		}
	}
}